using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CIS.FastPayments.AlfaBank.Models;
//using Org.BouncyCastle.Asn1.Ocsp;
//using Org.BouncyCastle.Crypto;
//using Org.BouncyCastle.Crypto.Parameters;
//using Org.BouncyCastle.OpenSsl;
//using Org.BouncyCastle.Security;

namespace CIS.FastPayments.AlfaBank.Helpers
{
    //TODO: После обновления на NET 6 или выше
    // попробовать использовать нативный NET метод System.Security.Cryptography.RSA.ImportFromEncryptedPem:
    // https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.rsa.importfrompem
    // Если он заработает, то удалить стороннюю библиотеку BouncyCastle (она довольно тяжелая >3Мб и избыточная для этого кейса).

    /// <summary>
    /// Хелпер для работы с криптографией (аналог OpenSsl).
    /// </summary>
    /// <remarks>
    /// PEM (Privacy-Enhanced Mail) (RFC 7468), DER (Distinguished Encoding Rules) (X.690).<br/>
    ///  If the file's content is text data and contains -----BEGIN ?????----,
    ///  the file format is PEM.<br/>
    ///  On the other hand, if the file contains binary data, it is highly likely that the file format is DER.
    /// </remarks>
    public static class CryptoHelper
    {
        //private const string _algorithm = "SHA256withRSA";

        ///// <summary>
        ///// Расчет hash SHA256 и закрытия его приватным ключом RSA сертификата (c использованием библиотеки BouncyCastle).
        ///// </summary>
        //public static string Sign(Certificate certificate, string stringToSign)
        //{
        //    using var sr = new StringReader(certificate.PrivateKey);
        //    var pr = new PemReader(sr);
        //    var pemKeyParams = (RsaPrivateCrtKeyParameters)pr.ReadObject();
        //    var cypherParams = new RsaKeyParameters(true, pemKeyParams.Modulus, pemKeyParams.Exponent);

        //    // Init alg 
        //    var signer = SignerUtilities.GetSigner(_algorithm);
        //    // Populate key
        //    signer.Init(true, cypherParams);

        //    var bytes = DefaultSettings.Encoding.GetBytes(stringToSign);
        //    signer.BlockUpdate(bytes, 0, bytes.Length);
        //    var signature = signer.GenerateSignature();
        //    var result = Convert.ToBase64String(signature);

        //    return result;
        //}

        ///// <summary>
        ///// Проверка с помощью публичного ключа зашифрованного значения (c использованием библиотеки BouncyCastle).
        ///// </summary>
        //public static bool Verify(Certificate certificate, string stringToVerify, string expectedSignature)
        //{
        //    using var sr = new StringReader(certificate.PublicBody);
        //    var pr = new PemReader(sr);
        //    var x509Certificate = (Org.BouncyCastle.X509.X509Certificate)pr.ReadObject();
        //    var pemKeyParams = x509Certificate.GetPublicKey();

        //    // Init alg 
        //    var signer = SignerUtilities.GetSigner(_algorithm);
        //    // Populate key
        //    signer.Init(false, pemKeyParams);

        //    // Get the signature into bytes
        //    var expectedSig = Convert.FromBase64String(expectedSignature);

        //    // Get the bytes to be signed from the string
        //    var msgBytes = DefaultSettings.Encoding.GetBytes(stringToVerify);

        //    // Calculate the signature and see if it matches
        //    signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
        //    return signer.VerifySignature(expectedSig);
        //}

        /// <summary>
        /// Расчет hash SHA256 и закрытия его приватным ключом RSA сертификата (на основе .NET Core).
        /// </summary>
        public static string SignByCore(Certificate certificate, string stringToSign)
        {
            using var csp = CreateRsaProviderFromPrivateKey(certificate.PrivateKey);

            var originalData = DefaultSettings.Encoding.GetBytes(stringToSign);
            var signed = csp.SignData(originalData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            
            return Convert.ToBase64String(signed);
        }

        /// <summary>
        /// Проверка с помощью публичного ключа зашифрованного значения  (на основе .NET Core).
        /// </summary>
        public static bool VerifyByCore(Certificate certificate, string stringToVerify, string expectedSignature)
        {
            using var c = new X509Certificate2(DefaultSettings.Encoding.GetBytes(certificate.PublicBody));

            // Get the signature into bytes
            var expectedSig = Convert.FromBase64String(expectedSignature);

            // Get the bytes to be signed from the string
            var msgBytes = DefaultSettings.Encoding.GetBytes(stringToVerify);

            using var rsa = c.GetRSAPublicKey();
            if (rsa != null)
                return rsa.VerifyData(msgBytes, expectedSig, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            using var ecdsa = c.GetECDsaPublicKey();
            if (ecdsa != null)
                return ecdsa.VerifyData(msgBytes, expectedSig, HashAlgorithmName.SHA256);

            using var dsa = c.GetDSAPublicKey();
            if (dsa != null)
                return dsa.VerifyData(msgBytes, expectedSig, HashAlgorithmName.SHA256);

            return false;
        }

        //TODO: После обновления на NET 6 или выше
        // попробовать использовать нативный NET метод System.Security.Cryptography.RSA.ImportFromEncryptedPem
        // вместо ImportPkcs8PrivateKey, ImportRSAPrivateKey, ImportPkcs8PrivateKey
        private static RSA CreateRsaProviderFromPrivateKey(string privateKey)
        {
            var lines = privateKey.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();

            var base64Key = string.Join(String.Empty,
            (
                from s in lines
                where !string.IsNullOrEmpty(s) && s[0] != '-'
                select s
             ).ToList().ToArray()).Replace(Environment.NewLine, String.Empty);

            var privateKeyBytes = Convert.FromBase64String(base64Key);

            var rsa = RSA.Create();// new RSACryptoServiceProvider(); // RSA.Create() не работает для Альфы

            // https://stackoverflow.com/a/48960291
            try
            {
                rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _); // -----BEGIN PRIVATE KEY-----
            }
            catch
            {
                try
                {
                    rsa.ImportRSAPrivateKey(privateKeyBytes, out _);  // -----BEGIN RSA PRIVATE KEY-----  
                }
                catch
                {
                    rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _); // -----BEGIN ENCRYPTED PRIVATE KEY-----
                }
            }

            return rsa;
        }
    }
}
