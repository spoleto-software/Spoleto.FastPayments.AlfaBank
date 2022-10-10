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
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRCodeResponseModel GetQRCode(AlfaOption settings, QRCodeRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => GetQRCodeAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRCodeResponseModel> GetQRCodeAsync(AlfaOption settings, QRCodeRequestModel requestModel, bool throwIfErrorCodeIsFailed);

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRCodeStatusResponseModel GetQRCodeStatus(AlfaOption settings, QRCodeStatusRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => GetQRCodeStatusAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRCodeStatusResponseModel> GetQRCodeStatusAsync(AlfaOption settings, QRCodeStatusRequestModel requestModel, bool throwIfErrorCodeIsFailed);

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
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRCodeReversalDataResponseModel GetQRCodeReversalData(AlfaOption settings, QRCodeReversalDataRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => GetQRCodeReversalDataAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

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
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRCodeReversalDataResponseModel> GetQRCodeReversalDataAsync(AlfaOption settings, QRCodeReversalDataRequestModel requestModel, bool throwIfErrorCodeIsFailed);

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
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRCodeReversalResponseModel GetQRCodeReversal(AlfaOption settings, QRCodeReversalRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => GetQRCodeReversalAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

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
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRCodeReversalResponseModel> GetQRCodeReversalAsync(AlfaOption settings, QRCodeReversalRequestModel requestModel, bool throwIfErrorCodeIsFailed);

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRСodeCashLinkResponseModel RegQRСodeCashLink(AlfaOption settings, QRСodeCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => RegQRСodeCashLinkAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

        /// <summary>
        /// Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRСodeCashLinkResponseModel> RegQRСodeCashLinkAsync(AlfaOption settings, QRСodeCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed);

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRСodeActivateCashLinkResponseModel ActivateQRСodeCashLink(AlfaOption settings, QRСodeActivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => ActivateQRСodeCashLinkAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRСodeActivateCashLinkResponseModel> ActivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeActivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed);

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        QRСodeDeactivateCashLinkResponseModel DeactivateQRСodeCashLink(AlfaOption settings, QRСodeDeactivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed)
            => DeactivateQRСodeCashLinkAsync(settings, requestModel, throwIfErrorCodeIsFailed).GetAwaiter().GetResult();

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <param name="throwIfErrorCodeIsFailed">Выкинуть исключение, если пришел ответ со статусом не "ОК" (ErrorCode != "0").</param>
        Task<QRСodeDeactivateCashLinkResponseModel> DeactivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeDeactivateCashLinkRequestModel requestModel, bool throwIfErrorCodeIsFailed);

    }
}
