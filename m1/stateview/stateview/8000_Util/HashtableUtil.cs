using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HashtableUtil
{
    public static string[] GetSortedKeys(Hashtable ht)
    {
        var list = new List<string>();
        foreach(var k in ht.Keys)
        {
            list.Add(k.ToString());
        }
        list.Sort();
        return list.ToArray();
    }

    public static void SetForce(Hashtable ht, object key, object val)
    {
        if (ht.ContainsKey(key))
        {
            ht[key] = val;
        }
        else
        {
            ht.Add(key,val);
        }
    }
}
