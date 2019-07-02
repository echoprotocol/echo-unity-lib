using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(KeyValuePairConverter))]
    public sealed class KeyValuePair : Pair<string, string>
    {
        public KeyValuePair(string key, string value) : base(key, value) { }
    }
}