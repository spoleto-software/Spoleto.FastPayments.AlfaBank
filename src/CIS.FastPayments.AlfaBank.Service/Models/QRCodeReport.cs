using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.FastPayments.AlfaBank.Service.Models
{
    /// <summary>
    /// Уведомлением от НСПК по адресу из параметра “notificationUrl”.
    /// </summary>
    public class QRCodeReport
    {
        /// <summary>
        /// Идентификатор QR-кода.
        /// </summary>
        [JsonPropertyName("qrcId")]
        [Required]
        public string QrcId { get; set; }

        /// <summary>
        /// Номер транзакции СБП.
        /// </summary>
        [JsonPropertyName("trxId")]
        public string TrxId { get; set; }

        /// <summary>
        /// Идентификатор активных значений параметров Платежной ссылки СБП.
        /// </summary>
        /// <remarks>
        /// Передается, если оплата происходила по Кассовой ссылке СБП.
        /// </remarks>
        [JsonPropertyName("paramsId")]
        public string ParamsId { get; set; }

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
        public string Status { get; set; }

        /// <summary>
        /// Сумма операции СБП.
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Дата и время выполнения Операции СБП.
        /// </summary>
        [JsonPropertyName("timestamp ")]
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Маскированный номер телефона Клиента-Плательщика.
        /// </summary>
        [JsonPropertyName("payerId")]
        public string PayerId { get; set; }

        /// <summary>
        /// Уникальный идентификатор операции присвоенный от НСПК.
        /// </summary>
        [JsonPropertyName("kzo")]
        public string Kzo { get; set; }
    }
}
