namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Статусы оплаты QR-кода в НСПК
    /// </summary>
    public enum QRCodeStatus
    {
        /// <summary>
        /// NOT_STARTED операции по QR коду не существует
        /// </summary>
        NTST,

        /// <summary>
        /// RECEIVED операция в обработке
        /// </summary>
        RCVD,

        /// <summary>
        /// IN_PROGRESS операция в обработке
        /// </summary>
        ACTC,

        /// <summary>
        /// ACCEPTED операция завершена успешно
        /// </summary>
        ACWP,

        /// <summary>
        /// REJECTED операция отклонена
        /// </summary>
        RJCT
    }
}
