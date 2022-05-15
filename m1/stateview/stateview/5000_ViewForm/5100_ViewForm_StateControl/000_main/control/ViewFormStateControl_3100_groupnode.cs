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

// 2018.4.28
// 注意
//     本ファイルは他の分類方法とは異なり、Group Nodeに関連する部分を集約しています。
//     ・・・命令カテゴリによるファイル分割手法は、ステート数が増えると管理不可能だと感じた。

public partial class ViewFormStateControl {

    //string m_focus_groupnode;
    //string m_refstate_groupnode; //グループ描画のため参照したステート名

    //at S_IDLE2
    void br_isOnGroupNode(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.ONGROUPNODE)
        {
            SetNextState(st);
        }
    }
    //at S_FocusGroupNode
    void focus_set_groupnode()
    {
        if (!AltState.IsAltState(m_state_under_pointer))
        {
            G.NoticeToUser_warning("Unexpected! {4BD6EAD8-B646-4037-A033-8B22A06ADA87}");
            return;
        }
        m_groupnode_name = AltState.TrimAltStateName(m_state_under_pointer);          //G.node_get_groupname(m_state_under_pointer);
    }
    void focus_draw_groupnode()
    {
        //m_refstate_groupnode = DrawBenri.draw_groupnode_focus(m_groupnode_name);
        DrawBenri.draw_groupnode_focus(m_groupnode_name);
        G.main_picturebox.Refresh();
    }
    //at S_CheckMouseGN
    void subsc_chkonfcsGN_init()
    {
        m_chkonfcsGNSc.Init();
    }
    void subsc_chkonfcsGN_update()
    {
        m_chkonfcsGNSc.Update();
    }
    bool subsc_chkonfcsGN_done()
    {
        return m_chkonfcsGNSc.IsDone();
    }
    void br_isDrgGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.DRAG)
        {
            if (!string.IsNullOrEmpty(m_groupnode_name) &&  m_save_groupnode_name == m_groupnode_name)
            { 
                if (G.option_mrb_enable)
                {
                    if ((G.mouse_latest_button & MouseButtons.Right) == MouseButtons.Right)
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
    }
    void br_isDrgOtherGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.DRAG)
        {
            if (!string.IsNullOrEmpty(m_groupnode_name) &&  m_save_groupnode_name != m_groupnode_name)
            { 
                SetNextState(st);
            }
        }
    }
    void br_isClickStateGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.CLICK)
        {

            var pos = GetPointerOnMainBmp();
            var rect = G.get_draw_wp_frame(m_altstate_name);
            if (rect != null && ((RectangleF)rect).Contains(pos))
            {
                if (!string.IsNullOrEmpty(m_groupnode_name) && m_save_groupnode_name == m_groupnode_name)
                {
                    SetNextState(st);
                }
            }
        }
    }
    void br_isDClickStateGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.DCLICK)
        {

            var pos = GetPointerOnMainBmp();
            var rect = G.get_draw_wp_frame(m_altstate_name);
            if (rect != null && ((RectangleF)rect).Contains(pos))
            {
                if (!string.IsNullOrEmpty(m_groupnode_name) && m_save_groupnode_name == m_groupnode_name)
                {
                    SetNextState(st);
                }
            }
        }
    }
    void br_isClickOtherStateGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.CLICK)
        {
            //System.Diagnostics.Debug.WriteLine(m_groupnode_name);
            //System.Diagnostics.Debug.WriteLine(m_focus_state);
            //System.Diagnostics.Debug.WriteLine(m_state_under_pointer);

            var pos = GetPointerOnMainBmp();
            var rect = G.get_draw_wp_frame(m_altstate_name);
            if (rect != null && ((RectangleF)rect).Contains(pos))
            {
                if (!string.IsNullOrEmpty(m_groupnode_name) && m_save_groupnode_name != m_groupnode_name)
                {
                    SetNextState(st);
                }
            }
        }
    }
    void br_isClickOnStateGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.CLICK_ON_STATE)
        {
            SetNextState(st);
        }
    }

    void br_isClickNotStateGN(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_chkonfcsGNSc.m_result == ChkOnFcsGNStateControl.RESULT.CLICK)
        {
            if (!HasNextState())
            {
                SetNextState(st);
            }
        }
    }
    //at S_DragAndDropGN
    void subsc_dandGN_init()
    {
        m_dandGNSc.Init();
    }
    void subsc_dandGN_update()
    {
        m_dandGNSc.Update();
    }
    bool subsc_dandGN_wait()
    {
        if (m_dandGNSc.IsDone())
        {
            m_okCancel = m_dandGNSc.m_result == DAnDGNStateControl.RESULT.SUCCESS;
            if (m_okCancel) {
                var state = AltState.TrimAltStateName( m_altstate_name);
                stateview.History2.SaveForce_modify_pos("g:"+state);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //S_ShowGroupNodeMenu
    public static ViewFormStateMenuGNItem m_viewFormStateMenuGNItem;
    void statemenuGN_init()
    {
        var state = GetStateOnPointer();
        if (AltState.IsAltState(state)) {  //if (m_groupnode_allstates_onfoucsgroup!=null && m_groupnode_allstates_onfoucsgroup.Contains(state)) {
            m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.UNKNOWN;
            G.view_form.groupNodeMenu.Show(G.view_form,  G.view_form.PointToClient(PointUtil.Add_XY(Cursor.Position,-20,-10)));
        }
        else {
            m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.CANCEL;
        }

    }
    bool statemenuGN_wait()
    {
        var sm = G.view_form.groupNodeMenu;
        var pad = 10;
        var rec = new Rectangle(sm.Left-pad,sm.Top-pad,sm.Width + 2 * pad,sm.Height + 2 * pad);

        if (rec.Contains(Cursor.Position))
        {
            return m_viewFormStateMenuGNItem != ViewFormStateMenuGNItem.UNKNOWN;
        }
        else
        {
            G.view_form.groupNodeMenu.Close();
            m_viewFormStateMenuGNItem = ViewFormStateMenuGNItem.CANCEL;
            return true;
        }
    }

    void br_EnterGN(Action<int,bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.ENTER)
        {
            SetNextState(state);
        }
    }

    void br_UngroupGN(Action<int,bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.UNGROUPING)
        {
            SetNextState(state);
        }
    }

    void br_EditGN(Action<int,bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.EDIT)
        {
            SetNextState(state);
        }
    }

    void br_MovetoGN(Action<int,bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.MOVETO)
        {
            SetNextState(state);
        }
    }

    void br_CopyGN(Action<int,bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.COPY)
        {
            SetNextState(state);
        }
    }

    void br_DeleteGN(Action<int, bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.DELETE)
        {
            SetNextState(state);
        }
    }

    void br_ExportClipboardGN(Action<int, bool> state)
    {
        if (m_viewFormStateMenuGNItem == ViewFormStateMenuGNItem.EXPORTCLIPBOARD)
        {
            SetNextState(state);
        }
    }

    //S_EnterGN
    void statemenu_enterGN()
    {
        G.node_enter_group(m_groupnode_name);
        G.view_form.tabControl.SelectedTab = G.view_form.tabPageMain;
        //History.ReqToSave("Enterd a group");
        FocusTrack.Record_curpath();
    }

    //S_UngroupingGN
    void statemenu_ungroupingGN()
    {
        G.node_ungrouping(m_groupnode_name);
        m_request_redrawNodeTreeView = true;
        //History.ReqToSave("Ungrouped");
        History2.SaveForce_ungrouping("Ungrouped " + m_groupnode_name);
    }

    //S_EditGN
    stateview._5000_ViewForm.dialog.EditGroupForm m_editgroupform=null;
    void statemenu_editGN()
    {
        m_editgroupform = new stateview._5000_ViewForm.dialog.EditGroupForm();
        m_editgroupform.m_groupname = m_groupnode_name;
        m_editgroupform.m_comment   = G.node_get_comment_by_groupname(m_groupnode_name);

        m_editgroupform.Show(G.view_form);
    }
    bool statemenu_editGN_wait()
    {
        if (m_editgroupform.DialogResult != DialogResult.None)
        {
            m_okCancel = (m_editgroupform.DialogResult == DialogResult.OK);

            if (m_okCancel)
            {
                var from = m_groupnode_name;
                var to   = m_editgroupform.m_groupname;
                var cmt  = m_editgroupform.m_comment;
                G.node_set_comment_by_groupname(from,cmt);
                if (from != to)  G.node_rename_groupname(from,to);

                G.node_save_position();
                stateview.SaveLoadIni.SaveTempIni();

                //History.ReqToSave("Edited a group");
                History2.SaveForce_editgroup(to);
            }

            return true;
        }
        return false;
    }

    //S_MovetoGN
    stateview._5000_ViewForm.dialog.MovetoForm m_movetoform = null;
    void statemenu_movetoGN()
    {
        m_movetoform = new stateview._5000_ViewForm.dialog.MovetoForm();
        m_movetoform.m_pathdir = G.node_get_dirpath_by_groupname(m_groupnode_name);
        m_movetoform.Show(G.view_form);
        m_movetoform.Location = Cursor.Position;
    }
    bool statemenu_movetoGN_done()
    {
        if (m_movetoform.DialogResult == DialogResult.None) return false;
        if (m_movetoform.DialogResult == DialogResult.OK)
        {
            var pathdir = m_movetoform.m_pathdir;
            G.node_moveto_group(m_altstate_name,pathdir);
            m_okCancel = true;

            History.ReqToSave("Moved a group");
        }
        else
        {
            m_okCancel = false;
        }
        return true;
    }

    //at S_CopyGN
    void statemenu_copyGN()
    {
        G.node_copy_group(m_groupnode_name);

        History.ReqToSave("Copied a group");
    }

    //at S_ExportClipboardGN
    void statemenu_exportGN()
    {
        var templist = G.node_get_allstates_on_groupnode(m_groupnode_name);
        G.ExportToClipboard(templist, new List<string> { AltState.MakeAltStateName(m_groupnode_name)  });
    }

    //at S_DeleteGN
    void statemenu_deleteGN_init()
    {
        var nl = Environment.NewLine;
        m_delconfDlg = new stateview._5000_ViewForm.dialog.OKCancelForm();
        m_delconfDlg.textBox1.Text = G.Localize("del_Confirmation");
        m_delconfDlg.textBox1.Text = G.Localize("del_delete_group") /*"Is it to ok to delete group ?"*/ +nl + nl + m_groupnode_name ;
        m_delconfDlg.Show(G.view_form);
        m_delconfDlg.Location = Cursor.Position;
    }
    bool wait_statemenu_deleteGN_done()
    {
        if (m_delconfDlg.DialogResult == DialogResult.None) return false;
        if (m_delconfDlg.DialogResult == DialogResult.OK)
        {
            G.node_delete_group(m_groupnode_name);
            m_okCancel = true;
            //History.ReqToSave("Deleted a group");
            History2.SaveForce_delete("g:" + m_groupnode_name);
        }
        else
        {
            m_okCancel = false;
        }
        return true;
    }
    #region group選択後 CTRLでgroup追加
    void focus_set_2group()
    {
        if (m_group_focus_list!=null)
        { 
            m_group_focus_list.Clear();
        }
        else
        {
            m_group_focus_list = new List<string>();
        }
        m_group_focus_list.Add(AltState.MakeAltStateName(m_save_groupnode_name));
        m_group_focus_list.Add(m_state_under_pointer);
        m_focus_state = m_state_under_pointer;

        FocusTrack.Record(m_group_focus_list);

    }
    void focus_set_group_and_state()
    {
        if (m_group_focus_list!=null)
        { 
            m_group_focus_list.Clear();
        }
        else
        {
            m_group_focus_list = new List<string>();
        }
        m_group_focus_list.Add(AltState.MakeAltStateName(m_save_groupnode_name));
        m_group_focus_list.Add(m_state_under_pointer);
        m_focus_state = m_state_under_pointer;

        FocusTrack.Record(m_group_focus_list);
    }
    #endregion
}
