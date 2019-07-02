using System;
using Base.Config;
using Base.Data.Json;
using Buffers;
using Newtonsoft.Json.Linq;
using Tools.Json;
using Tools.Time;


namespace Base.Data.VestingPolicies
{
    public sealed class LinearVestingPolicyData : VestingPolicyData
    {
        private const string BEGIN_TIMESTAMP_FIELD_KEY = "begin_timestamp";
        private const string VESTING_CLIFF_SECONDS_FIELD_KEY = "vesting_cliff_seconds";
        private const string VESTING_DURATION_SECONDS_FIELD_KEY = "vesting_duration_seconds";
        private const string BEGIN_BALANCE_FIELD_KEY = "begin_balance";


        public DateTime BeginTimestamp { get; set; }
        public uint VestingCliffSeconds { get; set; }
        public uint VestingDurationSeconds { get; set; }
        public long BeginBalance { get; set; }

        public override ChainTypes.VestingPolicy Type => ChainTypes.VestingPolicy.Linear;

        public override ByteBuffer ToBufferRaw(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteDateTime(BeginTimestamp);
            buffer.WriteUInt32(VestingCliffSeconds);
            buffer.WriteUInt32(VestingDurationSeconds);
            buffer.WriteInt64(BeginBalance);
            return buffer;
        }

        public override string Serialize()
        {
            return new JsonBuilder(new JsonDictionary {
                { BEGIN_TIMESTAMP_FIELD_KEY,             DateTimeConverter.ConvertTo(BeginTimestamp) },
                { VESTING_CLIFF_SECONDS_FIELD_KEY,       VestingCliffSeconds },
                { VESTING_DURATION_SECONDS_FIELD_KEY,    VestingDurationSeconds },
                { BEGIN_BALANCE_FIELD_KEY,               BeginBalance }
            }).Build();
        }

        public static LinearVestingPolicyData Create(JObject value)
        {
            var token = value.Root;
            var instance = new LinearVestingPolicyData();
            instance.BeginTimestamp = value.TryGetValue(BEGIN_TIMESTAMP_FIELD_KEY, out token) ? token.ToObject<DateTime>(new DateTimeConverter().GetSerializer()) : TimeTool.ZeroTime();
            instance.VestingCliffSeconds = value.TryGetValue(VESTING_CLIFF_SECONDS_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            instance.VestingDurationSeconds = value.TryGetValue(VESTING_DURATION_SECONDS_FIELD_KEY, out token) ? token.ToObject<uint>() : uint.MinValue;
            instance.BeginBalance = value.TryGetValue(BEGIN_BALANCE_FIELD_KEY, out token) ? token.ToObject<long>() : 0;
            return instance;
        }
    }
}