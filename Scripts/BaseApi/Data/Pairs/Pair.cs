using Newtonsoft.Json.Linq;


namespace Base.Data.Pairs
{
    public abstract class Pair<K, V>
    {
        public K Key { get; protected set; }
        public V Value { get; protected set; }

        protected Pair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public virtual JArray ToJArray() => new JArray(JToken.FromObject(Key), JToken.FromObject(Value));
    }
}