using Core.Application.Abstractions;
using Core.Domain.Entities;
using Infrastructure.SQLite.DbContexts;

namespace Infrastructure.SQLite
{
    public class SqliteStorageCreator : ICardsStorageCreator
    {
        private readonly AppDbContext _context;

        public SqliteStorageCreator(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(IEnumerable<Card> cards)
        {
            var a = _context.Cards;

            // TODO: проверять на наличие, добавлять только новые

            await _context.AddRangeAsync(cards);
            //await _context.SaveChangesAsync();

            var a1 = _context.Cards;
        }
    }
}
