using Base.Data.Assets;
using Newtonsoft.Json;


namespace Base.Data.Operations.Fee
{
    public sealed class FeeForCallContractData : SerializableObject, IFeeAsset
    {
        [JsonProperty("fee")]
        public AssetData Fee { get; private set; }
        [JsonProperty("user_to_pay")]
        public AssetData UserToPay { get; private set; }

        public AssetData FeeAsset => Fee;
    }
}