using Newtonsoft.Json;


namespace Base.Data
{
    public sealed class ContractResultObject : IdObject
    {
        [JsonProperty("contracts_id")]
        public SpaceTypeId[] Contracts { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}