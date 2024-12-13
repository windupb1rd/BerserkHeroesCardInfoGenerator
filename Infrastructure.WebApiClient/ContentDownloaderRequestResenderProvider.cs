using Infrastructure.WebApiClient.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApiClient
{
    public class ContentDownloaderRequestResenderProvider : IContentDownloaderProvider
    {
        private readonly IContentDownloaderProvider _provider;

        public ContentDownloaderRequestResenderProvider(
            IContentDownloaderProvider provider)
        {
            _provider = provider;
        }

        public IContentDownloader Create()
        {
            return new ContentDownloaderRequestResender(_provider.Create());
        }
    }
}
