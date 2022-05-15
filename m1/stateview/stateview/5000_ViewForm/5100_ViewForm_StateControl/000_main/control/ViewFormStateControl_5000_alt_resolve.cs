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

// ソースとディスティネーションがALT STATE時の解決用
public partial class ViewFormStateControl {

    private void br_is_dst_altstate(Action<int,bool> state) {
        if (!HasNextState())
        { 
            if (m_freeArrowSc.m_result == FreeArrowStateControl.RESULT.dst_is_altstate)
            {
                SetNextState(state);
            }
        }
    }
    private void br_is_src_altstate(Action<int,bool> state)
    {
        if (!HasNextState())
        { 
            if (
                m_freeArrowSc.m_result != FreeArrowStateControl.RESULT.cancel 
                && 
                m_freeArrowSc.m_result != FreeArrowStateControl.RESULT.none
                )
            { 
                if (AltState.IsAltState(m_save_branchInfo.m_branchpoint_state))
                {
                    SetNextState(state);
                }
            }
        }
    }
    stateview._5000_ViewForm.dialog.AltStateDistinationForm1 m_altdstform;
    private void altstate_dst_dialog_open()
    {
        m_altdstform = new stateview._5000_ViewForm.dialog.AltStateDistinationForm1();
        m_altdstform.DialogResult = DialogResult.None;
        m_altdstform.m_parent = this;
        m_altdstform.m_dstpath = GroupNodeUtil.pathcombine(G.node_get_cur_dirpath(), AltState.TrimAltStateName(m_freeArrowSc.m_new_desitination_state));
        m_altdstform.Show(G.view_form);
    }
    private bool altstate_dst_dialog_done()
    {
        return m_altdstform.DialogResult!=DialogResult.None;
    }
    private bool altstate_dst_dialog_ok()
    {
        return m_altdstform.DialogResult == DialogResult.OK;
    }
    stateview._5000_ViewForm.dialog.AltStateSourceForm1 m_altsrcform;
    private void altstate_src_dialog_open()
    {
        m_altsrcform = new stateview._5000_ViewForm.dialog.AltStateSourceForm1();
        m_altsrcform.DialogResult = DialogResult.None;
        m_altsrcform.m_dstination = m_freeArrowSc.m_new_desitination_state;
        m_altsrcform.m_dirpath =  GroupNodeUtil.pathcombine(G.node_get_cur_dirpath(),AltState.TrimAltStateName(m_save_branchInfo.m_branchpoint_state));
        m_altsrcform.Show(G.view_form);

    }
    private bool altstate_src_dialog_done()
    {
        return m_altsrcform.DialogResult!=DialogResult.None;
    }
    private bool altstate_src_dialog_ok()
    {
        return m_altsrcform.DialogResult== DialogResult.OK;
    }
    private void altsrcbi_to_srcbi()
    {
        m_save_branchInfo = m_altsrcform.m_resultbi;
    }
}   