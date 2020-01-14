using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Base.Api.Database;
using Base.Data;
using Base.Data.Accounts;
using Base.Data.Assets;
using Base.Data.Balances;
using Base.Data.Block;
using Base.Data.BTC;
using Base.Data.Contract;
using Base.Data.ERC20;
using Base.Data.ETH;
using Base.Data.Operations;
using Base.Data.Properties;
using Base.Data.SpecialAuthorities;
using Base.Data.Transactions;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Action;
using Newtonsoft.Json.Linq;
using RSG;
using Tools.Json;
using IdObjectDictionary = System.Collections.Generic.Dictionary<Base.Data.SpaceTypeId, Base.Data.IdObject>;


namespace Base.Storage
{
    public static class Repository
    {
        private const int BLOCKS_LIMIT = 100;

        public static event Action<uint, SignedBlockData> OnGetBlock;
        public static event Action<IdObject> OnGetObject;
        public static event Action<string> OnGetString;

        private readonly static Dictionary<uint, SignedBlockData> blocks = new Dictionary<uint, SignedBlockData>();
        private readonly static Dictionary<SpaceType, IdObjectDictionary> root = new Dictionary<SpaceType, IdObjectDictionary>();
        private readonly static object rootLocler = new object();
        private readonly static object blocksLocler = new object();

        private static DatabaseApi databaseApi;


        private static void GetBlock(uint blockNumber, SignedBlockData block) => OnGetBlock.SafeInvoke(blockNumber, block);

        private static void GetObject(IdObject idObject) => OnGetObject.SafeInvoke(idObject);

        private static void GetString(string value) => OnGetString.SafeInvoke(value);

        private static async void ChangeNotifyAsync(JToken[] list) => await Task.Run(() => ChangeNotify(list));

        private static void ChangeNotify(JToken[] list)
        {
            var notifyObjectList = new List<IdObject>();
            var notifyStringList = new List<string>();
            foreach (var item in list)
            {
                if (item.Type.Equals(JTokenType.Object))
                {
                    var idObject = item.ToIdObject();
                    if (idObject.IsNull())
                    {
                        continue;
                    }
                    if (idObject.SpaceType == SpaceType.DynamicGlobalProperties)
                    {
                        var blockNumber = (idObject as DynamicGlobalPropertiesObject).HeadBlockNumber;
                        databaseApi?.GetBlock(blockNumber).Then(block =>
                        {
                            AddBlock(blockNumber, block);
                            GetBlock(blockNumber, block);
                        });
                    }
                    AddObject(idObject);
                    notifyObjectList.Add(idObject);
                    CustomTools.Console.DebugLog("Update object:", CustomTools.Console.LogGreenColor(idObject.SpaceType), idObject.Id, '\n', CustomTools.Console.LogWhiteColor(idObject));
                }
                else
                if (item.Type.Equals(JTokenType.String))
                {
                    notifyStringList.Add(item.ToString());
                    CustomTools.Console.DebugLog("Get string:", CustomTools.Console.LogCyanColor(item));
                }
                else
                {
                    CustomTools.Console.DebugWarning("Get unexpected json type:", CustomTools.Console.LogYellowColor(item.Type), CustomTools.Console.LogCyanColor(item));
                }
            }
            foreach (var newObject in notifyObjectList)
            {
                GetObject(newObject);
            }
            notifyObjectList.Clear();
            foreach (var newString in notifyStringList)
            {
                GetString(newString);
            }
            notifyStringList.Clear();
        }

        private static void AddObject(IdObject idObject)
        {
            lock (rootLocler)
            {
                (root.ContainsKey(idObject.SpaceType) ? root[idObject.SpaceType] : (root[idObject.SpaceType] = new IdObjectDictionary()))[idObject.Id] = idObject;
            }
        }

        private static void AddBlock(uint blockNumber, SignedBlockData block)
        {
            lock (blocksLocler)
            {
                if (blocks.Count > BLOCKS_LIMIT)
                {
                    blocks.Clear();
                }
                blocks[blockNumber] = block;
            }
        }

        private static IPromise AddObjectInPromise(IdObject idObject)
        {
            AddObject(idObject);
            return Promise.Resolved();
        }

        private static IPromise Init(DatabaseApi api)
        {
            return Promise.All(
                api.GetDynamicGlobalProperties().Then(AddObjectInPromise),
                api.GetGlobalProperties().Then(AddObjectInPromise),
                api.GetAsset().Then(AddObjectInPromise)
            );
        }

        public static IPromise SubscribeToNotice(DatabaseApi api) => api.SubscribeNotice(ChangeNotifyAsync).Then(() => Init(api));

        public static IPromise SubscribeToDynamicGlobalProperties(DatabaseApi api)
        {
            databaseApi = api;
            return api.SubscribeToDynamicGlobalProperties();
        }

        public static bool IsExist(SpaceTypeId spaceTypeId)
        {
            lock (rootLocler)
            {
                return root.ContainsKey(spaceTypeId.SpaceType) && root[spaceTypeId.SpaceType].ContainsKey(spaceTypeId);
            }
        }

