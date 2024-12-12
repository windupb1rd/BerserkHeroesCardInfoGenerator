using Core.Application.UseCases;

namespace Core.Application.Services
{
    public class CardsInfoUpdater : IDisposable
    {
        private readonly SaveCardsUseCase _useCase;
        private readonly Timer _timer;

        public CardsInfoUpdater(SaveCardsUseCase useCase)
        {
            _useCase = useCase;

            _timer = new Timer(Update, null, 60000, 86400000);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public void Update(object? state)
        {
            Task.Run(async () =>
            {
                await _useCase.ExecuteAsync();
            });
        }
    }
}
