using System;
using System.Collections.Generic;
using System.IO;
using StateViewer_starter2;

public partial class ShortcutControl  {
   
    public StateViewer_starter2.CreateNewForm m_form;
    private Start2Form m_sf              { get { return Start2Form.m_form; } }

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

	#region    // [PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:doc\ShortcutControl.psgg

    /*
        E_0012
    */
    public enum RESULT
    {
        none,
        setdetail,
        gocopy
    }
    public RESULT m_result;
    /*
        S_0003
        ショートカットタイプの選択ダイアログ表示
    */
    void S_0003(bool bFirst)
    {
        var dlg= new StateViewer_starter2.CreateNewForm_ShortcutFotm();
        var bOk = dlg.ShowDialog(m_form) == System.Windows.Forms.DialogResult.OK;
        // branch
        if (bOk) { Goto( S_0004 ); }
        else { Goto( S_0007 ); }
    }
    /*
        S_0004
        詳細設定へ？
    */
    void S_0004(bool bFirst)
    {
        var bDetailType = StateViewer_starter2.WORK.SrcDocFolderDefineType == StateViewer_starter2.SRC_DOC_FOLDER_DFEINE_TYPE.none;
        // branch
        if (bDetailType) { Goto( S_0006 ); }
        else { Goto( S_0005 ); }
    }
    /*
        S_0005
        ソースフォルダパス設定ダイアログ表示
    */
    void S_0005(bool bFirst)
    {
        if (string.IsNullOrEmpty(WORK.GENDIR))
        {
            WORK.GENDIR = m_sf.get_gendir_candidate();
        }
        var dlg = new StateViewer_starter2.CreateNewForm_ShortcutFotm2();
        dlg.textBoxSrcFolder.Text = WORK.GENDIR;
        var bOk = dlg.ShowDialog(m_form) == System.Windows.Forms.DialogResult.OK;
        WORK.GENDIR = dlg.textBoxSrcFolder.Text;
        // branch
        if (bOk) { Goto( S_0008 ); }
        else { Goto( S_0003 ); }
    }
    /*
        S_0006
        詳細設定へ
    */
    void S_0006(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_result = RESULT.setdetail;
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_0007
        戻る
    */
    void S_0007(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_result = RESULT.none;
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_0008
        タイプ別に
    */
    void S_0008(bool bFirst)
    {
        var bAll = StateViewer_starter2.WORK.SrcDocFolderDefineType == StateViewer_starter2.SRC_DOC_FOLDER_DFEINE_TYPE.all_in_one_folder;
        // branch
        if (bAll) { Goto( S_0009 ); }
        else { Goto( S_0011 ); }
    }
    /*
        S_0009
        オールインワン設定
    */
    void S_0009(bool bFirst)
    {
        //
        if (bFirst)
        {
            WORK.XLSDIR = WORK.GENDIR;
            WORK.UpdateByInputText2();
            m_form.m_cw = new WORK.CreateFileWork();
        }
        //
        if (!HasNextState())
        {
            Goto(S_0010);
        }
    }
    /*
        S_0010
        コピーページへ
    */
    void S_0010(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_result = RESULT.gocopy;
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_0011
        Srcフォルダ内にdocフォルダ設定
    */
    void S_0011(bool bFirst)
    {
        //
        if (bFirst)
        {
            WORK.XLSDIR = Path.Combine(WORK.GENDIR,"doc");
            WORK.UpdateByInputText2();
            m_form.m_cw = new WORK.CreateFileWork();
        }
        //
        if (!HasNextState())
        {
            Goto(S_0010);
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
            Goto(S_0003);
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

