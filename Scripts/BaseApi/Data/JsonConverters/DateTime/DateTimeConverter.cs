using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Base.Data.Json
{
    public sealed class DateTimeConverter : JsonCustomConverter<DateTime, JToken>
    {
        private const string DATE_TIME_FORMAT = "s"; // "yyyy-MM-ddTHH:mm:ss";


        protected override DateTime Deserialize(JToken value, Type objectType) => ConvertFrom(value);

        protected override JToken Serialize(DateTime value) => ConvertTo(value);

        public static JToken ConvertTo(DateTime value)
        {
            return JToken.FromObject(SpecifyKindToUtc(value).ToString(DATE_TIME_FORMAT, DateTimeFormatInfo.InvariantInfo));
        }

        public static DateTime ConvertFrom(JToken value)
        {
            var result = DateTime.Now;
            if (value.Type.Equals(JTokenType.Date))
            {
                result = value.ToObject<DateTime>();
            } else
            if (value.Type.Equals(JTokenType.String))
            {
                if (!DateTime.TryParseExact(value.ToString(), DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    CustomTools.Console.Error("Unable to convert:", value, "Expected format:", DATE_TIME_FORMAT);
                }
            }
            return SpecifyKindToUtc(result);
        }

        private static DateTime SpecifyKindToUtc(DateTime date)
        {
            switch (date.Kind)
            {
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(date, DateTimeKind.Utc);
                case DateTimeKind.Local:
                    return date.ToUniversalTime();
                default:
                    return date;
            }
        }

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