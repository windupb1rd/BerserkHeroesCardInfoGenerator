namespace Infrastructure.WebApiClient.Abstractions
{
    public interface IJsonDownloader
    {
        Task<string> GetJson(string url);
    }
}
