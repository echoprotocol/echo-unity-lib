using Newtonsoft.Json;


namespace Base.Data.Assets
{
    public sealed class GasPriceData : SerializableObject
    {
        [JsonProperty("price")]
        public ulong Price { get; private set; }
        [JsonProperty("gas_amount")]
        public ulong GasAmount { get; private set; }
    }
}