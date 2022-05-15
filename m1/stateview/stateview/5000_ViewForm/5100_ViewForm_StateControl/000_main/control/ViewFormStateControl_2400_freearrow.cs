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
using System.Diagnostics;
using stateview;

// freearrow -- 遷移先ステート決定用の自由矢印

public partial class ViewFormStateControl {
    void subsc_init_freearrow()
    {
        m_freeArrowSc.Init();
    }

    void subsc_update_freearrow()
    {
        m_freeArrowSc.Update();
    }

    bool wait_freearrow_done()
    {
        return m_freeArrowSc.IsDone();
    }
}
