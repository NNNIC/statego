//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview {

    public class helpProgram2
    {
        //Hashtable m_ht {
        //    get {
        //        if (__ht==null)
        //        {
        //            __ht = IniUtil.CreateHashtable(G.excel_convertsettings.m_help_ini);
        //        }
        //        return __ht;
        //    }
        //}
        //Hashtable __ht;

        string system_lang { get { return G.system_lang; } }

        public string GetHelp(string name)
        {
            //return IniUtil.GetValueFromHashtable(name,system_lang,m_ht);

            return IniUtil.GetValue(name,system_lang,G.excel_convertsettings.m_help_ini);
        }
        public string GetHelp(string name, bool j_or_e)
        {
            var lang = j_or_e ? "jpn" :"en";
            var s = IniUtil.GetValue(name,lang,G.excel_convertsettings.m_help_ini);
            if (s==null) s = string.Empty;
            return s;
        }
    }
}
