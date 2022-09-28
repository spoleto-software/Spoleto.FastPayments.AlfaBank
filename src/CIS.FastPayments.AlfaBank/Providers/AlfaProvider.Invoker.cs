using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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
            using var client = _httpClientFactory.CreateClient();
            client.ConfigureHttpClient();

            using var requestMessage = new HttpRequestMessage(method, uri);
            InitRequestHeaders(requestMessage, settings, requestJsonContent);

            requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);

            using var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
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

        private void InitRequestHeaders(HttpRequestMessage requestMessage, AlfaOption settings, string requestJsonContent)
        {
            requestMessage.ConfigureRequestMessage();

            var signedJsonContent = CryptoHelper.Sign(settings.Certificate, requestJsonContent);

            requestMessage.Headers.TryAddWithoutValidation("Authorization", signedJsonContent);
            requestMessage.Headers.Add("key-name", settings.Certificate.Name);
        }

        private string DecodePercentEncodedToRealCharacters(string s)
        {
            if (s.IndexOf('%', StringComparison.Ordinal) < 0)
                return s;

            return HttpUtility.UrlDecode(s);
        }
    }
}
