using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса для активации кассовой ссылки СБП.
    /// </summary>
    public class QRСodeActivateCashLinkRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «ActivateCashQRc».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "ActivateCashQRc";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

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
        public decimal? Amount { get; set; }

        /// <summary>
        /// Код валюты (для рублей должен передаваться «RUB»).
        /// </summary>
        [JsonPropertyName("currency")]
        [Required]
        public string Currency { get; set; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [JsonPropertyName("paymentPurpose")]
        [Required]
        public string PaymentPurpose { get; set; }

        /// <summary>
        /// Полный URL для получения уведомления о финальном статусе оплаты QR-кода.
        /// </summary>
        /// <remarks>
        /// Если параметр отсутствует, уведомление не производится.
        /// </remarks>
        [JsonPropertyName("queryData")]
        public QRCodeQueryData QRCodeQueryData { get; set; }

        /// <summary>
        /// Период использования Кассовой ссылки в минутах. Необязательное поле.
        /// </summary>
        /// <remarks>
        /// Допустимый диапазон: 5-20.<br/>
        /// Значение по умолчанию – 5 минут.
        /// </remarks>
        [JsonPropertyName("qrTtl")]
        public int? QRTotal { get; set; }
    }
}
