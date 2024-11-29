using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Abstractions
{
    public interface IWebApiClient
    {
        Task<IEnumerable<Card>> GetCardsAsync(); // сущность домена? модель?
    }
}
