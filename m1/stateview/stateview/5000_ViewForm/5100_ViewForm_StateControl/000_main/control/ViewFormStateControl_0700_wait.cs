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
    //bool wait_click()
    //{
    //    return G.mouse_event == stateview.MouseEventId.CLICK;
    //}

    bool wait_mousedown()
    {
        return G.mouse_down_or_up || G.mouse_event == stateview.MouseEventId.ABORT;
    }
    bool wait_reqredraw()
    {
        return m_isReqRedraw;
    }

    bool wait_mouseany()
    {
        if (G.mouse_event == stateview.MouseEventId.MOVE || G.mouse_event == stateview.MouseEventId.NONE)
        {
            return false;
        }
        return true;
    }

    bool wait_time(float limit,int maxlen)
    {
        if (G.mouse_event == stateview.MouseEventId.DOUBLECLICK || G.mouse_double_clicked)  m_dclick = true;
        if (G.mouse_event == stateview.MouseEventId.MOUSEUP)      m_mup    = true;

        if (m_dclick) { m_mup = true;  return true; }

        var diff = PointUtil.Sub_Point(m_savepos, GetPointerOnMainBmp());
        var len = Math.Sqrt(diff.X * diff.X + diff.Y  * diff.Y);
        if (len > maxlen) return true;

        m_time += G.delta_time;
        return m_time > limit;
    }

    DialogResult m_multieditcontrol_result;
    bool wait_editdone()
    {
        if (G.multiedit_control!=null)
        {
            var b = G.multiedit_control.IsDone();
            if (b)
            {
                m_multieditcontrol_result = G.multiedit_control.DialogResult;
                G.multiedit_control = null; //排他のためnull

                if (m_multieditcontrol_result != DialogResult.Cancel)
                {
                    stateview.History2.SaveForce_modify_value("Edited a state.");
                }

                return true;
            }
            else
            {
                return false;
            }
        }
       return true;
    }

    bool wait_branchdlg_close()
    {
        return (m_nextform!=null && m_nextform.DialogResult != DialogResult.None);
    }
}
