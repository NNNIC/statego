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

public partial class ViewFormStateControl : StateControlBase {

    public bool m_isReqRedraw = false;

    //Sub State Controel
    public IdleStateControl  m_idleSc;
    CheckOnFocusStateControl m_chkonfcsSc;
    FreeArrowStateControl    m_freeArrowSc;
    GroupFocusStateControl   m_groupFcsSc;
    ChkOnFcsGFStateControl   m_chkonfcsGFSc;
    DAnDGFStateControl       m_dandGFSc;
    ChkOnFcsGNStateControl   m_chkonfcsGNSc;
    DAnDGNStateControl       m_dandGNSc;
    SlidingStateControl      m_slidingSC;
    WebAdCheckControl        m_webAdSc;

    public Point             m_group_focus_start;
    public List<string>      m_group_focus_list;
    public string            m_group_focus_click_state;     //矩形で囲んだ中のステートを

    public Point             m_group_focus_move_start;      //矩形で囲んだ中の移動開始点

    public string            m_groupnode_focus_click_state; //グループが１ノードになったものを・・
    public string            m_groupnode_name {              //対象のグループのノード名
        set;
        get;
    }
    public Point             m_groupnode_focus_move_start;  //グループが１ノードになったものの移動開始点

    public string            m_altstate_name { get { return stateview.AltState.MakeAltStateName(m_groupnode_name); } }
    //public List<string>      m_groupnode_allstates_onfoucsgroup { //フォーカスされたグループノードの全ステート
    //                            get { return G.node_get_allstates_on_groupnode(m_groupnode_name); }
    //                         }

    bool m_okCancel;

    public bool              m_request_redrawNodeTreeView= false;   //　ノードツリー再描画依頼 　statebox_optdrawで使用

    public void Init()
    {
        m_idleSc      = new IdleStateControl(this);
        m_chkonfcsSc  = new CheckOnFocusStateControl(this);
        m_freeArrowSc = new FreeArrowStateControl(this);
        m_groupFcsSc  = new GroupFocusStateControl(this);
        m_chkonfcsGFSc= new ChkOnFcsGFStateControl(this);
        m_dandGFSc    = new DAnDGFStateControl(this);
        m_chkonfcsGNSc= new ChkOnFcsGNStateControl(this);
        m_dandGNSc    = new DAnDGNStateControl(this);
        m_slidingSC   = new SlidingStateControl(this);


        m_webAdSc     = new WebAdCheckControl();
        m_webAdSc.Start();

        sc_start(S_Init);

        Dbg_SetLoggingState((s)=> { G.logAppend(s + Environment.NewLine); });
    }

    //string m_curFuncName;
    public void Update()
    {
        sc_update();

        if (m_webAdSc!=null) m_webAdSc.update();
    }

    public void ReqRedraw()
    {
        m_isReqRedraw = true;
    }

    public bool IsIdle()
    {
        return CheckState(S_Idle2);
    }
    public bool IsValidStateforCenterFocus()
    {
        return CheckState(S_Idle2) || CheckState(S_IsMouseDown) || CheckState(S_CheckMouseGN); 
    }
}
