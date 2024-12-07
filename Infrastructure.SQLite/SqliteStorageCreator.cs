using Core.Application.Abstractions;
using Core.Domain.Entities;
using Infrastructure.SQLite.DbContexts;

namespace Infrastructure.SQLite
{
    /// <summary>
    /// Сервис сохранения карточек в БД SQLite.
    /// </summary>
    public class SqliteStorageCreator : ICardsStorageCreator
    {
        private readonly AppDbContext _context;

        public SqliteStorageCreator(AppDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task Create(IEnumerable<Card> cards)
        {
            var cardsInDb = _context.Cards.ToList();

            var cardsToSave = cards
                .Where(x => !cardsInDb
                    .Select(x => x.ExternalId)
                    .Contains(x.ExternalId));

            await _context.AddRangeAsync(cardsToSave);

            await _context.SaveChangesAsync();

            var a1 = _context.Cards;
        }
    }
}
