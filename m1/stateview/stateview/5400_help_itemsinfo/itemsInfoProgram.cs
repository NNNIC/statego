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

namespace stateview
{
    public class itemsInfoProgram
    {
        private ItemEditControl.Info m_info;

        private void Load()
        {
            m_info = new ItemEditControl.Info();
            m_info.load();
        }

        public bool IsValid(string state, string itemname)
        {
            if (m_info == null) Load();

            var state_head = RegexUtil.Get1stMatch(@".+?_",state);
            if (string.IsNullOrEmpty(state_head)) return false;

            var sx = m_info.STATELOC_get(state_head);
            if (string.IsNullOrEmpty(sx)) return false;

            var sarg = m_info.get_sarg_errorrecover(itemname);
            if (RegexUtil.Get1stMatch(sarg,sx)==sx)
            {
                return true;
            }

            return false;
        }

        public string GetMethod(string itemname)
        {
            if (m_info == null) Load();
            var s = m_info.METHOD_get(itemname);
            if ( s== null) s = string.Empty;
            return s;
        }

    }
}
