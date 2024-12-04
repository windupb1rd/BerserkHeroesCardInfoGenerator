
using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Декоратор повторной отправки запроса сервисом загрузки данных с API при неудачной попытке.
    /// </summary>
    public class ContentDownloaderRequestResender : IContentDownloader
    {
        private readonly IContentDownloader _client;

        public ContentDownloaderRequestResender(IContentDownloader client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        /// <inheritdoc/>
        public async Task<string> GetContentString(string url)
        {
            Console.WriteLine("Вызван декоратор переотправки запроса.");

            TimeSpan nextDelay = TimeSpan.FromSeconds(1);
            for (int i = 0; i <= 3; i++)
            {
                try
                {
                    return await _client.GetContentString(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка клиента {ex.ToString()}.");
                    nextDelay += nextDelay;
                    await Task.Delay(nextDelay);
                }

                Console.WriteLine($"Повторная попытка подключения...");
            }

            return await _client.GetContentString(url);
        }
    }
}
