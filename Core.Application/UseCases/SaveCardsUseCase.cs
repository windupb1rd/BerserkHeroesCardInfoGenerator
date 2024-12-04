using Core.Application.Abstractions;

namespace Core.Application.UseCases
{
    public class SaveCardsUseCase
    {
        private readonly IWebApiClient _webApiClient;
        private readonly IEnumerable<ICardsStorageCreator> _сreators;

        public SaveCardsUseCase(
            IWebApiClient webApiClient,
            IEnumerable<ICardsStorageCreator> сreators)
        {
            _webApiClient = webApiClient;
            _сreators = сreators;
        }

        public async Task ExecuteAsync()
        {
            var cards = await _webApiClient.GetCardsAsync();
            foreach (var creator in _сreators)
            {
                await creator.Create(cards);
            }
        }
    }
}
