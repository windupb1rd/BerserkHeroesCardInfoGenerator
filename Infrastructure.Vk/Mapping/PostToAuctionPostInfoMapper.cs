using Core.Domain.Entities;
using VkNet.Model;

namespace Infrastructure.Vk.Mapping
{
    /// <summary>
    /// Маппер из коллекции <see cref="Post"/> в коллекцию <see cref="Card"/>.
    /// </summary>
    public class PostToAuctionPostInfoMapper
    {
        /// <summary>
        /// Маппит коллекцию <see cref="Post"/> в коллекцию <see cref="AuctionPostInfo"/>
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public IEnumerable<AuctionPostInfo> Map(IEnumerable<Post> content)
        {
            var aucPostInfos = new List<AuctionPostInfo>();

            foreach (var post in content)
            {
                var dateIndex = post.Text.IndexOf("аукцион продлится до ") + "аукцион продлится до ".Length;
                var priceStartIndex = post.Text.IndexOf("Аукцион завершен со ставкой ") + "Аукцион завершен со ставкой ".Length;
                var priceEndIndex = post.Text.IndexOf(" рублей") - priceStartIndex;
                var descriptionStartIndex = post.Text.IndexOf("Описание лота: ") + "Описание лота: ".Length;
                var descriptionEndIndex = post.Text.IndexOf("Состояние лота:") - descriptionStartIndex;

                var endDate = post.Text.Substring(dateIndex, 8);
                var price = post.Text.Substring(priceStartIndex, priceEndIndex);
                var description = post.Text.Substring(descriptionStartIndex, descriptionEndIndex);

                aucPostInfos.Add(new AuctionPostInfo
                {
                    FinalPrice = price,
                    EndDate = endDate,
                    LotDescription = description,
                    ExternalId = post.Id
                });
            }

            return aucPostInfos;
        }
    }
}
