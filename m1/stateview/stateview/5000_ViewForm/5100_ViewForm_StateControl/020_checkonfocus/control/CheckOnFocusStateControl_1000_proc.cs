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

    bool wait_isMbDown()
    {
        return G.mouse_down_or_up;
    }

    DateTime      m_saveTime;
    public PointF m_savePosOnMainBmp;
    Point         m_savePosOnScreen;
    bool          m_hasEvent;
    bool          m_keepMouseDown;
    MouseEventId  m_save_mouse_event = MouseEventId.NONE;

    void init_vars()
    {
        m_saveTime         = DateTime.Now;
        m_savePosOnMainBmp = m_parent.GetPointerOnMainBmp();
        m_savePosOnScreen  = Cursor.Position;
        m_hasEvent         = false;
        m_keepMouseDown    = true;
        m_save_mouse_event = MouseEventId.NONE;
    }
}
