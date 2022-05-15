using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DictionaryUtil
{
    public static T Get<K,T>(Dictionary<K,T> dic, K key)
    {
        if (dic!=null && dic.ContainsKey(key))
        {
            return dic[key];
        }
        return default(T);
    }
    public static void Set<K,T>(Dictionary<K,T> dic, K key, T val )
    {
        if (dic.ContainsKey(key))
        {
            dic[key] = val;
        }
        else
        {
            dic.Add(key,val);
        }
    }
    public static bool Remove<K,T>(Dictionary<K,T> dic, K key)
    {
        if (dic.ContainsKey(key))
        {
            dic.Remove(key);
            return true;
        }
        return false;
    }

    public static Dictionary<K,T> Clone<K,T>(Dictionary<K,T> dic)
    {
        var dic2 = new Dictionary<K,T>();
        foreach(var p in dic)
        {
            dic2.Add(p.Key,p.Value);
        }
        return dic2;
    }

    /// <summary>
    /// Dictionaryを ini形式で出力する。
    /// Keyはソート
    /// </summary>
    public static string DicToIni<K,T>(Dictionary<K,T> dic)
    {
        if (dic==null) throw new SystemException("{1F389F8A-5900-4A46-BC5E-34D90545E69C}");
        if (dic.Count==0) return string.Empty;

        var keylist = new List<string>();
        foreach(var k in dic.Keys) keylist.Add(k.ToString()+"=dummy");
        keylist.Sort();

        //dicをhashtableにして、 iniのMakeOutputで出力させる
        var ht = new Hashtable();
        foreach(var k in dic.Keys) ht.Add(k,dic[k]);

        return IniUtil.MakeOutput_by_orderkey(ht,keylist,Environment.NewLine);
    }

    public static Hashtable DicToHashtable<K,T>(Dictionary<K,T> dic)
    {
        if (dic==null) throw new SystemException("{4BB268B3-B075-4FBD-A81B-B81F2F0456BD}");

        var ht =new Hashtable();
        foreach(var k in dic.Keys)
        {
            ht.Add(k,dic[k]);
        }

        return ht;

    }
}
