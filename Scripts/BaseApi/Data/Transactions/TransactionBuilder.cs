using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Base.Config;
using Base.Data.Assets;
using Base.Data.Operations;
using Base.Data.Properties;
using Base.Keys;
using Base.Storage;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using RSG;
using Tools.Hash;
using Tools.HexBinDec;
using Tools.Json;
using Tools.Time;


namespace Base.Data.Transactions
{
    public class TransactionBuilder : SerializableObject
    {
        protected static DateTime headBlockTime = TimeTool.ZeroTime();

        private ushort referenceBlockNumber = ushort.MinValue;
        private uint referenceBlockPrefix = uint.MinValue;
        private DateTime expiration = TimeTool.ZeroTime();
        private bool signed = false;
        private byte[] buffer = new byte[0];

        private readonly List<OperationData> operations = new List<OperationData>();
        private readonly List<byte[]> signatures = new List<byte[]>();
        private readonly List<KeyPair> signerKeys = new List<KeyPair>();


        public ushort ReferenceBlockNumber => referenceBlockNumber;

        public uint ReferenceBlockPrefix => referenceBlockPrefix;

        public DateTime Expiration => expiration;

        public OperationData[] Operations => operations.ToArray();

        public byte[][] Signatures => signatures.ToArray();

        public TransactionBuilder() { }

