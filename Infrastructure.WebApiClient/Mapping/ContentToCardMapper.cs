using Core.Domain.Entities;
using Infrastructure.WebApiClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApiClient.Mapping
{
    public class ContentToCardMapper
    {
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
