using System;
using Newtonsoft.Json;
using Tools.HexBinDec;


namespace Base.Data.Json
{
    public sealed class ByteArrayConverter : JsonCustomConverter<byte[], string>
    {
        protected override byte[] Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(byte[] value) => ConvertTo(value);

        public static string ConvertTo(byte[] value) => value.ToHexString();

        public static byte[] ConvertFrom(string value) => value.FromHex2Data();

        public JsonSerializer GetSerializer()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Converters = new JsonConverter[] { this }
            };
            serializerSettings.TypeNameHandling = TypeNameHandling.Objects;
            return JsonSerializer.Create(serializerSettings);
        }
    }
}