using Infrastructure.SQLite.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLite.DbContexts
{
    public class AppDbContext : DbContext
    {
        private readonly IOptions<AppDbContextOptions> _options;

        //public AppDbContext(IOptions<AppDbContextOptions> options)
        //{
        //    _options = options;
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_options.Value.ConnectionString);
        }
    }
}
