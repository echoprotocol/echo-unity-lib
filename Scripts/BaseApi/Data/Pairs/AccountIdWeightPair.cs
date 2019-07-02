using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(AccountIdWeightPairConverter))]
    public sealed class AccountIdWeightPair : Pair<SpaceTypeId, ushort>
    {
        public AccountIdWeightPair(SpaceTypeId account, ushort weight) : base(account, weight) { }
    }
}