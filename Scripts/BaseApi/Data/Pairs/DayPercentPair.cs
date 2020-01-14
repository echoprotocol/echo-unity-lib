using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(DayPercentPairConverter))]
    public sealed class DayPercentPair : Pair<ushort, uint>
    {
        public DayPercentPair(ushort days, uint percent) : base(days, percent) { }
    }
}