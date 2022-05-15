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

    void setevt_click()
    {
        m_result = RESULT.CLICK;
    }

    void setevt_dclick()
    {
        m_result = RESULT.DCLICK;
    }

    void setevt_drag()
    {
        m_result = RESULT.DRAG;
    }

    void setevt_cancel()
    {
        m_result = RESULT.CANCEL;
    }
}