        public static IPromise<T> GetInPromise<T>(SpaceTypeId key, Func<IPromise<T>> getter = null) where T : IdObject
        {
            lock (rootLocler)
            {
                if (root.ContainsKey(key.SpaceType) && root[key.SpaceType].ContainsKey(key))
                {
                    return Promise<T>.Resolved((T)root[key.SpaceType][key]);
                }
            }
            return getter.IsNull() ? Promise<T>.Resolved(null) : getter.Invoke().Then(idObject =>
            {
                AddObjectInPromise(idObject);
                return Promise<T>.Resolved(idObject);
            });
        }

        public static IdObject[] GetAll(SpaceType spaceType)
        {
            lock (rootLocler)
            {
                return root.ContainsKey(spaceType) ? new List<IdObject>(root[spaceType].Values).ToArray() : new IdObject[0];
            }
        }
    }


    public static class Extensions
    {
        public static IdObject ToIdObject(this JToken source)
        {
            var sample = source.ToObject<IdObject>();
            if (sample.Id.IsNullOrEmpty())
            {
                CustomTools.Console.DebugWarning("Get unexpected object:", source.ToString());
                return null;
            }
            switch (sample.SpaceType)
            {
                case SpaceType.Base:/*                          */return source.ToObject<BaseObject>();
                case SpaceType.Account:/*                       */return source.ToObject<AccountObject>();
                case SpaceType.Asset:/*                         */return source.ToObject<AssetObject>();
                case SpaceType.CommitteeMember:/*               */return source.ToObject<CommitteeMemberObject>();
                case SpaceType.Proposal:/*                      */return source.ToObject<ProposalObject>();
                case SpaceType.OperationHistory:/*              */return source.ToObject<OperationHistoryObject>();
                case SpaceType.VestingBalance:/*                */return source.ToObject<VestingBalanceObject>();
                case SpaceType.Balance:/*                       */return source.ToObject<BalanceObject>();
                case SpaceType.FrozenBalance:/*                 */return source.ToObject<FrozenBalanceObject>();
                case SpaceType.CommitteeFrozenBalance:/*        */return source.ToObject<CommitteeFrozenBalanceObject>();
                case SpaceType.Contract:/*                      */return source.ToObject<ContractObject>();
                case SpaceType.ContractResult:/*                */return source.ToObject<ContractResultObject>();
                case SpaceType.ETH_Address:/*                   */return source.ToObject<ETH_AddressObject>();
                case SpaceType.ETH_Deposit:/*                   */return source.ToObject<ETH_Deposit_Object>();
                case SpaceType.ETH_Withdraw:/*                  */return source.ToObject<ETH_Withdraw_Object>();
                case SpaceType.ERC20_Token:/*                   */return source.ToObject<ERC20_TokenObject>();
                case SpaceType.ERC20_DepositToken:/*            */return source.ToObject<ERC20_DepositTokenObject>();
                case SpaceType.ERC20_WithdrawToken:/*           */return source.ToObject<ERC20_WithdrawTokenObject>();
                case SpaceType.BTC_Address:/*                   */return source.ToObject<BTC_AddressObject>();
                case SpaceType.BTC_IntermediateDeposit:/*       */return source.ToObject<BTC_IntermediateDepositObject>();
                case SpaceType.BTC_Deposit:/*                   */return source.ToObject<BTC_DepositObject>();
                case SpaceType.BTC_Withdraw:/*                  */return source.ToObject<BTC_WithdrawObject>();
                case SpaceType.BTC_Aggregating:/*               */return source.ToObject<BTC_AggregatingObject>();
                case SpaceType.GlobalProperties:/*              */return source.ToObject<GlobalPropertiesObject>();
                case SpaceType.DynamicGlobalProperties:/*       */return source.ToObject<DynamicGlobalPropertiesObject>();
                case SpaceType.AssetDynamicData:/*              */return source.ToObject<AssetDynamicDataObject>();
                case SpaceType.AssetBitassetData:/*             */return source.ToObject<AssetBitassetDataObject>();
                case SpaceType.AccountBalance:/*                */return source.ToObject<AccountBalanceObject>();
                case SpaceType.AccountStatistics:/*             */return source.ToObject<AccountStatisticsObject>();
                case SpaceType.Transaction:/*                   */return source.ToObject<TransactionObject>();
                case SpaceType.BlockSummary:/*                  */return source.ToObject<BlockSummaryObject>();
                case SpaceType.AccountTransactionHistory:/*     */return source.ToObject<AccountTransactionHistoryObject>();
                case SpaceType.ChainProperty:/*                 */return source.ToObject<ChainPropertyObject>();
                case SpaceType.SpecialAuthority:/*              */return source.ToObject<SpecialAuthorityObject>();
                case SpaceType.ContractBalance:/*               */return source.ToObject<ContractBalanceObject>();
                case SpaceType.ContractHistory:/*               */return source.ToObject<ContractHistoryObject>();
                case SpaceType.ContractStatistics:/*            */return source.ToObject<ContractStatisticsObject>();
                case SpaceType.AccountAddress:/*                */return source.ToObject<AccountAddressObject>();
                case SpaceType.ContractPool:/*                  */return source.ToObject<ContractPoolObject>();
                case SpaceType.MaliciousCommitteemen:/*         */return source.ToObject<MaliciousCommitteemenObject>();
                default:
                    CustomTools.Console.DebugWarning("Get unexpected SpaceType:", CustomTools.Console.LogCyanColor(sample.SpaceType), sample.Id, '\n', source);
                    return null;
            }
        }
    }
}