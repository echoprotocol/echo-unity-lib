using Base.Data.Assets;
using Base.Data.Operations.Fee;
using Base.Data.Pairs;
using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class ChainParametersData : SerializableObject
    {
        [JsonProperty("current_fees")]
        public FeeScheduleData CurrentFees { get; private set; }
        [JsonProperty("maintenance_interval")]
        public uint MaintenanceInterval { get; private set; }
        [JsonProperty("maintenance_duration_seconds")]
        public byte MaintenanceDurationSeconds { get; private set; }
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
        [JsonProperty("maximum_authority_membership")]
        public ushort MaximumAuthorityMembership { get; private set; }
        [JsonProperty("max_authority_depth")]
        public byte MaxAuthorityDepth { get; private set; }
        [JsonProperty("block_emission_amount")]
        public long BlockEmissionAmount { get; private set; }
        [JsonProperty("block_producer_reward_ratio")]
        public ushort BlockProducerRewardRatio { get; private set; }
        [JsonProperty("committee_frozen_balance_to_activate")]
        public ulong CommitteeFrozenBalanceToActivate { get; private set; }
        [JsonProperty("committee_maintenance_intervals_to_deposit")]
        public ulong CommitteeMaintenanceIntervalsToDeposit { get; private set; }
        [JsonProperty("committee_balance_unfreeze_duration_seconds")]
        public uint CommitteeBalanceUnfreezeDurationSeconds { get; private set; }
        [JsonProperty("x86_64_maximum_contract_size")]
        public ulong MaximumContractSizeForX86_64 { get; private set; }
        [JsonProperty("frozen_balances_multipliers")]
        public DayPercentPair[] FrozenBalancesMultipliers { get; private set; }
        [JsonProperty("echorand_config")]
        public EchorandConfigData EchorandConfig { get; private set; }
        [JsonProperty("sidechain_config")]
        public SidechainConfigData SidechainConfig { get; private set; }
        [JsonProperty("erc20_config")]
        public ERC20_ConfigData ERC20Config { get; private set; }
        [JsonProperty("gas_price")]
        public GasPriceData GasPrice { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}