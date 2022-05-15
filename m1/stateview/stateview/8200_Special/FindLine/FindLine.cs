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
    public class FindLine
    {
        public static string get_find_matched_lines()
        {
            try {
                var sm = new FindLineControl();
                sm.m_find_text = G.view_form.scintillaBoxTabFunc.Text; //sm.m_find_text = G.view_form.textBoxTabFunc.Text;
                sm.m_target_text = File.ReadAllText( G.gen_file, G.src_enc);
                sm.Run();
                if (sm.m_linenum>0)
                {
                    return (sm.m_linenum + 1).ToString();
                }
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected! Find Line {807774E2-3388-4869-868E-3AC6F85D87B2} " + e.Message);
            }
            return "?";
        }
    }
}
