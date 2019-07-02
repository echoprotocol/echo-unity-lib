using Base.Data.Balances;
using Base.Data.Json;
using Base.Data.Orders;
using Newtonsoft.Json;


namespace Base.Data.Accounts
{
    public sealed class FullAccountData : SerializableObject
    {
        [JsonProperty("account")]
        public AccountObject Account { get; set; }
        [JsonProperty("statistics")]
        public AccountStatisticsObject Statistics { get; set; }
        [JsonProperty("registrar_name")]
        public string RegistrarName { get; set; }
        [JsonProperty("referrer_name")]
        public string ReferrerName { get; set; }
        [JsonProperty("lifetime_referrer_name")]
        public string LifetimeReferrerName { get; set; }
        [JsonProperty("votes"), JsonConverter(typeof(VotesConverter))]
        public IdObject[] Votes { get; set; }
        [JsonProperty("cashback_balance", NullValueHandling = NullValueHandling.Ignore)]
        public VestingBalanceObject CashbackBalance { get; set; }
        [JsonProperty("balances")]
        public AccountBalanceObject[] Balances { get; set; }
        [JsonProperty("vesting_balances")]
        public VestingBalanceObject[] VestingBalances { get; set; }
        [JsonProperty("limit_orders")]
        public LimitOrderObject[] LimitOrders { get; set; }
        [JsonProperty("call_orders")]
        public CallOrderObject[] CallOrders { get; set; }
        [JsonProperty("settle_orders")]
        public ForceSettlementObject[] SettleOrders { get; set; }
        [JsonProperty("proposals")]
        public ProposalObject[] Proposals { get; set; }
        [JsonProperty("assets")]
        public SpaceTypeId[] Assets { get; set; }
        [JsonProperty("withdraws")]
        public WithdrawPermissionObject[] Withdraws { get; set; }
    }
}