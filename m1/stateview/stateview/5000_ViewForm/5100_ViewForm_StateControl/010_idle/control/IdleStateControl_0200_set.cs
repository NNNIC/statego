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

    void Set_OnState() {
        m_result = RESULT.ONSTATE;
    }
    void Set_DcOnState() {
        m_result = RESULT.DC_ONSTATE;
    }
    void Set_DcOnBranch() {
        m_result = RESULT.DC_ONBRANCH;
    }
    void Set_DragBranch() {
        m_result = RESULT.DRAGBRANCH;
    }
    void Set_ReqRedraw() {
        m_result = RESULT.REQREDRAW;
    }
    void Set_ReqCenterFocusState()
    {
        m_result = RESULT.REQCENTERFOCUSSTATE;
    }
    void Set_ReqCenterFocusGroup()
    {
        m_result = RESULT.REQCENTERFOCUSGROUP;
    }
    void Set_DragInSpace() {
        m_result = RESULT.DRAGINSPACE;
    }
    void Set_OnGroupNode() {
        m_result = RESULT.ONGROUPNODE;
    }
    void Set_ClickOnBlank()
    {
        m_result = RESULT.CLICK_ONBLANK;
    }
    void Set_HoldMBD() {
        m_result = RESULT.HOLD_MBD;
    }
    void Set_ClickOnBranch()
    {
        m_result = RESULT.CLICK_ON_BRANCH;
        //G.NoticeToUser("!!! CLICK_ON_BRANCH !!!");
    }
    void Set_ReqCCDragEnter() {
        m_result = RESULT.CC_DRAGENTER;
    }
}
