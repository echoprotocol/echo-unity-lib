using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json;
using Tools.HexBinDec;


namespace Base.Data.Transactions
{
    public class SignedTransactionData : TransactionData
    {
        [JsonProperty("signatures")]
        public string[] Signatures { get; set; }

        public SignedTransactionData() : base()
        {
            Signatures = new string[0];
        }

        public SignedTransactionData(TransactionBuilder builder) : base(builder)
        {
            Signatures = builder.Signatures.ConvertAll(signature => signature.ToHexString());
        }
    }
}