using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateViewer_starter2;

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
}
