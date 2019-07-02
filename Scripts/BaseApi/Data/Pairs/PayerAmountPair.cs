using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(PayerAmountPairConverter))]
    public sealed class PayerAmountPair : Pair<SpaceTypeId, long>
    {
        public PayerAmountPair(SpaceTypeId payer, long amount) : base(payer, amount) { }
    }
}