using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CsvUtil
{
    /*
        csv format 
        1行目はヘッダ。 id, hoge1,hoge2,--- と並ぶ
        id はユニーク
    */
    public static Dictionary<string, string[]> CreateDictionary(string csvtext)
    {
        var dic = new Dictionary<string,string[]>();
        var lines = csvtext.Split('\n');
        foreach(var l in lines)
        {
            var s = l.Trim();
            var tokens = s.Split(',');
            var items = new List<string>();
            foreach(var i in tokens)
            {
                var p = string.Empty;
                if (i != null)
                {
                    p = i.Trim();
                }
                items.Add(p);
            }

            if (items.Count==0) continue;

            var key = items[0];
            if (string.IsNullOrEmpty(key)) continue;

            items.RemoveAt(0);
            //先頭と末尾の""を消す
            for(var i = 0; i<items.Count; i++)
            {
                var v = items[i];
                if (string.IsNullOrEmpty(v)) continue;
                if (v[0]=='\"') v = v.Substring(1);
                if (string.IsNullOrEmpty(v)) continue;
                if (v[v.Length-1] == '\"') v = v.Substring(0,v.Length-1);
                items[i] = v;
            }

            var val = items.ToArray();

            dic.Add(key,val);
        }
        return dic;
    }
    public static string Get(Dictionary<string,string[]> dic, string id, string column_name)
    {
        if (!dic.ContainsKey("id")) return null;
        var header = dic["id"];
        var idx = Array.IndexOf(header, column_name);
        if (idx < 0) return null;
        
        if (!dic.ContainsKey(id)) return null;
        var list = dic[id];
        if (list.Length <= idx) return null;
        return list[idx];
    }
    public static string ToCSV<T>(List<T> list)
    {
        string s = null;
        if (list == null) throw new SystemException("{8D91E40B-BD34-433D-A2C4-8D53BE9FD7A8}");
        list.ForEach(i=> {
            if (s!=null) s+=",";
            if (i==null)
            {
                s += "";
            }
            else
            { 
                s += i.ToString();
            }
        });
        return s;
    }
    public static int[] ToIntList(string s, int[] err = null)
    {
        if (string.IsNullOrEmpty(s)) return err;
        var tokens = s.Split(',');
        var list = new List<int>();
        foreach (var v in tokens)
        {
            var a = 0;
            if (int.TryParse(v, out a))
            {
                list.Add(a);
            }
            else
            {
                return err;
            }
        }
        return list.ToArray();
    }
}

