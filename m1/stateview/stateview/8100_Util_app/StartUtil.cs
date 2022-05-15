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
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview
{
    public class StartUtil
    {
        /// <summary>
        /// restart用
        /// </summary>
        public static void OpenNewOrLoad()
        {
            var p = System.Diagnostics.Process.GetCurrentProcess();
            var cmd = p.MainModule.FileName;
            ExecUtil.execute_wo_args(cmd,Path.GetDirectoryName(G.load_file));
        }
    }
}
