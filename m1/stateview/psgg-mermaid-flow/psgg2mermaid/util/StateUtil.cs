using System;
using System.Collections;
using System.Collections.Generic;

namespace lib.util
{
    public class StateUtil
    {
        ///// <summary>
        ///// ステート名作成
        ///// 数字部分を１増やす
        ///// 無い場合は１が追加
        ///// </summary>
        //public static string MakeIncName(string name)
        //{
        //    var numstr = RegexUtil.Get1stMatch("[0-9]+$",name);
        //    if (!string.IsNullOrEmpty(numstr))
        //    {
        //        var prename = name.Substring(0,name.Length -  numstr.Length);

        //        var num = ParseUtil.ParseInt(numstr);
        //        num++;
        //        if (numstr.Length>1 && numstr[0]=='0')
        //        {
        //            return prename + num.ToString( "D" + numstr.Length.ToString() );
        //        }
        //        else
        //        {
        //            return prename + num.ToString();
        //        }
        //    }
        //    return name +"1";
        //}


        /// <summary>
        /// ステート名を作成
        /// 後部に "_ex"を追加
        /// </summary>
        public static string MakeExtraName(string name)
        {
            return name + "_ex";
        }
        public static bool IsValidStateName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return RegexUtil.Get1stMatch(@"[_a-zA-Z]+_[_a-zA-Z0-9]*", name) == name;
        }

        //public static Dictionary< string, Dictionary<string, string> > MakeImportIni(string s)
        //{
        //    try {
        //        var dicdic = new Dictionary<string, Dictionary<string, string>>();
        //        var ht = IniUtil.CreateHashtable(s);
        //        foreach(var cat in ht.Keys)
        //        {
        //            var ht2 = (Hashtable)ht[cat.ToString()];
        //            if (ht2==null) {
        //                Console.WriteLine("Unexpected! {857DCF0D-672B-42F4-80CF-B3A10B5A7A53}");
        //                continue;
        //            }
        //            var dic = new Dictionary<string,string>();
        //            foreach(var name in ht2.Keys)
        //            {
        //                var v = ht2[name];
        //                if (v==null) continue;
        //                dic.Add(name.ToString(),v.ToString());
        //            }
        //            dicdic.Add(cat.ToString(),dic);
        //        }
        //        return dicdic;
        //    } catch (SystemException e)
        //    {
        //        Console.WriteLine(e.Message + "Unexpected! {62E834D8-3FEE-4013-90B1-8C9AA0260286}");
        //    }
        //    return null;
        //}




    }
}
