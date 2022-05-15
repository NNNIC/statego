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

public partial class ChkOnFcsGNStateControl  {

    bool m_mousedown;
    bool m_mousedown_on_state; //グループノード区別するため。
    bool wait_mbdown()
    {
        m_mousedown_on_state = false;

        if (G.mouse_event == stateview.MouseEventId.ABORT)
        {
            m_mousedown = false;
            return true;
        }
        if (G.mouse_down_or_up)
        {
            var state = m_parent.GetStateOnPointer();
            if (!stateview.AltState.IsAltState(state))
            {
                if (stateview.StateUtil.IsValidStateName(state))
                {
                    m_mousedown_on_state = true;
                    m_state_under_pointer = state;
                }
                m_mousedown = false;
                return true;
            }
            var group = stateview.AltState.TrimAltStateName(state); //G.node_get_groupname(state);
            //var allstate_on_group = G.node_get_allstates_on_groupnode(group);
            //if (
            //      allstate_on_group!=null && !allstate_on_group.Contains(state)
            //   )
            //{
            //    m_mousedown = false;
            //    return true;
            //}
            m_mousedown = true;
            m_groupnode_focus_click_state = state;
            m_groupnode_focus = group;
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

    void br_IsMBD_onState(Action<int,bool> st)
    {
        if (m_mousedown_on_state)
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
    bool m_bdclick;
    bool wait_move()
    {
        m_bmove   = false;
        m_bclick  = false;
        m_bdclick = false;
        if (G.mouse_event == stateview.MouseEventId.ABORT)
        {
            m_bmove  = false;
            m_bclick = false;
            return true;
        }
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
        if (G.mouse_event == stateview.MouseEventId.DOUBLECLICK)
        {
            m_bmove   = false;
            m_bclick  = false;
            m_bdclick = true;
            return true;
        }
        
        var pos = Cursor.Position;
        if (PointUtil.Len_Point(m_start,pos)>10)
        {
            m_bmove  = true;
            m_bclick = false;
            return true;
        }
        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
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
    void br_IsDClick(Action<int,bool> st)
    {
        if (m_bdclick)
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
    void def_DClick()
    {
        m_result = RESULT.DCLICK;
    }
    void def_Cancel()
    {
        m_result = RESULT.CANCEL;
    }
    void def_Click_onState()
    {
        m_result = RESULT.CLICK_ON_STATE;
    }
}
