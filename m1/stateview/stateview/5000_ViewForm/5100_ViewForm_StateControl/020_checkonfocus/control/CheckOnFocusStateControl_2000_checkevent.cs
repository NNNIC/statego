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

public partial class CheckOnFocusStateControl {
    const int    m_limitMillisec = 350;
    const double m_limitLength = 10;
    void check_event()
    {
        //一定時間以内にダブルクリック
        //一定時間以内にクリック
        //一定時間以内ボタンが押されたまま
        //一定時間以内にボタンが離されたー＞クリックとする


        if (
            ((DateTime.Now - m_saveTime).TotalMilliseconds > m_limitMillisec)
            ||
            (PointUtil.Len_Point(Cursor.Position, m_savePosOnScreen) > m_limitLength &&  m_save_mouse_event == MouseEventId.NONE && m_keepMouseDown==true)
           )
        {
            if (m_save_mouse_event == MouseEventId.NONE && m_keepMouseDown==false)
            {
                m_save_mouse_event = MouseEventId.CLICK;
            }

            m_hasEvent = true;
            return;
        }
        if (G.mouse_event == MouseEventId.CANCEL)
        {
            m_keepMouseDown = false;
            m_save_mouse_event = MouseEventId.NONE;
            m_hasEvent = true;

            return;
        }

        if ( G.mouse_down_or_up == false) m_keepMouseDown=false;
        if ( m_save_mouse_event != MouseEventId.DOUBLECLICK && !G.mouse_double_clicked  && G.mouse_event == MouseEventId.CLICK)
        {
            m_save_mouse_event = MouseEventId.CLICK;
        }
        if (G.mouse_event == MouseEventId.DOUBLECLICK || G.mouse_double_clicked)
        {
            m_save_mouse_event = MouseEventId.DOUBLECLICK;
            m_hasEvent = true;
            return;
        }
        //マウスボタン右が押されていたら、速攻ドラッグ決定
        //if  ((Control.MouseButtons & MouseButtons.Right) == MouseButtons.Right)
        //{
        //    m_hasEvent = true;
        //    return;
        //}
        // Shift同時押しで、速攻ドラッグ決定
        if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
        {
            m_hasEvent = true;
            return;
        }
    }

    bool wait_has_event()
    {
        return  m_hasEvent;
    }

    void br_Click(Action<int,bool> st)
    {
        if (m_save_mouse_event == MouseEventId.CLICK)
        {
            //G.log+="Click"+ Environment.NewLine;
            SetNextState(st);
        }
    }

    void br_DClick(Action<int,bool> st)
    {
        if (m_save_mouse_event == MouseEventId.DOUBLECLICK || G.mouse_double_clicked)
        {
            //G.log+="DClick"+ Environment.NewLine;
            SetNextState(st);
        }
    }

    void br_Drag(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            if (m_keepMouseDown)
            {
                //G.log+="Drag"+ Environment.NewLine;
                SetNextState(st);
            }
        }
    }

    void br_NotAbove(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            //G.log+="NotAbove"+ Environment.NewLine;
            SetNextState(st);
        }
    }

}
