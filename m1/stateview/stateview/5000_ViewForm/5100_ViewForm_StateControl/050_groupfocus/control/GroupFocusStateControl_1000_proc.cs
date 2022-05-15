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

public partial class GroupFocusStateControl {
    #region S_INIT
    void init()
    {
        m_pb.Enabled = true;
        m_pb.Visible = false;
        m_pb.Parent  = G.main_picturebox;
        m_pb.BackColor = Color.FromArgb(50,Color.Red);
        m_savePoint = Point.Empty;
        m_group_focus_list = null;
    }
    #endregion

    #region S_CHECKPOS
    Point m_savePoint;
    bool  m_diffOrSame;
    void checkpos()
    {
        var curpos = Cursor.Position;
        m_diffOrSame = m_savePoint != curpos;
        m_savePoint = curpos;
    }
    void br_diff_pos(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            if (m_diffOrSame)
            {
                SetNextState(st);
            }
        }
    }
    void br_same_pos(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            if (!m_diffOrSame)
            {
                SetNextState(st);
            }
        }
    }
    #endregion

    #region S_UPDATE_PB
    void update_pb()
    {
        var start   = GetPosOnMainPB(  m_start );
        m_goal      = GetPointerOnMainBmp();
        var goal    = GetPosOnMainPB( m_goal);

        var rect  = PointUtil.MakeRectangle(start,goal);

        m_pb.Visible = false;

        m_pb.Location = Point.Truncate(rect.Location);
        m_pb.Width    = (int)rect.Width;
        m_pb.Height   = (int)rect.Height;

        m_pb.Visible = true;

        m_pb.Refresh();
    }
    #endregion

    #region S_CHECKMOUSE
    void check_mouse()
    {
    }
    void br_MBDown(Action<int,bool> st)
    {
        if (G.mouse_down_or_up)
        {
            SetNextState(st);
        }
    }
    void br_MBUp(Action<int,bool> st)
    {
        if (!G.mouse_down_or_up)
        {
            SetNextState(st);
        }
    }
    void br_MBCancel(Action<int,bool> st)
    {
        if (G.mouse_event == stateview.MouseEventId.CANCEL)
        {
            SetNextState(st);
        }
    }
    #endregion

    #region S_COLLECT
    void collect_targets()
    {
        var rect = RectangleUtil.MakeRectangle(m_start,m_goal);

        m_group_focus_list = new List<string>();
        foreach(var p in G.m_draw_data_list)
        {
            var sd = p.Value;
            var st = p.Key;
            if (sd==null || sd.wp_frame_drect==null) continue;

            if (rect.Contains(sd.wp_frame_drect))
            {
                m_group_focus_list.Add(st);
            }
        }
        if (m_group_focus_list.Count==0)
        {
            m_group_focus_list = null;
        }
    }
    void br_Exist(Action<int,bool> st)
    {
        if (m_group_focus_list!=null)
        {
            SetNextState(st);
        }
    }
    void br_None(Action<int,bool> st)
    {
        if (m_group_focus_list==null)
        {
            SetNextState(st);
        }
    }
    #endregion

    #region S_DRAW_FOCUS
    void drow_focuses()
    {
        DrawBenri.draw_focuses(m_group_focus_list);
        G.main_picturebox.Refresh();
    }
    #endregion

    #region S_DESTROY
    void destroy()
    {
        m_pb.Enabled =false;
        m_pb.Visible = false;
    }
    #endregion
}
