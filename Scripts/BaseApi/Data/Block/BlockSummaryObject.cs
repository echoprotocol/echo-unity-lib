using Newtonsoft.Json;


namespace Base.Data.Block
{
    public sealed class BlockSummaryObject : IdObject
    {
        [JsonProperty("block_id")]
        public string BlockId { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}