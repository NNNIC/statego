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

public partial class FreeArrowStateControl : StateControlBase {

    ViewFormStateControl m_parent;

    ViewFormStateControl.BranchInfo  m_save_branchInfo { get { return m_parent.m_save_branchInfo; } }

    bool m_isReqRedraw { set { m_parent.m_isReqRedraw = value; } get { return m_parent.m_isReqRedraw ;} }

    PointF m_save_pos;

    PictureBox m_pb { get { return G.freearrow_picturebox; } }

    Bitmap m_bmp;
    Graphics m_g;

    public FreeArrowStateControl(ViewFormStateControl parent)
    {
        m_parent = parent;
    }

    public void Init()
    {
        m_save_pos = new PointF();

        disposeBmp_g();

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

    public void ReqRedraw()
    {
        m_isReqRedraw = true;
    }
}
