using Core.Application.Services;
using Core.Application.UseCases;
using Infrastructure.TelegramBot;
using Infrastructure.Vk;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
    internal class MainHostedService : IHostedService
    {
        private readonly TelegramBotService _telegramBotClient;
        private readonly VkApplicationClient _vkApplicationClient;
        private readonly CardsInfoUpdater _cardsInfoUpdater;

        public MainHostedService(
            TelegramBotService telegramBotClient,
            VkApplicationClient vkApplicationClient,
            CardsInfoUpdater cardsInfoUpdater)
        {
            _telegramBotClient = telegramBotClient;
            _vkApplicationClient = vkApplicationClient;
            _cardsInfoUpdater = cardsInfoUpdater;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _cardsInfoUpdater.Start();
            await _vkApplicationClient.Start();
            await _telegramBotClient.Start();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _vkApplicationClient.Stop();
            return Task.CompletedTask;
        }
    }
}
