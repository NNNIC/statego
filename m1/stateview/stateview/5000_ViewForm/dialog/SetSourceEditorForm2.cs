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

namespace stateview._5000_ViewForm.dialog
{
    public partial class SetSourceEditorForm2 : Form
    {
        string historyfile { get {
                return Path.Combine( Path.GetTempPath(), "psgg-sourceeditor-history.txt");
        } }

        public SetSourceEditorForm2()
        {
            InitializeComponent();
        }

        private void SetSourceEditorForm2_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            this.DialogResult = DialogResult.None;

            this.checkBox_usecmn.Checked = G.use_cmn_editor;

            init_lang_setname_command();
            init_editor_set();
            init_search_editors();
            init_history();

            textBox_Command.Select();

            textBox_SetName.Visible = false;      //停止
            textBox_LabelSetName.Visible = false; //停止
            textBox_SetNameDefault.Visible = false; //停止
        }
        void init_lang_setname_command()
        {
            this.textBox_Lang.Text = SettingIniUtil.GetLangFramework();
            this.textBox_Command.Text = G.external_source_editor;
            this.checkBox_jumpforVS2015.Checked = G.source_editor_vs2015_support;
            this.checkBox_use_batch.Checked = G.use_batch_for_source_editor_open;
        }
        void init_editor_set()
        {
            var setname = G.source_editor_set;
            this.textBox_SetName.Visible = false;
            this.textBox_SetName.Enabled = false;
            this.textBox_SetName.Text = setname;

            this.textBox_SetNameDefault.Visible = false;
            this.textBox_SetNameDefault.Enabled = false;

            if (string.IsNullOrEmpty(setname))
            {
                this.textBox_SetNameDefault.Visible = true;
                this.textBox_SetNameDefault.Enabled = true;
            }
            else
            {
                this.textBox_SetName.Visible = true;
                this.textBox_SetName.Enabled = true;
            }
        }

        void init_history()
        {
            var list = new List<string>();
            try { 
                if (File.Exists(historyfile))
                {
                    var s = File.ReadAllText(historyfile,Encoding.UTF8);
                    var newlist = StringUtil.SplitTrim(s,'\x0a');
                    if (newlist!=null)
                    {
                        list.AddRange(newlist);
                    }
                }
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Error history setup : " + e.Message);
            }
            listBox_History.Items.Clear();
            foreach(var i in list)
            {
                listBox_History.Items.Add(i);
            }
        }
        #region 検索可能エディタ
        public class seatchEditorItem {
            public string label;
            public string path;
            public string param;
            public bool use_VisualStudioFileOpenTool=false;
        }
        Dictionary<string,seatchEditorItem> m_searcheditor_dic;

        /// <summary>
        ///  エディタ定義
        ///  item 1 - ラベル名
        ///  item 2 - パス 環境変数と * 込み
        ///  item 3 - 実行時引数 %1:ファイル名 %2:行数
        ///  item 4 - (オプション)
        ///   
        ///   ref https://stackoverflow.com/questions/350323/open-a-file-in-visual-studio-at-a-specific-line-number
        ///  
        /// </summary>
        List<string[]> m_editor_defines = new List<string[]>() {

            new string[] {
                @"Microsoft Visual Studio 2015",
                @"%ProgramFiles(x86)%\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe",
                "%3 /Command \"Edit.Goto %2\" %1",
                @"use_VisualStudioFileOpenTool"
            },
            new string[] {
                @"Microsoft Visual Studio 2015 %VS2015Exe%",
                @"%VS2015Exe%",
                "%3 /Command \"Edit.Goto %2\" %1",
                @"use_VisualStudioFileOpenTool"
            },

            new string[] {
                @"Microsoft Visual Studio 2017",
                @"%ProgramFiles(x86)%\Microsoft Visual Studio\2017\*\Common7\IDE\devenv.exe",
                "%3 /Command \"Edit.Goto %2\" %1",
                //@"/Edit %1",
                @"use_VisualStudioFileOpenTool"
            },
            new string[] {
                @"Microsoft Visual Studio 2017 %VS2017Exe%",
                @"%VS2017Exe%",
                "%3 /Command \"Edit.Goto %2\" %1",
                //@"/Edit %1",
                @"use_VisualStudioFileOpenTool"
            },

            new string[] {
                "Microsoft Visual Studio 2019",
                @"%ProgramFiles(x86)%\Microsoft Visual Studio\2019\*\Common7\IDE\devenv.exe",
                "%3 /Command \"Edit.Goto %2\" %1",
                //@"/Edit %1",
                @"use_VisualStudioFileOpenTool"
            },
            new string[] {
                "Microsoft Visual Studio 2019 %VS2019Exe%",
                @"%VS2019Exe%",
                "%3 /Command \"Edit.Goto %2\" %1",
                //@"/Edit %1",
                @"use_VisualStudioFileOpenTool"
            },

            new string[] {
                "Visual Studio Code",
                @"%USERPROFILE%\AppData\Local\Programs\Microsoft VS Code\bin\code.cmd",
                @"-g %1:%2"
            },
            new string[] {
                "Visual Studio Code %VSCodeExe%",
                @"%VSCodeExe%",
                @"-g %1:%2"
            },
        };
        #endregion

