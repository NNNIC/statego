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
//>>>

#pragma warning disable CS0649

namespace StateViewer_starter2.NEW2019 { 

    public partial class NewWorkControl  {
    #region implement

        string m_system_lang { get {  return RegistryWork.Get_lang()=="jp" ? "jpn" : "en";  } }

        string Localize(string id)
        {
            return WordStorage.Res.Get(id,m_system_lang);
        }

        string mcnd(string file)
        {
            if (m_err != null) return null;
            if (string.IsNullOrEmpty(file)) return null;

            var filename = Path.GetFileName(file);
            var newfilename = filename.Replace(m_orgname,m_newname);

            if (PathUtil.CheckValid(m_newdocdir))
            {
                return Path.Combine(m_newdocdir, newfilename);
            }
            else
            {
                m_err = "{6FDB2E09-C201-41EF-87CA-AED966294A9A}";
                return null;
            }
        }

        string mcng(string file)
        {
            if (m_err != null) return null;
            if (string.IsNullOrEmpty(file)) return null;
            var filename = Path.GetFileName(file);

            var newfilename = filename.Replace(m_orgname, m_newname);

            if (PathUtil.CheckValid(m_newsrcdir))
            {
                return Path.Combine(m_newsrcdir, newfilename);
            }
            else
            {
                m_err = "{51F6F6C7-C040-47A0-B914-BF6ED3151569}";
                return null;
            }
        }

        Encoding getgenc(string enc)
        {
            if (string.IsNullOrEmpty(enc))
            {
                return Encoding.UTF8;
            }
            try {
                var encoding = Encoding.GetEncoding(enc);
                return encoding;
            }
            catch { }
            return Encoding.UTF8;
        }
        string make_clone_confirm_msg()
        {
            var msg = WordStorage.Res.Get("cfc_filecreated", m_system_lang) + "\n";// "The below files will be created.\n";
            msg += m_new_psgg + "\n";
            msg += m_new_genfile + "\n";
            if (!string.IsNullOrEmpty(m_new_impfile))
            {
                msg += m_new_impfile + "\n";
            }
            if (!string.IsNullOrEmpty(m_new_macro))
            {
                msg += m_new_macro + "\n";
            }

            return msg;
        }

        void copyfiles() {
            Action<string,string> cp = (a,b) => {
                if (string.IsNullOrEmpty(a))
                {
                    //ない場合もある。 エラーではない！
                    return;
                }
                if (string.IsNullOrEmpty(b))
                {
                    m_err = "{F8BA0E1E-2B25-4CC5-8915-7CCC7030F55D}";
                    return;
                }
                
                if (PathUtil.CheckValid(a) && PathUtil.CheckValid(b))
                {
                    var dd = Path.GetDirectoryName(b);
                    if (!Directory.Exists(dd))
                    {
                        Directory.CreateDirectory(dd);
                    }
                    File.Copy(a,b,true);
                }
                else
                {
                    m_err = "{227B2D76-B11D-4A97-B18F-27AA1335CBE0}";
               }
            };

            //cp(m_org_excel, m_new_excel);
            cp(m_org_psgg, m_new_psgg);
            //cp(m_org_helpweb, m_new_helpweb);
            cp(m_org_genfile, m_new_genfile);
            cp(m_org_impfile, m_new_impfile);
            cp(m_org_macro, m_new_macro);
        }

#if obs
        string[] m_new_psgg_list;

        Hashtable m_header_ht;
        List<string> m_header_order;

        Hashtable m_setting_ht;
        List<string> m_setting_order;

