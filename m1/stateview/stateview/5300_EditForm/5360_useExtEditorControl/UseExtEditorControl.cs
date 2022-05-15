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
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

using stateview;

public partial class UseExtEditorControl  {
   
    //public stateview._5300_EditForm.EditForm_textForm m_parent;
    public Control m_parent;


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
    //             psggConverterLib.dll converted from psgg-file:doc\UseExtEditorControl.psgg                       // *DoNotEdit*
                                                                            // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        E_IMPORT                                                            // *DoNotEdit*
        対象のステートとアイテム指定                                        // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    public string m_state;                                                  // *DoNotEdit*
    public string m_item;                                                   // *DoNotEdit*
    public string m_val;                                                    // *DoNotEdit*
    public int?   m_item_line_index;                                        // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_BACKUP_SRC                                                        // *DoNotEdit*
        対象ファイルのバックアップ作成                                      // *DoNotEdit*
        ドキュメントフォルダにリネームして格納                              // *DoNotEdit*
        ~[filename].psgg-edititem-backup                                    // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_BACKUP_SRC(bool bFirst)                                          // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            backup_src();                                                   // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_CVT_W_MARK);                                             // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_CVT_W_MARK                                                        // *DoNotEdit*
        対象部分にマークを入れて、コンバート出力                            // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_CVT_W_MARK(bool bFirst)                                          // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            convert_with_mark();                                            // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_OPEN_EXT_EDITOR);                                        // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_DESC                                                              // *DoNotEdit*
        編集の説明                                                          // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_DESC(bool bFirst)                                                // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            MessageBox.Show(G.Localize("ueec_desc"));                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_BACKUP_SRC);                                             // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_END                                                               // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_END(bool bFirst)                                                 // *DoNotEdit*
    {                                                                       // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_INIT                                                              // *DoNotEdit*
        初期化                                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_INIT(bool bFirst)                                                // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            init();                                                         // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_DESC);                                                   // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_OPEN_EXT_EDITOR                                                   // *DoNotEdit*
        外部エディタを開く。                                                // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_OPEN_EXT_EDITOR(bool bFirst)                                     // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            open_external_editor();                                         // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_WAIT_AND_ASK);                                           // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_PICKOUT                                                           // *DoNotEdit*
        内容取得                                                            // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_PICKOUT(bool bFirst)                                             // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        pickout();                                                          // *DoNotEdit*
        m_output = m_pickup;                                                // *DoNotEdit*
        m_bOk = true;                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_RESTORE_BACKUP);                                         // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_RESTORE_BACKUP                                                    // *DoNotEdit*
        バックアップ復活                                                    // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_RESTORE_BACKUP(bool bFirst)                                      // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            restore_src();                                                  // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_END);                                                    // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_START                                                             // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_START(bool bFirst)                                               // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_INIT);                                                   // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_WAIT_AND_ASK                                                      // *DoNotEdit*
        終了待ち兼取出し確認                                                // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_WAIT_AND_ASK(bool bFirst)                                        // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        var bYesPickOut = false;                                            // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (MessageBox.Show(G.Localize("ueec_aftermodifed") ,"Confirmation",MessageBoxButtons.YesNo) == DialogResult.Yes)           // *DoNotEdit*
            {                                                               // *DoNotEdit*
                bYesPickOut = true;                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        // branch                                                           // *DoNotEdit*
        if (bYesPickOut) { Goto( S_PICKOUT ); }                             // *DoNotEdit*
        else { Goto( S_RESTORE_BACKUP ); }                                  // *DoNotEdit*
    }                                                                       // *DoNotEdit*
                                                                            // *DoNotEdit*
                                                                            // *DoNotEdit*
    #endregion // [PSGG OUTPUT END]


    #region BACKUP AND RESTORE
    const string backup_extention = ".psgg-edititem-backup";
    const string backup_prefix = "~";
    string backupfilename { 
        get {
            var genfile = Path.GetFileName(G.gen_file);
            return backup_prefix + genfile + backup_extention;
        }
    }
    string backupfile_fullpath
    {
        get
        {
            return Path.Combine( G.load_file_dir, backupfilename); 
        }
    }
    void backup_src()
    {
        if (File.Exists(backupfile_fullpath))
        {
            G.NoticeToUser("Because existing the backup file is unexpected, delete it.");
            File.Delete(backupfile_fullpath);
        }

        File.Copy(G.gen_file, backupfile_fullpath);
        G.NoticeToUser("Created a backup file.");
    }
    void restore_src()
    {
        if (!File.Exists(backupfile_fullpath))
        {
            G.NoticeToUser_warning("Unexpected. Backup File is missing.");
            return;
        }

        File.Copy(backupfile_fullpath,G.gen_file,true);

        File.Delete(backupfile_fullpath);

        G.NoticeToUser("The source has been restored.");
    }
    #endregion

    #region 
    const string MARK_START = "psgg-itemedit-start:";
    const string MARK_END   = "psgg-itemedit-end:";
    string m_mark_start_line
    {
        get {
            var start_mark ="   " + G.Localize("ueec_donotdelete_begin") + "   " + MARK_START + "item=" + m_item;
            return make_commentline(start_mark);
        }
    }
    string m_mark_end_line
    {
        get {
            var end_mark   ="   " + G.Localize("ueec_donotdelete_end")  + "   " +MARK_END + "item=" + m_item;
            return make_commentline(end_mark);
        }
    }
    string make_commentline(string s)
    {
        if (G.macro_ini!=null)
        { 
            var commentline_macro = G.macro_ini.GetValue("commentline");
            if (commentline_macro!=null)
            { 
                var output = commentline_macro.Replace("{%0}", s);
                return output;
            }
        }
        G.NoticeToUser_warning("Cannot make comment line.");
        return s;
    }

