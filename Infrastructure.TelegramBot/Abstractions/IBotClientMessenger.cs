using Telegram.Bot.Types;

namespace Infrastructure.TelegramBot.Abstractions
{
    public interface IBotClientMessenger
    {
        Task SendMessage(long chatId, string text, int msgId);

        Task SendMessage(long chatId, string text);

        Task SendPhoto(long chatId, string imageUrl, int msgId);

        Task SendDocument(long chatId, InputFileStream document, string caption);
    }
}