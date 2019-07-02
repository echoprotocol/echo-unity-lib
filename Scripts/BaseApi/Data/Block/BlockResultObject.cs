using Newtonsoft.Json;


namespace Base.Data.Block
{
    // id "1.16.x"
    public sealed class BlockResultObject : IdObject
    {
        [JsonProperty("results_id")]
        public SpaceTypeId[] Results { get; private set; }
    }
}