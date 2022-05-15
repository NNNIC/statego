using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public partial class CollectControl  {
   
    //public CancellationToken m_token;

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
        //             psggConverterLib.dll converted from psgg-file:doc\CollectControl.psgg

        /*
            E_INPUTTEXT
        */
        public string m_input;
        public string m_input_low { get { return m_input?.ToLower();   } }
        /*
            E_RESULT
        */
        public List<string> m_result_list;
        /*
            S_CHECK_HISTORY
            ヒストリリストのファイル名部分のヒット確認して、リスト化。
        */
        void S_CHECK_HISTORY(bool bFirst)
        {
            //
            if (bFirst)
            {
                collect_in_history();
            }
            //
            if (!HasNextState())
            {
                Goto(S_TRAVERSE);
            }
        }
        /*
            S_CHECK_INPUT
        */
        void S_CHECK_INPUT(bool bFirst)
        {
            // branch
            if (string.IsNullOrEmpty(m_input)) { Goto( S_END ); }
            else { Goto( S_CHECK_HISTORY ); }
        }
        /*
            S_CLEAR_LIST
            結果リストをクリア
        */
        void S_CLEAR_LIST(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_parent.result_clear();
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_INPUT);
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_CLEAR_LIST);
            }
        }
        /*
            S_TRAVERSE
            ヒストリのパスを使ってトラバースしてリスト化
        */
        void S_TRAVERSE(bool bFirst)
        {
            //
            if (bFirst)
            {
                traverse();
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }


    #endregion // [PSGG OUTPUT END]

    public FileCandidateControl m_parent;

    void collect_in_history()
    {
        var historylist = StateViewer_starter2.Starter2.GetHistory();
        if (historylist!=null)
        {
            foreach(var s in historylist)
            { 
                var file = Path.GetFileName(s)?.ToLower();
                if (file!=null && file.Contains(m_input_low))
                {
                    m_parent.result_add(s);
                }
            }
        }
        
    }
    void traverse()
    {
        var historylist = StateViewer_starter2.Starter2.GetHistory();
        if (historylist == null) return;
        var checkedpathlist = new List<string>();
        foreach(var s in historylist)
        {
            var path = Path.GetDirectoryName(s);

            if (checkedpathlist.Contains(path)) continue;

            var file = m_input_low;
            var extention = Path.GetExtension(m_input_low);
            if (extention != ".psgg")
            {
                file = m_input_low + ".psgg";
            }
            var err = "";
            var list = PathUtil.CollectTraverseDownAndUp(path,file,2000,out err);
            if (list!=null && list.Count>0)
            {
                
                list.ForEach(i=> {
                    var f = PathUtil.GetAcculateFileName(i);
                    if (f!=null) m_parent.result_add(f);
                }
                );
            }

            checkedpathlist.Add(path);
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

