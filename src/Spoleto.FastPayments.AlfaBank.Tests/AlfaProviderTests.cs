using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;
using Spoleto.FastPayments.AlfaBank.Providers;

namespace Spoleto.FastPayments.AlfaBank.Tests
{
    public class AlfaProviderTests
    {
        private ServiceProvider _serviceProvider;
        private AlfaOption _settings;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddTransient<IAlfaProvider, AlfaProvider>();

            _serviceProvider = services.BuildServiceProvider();

            _settings = ConfigurationHelper.GetAlfaSettings();
        }

        [Test]
        public void GetQRCode()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5"
            }, false);

            var qrCode2 = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки 5",
                QRCodeType = "02",
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = "http://service-test.cashmere.ru/alfaservice" }
            }, false);


            //qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var image = ImageHelper.ConvertToImage(qrCode.ImageBytes);
                    Assert.That(image, Is.Not.Null);
                }
            });
        }

        [Test]
        public void GetQRCodeStatus()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки",
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = "http://service-test.cashmere.ru/alfaservice" }
            }, false);

            Thread.Sleep(500);

            var qrCodeStatus = provider.GetQRCodeStatus(_settings, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                Assert.That(qrCodeStatus, Is.Not.Null);
            });
        }

        [Test]
        public void GetQRCodeReversalData()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var messageId = "MyUniqRequest";

            // Act
            var qrCode = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            }, false);

            Thread.Sleep(500);

            // test for a fake payment:
            var qrCodeReversalData = provider.GetQRCodeReversalData(_settings, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId,
                Amount = 10000,
                Currency = "RUB",
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                Assert.That(qrCodeReversalData, Is.Not.Null);
                Assert.That(qrCodeReversalData.MessageID, Is.EqualTo(messageId));
            });
        }

        [Test]
        public void GetQRCodeReversal()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var messageId = "MyUniqRequest";

            // Act
            var qrCode = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 10000,
                Currency = "RUB",
                PaymentPurpose = "Оплата тестовой покупки"
            }, false);

            Thread.Sleep(500);

            //// test for a fake payment:
            //var qrCodeReversalData = provider.GetQRCodeReversalData(_settings, new()
            //{
            //    TerminalNumber = "90000018",
            //    QrcId = qrCode.QrcId,
            //    Amount = 10000,
            //    Currency = "RUB",
            //    MessageID = messageId,
            //    TrxId = qrCode.QrcId
            //});

            // test for a fake payment:
            var qrCodeReversal = provider.GetQRCodeReversal(_settings, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCode.QrcId,
                Amount = 10000,
                Currency = "RUB",
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                Assert.That(qrCodeReversal, Is.Not.Null);
                Assert.That(qrCodeReversal.MessageID, Is.EqualTo(messageId));
            });
        }

        [Test]
        public void RegQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            //qrCodeCashLink.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeCashLink, Is.Not.Null);
                Assert.That(qrCodeCashLink.Content, Is.Not.Null);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var image = ImageHelper.ConvertToImage(qrCodeCashLink.ContentBytes);
                    Assert.That(image, Is.Not.Null);
                }
            });
        }

        [Test]
        public void ActivateQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var paymentPurpose = "Оплата тестового товара по кассовой ссылке";

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 100,
                Currency = "RUB",
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = "http://service-test.cashmere.ru/alfaservice" },
                QRTotal = 10
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeActivateCashLink, Is.Not.Null);
                Assert.That(qrCodeActivateCashLink.PaymentPurpose, Is.EqualTo(paymentPurpose));
            });
        }

        [Test]
        public void DectivateQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var paymentPurpose = "Оплата тестового товара по кассовой ссылке";

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                Amount = 100000,
                Currency = "RUB",
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRTotal = 10
            }, false);

            Thread.Sleep(500);

            var qrCodeDeactivateCashLink = provider.DeactivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = "90000018",
                QrcId = qrCodeCashLink.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeActivateCashLink, Is.Not.Null);
                Assert.That(qrCodeActivateCashLink.PaymentPurpose, Is.EqualTo(paymentPurpose));
                Assert.That(qrCodeDeactivateCashLink, Is.Not.Null);
            });
        }
    }
}