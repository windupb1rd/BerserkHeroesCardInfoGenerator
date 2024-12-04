using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Models;
using Newtonsoft.Json;

namespace Infrastructure.WebApiClient
{
    /// <summary>
    /// Десерилизатор в JSON.
    /// </summary>
    public class JsonStringDeserializer : IContentStringDeserializer
    {
        /// <inheritdoc/>
        public Page DeserilizeIntoPage(string response)
        {
            return JsonConvert.DeserializeObject<Page>(response);
        }
    }
}
