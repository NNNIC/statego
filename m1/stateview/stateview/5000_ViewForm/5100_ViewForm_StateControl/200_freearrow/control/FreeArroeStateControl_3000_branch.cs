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

public partial class FreeArrowStateControl  {

    void br_diff_pos(Action<int,bool> st)
    {
        if (!m_samepos)
        {
            SetNextState(st);
        }
    }

    void br_same_pos(Action<int,bool> st)
    {
        if (m_samepos)
        {
            SetNextState(st);
        }
    }

    void br_stay(Action<int,bool> st)
    {
        if (m_bStay)
        {
            SetNextState(st);
        }
    }

    void br_leave(Action<int,bool> st)
    {
        if (!m_bStay)
        {
            SetNextState(st);
        }
    }

    void br_setdst(Action<int,bool> st)
    {
        if (m_setdst)
        {
            SetNextState(st);
        }
    }

    void br_cancel(Action<int,bool> st)
    {
        if (!m_setdst)
        {
            SetNextState(st);
        }
    }

    void br_dst_is_state(Action<int,bool> st)
    {
        if (!stateview.AltState.IsAltState(m_new_desitination_state))
        {
            SetNextState(st);
        }
    }
    void br_dst_is_altstate(Action<int,bool> st)
    {
        if (stateview.AltState.IsAltState(m_new_desitination_state))
        {
            SetNextState(st);
        }
    }
}
