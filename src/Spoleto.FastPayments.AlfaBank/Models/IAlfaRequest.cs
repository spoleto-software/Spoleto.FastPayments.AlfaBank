namespace Spoleto.FastPayments.AlfaBank.Models
{
    /// <summary>
    /// Базовый интерфейс для всех видов запросов для хоста Альфа-Банка.
    /// </summary>
    public interface IAlfaRequest
    {
        /// <summary>
        /// Тип запроса.
        /// </summary>
        string Command { get; }

        /// <summary>
        /// Уникальный идентификатор терминала.
        /// </summary>
        string TerminalNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string ToString() => $"{TerminalNumber}: {Command}";
    }
}
