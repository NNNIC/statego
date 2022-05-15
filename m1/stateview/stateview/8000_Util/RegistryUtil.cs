using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

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
            throw new SystemException("Unexpected! {64C42891-CEEC-4F3D-AD91-D82A17759834}");
        }
        regkey.SetValue(item,val);
    }
    public static string[] List(string key)
    {
        var regkey = Registry.CurrentUser.OpenSubKey(key,true);
        return regkey.GetSubKeyNames();
    }
}
