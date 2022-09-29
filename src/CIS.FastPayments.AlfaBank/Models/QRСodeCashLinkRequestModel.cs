using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса для регистрация кассовой ссылки СБП.
    /// </summary>
    public class QRСodeCashLinkRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «RegCashQRc».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "RegCashQRc";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

        /// <summary>
        /// Тип картинки QR-кода.
        /// </summary>
        /// <remarks>
        /// По умолчанию: "image/png".
        /// </remarks>
        [JsonPropertyName("mediaType")]
        public string MediaType { get; set; } = "image/png";

        /// <summary>
        /// Ширина картинка QR-кода.
        /// </summary>
        [JsonPropertyName("width")]
        public string Width { get; set; }

        /// <summary>
        /// Высота картинка QR-кода.
        /// </summary>
        [JsonPropertyName("height")]
        public string Height { get; set; }

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
