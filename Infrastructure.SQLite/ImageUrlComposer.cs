using Infrastructure.SQLite.DbContexts;
using Infrastructure.TelegramBot.Abstractions;
using Infrastructure.WebApiClient.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLite
{
    public class ImageUrlComposer : IImageUrlComposer
    {
        private readonly AppDbContext _context;
        private readonly WebApiOptions _options;

        public ImageUrlComposer(
            AppDbContext context,
            IOptions<WebApiOptions> options)
        {
            _context = context;
            _options = options.Value;
        }

        public string? ComposeByCardName(string cardName)
        {
            var card = _context.Cards
                .Where(x => x.NameForSearching.Contains(cardName))
                .FirstOrDefault();

            return card == null
                ? null
                : string.Format(_options.ImageUrlPattern, card.SetNumber, card.Number, card.Variant);
        }
    }
}
