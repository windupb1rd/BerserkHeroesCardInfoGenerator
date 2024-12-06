using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Core.Application.UseCases;
using Infrastructure.TelegramBot.Abstractions;
using CONST = Infrastructure.TelegramBot.Constants.BotCommandsConstants;
using Infrastructure.Common.Extensions;

namespace Infrastructure.TelegramBot.Handlers
{
    internal class OnMessageHandler
    {
        private readonly Telegram.Bot.TelegramBotClient _bot;
        private readonly IImageUrlComposer _imageUrlComposer;
        private readonly SaveCardsUseCase _saveCarsUseCase;

        public OnMessageHandler(
            Telegram.Bot.TelegramBotClient bot,
            IImageUrlComposer imageUrlComposer,
            SaveCardsUseCase saveCarsUseCase)
        {
            _bot = bot;
            _imageUrlComposer = imageUrlComposer;
            _saveCarsUseCase = saveCarsUseCase;
        }

        // method that handle messages received by the bot:
        public async Task OnMessage(Message msg, UpdateType type)
        {
            Console.WriteLine($"Message: {msg.Text}");

            var text = msg.Text.ToLower();
            if (text.StartsWith(CONST.BOT_ADDRESSING_COMMAND))
            {
                if (text.Contains(CONST.GET_CARD_COMMAND))
                {
                    var cardName = text
                        .Replace(CONST.BOT_ADDRESSING_COMMAND, "")
                        .Replace(CONST.GET_CARD_COMMAND, "")
                        .ToSearchable();

                    var imageUrl = _imageUrlComposer.ComposeByCardName(cardName);
                    if (imageUrl != null)
                    {
                        await _bot.SendPhoto(msg.Chat, imageUrl, replyParameters: msg.Id);
                    }
                    else
                    {
                        await _bot.SendMessage(msg.Chat, "Не нашлось такой карты");
                    }
                }

                if (text.Contains(CONST.GET_TERM_COMMAND))
                {
                    var term = text
                        .Replace(CONST.BOT_ADDRESSING_COMMAND, "")
                        .Replace(CONST.GET_TERM_COMMAND, "")
                        .ToSearchable();

                    if (term == "броня")
                    {
                        await _bot.SendMessage(msg.Chat,
                            "Броня Х: карта с бронёй Х не получает первые" +
                            " Х ран от немагических атак в течение каждого хода (и своего, и противника).");
                    }
                    else
                    {
                        await _bot.SendMessage(msg.Chat, "Не нашлось такого термина");
                    }
                }

                if (text.Contains(CONST.GET_XLSX_DUMP_COMMAND))
                {
                    await _saveCarsUseCase.ExecuteAsync();

                    await using Stream stream = System.IO.File.OpenRead("MyBerserkHeroesCollection.xlsx");
                    var message = await _bot.SendDocument(msg.Chat, document: InputFile.FromStream(
                        stream, $"BHCollection-{DateTime.Now.Date.ToShortDateString()}.xlsx"),
                        caption: $"Выгрузка базы карт сайта berserkdeck.ru за {DateTime.Now.Date.ToShortDateString()}");
                }
            }

            if (msg.Text == "/start")
            {
                await _bot.SendMessage(msg.Chat, "Welcome! Pick one direction",
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
    }
}
