using Core.Domain.Entities;
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
                    Cost = card.Cost,
                    Health = card.Health,
                    Attack = card.Attack,
                    Elements = string.Join(", ", card.Elements),
                    CardClasses = string.Join(", ", card.CardClasses),
                    Rarity = card.Rarity,
                    IsFoil = card.IsFoil,
                    Text = card.Text,
                    Number = card.Number,
                    @Type = card.Type,
                    Variant = card.Variant,
                    Set = $"{card.SetInfo.Name}",
                });
            }

            return cards;
        }
    }
}
