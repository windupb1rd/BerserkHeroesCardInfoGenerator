using Core.Application.Abstractions;

namespace Core.Application.UseCases
{
    /// <summary>
    /// Операция сохранения карточек.
    /// </summary>
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

        /// <summary>
        /// Сохраняет карточки.
        /// </summary>
        /// <returns></returns>
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
