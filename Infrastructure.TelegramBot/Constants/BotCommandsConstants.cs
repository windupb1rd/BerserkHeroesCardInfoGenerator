namespace Infrastructure.TelegramBot.Constants
{
    /// <summary>
    /// Константы текстовых команд бота.
    /// </summary>
    public class BotCommandsConstants
    {
        /// <summary>
        /// Слово, с которого должно начинаться сообщение, чтобы на него отреагировал бот.
        /// </summary>
        public const string BOT_ADDRESSING_COMMAND = "унгар";

        /// <summary>
        /// Команда поиска карты.
        /// </summary>
        public const string GET_CARD_COMMAND = "карта";

        /// <summary>
        /// Команда поиска карты.
        /// </summary>
        public const string GET_TERM_COMMAND = "термин";

        /// <summary>
        /// Команда выгрузки базы карт.
        /// </summary>
        public const string GET_XLSX_DUMP_COMMAND = "выгрузка";
    }
}
