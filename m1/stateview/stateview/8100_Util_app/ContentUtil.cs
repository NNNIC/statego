using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    public class ContentUtil
    {

        public static string Delete_prefix_eachline(string prefix, string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var newlinechar = StringUtil.FindNewLineChar(s);
            if (!string.IsNullOrEmpty(newlinechar))
            {
                var lines = s.Split(newlinechar.ToArray());
                var s2 = string.Empty;
                foreach(var l in lines)
                {
                    if (string.IsNullOrEmpty(l)) continue;
                    if (!string.IsNullOrEmpty(s2)) s2 += newlinechar;
                    if (l.StartsWith(prefix))
                    {
                        s2 += l.Substring(prefix.Length);
                    }
                    else
                    {
                        s2 += l;
                           
                    }
                }
                return s2;
            }
            else
            {
                if (s.StartsWith(prefix)) return s.Substring(prefix.Length);
            }
            return s;
        }
        public static string Delete_regex_eachline(string regex, string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var newlinechar = StringUtil.FindNewLineChar(s);
            if (!string.IsNullOrEmpty(newlinechar))
            {
                var lines = s.Split(newlinechar.ToArray());
                var s2 = string.Empty;
                foreach(var l in lines)
                {
                    if (string.IsNullOrEmpty(l)) continue;
                    if (!string.IsNullOrEmpty(s2)) s2 += newlinechar;

                    var l2 = RegexUtil.Replace1stMatch(l,regex,"");

                    s2 += l2;
                }
                return s2;
            }
            else
            {
                return RegexUtil.Replace1stMatch(s,regex,"");
            }
        }
    }
}
