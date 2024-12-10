using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Infrastructure.SQLite.DbContexts;
using Infrastructure.TelegramBot.Abstractions;

namespace Infrastructure.SQLite.Repositories
{
    public class TermRepository : ITermRepository
    {
        private readonly AppDbContext _context;

        public TermRepository(AppDbContext context)
        {
            _context = context;
        }

        public string? GetTermByName(string termName)
        {
            var term = _context.Terms
                .Where(x => x.NameForSearching.Contains(termName))
                .FirstOrDefault();

            return term == null
                ? null
                : $"{term.Name}.\r\n\r\n{term.Text}";
        }
    }
}
