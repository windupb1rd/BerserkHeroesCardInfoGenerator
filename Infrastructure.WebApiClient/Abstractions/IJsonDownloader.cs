namespace Infrastructure.WebApiClient.Abstractions
{
    public interface IContentDownloader : IDisposable
    {
        Task<string> GetContentString(string url);
    }
}