        public TransactionBuilder AddOperation(OperationData operation)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("AddOperation... Already finalized");
            }
            if (operation.Fee.IsNull())
            {
                operation.Fee = new AssetData(0, SpaceTypeId.CreateOne(SpaceType.Asset));
            }
            if (operation.Type.Equals(ChainTypes.Operation.ProposalCreate))
            {
                var proposalCreateOperation = (ProposalCreateOperationData)operation;
                if (proposalCreateOperation.ExpirationTime.IsZero())
                {
                    proposalCreateOperation.ExpirationTime = TimeTool.ZeroTime().AddSeconds(BaseExpirationSeconds + ChainConfig.ExpireInSecondsProposal);
                }
            }
            operations.Add(operation);
            return this;
        }

        // Typically this is called automatically just prior to signing. Once finalized this transaction can not be changed.
        private IPromise Finalize()
        {
            if (IsFinalized)
            {
                return Promise.Rejected(new InvalidOperationException("Finalize... Already finalized"));
            }
            var dynamicGlobalProperties = SpaceTypeId.CreateOne(SpaceType.DynamicGlobalProperties);
            return Repository.GetInPromise(dynamicGlobalProperties, () => EchoApiManager.Instance.Database.GetObject<DynamicGlobalPropertiesObject>(dynamicGlobalProperties)).Then(FinalizeInPromise);
        }

        private IPromise FinalizeInPromise(DynamicGlobalPropertiesObject dynamicGlobalProperty)
        {
            return new Promise((resolve, reject) =>
            {
                new Task(() =>
                {
                    try
                    {
                        headBlockTime = dynamicGlobalProperty.Time;
                        if (expiration.IsZero())
                        {
                            expiration = TimeTool.ZeroTime().AddSeconds(BaseExpirationSeconds + ChainConfig.ExpireInSeconds);
                        }
                        referenceBlockNumber = (ushort)dynamicGlobalProperty.HeadBlockNumber;
                        var prefix = dynamicGlobalProperty.HeadBlockId.FromHex2Data();
                        if (!BitConverter.IsLittleEndian)
                        {
                            Array.Reverse(prefix);
                        }
                        referenceBlockPrefix = BitConverter.ToUInt32(prefix, 4);
                        buffer = new TransactionData(this).ToBuffer().ToArray();
                        resolve();
                    }
                    catch (Exception ex)
                    {
                        reject(ex);
                    }
                }).Start();
            });
        }

        public string Id
        {
            get
            {
                if (!IsFinalized)
                {
                    throw new InvalidOperationException("Not finalized");
                }
                return SHA256.Create().HashAndDispose(buffer).ToHexString().Substring(0, 40);
            }
        }

        public DateTime SetExpireSeconds(long seconds)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("SetExpireSeconds... Already finalized");
            }
            return expiration = TimeTool.ZeroTime().AddSeconds(BaseExpirationSeconds + seconds);
        }

        // Wraps this transaction in a proposal_create transaction
        public TransactionBuilder Propose(ProposalCreateOperationData proposalCreateOperation)
        {
            if (IsFinalized)
            {
                throw new InvalidOperationException("Propose... Already finalized");
            }
            if (operations.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Propose... Add operation first");
            }
            var proposedOperations = new OperationWrapperData[operations.Count];
            for (var i = 0; i < operations.Count; i++)
            {
                proposedOperations[i] = new OperationWrapperData { Operation = operations[i] };
            }
            operations.Clear();
            signatures.Clear();
            signerKeys.Clear();
            proposalCreateOperation.ProposedOperations = proposedOperations;
            return AddOperation(proposalCreateOperation);
        }

        public bool HasProposedOperation => operations.Exists(o => o.Type.Equals(ChainTypes.Operation.ProposalCreate));

        // Optional: the fees can be obtained from the witness node
        public static IPromise<TransactionBuilder> SetRequiredFees(TransactionBuilder builder, SpaceTypeId asset = null)
        {
            var feePool = 0L;
            if (builder.IsFinalized)
            {
                throw new InvalidOperationException("SetRequiredFees... Already finalized");
            }
            if (builder.operations.IsNullOrEmpty())
            {
                throw new InvalidOperationException("SetRequiredFees... Add operation first");
            }
            var ops = new OperationData[builder.operations.Count];
            for (var i = 0; i < builder.operations.Count; i++)
            {
                ops[i] = builder.operations[i].Clone();
            }
            var zeroAsset = SpaceTypeId.CreateOne(SpaceType.Asset);
            if (asset.IsNull())
            {
                var firstFee = ops.First().Fee;
                if (!firstFee.IsNull() && !firstFee.Asset.IsNullOrEmpty())
                {
                    asset = firstFee.Asset;
                }
                else
                {
                    asset = zeroAsset;
                }
            }

            var promises = new List<IPromise<object>>();
            promises.Add(EchoApiManager.Instance.Database.GetRequiredFees(ops, asset.ToUintId).Then<object>(feesData => feesData));

            if (!asset.Equals(zeroAsset))
            {
                // This handles the fallback to paying fees in BTS if the fee pool is empty.
                promises.Add(EchoApiManager.Instance.Database.GetRequiredFees(ops, zeroAsset.ToUintId).Then<object>(coreFeesData => coreFeesData));
                promises.Add(Repository.GetInPromise(asset, () => EchoApiManager.Instance.Database.GetAsset(asset.ToUintId)).Then<object>(assetObject => assetObject));
            }

            return Promise<object>.All(promises.ToArray()).Then(results =>
            {
                var list = new List<object>(results).ToArray();

                var feesData = list.First() as AssetData[];
                var coreFeesData = (list.Length > 1) ? (list[1] as AssetData[]) : null;
                var assetObject = (list.Length > 2) ? (list[2] as AssetObject) : null;

                var dynamicPromise = (!asset.Equals(zeroAsset) && !asset.IsNull()) ?
                    EchoApiManager.Instance.Database.GetObject<AssetDynamicDataObject>(assetObject.DynamicAssetData) : Promise<AssetDynamicDataObject>.Resolved(null);

                return dynamicPromise.Then(dynamicObject =>
                {
                    if (!asset.Equals(zeroAsset))
                    {
                        feePool = !dynamicObject.IsNull() ? dynamicObject.FeePool : 0L;
                        var totalFees = 0L;
                        for (var j = 0; j < coreFeesData.Length; j++)
                        {
                            totalFees += coreFeesData[j].Amount;
                        }
                        if (totalFees > feePool)
                        {
                            feesData = coreFeesData;
                            asset = zeroAsset;
                        }
                    }

                    // Proposed transactions need to be flattened
                    var flatAssets = new List<AssetData>();

                    void Flatten(object obj)
                    {
                        if (obj.IsArray())
                        {
                            var array = obj as IList;
                            for (var k = 0; k < array.Count; k++)
                            {
                                Flatten(array[k]);
                            }
                        }
                        else
                        {
                            flatAssets.Add((AssetData)obj);
                        }
                    }

                    Flatten(feesData.OrEmpty());

                    var assetIndex = 0;

                    void SetFee(OperationData operation)
                    {
                        if (operation.Fee.IsNull() || operation.Fee.Amount == 0L)
                        {
                            operation.Fee = flatAssets[assetIndex];
                        }
                        assetIndex++;
                        if (operation.Type.Equals(ChainTypes.Operation.ProposalCreate))
                        {
                            var proposedOperations = (operation as ProposalCreateOperationData).ProposedOperations;
                            for (var y = 0; y < proposedOperations.Length; y++)
                            {
                                SetFee(proposedOperations[y].Operation);
                            }
                        }
                    }

                    for (var i = 0; i < builder.operations.Count; i++)
                    {
                        SetFee(builder.operations[i]);
                    }
                });
            }).Then(() => Promise<TransactionBuilder>.Resolved(builder));
        }

        public IPromise<IPublicKey[]> GetPotentialSignatures()
        {
            return EchoApiManager.Instance.Database.GetPotentialSignatures(new SignedTransactionData(this));
        }

        public IPromise<IPublicKey[]> GetRequiredSignatures(IPublicKey[] availableKeys)
        {
            if (availableKeys.IsNullOrEmpty())
            {
                return Promise<IPublicKey[]>.Resolved(new IPublicKey[0]);
            }
            return EchoApiManager.Instance.Database.GetRequiredSignatures(new SignedTransactionData(this), availableKeys);
        }

        public TransactionBuilder AddSigner(KeyPair key)
        {
            if (signed)
            {
                throw new InvalidOperationException("AddSigner... Already signed");
            }
            // prevent duplicates
            if (!signerKeys.Contains(key))
            {
                signerKeys.Add(key);
            }
            return this;
        }

        private void Sign()
        {
            if (!IsFinalized)
            {
                throw new InvalidOperationException("Sign... Not finalized");
            }
            if (signed)
            {
                throw new InvalidOperationException("Sign... Already signed");
            }
            if (signerKeys.IsNullOrEmpty())
            {
                throw new InvalidOperationException("Sign... Transaction was not signed. Do you have a private key?");
            }
            foreach (var key in signerKeys)
            {
                var chainId = EchoApiManager.ChainId.FromHex2Data();
                var data = chainId.Concat(buffer.OrEmpty());
                chainId.Clear();
                var signature = key.Private.Sign(data);
                data.Clear();
                signatures.Add(signature);
                signature.Clear();
            }
            signerKeys.Clear();
            signed = true;
        }

        public override string Serialize() => new SignedTransactionData(this).Serialize();

        public bool IsFinalized => !buffer.IsNullOrEmpty();

        public IPromise Broadcast(Action<TransactionConfirmationData> resultCallback = null)
        {
            if (IsFinalized)
            {
                return BroadcastTransaction(this, resultCallback);
            }
            return Finalize().Then(() => BroadcastTransaction(this, resultCallback));
        }

        private static double BaseExpirationSeconds
        {
            get
            {
                var headBlockSeconds = Math.Ceiling(headBlockTime.GetTimeFrom1Jan1970AtMilliseconds() / 1000.0);
                var nowSeconds = Math.Ceiling(DateTime.UtcNow.GetTimeFrom1Jan1970AtMilliseconds() / 1000.0);
                // The head block time should be updated every 3 seconds.  If it isn't
                // then help the transaction to expire (use headBlockSeconds)
                if (nowSeconds - headBlockSeconds > 30.0)
                {
                    return headBlockSeconds;
                }
                // If the user's clock is very far behind, use the head block time.
                return Math.Max(nowSeconds, headBlockSeconds);
            }
        }

        private static IPromise BroadcastTransaction(TransactionBuilder builder, Action<TransactionConfirmationData> resultCallback = null)
        {
            return new Promise((resolve, reject) => new Task(() =>
            {
                try
                {
                    if (!builder.signed)
                    {
                        builder.Sign();
                    }
                    if (!builder.IsFinalized)
                    {
                        throw new InvalidOperationException("Not finalized");
                    }
                    if (builder.signatures.IsNullOrEmpty())
                    {
                        throw new InvalidOperationException("Not signed");
                    }
                    if (builder.operations.IsNullOrEmpty())
                    {
                        throw new InvalidOperationException("No operations");
                    }
                    EchoApiManager.Instance.NetworkBroadcast.BroadcastTransactionWithCallback(new SignedTransactionData(builder), async result =>
                    {
                        resultCallback?.Invoke(result.IsNullOrEmpty() ? null : await result.First().ToObjectAsync<TransactionConfirmationData>());
                    }).Then(resolve).Catch(reject);
                }
                catch (Exception ex)
                {
                    reject(ex);
                }
            }).Start());
        }
    }
}