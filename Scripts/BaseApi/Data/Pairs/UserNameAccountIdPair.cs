using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Pairs
{
    [JsonConverter(typeof(UserNameAccountIdPairConverter))]
    public sealed class UserNameAccountIdPair : Pair<string, SpaceTypeId>
    {
        public UserNameAccountIdPair(string userName, SpaceTypeId id) : base(userName, id) { }
    }
}