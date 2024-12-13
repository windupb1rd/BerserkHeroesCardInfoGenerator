using Infrastructure.TelegramBot.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.TelegramBot
{
    public class BotClientMessenger : IBotClientMessenger
    {
        private readonly TelegramBotClient _telegramBotClient;

        public BotClientMessenger(TelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public Task SendDocument(long chatId, InputFileStream document, string caption)
        {
            return _telegramBotClient.SendDocument(chatId, document, caption);
        }

        public Task SendMessage(long chatId, string text, int msgId)
        {
            return _telegramBotClient.SendMessage(chatId, text, replyParameters: msgId);
        }

        public Task SendMessage(long chatId, string text)
        {
            return _telegramBotClient.SendMessage(chatId, text);
        }

        public Task SendPhoto(long chatId, string imageUrl, int msgId)
        {
            return _telegramBotClient.SendPhoto(chatId, imageUrl, replyParameters: msgId);
        }
    }
}
