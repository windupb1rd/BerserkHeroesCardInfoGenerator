using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Microsoft.Extensions.Options;
using Infrastructure.TelegramBot.Options;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Infrastructure.TelegramBot.Abstractions;
using Infrastructure.Common.Extensions;
using Core.Application.UseCases;
using CONST = Infrastructure.TelegramBot.Constants.BotCommandsConstants;
using Infrastructure.TelegramBot.Handlers;

namespace Infrastructure.TelegramBot
{
    /// <summary>
    /// Клиент телеграм бота.
    /// </summary>
    public class TelegramBotClient
    {
        private readonly TelegramBotOptions _options;
        private readonly IImageUrlComposer _imageUrlComposer;
        private readonly SaveCardsUseCase _useCase;

        public TelegramBotClient(
            IOptions<TelegramBotOptions> options,
            IImageUrlComposer imageUrlComposer,
            SaveCardsUseCase useCase)
        {
            _options = options.Value;
            _imageUrlComposer = imageUrlComposer;
            _useCase = useCase;
        }

        public async Task Start()
        {
            using var cts = new CancellationTokenSource();
            var bot = new Telegram.Bot.TelegramBotClient(_options.Token, cancellationToken: cts.Token);
            var me = await bot.GetMe();

            // придумать как зарезолвить хендлеры из контейнера. Пока проблема в зависимости от bot.
            var onMessageHandler = new OnMessageHandler(bot, _imageUrlComposer, _useCase);
            var onUpdateHandler = new OnUpdateHandler(bot);
            var onErrorHandler = new OnErrorHandler();

            bot.OnMessage += onMessageHandler.OnMessage;
            bot.OnUpdate += onUpdateHandler.OnUpdate;
            bot.OnError += onErrorHandler.OnError;

            Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
            Console.ReadLine();
            cts.Cancel(); // stop the bot
        }
    }
}
