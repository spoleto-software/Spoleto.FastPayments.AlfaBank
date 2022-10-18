using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.FastPayments.AlfaBank.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса возможности проведения возврата по успешной оплате QR-кода.
    /// </summary>
    public class QRCodeReversalDataRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «GetQRCreversalData».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "GetQRCreversalData";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

        /// <summary>
        /// Референсный номер платежа.
        /// </summary>
        ///  <remarks>
        /// Обязательно для операций по кассовой платежной ссылке.
        ///  </remarks>
        [JsonPropertyName("payrrn")]
        public string Payrrn { get; set; }

        /// <summary>
        /// Идентификатор QR-кода.
        /// </summary>
        /// <remarks>
        /// Для операций по кассовой платежной ссылке ключ не передавать.
        /// </remarks>
        [JsonPropertyName("qrcId")]
        public string QrcId { get; set; }

        /// <summary>
        /// Дата и время отменяемого платежа (ГГГГММДДччммсс).
        /// </summary>
        [JsonConverter(typeof(NumericNullableDateTimeConverter))]
        [JsonPropertyName("trxDT")]
        public DateTime? TrxDateTime { get; set; }

        /// <summary>
        /// Референсный идентификатор операции.
        /// </summary>
        [JsonPropertyName("trxId")]
        [Required]
        public string TrxId { get; set; }

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
        /// Уникальный идентификатор сообщения. Только латинские символы.
        /// </summary>
        [JsonPropertyName("messageID")]
        public string MessageID { get; set; }
    }
}
