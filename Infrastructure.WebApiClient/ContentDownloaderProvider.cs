using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class ContentDownloaderProvider : IContentDownloaderProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContentDownloaderProvider(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IContentDownloader Create()
        {
            var client = _httpClientFactory.CreateClient();
            return new ContentDownloader(client);
        }
    }
}
