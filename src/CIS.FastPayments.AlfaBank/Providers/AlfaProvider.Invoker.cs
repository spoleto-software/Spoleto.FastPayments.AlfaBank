using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CIS.Cryptography.Rsa;
using CIS.FastPayments.AlfaBank.Extensions;
using CIS.FastPayments.AlfaBank.Helpers;
using CIS.FastPayments.AlfaBank.Models;

namespace CIS.FastPayments.AlfaBank.Providers
{
    public partial class AlfaProvider
    {
        private const string _successCode = "0";

        private async Task<T> InvokeAsync<T>(AlfaOption settings, Uri uri, HttpMethod method, string requestJsonContent) where T : IAlfaResponse
        {
            using var certificate = RSACryptoPemHelper.CreateCertificate(settings.Certificate.AlfaPublicBody, settings.Certificate.AlfaPrivateKey);

            var client = HttpClientHelper.CreateClient(certificate);// _httpClientFactory.CreateClient();

            using var requestMessage = new HttpRequestMessage(method, uri);
            InitRequestHeaders(requestMessage, settings, requestJsonContent);

            requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);
            using var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                VerifyResponse(settings, responseMessage, result);

                if (String.IsNullOrEmpty(result))
                    return default;

                var objectResult = JsonHelper.FromJson<T>(result);
                objectResult.Message = DecodePercentEncodedToRealCharacters(objectResult.Message);
                if (objectResult.ErrorCode != _successCode)
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

        private void InitRequestHeaders(HttpRequestMessage requestMessage, AlfaOption settings, string requestJsonContent)
        {
            requestMessage.ConfigureRequestMessage();

            var signedJsonContent = RSACryptoPemHelper.Sign(settings.Certificate.PrivateKey, requestJsonContent);

            requestMessage.Headers.TryAddWithoutValidation("Authorization", signedJsonContent);
            requestMessage.Headers.Add("key-name", settings.Certificate.AlfaAlias);
        }

        private string DecodePercentEncodedToRealCharacters(string s)
        {
            if (s.IndexOf('%', StringComparison.Ordinal) < 0)
                return s;

            return HttpUtility.UrlDecode(s);
        }
    }
}
