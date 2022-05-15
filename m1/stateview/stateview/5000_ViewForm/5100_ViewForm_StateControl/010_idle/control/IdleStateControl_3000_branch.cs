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

public partial class IdleStateControl {

    void br_yes(Action<int,bool> st) {
        if (m_yesno)
        {
            SetNextState(st);
        }
    }
    void br_no(Action<int,bool> st) {
        if (!m_yesno)
        {
            SetNextState(st);
        }
    }
    void br_reached(Action<int,bool> st) { }
    void br_gotick(Action<int,bool> st) {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }
    void br_clickOnBlank(Action<int,bool> st) {
        if (m_click)
        {
            SetNextState(st);
        }
    }
    void br_holdMBD(Action<int,bool> st) {
        if (m_hold)
        {
            SetNextState(st);
        }
    }
}
