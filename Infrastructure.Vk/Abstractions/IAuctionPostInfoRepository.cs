using Core.Domain.Entities;

namespace Infrastructure.Vk.Abstractions
{
    public interface IAuctionPostInfoRepository
    {
        Task AddRangeAsync(IEnumerable<AuctionPostInfo> range);

        Task SaveChangesAsync();

        string? GetAucInfosByCardName(string cardName);
    }
}
