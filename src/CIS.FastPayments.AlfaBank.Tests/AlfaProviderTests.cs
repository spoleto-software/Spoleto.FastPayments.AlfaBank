using CIS.Cryptography.Rsa;
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
            var signedData = RSACryptoPemHelper.Sign(certificate.PrivateKey, json);
            var isVerified = RSACryptoPemHelper.Verify(certificate.AlfaPublicBody, json, signedData);

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
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5"
            }, false);

            var qrCode2 = provider.GetQRCode(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5"
            }, false);

            var image = qrCode.ToImage();

            //qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                Assert.That(image, Is.Not.Null);
            });
        }

        [Test]
        public void GetQRCodeStatus()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            }, false);

            Thread.Sleep(500);

            var qrCodeStatus = provider.GetQRCodeStatus(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId
            }, false);

            // Assert
            Assert.NotNull(qrCode);
            Assert.NotNull(qrCode.Image);
            Assert.NotNull(qrCodeStatus);
        }

        [Test]
        public void GetQRCodeReversalData()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var messageId = "MyUniqRequest";

            // Act
            var qrCode = provider.GetQRCode(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            }, false);

            Thread.Sleep(500);

            // test for a fake payment:
            var qrCodeReversalData = provider.GetQRCodeReversalData(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId,
                Amount = 10000,
                Currency = "RUB",
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.NotNull(qrCode);
            Assert.NotNull(qrCode.Image);
            Assert.NotNull(qrCodeReversalData);
            Assert.AreEqual(messageId, qrCodeReversalData.MessageID);
        }

        [Test]
        public void GetQRCodeReversal()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var messageId = "MyUniqRequest";

            // Act
            var qrCode = provider.GetQRCode(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            }, false);

            Thread.Sleep(500);

            //// test for a fake payment:
            //var qrCodeReversalData = provider.GetQRCodeReversalData(AlfaOption.DemoOption, new()
            //{
            //    TerminalNumber = "90000018",
            //    QrcId = qrCode.QrcId,
            //    Amount = 10000,
            //    Currency = "RUB",
            //    MessageID = messageId,
            //    TrxId = qrCode.QrcId
            //});

            // test for a fake payment:
            var qrCodeReversal = provider.GetQRCodeReversal(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId,
                Amount = 10000,
                Currency = "RUB",
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.NotNull(qrCode);
            Assert.NotNull(qrCode.Image);
            Assert.NotNull(qrCodeReversal);
            Assert.AreEqual(messageId, qrCodeReversal.MessageID);
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
                Height = "1000",
                Width = "1000"
            }, false);

            var image = qrCodeCashLink.ToImage();

            qrCodeCashLink.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

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
            var paymentPurpose = "Оплата тестового товара по кассовой ссылке";

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRTotal = 10
            }, false);

            // Assert
            Assert.NotNull(qrCodeActivateCashLink);
            Assert.AreEqual(paymentPurpose, qrCodeActivateCashLink.PaymentPurpose);
        }

        [Test]
        public void DectivateQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var paymentPurpose = "Оплата тестового товара по кассовой ссылке";

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Amount = 100000,
                Currency = "RUB",
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRTotal = 10
            }, false);

            Thread.Sleep(500);

            var qrCodeDeactivateCashLink = provider.DeactivateQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCodeCashLink.QrcId,
            }, false);

            // Assert
            Assert.NotNull(qrCodeActivateCashLink);
            Assert.AreEqual(paymentPurpose, qrCodeActivateCashLink.PaymentPurpose);
            Assert.NotNull(qrCodeDeactivateCashLink);
        }
    }
}