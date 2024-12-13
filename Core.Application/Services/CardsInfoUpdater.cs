using Core.Application.UseCases;

namespace Core.Application.Services
{
    public class CardsInfoUpdater : IDisposable
    {
        private readonly SaveCardsUseCase _useCase;
        private Timer? _timer;

        public CardsInfoUpdater(SaveCardsUseCase useCase)
        {
            _useCase = useCase;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public void Start()
        {
            _timer = new Timer(Update, null, 60000, 86400000);
        }

        private void Update(object? state)
        {
            Task.Run(async () =>
            {
                await _useCase.ExecuteAsync();
            });
        }
    }
}
