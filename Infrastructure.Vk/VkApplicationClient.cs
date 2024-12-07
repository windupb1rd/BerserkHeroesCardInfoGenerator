using VkNet.Model;
using VkNet;
using Infrastructure.Vk.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Vk
{
    public class VkApplicationClient
    {
        private readonly VkApplicationClientOptions _options;

        public VkApplicationClient(IOptions<VkApplicationClientOptions> options)
        {
            _options = options.Value;
        }

        public Task Start()
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = _options.ApplicationId,
                AccessToken = _options.Token
            });

            // Отправка сообщения себе
            //api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            //{
            //    ChatId = api.UserId.Value,
            //    Message = "message"
            //});

            var wall = api.Wall.Get(new WallGetParams
            {
                OwnerId = -218709395, 
                Count = 100,
                Offset = 0,
                Extended = true
            });

            //var c = api.Wall.GetComments(new WallGetCommentsParams { PostId = 109827 }); // нет доступа к комментариям из приложения

            //Console.ReadLine();

            return Task.CompletedTask;
        }
    }
}
