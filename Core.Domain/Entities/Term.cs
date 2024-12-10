using Infrastructure.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{

    /// <summary>
    /// Игровой термин.
    /// </summary>
    public class Term
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; init; }

        private string _name;

        /// <summary>
        /// Название.
        /// </summary>
        public string Name
        {
            get => _name;
            init 
            { 
                _name = value;
                this.NameForSearching = _name.ToSearchable();
            }
        }

        /// <summary>
        /// Название для поиска (без лишних символов, приведенное в нижнему регистру).
        /// Костыль, так как применение ToLowerInvariant не транслируется в SQL, а ToLower не работает.
        /// </summary>
        public string NameForSearching { get; private set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Text { get; init; }
    }
}
