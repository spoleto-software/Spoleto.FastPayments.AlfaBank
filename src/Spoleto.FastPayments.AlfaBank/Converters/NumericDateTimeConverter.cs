using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Spoleto.FastPayments.AlfaBank.Converters
{
    /// <summary>
    /// JSON converter with support datetime as int.
    /// </summary>
    public class NumericDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly string _format = "yyyyMMddHHmmss";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    {
                        var str = reader.GetString();
                        var dt = DateTime.ParseExact(str, _format, System.Globalization.CultureInfo.InvariantCulture);
                        return dt;
                    }

                case JsonTokenType.Number:
                    {
                        var num = reader.GetInt64();
                        var str = num.ToString();
                        var dt = DateTime.ParseExact(str, _format, System.Globalization.CultureInfo.InvariantCulture);
                        return dt;
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(_format));
    }
}
