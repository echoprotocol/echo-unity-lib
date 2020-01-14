using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class SidechainConfigData : SerializableObject
    {
        [JsonProperty("eth_contract_address")]
        public string ETH_ContractAddress { get; private set; }
        [JsonProperty("eth_committee_update_method")]
        public ETH_MethodData ETH_CommitteeUpdateMethod { get; private set; }
        [JsonProperty("eth_gen_address_method")]
        public ETH_MethodData ETH_GenerateAddressMethod { get; private set; }
        [JsonProperty("eth_withdraw_method")]
        public ETH_MethodData ETH_WithdrawMethod { get; private set; }
        [JsonProperty("eth_update_addr_method")]
        public ETH_MethodData ETH_UpdateAddressMethod { get; private set; }
        [JsonProperty("eth_update_contract_address")]
        public ETH_MethodData ETH_UpdateContractAddress { get; private set; }
        [JsonProperty("eth_withdraw_token_method")]
        public ETH_MethodData ETH_WithdrawTokenMethod { get; private set; }
        [JsonProperty("eth_collect_tokens_method")]
        public ETH_MethodData ETH_CollectTokensMethod { get; private set; }
        [JsonProperty("eth_committee_updated_topic")]
        public string ETH_CommitteeUpdatedTopic { get; private set; }
        [JsonProperty("eth_gen_address_topic")]
        public string ETH_GenAddressTopic { get; private set; }
        [JsonProperty("eth_deposit_topic")]
        public string ETH_DepositTopic { get; private set; }
        [JsonProperty("eth_withdraw_topic")]
        public string ETH_WithdrawTopic { get; private set; }
        [JsonProperty("erc20_deposit_topic")]
        public string ERC20_DepositTopic { get; private set; }
        [JsonProperty("erc20_withdraw_topic")]
        public string ERC20_WithdrawTopic { get; private set; }
        [JsonProperty("ETH_asset_id")]
        public SpaceTypeId ETH_Asset { get; private set; }
        [JsonProperty("BTC_asset_id")]
        public SpaceTypeId BTC_Asset { get; private set; }
        [JsonProperty("fines")]
        public SidechainFinesData Fines { get; private set; }
        [JsonProperty("gas_price")]
        public ulong GasPrice { get; private set; }
        [JsonProperty("satoshis_per_byte")]
        public uint SatoshisPerByte { get; private set; }
        [JsonProperty("coefficient_waiting_blocks")]
        public uint CoefficientWaitingBlocks { get; private set; }
    }


    public sealed class SidechainFinesData : SerializableObject
    {
        [JsonProperty("generate_eth_address")]
        public long GenerateETHAddress { get; private set; }
    }
}