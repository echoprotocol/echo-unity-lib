using System;
using Base.Data.Pairs;
using CustomTools.Extensions.Core;
using Newtonsoft.Json.Linq;
using Tools.Json;


namespace Base.Data.Json
{
    public abstract class PairConverter<TPair, K, V> : JsonCustomConverter<Pair<K, V>, JArray> where TPair : Pair<K, V>
    {
        protected override Pair<K, V> Deserialize(JArray value, Type objectType)
        {
            return (value.IsNullOrEmpty() || value.Count != 2) ? null : ConvertFrom(value.First, value.Last);
        }

        protected override JArray Serialize(Pair<K, V> value)
        {
            return value.IsNull() ? new JArray() : ConvertTo(value);
        }

        protected virtual Pair<K, V> ConvertFrom(JToken key, JToken value) => (TPair)Activator.CreateInstance(typeof(TPair), key.ToObject<K>(), value.ToObject<V>());

        protected virtual JArray ConvertTo(Pair<K, V> pair) => pair.ToJArray();
    }
}