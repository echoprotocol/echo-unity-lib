using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Tools.Json
{
    public class JsonDictionary : Dictionary<string, object>
    {
        public JsonDictionary() : base() { }

        public JsonDictionary(IDictionary<string, object> fields) : base(fields) { }
    }


    public class JsonBuilder
    {
        private StringBuilder builder = new StringBuilder();


        public JsonBuilder(JsonDictionary pairs = null)
        {
            if (!pairs.IsNullOrEmpty())
            {
                WriteKeyValuePairs(pairs);
            }
        }

        public JsonBuilder WriteKeyValuePairs(Dictionary<string, object> pairs, params JsonConverter[] converters)
        {
            foreach (var pair in pairs)
            {
                WriteKeyValuePair(pair.Key, pair.Value, converters);
            }
            return this;
        }

        public JsonBuilder WriteKeyValuePair(string key, object value, params JsonConverter[] converters)
        {
            key = JsonConvert.SerializeObject(key);
            value = JsonConvert.SerializeObject(value, converters);
            ((builder.Length > 0) ? builder.Append(',') : builder).Append(key).Append(':').Append(value);
            return this;
        }

        public JsonBuilder WriteOptionalStructKeyValuePair<T>(string key, T? nullable, params JsonConverter[] converters) where T : struct
        {
            return nullable.HasValue ? WriteKeyValuePair(key, nullable.Value, converters) : this;
        }

        public JsonBuilder WriteOptionalClassKeyValuePair<T>(string key, T nullable, params JsonConverter[] converters) where T : class
        {
            return nullable.IsNull() ? this : WriteKeyValuePair(key, nullable, converters);
        }

        public JsonBuilder WriteKeyValuesPair<T>(string key, T[] values, Func<T, string> writeItem)
        {
            if (builder.Length > 0)
            {
                builder.Append(',');
            }
            key = JsonConvert.SerializeObject(key);
            builder.Append(key).Append(':').Append('[');
            for (var i = 0; i < values.Length; i++)
            {
                ((i > 0) ? builder.Append(',') : builder).Append(writeItem(values[i]));
            }
            builder.Append(']');
            return this;
        }

        public string Build() => new StringBuilder().Append('{').Append(builder.ToString()).Append('}').ToString();

        public override string ToString() => Build();
    }


    public static class Extensions
    {
        public static bool IsNullOrEmpty(this JContainer jC) => jC.IsNull() || jC.Count == 0;

        public static T ToNullableObject<T>(this JObject jO) where T : class => jO.IsNull() ? null : jO.ToObject<T>();

        public static async Task<T> ToObjectAsync<T>(this JToken jT) => await Task.Run(() => jT.ToObject<T>());
    }
}