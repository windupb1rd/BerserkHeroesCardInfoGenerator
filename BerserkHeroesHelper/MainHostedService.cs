using Core.Application.UseCases;
using Infrastructure.TelegramBot;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
    internal class MainHostedService : IHostedService
    {
        private readonly SaveCardsUseCase _saveCardsUseCase;
        private readonly TelegramBotClient _telegramBotClient;

        public MainHostedService(
            SaveCardsUseCase saveCardsUseCase,
            TelegramBotClient telegramBotClient)
        {
            _saveCardsUseCase = saveCardsUseCase;
            _telegramBotClient = telegramBotClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _saveCardsUseCase.ExecuteAsync();
            await _telegramBotClient.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
