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
        QRCodeResponseModel GetQRCode(AlfaOption settings, QRCodeRequestModel requestModel)
            => GetQRCodeAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRCodeResponseModel> GetQRCodeAsync(AlfaOption settings, QRCodeRequestModel requestModel);

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRCodeStatusResponseModel GetQRCodeStatus(AlfaOption settings, QRCodeStatusRequestModel requestModel)
            => GetQRCodeStatusAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRCodeStatusResponseModel> GetQRCodeStatusAsync(AlfaOption settings, QRCodeStatusRequestModel requestModel);

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRСodeCashLinkResponseModel RegQRСodeCashLink(AlfaOption settings, QRСodeCashLinkRequestModel requestModel)
            => RegQRСodeCashLinkAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRСodeCashLinkResponseModel> RegQRСodeCashLinkAsync(AlfaOption settings, QRСodeCashLinkRequestModel requestModel);

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRСodeActivateCashLinkResponseModel ActivateQRСodeCashLink(AlfaOption settings, QRСodeActivateCashLinkRequestModel requestModel)
            => ActivateQRСodeCashLinkAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRСodeActivateCashLinkResponseModel> ActivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeActivateCashLinkRequestModel requestModel);

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRСodeDeactivateCashLinkResponseModel DeactivateQRСodeCashLink(AlfaOption settings, QRСodeDeactivateCashLinkRequestModel requestModel)
            => DeactivateQRСodeCashLinkAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRСodeDeactivateCashLinkResponseModel> DeactivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeDeactivateCashLinkRequestModel requestModel);

    }
}
