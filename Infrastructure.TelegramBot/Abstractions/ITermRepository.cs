using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TelegramBot.Abstractions
{
    public interface ITermRepository
    {
        /// <summary>
        /// Получить определение игрового термина по имени или части имени.
        /// </summary>
        /// <returns></returns>
        string GetTermByName(string termName);
    }
}
