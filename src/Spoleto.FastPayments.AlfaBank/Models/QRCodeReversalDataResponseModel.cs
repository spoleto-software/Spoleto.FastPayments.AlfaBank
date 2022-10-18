using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.FastPayments.AlfaBank.Converters;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос возможности проведения возврата по успешной оплате QR-кода.
    /// </summary>
    public class QRCodeReversalDataResponseModel : IAlfaResponse
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
        public string QrcId { get; set; }

        /// <summary>
        /// Уникальный идентификатор платежа в НСПК.
        /// </summary>
        [JsonPropertyName("trxId")]
        [Required]
        public string TrxId { get; set; }

        /// <summary>
        /// Дата и время платежа в AnyWay (ГГГГММДДччммсс).
        /// </summary>
        [JsonConverter(typeof(NumericNullableDateTimeConverter))]
        [JsonPropertyName("trxDT")]
        [Required]
        public DateTime? TrxDateTime { get; set; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        [JsonPropertyName("payerId")]
        [Required]
        public string PayerId { get; set; }

        /// <summary>
        /// PAM получателя (Personal Authentication Message).
        /// </summary>
        [JsonPropertyName("payerPam")]
        [Required]
        public string PayerPam { get; set; }

        /// <summary>
        /// БИК банка получателя.
        /// </summary>
        [JsonPropertyName("payerBnkBIC")]
        [Required]
        public string PayerBnkBIC { get; set; }

        /// <summary>
        /// Наименование банка получателя.
        /// </summary>
        [JsonPropertyName("payerBnkName")]
        [Required]
        public string PayerBnkName { get; set; }

        /// <summary>
        /// Референсный номер платежа из запроса.
        /// </summary>
        [JsonPropertyName("payrrn")]
        public string Payrrn { get; set; }

        /// <summary>
        /// Уникальный идентификатор сообщения. Только латинские символы.
        /// </summary>
        [JsonPropertyName("messageID")]
        public string MessageID { get; set; }
    }
}
