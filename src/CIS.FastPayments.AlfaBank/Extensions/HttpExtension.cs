using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CIS.FastPayments.AlfaBank.Extensions
{
    public static class HttpExtension
    {
        public static void ConfigureRequestMessage(this HttpRequestMessage requestMessage)
        {
            requestMessage.Headers.Accept.Clear();
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultSettings.ContentType));
            requestMessage.Headers.AcceptCharset.ParseAdd(DefaultSettings.Charset);
        }

        public static void ConfigureHttpClient(this HttpClient client)
        {
            client.Timeout = new TimeSpan(0, 0, 5, 0);
        }

        public static void InitializeException(this Exception exception, HttpResponseMessage responseMessage)
        {
            exception.Data.Add(nameof(responseMessage.StatusCode), responseMessage.StatusCode);
            exception.Data.Add(nameof(responseMessage.ReasonPhrase), responseMessage.ReasonPhrase);

            if (responseMessage.RequestMessage?.RequestUri != null)
                exception.Data.Add(nameof(responseMessage.RequestMessage.RequestUri), responseMessage.RequestMessage?.RequestUri);

        }
    }
}
