using Base.Data.Assets;
using Newtonsoft.Json;


namespace Base.Data.Orders
{
    // id "1.7.x"
    public sealed class CallOrderObject : IdObject
    {
        [JsonProperty("borrower")]
        public SpaceTypeId Borrower { get; set; }
        [JsonProperty("collateral")]
        public long Collateral { get; set; }
        [JsonProperty("debt")]
        public long Debt { get; set; }
        [JsonProperty("call_price")]
        public PriceData CallPrice { get; set; }
    }
}