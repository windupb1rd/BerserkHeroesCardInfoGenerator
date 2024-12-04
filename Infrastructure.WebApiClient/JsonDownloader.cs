using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class JsonDownloader : IJsonDownloader
    {
        public Task<string> GetJson(string url)
        {
            HttpClient client = new HttpClient();
            return client.GetStringAsync(url);
        }
    }
}
