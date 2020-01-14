using Newtonsoft.Json;


namespace Base.Data.SpecialAuthorities
{
    public sealed class SpecialAuthorityObject : IdObject
    {
        [JsonProperty("account")]
        public SpaceTypeId Account { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}