using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class SidechainConfigData : SerializableObject
    {
        [JsonProperty("eth_contract_address")]
        public string ETHContractAddress { get; private set; }
        [JsonProperty("eth_committee_update_method")]
        public ETHMethodData ETHCommitteeUpdateMethod { get; private set; }
        [JsonProperty("eth_gen_address_method")]
        public ETHMethodData ETHGenerateAddressMethod { get; private set; }
        [JsonProperty("eth_withdraw_method")]
        public ETHMethodData ETHWithdrawMethod { get; private set; }
        [JsonProperty("eth_update_addr_method")]
        public ETHMethodData ETHUpdateAddressMethod { get; private set; }
        [JsonProperty("eth_withdraw_token_method")]
        public ETHMethodData ETHWithdrawTokenMethod { get; private set; }
        [JsonProperty("eth_collect_tokens_method")]
        public ETHMethodData ETHCollectTokensMethod { get; private set; }
        [JsonProperty("eth_committee_updated_topic")]
        public string ETHCommitteeUpdatedTopic { get; private set; }
        [JsonProperty("eth_gen_address_topic")]
        public string ETHGenAddressTopic { get; private set; }
        [JsonProperty("eth_deposit_topic")]
        public string ETHDepositTopic { get; private set; }
        [JsonProperty("eth_withdraw_topic")]
        public string ETHWithdrawTopic { get; private set; }
        [JsonProperty("erc20_deposit_topic")]
        public string ERC20DepositTopic { get; private set; }
        [JsonProperty("erc20_withdraw_topic")]
        public string ERC20WithdrawTopic { get; private set; }
        [JsonProperty("ETH_asset_id")]
        public SpaceTypeId ETHAsset { get; private set; }
        [JsonProperty("fines")]
        public SidechainFinesData Fines { get; private set; }
        [JsonProperty("waiting_blocks")]
        public uint WaitingBlocks { get; private set; }
        [JsonProperty("gas_price")]
        public ulong GasPrice { get; private set; }
    }


    public sealed class SidechainFinesData : SerializableObject
    {
        [JsonProperty("generate_eth_address")]
        public long GenerateETHAddress { get; private set; }
    }
}