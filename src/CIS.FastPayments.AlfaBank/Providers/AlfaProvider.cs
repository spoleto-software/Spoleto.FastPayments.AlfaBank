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

            var result = await InvokeAsync<QRCodeResponseModel>(settings, uri, HttpMethod.Post, jsonModel);

            return result;
        }
    }
}
