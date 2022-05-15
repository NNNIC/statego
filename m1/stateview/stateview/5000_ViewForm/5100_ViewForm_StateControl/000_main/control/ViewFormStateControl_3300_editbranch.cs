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

using stateview;


public partial class ViewFormStateControl {

    void statemenu_add_branch()
    {
        var state = m_focus_state;
        var list = BranchUtil.GetApiAndLabelListFromState(state);
        var addapi = G.brancApiCollector.EditClickInfo;
        if (!string.IsNullOrEmpty(addapi))
        {
            list.Add( new BranchUtil.APILABEL() { mode = BranchUtil.APIMODE.API, api = addapi, label = "?" }  );
        }
        string buf_branch;
        string buf_brcond;
        string buf_brcmt;
        BranchUtil.MakeBranchStringFromApiAndLabelList(list, out buf_branch, out buf_brcond, out buf_brcmt);

        G.excel_program.SetString(state, G.STATENAME_branch, buf_branch);
        G.excel_program.SetString(state, G.STATENAME_brcond, buf_brcond);

        stateview.History2.SaveForce_modify_value("Save the adding new branch.");

    }

    stateview._5000_ViewForm.dialog_editbranch.EditBranchForm m_ebform;

    void statemenu_editbranch_show()
    {
        m_ebform = new stateview._5000_ViewForm.dialog_editbranch.EditBranchForm();
        m_ebform.DialogResult = DialogResult.None;
        m_ebform.m_state = m_focus_state;
        
        m_ebform.Show(G.view_form);
    }

    bool wait_editbranch_done()
    {
        if (m_ebform.DialogResult == DialogResult.None) return false;
        if (m_ebform.DialogResult == DialogResult.OK)
        {
            //G.req_redraw();
            stateview.History2.SaveForce_modify_value("Edited the branch of the state.");
        }
        return true;
    }

    void br_isEdBrCncl(Action<int,bool> st)
    {
        if (m_ebform.DialogResult == DialogResult.Cancel)
        {
            SetNextState(st);
        }
    }
}