        void init_search_editors()
        {
            m_searcheditor_dic = new Dictionary<string, seatchEditorItem>();
            foreach(var def in m_editor_defines)
            {
                var label = ArrayUtil.GetVal(def,0,null);
                if (label == null) throw new SystemException("{9FA0B5F9-6EAB-4FB5-844A-639955E876FD}");
                var path = ArrayUtil.GetVal(def,1,null);
                if (path == null) throw new SystemException("{D4DB57A3-DA75-44D0-B7AE-CF977A295228}");
                var param = ArrayUtil.GetVal(def,2,null);
                if (param == null) throw new SystemException("{148E973D-27D1-4AC3-96EE-DB1DE7BEE0C4}");
                var opt = ArrayUtil.GetVal(def,3,null);
                
                // パスを実際に存在するものに変換
                var path2 = PathUtil.ExtractPathWithEnvVals(path); 
                var path3 = string.Empty;
                if (path2.Contains("*"))
                {
                    try {
                        path3 = PathUtil.FindMatchPathFileWithAstariskPath(path2);
                    } catch (SystemException e ){
                        G.NoticeToUser(e.Message);       
                    }
                }
                else
                {
                    if (File.Exists(path2))
                    {
                        path3 = path2;
                    }
                }
                if (!string.IsNullOrEmpty(path3))
                {
                    var item = new seatchEditorItem();
                    item.label = label;
                    item.path = path3;
                    item.param = param;
                    item.use_VisualStudioFileOpenTool = (opt!=null && opt.Contains("use_VisualStudioFileOpenTool"));
                    m_searcheditor_dic.Add(item.label,item);
                }
            }

            comboBox_EditorCandidate.Items.Clear();
            if (m_searcheditor_dic.Count == 0)
            {
                comboBox_EditorCandidate.Items.Add("(なし)");
            }
            else {
                foreach(var key in m_searcheditor_dic.Keys)
                {
                    comboBox_EditorCandidate.Items.Add(key);
                }
                var cur_command_path = G.external_source_editor_path;
                var index = 0;
                for(var i= 0; i < comboBox_EditorCandidate.Items.Count; i++)
                {
                    var item = DictionaryUtil.Get(m_searcheditor_dic,comboBox_EditorCandidate.Items[i].ToString());
                    if (item != null)
                    {
                        if (item.path == cur_command_path)
                        {
                            index = i;
                            break;
                        }
                    }
                }
                comboBox_EditorCandidate.Text = comboBox_EditorCandidate.Items[index].ToString();
            }
        }

        private void button_Input_Click(object sender, EventArgs e)
        {
            var dq = "\"";
            var key = comboBox_EditorCandidate.Text;
            if (m_searcheditor_dic.ContainsKey(key))
            {
                var item = m_searcheditor_dic[key];
                textBox_Command.Text = dq + item.path +dq + " " + item.param;
                checkBox_jumpforVS2015.Checked = item.use_VisualStudioFileOpenTool;
            }
        }

