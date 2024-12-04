using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Сервис загрузки данных с API.
    /// </summary>
    public class ContentDownloader : IContentDownloader
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        /// <inheritdoc/>
        public Task<string> GetContentString(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}
