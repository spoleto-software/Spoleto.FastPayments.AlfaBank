using Spoleto.FastPayments.AlfaBank.Models;

namespace Spoleto.FastPayments.AlfaBank.Tests
{
    internal class AlfaOptionExtended : AlfaOption
    {
        /// <summary>
        /// Сертификат для подписи запроса.
        /// </summary>
        public Certificate Certificate { get; set; }

        public string AlfaTerminalNumber { get; set; }

        public string CallBackUrl { get; set; }

        public string Currency { get; set; }

        public decimal Amount { get; set; }

        public string PaymentPurpose { get; set; }

        public string CashLinkPaymentPurpose { get; set; }
    }
}
