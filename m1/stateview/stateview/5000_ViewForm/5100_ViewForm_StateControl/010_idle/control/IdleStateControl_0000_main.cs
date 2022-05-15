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

public partial class IdleStateControl : StateControlBase {

    public enum RESULT
    {
        UNKNOWN,

        REQREDRAW,
        REQCENTERFOCUSSTATE, //Request center focus state
        REQCENTERFOCUSGROUP, //Request center focus group

        ONSTATE,
        DC_ONSTATE,   //Double Click On State
        DC_ONBRANCH,  //Double Click On Branch
        DRAGBRANCH,
        DRAGINSPACE,
        ONGROUPNODE,
        CLICK_ONBLANK,
        HOLD_MBD,

        CLICK_ON_BRANCH, //一回クリックのみブランチにて

        CC_DRAGENTER,    //コピーコレクションのドラッグエンター
    }

    public RESULT m_result;

    ViewFormStateControl m_parent;

    bool    m_isReqRedraw                    { set { m_parent.m_isReqRedraw = value; }                    get { return m_parent.m_isReqRedraw ;} }

    string m_state_under_pointer             { set { m_parent.m_state_under_pointer = value; }            get { return m_parent.m_state_under_pointer; } }

    PointF m_branchpoint_pos                 { set { m_parent.m_branchInfo.m_branchpoint_pos = value; }                get { return m_parent.m_branchInfo.m_branchpoint_pos; } }
    string m_branchpoint_state               { set { m_parent.m_branchInfo.m_branchpoint_state = value;   }            get { return m_parent.m_branchInfo.m_branchpoint_state; } }
    string m_branchpoint_label               { set { m_parent.m_branchInfo.m_branchpoint_label = value;   }            get { return m_parent.m_branchInfo.m_branchpoint_label; } }

    int?  m_branchpoint_isNextStateOrBranchOrGosub { set { m_parent.m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub =value; } get { return m_parent.m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub;  } }
    int?   m_branchpoint_branch_index        { set { m_parent.m_branchInfo.m_branchpoint_branch_index= value;   }      get { return m_parent.m_branchInfo.m_branchpoint_branch_index;  } }
    string m_branchpoint_branch_string       { set { m_parent.m_branchInfo.m_branchpoint_branch_string = value; }      get { return m_parent.m_branchInfo.m_branchpoint_branch_string; } }

    bool   m_branchpoint_inputpoint   //入力ポイント、無理くりでブランチポイントに含める 2019.12.22 
                                             { set { m_parent.m_branchInfo.m_branchpoint_inputpoint = value; } get { return m_parent.m_branchInfo.m_branchpoint_inputpoint;} }

    bool m_yesno;
    bool m_click;
    bool m_hold;

    public IdleStateControl(ViewFormStateControl parent)
    {
        m_parent = parent;
    }

    public void Init()
    {

        m_result = RESULT.UNKNOWN;
        m_click = false;
        m_hold  = false;
        sc_start(S_START);

        Dbg_SetLoggingState((s)=> { G.logAppend(s + Environment.NewLine); });
    }

    public void Update()
    {
        if (G.mouse_event == stateview.MouseEventId.CLICK)
        {
            if ( m_sm.GetCurFunc() != null )
            {
                G.log+="!clicked @ " +  m_sm.GetCurFunc().Method.Name +  Environment.NewLine;
            }
        }

        sc_update();
    }

    public bool IsDone()
    {
        return m_sm.CheckState(S_END);
    }
}