    #region 変更部分にマークを入れる
    void convert_with_mark()
    {
        if (m_item_line_index == null)
        {
            convert_with_mark_normal();
        }
        else
        {
            convert_with_mark_use_item_line_index(); //branchで使う
        }
    }

    void convert_with_mark_normal()
    {
        var dic = G.excel_program.GetAllString(m_state);
        if (!dic.ContainsKey(m_item))
        {
            G.NoticeToUser_warning("Unexpected! {B42BC221-381A-47FF-83D2-2454854C31AA}");
            return;
        }

        var savedic = DictionaryUtil.Clone(dic);
        
        var NL = "\x0a";
        var temp_val = string.Empty;
        temp_val = m_mark_start_line + NL;

        var val = m_val; //  dic[m_item];
        if (string.IsNullOrEmpty(val))
        {
            var insercomment = string.Format(G.Localize("ueec_writehere") ,m_item);
            temp_val += make_commentline( insercomment) +NL;
        }
        else
        {
            temp_val += val + NL;
        }
        temp_val += m_mark_end_line;

        dic[m_item] = temp_val;

        G.excel_program.SetAllString(m_state,dic); //一時的に変更した値でセーブ
        G.donotedit_push(false);

        Converter.Convert(); //無理やりコンバート

        G.donotedit_pop();
        G.excel_program.SetAllString(m_state,savedic); //元に戻しておく
    }
    void convert_with_mark_use_item_line_index()
    {
        var line_index = (int)m_item_line_index;

        var dic = G.excel_program.GetAllString(m_state);
        if (!dic.ContainsKey(m_item))
        {
            G.NoticeToUser_warning("Unexpected! {B42BC221-381A-47FF-83D2-2454854C31AA}");
            return;
        }
        var savedic = DictionaryUtil.Clone(dic);
        
        var NL = G.branch_special_newlinechar;
        var temp_val = string.Empty;
        temp_val = m_mark_start_line + NL;

        if (string.IsNullOrEmpty(m_val) || m_val.Trim()=="?")
        {
            var insercomment = string.Format(G.Localize("ueec_writehere") ,m_item);
            temp_val += make_commentline( insercomment) +NL;
        }
        else
        {
            temp_val += m_val + NL;
        }
        temp_val += m_mark_end_line;

        var newval = StringUtil.ReplaceLine(dic[m_item], line_index, temp_val);

        dic[m_item] = newval;

        G.excel_program.SetAllString(m_state,dic); //一時的に変更した値でセーブ
        G.donotedit_push(false);

        Converter.Convert(); //無理やりコンバート

        G.donotedit_pop();
        G.excel_program.SetAllString(m_state,savedic); //元に戻しておく
    }
    #endregion

    string m_pickup;
    void pickout()
    {
        m_pickup = string.Empty;

        var startline = FileUtil.GetLineNumOfSearchingWord(G.gen_file,G.src_enc,MARK_START);
        var endline = FileUtil.GetLineNumOfSearchingWord(G.gen_file,G.src_enc,MARK_END);
        var lines = FileUtil.CropByLineNum(G.gen_file, G.src_enc, startline,endline);
       
        if (lines == null)
        {
            G.NoticeToUser("Unexpected! {F8B8569A-8A53-4727-B16A-2B1F4F940AC8}");
            m_pickup = string.Empty;
            return;
        }
        
        //開始行と終了行を削除
        if (lines.Count <= 2)
        {
            m_pickup = string.Empty;
            return;
        }
        lines.RemoveAt(0); //先頭削除
        lines.RemoveAt(lines.Count-1); //最終削除

        var newlines = StringUtil.NomalizeWithTrimHeaderSpace(lines);

        //newlines.ForEach( i=>{
        //        m_pickup += i + Environment.NewLine;
        //});

        if (newlines!=null && newlines.Count>0)
        {
            for(var n = 0; n < newlines.Count; n++)
            {
                var i = newlines[n];
                m_pickup += i;
                if (n<newlines.Count-1) //最終行は改行なし
                {
                    m_pickup += Environment.NewLine;
                }
            }
        }

        return;
    }
    #endregion

    void open_external_editor()
    {
        //System.Diagnostics.Debugger.Launch();
        var linenum = FileUtil.GetLineNumOfSearchingWord(G.gen_file,G.src_enc, MARK_START);
        if (linenum>=0)
        { 
            linenum++;
            //ExecEditorUtil.Exec(G.gen_file,linenum);
            OpenEditorUtil.OpenEditor(linenum);
        }
        else
        {
            G.NoticeToUser_warning("{E6FE2E55-2E41-484C-B0B6-A3B995F94D6E}");
        }
    }

    #region output
    public string m_output;
    public bool   m_bOk;
    void init()
    {
        m_output = null;
        m_bOk = false;
    }
    void open_confirm_result_dlg(Control c)
    {
        var dlg = new stateview._5300_EditForm._5360_useExtEditorControl.ConfirmPickOutForm();
        dlg.m_state = m_state;
        dlg.textBox1.Text = m_pickup;
        if (dlg.ShowDialog(c) == DialogResult.OK)
        {
            m_output = dlg.textBox1.Text;
            m_bOk = true;
        }
    }
    #endregion








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

