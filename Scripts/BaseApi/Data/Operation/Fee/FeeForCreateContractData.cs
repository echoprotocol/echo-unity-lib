using Base.Data.Assets;
using Newtonsoft.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class FeeForCreateContractData : SerializableObject, IFeeAsset
    {
        [JsonProperty("fee")]
        public AssetData Fee { get; private set; }

        public AssetData FeeAsset => Fee;
    }
}