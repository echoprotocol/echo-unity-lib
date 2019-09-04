using Newtonsoft.Json;


namespace Base.Data.Accounts
{
    // id "2.15.x"
    public sealed class AccountAddressObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; private set; }
        [JsonProperty("label")]
        public string Label { get; private set; }
        [JsonProperty("address")]
        public string Address { get; private set; }
    }
}