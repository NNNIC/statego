using System.Collections;
using System.Collections.Generic;

namespace lib.util
{
    public class CsvUtil
    {
        public static string MakeALine<T>(List<T> data)
        {
            var s = string.Empty;
            if (data != null) foreach (var d in data)
                {
                    var p = d.ToString();
                    if (p.Contains(",")) p = "\"" + p + "\"";
                    if (!string.IsNullOrEmpty(s)) s += ",";
                    s += p;
                }
            return s;
        }
        public static List<int> GetALine(string s, object error = null)
        {
            if (error != null)
            {
                return ParseUtil.ParseIntList(s, (int)error);
            }
            return ParseUtil.ParseIntList(s);
        }
        //public static List<System.UInt64> GetALineUInt64(string s, System.UInt64 error = System.UInt64.MaxValue)
        //{
        //    return ParseUtil.ParseIntListUInt64(s, error);
        //}

        public static List<string> GetALineString(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            var list = new List<string>();

            var tokens = s.Split(',');
            foreach (var t in tokens)
            {
                var t1 = t.Trim();
                list.Add(t1);
            }

            return list;
        }

        public static string Get(string s, int index)
        {
            if (string.IsNullOrEmpty(s)) return null;
            if (index < 0) return null;

            var tokens = s.Split(',');
            if (tokens != null && index < tokens.Length)
            {
                return tokens[index];
            }
            return null;
        }

        /// <summary>
        /// 指定インデックス以降をすべて取得。カンマが入ってもよい
        /// </summary>
        /// <returns></returns>
        public static string GetAllRest(string s, int index)
        {
            if (index < 0) return null;
            if (index == 0) return s;

            int cmidx = 0;
            int count_index = 0;
            while (count_index < index)
            {
                cmidx = s.IndexOf(',', cmidx);
                if (cmidx < 0) return null;
                cmidx++;
                count_index++;
                if (cmidx >= s.Length) return null;
            }
            return s.Substring(cmidx);
        }
    }
}