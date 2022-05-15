using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
public partial class FileCandidateControl  {
   
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

        update_result();
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

	#region    // [PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:doc\FileCandidateControl.psgg

    /*
        E_FLGS
    */
    public bool m_bTxtChg;
    public bool m_bBtnPush;
    /*
        E_FORM
    */
    public StateViewer_starter2.Start2Form m_form;
    /*
        E_RESULT_LIST
    */
    public object LOCKOBJ = 1;
    public List<string> m_result_list;
    public void result_clear()
    {
        lock(LOCKOBJ) {
            m_result_list = null;
            m_result_list = new List<string>();
        }
    }
    public void result_add(string s)
    {
        lock(LOCKOBJ)
        {
            if (m_result_list==null) m_result_list = new List<string>();
            m_result_list.Add(s);
        }
    }
    /*
        S_CHECK_TEXT_LENGTH
        入力文字長をN文字以上
    */
    void S_CHECK_TEXT_LENGTH(bool bFirst)
    {
        var len = m_form.textBox_findfilename.Text?.Length;
        // branch
        if (len>=1) { Goto( S_RESET_START_COLLECT_TASK ); }
        else { Goto( S_CLR_FLGS ); }
    }
    /*
        S_CLR_FLGS
        フラグクリア
    */
    void S_CLR_FLGS(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_bTxtChg=false;
            m_bBtnPush=false;
        }
        //
        if (!HasNextState())
        {
            Goto(S_WATCH_EVENT);
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_RESET_START_COLLECT_TASK
        収集タスクをリセットスタート
    */
    void S_RESET_START_COLLECT_TASK(bool bFirst)
    {
        //
        if (bFirst)
        {
            reset_start_collect();
        }
        //
        if (!HasNextState())
        {
            Goto(S_CLR_FLGS);
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
            Goto(S_CLR_FLGS);
        }
    }
    /*
        S_WATCH_EVENT
    */
    void S_WATCH_EVENT(bool bFirst)
    {
        // branch
        if (m_bTxtChg) { Goto( S_CHECK_TEXT_LENGTH ); }
        else if (m_bBtnPush) { Goto( S_RESET_START_COLLECT_TASK ); }
    }


	#endregion // [PSGG OUTPUT END]

    void reset_start_collect()
    {
        m_form.listBox_candidates.Items.Clear();
        result_clear();

        stop_task();
        start_task(); 
    }

    Task m_task;
    //CancellationTokenSource m_cancelTokenSource;   効かない！
    bool m_stop;
    void stop_task()
    {
        lock (LOCKOBJ) {  m_stop = true; PathUtil.m_halt = true; }
        while (true)
        {
            if (m_task == null) break;
            Thread.Sleep(100);
            if (
                m_task != null
                &&
                (m_task.IsCanceled || m_task.IsCompleted || m_task.IsFaulted)
                )
            {
                break;
            }
        }
        PathUtil.m_halt = false;
    }
    async void start_task()
    {
        m_stop = false;
        m_task = Task.Run(()=> {
            var sm = new CollectControl();
            //sm.m_token = m_cancelTokenSource.Token;
            sm.m_parent = this;
            sm.m_input = m_form.textBox_findfilename.Text.Trim();

            sm.Start();
            var LOOPMAX = 1E+6;
            for(var loop = 0; loop<=LOOPMAX; loop++)
            {
                if (loop == LOOPMAX) throw new SystemException("{72BE3D95-E790-4B89-A4E8-36E4E3EE56CB}");
                lock(LOCKOBJ) if (m_stop) break; 

                try { 
                    sm.Update();
                }
                catch (SystemException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                //update_result();

                if (sm.IsEnd()) break;
            }
        });
        await m_task;
    }
    List<string> m_save_result = null;
    int          m_save_result_count = 0;
    void update_result()
    {
        if (m_save_result == m_result_list)
        {
            if (m_result_list != null &&  m_save_result_count < m_result_list.Count)
            {
                for(var i = m_save_result_count; i < m_result_list.Count; i++)
                {
                    _listbox_add(m_result_list[i]);
                }
                m_save_result_count = m_result_list.Count;
            }
        }
        else
        {
            m_save_result = m_result_list;
            m_save_result_count = 0;
            if (m_result_list==null)
            {
                m_form.listBox_candidates.Items.Clear();
            }
            else
            { 
                m_form.listBox_candidates.Items.Clear();
                for(var i = 0; i<m_result_list.Count; i++)
                {
                    //m_form.listBox_candidates.Items.Add(m_result_list[i]);
                    _listbox_add(m_result_list[i]);
                }
                m_save_result_count = m_result_list.Count;
            }
        }
        _listbox_normalize();
    }
    void _listbox_add(string s)
    {
        var listbox = m_form.listBox_candidates;
        //後ろから見て、空白アイテムを消す
        for(var n = 0; n < 10; n++)
        {
            if (listbox.Items.Count > 0)
            {
                var index = listbox.Items.Count-1;
                if (string.IsNullOrWhiteSpace( listbox.Items[index].ToString()))
                {
                    listbox.Items.RemoveAt(index);
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        listbox.Items.Add(s);
    }
    void _listbox_normalize()
    {
        var listbox = m_form.listBox_candidates;
        if (listbox.Items.Count < 5)
        {
            var n = 5 - listbox.Items.Count;
            for(var i = 0; i<n; i++)
            {
                listbox.Items.Add("");
            }
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

