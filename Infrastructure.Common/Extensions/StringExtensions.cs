using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToSearchable(this string str)
        {
            return str
                .Replace(",", "")
                .Replace(".", "")
                .Replace(":", "")
                .Replace("!", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Replace(" ", "")
                .Replace(@"\", "")
                .Replace("\"", "")
                .ToLowerInvariant()
                .Trim();
        }
    }
}
