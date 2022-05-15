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

public partial class IdleStateControl {

    public Point? m_point_on_mouseDown { get; private set; }

    void CheckMouseDown() {

        if (G.mouse_down_or_up==true)
        {
            m_point_on_mouseDown = Cursor.Position;
        }
        else
        {
            m_point_on_mouseDown = null;
        }

        m_yesno = G.mouse_down_or_up;
    }

    void CheckDoubleClick()
    {

        m_yesno = ( G.mouse_event == MouseEventId.DOUBLECLICK || G.mouse_double_clicked);
    }

    void CheckIsOnState() {
//        m_state_under_pointer = null;
//        if (G.m_draw_data_list==null) return;

//        var pos = m_parent.GetPointerOnMainBmp();

//        //ステート検索
//#if obs
//        foreach (var p in G.m_draw_data_list)
//        {
//            var sd = p.Value;
//            if (sd==null) continue;
//            if (sd.wp_frame_drect.Contains(pos))
//            {
//                m_state_under_pointer = p.Key;
//                break;
//            }
//        }
//#else
//        for(var i = G.state_list.Count-1 ; i>=0; i--)
//        {
//            var state = G.state_list[i];
//            if (G.m_draw_data_list.ContainsKey(state))
//            {
//                var sd = G.m_draw_data_list[state];
//                if (sd==null) continue;
//                if (sd.wp_frame_drect.Contains(pos))
//                {
//                    m_state_under_pointer = state;
//                    break;
//                }
//            }
//        }
//#endif
        m_parent.Check_state_under_pointer();
        m_yesno = m_state_under_pointer != null;
    }
    //void Check_state_under_pointer()
    //{
    //    m_state_under_pointer = null;
    //    if (G.m_draw_data_list==null) return;

    //    var pos = m_parent.GetPointerOnMainBmp();
    //    for(var i = G.state_list.Count-1 ; i>=0; i--)
    //    {
    //        var state = G.state_list[i];
    //        if (G.m_draw_data_list.ContainsKey(state))
    //        {
    //            var sd = G.m_draw_data_list[state];
    //            if (sd==null) continue;
    //            if (sd.wp_frame_drect.Contains(pos))
    //            {
    //                m_state_under_pointer = state;
    //                break;
    //            }
    //        }
    //    }
    //}
    void CheckClickOnBranch() //クリックで、分岐上にあるか？
    {
        m_yesno = false;
        if (G.mouse_event == MouseEventId.CLICK)
        {
            CheckIsOnBranch();
        }
    }

    public void CheckIsOnBranch() {

        m_branchpoint_state  = null;     //分岐ポイント上にカーソルポインタがあるか？ 
        m_branchpoint_inputpoint =false; //2019.12.22 入力ソースのポイントも含める

        if (G.m_draw_data_list==null) return;

        var pos = m_parent.GetPointerOnMainBmp();
        m_branchpoint_label = string.Empty;
        m_branchpoint_isNextStateOrBranchOrGosub = null;
        m_branchpoint_branch_index        = null;
        m_branchpoint_branch_string       = null;

        foreach(var p in G.m_draw_data_list)
        {
            var sd = p.Value;
            var st = p.Key;
            if (sd == null) continue;

            if (sd.input_dcircle!=null && sd.wp_input_dcircle_w_margin.Contains(pos)) //"input"
            {
                m_branchpoint_pos = RectangleUtil.Center(sd.wp_input_dcircle_w_margin);
                m_branchpoint_state = st;
                m_branchpoint_inputpoint = true;
                break;
            }

            if (sd.output_dcircle!=null && sd.wp_output_dcircle_w_margin.Contains(pos)) //"nextstate"
            {
                m_branchpoint_pos = RectangleUtil.Center(sd.wp_output_dcircle_w_margin);
                m_branchpoint_state = st;
                m_branchpoint_label = G.excel_program.GetString(m_branchpoint_state,G.STATENAME_nextstate/* "nextstate"*/);
                m_branchpoint_isNextStateOrBranchOrGosub = 1;//true;
                break;
            }
            if (sd.gsout_dcircle!=null && sd.wp_gsout_dcircle_w_margin.Contains(pos))
            {
                m_branchpoint_pos = RectangleUtil.Center(sd.wp_gsout_dcircle_w_margin);
                m_branchpoint_state = st;
                m_branchpoint_label = G.excel_program.GetString(m_branchpoint_state,G.STATENAME_gosubstate/* "nextstate"*/);
                m_branchpoint_isNextStateOrBranchOrGosub = 3;//gosub
                break;
            }
            if (sd.num_of_branches==0 || sd.wp_bout_dcircle_w_margin_list==null) continue;
            for(int index = 0; index <sd.num_of_branches; index++)
            {
                var td = sd.wp_bout_dcircle_w_margin_list[index];
                if (td!=null && td.Contains(pos))
                {
                    m_branchpoint_pos = RectangleUtil.Center(td);
                    m_branchpoint_state = st;
                    m_branchpoint_isNextStateOrBranchOrGosub = 2;//false;
                    m_branchpoint_branch_index = index;

                    var s = G.excel_program.GetString(st,"branch");
                    m_branchpoint_branch_string = DTBranchUtil.GetOneLineByIndex(s,index);
                    m_branchpoint_label = DTBranchUtil.GetLabel(m_branchpoint_branch_string);
                    break;
                }
            }
        }

        m_yesno = m_branchpoint_state!=null;

    }

