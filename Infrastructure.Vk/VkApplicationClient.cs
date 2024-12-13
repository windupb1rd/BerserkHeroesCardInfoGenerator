using Infrastructure.Vk.Options;
using Microsoft.Extensions.Options;
using Infrastructure.Vk.Abstractions;

namespace Infrastructure.Vk
{
    public class VkApplicationClient
    {
        private readonly VkApplicationClientOptions _options;
        private readonly IAuctionPostInfoRepository _auctionPostInfoRepository;
        private AucInfoUpdater _updater;

        public VkApplicationClient(
            IOptions<VkApplicationClientOptions> options,
            IAuctionPostInfoRepository auctionPostInfoRepository)
        {
            _options = options.Value;
            _auctionPostInfoRepository = auctionPostInfoRepository;
        }

        public Task Start()
        {
            _updater = new AucInfoUpdater(_options, _auctionPostInfoRepository);
            _updater.Start();

            return Task.CompletedTask;
        }

        public void Stop()
        {
            _updater.Dispose();
        }
    }
}
