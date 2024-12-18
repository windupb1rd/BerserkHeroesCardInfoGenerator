﻿using Telegram.Bot.Polling;

namespace Infrastructure.TelegramBot.Handlers
{
    internal class OnErrorHandler
    {
        public async Task OnError(Exception exception, HandleErrorSource source)
        {
            Console.WriteLine(exception); // just dump the exception to the console
        }
    }
}
