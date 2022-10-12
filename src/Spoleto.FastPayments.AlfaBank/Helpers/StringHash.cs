using System;
using System.Security.Cryptography;
using System.Text;

namespace Spoleto.FastPayments.AlfaBank.Helpers
{
    /// <summary>
    /// Static class for strings hash generation.<br/>
    /// Default hash algorithm: <see cref="SHA256CryptoServiceProvider"/>.<br/>
    /// Default encoding: <see cref="DefaultSettings.Encoding"/>.
    /// </summary>
    public static class StringHash
    {
        private static SHA256CryptoServiceProvider DefaultHashProvider => GetSHA256CryptoServiceProvider();

        /// <summary>
        /// Gets the SHA256CryptoServiceProvider.
        /// </summary>
        public static SHA256CryptoServiceProvider GetSHA256CryptoServiceProvider() => new();

        private static string GetHash(string stringToHash, Encoding enc, HashAlgorithm hashProvider)
        {
            var bytes = enc.GetBytes(stringToHash);
            var hash = Convert.ToBase64String(hashProvider.ComputeHash(bytes));

            return  hash;
        }

        private static byte[] GetHashBytes(string stringToHash, Encoding enc, HashAlgorithm hashProvider)
        {
            var bytes = enc.GetBytes(stringToHash);
            var hash = hashProvider.ComputeHash(bytes);

            return hash;
        }

        /// <summary>
        /// Get hash for string as base64 string.
        /// </summary>
        public static string GetHash(string stringToHash) => GetHash(stringToHash, DefaultSettings.Encoding, DefaultHashProvider);

        /// <summary>
        /// Get hash for string as bytes.
        /// </summary>
        public static byte[] GetHashBytes(string stringToHash) => GetHashBytes(stringToHash, DefaultSettings.Encoding, DefaultHashProvider);
    }
}