using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace Spoleto.Service.Client.Converters
{
    /// <summary>
    /// JSON converter with support string as percent encoded string.
    /// </summary>
    public class PercentEncodedStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();

            if (str.IndexOf('%', StringComparison.Ordinal) < 0)
                return str;

            return HttpUtility.UrlDecode(str);
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            var encodedStr = value;// HttpUtility.UrlEncode(value);
            writer.WriteStringValue(encodedStr);
        }
    }
}
