using System;
using Base.Config;
using Base.Data.VestingPolicies;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public sealed class VestingPolicyDataPairConverter : JsonCustomConverter<VestingPolicyData, JArray>
    {
        protected override VestingPolicyData Deserialize(JArray value, Type objectType)
        {
            if (value.IsNullOrEmpty() || value.Count != 2)
            {
                return null;
            }
            var type = (ChainTypes.VestingPolicy)Convert.ToInt32(value.First);
            switch (type)
            {
                case ChainTypes.VestingPolicy.Linear:
                    return LinearVestingPolicyData.Create(value.Last as JObject);
                case ChainTypes.VestingPolicy.Cdd:
                    return CddVestingPolicyData.Create(value.Last as JObject);
                default:
                    CustomTools.Console.DebugError("Unexpected vesting policy type:", type);
                    return null;
            }
        }

        protected override JArray Serialize(VestingPolicyData value)
        {
            return value.IsNull() ? new JArray() : new JArray((int)value.Type, JToken.FromObject(value.ToString()));
        }
    }
}