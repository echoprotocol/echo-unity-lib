using System;
using Base.Data.Json;
using Base.Data.Transactions;
using Newtonsoft.Json;


namespace Base.Data.Block
{
    public class BlockHeaderData : SerializableObject
    {
        [JsonProperty("previous")]
        public string Previous { get; private set; }
        [JsonProperty("round")]
        public ulong Round { get; private set; }
        [JsonProperty("timestamp"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; private set; }
        [JsonProperty("account")]
        public SpaceTypeId Account { get; private set; }
        [JsonProperty("delegate")]
        public SpaceTypeId Delegate { get; private set; }
        [JsonProperty("transaction_merkle_root")]
        public string TransactionMerkleRoot { get; private set; }
        [JsonProperty("vm_root")]
        public string[] VMRoot { get; private set; }
        [JsonProperty("prev_signatures")]
        public BlockSignatureData[] PreviousSignatures { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }


    public class SignedBlockHeaderData : BlockHeaderData
    {
        [JsonProperty("ed_signature")]
        public string EdSignature { get; private set; }
        [JsonProperty("rand")]
        public string Rand { get; private set; }
        [JsonProperty("cert")]
        public BlockSignatureData[] Certificate { get; private set; }
    }


    public class BlockSignatureData : SerializableObject
    {
        [JsonProperty("_step")]
        public ulong Step { get; private set; }
        [JsonProperty("_value")]
        public byte Value { get; private set; }
        [JsonProperty("_leader")]
        public ulong Leader { get; private set; }
        [JsonProperty("_producer")]
        public ulong Producer { get; private set; }
        [JsonProperty("_delegate")]
        public ulong Delegate { get; private set; }
        [JsonProperty("_fallback")]
        public ulong Fallback { get; private set; }
        [JsonProperty("_bba_sign")]
        public string BBASign { get; private set; }
    }


    public sealed class SignedBlockData : SignedBlockHeaderData
    {
        [JsonProperty("transactions")]
        public ProcessedTransactionData[] Transactions { get; private set; }
    }
}