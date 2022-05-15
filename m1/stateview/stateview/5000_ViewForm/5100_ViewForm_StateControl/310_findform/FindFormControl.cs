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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>
using stateview._5000_ViewForm.dialog;

using stateview;

public partial class FindFormControl  {

    void exec_find()
    {
        var statelist = G.excel_program.GetStateList();
        var namelist = G.excel_program.GetNameList();

        //ctsとotsのnamelist hash作成
        var hash_ots = new List<string>();
        var hash_cts = new List<string>();
        foreach(var n in namelist)
        {
            if (string.IsNullOrEmpty(n)) continue;
            if (n == G.STATENAME_state) continue;
                        
            if (Array.FindIndex(G.STATENAME_ALLRESERVES,i=>i==n)<0 || !n.StartsWith("!"))
            {
                hash_cts.Add(n);
            }
            else
            {
                hash_ots.Add(n);
            }
        }

        var resultdic = new Dictionary<string,string>();
        string result = null;
        foreach (var st in statelist)
        {
            if (AltState.IsAltState(st)) continue;
            var dir = G.node_get_dirpath(st);
            var pos = G.excel_program.GetString(st,G.STATENAMESYS_pos);
            string sr = null;
            Action<string> sradd = (i) => { if (sr != null) sr += Environment.NewLine; sr +="  " + i; };
            Action<string,string> dicadd =(i,j) => { if (!resultdic.ContainsKey(i)) resultdic.Add(i,j); };

            if (iscb_st)
            {
                if (exec_find_sub(st))
                {
                    sradd("found in [state]");
                    dicadd(st,dir);
                }
            }
            if (iscb_cts)
            {
                foreach(var n in hash_cts)
                {
                    if (exec_find_sub(G.excel_program.GetString(st,n)))
                    {
                        sradd(string.Format("found in [{0}]", n));
                        dicadd(st,dir);
                    }
                }
            }
            if (iscb_ots)
            {
                foreach (var n in hash_ots)
                {
                    if (exec_find_sub(G.excel_program.GetString(st,n)))
                    {
                        sradd(string.Format("foind in [{0}]", n));
                        dicadd(st,dir);
                    }
                }
            }
            if (sr!=null)
            {
                var sr2 = "* FOUND IN STATE : " + dir + st + " AT " + pos + Environment.NewLine;
                sr2 += sr;
                if (result!=null) result += Environment.NewLine;
                result += sr2;
            }
        }
        if (result == null)
        {
            result = "not found";
        }
        m_form.tb_result.Text = result;
        

        m_form.dataGridView1.Rows.Clear();
        m_form.label_result.Visible = false;
        if (resultdic.Count==0)
        {
            m_form.label_result.Text = "Not Found";
            m_form.label_result.Visible = true;
        }
        else
        { 
            foreach(var i in resultdic)
            { 
                var cmt = G.excel_program.GetString(i.Key,"state-cmt");
                m_form.dataGridView1.Rows.Add( i.Key, i.Value,cmt  );
            }
        }
    }
    bool exec_find_sub(string s)
    {
        if (string.IsNullOrEmpty(s)) return false;

        var text = m_form.combox_text.Text;
        if (iscb_rx)
        {
            return !string.IsNullOrEmpty( RegexUtil.Get1stMatch(text,s) );
        }
        if (iscb_cs)
        {
            s = s.ToUpper();
            text = text.ToUpper();
        }
        if (iscb_wd)
        {
            var t2 = @"\b" + text + @"\b";
            return !string.IsNullOrEmpty(RegexUtil.Get1stMatch(t2, s));
        }
        else
        {
            return s.Contains(text);
        }
    }

    void save_hist()
    {
        var text = m_form.combox_text.Text.Trim();
        if (string.IsNullOrEmpty(text)) return;
        var list = new List<string>(); foreach(var i in m_form.combox_text.Items) list.Add(i.ToString());
        var idx = list.FindIndex(i=>i==text);
        if (idx >= 0)
        {
            list.RemoveAt(idx);
        }
        list.Insert(0,text);
        
        m_form.combox_text.Items.Clear();
        list.ForEach(i=>m_form.combox_text.Items.Add(i));
    }

