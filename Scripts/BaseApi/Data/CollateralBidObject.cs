using Base.Data.Assets;
using Newtonsoft.Json;


namespace Base.Data
{
    // id "2.14.x"
    public sealed class CollateralBidObject : IdObject
    {
        [JsonProperty("bidder")]
        public SpaceTypeId Bidder { get; private set; }
        [JsonProperty("inv_swan_price")]
        public PriceData InvSwanPrice { get; private set; }
    }
}