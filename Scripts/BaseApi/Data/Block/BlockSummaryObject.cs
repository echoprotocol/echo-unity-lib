using Newtonsoft.Json;


namespace Base.Data.Block
{
    // id "2.7.x"
    public sealed class BlockSummaryObject : IdObject
    {
        [JsonProperty("block_id")]
        public string BlockId { get; private set; }
    }
}