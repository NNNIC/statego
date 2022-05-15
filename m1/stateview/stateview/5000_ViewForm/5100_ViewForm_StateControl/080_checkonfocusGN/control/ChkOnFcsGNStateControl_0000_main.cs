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

public partial class ChkOnFcsGNStateControl  : StateControlBase {
    public enum RESULT
    {
        UNKNOWN,
        DRAG,
        CLICK,
        DCLICK,
        CANCEL,

        CLICK_ON_STATE, //他のステート上でクリック
    }

    ViewFormStateControl m_parent;
    public RESULT        m_result;

    //bool m_isReqRedraw { set { m_parent.m_isReqRedraw = value; } get { return m_parent.m_isReqRedraw ;} }

    //List<string> m_group_focus_list { get { return m_parent.m_group_focus_list; } }
    string m_groupnode_focus { set { m_parent.m_groupnode_name = value;             }  get { return m_parent.m_groupnode_name;             } }
    Point m_start            { set { m_parent.m_groupnode_focus_move_start = value; }  get { return m_parent.m_groupnode_focus_move_start; } }
    DateTime m_startTime;

    string m_groupnode_focus_click_state { set { m_parent.m_groupnode_focus_click_state = value;  }  get { return m_parent.m_groupnode_focus_click_state; }  }

    string m_state_under_pointer { set { m_parent.m_state_under_pointer = value; } get { return m_parent.m_state_under_pointer; } }

    public ChkOnFcsGNStateControl(ViewFormStateControl parent)
    {
        m_parent = parent;
    }

    public void Init()
    {
        m_result = RESULT.UNKNOWN;
        sc_start(S_START);
        Dbg_SetLoggingState((s)=> { G.logAppend(s + Environment.NewLine); });
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
