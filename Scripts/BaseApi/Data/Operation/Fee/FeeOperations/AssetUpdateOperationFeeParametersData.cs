using Base.Config;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class AssetUpdateOperationFeeParametersData : FeeParametersData
    {
        private const string FEE_FIELD_KEY = "fee";
        private const string PRICE_PER_KBYTE_FIELD_KEY = "price_per_kbyte";


        public ulong Fee { get; set; }
        public uint PricePerKByte { get; set; }

        public override ChainTypes.FeeParameters Type => ChainTypes.FeeParameters.AssetUpdateOperation;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteUInt64(Fee);
            buffer.WriteUInt32(PricePerKByte);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { FEE_FIELD_KEY,                Fee },
                { PRICE_PER_KBYTE_FIELD_KEY,    PricePerKByte }
            }).Build();
        }

        public static AssetUpdateOperationFeeParametersData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AssetUpdateOperationFeeParametersData();
            instance.Fee = value.TryGetValue(FEE_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.PricePerKByte = value.TryGetValue(PRICE_PER_KBYTE_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            return instance;
        }
    }
}