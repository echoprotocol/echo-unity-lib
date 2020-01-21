using System;
using Base.Config;
using Base.Data.Operations.Result;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class OperationResultDataPairConverter : JsonCustomConverter<OperationResultData, JArray>
    {
        protected override OperationResultData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.OperationResult)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.OperationResult.Void:
                    return VoidOperationResultData.Create(JToken.FromObject(value.Last));
                case ChainTypes.OperationResult.SpaceTypeId:
                    return SpaceTypeIdOperationResultData.Create(JToken.FromObject(value.Last));
                case ChainTypes.OperationResult.Asset:
                    return AssetOperationResultData.Create(JToken.FromObject(value.Last));
                default:
                    CustomTools.Console.DebugError("Unexpected operation result type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(OperationResultData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JToken.FromObject(value.Value));
        }
    }
}