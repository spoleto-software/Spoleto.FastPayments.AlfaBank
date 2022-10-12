using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.FastPayments.AlfaBank.Converters;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос возврата по успешной оплате QR-кода.
    /// </summary>
    /// <remarks>
    /// По возврату не делается запрос статуса,
    /// т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.
    /// </remarks>
    public class QRCodeReversalResponseModel : IAlfaResponse
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
        /// Уникальный идентификатор платежа в НСПК.
        /// </summary>
        [JsonPropertyName("trxId")]
        [Required]
        public string TrxId { get; set; }

        /// <summary>
        /// Дата и время платежа в AnyWay (ГГГГММДДччммсс).
        /// </summary>
        [JsonConverter(typeof(NumericDateTimeConverter))]
        [JsonPropertyName("trxDT")]
        [Required]
        public DateTime? TrxDateTime { get; set; }

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
