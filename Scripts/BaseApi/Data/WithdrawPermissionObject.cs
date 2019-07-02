using System;
using Base.Data.Assets;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data
{
    // id "1.11.x"
    public sealed class WithdrawPermissionObject : IdObject
    {
        [JsonProperty("withdraw_from_account")]
        public SpaceTypeId WithdrawFromAccount { get; private set; }
        [JsonProperty("authorized_account")]
        public SpaceTypeId AuthorizedAccount { get; private set; }
        [JsonProperty("withdrawal_limit")]
        public AssetData WithdrawalLimit { get; private set; }
        [JsonProperty("withdrawal_period_sec")]
        public uint WithdrawalPeriodSeconds { get; private set; }
        [JsonProperty("period_start_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime PeriodStartTime { get; private set; }
        [JsonProperty("expiration"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Expiration { get; private set; }
        [JsonProperty("claimed_this_period")]
        public long ClaimedThisPeriod { get; private set; }
    }
}