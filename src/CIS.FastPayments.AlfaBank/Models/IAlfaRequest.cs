namespace CIS.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Базовый интерфейс для всех видов запросов для хоста Альфа-Банка.
    /// </summary>
    public interface IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        string Command { get; set; }

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        string TerminalNumber { get; set; }
    }
}
