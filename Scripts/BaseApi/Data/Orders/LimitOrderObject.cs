using System;
using Base.Data.Assets;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Orders
{
    // id "1.6.x"
    public sealed class LimitOrderObject : IdObject
    {
        [JsonProperty("expiration"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Expiration { get; set; }
        [JsonProperty("seller")]
        public SpaceTypeId Seller { get; set; }
        [JsonProperty("for_sale")]
        public long ForSale { get; set; }
        [JsonProperty("sell_price")]
        public PriceData SellPrice { get; set; }
        [JsonProperty("deferred_fee")]
        public long DeferredFee { get; set; }
    }
}