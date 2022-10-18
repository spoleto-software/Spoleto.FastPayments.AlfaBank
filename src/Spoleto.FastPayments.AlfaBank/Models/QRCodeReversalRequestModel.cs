using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spoleto.FastPayments.AlfaBank.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса возврата по успешной оплате QR-кода.
    /// </summary>
    public class QRCodeReversalRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «QRCreversal».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "QRCreversal";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

        /// <summary>
        /// Референсный номер платежа.
        /// </summary>
        [JsonPropertyName("payrrn")]
        public string Payrrn { get; set; }

        /// <summary>
        /// Идентификатор QR-кода.
        /// </summary>
        [JsonPropertyName("qrcId")]
        public string QrcId { get; set; }

        /// <summary>
        /// Референсный идентификатор операции.
        /// </summary>
        [JsonPropertyName("trxId")]
        [Required]
        public string TrxId { get; set; }

        /// <summary>
        /// Дата и время отменяемого платежа (ГГГГММДДччммсс).
        /// </summary>
        [JsonConverter(typeof(NumericNullableDateTimeConverter))]
        [JsonPropertyName("trxDT")]
        public DateTime? TrxDateTime { get; set; }

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
        /// Одноразовый пароль для подтверждения воз-врата, вводится кассиром.
        /// </summary>
        /// <remarks>
        /// Возможность использования оговаривается отдельно.
        /// </remarks>
        [JsonPropertyName("OTP")]
        public string OneTimePassword { get; set; }

        /// <summary>
        /// Уникальный идентификатор сообщения, GUID из сообщения «GetQRCreversalData».
        /// </summary>
        /// <remarks>
        /// Обязательно, если было указано при запросе <b>GetQRCreversalData</b>.<br/><br/>
        /// Необходимо передавать точное значение, которые было указано в ответе на запрос GetQRCreversalData.<br/>
        /// Если значение изменилось, необходимо повторно создать запрос с указанием messageID и затем использовать полученный GUID при запросе на возврат.
        /// </remarks>
        [JsonPropertyName("messageID")]
        public string MessageID { get; set; }
    }
}
