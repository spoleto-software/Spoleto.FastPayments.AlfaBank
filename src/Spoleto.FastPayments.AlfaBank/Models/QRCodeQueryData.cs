using System.Text.Json.Serialization;
using Spoleto.Service.Client.Converters;

namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Дополнительные данные для запроса для получения QR-кода.
    /// </summary>
    public class QRCodeQueryData
    {
        /// <summary>
        /// Полный URL для получения уведомления о фи-\нальном статусе оплаты QR-кода.
        /// </summary>
        /// <remarks>
        /// Если параметр отсутствует, уведомление не производится.
        /// </remarks>
        [JsonPropertyName("notificationUrl")]
        public string NotificationUrl { get; set; }
    }
}
