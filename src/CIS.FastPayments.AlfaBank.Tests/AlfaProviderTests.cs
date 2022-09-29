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
        public void CryptoVerify()
        {
            // Arrange
            var model = new QRCodeRequestModel
            {
                TerminalNumber = "90000018",
                Amount = 10000000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5"
            };
            var json = JsonHelper.ToJson(model);
            var certificate = AlfaOption.DemoOption.Certificate;

            // Act
            var signedData = CryptoHelper.SignByCore(certificate, json);
            var isVerified = CryptoHelper.VerifyByCore(certificate, json, signedData);

            // Assert
            Assert.True(isVerified);
        }

        [Test]
        public void GetQRCode()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5"
            });

            var image = qrCode.ToImage();

            //qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.NotNull(qrCode);
            Assert.NotNull(qrCode.Image);
            Assert.NotNull(image);
        }

        [Test]
        public void RegQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Height = "500",
                Width = "500"
            });

            var image = qrCodeCashLink.ToImage();

            //qrCodeCashLink.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.NotNull(qrCodeCashLink);
            Assert.NotNull(qrCodeCashLink.Content);
            Assert.NotNull(image);
        }

        [Test]
        public void ActivateQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 100000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестового товара по кассовой ссылке",
                QrcId = "123",
                QRTotal = 10
            });

            // Assert
            Assert.NotNull(qrCodeActivateCashLink);
        }
    }
}