    #region manger
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;
    Action<bool> m_tempfunc;

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
    // for tempfunc
    void SetNextState(Action<bool> func)
    {
        m_tempfunc = func;
    }
    void GoNextState()
    {
        m_nextfunc = m_tempfunc;
        m_tempfunc = null;
    }
    bool HasNextState()
    {
        return m_tempfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
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

	#region    // [SYN-G-GEN OUTPUT START] indent(8) $/./$
        //             psggConverterLib.dll converted from psgg-file:doc\FindFormControl.psgg

        /*
            E_CB_ITEMS
        */
        bool ce(string s)
        {
            return m_curevt == s;
        }
        /*
            E_CBENABLED
        */
        void encb_cs(bool b) { m_form.cb_case.Enabled = b; }
        void encb_wd(bool b) { m_form.cb_word.Enabled = b; }
        /*
            E_CHGCB
        */
        bool chkcb_st()   { var b = sv_st != iscb_st; sv_st = iscb_st; return b; }
        bool chkcb_cts()   { var b = sv_cts != iscb_cts; sv_cts = iscb_cts; return b; }
        bool chkcb_ots()   { var b = sv_ots != iscb_ots; sv_ots = iscb_ots; return b; }
        bool chkcb_all()   { var b = sv_all != iscb_all; sv_all = iscb_all; return b; }
        bool chkcb_cs()   { var b = sv_cs != iscb_cs; sv_cs = iscb_cs; return b; }
        bool chkcb_wd()   { var b = sv_wd != iscb_wd; sv_wd = iscb_wd; return b; }
        bool chkcb_rx()   { var b = sv_rx != iscb_rx; sv_rx = iscb_rx; return b; }
        /*
            E_CHKCHG
        */
        bool chg_st;
        bool chg_cts;
        bool chg_ots;
        bool chg_all;
        bool chg_cs;
        bool chg_wd;
        bool chg_rx;
        /*
            E_CHKCHG_ALL
        */
        bool chkchg_all()
        {
            chg_st = chkcb_st();
            chg_cts= chkcb_cts();
            chg_ots= chkcb_ots();
            chg_all = chkcb_all();
            chg_cs = chkcb_cs();
            chg_wd = chkcb_wd();
            chg_rx = chkcb_rx();
            return chg_st || chg_cts || chg_ots || chg_all || chg_cs || chg_wd || chg_rx;
        }
        /*
            E_CONST
        */
        public readonly string E_BTNFIND="E_BTNFIND";
        /*
            E_CTLCBALL
        */
        void ctl_cball()
        {
            var at = iscb_st && iscb_cts && iscb_ots;
            var af = !iscb_st && !iscb_cts && !iscb_ots;
            if (at) iscb_all = true;
            if (af) iscb_all = false;
        }
        /*
            E_EVENTLIST
        */
        Queue<string> m_eventlist = new Queue<string>();
        public void event_add(string evt)
        {
            m_eventlist.Enqueue(evt);
        }
        public string event_get()
        {
            if (m_eventlist.Count > 0) return m_eventlist.Dequeue();
            return null;
        }
        /*
            E_FORM
        */
        public FindForm m_form;
        /*
            E_ISCB
        */
        bool iscb_st { get { return cb_st.Checked; } set { cb_st.Checked = value; } }
        bool iscb_cts { get { return cb_cts.Checked; } set { cb_cts.Checked = value; } }
        bool iscb_ots { get { return cb_ots.Checked; } set { cb_ots.Checked = value; } }
        bool iscb_all { get { return cb_all.Checked; } set { cb_all.Checked = value; } }
        bool iscb_cs { get { return cb_cs.Checked; } set { cb_cs.Checked = value; } }
        bool iscb_wd { get { return cb_wd.Checked; } set { cb_wd.Checked = value; } }
        bool iscb_rx { get { return cb_rx.Checked; } set { cb_rx.Checked = value; } }
        /*
            E_SAVE_CB
        */
        bool sv_st;
        bool sv_cts;
        bool sv_ots;
        bool sv_all;
        bool sv_cs;
        bool sv_wd;
        bool sv_rx;
        /*
            E_VAR_CB_XX
        */
        CheckBox cb_st { get { return m_form.cb_state; }}
        CheckBox cb_cts { get { return m_form.cb_contents; }}
        CheckBox cb_ots { get { return m_form.cb_others; }}
        CheckBox cb_all { get { return m_form.cb_all; }}
        CheckBox cb_cs { get { return m_form.cb_case; }}
        CheckBox cb_wd { get { return m_form.cb_word; }}
        CheckBox cb_rx { get { return m_form.cb_regex; }}
        /*
            E_VARS
        */
        string m_curevt;
        /*
            S_BACKTO_WAIT_EVT
        */
        void S_BACKTO_WAIT_EVT(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_TICK);
            }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_BTNFIND1
        */
        void S_BTNFIND1(bool bFirst)
        {
            if (bFirst)
            {
                exec_find();
                save_hist();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_BACKTO_WAIT_EVT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CBALL
        */
        void S_CBALL(bool bFirst)
        {
            if (bFirst)
            {
                ctl_cball();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT1);
            }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CBALL1
        */
        void S_CBALL1(bool bFirst)
        {
            if (bFirst)
            {
                iscb_st = iscb_cts = iscb_ots = iscb_all;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT1);
            }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CE_ITEMGROUP
        */
        void S_CE_ITEMGROUP(bool bFirst)
        {
            // branch
            if (chg_st) { SetNextState( S_CBALL ); }
            else if (chg_cts) { SetNextState( S_CBALL ); }
            else if (chg_ots) { SetNextState( S_CBALL ); }
            else if (chg_all) { SetNextState( S_CBALL1 ); }
            else { SetNextState( S_CS_OPTS ); }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CHK_EVT
        */
        void S_CHK_EVT(bool bFirst)
        {
            m_curevt = event_get();
            // branch
            if (ce(E_BTNFIND)) { SetNextState( S_BTNFIND1 ); }
            else { SetNextState( S_CHKCHG ); }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CHKCHG
        */
        void S_CHKCHG(bool bFirst)
        {
            var bchg = false;
            bchg = chkchg_all();
            // branch
            if (bchg) { SetNextState( S_CE_ITEMGROUP ); }
            else { SetNextState( S_BACKTO_WAIT_EVT ); }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CS_OPTS
        */
        void S_CS_OPTS(bool bFirst)
        {
            // branch
            if (chg_rx) { SetNextState( S_CS_OPTS1 ); }
            else { SetNextState( S_BACKTO_WAIT_EVT ); }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_CS_OPTS1
        */
        void S_CS_OPTS1(bool bFirst)
        {
            if (bFirst)
            {
                var b = !iscb_rx;
                encb_cs(b);
                encb_wd(b);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT1);
            }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INIT
        */
        void S_INIT(bool bFirst)
        {
            if (bFirst)
            {
                chkchg_all();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_TICK);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INIT1
        */
        void S_INIT1(bool bFirst)
        {
            if (bFirst)
            {
                chkchg_all();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_BACKTO_WAIT_EVT);
            }
            //
            if (HasNextState())
            {
                NoWait();
                GoNextState();
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
                SetNextState(S_INIT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_TICK
        */
        void S_TICK(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_CHK_EVT);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }


	#endregion // [SYN-G-GEN OUTPUT END]

	// write your code below

	bool m_bYesNo;
	
	void br_YES(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (m_bYesNo)
			{
				SetNextState(st);
			}
		}
	}

	void br_NO(Action<bool> st)
	{
		if (!HasNextState())
		{
			if (!m_bYesNo)
			{
				SetNextState(st);
			}
		}
	}

}
