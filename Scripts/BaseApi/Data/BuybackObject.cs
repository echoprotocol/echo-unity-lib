using Newtonsoft.Json;


namespace Base.Data
{
    // id "2.13.x"
    public sealed class BuybackObject : IdObject
    {
        [JsonProperty("asset_to_buy")]
        public SpaceTypeId AssetToBuy { get; private set; }
    }
}