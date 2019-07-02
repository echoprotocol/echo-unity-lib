using Buffers;
using Base.Config;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class AccountUpgradeOperationFeeParametersData : FeeParametersData
    {
        private const string MEMBERSHIP_ANNUAL_FEE_FIELD_KEY = "membership_annual_fee";
        private const string MEMBERSHIP_LIFETIME_FEE_FIELD_KEY = "membership_lifetime_fee";


        public ulong MembershipAnnualFee { get; set; }
        public ulong MembershipLifetimeFee { get; set; }

        public override ChainTypes.FeeParameters Type => ChainTypes.FeeParameters.AccountUpgradeOperation;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteUInt64(MembershipAnnualFee);
            buffer.WriteUInt64(MembershipLifetimeFee);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { MEMBERSHIP_ANNUAL_FEE_FIELD_KEY,      MembershipAnnualFee },
                { MEMBERSHIP_LIFETIME_FEE_FIELD_KEY,    MembershipLifetimeFee }
            }).Build();
        }

        public static AccountUpgradeOperationFeeParametersData Create(JObject value)
        {
            var token = value.Root;
            var instance = new AccountUpgradeOperationFeeParametersData();
            instance.MembershipAnnualFee = value.TryGetValue(MEMBERSHIP_ANNUAL_FEE_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            instance.MembershipLifetimeFee = value.TryGetValue(MEMBERSHIP_LIFETIME_FEE_FIELD_KEY, out token) ? token.ToObject<ulong>() : 0;
            return instance;
        }
    }
}