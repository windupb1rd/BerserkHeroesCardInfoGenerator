namespace Infrastructure.WebApiClient.Abstractions
{
    /// <summary>
    /// Абстракция загрузчика данных с API.
    /// </summary>
    public interface IContentDownloader : IDisposable
    {
        /// <summary>
        /// Получает данные с API в виде строки.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> GetContentString(string url);
    }
}
