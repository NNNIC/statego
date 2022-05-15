using System.Collections;
using System;

public static class EnumUtil
{
    public static T GetNextCycric<T>(T n)
    {
        var list = Enum.GetNames(typeof(T));
        int idx = Array.FindIndex(list,i=>i==n.ToString());
        idx = (idx + 1) % list.Length;
        return (T)Enum.Parse(typeof(T),list[idx]);
    }

    //パースしてEnum値取得。無い場合は noneを返却
    public static T Parse<T>(string s, T none)
    {
        if ( Array.IndexOf(Enum.GetNames(typeof(T)),s)<0)
        {
            return none;
        }
        return (T)Enum.Parse(typeof(T),s);
    }

}
