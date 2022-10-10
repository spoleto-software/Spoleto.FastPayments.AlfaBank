using System.Collections.Concurrent;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using CIS.FastPayments.AlfaBank.Extensions;

namespace CIS.FastPayments.AlfaBank.Helpers
{
    public static class HttpClientHelper
    {
        private static readonly ConcurrentDictionary<string, HttpClient> _clients = new();

        //TODO: надо ли диспозить старые handler, возможно, по на основе кеша со SlidingExpiration.
        public static HttpClient CreateClient(X509Certificate2 certificate)
        {
            var key = certificate?.Thumbprint ?? string.Empty;

            if (!_clients.TryGetValue(key, out var httpClient))
            {
                HttpClientHandler handler = null;
                if (certificate != null)
                {
                    handler = new HttpClientHandler
                    {
                        CheckCertificateRevocationList = false,
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        SslProtocols = SslProtocols.Tls12,
                        ServerCertificateCustomValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) =>
                    {
                        return true;
                    }
                    };

                    handler.ClientCertificates.Add(certificate);
                }

                _clients[key] = httpClient = handler == null ? new HttpClient() : new HttpClient(handler);

                httpClient.ConfigureHttpClient();
            }

            return httpClient;
        }
    }
}
