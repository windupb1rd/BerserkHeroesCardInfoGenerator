using Infrastructure.WebApiClient.Abstractions;
using System.Net.Http;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Сервис загрузки данных с API.
    /// </summary>
    public class ContentDownloader : IContentDownloader
    {
        private readonly HttpClient _httpClient;
        private bool _isDisposed = false;

        public ContentDownloader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _isDisposed = true;
        }

        /// <inheritdoc/>
        public Task<string> GetContentString(string url)
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(nameof(ContentDownloader));
            }

            return _httpClient.GetStringAsync(url);
        }
    }
}
