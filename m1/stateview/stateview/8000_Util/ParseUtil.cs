using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ParseUtil
{
    public static int ParseInt(string s,int error=int.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return error;

        var o = 0;
        if (int.TryParse(s,out o))
        {
            return o;
        }
        return error;
    }
    public static long ParseLongInt(string s,long error=long.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return error;

        var o = 0L;
        if (long.TryParse(s,out o))
        {
            return o;
        }
        return error;
    }
    public static float ParseFloat(string s, float error = float.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return error;

        var o = 0f;
        if (float.TryParse(s,out o))
        {
            return o;
        }
        return error;
    }
    public static double ParseDouble(string s, double error = double.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return error;
        var o = default(double);
        if (double.TryParse(s,out o))
        {
            return o;
        }
        return error;
    }

    /// <summary>
    /// 数字部分をパースして返す
    /// </summary>
    public static int ParseIntExtract(string s, int min, int max,int error=int.MinValue)
    {
        if (string.IsNullOrEmpty(s)) return error;

        var numstr = RegexUtil.Get1stMatch(@"[+\-]?[0-9]+",s);
        if (string.IsNullOrEmpty(numstr)) return error;

        var o = 0;
        if (int.TryParse(s,out o))
        {
            return MathX.Clamp(o,min,max);
        }
        return error;
    }

    public static int[] ParseIntArray(string s)
    {
        if (string.IsNullOrEmpty(s)) throw new SystemException("{F56F9B24-8AD7-4B3D-B6D8-8C71B6EDDC98}");
        var list = new List<string>( s.Split(','));
        // delete nothing tail
        for(var loop = 0; loop<=1E+10; loop++)
        {
            if (loop == 1E+10) throw new SystemException("{702294CC-1940-486C-8FC3-7FDDA4626394}");
            if (list.Count>0)
            {
                if (string.IsNullOrEmpty(list[list.Count-1]))
                { 
                    list.RemoveAt(list.Count-1);
                }
                else
                {
                    break;
                }
            }
        }
        var numlist = new List<int>();
        foreach(var i in list)
        {
            var num = ParseInt(i);
            if (num == int.MinValue)
            {
                throw new SystemException("{F56F9B24-8AD7-4B3D-B6D8-8C71B6EDDC98}");
            }
            numlist.Add(num);
        }
        return numlist.ToArray();
    }
    public static bool ParseBool(string s, bool error = false)
    {
        if (string.IsNullOrEmpty(s)) return error;
        bool o = false;
        if (bool.TryParse(s, out o))
        {
            return o;
        }
        return error;
    }
}
