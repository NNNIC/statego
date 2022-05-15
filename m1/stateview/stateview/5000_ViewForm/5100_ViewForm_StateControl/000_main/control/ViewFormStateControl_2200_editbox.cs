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
using System.Diagnostics;

public partial class ViewFormStateControl {
    void editbox_show()
    {
        if (G.multiedit_control==null||G.multiedit_control.IsDone()) {
            G.multiedit_control = new stateview.MultiEditControl();
            G.multiedit_control.Open(m_focus_state,G.view_form);

            Console.WriteLine();
        }
    }
}
