using System.Collections;
using System.Collections.Generic;

namespace lib.util
{
    public class ParseUtil
    {
        public static int ParseInt(string s, int errorvalue = int.MinValue)
        {
            if (string.IsNullOrEmpty(s)) return errorvalue;

            int ret;
            double retf;
            if (int.TryParse(s, out ret))
                return ret;

            if (double.TryParse(s, out retf)) //小数点対策
                return (int)retf;

            return errorvalue;
        }
        public static long ParseLong(string s, long errorvalue = long.MinValue)
        {
            long ret;
            double retf;
            if (long.TryParse(s, out ret))
                return ret;
            if (double.TryParse(s, out retf))
                return (long)retf;

            return errorvalue;
        }
        public static float ParseFloat(string s, float errorvalue = float.MinValue)
        {
            float ret;
            if (float.TryParse(s, out ret))
                return ret;

            return errorvalue;
        }
        public static List<float> ParseFloatList(string s, float errorvalue = float.MinValue)
        {
            if (string.IsNullOrEmpty(s)) return null;

            var list = new List<float>();
            var tokens = s.Split(',');
            foreach (var i in tokens)
            {
                var f = ParseFloat(i, errorvalue);
                list.Add(f);
            }
            return list;
        }
        public static List<int> ParseIntList(string s, int errorvalue = int.MinValue)
        {
            if (string.IsNullOrEmpty(s)) return null;

            var list = new List<int>();
            var tokens = s.Split(',');
            foreach (var i in tokens)
            {
                var v = ParseInt(i, errorvalue);
                list.Add(v);
            }
            return list;
        }
        //public static List<System.UInt64> ParseIntListUInt64(string s, System.UInt64 errorvalue = System.UInt64.MaxValue)
        //{
        //    if (string.IsNullOrEmpty(s)) return null;

        //    var list = new List<System.UInt64>();
        //    var tokens = s.Split(',');
        //    foreach (var i in tokens)
        //    {
        //        var v = default(System.UInt64);
        //        if (!System.UInt64.TryParse(s, out v))
        //        {
        //            v = errorvalue;
        //        }
        //        list.Add(v);
        //    }
        //    return list;
        //}



        public static bool ParseBool(string s, bool error)
        {
            if (string.IsNullOrEmpty(s)) return error;
            if (s.Trim().ToLower() == "true") return true;
            if (s.Trim().ToLower() == "false") return false;
            return error;
        }

    }
}