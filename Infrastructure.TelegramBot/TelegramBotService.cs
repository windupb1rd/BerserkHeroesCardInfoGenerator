using Telegram.Bot;
using Infrastructure.TelegramBot.Handlers;

namespace Infrastructure.TelegramBot
{
    /// <summary>
    /// Клиент телеграм бота.
    /// Документация: https://telegrambots.github.io/book/index.html.
    /// </summary>
    public class TelegramBotService
    {
        private readonly TelegramBotClient _bot;
        private readonly OnMessageHandler _onMessageHandler;
        private readonly OnUpdateHandler _onUpdateHandler;
        private readonly OnErrorHandler _onErrorHandler;

        public TelegramBotService(
            OnMessageHandler onMessageHandler,
            OnUpdateHandler onUpdateHandler,
            OnErrorHandler onErrorHandler,
            TelegramBotClient bot)
        {
            _onMessageHandler = onMessageHandler;
            _onUpdateHandler = onUpdateHandler;
            _onErrorHandler = onErrorHandler;
            _bot = bot;
        }

        public async Task Start()
        {
            var me = await _bot.GetMe();
            Console.WriteLine($"{me.Username} is started.");

            _bot.OnMessage += _onMessageHandler.OnMessage;
            _bot.OnUpdate += _onUpdateHandler.OnUpdate;
            _bot.OnError += _onErrorHandler.OnError;
        }
    }
}
