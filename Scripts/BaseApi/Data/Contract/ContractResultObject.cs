using Newtonsoft.Json;


namespace Base.Data
{
    // id "1.10.x"
    public sealed class ContractResultObject : IdObject
    {
        [JsonProperty("contracts_id")]
        public SpaceTypeId[] Contracts { get; private set; }
    }
}