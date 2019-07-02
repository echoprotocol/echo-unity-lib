using Buffers;
using Base.Config;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class AssetCreateOperationFeeParametersData : FeeParametersData
    {
        private const string SYMBOL3_FIELD_KEY = "symbol3";
        private const string SYMBOL4_FIELD_KEY = "symbol4";
        private const string LONG_SYMBOL_FIELD_KEY = "long_symbol";
        private const string PRICE_PER_KBYTE_FIELD_KEY = "price_per_kbyte";


        public ulong Symbol3 { get; set; }
        public ulong Symbol4 { get; set; }
        public ulong LongSymbol { get; set; }
        public uint PricePerKByte { get; set; }

        public override ChainTypes.FeeParameters Type => ChainTypes.FeeParameters.AssetCreateOperation;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteUInt64(Symbol3);
            buffer.WriteUInt64(Symbol4);
            buffer.WriteUInt64(LongSymbol);
            buffer.WriteUInt32(PricePerKByte);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { SYMBOL3_FIELD_KEY,            Symbol3 },
                { SYMBOL4_FIELD_KEY,            Symbol4 },
                { LONG_SYMBOL_FIELD_KEY,        LongSymbol },
                { PRICE_PER_KBYTE_FIELD_KEY,    PricePerKByte }
            }).Build();
        }

        public static AssetCreateOperationFeeParametersData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AssetCreateOperationFeeParametersData();
            instance.Symbol3 = value.TryGetValue(SYMBOL3_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.Symbol4 = value.TryGetValue(SYMBOL4_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.LongSymbol = value.TryGetValue(LONG_SYMBOL_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.PricePerKByte = value.TryGetValue(PRICE_PER_KBYTE_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            return instance;
        }
    }
}