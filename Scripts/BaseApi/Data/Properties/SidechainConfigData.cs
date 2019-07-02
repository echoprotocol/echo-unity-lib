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
        [JsonProperty("eth_committee_updated_topic")]
        public string ETHCommitteeUpdatedTopic { get; private set; }
        [JsonProperty("eth_gen_address_topic")]
        public string ETHGenAddressTopic { get; private set; }
        [JsonProperty("eth_deposit_topic")]
        public string ETHDepositTopic { get; private set; }
        [JsonProperty("eth_withdraw_topic")]
        public string ETHWithdrawTopic { get; private set; }
        [JsonProperty("ETH_asset_id")]
        public SpaceTypeId ETHAsset { get; private set; }
    }


    public sealed class ETHMethodData : SerializableObject
    {
        [JsonProperty("method")]
        public string Method { get; private set; }
        [JsonProperty("gas")]
        public ulong Gas { get; private set; }
    }
}