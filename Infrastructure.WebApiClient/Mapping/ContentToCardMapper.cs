using Core.Domain.Entities;
using Infrastructure.Common.Extensions;
using Infrastructure.WebApiClient.Models;

namespace Infrastructure.WebApiClient.Mapping
{
    /// <summary>
    /// Маппер из коллекции <see cref="Content"/> в коллекцию <see cref="Card"/>.
    /// </summary>
    public class ContentToCardMapper
    {
        /// <summary>
        /// Маппит коллекцию <see cref="Content"/> в коллекцию <see cref="Card"/>
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IEnumerable<Card> Map(IEnumerable<Content> content)
        {
            var cards = new List<Card>();

            foreach (var card in content)
            {
                cards.Add(new Card
                {
                    ExternalId = card.Id,
                    Name = card.Name,
                    NameForSearching = card.Name.ToSearchable(),
                    Cost = card.Cost,
                    Health = card.Health,
                    Attack = card.Attack,
                    Elements = string.Join(", ", card.Elements),
                    FirstClass = card.FirstClass,
                    SecondClass = card.SecondClass,
                    Rarity = card.Rarity,
                    IsFoil = card.IsFoil,
                    Text = card.Text,
                    Number = card.Number,
                    @Type = card.Type,
                    Variant = card.Variant,
                    SetName = card.SetInfo.Name,
                    SetNumber = card.SetInfo.Ordinal,
                    Errata = card.Errata,
                    IsActual = card.IsActual,
                    Painter = card.Painter
                });
            }

            return cards;
        }
    }
}