        private void button_SaveClose_Click(object sender, EventArgs e)
        {
            if (
                (!string.IsNullOrEmpty(G.load_file) && File.Exists(G.load_file) && (new FileInfo(G.load_file)).Attributes.HasFlag(FileAttributes.ReadOnly) )
                ||
                (!string.IsNullOrEmpty(G.psgg_file) && File.Exists(G.psgg_file) && (new FileInfo(G.psgg_file)).Attributes.HasFlag(FileAttributes.ReadOnly) )
             )
            {
                MessageBox.Show(G.Localize("ssed_readonly"));
                return;
            }

            G.external_source_editor = textBox_Command.Text.Trim();
            G.source_editor_vs2015_support = checkBox_jumpforVS2015.Checked;
            G.source_editor_set = textBox_SetName.Text;
            G.use_batch_for_source_editor_open = checkBox_use_batch.Checked;

            history_record(G.external_source_editor);

            var option = (G.source_editor_vs2015_support ? WordStorage.Store.srceditcmd_option_vs2015 :"");


            var registname = SettingIniUtil.GetLangFramrwork_registName();
            if (this.checkBox_usecmn.Checked)
            {
                registname = "cmn(cmn)";
            }

            if (!string.IsNullOrEmpty(G.source_editor_set)) {
                registname += "~~" + G.source_editor_set;
            }

            RegistryWork.Set_srceditcmd(G.external_source_editor, registname, option);
            RegistryWork.Set_execbatch_editor(G.use_batch_for_source_editor_open);

            RegistryWork.Set_use_cmn_editor(this.checkBox_usecmn.Checked);


            this.DialogResult = DialogResult.OK;

            Close();
        }
        void history_record(string i_s)
        {
            var s = i_s.Trim();

            var list = new List<string>();
            for(var i = 0; i< listBox_History.Items.Count; i++)
            {
                list.Add(listBox_History.Items[i].ToString().Trim());
            }

            var findindex = list.FindIndex(i=>i==s);
            if (findindex >= 0)
            {
                list.RemoveAt(findindex);
            }

            list.Insert(0,s);

            var filetext = string.Empty;
            foreach(var l in list)
            {
                if (!string.IsNullOrEmpty(filetext)) filetext += Environment.NewLine;
                filetext += l;
            }

            try { 
                File.WriteAllText(historyfile,filetext,Encoding.UTF8);
            } catch ( SystemException e)
            {
                G.NoticeToUser_warning("Error history_record. : " + e.Message);
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        //デフォルト表示時にクリック　textbox_setnameに替わる
        private void textBox_SetNameDefault_Click(object sender, EventArgs e)
        {
            textBox_SetNameDefault.Enabled = false;
            textBox_SetNameDefault.Visible = false;
            textBox_SetName.Visible = true;
            textBox_SetName.Enabled = true;
            textBox_SetName.Focus();
        }

        //textbox_setnameのフォーカスが外れる
        private void textBox_SetName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( textBox_SetName.Text ))
            {
                //設定値がないときは、textBox_SetNameDefaultをEnableへ。
                textBox_SetName.Enabled = false;
                textBox_SetName.Visible = false;
                textBox_SetNameDefault.Visible = true;
                textBox_SetNameDefault.Enabled = true;
            }
        }

        private void listBox_History_DoubleClick(object sender, EventArgs e)
        {
            var i = listBox_History.SelectedIndex;
            if (i>=0 && i < listBox_History.Items.Count)
            {
                textBox_Command.Text = listBox_History.Items[i].ToString();
            }
        }

        #region 言語フレームワークの変更
        private void textBox_Lang_DoubleClick(object sender, EventArgs e)
        {
            change_language_platform();
        }

        private void textBox_Lang_KeyDown(object sender, KeyEventArgs e)
        {
            change_language_platform();
        }
        private void button_changelangfw_Click(object sender, EventArgs e)
        {
            change_language_platform();
        }

        private void change_language_platform()
        {
            if (G.bDirty)
            {
                MessageBox.Show(G.Localize("setlf_warn") /*"保存されてない変更があるため編集ができません。変更がない状態にしてからお使い下さい。"*/);
                return;
            }

            var dlg = new SetSourceEditor_langPfChangeForm2();
            var ret = dlg.ShowDialog();
            if (ret == DialogResult.OK)
            {
                this.textBox_Lang.Text = SettingIniUtil.GetLangFramework();
                reset_cmdtext();
            }
        }
        #endregion

        private void button_cmdreset_Click(object sender, EventArgs e)
        {
            reset_cmdtext();
        }
        private void reset_cmdtext()
        {
            var langfw = textBox_Lang.Text;
            if (RegistryWork.Get_use_cmn_editor())
            {
                langfw = "cmn(cmn)";
            }

            if (string.IsNullOrEmpty(langfw)) return;
            textBox_Command.Text = RegistryWork.Get_srceditcmd(langfw);
            checkBox_jumpforVS2015.Checked = RegistryWork.Get_srceditcmd_option(langfw) == WordStorage.Store.srceditcmd_option_vs2015;
        }

        private void label_help_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","set-source-editor-dlg",Globals.system_lang=="jpn");
        }

        private void button_editor_cand_Click(object sender, EventArgs e)
        {

            button_editor_cand.Visible           = false;

            textBox_LabelEditorCandidate.Visible = true;
            comboBox_EditorCandidate.Visible     = true;
            button_Input.Visible                 = true;


        }

        private void checkBox_usecmn_CheckedChanged(object sender, EventArgs e)
        {
            var b = !checkBox_usecmn.Checked;
            set_lang_frame_onoff(b);
        }

        private void set_lang_frame_onoff(bool b)
        {
            this.textBox_LabelLang.Visible = b;
            this.textBox_Lang.Visible = b;
            this.button_changelangfw.Visible = b;

            this.textBox_LabelSetName.Visible = b;
            this.textBox_SetNameDefault.Visible = b;

            reset_cmdtext();
        }



    }
}
