using Core.Application.Abstractions;
using Core.Domain.Entities;
using Infrastructure.WebApiClient.Mapping;
using Infrastructure.WebApiClient.Models;
using Infrastructure.WebApiClient.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.WebApiClient
{
    public class WebApiClient : IWebApiClient
    {
        private readonly WebApiOptions _options;

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

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            string url = "{0}?sort=setInfo.ordinal,desc&sort=number&page={1}&size=2000";
            HttpClient client = new HttpClient();

            var content = new List<Content>();

            var pageNumber = 0;
            var totalNumberOfPages = 0;
            do
            {
                string response = await client.GetStringAsync(string.Format(url, _options.Url, ++pageNumber));
                var data = JsonConvert.DeserializeObject<Page>(response);
                content.AddRange(data.Content);
                totalNumberOfPages = data.TotalPages;
            }
            while (pageNumber < totalNumberOfPages);

            return new ContentToCardMapper().Map(content);
        }
    }
}
