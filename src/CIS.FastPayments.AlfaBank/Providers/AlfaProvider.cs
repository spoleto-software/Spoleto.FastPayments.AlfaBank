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
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        public AlfaProvider(ILogger<AlfaProvider> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
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
