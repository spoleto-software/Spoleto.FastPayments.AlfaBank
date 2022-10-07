using System;
using System.Net.Http;
using System.Threading.Tasks;
using CIS.FastPayments.AlfaBank.Helpers;
using CIS.FastPayments.AlfaBank.Models;
using Microsoft.Extensions.Logging;

namespace CIS.FastPayments.AlfaBank.Providers
{
    /// <summary>
    /// Провайдер для работы с хостом Альфа-Банка для оплаты покупок через СБП.
    /// </summary>
    public partial class AlfaProvider : IAlfaProvider
    {
        private readonly ILogger<AlfaProvider> _logger;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        public AlfaProvider(ILogger<AlfaProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Запрос QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        public async Task<QRCodeResponseModel> GetQRCodeAsync(AlfaOption settings, QRCodeRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Запрос статуса оплаты QR-кода.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        public async Task<QRCodeStatusResponseModel> GetQRCodeStatusAsync(AlfaOption settings, QRCodeStatusRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeStatusResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }

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
        public async Task<QRCodeReversalDataResponseModel> GetQRCodeReversalDataAsync(AlfaOption settings, QRCodeReversalDataRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeReversalDataResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }

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
        public async Task<QRCodeReversalResponseModel> GetQRCodeReversalAsync(AlfaOption settings, QRCodeReversalRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRCodeReversalResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        ///  Регистрация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        /// <returns></returns>
        public async Task<QRСodeCashLinkResponseModel> RegQRСodeCashLinkAsync(AlfaOption settings, QRСodeCashLinkRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeCashLinkResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Активация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        public async Task<QRСodeActivateCashLinkResponseModel> ActivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeActivateCashLinkRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeActivateCashLinkResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            result.PaymentPurpose = DecodePercentEncodedToRealCharacters(result.PaymentPurpose);

            return result;
        }

        /// <summary>
        /// Деактивация Кассовой ссылки СБП.
        /// </summary>
        /// <param name="settings">Настройки для API.</param>
        /// <param name="requestModel">Параметры запроса.</param>
        public async Task<QRСodeDeactivateCashLinkResponseModel> DeactivateQRСodeCashLinkAsync(AlfaOption settings, QRСodeDeactivateCashLinkRequestModel requestModel)
        {
            var uri = new Uri(settings.ServiceUrl);

            var jsonModel = JsonHelper.ToJson(requestModel);

            var result = await InvokeAsync<QRСodeDeactivateCashLinkResponseModel>(settings, uri, HttpMethod.Post, jsonModel).ConfigureAwait(false);

            return result;
        }
    }
}
