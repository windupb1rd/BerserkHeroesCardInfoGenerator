using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class JsonDownloader : IJsonDownloader
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public Task<string> GetJson(string url)
        {
            return _httpClient.GetStringAsync(url);
        }
    }
}
