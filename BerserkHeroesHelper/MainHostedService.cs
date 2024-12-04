using Core.Application.UseCases;
using Microsoft.Extensions.Hosting;

namespace ConsoleClient
{
    internal class MainHostedService : IHostedService
    {
        private readonly SaveCardsUseCase _saveCardsUseCase;

        public MainHostedService(SaveCardsUseCase saveCardsUseCase)
        {
            _saveCardsUseCase = saveCardsUseCase;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _saveCardsUseCase.ExecuteAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
