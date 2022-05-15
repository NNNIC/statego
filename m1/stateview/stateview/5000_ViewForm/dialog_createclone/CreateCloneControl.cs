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
        if (m_b_impfile!=null && (bool)m_b_impfile == false)
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
//  psggConverterLib.dll converted from CreateCloneControl.xlsx.    psgg-file:doc\CreateCloneControl.psgg
        /*
            E_BOOL_EXIST
        */
        bool? m_b_excel;
        bool? m_b_psgg;
        bool? m_b_helpweb;
        bool? m_b_genfile;
        bool? m_b_impfile;
        bool? m_b_macro;
        /*
            E_DEC_ENC
            生成時用エンコード
        */
        Encoding m_genc = Encoding.UTF8;
        /*
            E_DEC_NAME
        */
        string m_orgname;
        string m_newname;
        string m_newdocdir;
        string m_newsrcdir;
        /*
            E_DEC_NEWFILES
        */
        //新 フルパス格納
        string m_new_excel;
        string m_new_psgg;
        string m_new_helpweb;
        string m_new_genfile;
        string m_new_impfile;
        string m_new_macro;
        /*
            E_DEC_ORGFILES
        */
        //元 フルパス格納
        string m_org_excel;
        string m_org_psgg;
        string m_org_helpweb;
        string m_org_genfile;
        string m_org_impfile;
        string m_org_macro;
        /*
            E_VAR
        */
        string m_err = null;
        /*
            S_CHECK_PSGG_W_DATA
        */
        void S_CHECK_PSGG_W_DATA(bool bFirst)
        {
            // branch
            if (G.psgg_file_w_data) { SetNextState( S_CLEAR_ERR ); }
            else { SetNextState( S_CONV_SHEETS ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CLEAR_ERR
        */
        void S_CLEAR_ERR(bool bFirst)
        {
            if (bFirst)
            {
                m_err=null;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CREATE_NEW_GUID);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONFIRM_NEW_EXCEL
            新規エクセルと同名ファイルが存在しない
            ※helpwebは除く
        */
        void S_CONFIRM_NEW_EXCEL(bool bFirst)
        {
            if (bFirst)
            {
                m_b_excel = cfe(m_new_excel);
                m_b_psgg  = cfe(m_new_psgg);
                m_b_helpweb = cfe(m_new_helpweb);
                m_b_genfile = cfe(m_new_genfile);
                m_b_impfile = cfe(m_new_impfile);
                m_b_macro = cfe(m_new_macro);
                m_bYesNo = check_exist_result();
            }
            // branch
            br_YES(S_CONFRM_DIALOG);
            br_NO(S_ERROR_EXIST);
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONFRM_DIALOG
        */
        void S_CONFRM_DIALOG(bool bFirst)
        {
            var msg = make_clone_confirm_msg();
            if (bFirst)
            {
                show_confirm_dlg(msg);
            }
            // branch
            br_YES(S_COPY);
            br_NO(S_END);
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONV_FILES
            他ファイル内のステートマシン名を変換
        */
        void S_CONV_FILES(bool bFirst)
        {
            if (bFirst)
            {
                chgs(m_new_psgg);
                chgs(m_new_helpweb);
                chgs(m_new_genfile,m_genc);
                chgs(m_new_impfile,m_genc);
                chgs(m_new_macro);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_MOD_GUID_GENRDIR);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONV_FILES1
            他ファイル内のステートマシン名を変換
        */
        void S_CONV_FILES1(bool bFirst)
        {
            if (bFirst)
            {
                chgs(m_new_genfile,m_genc);
                chgs(m_new_impfile,m_genc);
                chgs(m_new_macro);
            }
            // branch
            if (m_err==null) { SetNextState( S_SET_PATH_SETTINGINI ); }
            else { SetNextState( S_ERRMSG ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CONV_SHEETS
            各シート内のステートマシン名を変換
        */
        void S_CONV_SHEETS(bool bFirst)
        {
            if (bFirst)
            {
                cvtsheet("setting.ini");
                cvtsheet("help");
                cvtsheet("template-statefunc");
                cvtsheet("templcate-source");
                cvtsheet("config");
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CONV_FILES);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_COPY
            コピー
            ※helpwebは既にあれば除外
        */
        void S_COPY(bool bFirst)
        {
            if (bFirst)
            {
                copyfiles();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CHECK_PSGG_W_DATA);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_COPY_FILEDB
            GUIDを入れ替えたＰＳＧＧからFileDBを作成
        */
        void S_COPY_FILEDB(bool bFirst)
        {
            if (bFirst)
            {
                np_create_filedb();
            }
            // branch
            if (m_err==null) { SetNextState( S_CONV_FILES1 ); }
            else { SetNextState( S_ERRMSG ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CREATE_EXCEL_IFNEEDS
            必要があればエクセルを作成
        */
        void S_CREATE_EXCEL_IFNEEDS(bool bFirst)
        {
            if (bFirst)
            {
                np_create_excel_ifneeds();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_DONEDIALOG1);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CREATE_NEW_GUID
            新規GUIDを生成して、コピー先のPSGG内の現GUIDと入れ替える。
        */
        void S_CREATE_NEW_GUID(bool bFirst)
        {
            if (bFirst)
            {
                np_new_psgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_REPLACE_WORDS ); }
            else { SetNextState( S_ERRMSG ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CREATE_PSGG
            生成したファイルから新規のＰＳＧＧを作成
        */
        void S_CREATE_PSGG(bool bFirst)
        {
            if (bFirst)
            {
                np_create_psgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_CREATE_EXCEL_IFNEEDS ); }
            else { SetNextState( S_ERRMSG ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_DONEDIALOG
        */
        void S_DONEDIALOG(bool bFirst)
        {
            if (bFirst)
            {
                show_done_dlg();
            }
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
            S_DONEDIALOG1
        */
        void S_DONEDIALOG1(bool bFirst)
        {
            if (bFirst)
            {
                show_done_dlg();
            }
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
            S_ERRMSG
        */
        void S_ERRMSG(bool bFirst)
        {
            if (bFirst)
            {
                show_err_start();
            }
            if (show_err_done()==false) return;
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
            S_ERROR_EXIST
        */
        void S_ERROR_EXIST(bool bFirst)
        {
            if (bFirst)
            {
                m_err = G.Localize("ccf_errore");// "Cloning files exist or state machine name is not valid. The clone process was terminated.";
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_ERRORDIALOG);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_ERRORDIALOG
        */
        void S_ERRORDIALOG(bool bFirst)
        {
            if (bFirst)
            {
                show_error_dlg(m_err);
            }
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
            S_INIT_DSTFILENAME
        */
        void S_INIT_DSTFILENAME(bool bFirst)
        {
            if (bFirst)
            {
                m_new_excel = mcnd(m_org_excel);
                m_new_psgg = mcnd(m_org_psgg);
                m_new_helpweb = mcnd(m_org_helpweb);
                m_new_genfile = mcng(m_org_genfile);
                m_new_impfile = mcng(m_org_impfile);
                m_new_macro = mcnd(m_org_macro);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT_ENC);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INIT_ENC
        */
        void S_INIT_ENC(bool bFirst)
        {
            if (bFirst)
            {
                m_genc = G.src_enc;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CONFIRM_NEW_EXCEL);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INIT_ORGFILENAME
        */
        void S_INIT_ORGFILENAME(bool bFirst)
        {
            if (bFirst)
            {
                m_org_excel = G.load_file;
                m_org_psgg  = G.psgg_file;
                //m_org_helpweb = G.helpweb_file;
                m_org_genfile = G.gen_file;
                m_org_impfile = G.imp_file;
                m_org_macro = G.macro_file;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT_DSTFILENAME);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_INITNAMES
        */
        void S_INITNAMES(bool bFirst)
        {
            if (bFirst)
            {
                m_orgname = m_form.textBox_this.Text;
                m_newname =m_form.textBox_new.Text;
                m_newdocdir=m_form.textBox_docdir.Text;
                m_newsrcdir = m_form.textBox_srcdir.Text;
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_INIT_ORGFILENAME);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_MOD_GUID_GENRDIR
            S_MOD_GUID_GENRDIR
            シートのguidとgenrdirの更新
        */
        void S_MOD_GUID_GENRDIR(bool bFirst)
        {
            if (bFirst)
            {
                newguidsheet("config");
                modgenrdir("setting.ini",m_new_psgg,m_new_genfile);
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_DONEDIALOG);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_NEW_GUID
            configシートのGUIDを更新
        */
        void S_NEW_GUID(bool bFirst)
        {
            if (bFirst)
            {
                newguidsheet("config");
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_DONEDIALOG);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_REPLACE_WORDS
            PSGG内の全ステートマシン名を入れ替える
        */
        void S_REPLACE_WORDS(bool bFirst)
        {
            if (bFirst)
            {
                np_replace_psgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_COPY_FILEDB ); }
            else { SetNextState( S_ERRMSG ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SET_PATH_SETTINGINI
            シートsetting.ini内のパスを調整する。
        */
        void S_SET_PATH_SETTINGINI(bool bFirst)
        {
            if (bFirst)
            {
                set_path_setting();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_CREATE_PSGG);
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
                SetNextState(S_INITNAMES);
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

}
