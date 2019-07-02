using Base.Config;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class AccountCreateOperationFeeParametersData : FeeParametersData
    {
        private const string BASIC_FEE_FIELD_KEY = "basic_fee";
        private const string PREMIUM_FEE_FIELD_KEY = "premium_fee";
        private const string PRICE_PER_KBYTE_FIELD_KEY = "price_per_kbyte";


        public ulong BasicFee { get; set; }
        public ulong PremiumFee { get; set; }
        public uint PricePerKByte { get; set; }

        public override ChainTypes.FeeParameters Type => ChainTypes.FeeParameters.AccountCreateOperation;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteUInt64(BasicFee);
            buffer.WriteUInt64(PremiumFee);
            buffer.WriteUInt32(PricePerKByte);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { BASIC_FEE_FIELD_KEY,          BasicFee },
                { PREMIUM_FEE_FIELD_KEY,        PremiumFee },
                { PRICE_PER_KBYTE_FIELD_KEY,    PricePerKByte }
            }).Build();
        }

        public static AccountCreateOperationFeeParametersData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AccountCreateOperationFeeParametersData();
            instance.BasicFee = value.TryGetValue(BASIC_FEE_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.PremiumFee = value.TryGetValue(PREMIUM_FEE_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.PricePerKByte = value.TryGetValue(PRICE_PER_KBYTE_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            return instance;
        }
    }
}