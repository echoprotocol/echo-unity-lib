using System;
using Buffers;
using Base.Data.Pairs;
using Newtonsoft.Json;


namespace Base.Data
{
    public enum AuthorityClassification
    {
        Active/*   */= 0,
        Echorand/* */= 1
    }


    public sealed class AuthorityData : SerializableObject, ISerializeToBuffer
    {
        [JsonProperty("weight_threshold")]
        public uint WeightThreshold { get; private set; }
        [JsonProperty("account_auths")]
        public AccountIdWeightPair[] AccountAuths { get; private set; }
        [JsonProperty("key_auths")]
        public PublicKeyWeightPair[] KeyAuths { get; private set; }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            //buffer = buffer ?? new ByteBuffer( ByteBuffer.LITTLE_ENDING );
            //return buffer;
            throw new NotImplementedException();
        }
    }
}