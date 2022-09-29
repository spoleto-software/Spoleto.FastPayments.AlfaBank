using System.Threading.Tasks;
using CIS.FastPayments.AlfaBank.Models;

namespace CIS.FastPayments.AlfaBank.Providers
{
    /// <summary>
    /// Провайдер для работы с хостом Альфа-Банка для оплаты покупок через СБП.
    /// </summary>
    public interface IAlfaProvider
    {
        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        QRCodeResponseModel GetQRCode(AlfaOption settings, QRCodeRequestModel requestModel)
            => GetQRCodeAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        Task<QRCodeResponseModel> GetQRCodeAsync(AlfaOption settings, QRCodeRequestModel requestModel);

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        QRСodeCashLinkResponseModel RegQRСodeCashLink(AlfaOption settings, QRСodeCashLinkRequestModel requestModel)
            => RegQRСodeCashLinkAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        Task<QRСodeCashLinkResponseModel> RegQRСodeCashLinkAsync(AlfaOption settings, QRСodeCashLinkRequestModel requestModel);
    }
}
