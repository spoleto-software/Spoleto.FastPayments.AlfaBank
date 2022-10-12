using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос для регистрация кассовой ссылки СБП.
    /// </summary>
    public class QRСodeCashLinkResponseModel : IAlfaResponse
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
        /// Идентификатор QR-кода.
        /// </summary>
        [JsonPropertyName("qrcId")]
        [Required]
        public string QrcId { get; set; }

        /// <summary>
        /// Платежная ссылка.
        /// </summary>
        [JsonPropertyName("payload")]
        [Required]
        public string Payload { get; set; }

        /// <summary>
        /// Тип картинки QR-кода.
        /// </summary>
        [JsonPropertyName("mediaType")]
        public string MediaType { get; set; }

        /// <summary>
        /// Base64-кодированные данные файла изображения QR-кода. 
        /// </summary>
        [JsonPropertyName("content")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Массив байтов изображения.
        /// </summary>
        public byte[] ContentBytes => Content == null ? null : Convert.FromBase64String(Content);

        /// <summary>
        /// Статус ответа.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
