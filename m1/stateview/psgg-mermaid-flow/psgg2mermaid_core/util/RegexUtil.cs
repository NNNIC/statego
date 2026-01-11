using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace lib.util
{
    public class RegexUtil
    {
        public static readonly string VARNAME_PATTERN = @"[_a-zA-Z][_a-zA-Z0-9]+"; //変数名・関数名のパターン

        public static bool IsMatch(string regexstr, string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (string.IsNullOrEmpty(regexstr)) return false;
            try
            {
                return (new Regex(regexstr)).IsMatch(s);
            }
            catch
            {
                return false;
            }
        }

        public static string Get1stMatch(string regexstr, string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (string.IsNullOrEmpty(regexstr)) return string.Empty;
            var regex = new Regex(regexstr);
            var matches = regex.Matches(s);
            if (matches == null || matches.Count == 0) return string.Empty;

            foreach (var i in matches)
            {
                var m = (Match)i;
                return m.Value;
            }
            return string.Empty;
        }

        //public static Match Get1stMatchAsMatch(string regexstr, string s)
        //{
        //    if (string.IsNullOrEmpty(s)) return null;
        //    if (string.IsNullOrEmpty(regexstr)) return null;
        //    var regex   = new Regex(regexstr);
        //    var matches =  regex.Matches(s);
        //    if (matches == null || matches.Count==0) return null;

        //    foreach(var i in matches)
        //    {
        //        var m = (Match)i;
        //        return m;
        //    }
        //    return null;
        //}

        //public static string[] GetAllMatches(string regexstr, string s)
        //{
        //    if (string.IsNullOrEmpty(s)) return null;
        //    if (string.IsNullOrEmpty(regexstr)) return null;
        //    var regex   = new Regex(regexstr);
        //    var matches = regex.Matches(s);
        //    var list    = new List<string>();
        //    foreach(var i in matches)
        //    {
        //        var m   = (Match)i;
        //        list.Add(m.Value);
        //    }
        //    return list.ToArray();
        //}

        //public static string Replace1stMatch(string s, string regstr, string target)
        //{
        //    var regex = new Regex(regstr);
        //    var matches = regex.Matches(s);
        //    if (matches == null || matches.Count==0) return s;

        //    foreach(var i in matches)
        //    {
        //        var m = (Match)i;

        //        var pre  = s.Substring(0,m.Index);
        //        var post = s.Substring(m.Index + m.Length);

        //        return pre + target + post;
        //    }
        //    return s;
        //}

        public static string GetNthMatch(string regexstr, string s, int n)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (string.IsNullOrEmpty(regexstr)) return string.Empty;
            var regex = new Regex(regexstr);
            var matches = regex.Matches(s);
            if (matches == null || matches.Count == 0) return string.Empty;

            int c = 0;
            foreach (var i in matches)
            {
                c++;
                if (c == n)
                {
                    var m = (Match)i;
                    return m.Value;
                }
            }
            return string.Empty;
        }
    }
}