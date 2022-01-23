using System.Text;

namespace JsonParser.Extensions
{
    public static class StringExtension
    {
        public static string MassReplace(this string str, string replace, params string[] to_replace)
        {
            StringBuilder sb = new(str);

            foreach (string s in to_replace)
            {
                sb.Replace(s, replace);
            }

            return sb.ToString();
        }
    }
}
