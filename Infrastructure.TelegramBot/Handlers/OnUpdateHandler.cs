using Infrastructure.TelegramBot.Abstractions;
using Telegram.Bot.Types;

namespace Infrastructure.TelegramBot.Handlers
{
    public class OnUpdateHandler
    {
        private readonly IBotClientMessenger _bot;

        public OnUpdateHandler(
            IBotClientMessenger bot)
        {
            _bot = bot;
        }

        // method that handle other types of updates received by the bot:
        public async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                //await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendMessage(query.Message!.Chat.Id, $"User {query.From} clicked on {query.Data}");
            }
        }
    }
}
