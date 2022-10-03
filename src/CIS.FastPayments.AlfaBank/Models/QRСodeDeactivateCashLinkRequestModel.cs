using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса для деактивации кассовой ссылки СБП.
    /// </summary>
    public class QRСodeDeactivateCashLinkRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «DelCashQRc».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "DelCashQRc";

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
    }
}
