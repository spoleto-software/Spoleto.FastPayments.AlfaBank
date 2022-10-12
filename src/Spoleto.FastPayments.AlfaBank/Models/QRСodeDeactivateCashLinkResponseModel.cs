using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос для деактивации кассовой ссылки СБП.
    /// </summary>
    public class QRСodeDeactivateCashLinkResponseModel : IAlfaResponse
    {
        /// <summary>
        /// Код ответа.
        /// </summary>
        [JsonConverter(typeof(StringIntConverter))]
        [JsonPropertyName("ErrorCode")]
        [Required]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Расшифровка кода ответа.
        /// </summary>
        [JsonConverter(typeof(PercentEncodedStringConverter))]
        [JsonPropertyName("message")]
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public int TerminalNumber { get; set; }

        /// <summary>
        /// Статус ответа.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
