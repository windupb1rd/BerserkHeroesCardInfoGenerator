using Telegram.Bot;
using Telegram.Bot.Types;

namespace Infrastructure.TelegramBot.Handlers
{
    internal class OnUpdateHandler
    {
        private readonly Telegram.Bot.TelegramBotClient _bot;

        public OnUpdateHandler(
            Telegram.Bot.TelegramBotClient bot)
        {
            _bot = bot;
        }

        // method that handle other types of updates received by the bot:
        public async Task OnUpdate(Update update)
        {
            if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
            {
                await _bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                await _bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
            }
        }
    }
}
