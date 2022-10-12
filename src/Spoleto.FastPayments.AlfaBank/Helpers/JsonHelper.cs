using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Spoleto.FastPayments.AlfaBank.Helpers
{
    public static class JsonHelper
    {
        private static readonly JavaScriptEncoder _encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
        private static readonly JsonSerializerOptions _defaultSerializerOptions;

        static JsonHelper()
        {
            _defaultSerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                Encoder = _encoder
            };

            _defaultSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        /// <summary>
        /// Adds the custom converter for Json serializer.
        /// </summary>
        /// <param name="converter">The custom converter.</param>
        public static void AddConverter(JsonConverter converter)
        {
            if (!_defaultSerializerOptions.Converters.Contains(converter))
                _defaultSerializerOptions.Converters.Add(converter);
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static string ToJson<T>(T body)
        {
            if (body == null)
                return null;

            var bodyJson = JsonSerializer.Serialize(body, _defaultSerializerOptions);

            return bodyJson;
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static T FromJson<T>(string bodyJson)
        {
            if (string.IsNullOrEmpty(bodyJson))
                return default;

            var body = JsonSerializer.Deserialize<T>(bodyJson, _defaultSerializerOptions);

            return body;
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static string ToJson(object body, Type inputType)
        {
            var bodyJson = JsonSerializer.Serialize(body, inputType, _defaultSerializerOptions);

            return bodyJson;
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static object FromJson(string bodyJson, Type returnType)
        {
            var body = JsonSerializer.Deserialize(bodyJson, returnType, _defaultSerializerOptions);
            return body;
        }

        /// <summary>
        /// Serialize object to Json format
        /// </summary>
        public static async Task ToJsonStreamAsync<T>(T body, Stream streamTo)
        {
            await JsonSerializer.SerializeAsync(streamTo, body, _defaultSerializerOptions).ConfigureAwait(false);
        }

        /// <summary>
        /// Deserialize object to Json format
        /// </summary>
        public static async Task<T> FromJsonStreamAsync<T>(Stream jsonStream)
        {
            var body = await JsonSerializer.DeserializeAsync<T>(jsonStream, _defaultSerializerOptions).ConfigureAwait(false);

            return body;
        }
    }
}
