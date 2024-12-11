using Core.Domain.Entities;
using Infrastructure.SQLite.DbContexts;
using Infrastructure.Vk.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLite.Repositories
{
    public class AuctionPostInfoRepository : IAuctionPostInfoRepository
    {
        private readonly AppDbContext _context;

        public AuctionPostInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(IEnumerable<AuctionPostInfo> range)
        {
            var existingAucInfos = _context.AuctionPostInfos.Select(x => x.ExternalId).ToHashSet();
            var aucInfosToSave = range.Where(x => !existingAucInfos.Contains(x.ExternalId));

            await _context.AddRangeAsync(aucInfosToSave);
        }

        public void AddRange(IEnumerable<AuctionPostInfo> range)
        {
            var existingAucInfos = _context.AuctionPostInfos.Select(x => x.ExternalId).ToHashSet();
            var aucInfosToSave = range.Where(x => !existingAucInfos.Contains(x.ExternalId));

            _context.AddRange(aucInfosToSave);
        }

        public string? GetAucInfosByCardName(string cardName)
        {
            var infos = _context.AuctionPostInfos
                .Where(x => x.LotDescriptionForSearching.Contains(cardName));

            var resultStringBuilder  = new StringBuilder();
            resultStringBuilder.Append($"Упоминания карты в описаниях сыгравших и выкупленных лотов лотов:\r\n\r\n");
            foreach (var info in infos)
            {
                resultStringBuilder.Append($"{info.LotDescription} | {info.FinalPrice} р. | {info.EndDate}\r\n");
            }

            return resultStringBuilder.ToString();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
