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
using stateview;

public partial class ViewFormStateControl {

    stateview._5300_EditForm.EditForm_nextForm3 m_nextform;

    void branchdlg_open()
    {
        m_nextform = new stateview._5300_EditForm.EditForm_nextForm3();
        m_nextform.m_brinfo = m_branchInfo;
        m_nextform.m_text = m_branchInfo.m_branchpoint_label;
        
        m_nextform.Show(G.view_form);
    }

    void branchdlg_clear()
    {
        if (m_nextform.DialogResult == DialogResult.OK)
        {
            if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==1/*true*/)
            {
                var s = G.excel_program.GetString(m_branchInfo.m_branchpoint_state,/*"nextstate"*/G.STATENAME_nextstate);
                if (s!=m_nextform.m_text)
                {
                    G.excel_program.SetString(m_branchInfo.m_branchpoint_state,/*"nextstate"*/G.STATENAME_nextstate,m_nextform.m_text);
                    G.req_redraw();

                    History2.SaveForce_modify_value("Changed an arrow direction");
                }
            }
            else if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==2/*true*/)
            {
                var s = G.excel_program.GetString(m_branchInfo.m_branchpoint_state,"branch");
                var news = DTBranchUtil.SetLebel(s,(int)m_branchInfo.m_branchpoint_branch_index,m_nextform.m_text);

                if (s!=news)
                {
                    G.excel_program.SetString(m_branchInfo.m_branchpoint_state,"branch",news);
                    G.req_redraw();
                    History2.SaveForce_modify_value("Changed an arrow direction");
                }
            }
            else // 3:gosub
            {
                var s = G.excel_program.GetString(m_branchInfo.m_branchpoint_state,/*"nextstate"*/G.STATENAME_gosubstate);
                if (s!=m_nextform.m_text)
                {
                    var val = m_nextform.m_text;
                    if (string.IsNullOrEmpty(val) && G.excel_program.IsMandatoryGosub(m_branchInfo.m_branchpoint_state))
                    {
                        val = "?";
                    }
                    G.excel_program.SetString(m_branchInfo.m_branchpoint_state,/*"nextstate"*/G.STATENAME_gosubstate,val);
                    G.req_redraw();

                    History2.SaveForce_modify_value("Changed an arrow direction");
                }
            }
        }
        m_nextform.Dispose();
        m_nextform = null;
        m_branchInfo.Clear();
    }
    void br_checkStateForBranchEdit(Action<int,bool> st)
    {
        if (m_branchInfo!=null && !AltState.IsAltState( m_branchInfo.m_branchpoint_state ) && StateUtil.IsValidStateName(m_branchInfo.m_branchpoint_state))
        {
            SetNextState(st);
        }
    }
}
