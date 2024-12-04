using Infrastructure.WebApiClient.Models;

namespace Infrastructure.WebApiClient.Abstractions
{
    public interface IContentStringDeserializer
    {
        /// <summary>
        /// Десериализовать строку в объект <see cref="Page"/>
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public Page DeserilizeIntoPage(string response);
    }
}