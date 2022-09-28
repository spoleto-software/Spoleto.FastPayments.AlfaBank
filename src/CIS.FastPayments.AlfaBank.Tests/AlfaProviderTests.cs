using CIS.FastPayments.AlfaBank.Extensions;
using CIS.FastPayments.AlfaBank.Helpers;
using CIS.FastPayments.AlfaBank.Models;
using CIS.FastPayments.AlfaBank.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.FastPayments.AlfaBank.Tests
{
    public class Tests
    {
        private ServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddTransient<IAlfaProvider, AlfaProvider>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void OpenSslVerify()
        {
            // Arrange
            var model = new QRCodeRequestModel
            {
                TerminalNumber = "90000018",
                Amount = 1000000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            };
            var json = JsonHelper.ToJson(model);
            var certificate = AlfaOption.DemoOption.Certificate;

            // Act
            var signedData = CryptoHelper.Sign(certificate, json);
            var isVerified = CryptoHelper.Verify(certificate, json, signedData);

            // Assert
            Assert.True(isVerified);
        }

        [Test]
        public void GetQRCode()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(AlfaOption.DemoOption, new QRCodeRequestModel
            {
                TerminalNumber = "90000018",
                Amount = 1000000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            });

            qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.NotNull(qrCode);
        }
    }
}