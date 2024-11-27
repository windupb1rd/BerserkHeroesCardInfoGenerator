using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQLite.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDbContextOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public required string ConnectionString { get; set; }
    }
}
