using Newtonsoft.Json;


namespace Base.Data.Operations.Fee
{
    public class FeeScheduleData : SerializableObject
    {
        [JsonProperty("parameters")]
        public FeeParametersData[] Parameters { get; private set; }
        [JsonProperty("scale")]
        public uint Scale { get; private set; }
    }
}