using System.Collections;
using System.Collections.Generic;
using System;


namespace lib.util
{

    public class ArrayUtil
    {

        ///// <summary>
        ///// 配列の次の値を取得。ない場合は先頭から。
        ///// 重複した値がある場合は正しく動作しない
        ///// 該当下物がない場合最初の値を返す
        ///// </summary>
        //public static T GetNextCycril<T>(T[] list, T val)
        //{
        //    int i = Array.FindIndex(list, v => v.Equals(val));
        //    if (i < 0) return list[0];
        //    i = (i + 1) % list.Length;

        //    return list[i];
        //}

        //internal static string ToString<T>(T[] val)
        //{
        //    if (val == null) return "(null)";
        //    string s = null;
        //    Array.ForEach(val, i =>
        //    {
        //        if (s != null) s += ",";
        //        s += i.ToString();
        //    });
        //    return s;
        //}
        //internal static string ToString(object val)
        //{
        //    if (val == null) return "(null)";
        //    string s = null;

        //    try
        //    {
        //        int n = 0;
        //        foreach (var i in (Array)val)
        //        {
        //            if (s != null) s += ",";
        //            var d = i.ToString();
        //            if (d.Contains("\n"))
        //            {
        //                s += string.Format("\n\n[{0}]\n", n);
        //                s += d;
        //            }
        //            else
        //            {
        //                s += d;
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        s += "- unknown -";
        //    }
        //    return s;
        //}

        //public static bool IsValidIndex<T>(T[] list, int index)
        //{
        //    return list != null && index >= 0 && index < list.Length;
        //}
        //public static bool IsValidIndex<T>(T[,] list, int index0, int index1)
        //{
        //    return list != null && index0 >= 0 && index0 < list.GetLength(0) && index1 >= 0 && index1 < list.GetLength(1);
        //}
        //public static void Foreach<T>(T[,] array, Action<int, int> act)
        //{
        //    for (int x = 0; x < array.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < array.GetLength(1); y++)
        //        {
        //            act(x, y);
        //        }
        //    }
        //}
        //public static T? GetSafe<T>(T[,] array, int x, int y) where T : struct
        //{
        //    return IsValidIndex(array, x, y) ? (T?)array[x, y] : null;
        //}
        public static string GetVal(string[] array, int index, string error = null)
        {
            if (index >= 0 && index < array.Length)
            {
                return array[index];
            }
            return error;
        }
        //public static T GetVal<T>(T[] array, int index, T error = default(T))
        //{
        //    if (index >= 0 && index < array.Length)
        //    {
        //        return array[index];
        //    }
        //    return error;
        //}
    }

}