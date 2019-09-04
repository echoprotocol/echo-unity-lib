using System;
using Base.Data.Json;
using Base.Data.Pairs;
using Newtonsoft.Json;


namespace Base.Data.Assets
{
    // id "2.3.x"
    public sealed class AssetBitassetDataObject : IdObject
    {
        [JsonProperty("options")]
        public BitassetOptionsData Options { get; private set; }
        [JsonProperty("feeds")]
        public AccountIdDateTimePriceFeedDataPair[] Feeds { get; private set; }
        [JsonProperty("current_feed")]
        public PriceFeedData CurrentFeed { get; private set; }
        [JsonProperty("current_feed_publication_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CurrentFeedPublicationTime { get; private set; }
        [JsonProperty("is_prediction_market")]
        public bool IsPredictionMarket { get; private set; }
        [JsonProperty("force_settled_volume")]
        public long ForceSettledVolume { get; private set; }
        [JsonProperty("settlement_price")]
        public PriceData SettlementPrice { get; private set; }
        [JsonProperty("settlement_fund")]
        public long SettlementFund { get; private set; }
    }
}