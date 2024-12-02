using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApiClient.Models
{
    /// <summary>
    /// Модель страницы.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Содержимое страницы (карты).
        /// </summary>
        public IEnumerable<Content> Content { get; set; }

        /// <summary>
        /// Не понятно что за свойство.
        /// </summary>
        public bool Empty { get; set; }

        /// <summary>
        /// Признак первой страницы (предположительно). 
        /// </summary>
        public bool First { get; set; }

        /// <summary>
        /// Признак последней страницы.
        /// </summary>
        public bool Last { get; set; }

        /// <summary>
        /// Номер страницы. Первая страница под номером 0, вторая под номером 1 и т.д., 
        /// но при отправке запроса необходимо отправлять 1 для первой страницы, 2 для второй и т.д.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Фактическое количество карт на странице.
        /// </summary>
        public int NumberOfElements { get; set; }

        /// <summary>
        /// Параметры пагинации.
        /// </summary>
        public Pageable Pageable { get; set; }

        /// <summary>
        /// Максимальное количество карт на странице.
        /// При указании в запросе числа большее 2000, приходит все равно максимум 2000 записей.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Параметры сортировки
        /// </summary>
        public Sort Sort { get; set; }

        /// <summary>
        /// Всего карт в БД.
        /// </summary>
        public int TotalElements { get; set; }

        /// <summary>
        /// Всего страниц (меняется в зависимости от настроек пагинации).
        /// </summary>
        public int TotalPages { get; set; }
    }
}
