using System;
using System.Collections.Generic;
using G=stateview.Globals;
public partial class FindLineControl  {

    List<string> split(string s)
    {
        var newchars = StringUtil.FindNewLineChar(s);
        if (string.IsNullOrEmpty(newchars)) return new List<string>() {s };

        var newlinechar = newchars.Length == 2 ? newchars[1] : newchars[0];

        var lines = StringUtil.SplitTrimSpaces(s,newlinechar);
        return new List<string>(lines);
    }

    List<string> split_wo_donotedit_mark(string s)
    {
        var newchars = StringUtil.FindNewLineChar(s);
        if (string.IsNullOrEmpty(newchars)) return new List<string>() {s };

        var newlinechar = newchars.Length == 2 ? newchars[1] : newchars[0];

        var lines = StringUtil.SplitTrimSpaces(s,newlinechar);
        var lines2 = new List<string>();
        var donoteditmark = G.CMTLINE_DONOTENDMARK();
        foreach (var l in lines)
        {
            var l2 = l;
            if (!string.IsNullOrEmpty(l2))
            {
                l2 = l2.Replace(donoteditmark, "");
                l2 = StringUtil.TrimSpaces(l2);
            }
            lines2.Add(l2);
        }
        return lines2;     
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
        //             psggConverterLib.dll converted from psgg-file:doc\FindLineControl.psgg

        /*
            E_0003
        */
        List<string> m_tlines;
        List<string> m_flines;
        /*
            E_0004
        */
        int m_ti; //target index;
        int m_fi; //find index
        int m_fimax; //マッチしたfiの最大値
        string m_ts; //ターゲットの１行
        string m_fs; //Findの１行
        /*
            E_0005
        */
        string getts(int n) {
            return n < m_tlines.Count ? m_tlines[n] : null;
        }
        /*
            E_IMPORT
        */
        public string m_target_text;
        public string m_find_text;
        /*
            E_OUTPUT
        */
        public int m_linenum;
        /*
            S_CHECK_FI
        */
        void S_CHECK_FI(bool bFirst)
        {
            // branch
            if (m_fi < m_flines.Count) { SetNextState( S_CHECK04 ); }
            else { SetNextState( S_MATCHED ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CHECK04
        */
        void S_CHECK04(bool bFirst)
        {
            if (bFirst)
            {
                m_ts = getts(m_ti + m_fi);
                m_fs = m_flines[m_fi];
            }
            // branch
            if (m_fs == m_ts) { SetNextState( S_REC_MAX ); }
            else { SetNextState( S_INC_TI ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONVERT_TO_LINES
        */
        void S_CONVERT_TO_LINES(bool bFirst)
        {
            if (bFirst)
            {
                m_tlines = split_wo_donotedit_mark(m_target_text);
                m_flines = split(m_find_text);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_LOOP_INIT);
            }
            //
            if (HasNextState())
            {
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
            S_INC_FI
        */
        void S_INC_FI(bool bFirst)
        {
            if (bFirst)
            {
                m_fi++;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CHECK_FI);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INC_TI
        */
        void S_INC_TI(bool bFirst)
        {
            if (bFirst)
            {
                m_ti++;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_LOOP_CHECK);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INIT_FI
        */
        void S_INIT_FI(bool bFirst)
        {
            if (bFirst)
            {
                m_fi = 0;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CHECK_FI);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_LOOP_CHECK
        */
        void S_LOOP_CHECK(bool bFirst)
        {
            // branch
            if (m_ti < m_tlines.Count) { SetNextState( S_INIT_FI ); }
            else { SetNextState( S_END ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_LOOP_INIT
        */
        void S_LOOP_INIT(bool bFirst)
        {
            if (bFirst)
            {
                m_linenum = -1;
                m_ti = 0;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_LOOP_CHECK);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_MATCHED
        */
        void S_MATCHED(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_END);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_REC_MAX
        */
        void S_REC_MAX(bool bFirst)
        {
            if (bFirst)
            {
                if (m_fi > m_fimax)
                {
                    m_fimax = m_fi;
                    m_linenum = m_ti;
                }
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INC_FI);
            }
            //
            if (HasNextState())
            {
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
                SetNextState(S_CONVERT_TO_LINES);
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
