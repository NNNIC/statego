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

public partial class ViewFormStateControl {
    void br_isOnSt(Action<int, bool> state)
    {
        if (HasNextState()) return;
        if (!string.IsNullOrEmpty(m_state_under_pointer))
        {
            SetNextState(state);
        }
    }
    //void br_isReqRedraw(Action<int,bool> state)
    //{
    //    if (m_isReqRedraw)
    //    {
    //        m_isReqRedraw = false;
    //        SetNextState(state);
    //    }
    //}
    //void br_isOnBranch(Action<int,bool> state)
    //{
    //    //if (m_branchpoint_state!=null)
    //    //{
    //    //    SetNextState(state);
    //    //}
    //}
    void br_isOnFcs(Action<int,bool> state)
    {
        if (HasNextState()) return;
        if (!string.IsNullOrEmpty(m_state_under_pointer) && m_state_under_pointer == m_focus_state)
        {
            //G.log += "OnFcs" + Environment.NewLine;
            SetNextState(state);
        }
    }
    void br_isFcsCnclNotOnSt(Action<int,bool> state)
    {
        if (HasNextState()) return;
        if (string.IsNullOrEmpty(m_state_under_pointer) &&  m_state_under_pointer != m_focus_state)
        {
            SetNextState(state);
        }
    }
    void br_isFcsCnclOnSt(Action<int,bool> state)
    {
        if (HasNextState()) return;
        if (!string.IsNullOrEmpty(m_state_under_pointer) && m_state_under_pointer != m_focus_state)
        {
            SetNextState(state);
        }
    }
    void br_isCntrlKeyDown(Action<int,bool> state)
    {
        if ( isCtrlKey() && m_ctrl_down_at_focus==false)
        {
            SetNextState(state);
        }
        m_ctrl_down_at_focus=false;
    }
    void br_isDrg(Action<int,bool> state)
    {
        //if ( !m_mup && !m_dclick)
        //{
        //    SetNextState(state);
        //}
        if (m_chkonfcsSc.m_result == CheckOnFocusStateControl.RESULT.DRAG)
        {
            if (G.option_mrb_enable)
            {
                if ((G.mouse_latest_button & MouseButtons.Right) == MouseButtons.Right)
                {
                    SetNextState(state);
                }
                if (!HasNextState())
                {
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        SetNextState(state);
                    }
                }
            }
            else
            { 
                SetNextState(state);
            }
        }
    }
    void br_isDClck(Action<int,bool> state)
    {
        //if (m_dclick)
        //{
        //    SetNextState(state);
        //}
        if (m_chkonfcsSc.m_result == CheckOnFocusStateControl.RESULT.DCLICK)
        {
            SetNextState(state);
        }
    }
    void br_isClick(Action<int,bool> state)
    {
        if (m_chkonfcsSc.m_result == CheckOnFocusStateControl.RESULT.CLICK)
        {
            SetNextState(state);
        }
    }
    void br_notAbove(Action<int,bool> state)
    {
        if (!HasNextState())
        {
            SetNextState(state);
        }
    }
    void br_isDrp(Action<int,bool> state)
    {
        if (G.mouse_event == stateview.MouseEventId.MOUSEUP)
        {
            SetNextState(state);
        }
    }
    void br_isDrgCncl(Action<int,bool> state)
    {
        if (!HasNextState())
        {
            SetNextState(state);
        }
    }
    void br_isEdCncl(Action<int,bool> state)
    {
        if (m_multieditcontrol_result == DialogResult.Cancel)
        {
            SetNextState(state);
        }
    }
    //void br_isDragBranch(Action<int,bool> state)
    //{
    //    //TBI
    //}
    void br_isMouseLeave(Action<int,bool> state)
    {
        //TBI
    }

    void br_isOnState(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.ONSTATE)
        {
            SetNextState(state);
        }
    }
    void br_isDcOnState(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.DC_ONSTATE)
        {
            SetNextState(state);
        }
    }
    void br_isReqRedraw(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.REQREDRAW)
        {
            SetNextState(state);
        }
    }
    void br_isReqCenterFocusState(Action<int, bool> state)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.REQCENTERFOCUSSTATE)
        {
            SetNextState(state);
        }
    }
    void br_isReqCenterFocusGroup(Action<int, bool> state)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.REQCENTERFOCUSGROUP)
        {
            SetNextState(state);
        }
    }
    void br_isDcOnBranch(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.DC_ONBRANCH)
        {
            SetNextState(state);
        }
    }
    void br_isDragBranch(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.DRAGBRANCH)
        {
            SetNextState(state);
        }
    }
    void br_isDragInSpace(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.DRAGINSPACE)
        {
            SetNextState(state);
        }
    }
    void br_Ok(Action<int,bool> state) {
        if (m_okCancel)
        {
            SetNextState(state);
        }
    }
    void br_Cancel(Action<int,bool> state) {
        if (!m_okCancel)
        {
            SetNextState(state);
        }
    }
    void br_ExistOneGF_state(Action<int, bool> state) {
        if (m_group_focus_list!=null && m_group_focus_list.Count==1)
        {
            if (!stateview.AltState.IsAltState(m_group_focus_list[0]))
            { 
                SetNextState(state);
            }
        }
    }
    void br_ExistOneGF_group(Action<int, bool> state) {
        if (m_group_focus_list!=null && m_group_focus_list.Count==1)
        {
            if (stateview.AltState.IsAltState(m_group_focus_list[0]))
            { 
                SetNextState(state);
            }
        }
    }
    void br_ExistMultiGF(Action<int, bool> state) {
        if (m_group_focus_list!=null && m_group_focus_list.Count>1)
        {
            SetNextState(state);
        }
    }
    void br_NotExistGF(Action<int,bool> state) {
        if (m_group_focus_list==null || m_group_focus_list.Count==0)
        {
            SetNextState(state);
        }
    }
    void br_isDrgDG(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_chkonfcsGFSc!=null && m_chkonfcsGFSc.m_result== ChkOnFcsGFStateControl.RESULT.DRAG)
        {
            if (G.option_mrb_enable)
            {
                if ((G.mouse_latest_button & MouseButtons.Right) == MouseButtons.Right)
                {
                    SetNextState(state);
                }
                if (!HasNextState())
                {
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        SetNextState(state);
                    }
                }
            }
            else
            { 
                SetNextState(state);
            }
        }
    }
    void br_isCtrlClickDG(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_chkonfcsGFSc!=null && m_chkonfcsGFSc.m_result== ChkOnFcsGFStateControl.RESULT.CLICK && (Control.ModifierKeys & Keys.Control) == Keys.Control)
        {
            SetNextState(state);
        }
    }
    void br_isClickDG(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_chkonfcsGFSc!=null && m_chkonfcsGFSc.m_result== ChkOnFcsGFStateControl.RESULT.CLICK)
        {
            SetNextState(state);
        }
    }
    void br_isCtrlClickOnStateDG(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_state_under_pointer!=null && (Control.ModifierKeys & Keys.Control) == Keys.Control)
        {
            SetNextState(state);
        }
    }
    void br_isHoldMBD(Action<int,bool> state) {
        if (HasNextState()) return;
        if (m_idleSc!=null && m_idleSc.m_result == IdleStateControl.RESULT.HOLD_MBD)
        {
            SetNextState(state);
        }

    }
    void br_openContextMenu(Action<int,bool> state)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.open_contextmenu)
        {
            clearKeyexec();
            SetNextState(state);
        }
    }
    bool hasKeyexec()
    {
        return m_keyexec != stateview.KEYEXEC.none;
    }
    void clearKeyexec()
    {
        m_keyexec = stateview.KEYEXEC.none;
    }
    void br_hasKeyExec(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (hasKeyexec())
        {
            SetNextState(st);
        }
    }
    void br_keyPate(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (
            m_keyexec == stateview.KEYEXEC.paste_wo_outflow
            ||
            m_keyexec == stateview.KEYEXEC.paste_w_ouflow
        )
        {
            SetNextState(st);
        }
    }
    void br_keyHistoryBack(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.history_back)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyHistoryForward(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.history_forward)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyFocusTrackBack(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.focustrack_back)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyFocusTrackForward(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.focustrack_forward)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyFocusState(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (
            m_keyexec == stateview.KEYEXEC.focus_home
            ||
            m_keyexec == stateview.KEYEXEC.focus_end
            ||
            m_keyexec == stateview.KEYEXEC.focus_specified_state
            )
        {
            clearKeyexec();

            if (m_center_focus_state!=null)
            {
                SetNextState(st);
            }
        }
    }
    void br_keyFocusGroupNode(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (
            m_keyexec == stateview.KEYEXEC.focus_home
            ||
            m_keyexec == stateview.KEYEXEC.focus_end
            ||
            m_keyexec == stateview.KEYEXEC.focus_specified_state
            )
        {
            if (m_center_focus_group!=null)
            {
                clearKeyexec();
                SetNextState(st);
            }
        }
    }
    void br_keyFocusAll(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.focus_all)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyDelete(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.delete_states)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_openInOutMenu(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.open_inout_menu)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyFocusClear(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.focus_clear)
        {
            clearKeyexec();
            SetNextState(st);
        }

    }
    void br_keyEnterGroup(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.enter_group)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_keyLeaveGroup(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_keyexec == stateview.KEYEXEC.leave_group)
        {
            clearKeyexec();
            SetNextState(st);
        }
    }
    void br_open_if_mbr(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (G.option_mrb_enable)
        {
            if (G.mouse_latest_button == MouseButtons.Right)
            {
                SetNextState(st);
            }
        }
        else
        {
            SetNextState(st);
        }
    }
    void br_mbroption_if_mbr(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (G.option_mrb_enable)
        {
            if (G.mouse_latest_button == MouseButtons.Right)
            {
                SetNextState(st);
            }
        }
        else
        {
            SetNextState(st);
        }
    }
    void br_mbroption_if_mbl(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (G.option_mrb_enable)
        {
            if (G.mouse_latest_button == MouseButtons.Left)
            {
                SetNextState(st);
            }
        }
        else
        {
            SetNextState(st);
        }
    }
}
