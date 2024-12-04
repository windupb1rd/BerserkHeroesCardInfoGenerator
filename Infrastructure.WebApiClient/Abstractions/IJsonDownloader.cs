namespace Infrastructure.WebApiClient.Abstractions
{
    public interface IJsonDownloader : IDisposable
    {
        Task<string> GetJson(string url);
    }
}
