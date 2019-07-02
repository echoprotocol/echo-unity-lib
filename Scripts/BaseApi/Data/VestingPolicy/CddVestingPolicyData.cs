using System;
using Base.Config;
using Base.Data.Json;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;
using Tools.Time;


namespace Base.Data.VestingPolicies
{
    public sealed class CddVestingPolicyData : VestingPolicyData
    {
        private const string VESTING_SECONDS_FIELD_KEY = "vesting_seconds";
        private const string COIN_SECONDS_EARNED_FIELD_KEY = "coin_seconds_earned";
        private const string START_CLAIM_FIELD_KEY = "start_claim";
        private const string COIN_SECONDS_EARNED_LAST_UPDATE_FIELD_KEY = "coin_seconds_earned_last_update";


        public uint VestingSeconds { get; set; }
        public string CoinSecondsEarned { get; set; } // uint128
        public DateTime StartClaim { get; set; }
        public DateTime CoinSecondsEarnedLastUpdate { get; set; }

        public override ChainTypes.VestingPolicy Type => ChainTypes.VestingPolicy.Cdd;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            //buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            //buffer.WriteUInt32(VestingSeconds);
            //// write CoinSecondsEarned
            //buffer.WriteDateTime(StartClaim);
            //buffer.WriteDateTime(CoinSecondsEarnedLastUpdate);
            //return buffer;
            throw new NotImplementedException();
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { VESTING_SECONDS_FIELD_KEY,                    VestingSeconds },
                { COIN_SECONDS_EARNED_FIELD_KEY,                CoinSecondsEarned },
                { START_CLAIM_FIELD_KEY,                        DateTimeConverter.ConvertTo(StartClaim) },
                { COIN_SECONDS_EARNED_LAST_UPDATE_FIELD_KEY,    DateTimeConverter.ConvertTo(CoinSecondsEarnedLastUpdate) }
            }).Build();
        }

        public static CddVestingPolicyData Create(JObject value)
        {
            var token = value.Root;
            var instance = new CddVestingPolicyData();
            instance.VestingSeconds = value.TryGetValue(VESTING_SECONDS_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            instance.CoinSecondsEarned = value.TryGetValue(COIN_SECONDS_EARNED_FIELD_KEY, out token) ? token.ToObject<string>() : string.Empty;
            var serializer = new DateTimeConverter().GetSerializer();
            instance.StartClaim = value.TryGetValue(START_CLAIM_FIELD_KEY, out token) ? token.ToObject<DateTime>(serializer) : TimeTool.ZeroTime();
            instance.CoinSecondsEarnedLastUpdate = value.TryGetValue(COIN_SECONDS_EARNED_LAST_UPDATE_FIELD_KEY, out token) ? token.ToObject<DateTime>(serializer) : TimeTool.ZeroTime();
            return instance;
        }
    }
}