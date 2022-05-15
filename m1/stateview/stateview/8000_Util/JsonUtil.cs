using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

public class JsonUtil
{
    public static string Serialize(Object obj)
    {
        if (obj==null) return null;

        var type = obj.GetType();
        var bf = new DataContractJsonSerializer(type);
        using(var ms = new MemoryStream())
        {
            bf.WriteObject(ms,obj);
            var bin = ms.ToArray();
            return Encoding.UTF8.GetString(bin);
        }
    }
    public static string Serialize<T>(T obj)
    {
        if (obj==null) return null;

        var type = typeof(T);
        var bf = new DataContractJsonSerializer(type);
        using(var ms = new MemoryStream())
        {
            bf.WriteObject(ms,obj);
            var bin = ms.ToArray();
            return Encoding.UTF8.GetString(bin);
        }
    }



    public static Object Deserialize(string s, Type type)
    {
        if (s==null) return null;
        var bytes = Encoding.UTF8.GetBytes(s);
        using(var ms = new MemoryStream(bytes))
        {
            var bf = new DataContractJsonSerializer(type);
            return bf.ReadObject(ms);
        }
    }
}
