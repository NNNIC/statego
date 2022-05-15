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

public partial class ArrowFlowStateControl2
{
    void br_straight(Action<int,bool> st)
    {
        if (m_bStraight)
        {
            SetNextState(st);
        }
    }
    void br_checkQR(Action<int,bool> st)
    {
        if (m_bCheckQR)
        {
            SetNextState(st);
        }
    }
    void br_checkSQ(Action<int,bool> st)
    {
        if (m_bCheckSQ)
        {
            SetNextState(st);
        }
    }
    void br_checkRG(Action<int,bool> st)
    {
        if (m_bCheckRG)
        {
            SetNextState(st);
        }
    }
}
