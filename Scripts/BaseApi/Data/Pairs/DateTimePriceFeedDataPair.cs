using System;
using Base.Data.Assets;
using Base.Data.Json;
using Newtonsoft.Json.Linq;


namespace Base.Data.Pairs
{
    public sealed class DateTimePriceFeedDataPair : Pair<DateTime, PriceFeedData>
    {
        public DateTimePriceFeedDataPair(DateTime time, PriceFeedData price) : base(time, price) { }

        public override JArray ToJArray() => new JArray(JToken.FromObject(Key, new DateTimeConverter().GetSerializer()), JToken.FromObject(Value));
    }
}