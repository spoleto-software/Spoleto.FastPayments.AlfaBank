using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос для получения QR-кода.
    /// </summary>
    public class QRCodeResponseModel : IAlfaResponse
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
        /// Изображение QR-кода содержит энкодированный BASE64 файл изображения QR-кода 300х300 pix формата .png 
        /// </summary>
        [JsonPropertyName("image")]
        [Required]
        public string Image { get; set; }

        /// <summary>
        /// Массив байтов изображения.
        /// </summary>
        public byte[] ImageBytes => Image == null ? null : Convert.FromBase64String(Image);

        /// <summary>
        /// Референсный номер платежа.
        /// </summary>
        [JsonPropertyName("payrrn")]
        [Required]
        public string Payrrn { get; set; }

        /// <summary>
        /// Статус ответа.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
