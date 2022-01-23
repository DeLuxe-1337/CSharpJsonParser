using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParser.Extensions
{
    public static class StringExtension
    {
        public static string MassReplace(this string str, string replace, params string[] to_replace)
        {
            StringBuilder sb = new(str);

            foreach(var s in to_replace)
            {
                sb.Replace(s, replace);
            }

            return sb.ToString();
        }
    }
}
