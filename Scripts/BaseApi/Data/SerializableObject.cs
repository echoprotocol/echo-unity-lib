using Newtonsoft.Json;


namespace Base.Data
{
    public abstract class SerializableObject
    {
        public override string ToString()
        {
            return Serialize();
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}