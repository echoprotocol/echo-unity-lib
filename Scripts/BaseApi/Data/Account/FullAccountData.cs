using Base.Data.Balances;
using Base.Data.Json;
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
        [JsonProperty("votes"), JsonConverter(typeof(VotesConverter))]
        public IdObject[] Votes { get; set; }
        [JsonProperty("balances")]
        public AccountBalanceObject[] Balances { get; set; }
        [JsonProperty("vesting_balances")]
        public VestingBalanceObject[] VestingBalances { get; set; }
        [JsonProperty("proposals")]
        public ProposalObject[] Proposals { get; set; }
        [JsonProperty("assets")]
        public SpaceTypeId[] Assets { get; set; }
    }
}