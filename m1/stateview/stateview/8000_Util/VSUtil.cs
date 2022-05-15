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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>

/// <summary>
/// VisualStudio関連
/// </summary>
public class VSUtil
{
    public static string FindSln()
    {
        var bTimeout = false;
        var s = PathUtil.FindTraverseDownAndUp( G.gen_dir, "*.sln",1000,out bTimeout);
        if (bTimeout) return string.Empty;
        return s;
    }
}

