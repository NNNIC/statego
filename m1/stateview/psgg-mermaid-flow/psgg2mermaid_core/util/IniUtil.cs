using System.Collections;
using System.Collections.Generic;

using HT = System.Collections.Generic.Dictionary<string, object>;

namespace lib.util
{
    /// <summary>
    /// https://ja.wikipedia.org/wiki/INI%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB
    /// 
    /// 追加仕様
    /// 行を跨ぐ文字列取得。 
    /// 値に@@@を指定し、文字列を記入し、最後の行に@@@を指定
    /// hoge=@@@
    /// 文字列
    /// 文字列
    /// ：
    /// @@@
    /// 
    /// </summary>
    public class IniUtil
    {

        #region basic
        public static object GetValue(string key, string initext)
        {
            var ht = CreateHashtable(initext);
            return GetValueFromHashtable(key, ht);
        }
        public static string GetValue(string category, string key, string initext)
        {
            var ht = CreateHashtable(initext);
            return GetValueFromHashtable(category, key, ht);
        }
        public static object GetValueFromHashtable(string key, HT ht)
        {
            if (ht.ContainsKey(key))
            {
                return ht[key];
            }
            return null;
        }
        public static double GetDoubleFromHashtable(string key, HT ht, double error = -1)
        {
            var val = GetValueFromHashtable(key, ht);
            if (val == null) return error;
            var o = (double)0;
            if (double.TryParse(val.ToString(), out o))
            {
                return o;
            }
            return error;
        }
        public static string GetValueFromHashtable(string category, string key, HT ht)
        {
            if (ht != null && ht.ContainsKey(category))
            {
                var cateval = ht[category];
                if (cateval != null && (cateval is HT))
                {
                    var cathash = (HT)cateval;
                    if (cathash.ContainsKey(key))
                    {
                        return cathash[key].ToString();
                    }
                }
            }
            return null;
        }
        public static HT CreateHashtable(string initext)
        {
            if (string.IsNullOrEmpty(initext)) return null;

            HT mainhash = new HT();
            HT cathash = null;

            var lines = initext.Split('\n');

            //for(var n=0; n<lines.Length;n++)
            var n = -1;
            while (true)
            {
                n++;
                if (n >= lines.Length) break;

                var l = lines[n];
                if (string.IsNullOrEmpty(l) || string.IsNullOrEmpty(l.Trim()))
                {
                    continue;
                }
                if (l[0] == ';') continue;
                // check category
                if (l[0] == '[')
                {
                    if (l.Length <= 2)
                    {
                        continue;
                    }
                    var cindex = l.IndexOf(']', 1);
                    if (cindex < 0)
                    {
                        continue;
                    }
                    var category = l.Substring(1, cindex - 1);

                    cathash = new HT();
                    mainhash.Add(category, cathash);
                }


                // check key = value
                var eqindex = l.IndexOf('=');
                if (eqindex < 0)
                {
                    continue;
                }
                var key = l.Substring(0, eqindex).Trim();
                if (string.IsNullOrEmpty(key))
                {
                    continue;
                }

                if (eqindex + 2 > l.Length)
                {
                    continue;
                }
                var value = l.Substring(eqindex + 1).Trim();
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                if (value == "@@@")
                {
                    var value2 = string.Empty;
                    for (var n2 = n + 1; n2 < lines.Length; n2++)
                    {
                        var l2 = lines[n2];
                        l2 = l2.TrimEnd();
                        if (l2 == "@@@")
                        {
                            n = n2;
                            value = value2;
                            break;
                        }
                        if (!string.IsNullOrEmpty(value2))
                        {
                            value2 += System.Environment.NewLine;
                        }
                        value2 += l2.TrimEnd();
                    }
                }

                if (cathash != null)
                {
                    cathash.Add(key, value);
                }
                else
                {
                    mainhash.Add(key, value);
                }
            }
            return mainhash;
        }
        public static HT CreateHashtable(string initext, string category)
        {
            if (string.IsNullOrEmpty(initext)) return null;

            var ht = CreateHashtable(initext);
            if (ht != null && ht.ContainsKey(category))
            {
                var cateval = ht[category];
                if (cateval != null && (cateval is HT))
                {
                    var cathash = (HT)cateval;
                    return cathash;
                }
            }
            return null;
        }
        #endregion

        //使われていない。
        //public static T GetParsedValueFromHashtable<T>(string key, HT ht)
        //{
        //    var value = GetValueFromHashtable(key,ht);
        //    if (value==null) return default(T);

        //    var type = typeof(T);
        //    if (type == typeof(int))
        //    {
        //        int x=0;
        //        if (int.TryParse(value, out x))
        //        {
        //            return (T)((object)x);
        //        } 
        //        return default(T);            
        //    }
        //    if (type == typeof(float))
        //    {
        //        float x=0;
        //        if (float.TryParse(value, out x))
        //        {
        //            return (T)((object)x);
        //        } 
        //        return default(T);            
        //    }
        //    return default(T);
        //}

    }
}