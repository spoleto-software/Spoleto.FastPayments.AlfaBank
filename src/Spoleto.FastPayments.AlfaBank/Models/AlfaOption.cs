﻿namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Настройки ЦРПТ провайдера.
    /// </summary>
    public class AlfaOption
    {
        /// <summary>
        /// Адрес сервиса Альфа-Банка.
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Сертификат для подписи запроса.
        /// </summary>
        public Certificate Certificate { get; set; }
    }
}
