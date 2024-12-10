using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLite.DbContexts
{
    /// <summary>
    /// Основной контекст для БД SQLite.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Карточки.
        /// </summary>
        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// Игровые термины.
        /// </summary>
        public DbSet<Term> Terms { get; set; }

        /// <summary>
        /// Информация о сыгравших лотах.
        /// </summary>
        public DbSet<AuctionPostInfo> AuctionPostInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
