using Infrastructure.WebApiClient.Abstractions;
using System.Net.Http;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Сервис загрузки данных с API.
    /// </summary>
    public class ContentDownloader : IContentDownloader
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private bool _isDisposed = true;

        public ContentDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
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
                _httpClient = _httpClientFactory.CreateClient(url);
                _isDisposed = false;
            }

            return _httpClient.GetStringAsync(url);
        }
    }
}
