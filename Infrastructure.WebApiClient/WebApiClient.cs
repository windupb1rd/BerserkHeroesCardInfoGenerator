using Core.Application.Abstractions;
using Core.Domain.Entities;
using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Mapping;
using Infrastructure.WebApiClient.Models;
using Infrastructure.WebApiClient.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.WebApiClient
{
    public class WebApiClient : IWebApiClient
    {
        private const string URL_PATTERN = "{0}?sort=setInfo.ordinal,desc&sort=number&page={1}&size=2000";
        private readonly WebApiOptions _options;
        private readonly IJsonDownloader _jsonDownloader;

        public WebApiClient(
            IOptions<WebApiOptions> options,
            IJsonDownloader jsonDownloader)
        {
            _options = options.Value;
            _jsonDownloader = jsonDownloader;
        }

        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            var content = new List<Content>();

            var pageNumber = 0;
            var totalNumberOfPages = 0;
            do
            {
                string response = await _jsonDownloader.GetJson(string.Format(URL_PATTERN, _options.Url, ++pageNumber));
                var data = JsonConvert.DeserializeObject<Page>(response);
                content.AddRange(data.Content);
                totalNumberOfPages = data.TotalPages;
            }
            while (pageNumber < totalNumberOfPages);

            _jsonDownloader.Dispose();

            return new ContentToCardMapper().Map(content);
        }
    }
}