        void np_new_psgg() {

            var header = m_pd.m_header;
            var header_order = new List<string>();
            var header_ht = IniUtil.CreateHashtableWithOrderList(header,out header_order);

            var newguid = Guid.NewGuid().ToString();

            IniUtil.SetValueFromHashtable("guid",newguid,ref header_ht);

            var header_new = IniUtil.MakeOutput(header_ht,header_order);
                         
            var settingini = m_pd.m_settingini;
            var settingini_order = new List<string>();
            var settingini_ht = IniUtil.CreateHashtableWithOrderList(settingini,out settingini_order);

            IniUtil.SetValueFromHashtable("guid",newguid,ref header_ht);

            var settingini_new = IniUtil.MakeOutput(settingini_ht,settingini_order);

            m_new_psgg_list = new string[ m_pd.m_psgg_parts.Count];
            for(var n = 0; n < m_new_psgg_list.Length; n++)
            {
                if (n==0)
                {
                    m_new_psgg_list[n] = header_new;
                }
                else if (n==m_pd.m_settingini_index)
                {
                    m_new_psgg_list[n] = settingini_new;
                }
                else
                {
                    m_new_psgg_list[n] = m_pd.m_psgg_parts[n];
                }
            }
        }
        void np_replace_psgg()
        {
            for(var n = 0; n<m_new_psgg_list.Length; n++)
            {
                var buf = m_new_psgg_list[n];
                m_new_psgg_list[n] = buf.Replace(m_orgname, m_newname);
            }
        }
#endif
        void chgs(string file, Encoding enc = null)
        {
            if (file==null) return;
            try {
                if (enc == null) enc = Encoding.UTF8;
                enc = getEncW_CheckBOM(file,enc);
                var text = File.ReadAllText(file,enc);
                var newtext= text.Replace(m_orgname,m_newname);
                if (IsCloneExcnage_withUpperCamelWord()) //uppercamelも対象
                {
                    var org_uppercamel = StringUtil.convert_to_camel_word(m_orgname,true);
                    var new_uppercamel = StringUtil.convert_to_camel_word(m_newname,true);

                    newtext = newtext.Replace(org_uppercamel,new_uppercamel);
                }

                File.WriteAllText(file,newtext,enc);
            } catch (SystemException e)
            {
                m_err = "{4F7500D7-C034-4C87-B0D8-D2D202592ADF}" + e.Message;
            }
        }

        Encoding getEncW_CheckBOM(string file, Encoding enc)
        {
            if (enc == Encoding.UTF8)
            {
                var bom = false;
                var bytes = File.ReadAllBytes(file);
                if (bytes.Length > 3)
                {
                    if ((bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf))
                    {
                        bom = true;
                    }
                }
                enc = new UTF8Encoding(bom);                
            }
            return enc;
        }

        bool IsCloneExcnage_withUpperCamelWord() //rust用 ファイル名をupper camel文字列も変更の対象にする。
        {
            var s = IniUtil.GetValueFromHashtable(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_clone_exchange, m_setting_ht);
            return s == WordStorage.Store.settingini_setupinfo_clone_exchange_with_upper_camel_word;
        }
        void np_create_psgg()
        {
            m_new_psgg_list[0] = IniUtil.MakeOutput(m_header_ht, m_header_order);

            m_new_psgg_list[m_pd.m_settingini_index] = 
                m_setting_marks[0] + "\n" + 
                IniUtil.MakeOutput(m_setting_ht, m_setting_order) + "\n" +
                m_setting_marks[1];

            m_new_psgg_list[m_pd.m_config_index] = 
                m_config_marks[0] + "\n" +
                IniUtil.MakeOutput(m_config_ht, m_config_order) + "\n" +
                m_config_marks[1];

            var s = string.Empty;
            foreach(var p in m_new_psgg_list)
            {
                if (!string.IsNullOrEmpty(s)) s += "\n";
                s += p;
            }
            File.WriteAllText(m_new_psgg, s, Encoding.UTF8);
        }
        bool show_confirm_dlg(string msg)
        {
            var result = MessageBox.Show(msg, "CONFIRM", MessageBoxButtons.OKCancel );
            return result == DialogResult.OK;
        }
        void show_err_dlg() {
            MessageBox.Show(m_err);
        }
        void show_done_dlg() {
            MessageBox.Show(Localize("ccf_done"));
        }

        string[] m_new_psgg_list;

        Hashtable m_header_ht;
        List<string> m_header_order; //header再生用オーダー

        Hashtable m_setting_ht;
        List<string> m_setting_order; //setting再生成用オーダー
        string[]    m_setting_marks;

        Hashtable m_config_ht;
        List<string> m_config_order;  //config再生用オーダー
        string[] m_config_marks;

        void ng_prepare_newpsgg()
        {
            m_pd.read_output_psgg_parts(); //全PSGG読込む

            m_new_psgg_list = new string[ m_pd.m_psgg_parts.Count]; //コピー
            for(var n = 0; n < m_new_psgg_list.Length; n++){ m_new_psgg_list[n] = m_pd.m_psgg_parts[n]; }
        }

        void np_replace_psgg()
        {
            for(var n = 0; n<m_new_psgg_list.Length; n++)
            {
                var buf = m_new_psgg_list[n];
                // 通常
                var buf2 = buf.Replace(m_orgname, m_newname);

                //lower camel
                var buf3 = buf2.Replace(
                    StringUtil.convert_to_camel_word(m_orgname,false), 
                    StringUtil.convert_to_camel_word(m_newname,false)
                    );
                //upper camel
                var buf4 = buf3.Replace(
                    StringUtil.convert_to_camel_word(m_orgname,true), 
                    StringUtil.convert_to_camel_word(m_newname,true)
                    );
                //snake
                var buf5 = buf4.Replace(
                    StringUtil.convert_to_snake_word_and_lower(m_orgname), 
                    StringUtil.convert_to_snake_word_and_lower(m_newname)
                    );
                m_new_psgg_list[n] = buf5;
            }
        }

