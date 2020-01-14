using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class ERC20_ConfigData : SerializableObject
    {
        [JsonProperty("contract_code")]
        public string ContractCode { get; private set; }
        [JsonProperty("create_token_fee")]
        public ulong CreateTokenFee { get; private set; }
        [JsonProperty("transfer_topic")]
        public string TransferTopic { get; private set; }
        [JsonProperty("check_balance_method")]
        public ETH_MethodData CheckBalanceMethod { get; private set; }
        [JsonProperty("burn_method")]
        public ETH_MethodData BurnMethod { get; private set; }
        [JsonProperty("issue_method")]
        public ETH_MethodData IssueMethod { get; private set; }
    };
}