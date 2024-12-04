namespace Infrastructure.TelegramBot.Options
{
    /// <summary>
    /// Настройки телеграм бота.
    /// </summary>
    public class TelegramBotOptions
    {
        /// <summary>
        /// Токен телеграм бота (хранится в secrets.json).
        /// </summary>
        public string Token { get; init; }
    }
}
