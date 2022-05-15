using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class RegexUtil
{
    public static bool IsMatch(string regexstr, string s)
    {
        if (string.IsNullOrEmpty(s)) return false;
        if (string.IsNullOrEmpty(regexstr)) return false;
        try {
            return (new Regex(regexstr)).IsMatch(s);
        } catch
        {
            return false;
        }
    }

    public static string Get1stMatch(string regexstr, string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        if (string.IsNullOrEmpty(regexstr)) return string.Empty;
        var regex   = new Regex(regexstr);
        var matches =  regex.Matches(s);
        if (matches == null || matches.Count==0) return string.Empty;

        foreach(var i in matches)
        {
            var m = (Match)i;
            return m.Value;
        }
        return string.Empty;
    }

    public static string[] GetAllMatches(string regexstr, string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        if (string.IsNullOrEmpty(regexstr)) return null;
        var regex   = new Regex(regexstr);
        var matches = regex.Matches(s);
        var list    = new List<string>();
        foreach(var i in matches)
        {
            var m   = (Match)i;
            list.Add(m.Value);
        }
        return list.ToArray();
    }

    public static string Replace1stMatch(string s, string regstr, string target)
    {
        var regex = new Regex(regstr);
        var matches = regex.Matches(s);
        if (matches == null || matches.Count==0) return s;

        foreach(var i in matches)
        {
            var m = (Match)i;

            var pre  = s.Substring(0,m.Index);
            var post = s.Substring(m.Index + m.Length);

            return pre + target + post;
        }
        return s;
    }
}