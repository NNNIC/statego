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
    public class GroupNameUtil
    {
        public static bool IsOkNameForRename(string groupname, string orgname=null)
        {
            bool? bOk = null;

            if (RegexUtil.Get1stMatch("[0-9a-zA-Z_]+",groupname) != groupname) bOk = false;
            if (bOk==null)
            {
                if (!string.IsNullOrEmpty(orgname) &&  groupname == orgname) bOk = true;
            }
            if (bOk==null)
            {
                bOk = !G.node_get_groupsOnTargetDir().Contains(groupname);
            }

            return (bOk==true);
        }

        public static bool IsOkNameForNewOrAdd(string groupname)
        {
            if (!string.IsNullOrEmpty(groupname))
            {
                bool? bOk = null;

                if (RegexUtil.Get1stMatch("[0-9a-zA-Z_]+", groupname) == groupname) bOk = true;

                return (bOk == true);
            }
            return false;
        }

    }
}
