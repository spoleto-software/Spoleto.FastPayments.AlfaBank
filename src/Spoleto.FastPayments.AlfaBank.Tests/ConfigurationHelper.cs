using System.Text;
using Microsoft.Extensions.Configuration;
using Spoleto.FastPayments.AlfaBank.Models;

namespace Spoleto.FastPayments.AlfaBank.Tests
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ConfigurationHelper
    {
        private static readonly IConfigurationRoot _config;

        static ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true)
               .AddUserSecrets("c64ed9ba-1742-45fc-8261-c1a8356040ed")
               .Build();
        }

        public static AlfaOption GetAlfaSettings()
        {
            var settings = _config.GetSection(nameof(AlfaOption)).Get<AlfaOption>();

            settings.Certificate.PublicBody = Encoding.UTF8.GetString(Convert.FromBase64String(settings.Certificate.PublicBody));
            settings.Certificate.PrivateKey = Encoding.UTF8.GetString(Convert.FromBase64String(settings.Certificate.PrivateKey));

            if (settings.Certificate.AlfaPrivateKey != null)
                settings.Certificate.AlfaPrivateKey = Encoding.UTF8.GetString(Convert.FromBase64String(settings.Certificate.AlfaPrivateKey));

            if (settings.Certificate.AlfaPublicBody != null)
                settings.Certificate.AlfaPublicBody = Encoding.UTF8.GetString(Convert.FromBase64String(settings.Certificate.AlfaPublicBody));

            return settings;
        }
    }
}
