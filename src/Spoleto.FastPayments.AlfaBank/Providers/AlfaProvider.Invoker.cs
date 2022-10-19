using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Spoleto.FastPayments.AlfaBank.Extensions;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;
using Spoleto.Cryptography.Rsa;

namespace Spoleto.FastPayments.AlfaBank.Providers
{
    public partial class AlfaProvider
    {
        private readonly ConcurrentDictionary<string, X509Certificate2> _certificateCache = new();

        private async Task<T> InvokeAsync<T>(AlfaOption settings, Certificate certificate, Uri uri, HttpMethod method, string requestJsonContent, bool throwIfErrorCodeIsFailed) where T : IAlfaResponse
        {
            var rsaCertificate = certificate.AlfaPublicBody != null
                ? _certificateCache.GetOrAdd(certificate.AlfaPublicBody, x => RSACryptoPemHelper.CreateCertificate(certificate.AlfaPublicBody, certificate.AlfaPrivateKey, certificate.AlfaPassword))
                : null;

            var client = HttpClientHelper.CreateClient(rsaCertificate);// _httpClientFactory.CreateClient();

            using var requestMessage = new HttpRequestMessage(method, uri);
            InitRequestHeaders(requestMessage, certificate, requestJsonContent);

            requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);
            using var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                VerifyResponse(settings, responseMessage, result);

                if (String.IsNullOrEmpty(result))
                    return default;

                var objectResult = JsonHelper.FromJson<T>(result);
                if (throwIfErrorCodeIsFailed
                    && objectResult.ErrorCode != Constants.SuccessCode)
                {
                    throw new Exception($"{nameof(objectResult.ErrorCode)}: {objectResult.ErrorCode}{Environment.NewLine}{objectResult.Message}.");
                }

                return objectResult;
            }

            var errorResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            Exception exception;
            if (!String.IsNullOrEmpty(errorResult))
            {
                exception = new Exception(errorResult);
            }
            else
            {
                exception = new Exception($"{nameof(responseMessage.StatusCode)}: {responseMessage.StatusCode}." +
                    $"{Environment.NewLine}{nameof(responseMessage.ReasonPhrase)}: {responseMessage.ReasonPhrase}.");
            }

            exception.InitializeException(responseMessage);

            throw exception;
        }

        //check and maybe remove
        private void VerifyResponse(AlfaOption settings, HttpResponseMessage responseMessage, string result)
        {
            var auth = responseMessage.Headers.TryGetValues("Authorization", out var values) ? values.FirstOrDefault() : null;
            if (auth == null)
            {
                throw new Exception("Не найдены заголовки в ответе от сервиса Альфа-Банка для авторизации ответа.");
            }

            //var isVerified = CryptoHelper.VerifyByCore(settings.Certificate, result, auth);
            //if (!isVerified)
            //{
            //    //throw new Exception("Не пройдена проверка ответа от сервиса Альфа-Банка с помощью сертификата.");
            //}
        }

        private void InitRequestHeaders(HttpRequestMessage requestMessage, Certificate certificate, string requestJsonContent)
        {
            requestMessage.ConfigureRequestMessage();

            var signedJsonContent = RSACryptoPemHelper.Sign(certificate.PrivateKey, requestJsonContent);

            requestMessage.Headers.TryAddWithoutValidation("Authorization", signedJsonContent);
            requestMessage.Headers.Add("key-name", certificate.AlfaAlias);
        }


    }
}
