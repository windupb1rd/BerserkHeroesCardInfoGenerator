using Core.Application.Abstractions;
using Infrastructure.WebApiClient.Models;
using Infrastructure.WebApiClient.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.WebApiClient
{
    public class WebApiClient// : IWebApiClient
    {
        private readonly WebApiOptions _options;
        //private readonly List<Content> _cards = new List<Content>();

        public WebApiClient(IOptions<WebApiOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// для тестов из program, удалить
        /// </summary>
        /// <param name="url"></param>
        public WebApiClient(string url)
        {
            _options = new WebApiOptions { Url = url };
        }

        //public IEnumerable<Content> GetCards => _cards;

        public async Task<IEnumerable<Content>> GetCards()
        {
            string url = "{0}?sort=setInfo.ordinal,desc&sort=number&page={1}&size=2000";
            HttpClient client = new HttpClient();

            var cards = new List<Content>();

            var pageNumber = 0;
            var totalNumberOfPages = 0;
            do
            {
                string response = await client.GetStringAsync(string.Format(url, _options.Url, ++pageNumber));
                var data = JsonConvert.DeserializeObject<Page>(response);
                cards.AddRange(data.Content);
                totalNumberOfPages = data.TotalPages;
            }
            while (pageNumber < totalNumberOfPages);

            return cards;
        }
    }
}
