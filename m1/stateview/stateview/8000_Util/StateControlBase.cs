using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StateControlBase
{
    protected StateManagerWithPhase m_sm;
    protected bool                  m_nowait = false;

    protected void sc_start(Action<int,bool> state=null)
    {
        m_sm = new StateManagerWithPhase();
        m_sm.SetNext(state);
        m_sm.GoNext();
    }

    protected void sc_update()
    {
        if (m_sm!=null) m_sm.Update();
    }

    protected void SetNextState(Action<int,bool> state=null)
    {
        m_sm.SetNext(state);
    }

    protected bool HasNextState()
    {
        return m_sm.HasNextState();
    }

    protected void NoWait()
    {
        m_nowait = true;
    }

    protected void GoNextState()
    {
        var nowait = m_nowait;
        m_nowait = false;
        m_sm.GoNext(nowait);
    }
    protected void NextPhase()
    {
        m_sm.NextPhase(true);
    }

    internal bool CheckState(Action<int,bool> st)
    {
        return m_sm.CheckState(st);
    }

    #region For Debug

    string dbg_funcname;
    protected void Dbg_SetLoggingState( Action<string> logfunc )
    {
        if (logfunc == null) return;

        Action<Action<int,bool>,int,bool> dbgfunc = (func,p,b) => {
            var name = func.Method.Name;
            if (dbg_funcname == name) return;
            dbg_funcname = name;
            logfunc(dbg_funcname);    
        };

        m_sm.dbg_logfunc = dbgfunc;
    }
    protected void Dbg_Reset()
    {
        m_sm.dbg_logfunc = null;
    }

    #endregion
}
