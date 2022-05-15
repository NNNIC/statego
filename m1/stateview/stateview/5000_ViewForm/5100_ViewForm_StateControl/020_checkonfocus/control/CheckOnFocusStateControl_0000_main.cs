﻿//<<<include=using.txt
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

public partial class CheckOnFocusStateControl : StateControlBase {

    public enum RESULT
    {
        UNKNOWN,
        CLICK,
        DCLICK,
        DRAG,
        CANCEL
    }

    public RESULT m_result;

    ViewFormStateControl m_parent;

    public CheckOnFocusStateControl(ViewFormStateControl parent)
    {
        m_parent = parent;
    }

    public void Init()
    {
        m_result = RESULT.UNKNOWN;
        sc_start(S_START);
    }

    public void Update()
    {
        sc_update();
    }

    public bool IsDone()
    {
        return m_sm.CheckState(S_END);
    }
}
