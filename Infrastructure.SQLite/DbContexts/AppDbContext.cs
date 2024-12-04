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

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
