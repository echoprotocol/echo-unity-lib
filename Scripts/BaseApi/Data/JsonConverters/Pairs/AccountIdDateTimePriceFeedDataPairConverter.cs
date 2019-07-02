using System;
using Base.Data.Assets;
using Base.Data.Pairs;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class AccountIdDateTimePriceFeedDataPairConverter : PairConverter<AccountIdDateTimePriceFeedDataPair, SpaceTypeId, DateTimePriceFeedDataPair>
    {
        protected override Pair<SpaceTypeId, DateTimePriceFeedDataPair> ConvertFrom(JToken key, JToken value)
        {
            var array = value.ToObject<JArray>();
            return new AccountIdDateTimePriceFeedDataPair(
                key.ToObject<SpaceTypeId>(),
                new DateTimePriceFeedDataPair(
                    array.First.ToObject<DateTime>(new DateTimeConverter().GetSerializer()),
                    array.Last.ToObject<PriceFeedData>()
                )
            );
        }
    }
}