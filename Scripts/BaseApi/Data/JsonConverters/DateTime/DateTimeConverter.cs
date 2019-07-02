using System;
using System.Globalization;
using Newtonsoft.Json;


namespace Base.Data.Json
{
    public sealed class DateTimeConverter : JsonCustomConverter<DateTime, string>
    {
        private const string DATE_TIME_FORMAT = "G"; // "MM/dd/yyyy HH:mm:ss";


        protected override DateTime Deserialize(string value, Type objectType) => ConvertFrom(value);

        protected override string Serialize(DateTime value) => ConvertTo(value);

        public static string ConvertTo(DateTime value) => SpecifyKindToUtc(value).ToString(DATE_TIME_FORMAT, DateTimeFormatInfo.InvariantInfo);

        public static DateTime ConvertFrom(string value)
        {
            var result = DateTime.Now;
            if (!DateTime.TryParseExact(value, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                CustomTools.Console.Error("Unable to convert:", value, "Expected format:", DATE_TIME_FORMAT);
            }
            result = SpecifyKindToUtc(result);
            return result;
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