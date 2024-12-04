
using Infrastructure.WebApiClient.Abstractions;

namespace Infrastructure.WebApiClient
{
    public class JsonDownloaderRequestResender : IJsonDownloader
    {
        private readonly IJsonDownloader _client;

        public JsonDownloaderRequestResender(IJsonDownloader client)
        {
            _client = client;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<string> GetJson(string url)
        {
            Console.WriteLine("Вызван декоратор переотправки запроса.");

            TimeSpan nextDelay = TimeSpan.FromSeconds(1);
            for (int i = 0; i <= 3; i++)
            {
                try
                {
                    return await _client.GetJson(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка клиента {ex.ToString()}.");
                    nextDelay += nextDelay;
                    await Task.Delay(nextDelay);
                }

                Console.WriteLine($"Повторная попытка подключения...");
            }

            return await _client.GetJson(url);
        }
    }
}
