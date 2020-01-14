using System;
using Base.Data.Assets;
using Base.Data.Json;
using Base.Data.VestingPolicies;
using Newtonsoft.Json;


namespace Base.Data.Balances
{
    public sealed class BalanceObject : IdObject
    {
        [JsonProperty("owner")]
        public string Owner { get; private set; }
        [JsonProperty("balance")]
        public AssetData Balance { get; private set; }
        [JsonProperty("vesting_policy", NullValueHandling = NullValueHandling.Ignore)]
        public VestingPolicyData VestingPolicy { get; private set; } // LinearVestingPolicyData ?
        [JsonProperty("last_claim_date"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastClaimDate { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}