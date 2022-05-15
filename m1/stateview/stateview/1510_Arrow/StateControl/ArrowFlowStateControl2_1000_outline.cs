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
    PointF m_posP;
    PointF m_posQ;
    PointF m_posR;
    PointF m_posT;

    bool  m_bUD_PQ; //P->Q の方向 上か下か
    bool  m_bLR_QR; //Q->R の方向 右から左か

    void outline_create() {
        m_bUD_PQ = (m_posS.Y >= m_posG.Y );
        m_bLR_QR = (m_posS.X <= m_posG.X );

        m_posP = m_posS;
        m_posQ = m_posP;

        m_posT = m_posG;
        m_posR = m_posT;
    }
}
