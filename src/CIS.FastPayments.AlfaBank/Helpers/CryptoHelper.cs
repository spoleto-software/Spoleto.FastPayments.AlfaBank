using System;
using System.IO;
using CIS.FastPayments.AlfaBank.Models;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace CIS.FastPayments.AlfaBank.Helpers
{
    /// <summary>
    /// Хелпер для работы с криптографией (аналог OpenSsl).
    /// </summary>
    public static class CryptoHelper
    {
        private const string _algorithm = "SHA256withRSA";

        /// <summary>
        /// Расчет hash SHA256 и закрытия его приватным ключом RSA сертификата (c использованием библиотеки BouncyCastle).
        /// </summary>
        public static string Sign(Certificate certificate, string stringToSign)
        {
            using var sr = new StringReader(certificate.PrivateKey);
            var pr = new PemReader(sr);
            var pemKeyParams = (RsaPrivateCrtKeyParameters)pr.ReadObject();
            var cypherParams = new RsaKeyParameters(true, pemKeyParams.Modulus, pemKeyParams.Exponent);

            // Init alg 
            var signer = SignerUtilities.GetSigner(_algorithm);
            // Populate key
            signer.Init(true, cypherParams);

            var bytes = DefaultSettings.Encoding.GetBytes(stringToSign);
            signer.BlockUpdate(bytes, 0, bytes.Length);
            var signature = signer.GenerateSignature();
            var result = Convert.ToBase64String(signature);

            return result;
        }

        /// <summary>
        /// Проверка с помощью публичного ключа зашифрованного значения.
        /// </summary>
        public static bool Verify(Certificate certificate, string stringToVerify, string expectedSignature)
        {
            using var sr = new StringReader(certificate.PublicBody);
            var pr = new PemReader(sr);
            var x509Certificate = (Org.BouncyCastle.X509.X509Certificate)pr.ReadObject();
            var pemKeyParams = x509Certificate.GetPublicKey();

            // Init alg 
            var signer = SignerUtilities.GetSigner(_algorithm);
            // Populate key
            signer.Init(false, pemKeyParams);

            // Get the signature into bytes
            var expectedSig = Convert.FromBase64String(expectedSignature);

            // Get the bytes to be signed from the string
            var msgBytes = DefaultSettings.Encoding.GetBytes(stringToVerify);

            // Calculate the signature and see if it matches
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            return signer.VerifySignature(expectedSig);
        }
    }
}
