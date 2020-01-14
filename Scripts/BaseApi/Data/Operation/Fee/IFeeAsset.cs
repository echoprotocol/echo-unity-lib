using Base.Data.Assets;


namespace Base.Data.Operations.Fee
{
    public interface IFeeAsset
    {
        AssetData FeeAsset { get; }
    }
}