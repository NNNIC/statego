using System;
using System.Collections;
using System.Collections.Generic;

namespace StateViewer_starter2 {

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
public partial class IniUtil {

    #region basic
    public static string GetValue(string key,string initext)
    {
        var ht = CreateHashtable(initext);
        return GetValueFromHashtable(key,ht);
    }
    public static string GetValue(string category, string key, string initext)
    {
        var ht = CreateHashtable(initext);
        return GetValueFromHashtable(category,key,ht);
    }
    public static string GetValueFromHashtable(string key, Hashtable ht)
    {
        if (ht!=null && ht.Contains(key))
        {
            return ht[key].ToString();
        }
        return null;
    }
    public static double GetDoubleFromHashtable(string key, Hashtable ht, double error = -1)
    {
        var val = GetValueFromHashtable(key,ht);
        if (val==null) return error;
        var o = (double)0;
        if (double.TryParse(val,out o))
        {
            return o;
        }
        return error;
    }
    public static string GetValueFromHashtable(string category, string key, Hashtable ht)
    {
        if (ht!=null && ht.ContainsKey(category))
        {
            var cateval = ht[category];
            if (cateval!=null && ( cateval is Hashtable))
            {
                var cathash = (Hashtable)cateval;
                if (cathash.ContainsKey(key))
                {
                    return cathash[key].ToString();
                }
            }
        }
        return null;        
    }
    public static void SetValueFromHashtable(string key, string val, ref Hashtable ht)
    {
        if (ht == null) ht = new Hashtable();
        ht[key] = val;
    }
    public static void SetValueFromHashtable(string category, string key, string val, ref Hashtable ht)
    {
        if (ht == null) ht = new Hashtable();

        Hashtable catht = null;
        if (ht.ContainsKey(category))
        {
            var p = ht[category];
            catht = p as Hashtable;
        }
        if (catht == null) catht = new Hashtable();

        catht[key] = val;

        ht[category] = catht;
    }
    public static void DelKey(string key, ref Hashtable ht)
    {
        if (ht == null) return;
        if (ht.ContainsKey(key))
        {
            ht.Remove(key);
        }
    }
    public static void DelKey(string category, string key, ref Hashtable ht)
    {
        if (ht == null) return;
        Hashtable catht = null;
        if (ht.ContainsKey(category))
        {
            var p = ht[category];
            catht = p as Hashtable;
        }
        if (catht == null) return;
        DelKey(key, ref catht);
        ht[category] = catht;
    }
    public static Hashtable CreateHashtable(string initext)
    {
        List<string> orderlist = null;
        return CreateHashtableWithOrderList(initext,out orderlist);
    }
    public static Hashtable CreateHashtableWithOrderList(string initext,out List<string> save_key_order)
    {
        save_key_order = new List<string>();   // key orderには、category=key で記録 categoryがない場合は =keyとなる　

        if (string.IsNullOrEmpty(initext)) return null;

        Hashtable mainhash = new Hashtable();
        Hashtable cathash  = null;

        var category = string.Empty;

        var lines = initext.Split('\n');
        for(var n=0; n<lines.Length;n++)
        {
            var l = lines[n];
            if (string.IsNullOrEmpty(l) || string.IsNullOrEmpty(l.Trim()))
            {
                save_key_order.Add(string.Empty);
                continue;
            }
            if (l[0]==';') {
                save_key_order.Add(l);
                continue;
            }
            // check category
            if (l[0]=='[')
            {
                if (l.Length<=2)
                {
                    continue;
                }                
                var cindex = l.IndexOf(']',1);
                if (cindex < 0)
                {
                    continue;
                }
                category = l.Substring(1,cindex - 1);
                
                cathash = new Hashtable();
                mainhash.Add(category,cathash);
            }


            // check key = value
            var eqindex = l.IndexOf('=');
            if (eqindex<0)
            {
                continue;
            }
            var key = l.Substring(0,eqindex).Trim();
            if (string.IsNullOrEmpty(key))
            {
                continue;
            }
            
            if (eqindex + 2 > l.Length)
            {
                continue;
            } 
            var value = l.Substring(eqindex+1).Trim();
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;//continue;
            }
            if (value=="@@@")
            {
                var value2 = string.Empty;
                for(var n2=n+1; n2<lines.Length;n2++)
                {
                    var l2 = lines[n2];
                    l2 = l2.TrimEnd();
                    if (l2=="@@@")
                    {
                        n = n2;
                        value = value2;
                        break;
                    }
                    if (!string.IsNullOrEmpty(value2))
                    {
                        value2+=System.Environment.NewLine;
                    }
                    value2+=l2.TrimEnd();
                }
            }

            if (cathash!=null)
            {
                cathash.Add(key,value);
            }
            else
            {
                mainhash.Add(key,value);
            }
            save_key_order.Add(category + "=" + key); // =でカテゴリとキーを分離　=を使う理由は、iniフォーマットでキー名に使わることがないから
        }
        return mainhash;
    }
    public static Hashtable CreateHashtable(string initext, string category)
    {
        if (string.IsNullOrEmpty(initext)) return null;

        var ht = CreateHashtable(initext);
        if (ht!=null && ht.ContainsKey(category))
        {
            var cateval = ht[category];
            if (cateval!=null && ( cateval is Hashtable))
            {
                var cathash = (Hashtable)cateval;
                return cathash;
            }
        }
        return null;
    }

