using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class ContentDownloader : IContentDownloader
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public Task<string> GetContentString(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}
