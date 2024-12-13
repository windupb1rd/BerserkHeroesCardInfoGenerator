using Infrastructure.WebApiClient.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.WebApiClient
{
    public class ContentDownloaderProvider : IContentDownloaderProvider
    {
        private readonly Func<IContentDownloader> _contentDownloaderFactory;

        public ContentDownloaderProvider(Func<IContentDownloader> contentDownloaderFactory)
        {
            _contentDownloaderFactory = contentDownloaderFactory;
        }

        public IContentDownloader Create()
        {
            return _contentDownloaderFactory.Invoke();
        }
    }
}