        void np_create_ht()
        {
            //ヘッダーとsettinig.iniのみ取出し、ハッシュを作る
            var header = m_new_psgg_list[0];
            var setting = m_new_psgg_list[m_pd.m_settingini_index];
            var config  = m_new_psgg_list[m_pd.m_config_index];

            setting = __crop_topLine(setting, out m_setting_marks );
            config  = __crop_topLine(config, out m_config_marks);

            m_header_ht = IniUtil.CreateHashtableWithOrderList(header,out m_header_order);
            m_setting_ht = IniUtil.CreateHashtableWithOrderList(setting,out m_setting_order);
            m_config_ht = IniUtil.CreateHashtableWithOrderList(config, out m_config_order);
        }

        string __crop_topLine(string s, out string[] marks)
        {
            marks = null;
            if (string.IsNullOrEmpty(s)) return s;

            var newlinestr = StringUtil.FindNewLineChar(s);

            var mark_top = RegexUtil.Get1stMatch(@"^\-\-\-[\s\S]+?###VARIOUS\-CONTENTS\-BEGIN###",s);

            var buf_wo_top = s.Substring(mark_top.Length + newlinestr.Length);

            var mark_bot_index = buf_wo_top.IndexOf("###VARIOUS-CONTENTS-END###");
            
            var buf_wo_top_bot = buf_wo_top.Substring(0,mark_bot_index);

            marks = new string[] {
                mark_top + "\n",
                "###VARIOUS-CONTENTS-END###\n\n"
            };

            return buf_wo_top_bot;
        }

        void np_newguid()
        {
            var newguid = Guid.NewGuid().ToString();
            IniUtil.SetValueFromHashtable("guid",newguid,ref m_header_ht);
            IniUtil.SetValueFromHashtable("guid",newguid,ref m_config_ht);
        }

        void np_path_setting()
        {
            var xlsdir = Path.GetDirectoryName(m_new_psgg);
            var gendir = Path.GetDirectoryName(m_new_genfile);
            IniUtil.SetValueFromHashtable(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_xlsdir,xlsdir, ref m_setting_ht);
            IniUtil.SetValueFromHashtable(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_gendir,gendir, ref m_setting_ht);

            var genrdir = PathUtil.GetRelativePath(xlsdir, gendir);
            if (string.IsNullOrEmpty(genrdir)) genrdir = ".";

            IniUtil.SetValueFromHashtable(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_genrdir,genrdir, ref m_setting_ht);
        }

