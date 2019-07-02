using Base.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(AccountIdDateTimePriceFeedDataPairConverter))]
    public sealed class AccountIdDateTimePriceFeedDataPair : Pair<SpaceTypeId, DateTimePriceFeedDataPair>
    {
        public AccountIdDateTimePriceFeedDataPair(SpaceTypeId account, DateTimePriceFeedDataPair pair) : base(account, pair) { }

        public override JArray ToJArray() => new JArray(JToken.FromObject(Key), Value.ToJArray());
    }
}