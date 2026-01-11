package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class DictionaryUtil
{
    public static function Get<K, T>(dic:system.collections.generic.Dictionary<K, T>, key:K):T
    {
        if (dic != null && dic.ContainsKey(key))
        {
            return dic.GetValue_TKey(key);
        }
        return null;
    }
    public static function Set<K, T>(dic:system.collections.generic.Dictionary<K, T>, key:K, val:T):Void
    {
        if (dic.ContainsKey(key))
        {
            dic.SetValue_TKey(key, val);
        }
        else
        {
            dic.Add(key, val);
        }
    }
    public static function Remove<K, T>(dic:system.collections.generic.Dictionary<K, T>, key:K):Bool
    {
        if (dic.ContainsKey(key))
        {
            dic.Remove(key);
            return true;
        }
        return false;
    }
    public static function Clone<K, T>(dic:system.collections.generic.Dictionary<K, T>):system.collections.generic.Dictionary<K, T>
    {
        var dic2:system.collections.generic.Dictionary<K, T> = new system.collections.generic.Dictionary<K, T>();
        for (p in dic.GetEnumerator())
        {
            dic2.Add(p.Key, p.Value);
        }
        return dic2;
    }
    public function new()
    {
    }
}
