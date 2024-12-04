using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SQLite.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
