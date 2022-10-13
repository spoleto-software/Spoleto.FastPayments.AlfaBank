﻿using Microsoft.Extensions.DependencyInjection;
using Spoleto.Cryptography.Rsa;
using Spoleto.FastPayments.AlfaBank.Helpers;
using Spoleto.FastPayments.AlfaBank.Models;
using Spoleto.FastPayments.AlfaBank.Providers;

namespace Spoleto.FastPayments.AlfaBank.Tests
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
            Assert.That(isVerified, Is.True);
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
                PaymentPurpose = "Оплата тестовой покупки 5",
                QRCodeType = "02",
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = "http://service-test.cashmere.ru/alfaservice" }
            }, false);

            var image = ImageHelper.ConvertToImage(qrCode.ImageBytes);

            //qrCode.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCode, Is.Not.Null);
                Assert.That(qrCode.Image, Is.Not.Null);
                //Assert.That(image, Is.Not.Null);
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
                PaymentPurpose = "Оплата тестовой покупки",
                QRCodeQueryData = new QRCodeQueryData { NotificationUrl = "http://service-test.cashmere.ru/alfaservice" }
            }, false);

            Thread.Sleep(500);

            var qrCodeStatus = provider.GetQRCodeStatus(AlfaOption.DemoOption, new()
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
            var qrCodeCashLink = provider.RegQRСodeCashLink(AlfaOption.DemoOption, new()
            {
                TerminalNumber = "90000018",
                Height = "1000",
                Width = "1000"
            }, false);

            var image = ImageHelper.ConvertToImage(qrCodeCashLink.ContentBytes);

            //qrCodeCashLink.SaveImageToFile($@"C:\Alfa\qrcode_{DateTime.Now.Ticks}.png");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(qrCodeCashLink, Is.Not.Null);
                Assert.That(qrCodeCashLink.Content, Is.Not.Null);
                //Assert.That(image, Is.Not.Null);
            });
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