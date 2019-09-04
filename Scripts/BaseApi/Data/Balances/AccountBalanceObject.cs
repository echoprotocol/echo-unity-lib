using Newtonsoft.Json;


namespace Base.Data.Balances
{
    // id "2.4.x"
    public sealed class AccountBalanceObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("asset_type")]
        public SpaceTypeId Asset { get; private set; }
        [JsonProperty("balance")]
        public long Balance { get; private set; }
    }
}