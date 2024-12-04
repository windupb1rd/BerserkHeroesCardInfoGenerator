
using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Декоратор-логгер сервиса загрузки данных с API.
    /// </summary>
    public class ContentDownloaderLogger : IContentDownloader
    {
        private readonly IContentDownloader _client;

        public ContentDownloaderLogger(IContentDownloader client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        /// <inheritdoc/>
        public Task<string> GetContentString(string url)
        {
            Console.WriteLine("Вызван декоратор логгирования.");
            Console.WriteLine(url);
            return _client.GetContentString(url);
        }
    }
}
