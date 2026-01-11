using System.Collections;
using System.Collections.Generic;


namespace lib.util
{
    public class ListUtil
    {
        /// <summary>
        /// 指定したリストを指定した比較関数でソートしたリストを返す。元のリストはソートしない
        /// </summary>
        //public static List<T> GetSortList<T>(List<T> src, System.Comparison<T> comp = null)
        //{
        //    if (src == null)
        //        return null;
        //    if (src.Count <= 0)
        //        return src;
        //    List<T> dst = new List<T>();
        //    src.ForEach((l) =>
        //    {
        //        dst.Add(l);
        //    });

        //    if (comp == null)
        //        dst.Sort();
        //    else
        //        dst.Sort(comp);
        //    return dst;
        //}

        //internal static string ToString(object v)
        //{
        //    if (v == null) return "(null)";
        //    try
        //    {
        //        string s = null;

        //        var enumerable = v as IEnumerable;
        //        if (enumerable != null)
        //        {
        //            int n = 0;
        //            foreach (var i in enumerable)
        //            {
        //                if (s != null) s += ",";

        //                var d = i.ToString();
        //                if (d.Contains("\n"))
        //                {
        //                    s += string.Format("\n\n[{0}]\n", n);
        //                    s += d;
        //                }
        //                else
        //                {
        //                    s += d;
        //                }

        //                n++;
        //            }
        //        }
        //        else
        //        {
        //            return "-unknown-#1";
        //        }
        //        return s;

        //    }
        //    catch
        //    {
        //        return "-unknown-#2";
        //    }
        //}

        public static bool IsValidIndex<T>(List<T> list, int index)
        {
            return list != null && index >= 0 && index < list.Count;
        }

        //public static T Get<T>(List<T> list, int i) where T : class
        //{
        //    if (IsValidIndex(list, i))
        //    {
        //        return list[i];
        //    }
        //    return null;
        //}

        public static string Get(List<string> list, int i)
        {
            if (IsValidIndex(list, i))
            {
                return list[i];
            }
            return null;
        }

        //public static T GetVal<T>(List<T> list, int i, T errorval) where T : struct
        //{
        //    if (IsValidIndex(list, i))
        //    {
        //        return list[i];
        //    }
        //    return errorval;
        //}

        //public static List<T> GetNotUsedList<T>(List<T> alllist, List<T> usedlist) where T : struct
        //{
        //    var list = new List<T>();
        //    foreach (var v in alllist)
        //    {
        //        var index = usedlist.FindIndex(i => i.Equals(v));
        //        if (index < 0)
        //        {
        //            list.Add(v);
        //        }
        //    }
        //    return list;
        //}

        //public static List<int> CreateIntList(int start, int goal)
        //{
        //    var list = new List<int>();
        //    for (var i = start; i <= goal; i++)
        //    {
        //        list.Add(i);
        //    }
        //    return list;
        //}

    }
}