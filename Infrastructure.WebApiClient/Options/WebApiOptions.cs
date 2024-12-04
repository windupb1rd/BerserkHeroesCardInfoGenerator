using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.WebApiClient.Options
{
    /// <summary>
    /// Настройки веб-клиента.
    /// </summary>
    public class WebApiOptions
    {
        /// <summary>
        /// Url.
        /// </summary>
        public required string Url { get; set; }
    }
}
