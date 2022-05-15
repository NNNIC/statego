using System.Collections;
using System.Collections.Generic;
using System;

public class ArrayUtil  {

    /// <summary>
    /// 配列の次の値を取得。ない場合は先頭から。
    /// 重複した値がある場合は正しく動作しない
    /// 該当下物がない場合最初の値を返す
    /// </summary>
    public static T GetNextCycril<T>(T[] list,  T val)
    {
        int i = Array.FindIndex(list,v=>v.Equals(val));
        if (i<0) return list[0];
        i = (i+1) % list.Length;
        
        return list[i];
    }

    internal static string ToString<T>(T[] val)
    {
        if (val==null) return "(null)";
        string s = null;
        Array.ForEach(val,i=>{
            if (s!=null) s+=",";
            s+=i.ToString();
        });
        return s;
    }
    internal static string ToString(object val)
    {
        if (val==null) return "(null)";
        string s = null;

        try {
            int n = 0;
            foreach(var i in (Array)val)
            {
                if (s!=null) s+=",";
                var d = i.ToString();
                if (d.Contains("\n"))
                {
                    s+= string.Format("\n\n[{0}]\n",n);
                    s+=d;
                }
                else 
                {
                    s+= d;
                }
            }
        }
        catch
        {
            s+="- unknown -";
        }
        return s;
    }

    public static bool IsValidIndex<T>(T[] list, int index)
    {
        return list!=null && index >= 0 && index <list.Length; 
    }
    public static bool IsValidIndex<T>(T[,] list, int index0, int index1)
    {
        return list!=null && index0 >= 0 && index0 <list.GetLength(0) && index1 >= 0 && index1 < list.GetLength(1); 
    }
    public static void Foreach<T>(T[,] array, Action<int,int> act )
	{
		for(int x = 0; x<array.GetLength(0); x++) 
		{
			for(int y = 0; y<array.GetLength(1); y++)
			{
				act(x,y);
			}
		}
	}
    public static T? GetSafe<T>(T[,] array, int x, int y) where T : struct
    {
        return IsValidIndex(array,x,y) ? (T?)array[x,y] : null;
    }
    public static T GetVal<T>(T[] array, int index, T error = default(T)) 
    {
        if (index >= 0 && index < array.Length)
        {
            return array[index];
        }
        return error;
    }
    public static bool IsEquals<T>(T[] a, T[] b) where T : struct
    {
        if (a==null && b ==null) return true;
        if (a.Length != b.Length) return false;
        for(var n = 0; n < a.Length; n++)
        {
            if (!a[n].Equals(b[n]))
            {
                return false;
            }
        }
        return true;
    } 
}

public class ListUtil
{
    /// <summary>
    /// 指定したリストを指定した比較関数でソートしたリストを返す。元のリストはソートしない
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="src"></param>
    /// <param name="comp"></param>
    /// <returns></returns>
    public static List<T> GetSortList<T>(List<T> src, System.Comparison<T> comp = null)
    {
        if (src == null)
            return null;
        if (src.Count <= 0)
            return src;
        List<T> dst = new List<T>();
        src.ForEach((l) =>
        {
            dst.Add(l);
        });

        if (comp == null)
            dst.Sort();
        else
            dst.Sort(comp);
        return dst;
    }

    internal static string ToString(object v)
    {
        if (v==null) return "(null)";
        try {
            string s = null;

            var enumerable = v as IEnumerable;
            if (enumerable!=null)
            {
                int n = 0;
                foreach(var i in enumerable)
                {
                    if (s!=null) s+=",";

                    var d = i.ToString();
                    if (d.Contains("\n"))
                    {
                        s+= string.Format("\n\n[{0}]\n",n);
                        s+=d;
                    }
                    else 
                    {
                        s+= d;
                    }

                    n++;
                }
            }
            else
            {
                return "-unknown-#1";
            }
            return s;

        }catch{
            return "-unknown-#2";
        }
    }

    public static bool IsValidIndex<T>(List<T> list, int index)
    {
        return list!=null && index >= 0 && index <list.Count; 
    }

    public static T GetNextValue<T>(List<T> list, T val)
    {
        var index = list.FindIndex(i=>i.Equals(val));
        if (index <0)
        {
            index =0;
        }
        else
        {
            index = (index + 1) % list.Count;
        }
        return list[index];
    }
    public static T GetPrevValue<T>(List<T> list, T val)
    {
        var index = list.FindIndex(i=>i.Equals(val));
        if (index <0)
        {
            index =0;
        }
        else
        {
            index = (list.Count + index - 1) % list.Count;
        }
        return list[index];
    }

    /// <summary>
    /// 強制的に値をセット。
    /// 空きエリアは default(T)で埋める
    /// </summary>
    public static void SetValForce<T>(ref List<T> list, int index, T val )
    {
        if (list==null) list = new List<T>();
        if (index < list.Count)
        {
            list[index] = val;
            return;
        }
        while(index >= list.Count)
        {
            list.Add(default(T));
        }
        if (index >= list.Count) throw new SystemException("{3CD30006-E0DD-4B5F-8332-1EE5634078C3}");
        list[index] = val;
    }
    public static void AddValIfNotExist<T>(ref List<T> list, T val)
    {
        if (list == null) throw new SystemException("{748FC483-D8B4-4746-B5A9-4854135865B0}");
        if (list.Contains(val)) return;
        list.Add(val);
    }

    public static T GetVal<T>(List<T> list, int index, T errval = default(T))
    {
        if (IsValidIndex(list,index))
        {
            return list[index];
        }
        return errval;
    }

    public static bool Remove<T>(ref List<T> list, T target)
    {
        if (list.Contains(target))
        {
            list.Remove(target);
            return true;
        }
        return false;
    }
    public static bool RemoveAt<T>(ref List<T> list, int index)
    {
        if (IsValidIndex(list,index))
        {
            list.RemoveAt(index);
            return true;
        }
        return false;
    }

    public static List<string> Clone(List<string> org)
    {
        if (org == null) return null;
        var result = new List<string>();
        org.ForEach(i=>result.Add(i));
        return org;
    }
    /// <summary>
    /// 順番関係なく要素が等しいか？
    /// </summary>
    public static bool IsEqual_wo_Order(List<string> a, List<string> b)
    {
        if (a==null && b==null) return true; // nullどおし、結果：等しい
        if (a==null || b==null) return false;// どちらかがnull、結果：異なる
        if (a.Count != b.Count) return false;// 要素数が異なる
        
        //それぞれクローン＆ソートして、調べる
        var a2 = Clone(a);
        var b2 = Clone(b);
        a2.Sort();
        b2.Sort();

        for(var n = 0; n < a2.Count; n++)
        {
            if (a2[n] != b2[n]) return false; //要素が異なる
        }
        return true; //ここまでくれば同じ
    }
}
