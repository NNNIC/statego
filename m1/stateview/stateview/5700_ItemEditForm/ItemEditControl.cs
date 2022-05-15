using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using stateview;
using System.Windows.Forms;
using G = stateview.Globals;

public partial class ItemEditControl  {
   
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
			if (IsEnd()) break;
			
			Update();
		}
	}

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from ItemEditControl.xlsx.    psgg-file:doc\ItemEditControl.psgg
        /*
            E_GETTER
        */
        public DataGridView m_dg { get { return m_form.dataGridView1; } }
        /*
            E_IMPORT
        */
        public Mode  m_mode;
        public ItemEditForm m_form;
        public string m_evt;
        public int m_row;
        public int m_col;
        public string m_itemname;
        /*
            E_MODE
        */
        public enum Mode {
            none,
            onload,
            tick,
        }
        /*
            S_CANCEL
        */
        void S_CANCEL(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_CHECKMODE
        */
        void S_CHECKMODE(bool bFirst)
        {
            // branch
            if (m_mode==Mode.onload) { Goto( S_GETINI ); }
            else if (m_mode==Mode.tick) { Goto( S_INIT ); }
            else { Goto( S_MODENONE ); }
        }
        /*
            S_CHECKOFF
        */
        void S_CHECKOFF(bool bFirst)
        {
            //
            if (bFirst)
            {
                check_off();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_CHECKON
        */
        void S_CHECKON(bool bFirst)
        {
            //
            if (bFirst)
            {
                check_on();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_CLICK_OK
        */
        void S_CLICK_OK(bool bFirst)
        {
            //
            if (bFirst)
            {
                click_ok();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_CONDCHG
        */
        void S_CONDCHG(bool bFirst)
        {
            //
            if (bFirst)
            {
                cond_chg();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_CONTEXTMENU
        */
        void S_CONTEXTMENU(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_evt=null;
                open_contextmenu();
                //m_form.contextMenuStrip_main.Show(Cursor.Position);
            }
            if (m_evt==null) return;
            // branch
            if (m_evt=="msm_closing") { Goto( S_INIT ); }
            else if (m_evt=="msm_edit") { Goto( S_EDIT ); }
            else if (m_evt=="msm_condchg") { Goto( S_CONDCHG ); }
            else if (m_evt=="msm_checkon") { Goto( S_CHECKON ); }
            else if (m_evt=="msm_checkoff") { Goto( S_CHECKOFF ); }
            else if (m_evt=="msm_insert") { Goto( S_INSERT ); }
            else if (m_evt=="msm_remove") { Goto( S_REMOVE ); }
            else if (m_evt=="msm_up") { Goto( S_UP ); }
            else if (m_evt=="msm_down") { Goto( S_DOWN ); }
            else if (m_evt=="msm_cancel") { Goto( S_CANCEL ); }
            else { Goto( S_CANCEL ); }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_DOWN
        */
        void S_DOWN(bool bFirst)
        {
            //
            if (bFirst)
            {
                down_row();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_EDIT
        */
        void S_EDIT(bool bFirst)
        {
            //
            if (bFirst)
            {
                open_input_start();
            }
            if (!open_input_done()) return;
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_GETHELP
        */
        public Help m_help;
        void S_GETHELP(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_help = get_help();
            }
            //
            if (!HasNextState())
            {
                Goto(S_MAKE_TOPBAR);
            }
        }
        /*
            S_GETINI
        */
        public Info m_info;
        void S_GETINI(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_info = get_info();
            }
            //
            if (!HasNextState())
            {
                Goto(S_GETHELP);
            }
        }
        /*
            S_HIDE_UNUSED
            2019.9.23
            不要になったカラムを消す
        */
        void S_HIDE_UNUSED(bool bFirst)
        {
            //
            if (bFirst)
            {
                hide_unused();
            }
            //
            if (!HasNextState())
            {
                Goto(S_MODENONE);
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
                m_evt=null;
            }
            //
            if (!HasNextState())
            {
                Goto(S_WAITEVNT);
            }
        }
        /*
            S_INSERT
        */
        void S_INSERT(bool bFirst)
        {
            //
            if (bFirst)
            {
                open_new_start();
            }
            if (!open_new_done()) return;
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_IS_MODIFIABLE
        */
        void S_IS_MODIFIABLE(bool bFirst)
        {
            var b = is_modifiable_row();
            // branch
            if (b) { Goto( S_CONTEXTMENU ); }
            else { Goto( S_INIT ); }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_LOCALIZE
        */
        void S_LOCALIZE(bool bFirst)
        {
            //
            if (bFirst)
            {
                localize();
            }
            //
            if (!HasNextState())
            {
                Goto(S_HIDE_UNUSED);
            }
        }
        /*
            S_MAKE_BORDER
        */
        void S_MAKE_BORDER(bool bFirst)
        {
            //
            if (bFirst)
            {
                make_borader();
            }
            //
            if (!HasNextState())
            {
                Goto(S_MAKE_ITEMROWS);
            }
        }
        /*
            S_MAKE_ITEMROWS
        */
        void S_MAKE_ITEMROWS(bool bFirst)
        {
            //
            if (bFirst)
            {
                make_itemrows();
            }
            //
            if (!HasNextState())
            {
                Goto(S_LOCALIZE);
            }
        }
        /*
            S_MAKE_TOPBAR
        */
        //string[] m_slist = new string[]{  "S_", "E_", "C_" };
        void S_MAKE_TOPBAR(bool bFirst)
        {
            //
            if (bFirst)
            {
                make_topheader();
                make_toprows();
            }
            //
            if (!HasNextState())
            {
                Goto(S_MAKE_BORDER);
            }
        }
        /*
            S_MODENONE
        */
        void S_MODENONE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_mode = Mode.none;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_REMOVE
        */
        void S_REMOVE(bool bFirst)
        {
            //
            if (bFirst)
            {
                ask_delete_start();
            }
            if (!ask_delete_done()) return;
            var b = ask_delete_result();
            if (b)
            {
                remove_row();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_SETUPVAL
        */
        void S_SETUPVAL(bool bFirst)
        {
            //
            if (bFirst)
            {
                cellclick_init();
            }
            //
            if (!HasNextState())
            {
                Goto(S_IS_MODIFIABLE);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_CHECKMODE);
            }
        }
        /*
            S_UP
        */
        void S_UP(bool bFirst)
        {
            //
            if (bFirst)
            {
                up_row();
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT);
            }
        }
        /*
            S_WAITEVNT
        */
        void S_WAITEVNT(bool bFirst)
        {
            if (m_evt==null) return;
            // branch
            if (m_evt == "cellclick") { Goto( S_SETUPVAL ); }
            else if (m_evt=="clickok") { Goto( S_CLICK_OK ); }
            else { Goto( S_INIT ); }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }


	#endregion // [PSGG OUTPUT END]

	// write your code below

	bool m_bYesNo;
	
	void br_YES(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (m_bYesNo)
			{
				Goto(st);
			}
		}
	}

	void br_NO(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (!m_bYesNo)
			{
				Goto(st);
			}
		}
	}

    void hide_unused()
    {
        m_form.dataGridView1.Rows[0].Visible = false; // C_
        m_form.dataGridView1.Rows[1].Visible = false; // E_
        m_form.dataGridView1.Rows[2].Visible = false; // S_
        m_form.dataGridView1.Rows[3].Visible = false; // S_

        m_form.dataGridView1.Columns[2].Visible = false; // attr
        m_form.dataGridView1.Columns[3].Visible = false; // condition
        m_form.dataGridView1.Columns[4].Visible = false; //s0
        m_form.dataGridView1.Columns[5].Visible = false; //s1
        m_form.dataGridView1.Columns[6].Visible = false; //s2
    }


    public void insert_item_to_dg(string itemname, int row, string helpen, string helpjp, string method)
    {
        var dg = m_dg;
        dg.Rows.Insert(row, 1);

        dg[CC_INDEX, row].Value = NEWMARK;
        dg[CC_NAME, row].Value = itemname;
        dg[CC_COND, row].Value = ItemEditControl.Cond.exclusion.ToString();
        dg[CC_S0, row].Value   = CHECKMARK;
        dg[CC_HELPEN, row].Value = helpen;
        dg[CC_HELPJP, row].Value = helpjp;
        dg[CC_METHOD, row].Value = method;
    }

    void localize()
    {
        var dg = m_dg;
        dg.Columns[CC_INDEX].HeaderText   = G.Localize("ief_index"); //"インデックス";
        dg.Columns[CC_NAME].HeaderText    = G.Localize("ief_name");  // "アイテム名";
        dg.Columns[CC_HELPEN].HeaderText  = G.Localize("ief_helpen");//"説明(英語)";
        dg.Columns[CC_HELPJP].HeaderText  = G.Localize("ief_helpjp");//"説明(日本語)";
        dg.Columns[CC_METHOD].HeaderText  = G.Localize("ief_method");//"メソッド";
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

