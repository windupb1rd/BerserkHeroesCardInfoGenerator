using Infrastructure.WebApiClient.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApiClient
{
    public class ContentDownloaderLoggerProvider : IContentDownloaderProvider
    {
        private readonly IContentDownloaderProvider _provider;

        public ContentDownloaderLoggerProvider(IContentDownloaderProvider provider)
        {
            _provider = provider;
        }

        public IContentDownloader Create()
        {
            return new ContentDownloaderLogger(_provider.Create());
        }
    }
}
