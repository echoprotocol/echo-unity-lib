using Newtonsoft.Json;


namespace Base.Data.SpecialAuthorities
{
    // id "2.11.x"
    public sealed class SpecialAuthorityObject : IdObject
    {
        [JsonProperty("account")]
        public SpaceTypeId Account { get; private set; }
    }
}