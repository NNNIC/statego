using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using stateview;
using System.IO;
using G=stateview.Globals;
public partial class EditForm_nextForm3Control  {
   
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

    public void RunEvent_init()
    {
        Start();
		int LOOPMAX = (int)(1E+5);
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (m_busy==false) break;
		}
    }
    public void RunEvent(EVENT evt)
    {
        if (m_busy == true) return;

        m_evt = evt;
		int LOOPMAX = (int)(1E+5);
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (m_busy==false) break;
		}
    }

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from EditForm_nextForm3Control.xlsx.    psgg-file:EditForm_nextForm3Control.psgg
        /*
            E_0001
        */
        public bool m_busy { get; private set; }
        /*
            E_0002
        */
        public enum EVENT
        {
            none,
            onload,
            btn_ok,
            btn_cancel,
            btn_clear,
            rb_none,
            rb_gosub,
            rb_substart,
            rb_subreturn,
            rb_basestate,
            lbl_focusclose
        }
        public EVENT m_evt;
        /*
            E_0003
        */
        public stateview._5300_EditForm.EditForm_nextForm3 m_form;
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_ERROR
        */
        void S_ERROR(bool bFirst)
        {
            //
            if (bFirst)
            {
                throw new SystemException("{D78A774A-01AE-4569-9E6D-E7801CC569FF}");
            }
        }
        /*
            S_GOBACK
        */
        void S_GOBACK(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_evt = EVENT.none;
            }
            //
            if (!HasNextState())
            {
                Goto(S_WAIT);
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
                m_busy = true;
                m_evt = EVENT.none;
            }
            //
            if (!HasNextState())
            {
                Goto(S_WAIT1);
            }
        }
        /*
            S_NOTHING
            なにもしない
        */
        void S_NOTHING(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_ONLOAD
        */
        void S_ONLOAD(bool bFirst)
        {
            //
            if (bFirst)
            {
                onload();
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_SHOWALL
        */
        void S_SHOWALL(bool bFirst)
        {
            //
            if (bFirst)
            {
                filleterd_view();
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_SHOWBASESTATE
        */
        void S_SHOWBASESTATE(bool bFirst)
        {
            //
            if (bFirst)
            {
                var baselist = new List<string>();
                G.state_working_list.ForEach(s=> {
                    var basestate  =G.excel_program.GetString(s,G.STATENAME_basestate);
                    if (!baselist.Contains(basestate)) baselist.Add(s);
                });
                filleterd_view(s=> {
                    if (string.IsNullOrEmpty(s)) return false;
                    return baselist.Contains(s);
                });
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_SHOWGOSUB
        */
        void filltered_gosub_view()
        {
            filleterd_view(s=> {
                if (string.IsNullOrEmpty(s)) return false;
                var typ  =G.excel_program.GetString(s,G.STATENAME_statetyp);
                return typ == WordStorage.Store.state_typ_gosub;
            });
        }
        void S_SHOWGOSUB(bool bFirst)
        {
            //
            if (bFirst)
            {
                filltered_gosub_view();
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_SHOWSUBRETURN
        */
        void S_SHOWSUBRETURN(bool bFirst)
        {
            //
            if (bFirst)
            {
                filleterd_view(s=> {
                    if (string.IsNullOrEmpty(s)) return false;
                    var typ  =G.excel_program.GetString(s,G.STATENAME_statetyp);
                    return typ == WordStorage.Store.state_typ_subreturn;
                });
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_SHOWSUBSTART
        */
        void filltered_substart_view() {
            filleterd_view(s=> {
                if (string.IsNullOrEmpty(s)) return false;
                var typ  =G.excel_program.GetString(s,G.STATENAME_statetyp);
                return typ == WordStorage.Store.state_typ_substart;
            });
        }
        void S_SHOWSUBSTART(bool bFirst)
        {
            //
            if (bFirst)
            {
                filltered_substart_view();
            }
            //
            if (!HasNextState())
            {
                Goto(S_GOBACK);
            }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            Goto(S_INIT);
            NoWait();
        }
        /*
            S_WAIT
        */
        void S_WAIT(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_busy = false;
            }
            if (m_evt == EVENT.none) return;
            m_busy = true;
            // branch
            if (m_evt == EVENT.rb_none) { Goto( S_SHOWALL ); }
            else if (m_evt == EVENT.rb_gosub) { Goto( S_SHOWGOSUB ); }
            else if (m_evt == EVENT.rb_substart) { Goto( S_SHOWSUBSTART ); }
            else if (m_evt == EVENT.rb_subreturn) { Goto( S_SHOWSUBRETURN ); }
            else if (m_evt == EVENT.rb_basestate) { Goto( S_SHOWBASESTATE ); }
            else { Goto( S_NOTHING ); }
        }
        /*
            S_WAIT1
        */
        void S_WAIT1(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_busy = false;
            }
            if (m_evt == EVENT.none) return;
            m_busy = true;
            // branch
            if (m_evt == EVENT.onload) { Goto( S_ONLOAD ); }
            else { Goto( S_ERROR ); }
        }


	#endregion // [PSGG OUTPUT END]

	// write your code below

    void onload()
    {
        WordStorage.Res.ChangeAll(this,G.system_lang);

        m_form.DialogResult = DialogResult.None;
        if (!FormUtil.SetCenterInForm(m_form,m_form.m_parent))//this.Location = Cursor.Position;
        {
            m_form.Location = Cursor.Position;
        }

        if (m_form.m_brinfo!=null && m_form.m_brinfo.m_branchpoint_isNextStateOrBranchOrGosub == 3)
        {
            m_form.radioButtonSubStart.Checked = true;
            filltered_substart_view();
        }
        else
        { 
            filleterd_view();
        }
        //m_form.dataGridView1.Rows.Clear();
        //foreach(var s in G.state_working_list)
        //{
        //    if (stateview.AltState.IsAltState(s)) continue;
        //    var path = G.node_get_dirpath(s);
        //    var cmt  = G.excel_program.GetString(s,G.STATENAME_statecmt);
        //    m_form.dataGridView1.Rows.Add(path,s,cmt);
        //}
        //m_form.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        m_form.textBox1.Text = m_form.m_text;

        m_form.m_center_focus = false;
    }
    void filleterd_view(Func<string, bool> func=null)
    {
        m_form.dataGridView1.Rows.Clear();
        foreach(var s in G.state_working_list)
        {
            if (stateview.AltState.IsAltState(s)) continue;
            if (func!=null)
            {
                if (func(s)==false) continue;
            }

            var path = G.node_get_dirpath(s);
            var cmt  = G.excel_program.GetString(s,G.STATENAME_statecmt);
            m_form.dataGridView1.Rows.Add(path,s,cmt);
        }
        m_form.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

