using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос для активации кассовой ссылки СБП.
    /// </summary>
    public class QRСodeActivateCashLinkResponseModel : IAlfaResponse
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
        /// Идентификатор параметров ссылки СБП.
        /// </summary>
        [JsonPropertyName("paramsId")]
        [Required]
        public string ParamsId { get; set; }

        /// <summary>
        /// Идентификатор QR-кода.
        /// </summary>
        [JsonPropertyName("qrcId")]
        [Required]
        public string QrcId { get; set; }

        /// <summary>
        /// Сумма платежа в минорных единицах (копейках).
        /// </summary>
        [JsonPropertyName("amount")]
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Код валюты (для рублей должен передаваться «RUB»).
        /// </summary>
        [JsonPropertyName("currency")]
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [JsonConverter(typeof(PercentEncodedStringConverter))]
        [JsonPropertyName("paymentPurpose")]
        [Required]
        public string PaymentPurpose { get; set; }

        /// <summary>
        /// Референсный идентификатор запроса.
        /// </summary>
        [JsonPropertyName("payrrn")]
        [Required]
        public string Payrrn { get; set; }

        /// <summary>
        /// Платежная ссылка.
        /// </summary>
        [JsonPropertyName("payload")]
        [Required]
        public string Payload { get; set; }

        /// <summary>
        /// Статус ответа.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