    public static bool HasKeyFromHashtable(string key, Hashtable ht)
    {
        return (ht!=null && ht.ContainsKey(key));
    }
    public static bool HasKey(string key, string ini)
    {
        var ht = CreateHashtable(ini);
        return HasKeyFromHashtable(key,ht);
    }
    public static bool HasKeyFromHashtable(string category, string key, Hashtable ht)
    {
        if (ht!=null && ht.ContainsKey(category))
        {
            var ght = (Hashtable)ht[category];
            if (ght!=null)
            {
                return HasKeyFromHashtable(key,ght);
            }
        }
        return false;
    }
    public static bool HasKey(string category, string key, string ini)
    {
        var ht = CreateHashtable(ini);
        return HasKeyFromHashtable(category,key,ht);
    }
    #endregion

    public static T GetParsedValueFromHashtable<T>(string key, Hashtable ht)
    {
        var value = GetValueFromHashtable(key,ht);
        if (value==null) return default(T);

        var type = typeof(T);
        if (type == typeof(int))
        {
            int x=0;
            if (int.TryParse(value, out x))
            {
                return (T)((object)x);
            } 
            return default(T);            
        }
        if (type == typeof(float))
        {
            float x=0;
            if (float.TryParse(value, out x))
            {
                return (T)((object)x);
            } 
            return default(T);            
        }
        return default(T);
    }

    #region make output
    /// <summary>
    /// save_key_orderは、CreateHashtableWithOrderlistにて作成されたもの！
    /// </summary>
    public static string MakeOutput(Hashtable hash, List<string> save_key_order, string newlinechars=null)
    {
        if (newlinechars == null) newlinechars = Environment.NewLine;

        var sm = new IniUtilControl();
        sm.m_order_list = save_key_order;
        sm.m_hash = hash;
        sm.m_newlinechars = newlinechars;
        sm.Run();
        return sm.m_s;
    }
    public static string MakeOutput(Hashtable hash, string newlinechars = null)
    {
        var save_key_order = new List<string>();
        return MakeOutput(hash, save_key_order, newlinechars);
    }
    /// <summary>
    ///  orderkey_listは、keyのみのリスト。 MakeOuputを内部で呼出す。
    /// </summary>
    public static string MakeOutput_by_orderkey(Hashtable hash, List<string> orderkey_list, string newlinechars=null )
    {
        var nl = Environment.NewLine;
            
        var s = string.Empty;
        orderkey_list.ForEach(i=> s+= i + "=dummy" + nl );
        List<string> save_key_order = null;
        var temp = CreateHashtableWithOrderList(s,out save_key_order);

        return MakeOutput(hash,save_key_order);
    }
    #endregion

    public static bool Compare(Hashtable a, Hashtable b)
    {
        if (a==null && b==null) return true;
        if (a==null || b==null) return false;
        if (a.Keys.Count == 0 && b.Keys.Count==0) return true;

        var akeys = new List<string>(); foreach (var i in a.Keys) { akeys.Add(i.ToString()); }  akeys.Sort();
        var bkeys = new List<string>(); foreach (var i in b.Keys) { bkeys.Add(i.ToString()); }  bkeys.Sort();

        if (akeys.Count != bkeys.Count) return false;

        for (var n = 0; n<akeys.Count; n++) if (akeys[n] != bkeys[n]) return false;

        foreach(var k in akeys)
        {
            var va = a[k];
            var vb = b[k];
            if ( (va is Hashtable)  && (vb is Hashtable))
            {
                if (!Compare((Hashtable)va, (Hashtable)vb))
                {
                    return false;
                }
                continue;
            }
            if (va.ToString() != vb.ToString())
            {
                return false;
            }
        }
        return true;
    }
    public static bool Add(Hashtable hash, string cat, string key, string val)
    {
        if (!string.IsNullOrEmpty(cat))
        {
            var vcx = hash[cat];
            if (vcx != null)
            {
                if (vcx is Hashtable)
                {
                    var cathash = (Hashtable)vcx;
                    cathash[key] = val;
                    hash[cat] = cathash;
                    return true;
                }
                else
                {
                    //throw new SystemException("Unexpected! {9548E9ED-9BE3-40DD-A395-092DB9D94E5A}");
                    return false;
                }
            }
            else
            {
                var cathash = new Hashtable();
                cathash[key] = val;
                hash[cat] = cathash;
                return true;
            }
        }
        hash[key] = val;
        return true;
    }
    public static bool Del(Hashtable hash, string cat, string key)
    {
        if (!string.IsNullOrEmpty(cat))
        {
            var vcx = hash[cat];
            if (vcx != null)
            {
                if (vcx is Hashtable)
                {
                    var cathash = (Hashtable)vcx;
                    if (cathash.ContainsKey(key))
                    {
                        cathash.Remove(key);
                        if (cathash.Count == 0)
                        {
                            hash.Remove(cat);
                        }
                        else
                        {
                            hash[cat] = cathash;
                        }
                        return true;
                    }
                    return false;
                }
                else
                {
                    //throw new SystemException("Unexpected! {9548E9ED-9BE3-40DD-A395-092DB9D94E5A}");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        if (hash.ContainsKey(key))
        {
            hash.Remove(key);
            return true;
        }
        return false;
    }
    /// <summary>
    /// ini textに追加
    /// </summary>
    public static string Add(string s, string cat, string key, string val)
    {
        List<string> save_orders;
        var hash = CreateHashtableWithOrderList(s,out save_orders);
        Add(hash,cat,key,val);
        
        var newchar = StringUtil.FindNewLineChar(s);
        return MakeOutput(hash,save_orders,newchar);
    }

}
}