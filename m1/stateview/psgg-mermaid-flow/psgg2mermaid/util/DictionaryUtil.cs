using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.util
{
    public class DictionaryUtil
    {
        public static T Get<K, T>(Dictionary<K, T> dic, K key)
        {
            if (dic != null && dic.ContainsKey(key))
            {
                return dic[key];
            }
            return default(T);
        }
        public static void Set<K, T>(Dictionary<K, T> dic, K key, T val)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = val;
            }
            else
            {
                dic.Add(key, val);
            }
        }
        public static bool Remove<K, T>(Dictionary<K, T> dic, K key)
        {
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
                return true;
            }
            return false;
        }

        public static Dictionary<K, T> Clone<K, T>(Dictionary<K, T> dic)
        {
            var dic2 = new Dictionary<K, T>();
            foreach (var p in dic)
            {
                dic2.Add(p.Key, p.Value);
            }
            return dic2;
        }

    }
}