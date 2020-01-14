using Newtonsoft.Json;


namespace Base.Data.Balances
{
    public sealed class ContractBalanceObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("asset_type")]
        public SpaceTypeId Asset { get; private set; }
        [JsonProperty("balance")]
        public long Balance { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}