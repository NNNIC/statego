using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
///  
///  State Manager With Phase
/// 
///  State Function has internal phases.
///   
///  ex)
///  StateManagerWithPhase m_sm; 
///  void Start() {
///      m_sm = new StateManagerWithPhase();
///      m_sm.Goto(S_HOGE1);
///  }
///  void Update()
///  {
///      m_sm.Update();
///  }
///  void S_HOGE1(int phase, bool bFirst)
///  {
///      if (phase == 0)
///      {
///          if (bFirst) { initialize } 
///          else {
///             if (End?)
///             {
///                 m_sm.NextPhase();  //  phase will be 1
///             }
///          }
///          return;
///      }
///      if (phase == 1)
///      {
///          if (bFirst) { initialize } 
///          else {
///             if (End?)
///             {
///                 m_sm.NextPhase();  //  phase will be 2
///             }
///          }
///          return;
///      }
///      if (phase == 2)
///      {
///          if (bFirst) { initialize } 
///          else {
///             if (End?)
///             {
///                 m_sm.Goto(S_HOGE2);
///             }
///          }
///          return;
///      }
///  }
/// 
/// 
/// </summary>
public class StateManagerWithPhase  {

    //Debug
    public Action<Action<int,bool>,int,bool> dbg_logfunc = null;

    Action<int,bool> m_curFunc  = null;
    Action<int,bool> m_nextFunc = null;
    Action<int,bool> m_tempFunc = null;

    int? m_nextPhase = null;
    int  m_curPhase  = 0;

    bool m_bNoWait = false;

    public void Update()
    {
        while(true)//for(var loop = 0; loop < 10; loop++)
        {
            m_bNoWait = false;
            update_sub();
            if (m_bNoWait)
            {
                continue;
            }
            else
            {
                break;
            }
        }
    }
    
    void update_sub() {
        var bFirst = false;
        if (m_nextFunc!=null)
        {
            m_curFunc  = m_nextFunc;
            m_nextFunc = null;
            m_curPhase = 0;
            bFirst = true;
        }
        else
        {
            if (m_nextPhase!=null)
            {
                m_curPhase = (int)m_nextPhase;
                m_nextPhase = null;
                bFirst = true;
            }
        }

        if (m_curFunc!=null)
        {
            m_curFunc(m_curPhase,bFirst);

            if (dbg_logfunc!=null) {dbg_logfunc(m_curFunc,m_curPhase,bFirst); } 
        }

    }

    public void NextPhase(bool bNoWait=false)
    {
        m_nextPhase = (int?)(m_curPhase + 1);
        m_bNoWait   = bNoWait;
    }

    public void SetNext(Action<int,bool> func)
    {
        m_tempFunc = func;
    }

    public void GoNext(bool bNoWait=false)
    {
        m_nextFunc = m_tempFunc;
        m_tempFunc = null;
        m_bNoWait  = bNoWait;
    }

    public void Goto(Action<int,bool> func, bool bNoWait = false)
    {
        m_nextFunc = func;
        m_bNoWait  = bNoWait;
    }

    public bool HasNextState()
    {
        return (m_tempFunc!=null);
    }

    public bool CheckState(Action<int,bool> func)
    {
        return m_curFunc == func;
    }

    public Action<int,bool> GetCurFunc()
    {
        return m_curFunc;
    }
}