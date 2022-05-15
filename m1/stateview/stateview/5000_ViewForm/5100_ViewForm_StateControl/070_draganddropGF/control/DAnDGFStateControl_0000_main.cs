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

public partial class DAnDGFStateControl  : StateControlBase {

    public enum RESULT
    {
        UNKNOWN,
        SUCCESS,
        CANCEL
    }

    ViewFormStateControl m_parent;
    public RESULT        m_result;

    bool m_isReqRedraw { set { m_parent.m_isReqRedraw = value; } get { return m_parent.m_isReqRedraw ;} }

    List<string> m_group_focus_list { get { return m_parent.m_group_focus_list; } }
    Bitmap              m_bitmap;
    Graphics            m_graphics;
    RectangleF          m_allrect;

    const float         m_margin=0f;

    Point m_start { get { return m_parent.m_group_focus_move_start; } }


    public DAnDGFStateControl(ViewFormStateControl parent)
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
