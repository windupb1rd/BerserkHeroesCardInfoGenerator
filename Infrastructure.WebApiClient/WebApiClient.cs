using Core.Application.Abstractions;
using Core.Domain.Entities;
using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Mapping;
using Infrastructure.WebApiClient.Models;
using Infrastructure.WebApiClient.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Клиент получения данных с API.
    /// </summary>
    public class WebApiClient : IWebApiClient
    {
        private const string URL_PATTERN = "{0}?sort=setInfo.ordinal,desc&sort=number&page={1}&size=2000";
        private readonly WebApiOptions _options;
        private readonly IContentDownloaderProvider _contentDownloaderProvider;
        private readonly IContentStringDeserializer _contentStringDeserializer;

        public WebApiClient(
            IOptions<WebApiOptions> options,
            IContentStringDeserializer contentStringDeserializer,
            IContentDownloaderProvider contentDownloaderProvider)
        {
            _options = options.Value;
            _contentStringDeserializer = contentStringDeserializer;
            _contentDownloaderProvider = contentDownloaderProvider;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Card>> GetCardsAsync()
        {
            var content = new List<Content>();

            var pageNumber = 0;
            var totalNumberOfPages = 0;

            using (var contentDownloader = _contentDownloaderProvider.Create())
            {
                do
                {
                    string response = await contentDownloader.GetContentString(string.Format(URL_PATTERN, _options.Url, ++pageNumber));
                    var data = _contentStringDeserializer.DeserilizeIntoPage(response);
                    content.AddRange(data.Content);
                    totalNumberOfPages = data.TotalPages;
                }
                while (pageNumber < totalNumberOfPages);
            }

            return new ContentToCardMapper().Map(content);
        }
    }
}
