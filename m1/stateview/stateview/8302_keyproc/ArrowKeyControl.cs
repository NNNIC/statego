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

namespace stateview
{
public partial class ArrowKeyControl  {
   
    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (IsEnd()) break;
		}
	}

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from ArrowKeyControl.xlsx.    psgg-file:ArrowKeyControl.psgg
        /*
            E_INPUT
            入力値
            stateはポインタ直下とpin所属ステート
        */
        public Keys   m_key;
        public Point  m_pointer;
        public string m_state_under_pointer;
        public string m_state_with_pin;
        public bool   m_in_or_outflow_pin;
        public InOutBaseData.ATTRIB m_outflow_attr;
        public int    m_branch_index;
        public string m_nextstate;
        /*
            S_0002
        */
        void S_0002(bool bFirst)
        {
            // branch
            if (!focused_state()) { Goto( S_0004 ); }
            else { Goto( S_POINTER_ON_PIN ); }
        }
        /*
            S_0004
            フォーカスさせる
            ポインタ中央へ
        */
        void S_0004(bool bFirst)
        {
            //
            if (bFirst)
            {
                req_focus();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_0006
            流出先の数
            Group時の複数対応
        */
        void S_0006(bool bFirst)
        {
            var c=get_outflow_num();
            if (c>0 && !is_group())
            {
                c = 1;
            }
            // branch
            if (c==0) { Goto( S_OPEN_INOUT_MENU ); }
            else if (c==1) { Goto( S_GO_NEXTSTATE1 ); }
            else { Goto( S_OPEN_INOUT_MENU ); }
        }
        /*
            S_0008
            inflowの流入先数
        */
        void S_0008(bool bFirst)
        {
            var c=get_inflow_num();
            // branch
            if (c==0) { Goto( S_END ); }
            else if (c==1) { Goto( S_GO_PAST_STATE ); }
            else { Goto( S_OPEN_INOUT_MENU1 ); }
        }
        /*
            S_CheckKey
            方向キー確認
        */
        void S_CheckKey(bool bFirst)
        {
            // branch
            if (m_key == Keys.Right) { Goto( S_SITUATION ); }
            else if (m_key == Keys.Left) { Goto( S_SITUATION4 ); }
            else if (m_key == Keys.Down) { Goto( S_SITUATION5 ); }
            else if (m_key == Keys.Up) { Goto( S_SITUATION6 ); }
            else { Goto( S_END ); }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_GO_NEXTSTATE1
            次のステートのフォーカスへ
        */
        void S_GO_NEXTSTATE1(bool bFirst)
        {
            //
            if (bFirst)
            {
                go_nextstate();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_GO_PAST_STATE
            前ステートのフォーカス依頼
            フォーカス後は、ポインタも移動
        */
        void S_GO_PAST_STATE(bool bFirst)
        {
            //
            if (bFirst)
            {
                go_inflow_state();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_INIT
        */
        void S_INIT(bool bFirst)
        {
            //
            if (bFirst)
            {
                init();
            }
            //
            if (!HasNextState())
            {
                Goto(S_CheckKey);
            }
        }
        /*
            S_MOVE_POINTER
            ポインタ移動
        */
        void S_MOVE_POINTER(bool bFirst)
        {
            //
            if (bFirst)
            {
                move_pointer();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_OPEN_INOUT_MENU
            INOUTメニューを開く
        */
        void S_OPEN_INOUT_MENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                open_outflow_contextmenu();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_OPEN_INOUT_MENU1
            INOUTメニューを開く
        */
        void S_OPEN_INOUT_MENU1(bool bFirst)
        {
            //
            if (bFirst)
            {
                open_inflow_contextmenu();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_PAS001
        */
        void S_PAS001(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_GO_NEXTSTATE1);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_PAS002
        */
        void S_PAS002(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_0004);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_PAS003
        */
        void S_PAS003(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_0004);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_POINTER_ON_PIN
            可能な流出先PINにポインタを移動
        */
        void S_POINTER_ON_PIN(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_inoutflow_first();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_POINTER_ON_PIN2
            可能な流出先PINにポインタを移動
        */
        void S_POINTER_ON_PIN2(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_outflow_next();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_POINTER_ON_PIN3
            可能な流出先PINにポインタを移動
        */
        void S_POINTER_ON_PIN3(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_outflow_prev();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_SITUATION
            状況
        */
        void S_SITUATION(bool bFirst)
        {
            // branch
            if (notempty(m_state_under_pointer)) { Goto( S_0002 ); }
            else if (m_in_or_outflow_pin) { Goto( S_PAS002 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate) { Goto( S_0006 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub) { Goto( S_PAS001 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch) { Goto( S_PAS001 ); }
            else { Goto( S_MOVE_POINTER ); }
        }
        /*
            S_SITUATION4
            状況
        */
        void S_SITUATION4(bool bFirst)
        {
            // branch
            if (notempty(m_state_under_pointer)) { Goto( S_0002 ); }
            else if (m_in_or_outflow_pin) { Goto( S_0008 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate) { Goto( S_PAS003 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub) { Goto( S_PAS003 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch) { Goto( S_PAS003 ); }
            else { Goto( S_MOVE_POINTER ); }
        }
        /*
            S_SITUATION5
            状況
        */
        void S_SITUATION5(bool bFirst)
        {
            // branch
            if (notempty(m_state_under_pointer)) { Goto( S_0002 ); }
            else if (m_in_or_outflow_pin) { Goto( S_END ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate) { Goto( S_POINTER_ON_PIN2 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub) { Goto( S_POINTER_ON_PIN2 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch) { Goto( S_POINTER_ON_PIN2 ); }
            else { Goto( S_MOVE_POINTER ); }
        }
        /*
            S_SITUATION6
            状況
        */
        void S_SITUATION6(bool bFirst)
        {
            // branch
            if (notempty(m_state_under_pointer)) { Goto( S_0002 ); }
            else if (m_in_or_outflow_pin) { Goto( S_END ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate) { Goto( S_POINTER_ON_PIN3 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub) { Goto( S_POINTER_ON_PIN3 ); }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch) { Goto( S_POINTER_ON_PIN3 ); }
            else { Goto( S_MOVE_POINTER ); }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            Goto(S_INIT);
            NoWait();
        }


	#endregion // [PSGG OUTPUT END]

        List<InOutBaseData> m_out_list;
        List<InOutBaseData> m_in_list;

        public void reset()
        {
            m_key                 = Keys.None;
            m_pointer             = Point.Empty;
            m_state_under_pointer = null;
            m_state_with_pin      = null;
            m_in_or_outflow_pin   = false;
            m_outflow_attr        = InOutBaseData.ATTRIB._base;
            m_branch_index        = -1;
            m_nextstate           = null;
        }

        void init()
        {
            var state = !string.IsNullOrEmpty(m_state_with_pin) ? m_state_with_pin : m_state_under_pointer;
            if (!string.IsNullOrEmpty(state))
            { 
                m_out_list     = DictionaryUtil.Get(G.state_output_dst_list,state);
                if (m_out_list!=null)
                { 
                    m_out_list.RemoveAll(i=>i.attrib == InOutBaseData.ATTRIB._base);
                }
                 
                if (AltState.IsAltState(state))
                {
                    var statelist = DictionaryUtil.Get(G.group_input_src_list_wo_base_on_current,AltState.TrimAltStateName(state));
                    if (statelist!=null)
                    {
                        m_in_list = new List<InOutBaseData>();
                        foreach(var s in statelist)
                        {
                            var d = new InOutBaseData();
                            d.attrib = InOutBaseData.ATTRIB.nextstate;
                            d.branch_index = 0;
                            d.state = s;
                            d.target_state = state;
                            m_in_list.Add(d);
                        }
                    }
                }
                else
                { 
                    m_in_list      = DictionaryUtil.Get(G.state_input_src_list, state);
                    if (m_in_list!=null)
                    { 
                        m_in_list.RemoveAll(i=>i.attrib == InOutBaseData.ATTRIB._base);
                    }
                }
            }
        }
        bool focused_state()
        {
            var focuslist = G.vf_sc.GetFocusingStates();
            if (focuslist!=null && focuslist.FindIndex(i=>i==m_state_under_pointer)>=0)
            {
                return true;
            }
            return false;
        }
        int get_inflow_num()
        {
            if (m_in_list!=null) return m_in_list.Count;
            return 0;
        }
        int get_outflow_num()
        {
            if (m_out_list!=null) return m_out_list.Count;
            return 0;
        }
        bool notempty(string s)
        {
            return !string.IsNullOrEmpty(s);
        }
        void req_focus()
        {
            var state = !string.IsNullOrEmpty(m_state_with_pin) ? m_state_with_pin : m_state_under_pointer;
            _set_focus_req(state);
        }

        private static void _set_focus_req(string state)
        {
            if (!string.IsNullOrEmpty(state))
            {
                var bOk = false;

                if (AltState.IsAltState(state))
                {
                    var target = AltState.TrimAltStateName(state);
                    if (G.vf_sc.m_center_focus_group!=target)
                    { 
                        G.vf_sc.m_center_focus_group = AltState.TrimAltStateName(state);
                        bOk = true;
                    }
                }
                else
                {
                    if (G.vf_sc.m_center_focus_state!=state)
                    { 
                        G.vf_sc.m_center_focus_state = state;
                        bOk = true;
                    }
                }

                if (bOk) //命令多重阻止
                {
                    G.keyexec = KEYEXEC.focus_specified_state;
                }
                else
                {
                    //bOk = false;
                }
            }
        }

        void go_nextstate()
        {
            var state = m_nextstate;
            if (string.IsNullOrEmpty(state) && AltState.IsAltState(m_state_with_pin) && m_out_list!=null && m_out_list.Count>0 )
            {
                state = m_out_list[0].state;
            }

            _set_focus_req(state);
        }

        void go_inflow_state()
        {
            if (m_in_list!=null && m_in_list.Count>0)
            {
                _set_focus_req(m_in_list[0].state);
            }
        }

        void open_outflow_contextmenu()
        {
            G.keyexec = KEYEXEC.open_inout_menu;
            G.m_keyopen_in_or_out = false;
            return;
        }

        void open_inflow_contextmenu()
        {
            G.keyexec = KEYEXEC.open_inout_menu;
            G.m_keyopen_in_or_out = true;
        }

        bool _check_branch(string state,out bool has_nextstate, out bool has_branch, out bool has_gosub)
        {
            var nextstate= G.excel_program.GetStringWithBasestate(state,G.STATENAME_nextstate);
            has_nextstate = StateUtil.IsValidStateName(nextstate);
            has_branch = (!string.IsNullOrEmpty(G.excel_program.GetStringWithBasestate(state,G.STATENAME_branch)));
            var state_typ = G.excel_program.GetString(state,G.STATENAME_statetyp);
            has_gosub  = (state_typ==WordStorage.Store.state_typ_gosub || state_typ == WordStorage.Store.state_typ_loop);
            return true;
        }

        void set_inoutflow_first()
        {
            
            //DrawBenri.draw_opt();

            var state = m_state_under_pointer;

            if (m_key == Keys.Left)
            {
                var item = new InOutBaseData() { target_state = state };
                ViewUtil.SetPointerOnBranch(true,item);
                return;
            }

            var has_nextstate = false;
            var has_branch    = false;
            var has_gosub     = false;
            _check_branch(state, out has_nextstate,out has_branch, out has_gosub);

            var nextattr = InOutBaseData.ATTRIB.nextstate;
            if (!has_nextstate && has_branch)
            {
                if (has_gosub)
                {
                    nextattr = InOutBaseData.ATTRIB.gosub;
                }
                else
                {
                    nextattr = InOutBaseData.ATTRIB.branch;
                }
            }
            var psedoitem = new InOutBaseData() {
                 attrib = nextattr,
                 target_state = state,
                 branch_index = 0,
                 state = null
            };

            ViewUtil.SetPointerOnBranch(false,psedoitem);
        }
        void set_outflow_next() //下へ
        {
            var state = m_state_with_pin;
            var has_nextstate = false;
            var has_branch    = false;
            var has_gosub     = false;
            _check_branch(state, out has_nextstate,out has_branch, out has_gosub);

            InOutBaseData item = new InOutBaseData();
            item.target_state = state;
            item.attrib       = InOutBaseData.ATTRIB._base;

            if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate)
            {
                if (has_gosub)
                {
                    item.attrib = InOutBaseData.ATTRIB.gosub;
                }
                else
                {
                    if (has_branch)
                    { 
                        item.attrib       = InOutBaseData.ATTRIB.branch;
                        item.branch_index = 0;
                    }
                }
            }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub)
            {
                if (has_branch)
                {
                    item.attrib       = InOutBaseData.ATTRIB.branch;
                    item.branch_index = 0;
                }
                else
                {
                    if (has_nextstate || !has_branch)
                    {
                        item.attrib       = InOutBaseData.ATTRIB.nextstate;
                    }
                }
            }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch)
            {
                var next_branch_index = m_branch_index+1;
                var next_branch_item = m_out_list!=null ? m_out_list.Find(i=>i.attrib == InOutBaseData.ATTRIB.branch && i.branch_index == next_branch_index) : null;
                if (next_branch_item!=null)
                {
                    item.attrib = InOutBaseData.ATTRIB.branch;
                    item.branch_index = next_branch_index;
                }
                else
                {
                    if (has_nextstate)
                    {
                        item.attrib = InOutBaseData.ATTRIB.nextstate;
                    }
                    else if (has_gosub)
                    {
                        item.attrib = InOutBaseData.ATTRIB.gosub;
                    }
                    else
                    {
                        item.attrib       = InOutBaseData.ATTRIB.branch;
                        item.branch_index = 0;
                    }
                }
            }
            if (item.attrib!= InOutBaseData.ATTRIB._base)
            {   
                ViewUtil.SetPointerOnBranch(false, item);
            }
        }
        void set_outflow_prev() //上へ
        {
            var state = m_state_with_pin;
            var has_nextstate = false;
            var has_branch    = false;
            var has_gosub     = false;
            _check_branch(state, out has_nextstate,out has_branch, out has_gosub);

            InOutBaseData item = new InOutBaseData();
            item.target_state = state;
            item.attrib       = InOutBaseData.ATTRIB._base;

            if (m_outflow_attr == InOutBaseData.ATTRIB.nextstate)
            {
                if (has_branch)
                {
                    var d = BranchUtil.GetApiAndLabelListFromState(state);
                    if (d!=null)
                    {
                        item.attrib = InOutBaseData.ATTRIB.branch;
                        item.branch_index = d.Count -1;
                    }
                }
                else
                {
                    if (has_gosub)
                    {
                        item.attrib = InOutBaseData.ATTRIB.gosub;
                    }
                } 

            }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.gosub)
            {
                if (has_nextstate || !has_branch)
                {
                    item.attrib = InOutBaseData.ATTRIB.nextstate;
                }
                else
                {
                    if (has_branch)
                    {
                        var d = BranchUtil.GetApiAndLabelListFromState(state);
                        if (d!=null)
                        {
                            item.attrib = InOutBaseData.ATTRIB.branch;
                            item.branch_index = d.Count -1;
                        }
                    }
                }
            }
            else if (m_outflow_attr == InOutBaseData.ATTRIB.branch)
            {
                var prev_branch_index = m_branch_index-1;
                if (prev_branch_index >= 0)
                {
                    item.attrib = InOutBaseData.ATTRIB.branch;
                    item.branch_index = prev_branch_index;
                }
                else
                { 
                    if (has_gosub)
                    {
                        item.attrib = InOutBaseData.ATTRIB.gosub;
                    }
                    else
                    {
                        if (has_nextstate)
                        {
                            item.attrib = InOutBaseData.ATTRIB.nextstate;
                        }
                        else
                        {
                            var d = BranchUtil.GetApiAndLabelListFromState(state);
                            if (d!=null)
                            {
                                item.attrib = InOutBaseData.ATTRIB.branch;
                                item.branch_index = d.Count -1;
                            }
                        }
                    }

                }
            }
            if (item.attrib!= InOutBaseData.ATTRIB._base)
            {   
                ViewUtil.SetPointerOnBranch(false, item);
            }
        }
        bool is_group()
        {
            return AltState.IsAltState(m_state_with_pin);
        }
        void move_pointer()
        {
            var x = 0;
            var y = 0;
            var d = 10;
            switch(m_key)
            {
                case Keys.Up:    y -= d; break;
                case Keys.Right: x += d;  break;
                case Keys.Down:  y += d;  break;
                case Keys.Left:  x -= d;  break;
            }

            Cursor.Position = PointUtil.Add_XY(Cursor.Position, x,y);
        }
    }
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

