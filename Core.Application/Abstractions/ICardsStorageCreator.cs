using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Abstractions
{
    /// <summary>
    /// Абстракция сервиса для создания хранилища карт с описаниями.
    /// </summary>
    public interface ICardsStorageCreator
    {
        /// <summary>
        /// Создает хранилище карт.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public Task Create(IEnumerable<Card> cards);
    }
}
