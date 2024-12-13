using Core.Domain.Entities;
using Infrastructure.Vk.Abstractions;
using Infrastructure.Vk.Mapping;
using Infrastructure.Vk.Options;
using VkNet.Model;
using VkNet;

namespace Infrastructure.Vk
{
    public class AucInfoUpdater : IDisposable
    {
        private readonly VkApplicationClientOptions _options;
        private readonly IAuctionPostInfoRepository _auctionPostInfoRepository;
        private Timer _timer;

        public AucInfoUpdater(
            VkApplicationClientOptions options,
            IAuctionPostInfoRepository auctionPostInfoRepository)
        {
            _options = options;
            _auctionPostInfoRepository = auctionPostInfoRepository;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public void Start()
        {
            _timer = new Timer(Update, null, 60000, 86400000);
        }

        private void Update(object? state)
        {
            Task.Run(() =>
            {
                var api = new VkApi();

                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = _options.ApplicationId,
                    AccessToken = _options.Token
                });


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

                _auctionPostInfoRepository.AddRange(l);
                _auctionPostInfoRepository.SaveChanges();

                api.Dispose();
            });
        }
    }
}
