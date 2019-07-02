using Newtonsoft.Json;


namespace Base.Data.Contract
{
    // id "1.14.x"
    public sealed class ContractObject : IdObject
    {
        [JsonProperty("statistics")]
        public SpaceTypeId Statistics { get; private set; }
        [JsonProperty("suicided")]
        public bool Suicided { get; private set; }
    }
}