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
        /// Запрос возможности проведения возврата по успешной оплате QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRCodeReversalDataResponseModel GetQRCodeReversalData(AlfaOption settings, QRCodeReversalDataRequestModel requestModel)
            => GetQRCodeReversalDataAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос возможности проведения возврата по успешной оплате QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRCodeReversalDataResponseModel> GetQRCodeReversalDataAsync(AlfaOption settings, QRCodeReversalDataRequestModel requestModel);

        /// <summary>
        /// Запрос возврата оплаты QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        QRCodeReversalResponseModel GetQRCodeReversal(AlfaOption settings, QRCodeReversalRequestModel requestModel)
            => GetQRCodeReversalAsync(settings, requestModel).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос возврата оплаты QR-кода.
        /// </summary>
        /// <remarks>
        /// Возврат состоит из 2-х последовательных запросов:
        /// <list type="number">
        ///     <item>
        ///         <term>GetQRCreversalData</term>
        ///         <description>это предварительная проверка возврата, а далее методом QRCreversal выполнить возврат операции.</description>
        ///     </item>
        ///     <item>
        ///         <term>QRCreversal</term>
        ///         <description>по возврату не делается запрос статуса, т.к отсутствие ошибок ("ErrorCode": 0) в методе QRCreversal означает успешно проведённую операцию возврата.</description>
        ///     </item>     
        /// </list>
        /// </remarks>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        Task<QRCodeReversalResponseModel> GetQRCodeReversalAsync(AlfaOption settings, QRCodeReversalRequestModel requestModel);

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
