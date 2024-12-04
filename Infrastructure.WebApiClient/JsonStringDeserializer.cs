using Infrastructure.WebApiClient.Abstractions;
using Infrastructure.WebApiClient.Models;
using Newtonsoft.Json;

namespace Infrastructure.WebApiClient
{
    public class JsonStringDeserializer : IContentStringDeserializer
    {
        public Page DeserilizeIntoPage(string response)
        {
            return JsonConvert.DeserializeObject<Page>(response);
        }
    }
}