    void CheckIsOnBranch_CheckIsNotInput()
    {
        CheckIsOnBranch();
        if (m_yesno)
        {
            if (m_branchpoint_inputpoint)
            {
                m_yesno = false;
            }
        }
    }

    void CheckIsDragBranch() {

        if (m_parent.m_save_branchInfo!=null && m_parent.m_save_branchInfo.m_branchpoint_inputpoint==false)
        {
            var pos  = m_parent.GetPointerOnMainBmp();
            var len = PointUtil.Len_Point(m_parent.m_save_branchInfo.m_branchpoint_pos,pos);
            m_yesno = len > 10;
        }
        else
        {
            m_yesno = false;
        }
    }

    void CheckCCDragEnter()
    {
        m_yesno = G.m_cc_dragdrop == G.CCDD.dragenter;
    }

    void CheckIsReqRedraw() {
        m_yesno = m_isReqRedraw;
    }

    void Check_RecBranch() {
        m_yesno =  IsRecBranch();
    }

    Point m_dginspace_save_pos { get { return m_parent.m_group_focus_start; } set { m_parent.m_group_focus_start = value; } }
    DateTime m_start_time;
    void DragInSpace_init()
    {
        m_dginspace_save_pos = Cursor.Position;
        m_start_time = DateTime.Now;
    }

    bool DragInSpace_wait()
    {
        if (G.mouse_event == MouseEventId.CLICK)
        {
            var state = m_parent.GetStateOnPointer_outframe();
            if (string.IsNullOrEmpty(state))
            {
                m_click = true;
                m_yesno = false;
                return true;
            }
        }

        var pos = Cursor.Position;
        var len = PointUtil.Len_Point(m_dginspace_save_pos,pos);
        if (G.mouse_down_or_up==false)
        {
            m_yesno = false;
            return true;
        }
        // Drag in space
        var bDragInSpace = (Control.ModifierKeys & Keys.Control) == Keys.Control;//コントロールキーで速攻 Drag in spaceと判断
        //if (!bDragInSpace)
        //{
        //    bDragInSpace = (Control.MouseButtons & MouseButtons.Right)==MouseButtons.Right;
        //}
        if (!bDragInSpace)
        {
            bDragInSpace = len > 10;
        }
        if (bDragInSpace) 
        {
            m_yesno = true;
            return true;
        }

        //MB HOLD in space
        var bHoldInSpace = (Control.ModifierKeys & Keys.Shift) == Keys.Shift; //シフトで速攻 MB HOLDと判断
        if (!bHoldInSpace)
        {
            bHoldInSpace = (Control.MouseButtons & MouseButtons.Middle) == MouseButtons.Middle;
        }
        if (!bHoldInSpace)
        {
            bHoldInSpace = len <= 10 && (DateTime.Now - m_start_time).TotalMilliseconds > 200; //ある程度まって判断
        }
        if (bHoldInSpace) //シフトで速攻 MB HOLDと判断
        {
            m_hold = true;
            return true;
        }
        return false;
    }

    void CheckIsGroupNode() {
        if (!string.IsNullOrEmpty( m_state_under_pointer))
        {
            if (G.node_get_style(m_state_under_pointer)== StateStyle.STYLE.FORGROUP)
            {
                m_yesno = true;
                return;
            }
        }
        m_yesno = false;
        return;
    }

    void CheckClickOnBlank()
    {
        if (G.mouse_event == MouseEventId.CLICK)
        {
            var state = m_parent.GetStateOnPointer_outframe();
            if (string.IsNullOrEmpty(state))
            {
                m_yesno = true;
                return;
            }
        }
        m_yesno = false;
    }

    #region Center Focus State
    void CheckReqCenterFocusState()
    {
        m_yesno = !string.IsNullOrEmpty(m_parent.m_center_focus_state); 
    }
    void CheckReqCenterFocusGroup()
    {
        m_yesno = !string.IsNullOrEmpty(m_parent.m_center_focus_group); 
    }
    #endregion

    #region has execkey
    void CheckKeyExec()
    {
        m_yesno = G.keyexec != KEYEXEC.none;
    }
    #endregion

}
