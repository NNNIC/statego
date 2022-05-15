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

namespace stateview {

    public partial class EnterKeyControl  {
   
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
//  psggConverterLib.dll converted from EnterKeyControl.xlsx.    psgg-file:EnterKeyControl.psgg
        /*
            E_INPUT
        */
        public Point  m_pointer;
        public string m_state_under_pointer;
        public string m_state_with_pin;
        public bool   m_in_or_outflow_pin;
        public InOutBaseData.ATTRIB m_outflow_attr;
        public int    m_branch_index;
        public string m_nextstate;
        /*
            S_BLANKMENU
            ブランクメニュー表示
        */
        void S_BLANKMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.open_contextmenu;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_CHECK_FOCUS
            フォーカス確認
        */
        void S_CHECK_FOCUS(bool bFirst)
        {
            var b = is_state_focused();
            // branch
            if (b==false) { Goto( S_SET_FOCUS ); }
            else { Goto( S_CHECK_PLALERSELECT ); }
        }
        /*
            S_CHECK_FOCUS_SOME
            何かフォーカス中
        */
        void S_CHECK_FOCUS_SOME(bool bFirst)
        {
            var b = has_focuses();
            // branch
            if (b==false) { Goto( S_BLANKMENU ); }
            else { Goto( S_OFF_FOCUS ); }
        }
        /*
            S_CHECK_FOCUS1
            フォーカス確認
        */
        void S_CHECK_FOCUS1(bool bFirst)
        {
            var b = is_groupnode_focused();
            // branch
            if (b==false) { Goto( S_SET_FOCUS1 ); }
            else { Goto( S_CHECK_PLALERSELECT1 ); }
        }
        /*
            S_CHECK_PLALERSELECT
            複数選択中か
        */
        void S_CHECK_PLALERSELECT(bool bFirst)
        {
            var b = is_state_in_plural_selected();
            // branch
            if (b) { Goto( S_PLALERMENU ); }
            else { Goto( S_STATEMENU ); }
        }
        /*
            S_CHECK_PLALERSELECT1
            複数選択中か
        */
        void S_CHECK_PLALERSELECT1(bool bFirst)
        {
            var b = is_groupnode_in_plural_selected();
            // branch
            if (b) { Goto( S_PLALERMENU ); }
            else { Goto( S_GROUPMENU ); }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_GROUPMENU
            グループノードメニュー表示
        */
        void S_GROUPMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.open_contextmenu;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_INOUTMENU
            可能であれば、IN/.OUTMENU表示
        */
        void S_INOUTMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.open_inout_menu;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_OFF_FOCUS
            フォーカス削除
        */
        void S_OFF_FOCUS(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.focus_clear;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_PLALERMENU
            複数選択用fメニュー表示
        */
        void S_PLALERMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.open_contextmenu;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_SET_FOCUS
            フォーカスする
        */
        void S_SET_FOCUS(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_focus_state();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_SET_FOCUS1
            フォーカスする
        */
        void S_SET_FOCUS1(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_focus_groupnode();
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
            if (is_on_state()) { Goto( S_CHECK_FOCUS ); }
            else if (is_on_groupnode()) { Goto( S_CHECK_FOCUS1 ); }
            else if (is_on_inout_pin()) { Goto( S_INOUTMENU ); }
            else if (is_on_blank()) { Goto( S_CHECK_FOCUS_SOME ); }
            else { Goto( S_END ); }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            Goto(S_SITUATION);
            NoWait();
        }
        /*
            S_STATEMENU
            ステートメニュー表示
        */
        void S_STATEMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                G.keyexec = KEYEXEC.open_contextmenu;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }


	    #endregion // [PSGG OUTPUT END]

        public void reset()
        {
            m_pointer             = Point.Empty;
            m_state_under_pointer = null;
            m_state_with_pin      = null;
            m_in_or_outflow_pin   = false;
            m_outflow_attr        = InOutBaseData.ATTRIB._base;
            m_branch_index        = -1;
            m_nextstate           = null;
        }
        
        bool is_state_focused()
        {
            var state = m_state_under_pointer;
            if (StateUtil.IsValidStateName(state))
            {
                if (!AltState.IsAltState(state))
                {
                    var focuses = G.vf_sc.GetFocusingStates();
                    if (focuses!=null && focuses.Count > 0)
                    {
                        return focuses.Contains(state);
                    }
                }                    
            }
            return false;
        }
        bool is_groupnode_focused()
        {
            var state = m_state_under_pointer;
            if (StateUtil.IsValidStateName(state))
            {
                if (AltState.IsAltState(state))
                {
                    var focuses = G.vf_sc.GetFocusingStates();
                    if (focuses!=null && focuses.Count > 0)
                    {
                        return focuses.Contains(state);
                    }
                }                    
            }
            return false;
        }
        bool is_state_in_plural_selected()
        {
            var focused_list = G.vf_sc.GetFocusingStates();
            if (focused_list!=null && focused_list.Count>1)
            {
                if (focused_list.Contains(m_state_under_pointer))
                {
                    return true;
                }
            }
            return false;
        }
        bool is_groupnode_in_plural_selected()
        {
            var focused_list = G.vf_sc.GetFocusingStates();
            if (focused_list!=null && focused_list.Count>1)
            {
                if (focused_list.Contains(m_state_under_pointer))
                {
                    return true;
                }
            }
            return false;
        }
        bool is_on_state()
        {
            if (StateUtil.IsValidStateName(m_state_under_pointer))
            {
                if (!AltState.IsAltState(m_state_under_pointer))
                {
                    return true;
                }
            }
            return false;
        }
        bool is_on_groupnode()
        {
            if (StateUtil.IsValidStateName(m_state_under_pointer))
            {
                if (AltState.IsAltState(m_state_under_pointer))
                {
                    return true;
                }
            }
            return false;
        }
        bool is_on_inout_pin()
        {
            if (StateUtil.IsValidStateName(m_state_with_pin))
            {
                return true;
            }
            return false;
        }
        bool is_on_blank()
        {
            if (
                !StateUtil.IsValidStateName(m_state_under_pointer)
                &&
                !StateUtil.IsValidStateName(m_state_with_pin)
                )
            {
                return true;
            }
            return false;
        }
        void set_focus_state()
        {
            G.keyexec = KEYEXEC.focus_specified_state;
            G.vf_sc.m_center_focus_state = m_state_under_pointer;
        }
        void set_focus_groupnode()
        {
            G.keyexec = KEYEXEC.focus_specified_state;
            G.vf_sc.m_center_focus_group = AltState.TrimAltStateName( m_state_under_pointer );
        }
        bool has_focuses()
        {
            var focused_list = G.vf_sc.GetFocusingStates();
            if (focused_list!=null && focused_list.Count > 0)
            {
                return true;
            }
            return false;
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

