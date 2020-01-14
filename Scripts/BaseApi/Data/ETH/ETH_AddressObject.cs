using Newtonsoft.Json;


namespace Base.Data.ETH
{
    public sealed class ETH_AddressObject : IdObject
    {
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}