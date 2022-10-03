namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Базовый интерфейс для всех видов ответов хоста Альфа-Банка.
    /// </summary>
    public interface IAlfaResponse
    {
        /// <summary>
        /// Код ответа.
        /// </summary>
        string ErrorCode { get; set; }

        /// <summary>
        /// Расшифровка кода ответа.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        int TerminalNumber { get; set; }
    }
}
