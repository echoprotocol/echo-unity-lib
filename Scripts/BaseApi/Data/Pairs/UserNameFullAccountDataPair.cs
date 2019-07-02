using Base.Data.Accounts;
using Base.Data.Json;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(UserNameFullAccountDataPairConverter))]
    public sealed class UserNameFullAccountDataPair : Pair<string, FullAccountData>
    {
        public UserNameFullAccountDataPair(string userName, FullAccountData fullAccount) : base(userName, fullAccount)
        {
            if (!fullAccount.Account.Name.IsNullOrEmpty() && !fullAccount.Account.Name.Equals(userName))
            {
                Key = fullAccount.Account.Name;
            }
        }
    }
}