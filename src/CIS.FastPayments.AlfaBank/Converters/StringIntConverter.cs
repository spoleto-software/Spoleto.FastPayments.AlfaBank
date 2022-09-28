using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.Converters
{
    /// <summary>
    /// JSON converter with support string as int.
    /// </summary>
    public class StringIntConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    var str = reader.GetString();
                    return str;

                case JsonTokenType.Number:
                    var num = reader.GetInt32();
                    return num.ToString();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value);
    }
}
