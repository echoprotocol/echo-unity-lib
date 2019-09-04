using System;
using Base.Data.Json;
using Base.Data.SpecialAuthorities;
using Base.Keys;
using Base.Keys.EDDSA;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data.Accounts
{
    // id "1.2.x"
    public sealed class AccountObject : IdObject
    {
        [JsonProperty("registrar")]
        public SpaceTypeId Registrar { get; private set; }
        [JsonProperty("network_fee_percentage")]
        public ushort NetworkFeePercentage { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
        [JsonProperty("active")]
        public AuthorityData Active { get; private set; }
        [JsonProperty("echorand_key")]
        public PublicKey EchorandKey { get; private set; }
        [JsonProperty("options")]
        public AccountOptionsData Options { get; private set; }
        [JsonProperty("statistics")]
        public SpaceTypeId Statistics { get; private set; }
        [JsonProperty("whitelisting_accounts")]
        public SpaceTypeId[] WhitelistingAccounts { get; private set; }
        [JsonProperty("whitelisted_accounts")]
        public SpaceTypeId[] WhitelistedAccounts { get; private set; }
        [JsonProperty("blacklisted_accounts")]
        public SpaceTypeId[] BlacklistedAccounts { get; private set; }
        [JsonProperty("blacklisting_accounts")]
        public SpaceTypeId[] BlacklistingAccounts { get; private set; }
        [JsonProperty("owner_special_authority")]
        public SpecialAuthorityData OwnerSpecialAuthority { get; private set; }
        [JsonProperty("active_special_authority")]
        public SpecialAuthorityData ActiveSpecialAuthority { get; private set; }
        [JsonProperty("top_n_control_flags")]
        public byte TopNControlFlags { get; private set; }
        [JsonProperty("allowed_assets", NullValueHandling = NullValueHandling.Ignore)]
        public SpaceTypeId[] AllowedAssets { get; private set; }

        public bool IsEquelKey(AuthorityClassification role, KeyPair key)
        {
            switch (role)
            {
                case AuthorityClassification.Active:
                    if (!Active.IsNull() && !Active.KeyAuths.IsNull())
                    {
                        foreach (var keyAuth in Active.KeyAuths)
                        {
                            if (key.Equals(keyAuth.Key))
                            {
                                CustomTools.Console.DebugLog(CustomTools.Console.LogGreenColor("Active->", key.Public, "\n            Active<-", keyAuth.Key));
                                return true;
                            }
                            CustomTools.Console.DebugLog(CustomTools.Console.LogRedColor("generated_key Active->", key.Public, "\ngetted_key        Active<-", keyAuth.Key));
                        }
                    }
                    return false;
                case AuthorityClassification.Echorand:
                    if (!EchorandKey.IsNull())
                    {
                        if (key.Equals(EchorandKey))
                        {
                            CustomTools.Console.DebugLog(CustomTools.Console.LogGreenColor("Echorand->", key.Public, "\n            Echorand<-", EchorandKey));
                            return true;
                        }
                        CustomTools.Console.DebugLog(CustomTools.Console.LogRedColor("generated_key Echorand->", key.Public, "\ngetted_key        Echorand<-", EchorandKey));
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}