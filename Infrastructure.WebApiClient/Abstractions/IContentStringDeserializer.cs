using Infrastructure.WebApiClient.Models;

namespace Infrastructure.WebApiClient.Abstractions
{
    /// <summary>
    /// Абстракция десериализатора в объект <see cref="Page"/>
    /// </summary>
    public interface IContentStringDeserializer
    {
        /// <summary>
        /// Десериализует строку в объект <see cref="Page"/>
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Page DeserilizeIntoPage(string response);
    }
}