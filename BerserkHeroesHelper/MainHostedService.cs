using Core.Application.UseCases;
using Infrastructure.TelegramBot;
using Infrastructure.Vk;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
    internal class MainHostedService : IHostedService
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly VkApplicationClient _vkApplicationClient;

        public MainHostedService(
            TelegramBotClient telegramBotClient,
            VkApplicationClient vkApplicationClient)
        {
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
            _vkApplicationClient.Stop();
            return Task.CompletedTask;
        }
    }
}
