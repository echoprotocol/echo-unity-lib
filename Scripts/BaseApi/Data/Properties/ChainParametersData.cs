using Base.Data.Assets;
using Base.Data.Operations.Fee;
using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class ChainParametersData : SerializableObject
    {
        [JsonProperty("current_fees")]
        public FeeScheduleData CurrentFees { get; private set; }
        [JsonProperty("block_interval")]
        public byte BlockInterval { get; private set; }
        [JsonProperty("maintenance_interval")]
        public uint MaintenanceInterval { get; private set; }
        [JsonProperty("maintenance_skip_slots")]
        public byte MaintenanceSkipSlots { get; private set; }
        [JsonProperty("committee_proposal_review_period")]
        public uint CommitteeProposalReviewPeriod { get; private set; }
        [JsonProperty("maximum_transaction_size")]
        public uint MaximumTransactionSize { get; private set; }
        [JsonProperty("maximum_block_size")]
        public uint MaximumBlockSize { get; private set; }
        [JsonProperty("maximum_time_until_expiration")]
        public uint MaximumTimeUntilExpiration { get; private set; }
        [JsonProperty("maximum_proposal_lifetime")]
        public uint MaximumProposalLifetime { get; private set; }
        [JsonProperty("maximum_asset_whitelist_authorities")]
        public byte MaximumAssetWhitelistAuthorities { get; private set; }
        [JsonProperty("maximum_asset_feed_publishers")]
        public byte MaximumAssetFeedPublishers { get; private set; }
        [JsonProperty("maximum_committee_count")]
        public ushort MaximumCommitteeCount { get; private set; }
        [JsonProperty("maximum_authority_membership")]
        public ushort MaximumAuthorityMembership { get; private set; }
        [JsonProperty("reserve_percent_of_fee")]
        public ushort ReservePercentOfFee { get; private set; }
        [JsonProperty("network_percent_of_fee")]
        public ushort NetworkPercentOfFee { get; private set; }
        [JsonProperty("lifetime_referrer_percent_of_fee")]
        public ushort LifetimeReferrerPercentOfFee { get; private set; }
        [JsonProperty("cashback_vesting_period_seconds")]
        public uint CashbackVestingPeriodSeconds { get; private set; }
        [JsonProperty("cashback_vesting_threshold")]
        public long CashbackVestingThreshold { get; private set; }
        [JsonProperty("count_non_member_votes")]
        public bool CountNonMemberVotes { get; private set; }
        [JsonProperty("allow_non_member_whitelists")]
        public bool AllowNonMemberWhitelists { get; private set; }
        [JsonProperty("max_predicate_opcode")]
        public ushort MaxPredicateOpcode { get; private set; }
        [JsonProperty("fee_liquidation_threshold")]
        public long FeeLiquidationThreshold { get; private set; }
        [JsonProperty("accounts_per_fee_scale")]
        public ushort AccountsPerFeeScale { get; private set; }
        [JsonProperty("account_fee_scale_bitshifts")]
        public byte AccountFeeScaleBitshifts { get; private set; }
        [JsonProperty("max_authority_depth")]
        public byte MaxAuthorityDepth { get; private set; }
        [JsonProperty("echorand_config")]
        public EchorandConfigData EchorandConfig { get; private set; }
        [JsonProperty("sidechain_config")]
        public SidechainConfigData SidechainConfig { get; private set; }
        [JsonProperty("gas_price")]
        public GasPriceData GasPrice { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}