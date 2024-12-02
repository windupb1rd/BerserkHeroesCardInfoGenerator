using Core.Application.Abstractions;

namespace Core.Application.UseCases
{
    public class SaveCardsUseCase
    {
        private readonly IWebApiClient _webApiClient;
        private readonly ICardsStorageCreator _сreator;

        public SaveCardsUseCase(
            IWebApiClient webApiClient,
            ICardsStorageCreator сreator)
        {
            _webApiClient = webApiClient;
            _сreator = сreator;
        }

        public async Task ExecuteAsync()
        {
            var cards = await _webApiClient.GetCardsAsync();
            await _сreator.Create(cards);
        }
    }
}
