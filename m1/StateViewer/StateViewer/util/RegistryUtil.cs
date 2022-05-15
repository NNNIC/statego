using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace PSGGEditor
{

public class RegistryUtil
{
    public static string GetStr(string key,string item)
    {
        var v = GetObj(key,item);
        if (v!=null) return (string)v;
        return null;
    }
    public static void SetStr(string key,string item, string val)
    {
        SetObj(key,item,val);
    }
    public static int GetInt(string key, string item, int error = int.MinValue)
    {
        var v = GetObj(key,item);
        if (v!=null) return (int)v;
        return error;
    }
    public static void SetInt(string key, string item, int val)
    {
        SetObj(key,item,val);
    }
    public static object GetObj(string key,string item)
    {
        var regkey = Registry.CurrentUser.OpenSubKey(key,false);
        if (regkey==null) return null;

        var val = regkey.GetValue(item);
        return val;
    }
    public static void SetObj(string key,string item, object val)
    {
        var regkey = Registry.CurrentUser.OpenSubKey(key,true);
        if (regkey == null)
        {
            regkey = Registry.CurrentUser.CreateSubKey(key);
        }
        if (regkey == null)
        {
            throw new SystemException("Unexpected! {BB4C115B-99A1-438D-89F3-DE89DFD94F0D}");
        }
        regkey.SetValue(item,val);
    }

}
}