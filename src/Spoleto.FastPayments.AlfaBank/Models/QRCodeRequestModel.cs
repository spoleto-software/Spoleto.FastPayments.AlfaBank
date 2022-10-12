using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса для получения QR-кода.
    /// </summary>
    public class QRCodeRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «GetQRCd».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "GetQRCd";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

        /// <summary>
        /// Сумма платежа в минорных единицах (копейках).
        /// </summary>
        /// <remarks>
        /// Обязательна при значении qrcType = 02.
        /// </remarks>
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Код валюты (для рублей должен передаваться «RUB»).
        /// </summary>
        /// <remarks>
        /// Обязательна при значении qrcType=02.
        /// </remarks>
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Тип запрашиваемого QR-кода.
        /// </summary>
        /// <remarks>
        /// •	если тип не указан определяется настройкой ТСТ на AWGW;<br/>
        /// •	если указан, при запросе QR-кода в НСПК передается указанное значение:<br/>
        /// 01 – для статического QR-кода;<br/>
        /// 02 – для динамического QR-кода.
        /// </remarks>
        [StringLength(2)]
        [JsonPropertyName("qrcType")]
        public string QRCodeType { get; set; }

        /// <summary>
        /// Назначение платежа.
        /// </summary>
        [JsonPropertyName("paymentPurpose")]
        [Required]
        public string PaymentPurpose { get; set; }

        /// <summary>
        /// Уникальный идентификатор сообщения, GUID.
        /// </summary>
        [JsonPropertyName("messageID")]
        public Guid? MessageID { get; set; }

        /// <summary>
        /// Минимальное значение срока использования созданной динамической платежной ссылки в целых минутах.
        /// </summary>
        /// <remarks>
        /// Допустимый диапозон: 1-129600.<br/>
        /// Если параметр отсутствует, НСПК устанавливает значение по умолчанию, равное 72 часам.
        /// </remarks>
        [JsonPropertyName("qrTtl")]
        public int? QRTotal { get; set; }

        /// <summary>
        /// Дополнительные данные для запроса для получения QR-кода.
        /// </summary>
        [JsonPropertyName("queryData")]
        public QRCodeQueryData QRCodeQueryData { get; set; }

        /// <summary>
        /// Содержит   ссылку   для автоматического воз-врата  в  приложение или  на сайт ТСП.<br/>
        /// Допускаются  только символы в кодировке ASCII.<br/>
        /// Формат должен соответствовать правилам кодировки URL.
        /// </summary>
        /// <remarks>
        /// Если все же требуется использовать в redirectUrl кириллические символы,
        /// необходимо перекодировать их в соответствующий ASCII код,
        /// используя percent-encoded octets согласно RFC3986.
        /// </remarks>
        [JsonPropertyName("redirectUrl")]
        public string RedirectUrl { get; set; }
    }
}
