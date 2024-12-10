namespace Infrastructure.TelegramBot.Constants
{
    internal class ServiceMessages
    {
        public const string BOT_INFO = $"""
            Команды бота:
            
            {BotCommandsConstants.GET_CARD_COMMAND} - найти изображение карты по названию;
            {BotCommandsConstants.GET_TERM_COMMAND} - найти игровой термин по названию;
            {BotCommandsConstants.GET_XLSX_DUMP_COMMAND} - выгрузить полную базу карт на текущий момент в .xlsx.
            {BotCommandsConstants.GET_AUC_INFO_COMMAND} - получить информацию о сыгравших и выкупленных лотах по названию карты.
            """;
    }
}
