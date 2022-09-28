using System;

namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// RSA сертификат.
    /// </summary>
    public class Certificate
    {
        /// <summary>
        /// Наименование сертификата.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата начала действия.
        /// </summary>
        public DateTime DateIssue { get; set; }

        /// <summary>
        /// Дата окончания действия.
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Пароль к сертификату.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Приватный ключ сертификата.
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Тело сертификата.
        /// </summary>
        public string PublicBody { get; set; }
    }
}
