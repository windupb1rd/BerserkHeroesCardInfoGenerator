using VkNet.Model;
using VkNet;
using Infrastructure.Vk.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Core.Domain.Entities;
using Infrastructure.Vk.Mapping;
using Infrastructure.Vk.Abstractions;

namespace Infrastructure.Vk
{
    public class VkApplicationClient
    {
        private readonly VkApplicationClientOptions _options;
        private readonly IAuctionPostInfoRepository _auctionPostInfoRepository;

        public VkApplicationClient(
            IOptions<VkApplicationClientOptions> options,
            IAuctionPostInfoRepository auctionPostInfoRepository)
        {
            _options = options.Value;
            _auctionPostInfoRepository = auctionPostInfoRepository;
        }

        public async Task Start()
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                ApplicationId = _options.ApplicationId,
                AccessToken = _options.Token
            });

            //=============================================================
            //пополнение базы, довести до ума и сделать раз в сутки. значения offset достаточно будет 500.

            var l = new List<AuctionPostInfo>();

            for (ulong offset = 0; offset <= 500; offset += 100)
            {
                var wall = api.Wall.Get(new WallGetParams
                {
                    OwnerId = -218709395,
                    Count = 100,
                    Offset = offset,
                    Extended = false
                });

                var mapped = new PostToAuctionPostInfoMapper()
                    .Map(wall.WallPosts.Where(x => x.Text.Contains("Аукцион завершен со ставкой")));

                
                l.AddRange(mapped);
            }

            await _auctionPostInfoRepository.AddRangeAsync(l);
            await _auctionPostInfoRepository.SaveChangesAsync();

            //==============================================================

            //var c = api.Wall.GetComments(new WallGetCommentsParams { PostId = 109827 }); // нет доступа к комментариям из приложения

            // Отправка сообщения себе
            //api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            //{
            //    ChatId = api.UserId.Value,
            //    Message = "message"
            //});
        }
    }
}
