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
    float m_diff  = DUNIT;

    float m_diffSP = 0;
    float m_diffPQ = 0;
    float m_diffTG = 0;

    void setdiff_SP() {
        m_diffSP = m_diff;
    }
    void setdiff_PQ() {
        m_diffPQ = m_diff;
    }
    void setdiff_TG() {
        m_diffTG = m_diff;
    }

    void setdiff_clear_SP() {
        m_diffSP = 0;
    }
    void setdiff_clear_PQ() {
        m_diffPQ = 0;
    }
    void setdiff_clear_TG() {
        m_diffTG = 0;
    }
    void setdiff_allclear() {
        m_diffSP = 0;
        m_diffPQ = 0;
        m_diffTG = 0;
    }

    void setdiff_PQ_chkQR() {
        if (m_bCheckQR)
        {
            setdiff_PQ();
        }
    }
    void setdiff_SP_chkSQ() {
        if (m_bCheckSQ)
        {
            setdiff_SP();
        }
    }
    void setdiff_TG_chkRG() {
        if (m_bCheckRG)
        {
            setdiff_TG();
        }
    }
}
