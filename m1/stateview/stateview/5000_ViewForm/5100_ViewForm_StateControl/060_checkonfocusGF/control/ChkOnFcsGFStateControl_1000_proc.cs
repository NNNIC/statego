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

public partial class ChkOnFcsGFStateControl  {

    bool m_mousedown;
    bool wait_mbdown()
    {
        //if (G.mouse_event == stateview.MouseEventId.CANCEL)
        //{
        //    m_mousedown = false;
        //    return true;
        //}
        if (G.mouse_event == stateview.MouseEventId.ABORT)
        {
            m_mousedown = false;
            return true;
        }
        if (G.mouse_down_or_up)
        {
            var state = m_parent.GetStateOnPointer();
            m_state_under_pointer = state;
            if (string.IsNullOrEmpty(state) || m_group_focus_list==null || m_group_focus_list.IndexOf(state) < 0 )
            {
                m_mousedown = false;
                return true;
            }
            m_mousedown = true;
            m_group_focus_click_state = state;
            return true;
        }
        return false;
    }

    void br_IsMBD(Action<int,bool> st)
    {
        if (m_mousedown)
        {
            SetNextState(st);
        }
    }

    void br_NotAbove(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }

    void save_pos()
    {
        m_start = Cursor.Position;
        m_startTime = DateTime.Now;
    }

    bool m_bmove;
    bool m_bclick;
    bool wait_move()
    {
        if (G.mouse_event == stateview.MouseEventId.CANCEL)
        {
            m_bmove  = false;
            m_bclick = false;
            return true;
        }
        if (G.mouse_event == stateview.MouseEventId.CLICK)
        {
            m_bmove = false;
            m_bclick = true;
            return true;
        }

        var pos = Cursor.Position;
        var bDrag = PointUtil.Len_Point(m_start,pos)>10;
        //if (!bDrag)
        //{
        //    bDrag = (Control.MouseButtons & MouseButtons.Right) == MouseButtons.Right;
        //}
        if (!bDrag)
        {
            bDrag = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
        }
        if (bDrag)
        {
            m_bmove  = true;
            m_bclick = false;
            return true;
        }
        if ( (DateTime.Now -  m_startTime).TotalMilliseconds > 500) //0.5秒長押し
        {
            m_bmove = true;
            m_bclick = false;
            return true;
        }

        return false;
    }
    void br_IsDrag(Action<int,bool> st)
    {
        if (m_bmove)
        {
            SetNextState(st);
        }
    }
    void br_IsClick(Action<int,bool> st)
    {
        if (m_bclick)
        {
            SetNextState(st);
        }
    }
    void def_Drag()
    {
        m_result = RESULT.DRAG;
    }
    void def_Click()
    {
        m_result = RESULT.CLICK;
    }
    void def_Cancel()
    {
        m_result = RESULT.CANCEL;
    }
}
