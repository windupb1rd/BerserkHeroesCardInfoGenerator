using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Core.Application.UseCases;
using Infrastructure.TelegramBot.Abstractions;
using COMMANDS = Infrastructure.TelegramBot.Constants.BotCommandsConstants;
using SERVICE_MESSAGES = Infrastructure.TelegramBot.Constants.ServiceMessages;
using Infrastructure.Common.Extensions;
using Infrastructure.Vk.Abstractions;

namespace Infrastructure.TelegramBot.Handlers
{
    public class OnMessageHandler
    {
        private readonly IBotClientMessenger _bot;
        private readonly IImageUrlComposer _imageUrlComposer;
        private readonly ITermRepository _termRepository;
        private readonly IAuctionPostInfoRepository _auctionPostInfoRepository;
        private readonly SaveCardsUseCase _saveCarsUseCase;

        public OnMessageHandler(
            IBotClientMessenger bot,
            IImageUrlComposer imageUrlComposer,
            SaveCardsUseCase saveCarsUseCase,
            ITermRepository termRepository,
            IAuctionPostInfoRepository auctionPostInfoRepository)
        {
            _bot = bot;
            _imageUrlComposer = imageUrlComposer;
            _saveCarsUseCase = saveCarsUseCase;
            _termRepository = termRepository;
            _auctionPostInfoRepository = auctionPostInfoRepository;
        }

        // method that handle messages received by the bot:
        public async Task OnMessage(Message msg, UpdateType type)
        {
            Console.WriteLine($"Message: {msg.Text}");

            var text = msg.Text.ToLower();
            if (text.StartsWith(COMMANDS.GET_CARD_COMMAND))
            {
                var cardName = text
                    .Replace(COMMANDS.GET_CARD_COMMAND, "")
                    .ToSearchable();

                var imageUrl = _imageUrlComposer.ComposeByCardName(cardName);
                if (imageUrl != null)
                {
                    await _bot.SendPhoto(msg.Chat.Id, imageUrl, msg.Id);
                }
                else
                {
                    await _bot.SendMessage(msg.Chat.Id, "Не нашлось такой карты", msg.Id);
                }
            }

            if (text.StartsWith(COMMANDS.GET_TERM_COMMAND))
            {
                var termName = text
                    .Replace(COMMANDS.GET_TERM_COMMAND, "")
                    .ToSearchable();

                var term = _termRepository.GetTermByName(termName);
                if (term != null)
                {
                    await _bot.SendMessage(msg.Chat.Id, term, msg.Id);
                }
                else
                {
                    await _bot.SendMessage(msg.Chat.Id, "Не нашлось такого термина", msg.Id);
                }
            }

            if (text.StartsWith(COMMANDS.GET_AUC_INFO_COMMAND))
            {
                var cardName = text
                    .Replace(COMMANDS.GET_AUC_INFO_COMMAND, "")
                    .ToSearchable();

                var info = _auctionPostInfoRepository.GetAucInfosByCardName(cardName);
                if (info != null)
                {
                    await _bot.SendMessage(msg.Chat.Id, info, msg.Id);
                }
                else
                {
                    await _bot.SendMessage(msg.Chat.Id, "Ничего не нашлось", msg.Id);
                }
            }

            if (text.StartsWith(COMMANDS.GET_XLSX_DUMP_COMMAND))
            {
                await _saveCarsUseCase.ExecuteAsync();

                await using Stream stream = System.IO.File.OpenRead("MyBerserkHeroesCollection.xlsx");
                await _bot.SendDocument(msg.Chat.Id, document: InputFile.FromStream(
                    stream, $"BHCollection-{DateTime.Now.Date.ToShortDateString()}.xlsx"),
                    caption: $"Выгрузка базы карт сайта berserkdeck.ru за {DateTime.Now.Date.ToShortDateString()}");
            }

            //TODO
            if (new List<string> { "/help", "/h", "!п" }.Contains(msg.Text))
            {
                await _bot.SendMessage(msg.Chat.Id, SERVICE_MESSAGES.BOT_INFO, msg.Id);
            }

            if (msg.Text == "/start")
            {
                await _bot.SendMessage(msg.Chat.Id, $"Привет, герой!\r\n{SERVICE_MESSAGES.BOT_INFO}");
            }

            // служебная ручка, удалить
            if (msg.Text == "//update")
            {
                await _saveCarsUseCase.ExecuteAsync();
                await _bot.SendMessage(msg.Chat.Id, "Готово", msg.Id);
            }

            // пример добавления кнопок
            //await _bot.SendMessage(msg.Chat, $"Привет, герой!\r\n{SERVICE_MESSAGES.BOT_INFO}",
            //        replyMarkup: new InlineKeyboardMarkup().AddButtons("berserkdeck.ru", "Right"));

            if (msg.Text is null) return;   // we only handle Text messages here
                                            //Console.WriteLine($"Received {type} '{msg.Text}' in {msg.Chat}");
                                            //// let's echo back received text in the chat
                                            //await bot.SendMessage(msg.Chat, $"{msg.From} said: {msg.Text}");
        }
    }
}
