using Core.Domain.Entities;

namespace Infrastructure.Vk.Abstractions
{
    public interface IAuctionPostInfoRepository
    {
        Task AddRangeAsync(IEnumerable<AuctionPostInfo> range);

        void AddRange(IEnumerable<AuctionPostInfo> range);

        Task SaveChangesAsync();

        int SaveChanges();

        string? GetAucInfosByCardName(string cardName);
    }
}
