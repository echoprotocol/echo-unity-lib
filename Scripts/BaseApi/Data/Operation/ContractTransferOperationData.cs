using Base.Config;
using Base.Data.Assets;
using Buffers;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations
{
    public sealed class ContractTransferOperationData : OperationData
    {
        private const string FEE_FIELD_KEY = "fee";
        private const string FROM_FIELD_KEY = "from";
        private const string TO_FIELD_KEY = "to";
        private const string AMOUNT_FIELD_KEY = "amount";
        private const string EXTENSIONS_FIELD_KEY = "extensions";


        public override AssetData Fee { get; set; }
        public SpaceTypeId From { get; private set; }
        public SpaceTypeId To { get; private set; }
        public AssetData Amount { get; private set; }
        public object[] Extensions { get; private set; }

        public override ChainTypes.Operation Type => ChainTypes.Operation.ContractTransfer;

        public ContractTransferOperationData()
        {
            Extensions = new object[0];
        }

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            Fee.ToBuffer(buffer);
            From.ToBuffer(buffer);
            To.ToBuffer(buffer);
            Amount.ToBuffer(buffer);
            buffer.WriteArray(Extensions, (b, item) =>
            {
                if (!item.IsNull())
                {
                    ;
                }
            });
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { FEE_FIELD_KEY,           Fee },
                { FROM_FIELD_KEY,          From },
                { TO_FIELD_KEY,            To },
                { AMOUNT_FIELD_KEY,        Amount },
                { EXTENSIONS_FIELD_KEY,    Extensions }
            }).Build();
        }

        public static ContractTransferOperationData Create(JObject value)
        {
            var token = value.Root;
            var instance = new ContractTransferOperationData();
            instance.Fee = value.TryGetValue(FEE_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.From = value.TryGetValue(FROM_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.To = value.TryGetValue(TO_FIELD_KEY, out token) ? token.ToObject<SpaceTypeId>() : SpaceTypeId.EMPTY;
            instance.Amount = value.TryGetValue(AMOUNT_FIELD_KEY, out token) ? token.ToObject<AssetData>() : AssetData.EMPTY;
            instance.Extensions = value.TryGetValue(EXTENSIONS_FIELD_KEY, out token) ? token.ToObject<object[]>() : new object[0];
            return instance;
        }
    }
}