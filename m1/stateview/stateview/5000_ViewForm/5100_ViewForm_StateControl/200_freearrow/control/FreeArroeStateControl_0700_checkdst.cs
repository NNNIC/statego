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

public partial class FreeArrowStateControl  {

    stateview._5000_ViewForm.dialog.CheckDistForm  m_frm;
    void check_dst ()
    {
        if (m_new_desitination_state==null) return;

        if (m_frm==null)
        {
            m_frm = new stateview._5000_ViewForm.dialog.CheckDistForm();

            m_frm.Show(G.view_form);
            m_frm.Location = Cursor.Position; //G.view_form.PointToClient(Cursor.Position);
            //m_frm.textBox1.Text =
            //    m_save_branchInfo.m_branchpoint_state + ":" + m_save_branchInfo.m_branchpoint_branch_string + Environment.NewLine +
            //    "Change distination " + Environment.NewLine
            //    +" From "+ m_save_branchInfo.m_branchpoint_label +Environment.NewLine
            //    +" To  " + m_new_disitination_state;
            //m_frm.Location = G.main_picturebox.PointToClient(Cursor.Position);
        }
    }
    bool m_setdst;
    bool wait_checkdg_done() {
        if (m_new_desitination_state==null)
        {
            return true;
        }
        if (m_frm.DialogResult!= DialogResult.None)
        {
            m_setdst = m_frm.DialogResult == DialogResult.OK;
            m_frm = null;
            return true;
        }
        return false;
    }
}
