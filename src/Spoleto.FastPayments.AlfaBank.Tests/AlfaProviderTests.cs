using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Spoleto.Cryptography.Rsa;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;
using Spoleto.FastPayments.AlfaBank.Providers;

namespace Spoleto.FastPayments.AlfaBank.Tests
{
    public class AlfaProviderTests
    {
        private ServiceProvider _serviceProvider;
        private AlfaOptionExtended _settings;

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
        public void CryptoVerify()
        {
            // Arrange
            var model = new QRCodeRequestModel
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = _settings.PaymentPurpose
            };
            var json = JsonHelper.ToJson(model);
            var certificate = _settings.Certificate;

            // Act
            var signedData = RSACryptoPemHelper.Sign(certificate.PrivateKey, json);
            var isVerified = RSACryptoPemHelper.Verify(certificate.PublicBody, json, signedData);

            // Assert
            Assert.That(isVerified, Is.True);
        }

        [Test]
        public void GetQRCode()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();

            // Act
            var qrCode = provider.GetQRCode(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = _settings.PaymentPurpose,
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = _settings.CallBackUrl }
            }, false);

            //var qrCode2 = provider.GetQRCode(_settings, new()
            //{
            //    TerminalNumber = _settings.AlfaTerminalNumber,
            //    Amount = _settings.Amount00,
            //    Currency = _settings.Currency,
            //    PaymentPurpose = _settings.PaymentPurpose,
            //    QRCodeType = "02",
            //    QRCodeQueryData = new QRCodeQueryData { NotificationUrl = _settings.CallBackUrl }
            //}, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.ErrorCode, Is.EqualTo(Constants.SuccessCode));
                Assert.That(qrCode.Image, Is.Not.Null);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {            
                    //qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");
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
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = _settings.PaymentPurpose,
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = _settings.CallBackUrl }
            }, false);

            Thread.Sleep(500);

            var qrCodeStatus = provider.GetQRCodeStatus(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                QrcId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.ErrorCode, Is.EqualTo(Constants.SuccessCode));
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
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = _settings.PaymentPurpose
            }, false);

            Thread.Sleep(500);

            // test for a fake payment:
            var qrCodeReversalData = provider.GetQRCodeReversalData(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                QrcId = qrCode.QrcId,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.ErrorCode, Is.EqualTo(Constants.SuccessCode));
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
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = _settings.PaymentPurpose
            }, false);

            Thread.Sleep(500);

            //// test for a fake payment:
            //var qrCodeReversalData = provider.GetQRCodeReversalData(_settings, new()
            //{
            //    TerminalNumber = _settings.AlfaTerminalNumber,
            //    QrcId = qrCode.QrcId,
            //    Amount = _settings.Amount,
            //    Currency = _settings.Currency,
            //    MessageID = messageId,
            //    TrxId = qrCode.QrcId
            //});

            // test for a fake payment:
            var qrCodeReversal = provider.GetQRCodeReversal(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                QrcId = qrCode.QrcId,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                MessageID = messageId,
                TrxId = qrCode.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.ErrorCode, Is.EqualTo(Constants.SuccessCode));
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
                TerminalNumber = _settings.AlfaTerminalNumber,
                Height = "1000",
                Width = "1000"
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeCashLink, Is.Not.Null);
                Assert.That(qrCodeCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
                Assert.That(qrCodeCashLink.Content, Is.Not.Null);
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //qrCodeCashLink.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");
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
            var paymentPurpose = _settings.CashLinkPaymentPurpose;

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = _settings.CallBackUrl },
                QRTotal = 10
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeActivateCashLink, Is.Not.Null);
                Assert.That(qrCodeCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
               
                Assert.That(qrCodeActivateCashLink.PaymentPurpose, Is.EqualTo(paymentPurpose));
                Assert.That(qrCodeActivateCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
            });
        }

        [Test]
        public void DectivateQRСodeCashLink()
        {
            // Arrange
            var provider = _serviceProvider.GetService<IAlfaProvider>();
            var paymentPurpose = _settings.CashLinkPaymentPurpose;

            // Act
            var qrCodeCashLink = provider.RegQRСodeCashLink(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Height = "1000",
                Width = "1000"
            }, false);

            var qrCodeActivateCashLink = provider.ActivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                Amount = _settings.Amount,
                Currency = _settings.Currency,
                PaymentPurpose = paymentPurpose,
                QrcId = qrCodeCashLink.QrcId,
                QRTotal = 10
            }, false);

            Thread.Sleep(500);

            var qrCodeDeactivateCashLink = provider.DeactivateQRСodeCashLink(_settings, new()
            {
                TerminalNumber = _settings.AlfaTerminalNumber,
                QrcId = qrCodeCashLink.QrcId
            }, false);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeActivateCashLink, Is.Not.Null);
                Assert.That(qrCodeCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
                
                Assert.That(qrCodeActivateCashLink.PaymentPurpose, Is.EqualTo(paymentPurpose));
                Assert.That(qrCodeActivateCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
                
                Assert.That(qrCodeDeactivateCashLink, Is.Not.Null);
                Assert.That(qrCodeDeactivateCashLink.ErrorCode, Is.EqualTo(Constants.SuccessCode));
            });
        }
    }
}