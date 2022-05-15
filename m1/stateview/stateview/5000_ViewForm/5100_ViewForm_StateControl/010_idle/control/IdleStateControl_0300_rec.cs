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

    void RecBranch() {
        m_parent.m_save_branchInfo = m_parent.m_branchInfo.Clone();;
        m_parent.m_branchInfo.Clear();
    }

    void ClrBranch()
    {
        m_parent.m_save_branchInfo = null;
    }

    bool IsRecBranch()
    {
        return m_parent.m_save_branchInfo!=null;
    }
}
