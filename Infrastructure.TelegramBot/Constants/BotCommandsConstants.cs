namespace Infrastructure.TelegramBot.Constants
{
    /// <summary>
    /// Константы текстовых команд бота.
    /// </summary>
    public class BotCommandsConstants
    {
        /// <summary>
        /// Команда поиска карты.
        /// </summary>
        public const string GET_CARD_COMMAND = "!к";

        /// <summary>
        /// Команда поиска карты.
        /// </summary>
        public const string GET_TERM_COMMAND = "!т";

        /// <summary>
        /// Команда выгрузки базы карт.
        /// </summary>
        public const string GET_XLSX_DUMP_COMMAND = "!в";

        /// <summary>
        /// Команда поиска информаци об аукционах.
        /// </summary>
        public const string GET_AUC_INFO_COMMAND = "!аук";
    }
}
