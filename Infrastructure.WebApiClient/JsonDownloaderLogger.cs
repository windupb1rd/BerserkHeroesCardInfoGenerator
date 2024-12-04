
using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class JsonDownloaderLogger : IJsonDownloader
    {
        private readonly IJsonDownloader _client;

        public JsonDownloaderLogger(IJsonDownloader client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public Task<string> GetJson(string url)
        {
            Console.WriteLine("Вызван декоратор логгирования.");
            Console.WriteLine(url);
            return _client.GetJson(url);
        }
    }
}
