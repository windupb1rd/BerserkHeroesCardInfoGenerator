using Telegram.Bot;
using Microsoft.Extensions.Options;
using Infrastructure.TelegramBot.Options;
using Infrastructure.TelegramBot.Abstractions;
using Core.Application.UseCases;
using Infrastructure.TelegramBot.Handlers;
using Infrastructure.Vk.Abstractions;
using Core.Application.Services;

namespace Infrastructure.TelegramBot
{
    /// <summary>
    /// Клиент телеграм бота.
    /// Документация: https://telegrambots.github.io/book/index.html.
    /// </summary>
    public class TelegramBotClient
    {
        private readonly TelegramBotOptions _options;
        private readonly IImageUrlComposer _imageUrlComposer;
        private readonly ITermRepository _termRepository;
        private readonly IAuctionPostInfoRepository _auctionPostInfoRepository;
        private readonly SaveCardsUseCase _useCase;

        public TelegramBotClient(
            IOptions<TelegramBotOptions> options,
            IImageUrlComposer imageUrlComposer,
            SaveCardsUseCase useCase,
            ITermRepository termRepository,
            IAuctionPostInfoRepository auctionPostInfoRepository)
        {
            _options = options.Value;
            _imageUrlComposer = imageUrlComposer;
            _useCase = useCase;
            _termRepository = termRepository;
            _auctionPostInfoRepository = auctionPostInfoRepository;
        }

        public async Task Start()
        {
            var cardsUpdater = new CardsInfoUpdater(_useCase);

            using var cts = new CancellationTokenSource();
            var bot = new Telegram.Bot.TelegramBotClient(_options.Token, cancellationToken: cts.Token);
            var me = await bot.GetMe();

            // придумать как зарезолвить хендлеры из контейнера. Пока проблема в зависимости от bot.
            var onMessageHandler = new OnMessageHandler(bot, _imageUrlComposer, _useCase, _termRepository, _auctionPostInfoRepository);
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
