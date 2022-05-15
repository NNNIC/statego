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
using System.Reflection;
using stateview;
using System.Runtime.InteropServices;

public partial class OepnEditorControl  {
   
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
    //             psggConverterLib.dll converted from psgg-file:OepnEditorControl.psgg

    /*
        S_ASK_OPEN_FROM_UNITY
        Unity側からエディタを開いてから利用ください。
        それとも、強制的にひらく？
        Open or Cancel
    */
    void S_ASK_OPEN_FROM_UNITY(bool bFirst)
    {
        var bOpen = showDialog_open_or_cancel();
        // branch
        if (bOpen) { Goto( S_EXEC_EDITORCMD ); }
        else { Goto( S_END ); }
    }
    /*
        S_ASKDIALOG
        本ソリューションで開くか？
    */
    void S_ASKDIALOG(bool bFirst)
    {
        var b = AskSlnDialog();
        // branch
        if (b) { Goto( S_CHECK_PLATFORM1 ); }
        else { Goto( S_RESETSLN ); }
    }
    /*
        S_CHECK_PLATFORM
        プラットフォーム確認
    */
    void S_CHECK_PLATFORM(bool bFirst)
    {
        // branch
        if (isUnity()) { Goto( S_HasSln ); }
        else { Goto( S_OPTION_VS2006 ); }
    }
    /*
        S_CHECK_PLATFORM1
        new state
    */
    void S_CHECK_PLATFORM1(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_EXEC_EDITORCMD);
        }
    }
    /*
        S_CHECK_VSEXE
        VSのEXEか？
    */
    void S_CHECK_VSEXE(bool bFirst)
    {
        var b = checkVSExe();
        // branch
        if (b) { Goto( S_EXEOPENVS ); }
        else { Goto( S_EXEC_EDITORCMD ); }
    }
    /*
        S_CHECK_VSEXE1
        VSのEXEか？
    */
    void S_CHECK_VSEXE1(bool bFirst)
    {
        var b = checkVSExe();
        // branch
        if (b) { Goto( S_COMMAND_HAS_SLN ); }
        else { Goto( S_CHECK_PLATFORM1 ); }
    }
    /*
        S_COMMAND_HAS_SLN
        コマンドにソリューション代入があるか？
    */
    void S_COMMAND_HAS_SLN(bool bFirst)
    {
        var b =hasSlnInCommand();
        // branch
        if (b) { Goto( S_HasVSSln ); }
        else { Goto( S_CHECK_PLATFORM1 ); }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_ErrMsg
        失敗
    */
    void S_ErrMsg(bool bFirst)
    {
        //
        if (bFirst)
        {
            showerr();
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_EXEC_EDITORCMD
        エディタ開くコマンド実行
    */
    void S_EXEC_EDITORCMD(bool bFirst)
    {
        execOpenEditor();
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_EXEOPENVS
        VSオープン用のEXEを実行
    */
    void S_EXEOPENVS(bool bFirst)
    {
        execVSOpen();
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_FIND_EDITORPROC
        エディタのプロセスを探す。
    */
    void S_FIND_EDITORPROC(bool bFirst)
    {
        var b = existsEditorProcess();
        // branch
        if (b) { Goto( S_OPTION_VS2005 ); }
        else { Goto( S_CHECK_PLATFORM ); }
    }
    /*
        S_HasSln
        Unityの現プロジェクトのソリューションファイルがあるかか？
    */
    void S_HasSln(bool bFirst)
    {
        // branch
        if (hasUnitySln()) { Goto( S_SETSLN ); }
        else { Goto( S_ASK_OPEN_FROM_UNITY ); }
    }
    /*
        S_HasVSSln
        ソリューションがどこかにないか？
    */
    void S_HasVSSln(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_sln = FindSln();
        }
        // branch
        if (!string.IsNullOrEmpty(m_sln)) { Goto( S_ASKDIALOG ); }
        else { Goto( S_CHECK_PLATFORM1 ); }
    }
    /*
        S_OPTION_VS2005
        VSオプションが定義してあるか？
    */
    void S_OPTION_VS2005(bool bFirst)
    {
        var b = hasVSOpenOption();
        // branch
        if (b) { Goto( S_CHECK_VSEXE ); }
        else { Goto( S_EXEC_EDITORCMD ); }
    }
    /*
        S_OPTION_VS2006
        VSオプションが定義してあるか？
    */
    void S_OPTION_VS2006(bool bFirst)
    {
        var b = hasVSOpenOption();
        // branch
        if (b) { Goto( S_CHECK_VSEXE1 ); }
        else { Goto( S_CHECK_PLATFORM1 ); }
    }
    /*
        S_PAS001
    */
    void S_PAS001(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_COMMAND_HAS_SLN);
        }
    }
    /*
        S_RESETSLN
        ソリューション削除
    */
    void S_RESETSLN(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_sln = null;
        }
        //
        if (!HasNextState())
        {
            Goto(S_CHECK_PLATFORM1);
        }
    }
    /*
        S_SETSLN
        ソリューション名設定
    */
    void S_SETSLN(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_sln = UnityUtil.GetSlnFile();
        }
        //
        if (!HasNextState())
        {
            Goto(S_EXEC_EDITORCMD);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_FIND_EDITORPROC);
        NoWait();
    }


    #endregion // [PSGG OUTPUT END]

    public int? m_linenum=null;
    public string m_file = null;
    public string m_sln = null;

    bool showDialog_open_or_cancel()
    {
        var result = MessageBox.Show(G.Localize("oec_ooc")  /*"Unity Editorよりソースエディタを起動してください(CANCEL)。それとも、強制的に起動しますか?(OK)"*/, "",MessageBoxButtons.OKCancel);
        return result == DialogResult.OK;
    }
    bool isUnity()
    {
        var fw = SettingIniUtil.GetFramework();
        if ( fw!=null && fw.ToLower().Contains("unity") )
        {
            return true;
        }
        //更に確認
        if (UnityUtil.IsInUnityProject())
        {
            return true;
        }
        return false;
    }
    void execOpenEditor()
    {
        var fullpath = (m_file!=null) ? m_file : G.gen_file; 
		var file = Path.GetFileName(fullpath);
		var dir  = Path.GetDirectoryName(fullpath);
		var cmdline = string.Empty;
        var lineparam = m_linenum;
		if (string.IsNullOrEmpty(G.external_source_editor))
		{
			cmdline = FindWindowsAppUtil.FindAssociatedCommand(fullpath);
			if (cmdline.Contains("%1"))
			{
				cmdline = cmdline.Replace("%1", file);
			}
			else
			{
				cmdline = cmdline + " " + file;
			}
		}
		else
		{
			if (G.external_source_editor.Contains("%1"))
			{
				cmdline = G.external_source_editor.Replace("%1", file);
			}
			else
			{
				cmdline = string.Format(" \"{0}\" {1} ", G.external_source_editor, file);
			}
		}

        if (cmdline.Contains("%2"))
        {
            var line = lineparam==null ? 1: (int)lineparam;
            cmdline = cmdline.Replace("%2", line.ToString());
        }

        if (cmdline.Contains("%3"))
        {
            if (!string.IsNullOrEmpty( m_sln ))
            {
                cmdline = cmdline.Replace("%3",m_sln);
            }
            else
            {
                cmdline = cmdline.Replace("%3",string.Empty);
            }
        }

        cmdline = PathUtil.ExtractPathWithEnvVals(cmdline);

        if (G.use_batch_for_source_editor_open || CmdLineUtil.IsBatch(cmdline))
        {
            ExecUtil.execute_as_batch(cmdline,dir);
        }
        else
        {
            ExecUtil.execute_start2(cmdline,dir);
        }
    }
    void execVSOpen()
    {
        var fullpath = m_file!=null ? m_file : G.gen_file; 
		var file = Path.GetFileName(fullpath);
		var dir  = Path.GetDirectoryName(fullpath);

        if (G.source_editor_vs2015_support)
        {
            var line = m_linenum==null ? 1 : (int)m_linenum;
            exec_vs2015jump(dir,fullpath,line);
            //exec_vsopen(dir,fullpath,(int)m_linenum);
        }
    }
    void exec_vs2015jump(string dir, string src, int line)
    {
        var appdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        var cmd = "\"" + Path.Combine(appdir,"tools","VisualStudioOpenFile.exe") + "\"  \"" + src +"\" " + line.ToString();
        ExecUtil.execute_start2(cmd,dir);
    }
    //void exec_vsopen(string dir, string src, int line)
    //{
    //    var appdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
    //    var cmd = "\"" + Path.Combine(appdir,"vsopen","vsopen.exe") + "\"  \"" + src +"\" " + line.ToString();
    //    ExecUtil.execute_start2(cmd,dir);
    //}
    bool existsEditorProcess()
    {
        var sm = new System.Diagnostics.Stopwatch();
        sm.Start();

        var b = _existsEditorProcess();

        sm.Stop();

        G.NoticeToUser("Process search : " + ((double)sm.ElapsedMilliseconds / 1000f).ToString("F3") );

        return b;

    }
    bool _existsEditorProcess()
    {
        try {
            var procname = Path.GetFileNameWithoutExtension( G.external_source_editor_path);
            var allproc = System.Diagnostics.Process.GetProcessesByName(procname);
            var editorpath = G.external_source_editor_path.ToLower();

            foreach(var pi in allproc)
            {
                try {
                    var path = pi.GetMainModuleFileName();
                    if (path!=null && path.ToLower() == editorpath)
                    {
                        return true;
                    }
                } catch { }
            }
        } catch { }
        return false;
    }

    bool hasVSOpenOption()
    {
        return G.source_editor_vs2015_support;
    }

    void sleep(double t)
    {
        System.Threading.Thread.Sleep((int)(t * 1000f));
    }

    bool hasUnitySln()
    {
        return !string.IsNullOrEmpty(UnityUtil.GetSlnFile());
    }

    bool openSln()
    {
        var toolexe = G.external_source_editor_path;
        if (string.IsNullOrEmpty(toolexe)) return false;
        if (Path.GetFileNameWithoutExtension(toolexe).ToLower() == "devenv")
        {
            var cmdline = "\"" + toolexe + "\" " + UnityUtil.GetSlnFile();
            ExecUtil.execute_start2(cmdline,null);
            return true;
        }
        return false;
    }

    void showerr()
    {
        MessageBox.Show("{D43D6C68-9642-4006-8780-F57040300409}");
    }

    string FindSln()
    {
        return VSUtil.FindSln();
    }

    bool AskSlnDialog()
    {
        return MessageBox.Show(G.Localize("oec_ask")/*"以下のソリューションファイルを使用します。よろしいでしょうか？キャンセル時はソリューションファイルを指定せずに開きます。"*/ + m_sln, G.Localize("oec_cfm")  ,MessageBoxButtons.OKCancel) == DialogResult.OK;
    }

    bool hasSlnInCommand()
    {
        return  G.external_source_editor!=null && G.external_source_editor.Contains("%3");
    }

    bool checkVSExe()
    {
        var exe = G.external_source_editor_path;
        if (string.IsNullOrEmpty(exe)) return false;

        if (exe.ToLower().EndsWith(@"\ide\devenv.exe"))
        {
            return true;
        }
        return false;
    }
}

//https://stackoverflow.com/questions/5497064/how-to-get-the-full-path-of-running-process
internal static class Extensions {
    [DllImport("Kernel32.dll")]
    private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

    public static string GetMainModuleFileName(this  System.Diagnostics.Process process, int buffer = 1024) {
        var fileNameBuilder = new StringBuilder(buffer);
        uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
        return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
            fileNameBuilder.ToString() :
            null;
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

