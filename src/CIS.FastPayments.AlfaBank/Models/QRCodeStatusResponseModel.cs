using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CIS.Service.Client.Converters;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Ответ на запрос для получения статуса QR-кода.
    /// </summary>
    public class QRCodeStatusResponseModel : IAlfaResponse
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
        /// Статус оплаты QR-кода в НСПК.
        /// </summary>
        /// <remarks>
        /// Успешно оплаченным считается только QR-код, для которого получен «status» =  «ACWP» и «trxId».<br/>
        /// «status» = «NTST», «RCVD» или «ACTC» - означает необходимость повтора запроса до получения одного из финальных статусов «ACWP» или «RJCT».<br/>
        /// Получение «status» = «RJCT» означает отказ в оплате данного QR-кода
        /// </remarks>
        [JsonPropertyName("status")]
        [Required]
        public QRCodeStatus Status { get; set; }

        /// <summary>
        /// Уникальный идентификатор платежа в НСПК.
        /// </summary>
        /// <remarks>
        /// В случае получения значения trxId = null, повторить запрос – отсутствие значения означает,
        /// что в СБП еще нет успешного перевода с оплатой QR-кода с соответствующим идентификатором.
        /// </remarks>
        [JsonPropertyName("trxId")]
        [Required]
        public string TrxId { get; set; }

        /// <summary>
        /// Дата и время платежа в AnyWay (ГГГГММДДччммсс).
        /// </summary>
        [JsonPropertyName("trxDT")]
        public string TrxDateTime { get; set; }

        /// <summary>
        /// Референсный номер платежа из запроса.
        /// </summary>
        [JsonPropertyName("payrrn")]
        public string Payrrn { get; set; }

        /// <summary>
        /// Уникальный идентификатор смены.
        /// </summary>
        [JsonPropertyName("BatchId")]
        [Required]
        public int BatchId { get; set; }
    }
}
