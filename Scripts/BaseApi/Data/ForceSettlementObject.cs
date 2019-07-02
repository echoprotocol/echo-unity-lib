using System;
using Base.Data.Assets;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data
{
    // id "1.4.x"
    public sealed class ForceSettlementObject : IdObject
    {
        [JsonProperty("owner")]
        public SpaceTypeId Owner { get; set; }
        [JsonProperty("balance")]
        public AssetData Balance { get; set; }
        [JsonProperty("settlement_date"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime SettlementDate { get; set; }
    }
}