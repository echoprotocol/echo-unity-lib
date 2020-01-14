using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class EchorandConfigData : SerializableObject
    {
        [JsonProperty("_time_generate")]
        public ulong TimeGenerate { get; private set; }         // timeout in mills to generate block on GC1
        [JsonProperty("_time_net_1mb")]
        public ulong TimeNet1Mb { get; private set; }           // timeout in mills for 1Mb message spreads over the network
        [JsonProperty("_time_net_256b")]
        public ulong TimeNet256b { get; private set; }          // timeout in mills for 256b message spreads over the network
        [JsonProperty("_creator_count")]
        public ulong CreatorCount { get; private set; }         // number of max block creators for this node
        [JsonProperty("_verifier_count")]
        public ulong VerifierCount { get; private set; }        // number of max block verifiers for this node
        [JsonProperty("_ok_threshold")]
        public ulong OkThreshold { get; private set; }          // threshold to made ok decision, recommended eq. 0.69 * _creator_count
        [JsonProperty("_max_bba_steps")]
        public ulong MaxBBASteps { get; private set; }          // max number of BBA steps
        [JsonProperty("_gc1_delay")]
        public ulong GC1Delay { get; private set; }             // delay before sending GC1 messages in milliseconds
        [JsonProperty("_round_attempts")]
        public ulong RoundAttempts { get; private set; }        // number of max attempts to generate non-empty block on round before stop EchoRand
    };
}