using Core.Application.UseCases;
using Infrastructure.TelegramBot;
using Infrastructure.Vk;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
    internal class MainHostedService : IHostedService
    {
        private readonly SaveCardsUseCase _saveCardsUseCase;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly VkApplicationClient _vkApplicationClient;

        public MainHostedService(
            SaveCardsUseCase saveCardsUseCase,
            TelegramBotClient telegramBotClient,
            VkApplicationClient vkApplicationClient)
        {
            _saveCardsUseCase = saveCardsUseCase;
            _telegramBotClient = telegramBotClient;
            _vkApplicationClient = vkApplicationClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _vkApplicationClient.Start();
            await _telegramBotClient.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
