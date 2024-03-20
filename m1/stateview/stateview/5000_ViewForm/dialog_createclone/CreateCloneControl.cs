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

public partial class CreateCloneControl  {

    #region implement

    public stateview._5000_ViewForm.dialog_createclone.CreateCloneForm m_form;

    void cvtsheet(string sheetname)
    {
        var ed = new ExcelDll();
        ed.ReplaceWord(m_new_excel,sheetname, m_orgname, m_newname);
        ed.Dispose();
        ed = null;
    }
    //path = new doc dir + replace(file)
    string mcnd(string file)
    {
        if (string.IsNullOrEmpty(file)) return null;
        var filename = Path.GetFileName(file);
        var newfilename = filename.Replace(m_orgname,m_newname);
        return Path.Combine(m_newdocdir, newfilename);
    }
    string mcng(string file)
    {
        if (string.IsNullOrEmpty(file)) return null;
        var filename = Path.GetFileName(file);
        var newfilename = filename.Replace(m_orgname, m_newname);
        return Path.Combine(m_newsrcdir, newfilename);
    }
    bool? cfe(string file)
    {
        if (string.IsNullOrEmpty(file)) return null;
        return File.Exists(file);
    }
    bool check_exist_result()
    {
        Func<bool?,bool> isOkNotExist = (b)=> {
            return (b!=null && (bool)b == false);
        };
        Func<bool?,bool> isOkOrgNoneAndNotExist = (b) => {
            if (b==null) return true;
            return isOkNotExist(b);
        };

        if (m_newname.Contains(m_form.NEWNAME_SIGN))
        {
            return false;
        }

        if (m_newname == m_orgname)
        {
            // 新しいセーブディレクトリが違えば、問題なしとする。
            if (
                Path.GetDirectoryName(m_org_psgg) != m_newdocdir
                &&
                Path.GetDirectoryName(m_org_genfile) != m_newsrcdir)
            {
                //OK
            }
            else
            {
                return false;
            }
        }

        return 
            isOkNotExist(m_b_excel) 
            && 
            isOkNotExist(m_b_psgg) 
            && 
            isOkNotExist(m_b_genfile)
            &&
            isOkOrgNoneAndNotExist(m_b_impfile);
    }
    string make_clone_confirm_msg()
    {
        var msg = WordStorage.Res.Get("cfc_filecreated", G.system_lang) + "\n";// "The below files will be created.\n";
        msg += m_new_psgg + "\n";
        if (File.Exists(m_org_excel))
        {
           msg += m_new_excel + "\n";
        }
        if (m_b_helpweb!=null &&  (bool)m_b_helpweb == false)
        {
            msg += m_new_helpweb + "\n";
        }
        msg += m_new_genfile + "\n";
        if (m_b_genhpp != null && (bool)m_b_genhpp == false)
        {
            msg += m_new_genhpp + "\n";
        }
        if (m_b_impfile != null && (bool)m_b_impfile == false)
        {
            msg += m_new_impfile + "\n";
        }
        return msg;
    }
    void show_confirm_dlg(string msg)
    {
        var ret = MessageBox.Show(msg,"Confirm",MessageBoxButtons.OKCancel);
        m_bYesNo = ret == DialogResult.OK;
    }
    void copyfiles()
    {
        Action<string,string> cp = (a,b) => {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return;

            var dd = Path.GetDirectoryName(b);
            if (!string.IsNullOrEmpty(dd))
            {
                if (!Directory.Exists(dd))
                {
                    Directory.CreateDirectory(dd);
                }
            }

            if (File.Exists(a) && !File.Exists(b))
            {
                File.Copy(a,b);
                var fa = File.GetAttributes(b);
                if ((fa & FileAttributes.ReadOnly)==FileAttributes.ReadOnly)
                {
                    fa = fa & ~FileAttributes.ReadOnly;
                    File.SetAttributes(b,fa);
                }
            }
        };
        cp(m_org_excel, m_new_excel);
        cp(m_org_psgg, m_new_psgg);
        cp(m_org_helpweb, m_new_helpweb);
        cp(m_org_genfile, m_new_genfile);
        cp(m_org_genhpp, m_new_genhpp);
        cp(m_org_impfile, m_new_impfile);
        cp(m_org_macro, m_new_macro);
    }
    void chgs(string file, Encoding enc = null)
    {
        if (file==null) return;
        try {
            if (enc == null) enc = Encoding.UTF8;
            var text = File.ReadAllText(file,enc);
            var newtext= text.Replace(m_orgname,m_newname);

            if (stateview.SettingIniUtil.IsCloneExcnage_withUpperCamelWord())//uppercamelも対象
            { //lower
                var org_uppercamel = StringUtil.convert_to_camel_word(m_orgname,true);
                var new_uppercamel = StringUtil.convert_to_camel_word(m_newname,true);

                newtext = newtext.Replace(org_uppercamel,new_uppercamel);
            }
            { //upper
                var org_uppercamel = StringUtil.convert_to_camel_word(m_orgname,false);
                var new_uppercamel = StringUtil.convert_to_camel_word(m_newname,false);

                newtext = newtext.Replace(org_uppercamel,new_uppercamel);
            }
            { //snake
                var org_uppercamel = StringUtil.convert_to_snake_word_and_lower(m_orgname);
                var new_uppercamel = StringUtil.convert_to_snake_word_and_lower(m_newname);

                newtext = newtext.Replace(org_uppercamel,new_uppercamel);
            }

            File.WriteAllText(file,newtext,enc);
        } catch (SystemException e)
        {
            G.NoticeToUser_warning( G.Localize("w_errorcreateclonechangetext") /* "Error at Create Clone Change text in file :"*/ + file + "\n"  + e.Message);
        }
    }
    void newguidsheet(string sheet)
    {
        var ed = new ExcelDll();
        var val = ed.ReadCellSpecial(m_new_excel,sheet,0,0);
        if (val==null)
        {
            G.NoticeToUser_warning("Unexpected! {E04ABA6F-D984-4929-88B0-A477CB18EAA4}");
            return;
        }
        if (val.StartsWith("*error"))
        {
            G.NoticeToUser_warning(val);
            return;
        }
        var newval = IniUtil.Add(val,null,"guid",Guid.NewGuid().ToString());
        ed.WriteCellSpecial(m_new_excel, sheet,0,0,newval);
        ed.Dispose();
    }
    void modgenrdir(string sheet,string psgg,string gen)
    {
        var ed = new ExcelDll();
        var val = ed.ReadCellSpecial(m_new_excel,sheet,0,0);
        if (val == null)
        {
            G.NoticeToUser_warning("Unexpected! {DDCACBD0-CD10-4E09-9DA9-EC1964E9873B}");
            return;
        }
        if (val.StartsWith("*error"))
        {
            G.NoticeToUser_warning(val);
            return;
        }
        var docdir = Path.GetDirectoryName(psgg);
        var gendir = Path.GetDirectoryName(gen);
        var genrdir = PathUtil.GetRelativePath(docdir,gendir);

        var newval= IniUtil.Add(val, WordStorage.Store.settingini_group_setupinfo, WordStorage.Store.settingini_setupinfo_genrdir, genrdir);

        ed.WriteCellSpecial(m_new_excel,sheet,0,0,newval);
        ed.Dispose();
    }
    void show_error_dlg(string msg)
    {
        MessageBox.Show(msg,"ERROR");
    }
    void show_done_dlg()
    {
        MessageBox.Show( G.Localize("ccf_done")/* "Created Clone Files."*/);
    }
    #endregion

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
        //             psggConverterLib.dll converted from psgg-file:doc\CreateCloneControl.psgg                    // *DoNotEdit*
                                                                            // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_BOOL_EXIST                                                    // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        bool? m_b_excel;                                                    // *DoNotEdit*
        bool? m_b_psgg;                                                     // *DoNotEdit*
        bool? m_b_helpweb;                                                  // *DoNotEdit*
        bool? m_b_genfile;                                                  // *DoNotEdit*
        bool? m_b_genhpp;                                                   // *DoNotEdit*
        bool? m_b_impfile;                                                  // *DoNotEdit*
        bool? m_b_macro;                                                    // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_DEC_ENC                                                       // *DoNotEdit*
            生成時用エンコード                                              // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        Encoding m_genc = Encoding.UTF8;                                    // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_DEC_NAME                                                      // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        string m_orgname;                                                   // *DoNotEdit*
        string m_newname;                                                   // *DoNotEdit*
        string m_newdocdir;                                                 // *DoNotEdit*
        string m_newsrcdir;                                                 // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_DEC_NEWFILES                                                  // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        //新 フルパス格納                                                   // *DoNotEdit*
        string m_new_excel;                                                 // *DoNotEdit*
        string m_new_psgg;                                                  // *DoNotEdit*
        string m_new_helpweb;                                               // *DoNotEdit*
        string m_new_genfile;                                               // *DoNotEdit*
        string m_new_genhpp;                                                // *DoNotEdit*
        string m_new_impfile;                                               // *DoNotEdit*
        string m_new_macro;                                                 // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_DEC_ORGFILES                                                  // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        //元 フルパス格納                                                   // *DoNotEdit*
        string m_org_excel;                                                 // *DoNotEdit*
        string m_org_psgg;                                                  // *DoNotEdit*
        string m_org_helpweb;                                               // *DoNotEdit*
        string m_org_genfile;                                               // *DoNotEdit*
        string m_org_genhpp;                                                // *DoNotEdit*
        string m_org_impfile;                                               // *DoNotEdit*
        string m_org_macro;                                                 // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            E_VAR                                                           // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        string m_err = null;                                                // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CHECK_PSGG_W_DATA                                             // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CHECK_PSGG_W_DATA(bool bFirst)                               // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (G.psgg_file_w_data) { SetNextState( S_CLEAR_ERR ); }        // *DoNotEdit*
            else { SetNextState( S_CONV_SHEETS ); }                         // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CLEAR_ERR                                                     // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CLEAR_ERR(bool bFirst)                                       // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_err=null;                                                 // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_CREATE_NEW_GUID);                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CONFIRM_NEW_EXCEL                                             // *DoNotEdit*
            新規エクセルと同名ファイルが存在しない                          // *DoNotEdit*
            ※helpwebは除く                                                 // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CONFIRM_NEW_EXCEL(bool bFirst)                               // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_b_excel = cfe(m_new_excel);                               // *DoNotEdit*
                m_b_psgg  = cfe(m_new_psgg);                                // *DoNotEdit*
                m_b_helpweb = cfe(m_new_helpweb);                           // *DoNotEdit*
                m_b_genfile = cfe(m_new_genfile);                           // *DoNotEdit*
                m_b_genhpp  = cfe(m_new_genhpp);                            // *DoNotEdit*
                m_b_impfile = cfe(m_new_impfile);                           // *DoNotEdit*
                m_b_macro = cfe(m_new_macro);                               // *DoNotEdit*
                m_bYesNo = check_exist_result();                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            br_YES(S_CONFRM_DIALOG);                                        // *DoNotEdit*
            br_NO(S_ERROR_EXIST);                                           // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CONFRM_DIALOG                                                 // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CONFRM_DIALOG(bool bFirst)                                   // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            var msg = make_clone_confirm_msg();                             // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                show_confirm_dlg(msg);                                      // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            br_YES(S_COPY);                                                 // *DoNotEdit*
            br_NO(S_END);                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CONV_FILES                                                    // *DoNotEdit*
            他ファイル内のステートマシン名を変換                            // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CONV_FILES(bool bFirst)                                      // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                chgs(m_new_psgg);                                           // *DoNotEdit*
                chgs(m_new_helpweb);                                        // *DoNotEdit*
                chgs(m_new_genfile,m_genc);                                 // *DoNotEdit*
                chgs(m_new_genhpp,m_genc);                                  // *DoNotEdit*
                chgs(m_new_impfile,m_genc);                                 // *DoNotEdit*
                chgs(m_new_macro);                                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_MOD_GUID_GENRDIR);                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CONV_FILES1                                                   // *DoNotEdit*
            他ファイル内のステートマシン名を変換                            // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CONV_FILES1(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                chgs(m_new_genfile,m_genc);                                 // *DoNotEdit*
                chgs(m_new_genhpp,m_genc);                                  // *DoNotEdit*
                chgs(m_new_impfile,m_genc);                                 // *DoNotEdit*
                chgs(m_new_macro);                                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_err==null) { SetNextState( S_SET_PATH_SETTINGINI ); }     // *DoNotEdit*
            else { SetNextState( S_ERRMSG ); }                              // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CONV_SHEETS                                                   // *DoNotEdit*
            各シート内のステートマシン名を変換                              // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CONV_SHEETS(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                cvtsheet("setting.ini");                                    // *DoNotEdit*
                cvtsheet("help");                                           // *DoNotEdit*
                cvtsheet("template-statefunc");                             // *DoNotEdit*
                cvtsheet("templcate-source");                               // *DoNotEdit*
                cvtsheet("config");                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_CONV_FILES);                                 // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_COPY                                                          // *DoNotEdit*
            コピー                                                          // *DoNotEdit*
            ※helpwebは既にあれば除外                                       // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_COPY(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                copyfiles();                                                // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_CHECK_PSGG_W_DATA);                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_COPY_FILEDB                                                   // *DoNotEdit*
            GUIDを入れ替えたＰＳＧＧからFileDBを作成                        // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_COPY_FILEDB(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                np_create_filedb();                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_err==null) { SetNextState( S_CONV_FILES1 ); }             // *DoNotEdit*
            else { SetNextState( S_ERRMSG ); }                              // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CREATE_EXCEL_IFNEEDS                                          // *DoNotEdit*
            必要があればエクセルを作成                                      // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CREATE_EXCEL_IFNEEDS(bool bFirst)                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                np_create_excel_ifneeds();                                  // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_UE5ACTOR);                                   // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CREATE_NEW_GUID                                               // *DoNotEdit*
            新規GUIDを生成して、コピー先のPSGG内の現GUIDと入れ替える。      // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CREATE_NEW_GUID(bool bFirst)                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                np_new_psgg();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_err==null) { SetNextState( S_REPLACE_WORDS ); }           // *DoNotEdit*
            else { SetNextState( S_ERRMSG ); }                              // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_CREATE_PSGG                                                   // *DoNotEdit*
            生成したファイルから新規のＰＳＧＧを作成                        // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_CREATE_PSGG(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                np_create_psgg();                                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_err==null) { SetNextState( S_CREATE_EXCEL_IFNEEDS ); }    // *DoNotEdit*
            else { SetNextState( S_ERRMSG ); }                              // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_DONEDIALOG                                                    // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_DONEDIALOG(bool bFirst)                                      // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                show_done_dlg();                                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_END);                                        // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_DONEDIALOG1                                                   // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_DONEDIALOG1(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                show_done_dlg();                                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_END);                                        // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_END                                                           // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_END(bool bFirst)                                             // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_ERRMSG                                                        // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_ERRMSG(bool bFirst)                                          // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                show_err_start();                                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            if (show_err_done()==false) return;                             // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_END);                                        // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_ERROR_EXIST                                                   // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_ERROR_EXIST(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_err = "Cloning files exist or state machine name is not valid. The clone process was terminated.";                    // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_ERRORDIALOG);                                // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_ERRORDIALOG                                                   // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_ERRORDIALOG(bool bFirst)                                     // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                show_error_dlg(m_err);                                      // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_END);                                        // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_INIT_DSTFILENAME                                              // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_INIT_DSTFILENAME(bool bFirst)                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_new_excel = mcnd(m_org_excel);                            // *DoNotEdit*
                m_new_psgg = mcnd(m_org_psgg);                              // *DoNotEdit*
                m_new_helpweb = mcnd(m_org_helpweb);                        // *DoNotEdit*
                m_new_genfile = mcng(m_org_genfile);                        // *DoNotEdit*
                m_new_genhpp  = mcng(m_org_genhpp);                         // *DoNotEdit*
                m_new_impfile = mcng(m_org_impfile);                        // *DoNotEdit*
                m_new_macro = mcnd(m_org_macro);                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_INIT_ENC);                                   // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_INIT_ENC                                                      // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_INIT_ENC(bool bFirst)                                        // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_genc = G.src_enc;                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_CONFIRM_NEW_EXCEL);                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_INIT_ORGFILENAME                                              // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_INIT_ORGFILENAME(bool bFirst)                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_org_excel = G.load_file;                                  // *DoNotEdit*
                m_org_psgg  = G.psgg_file;                                  // *DoNotEdit*
                m_org_genfile = G.gen_file;                                 // *DoNotEdit*
                m_org_genhpp = G.genhpp_file;                               // *DoNotEdit*
                m_org_impfile = G.imp_file;                                 // *DoNotEdit*
                m_org_macro = G.macro_file;                                 // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_INIT_DSTFILENAME);                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_INITNAMES                                                     // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_INITNAMES(bool bFirst)                                       // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_orgname = m_form.textBox_this.Text;                       // *DoNotEdit*
                m_newname =m_form.textBox_new.Text;                         // *DoNotEdit*
                m_newdocdir=m_form.textBox_docdir.Text;                     // *DoNotEdit*
                m_newsrcdir = m_form.textBox_srcdir.Text;                   // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_INIT_ORGFILENAME);                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_MOD_GUID_GENRDIR                                              // *DoNotEdit*
            S_MOD_GUID_GENRDIR                                              // *DoNotEdit*
            シートのguidとgenrdirの更新                                     // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_MOD_GUID_GENRDIR(bool bFirst)                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                newguidsheet("config");                                     // *DoNotEdit*
                modgenrdir("setting.ini",m_new_psgg,m_new_genfile);         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_DONEDIALOG);                                 // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_NEW_GUID                                                      // *DoNotEdit*
            configシートのGUIDを更新                                        // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_NEW_GUID(bool bFirst)                                        // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                newguidsheet("config");                                     // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_DONEDIALOG);                                 // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_REPLACE_WORDS                                                 // *DoNotEdit*
            PSGG内の全ステートマシン名を入れ替える                          // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_REPLACE_WORDS(bool bFirst)                                   // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                np_replace_psgg();                                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_err==null) { SetNextState( S_COPY_FILEDB ); }             // *DoNotEdit*
            else { SetNextState( S_ERRMSG ); }                              // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_SET_PATH_SETTINGINI                                           // *DoNotEdit*
            シートsetting.ini内のパスを調整する。                           // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_SET_PATH_SETTINGINI(bool bFirst)                             // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                set_path_setting();                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_CREATE_PSGG);                                // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_START                                                         // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_START(bool bFirst)                                           // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_INITNAMES);                                  // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_UE5ACTOR                                                      // *DoNotEdit*
            UE5ActorであればAPI名変更                                       // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_UE5ACTOR(bool bFirst)                                        // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                if (G.is_ue5_actor_special_condition)                       // *DoNotEdit*
                {                                                           // *DoNotEdit*
                    ue5actor_work();                                        // *DoNotEdit*
                }                                                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                SetNextState(S_DONEDIALOG1);                                // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (HasNextState())                                             // *DoNotEdit*
            {                                                               // *DoNotEdit*
                GoNextState();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
                                                                            // *DoNotEdit*
                                                                            // *DoNotEdit*
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

    #region PSGG W DATA 新ＰＳＧＧ対応

    string m_np_guid;
    void np_new_psgg()
    {
        m_np_guid = Guid.NewGuid().ToString();
        try { 
            var psggbuf = File.ReadAllText(m_new_psgg,Encoding.UTF8);
            psggbuf = psggbuf.Replace(G.guid, m_np_guid);
            File.WriteAllText(m_new_psgg,psggbuf,Encoding.UTF8);
        }
        catch (SystemException e)
        {
            G.NoticeToUser_warning("{6C2EB129-6D9C-4C99-83A2-A3A5446419AD} " + e.Message);
            m_err = "Faile to create clone.";
        }
    }
    void np_replace_psgg()
    {
        try { 
            var psggbuf = File.ReadAllText(m_new_psgg,Encoding.UTF8);
            psggbuf = psggbuf.Replace(m_orgname, m_newname);
            File.WriteAllText(m_new_psgg,psggbuf,Encoding.UTF8);
        } 
        catch (SystemException e)
        {
            G.NoticeToUser_warning("{3E0260D6-6746-4628-B317-E7B38417394D} " + e.Message);
            m_err = "Faile to create clone.";
        }
    }
    

    StateViewer_filedb.FileDb m_np_db;
    void np_create_filedb()
    {
        try { 
            m_np_db = new StateViewer_filedb.FileDb();
            m_np_db.ReadPsgg(m_new_psgg,m_np_guid);
            m_np_db.write_filedb_all();

        } catch (SystemException e)
        {
            G.NoticeToUser_warning("{B771DFF6-6574-41B1-988B-1343B55C0102} " + e.Message);
            m_err = "Faile to create clone.";
        }
    }
    //void np_cvtsheets()
    //{
    //    Func<string,string> rep = (s) => {
    //        if (string.IsNullOrEmpty(s)) return s;
    //        return s.Replace(m_orgname,m_newname);
    //    };

    //    m_np_db.m_sheet_config_val          = rep(m_np_db.m_sheet_config_val);          m_np_db.write_filedb_config_sheet();
    //    m_np_db.m_sheet_help_val            = rep(m_np_db.m_sheet_help_val);            m_np_db.write_filedb_help_sheet();　　　　　　　　
    //    m_np_db.m_sheet_items_val           = rep(m_np_db.m_sheet_items_val);           m_np_db.write_filedb_items_sheet();
    //    m_np_db.m_sheet_setting_ini_val     = rep(m_np_db.m_sheet_setting_ini_val);     m_np_db.write_filedb_setting_sheet();
    //    m_np_db.m_sheet_template_func_val   = rep(m_np_db.m_sheet_template_func_val);   m_np_db.write_filedb_tempfunc_sheet();
    //    m_np_db.m_sheet_template_source_val = rep(m_np_db.m_sheet_template_source_val); m_np_db.write_filedb_tempsrc_sheet();

    //}

    void np_create_psgg()
    {
        string version;
        string file;
        string guid;
        string readfrom;
        string savemode;
        string check_excel_writable;
        var b = FileDbUtil.read_psgg_header_info(m_new_psgg,out version, out file, out guid, out readfrom, out savemode, out check_excel_writable);

        if (b)
        { 
            b = m_np_db.CreatePsgg(
                m_new_psgg,
                m_np_guid,
                readfrom,
                savemode,
                check_excel_writable
                );
        }
        
        if (!b)
        {
            G.NoticeToUser_warning("{D07DD0DB-617E-43F8-AF1D-3286295EF77B}");
            m_err = "Faild to create clone.";
        }
    }

    //stateview._5000_ViewForm.dialog.OkForm m_err_dlg;
    void show_err_start()
    {
        //m_err_dlg = new stateview._5000_ViewForm.dialog.OkForm();
        //m_err_dlg.textBox1.Text = m_err;
        //m_err_dlg.DialogResult = DialogResult.None;
        //m_err_dlg.Show();
        MessageBox.Show(m_err);
    }
    bool show_err_done()
    {
        return true;//return m_err_dlg.DialogResult != DialogResult.None;
    }

    void np_create_excel_ifneeds()
    {
        if (File.Exists(m_org_excel))
        {
            ExcelDll.Enabled = true; //ここだけＯＫ
            { 
                m_np_db.SaveExcel(m_new_excel);
            }
            ExcelDll.Enabled = false;
        }
    }
    void set_path_setting()
    {
        var newlinechar = StringUtil.FindNewLineChar(m_np_db.m_sheet_setting_ini_val);

        List<string> odr_list;
        var ht = IniUtil.CreateHashtableWithOrderList(m_np_db.m_sheet_setting_ini_val, out odr_list);

        var xlsdir = Path.GetDirectoryName(m_new_psgg);
        var gendir = Path.GetDirectoryName(m_new_genfile);

        IniUtil.SetValueFromHashtable("setupinfo","xlsdir",xlsdir, ref ht);
        IniUtil.SetValueFromHashtable("setupinfo","gendir",gendir, ref ht);
        
        var genrdir = PathUtil.GetRelativePath(xlsdir,gendir);
        if (string.IsNullOrEmpty(genrdir)) genrdir = ".";

        IniUtil.SetValueFromHashtable("setupinfo","genrdir",genrdir, ref ht);

        m_np_db.m_sheet_setting_ini_val = IniUtil.MakeOutput(ht,odr_list,newlinechar);
        m_np_db.write_filedb_setting_sheet();
    }
    #endregion

    #region UE5 ACTOR
    void ue5actor_work()
    {
        //new_genhpp_file ... 対象ファイル
        string projectname = null;
        string folder = Path.GetDirectoryName(m_new_genfile);
        string rootPath = Path.GetPathRoot(m_new_genfile);
        int loop = 0;
        while (!String.Equals(
            Path.GetFullPath(folder),
            Path.GetFullPath(rootPath),
            StringComparison.OrdinalIgnoreCase)
            )
        {
            loop++;
            if (loop > 100) break; //無限ループ回避

            var files = Directory.GetFiles(folder, "*.uproject");
            if (files.Length > 0)
            {
                projectname = Path.GetFileNameWithoutExtension(files[0]);
                break;
            }
            folder = Path.Combine(folder, "..");
        }
        if (projectname != null)
        {

            //m_new_genhppファイル内の UCLASS()の次の行を取得する。
            var lines = File.ReadAllLines(m_new_genhpp);
            var uclassline = "";
            for (var n = 0; n < lines.Length; n++)
            {
                if (lines[n].StartsWith("UCLASS()"))
                {
                    uclassline = lines[n + 1];
                    break;
                }
            }
            //その行から正規表現 "^class .+_API" を取得する
            var srcworrd = RegexUtil.Get1stMatch(@"^class\s.+?_API", uclassline);
            var text = File.ReadAllText(m_new_genhpp,m_genc);

            var repword = "class " + projectname.ToUpper() + "_API";
            var newtext = Regex.Replace(text, srcworrd, repword);
            File.WriteAllText(m_new_genhpp, newtext, m_genc);
        }
    }
    #endregion
}
