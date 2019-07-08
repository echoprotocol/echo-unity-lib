using Newtonsoft.Json;


namespace Base.Data
{
    public sealed class ETHMethodData : SerializableObject
    {
        [JsonProperty("method")]
        public string Method { get; private set; }
        [JsonProperty("gas")]
        public ulong Gas { get; private set; }
    }
}