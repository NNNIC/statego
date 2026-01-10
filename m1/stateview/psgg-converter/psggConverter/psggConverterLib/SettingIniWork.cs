using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psggConverterLib
{
    public class SettingIniWork
    {
        static Hashtable m_ht;
        public static void Init(string s)
        {
            m_ht = IniUtil.CreateHashtable(s);
        }
        public static string Get(string category, string key)
        {
            return IniUtil.GetValueFromHashtable(category,key,m_ht);
        }
    }
}
