using System;
using Base.Config;
using Base.Data.Contract.Result;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class ContractResultDataPairConverter : JsonCustomConverter<ContractResultData, JArray>
    {
        protected override ContractResultData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.ContractResult)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.ContractResult.Ethereum:
                    return Contract.Result.Ethereum.ResultExecuteData.Create(value.Last as JObject);
                case ChainTypes.ContractResult.X86_64:
                    return Contract.Result.X86_64.ResultExecuteData.Create(value.Last as JObject);
                default:
                    CustomTools.Console.DebugError("Unexpected contract execution result type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(ContractResultData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JObject.Parse(value.ToString()));
        }
    }
}