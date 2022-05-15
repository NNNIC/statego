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

public partial class EditForm_ClickControl  {

    void br_tumb(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_thumbnail)
        {
            SetNextState(state);
        }
    }

    void br_state(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_state)
        {
            SetNextState(state);
        }
    }

    void br_nextstate(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_nextstate)
        {
            SetNextState(state);
        }
    }

    void br_basestate(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_basestate)
        {
            SetNextState(state);
        }
    }

    void br_gosubstate(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_gosubstate)
        {
            SetNextState(state);
        }
    }

    void br_branch(Action<int,bool> state)
    {
        if (m_name == G.STATENAME_branch)
        {
            SetNextState(state);
        }
    }

    void br_other(Action<int,bool> state)
    {
        if (!HasNextState())
        {
            SetNextState(state);
        }
    }

    void br_text(Action<int,bool> state)
    {
        if (m_changeDirection_or_text == false)
        {
            SetNextState(state);
        }
    }

    void br_brgo(Action<int,bool> state)
    {
        if (m_changeDirection_or_text == true)
        {
            SetNextState(state);
        }
    }
}
