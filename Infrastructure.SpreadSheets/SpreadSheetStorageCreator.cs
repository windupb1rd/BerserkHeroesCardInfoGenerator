using Core.Application.Abstractions;
using Core.Domain.Entities;

namespace Infrastructure.SpreadSheets
{
    public class SpreadSheetStorageCreator : ICardsStorageCreator
    {
        public Task Create(IEnumerable<Card> cards)
        {
            throw new NotImplementedException();
        }
    }
}
