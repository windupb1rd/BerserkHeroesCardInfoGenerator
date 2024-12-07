using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Abstractions
{
    /// <summary>
    /// Абстракция клиента API для получения данных о картах.
    /// </summary>
    public interface IWebApiClient
    {
        /// <summary>
        /// Получает объекты карт.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Card>> GetCardsAsync();
    }
}
