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

namespace stateview
{
    public class MsgWinUtil
    {
        public static void ShowMsg(string text, string cap, string oktext, Action cb)
        {
            _6200_msgboxForm.MsgBoxForm.Show(text, cap, oktext, cb);
        }
        public static void ShowMsg(string text, Action cb)
        {
            _6200_msgboxForm.MsgBoxForm.Show(text, "Message", "OK", cb);
        }

        public static void Show2btnMsg(string text, string cap, string oktext, string canceltext, Action<DialogResult> cb)
        {
            _6200_msgboxForm.MsgBox2btnForm.Show(text, cap, oktext, canceltext, cb);
        }
        public static void Show2btnMsg(string text, Action<DialogResult> cb)
        {
            _6200_msgboxForm.MsgBox2btnForm.Show(text, "Message", "OK", "CANCEL", cb);
        }
    }
}