        void np_repw_files()
        {
            chgs(m_new_genfile, m_genc);
            chgs(m_new_impfile, m_genc);
            chgs(m_new_macro);
        }
        void np_set_helpweb()
        {       
            var helpwebrpath = PathUtil.GetRelativePath(m_starter_kit_path + @"\..",m_org_helpweb);
            IniUtil.SetValueFromHashtable(WordStorage.Store.settingini_group_setting, WordStorage.Store.settingini_setting_helpweb,helpwebrpath,ref m_setting_ht);
        }
        void np_set_kitpath()
        {
            var kitpath = Path.GetFullPath(Path.GetDirectoryName(m_pd.m_filepath));
            var kitrpath = PathUtil.GetRelativePath(m_starter_kit_path + @"\..",kitpath);
            IniUtil.SetValueFromHashtable(WordStorage.Store.settingini_group_setting, WordStorage.Store.settingini_setting_kitpath,kitrpath,ref m_setting_ht);
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
        //             psggConverterLib.dll converted from psgg-file:NewWorkControl.psgg

        /*
            E_DEC_ENC
            生成時用エンコード
        */
        public Encoding m_genc = Encoding.UTF8;
        /*
            E_DEC_NAME
        */
        public string m_orgname;
        public string m_newname;
        public string m_newdocdir;
        public string m_newsrcdir;
        /*
            E_DEC_NEWFILES
        */
        //新 フルパス格納
        public string m_new_excel;
        public string m_new_psgg;
        public string m_new_helpweb;
        public string m_new_genfile;
        public string m_new_impfile;
        public string m_new_macro;
        /*
            E_DEC_ORGFILES
        */
        //元 フルパス格納
        public string m_org_excel;
        public string m_org_psgg;
        public string m_org_helpweb;
        public string m_org_genfile;
        public string m_org_impfile;
        public string m_org_macro;
        /*
            E_KIT_PATH
            スタートキットパス格納
        */
        public string m_starter_kit_path;
        /*
            E_OK
        */
        public bool m_ok = false;
        /*
            E_PSGGDATA
        */
        public psgg_data m_pd;
        /*
            E_VAR
        */
        public string m_err = null;
        /*
            S_CHGWORD_OTHERS
            他のファイルの該当部分置き換え
        */
        void S_CHGWORD_OTHERS(bool bFirst)
        {
            if (bFirst)
            {
                np_repw_files();
            }
            // branch
            if (m_err==null) { SetNextState( S_DONEDIALOG1 ); }
            else { SetNextState( S_CLEAR_ERR3 ); }
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
                SetNextState(S_INIT_DSTFILENAME);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CLEAR_ERR1
        */
        void S_CLEAR_ERR1(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_CLEAR_ERR3);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CLEAR_ERR2
        */
        void S_CLEAR_ERR2(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_NG);
            }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CLEAR_ERR3
        */
        void S_CLEAR_ERR3(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                SetNextState(S_ERRMSG);
            }
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
            var bOk = show_confirm_dlg(msg);
            // branch
            if (bOk) { SetNextState( S_COPY ); }
            else { SetNextState( S_CLEAR_ERR2 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_COPY
            コピー
        */
        void S_COPY(bool bFirst)
        {
            if (bFirst)
            {
                copyfiles();
            }
            // branch
            if (m_err == null) { SetNextState( S_PREPARE_NEW_PSGG ); }
            else { SetNextState( S_CLEAR_ERR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CREATE_PSGG_HT
            HeaderとSetting.iniのヘッダーを作成する。
        */
        void S_CREATE_PSGG_HT(bool bFirst)
        {
            if (bFirst)
            {
                np_create_ht();
            }
            // branch
            if (m_err == null) { SetNextState( S_SET_NEWGUID ); }
            else { SetNextState( S_CLEAR_ERR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_CREATE_PSGG1
            生成したファイルから新規のＰＳＧＧを作成
        */
        void S_CREATE_PSGG1(bool bFirst)
        {
            if (bFirst)
            {
                np_create_psgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_CHGWORD_OTHERS ); }
            else { SetNextState( S_CLEAR_ERR3 ); }
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
                SetNextState(S_OK);
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
                show_err_dlg();
            }
            //
            if (!HasNextState())
            {
                SetNextState(S_NG);
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
                m_genc = getgenc(m_pd.m_src_enc);
            }
            // branch
            if (m_err == null) { SetNextState( S_CONFRM_DIALOG ); }
            else { SetNextState( S_CLEAR_ERR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_NG
        */
        void S_NG(bool bFirst)
        {
            if (bFirst)
            {
                m_ok = false;
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
            S_OK
        */
        void S_OK(bool bFirst)
        {
            if (bFirst)
            {
                m_ok = true;
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
            S_PREPARE_NEW_PSGG
            新規PSGGを作成する準備
        */
        void S_PREPARE_NEW_PSGG(bool bFirst)
        {
            if (bFirst)
            {
                ng_prepare_newpsgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_REPLACE_WORDS1 ); }
            else { SetNextState( S_CLEAR_ERR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_REPLACE_WORDS1
            PSGG内の全ステートマシン名を入れ替える
        */
        void S_REPLACE_WORDS1(bool bFirst)
        {
            if (bFirst)
            {
                np_replace_psgg();
            }
            // branch
            if (m_err==null) { SetNextState( S_CREATE_PSGG_HT ); }
            else { SetNextState( S_CLEAR_ERR1 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SET_NEWGUID
            新規GUIDをPSGGへ
        */
        void S_SET_NEWGUID(bool bFirst)
        {
            if (bFirst)
            {
                np_newguid();
            }
            // branch
            if (m_err==null) { SetNextState( S_SET_PATH_SETTINGINI2 ); }
            else { SetNextState( S_CLEAR_ERR3 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SET_PATH_SETTINGINI1
            シートsetting.ini内のパスを調整する。
        */
        void S_SET_PATH_SETTINGINI1(bool bFirst)
        {
            if (bFirst)
            {
                np_path_setting();
            }
            // branch
            if (m_err==null) { SetNextState( S_CREATE_PSGG1 ); }
            else { SetNextState( S_CLEAR_ERR3 ); }
            //
            if (HasNextState())
            {
                GoNextState();
            }
        }
        /*
            S_SET_PATH_SETTINGINI2
            ヘルプWEB設定
            kitpath設定
        */
        void S_SET_PATH_SETTINGINI2(bool bFirst)
        {
            if (bFirst)
            {
                np_set_helpweb();
                np_set_kitpath();
            }
            // branch
            if (m_err==null) { SetNextState( S_SET_PATH_SETTINGINI1 ); }
            else { SetNextState( S_CLEAR_ERR3 ); }
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
                SetNextState(S_CLEAR_ERR);
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
}