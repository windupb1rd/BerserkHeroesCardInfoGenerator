using Infrastructure.Common.Extensions;

namespace Core.Domain.Entities
{
    /// <summary>
    /// Информация о сыгравших лотах.
    /// </summary>
    public class AuctionPostInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        private string _lotDescription;

        /// <summary>
        /// Название.
        /// </summary>
        public string LotDescription
        {
            get => _lotDescription;
            init
            {
                _lotDescription = value;
                this.LotDescriptionForSearching = _lotDescription.ToSearchable();
            }
        }

        /// <summary>
        /// Название для поиска (без лишних символов, приведенное в нижнему регистру).
        /// Костыль, так как применение ToLowerInvariant не транслируется в SQL, а ToLower не работает.
        /// </summary>
        public string LotDescriptionForSearching { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FinalPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long? ExternalId { get; set; }
    }
}
