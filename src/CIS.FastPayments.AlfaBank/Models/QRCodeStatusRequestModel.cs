using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Параметры запроса для получения статуса QR-кода.
    /// </summary>
    public class QRCodeStatusRequestModel : IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        /// <remarks>
        /// Константа «GetQRCstatus».
        /// </remarks>
        [JsonPropertyName("command")]
        [Required]
        public string Command { get; } = "GetQRCstatus";

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        [JsonPropertyName("TermNo")]
        [Required]
        public string TerminalNumber { get; set; }

        /// <summary>
        /// Референсный идентификатор запроса.
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
        /// Уникальный идентификатор сообщения, GUID.
        /// </summary>
        [JsonPropertyName("messageID")]
        public Guid? MessageID { get; set; }


    }
}
