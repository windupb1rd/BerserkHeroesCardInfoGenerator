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
            bot.OnMessage += OnMessage;
            bot.OnUpdate += OnUpdate;
            bot.OnError += OnError;

            Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
            Console.ReadLine();
            cts.Cancel(); // stop the bot

            // method that handle messages received by the bot:
            async Task OnMessage(Message msg, UpdateType type)
            {
                Console.WriteLine($"Message: {msg.Text}");

                var text = msg.Text.ToLower();
                if (text.StartsWith("унгар"))
                {
                    if (text.Contains("карта"))
                    {
                        var cardName = text
                            .Replace("унгар", "")
                            .Replace("карта", "")
                            .ToSearchable();

                        var imageUrl = _imageUrlComposer.ComposeByCardName(cardName);
                        if (imageUrl != null)
                        {
                            await bot.SendPhoto(msg.Chat, imageUrl);
                        }
                        else
                        {
                            await bot.SendMessage(msg.Chat, "Не нашлось такой карты");
                        }
                    }

                    if (text.Contains("термин"))
                    {
                        var term = text
                            .Replace("унгар", "")
                            .Replace("термин", "")
                            .ToSearchable();

                        if (term == "броня")
                        {
                            await bot.SendMessage(msg.Chat,
                                "Броня Х: карта с бронёй Х не получает первые" +
                                " Х ран от немагических атак в течение каждого хода (и своего, и противника).");
                        }
                        else
                        {
                            await bot.SendMessage(msg.Chat, "Не нашлось такого термина");
                        }
                    }

                    if (text.Contains("выгрузка"))
                    {
                        await _useCase.ExecuteAsync();

                        await using Stream stream = System.IO.File.OpenRead("MyBerserkHeroesCollection.xlsx");
                        var message = await bot.SendDocument(msg.Chat, document: InputFile.FromStream(
                            stream, $"BHCollection-{DateTime.Now.Date.ToShortDateString()}.xlsx"),
                            caption: $"Выгрузка базы карт сайта berserkdeck.ru за {DateTime.Now.Date.ToShortDateString()}");
                    }
                }

                if (msg.Text == "/start")
                {
                    await bot.SendMessage(msg.Chat, "Welcome! Pick one direction",
                        replyMarkup: new InlineKeyboardMarkup().AddButtons("Left", "Right"));
                }

                //if (msg.Text == "болотница")
                //{
                //    await bot.SendPhoto(msg.Chat, "https://www.berserkdeck.ru/dev/api/images/cards-heroes/16/90/regular");
                //}
                

                if (msg.Text is null) return;   // we only handle Text messages here
                //Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
                //// let's echo back received text in the chat
                //await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
            }

            // method that handle other types of updates received by the bot:
            async Task OnUpdate(Update update)
            {
                if (update is { CallbackQuery: { } query }) // non-null CallbackQuery
                {
                    await bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
                    await bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
                }
            }

            async Task OnError(Exception exception, HandleErrorSource source)
            {
                Console.WriteLine(exception); // just dump the exception to the console
            }
        }
    }
}
