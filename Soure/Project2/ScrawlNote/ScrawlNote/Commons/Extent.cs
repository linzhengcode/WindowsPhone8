using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.Commons
{
    public static class Extent
    {
        public static bool IsEqual(this string s, string value)
        {
            if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(value))
            {
                return true;
            }
            if (string.IsNullOrEmpty(s) != string.IsNullOrEmpty(value))
            {
                return false;
            }
            return ((!string.IsNullOrEmpty(s) && !string.IsNullOrEmpty(value)) && s.Equals(value, StringComparison.OrdinalIgnoreCase));
        }
    }
}
