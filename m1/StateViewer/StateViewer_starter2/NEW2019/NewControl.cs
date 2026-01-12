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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace StateViewer_starter2.NEW2019
{
    partial class NewControl  {
   
        bool m_busy = false;
        bool m_lang_j_or_e { get {
                return RegistryWork.Get_lang() == "jp";
            } }

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
    
        private void Run()
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

        public void RunForEvent( NewFormEvent evt )
        {
            if (m_busy) return;

            m_evt = evt;

            int LOOPMAX = (int)(1E+5);
		    for(var loop = 0; loop <= LOOPMAX; loop++)
		    {
			    if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			    Update();
			    if (!m_busy) break;
		    }
        }

	    #region    // [PSGG OUTPUT START] indent(8) $/./$
        //             psggConverterLib.dll converted from psgg-file:NewControl.psgg

        /*
            E_EVENT
        */
        NewFormEvent m_evt;
        /*
            E_FORM
        */
        public NewForm m_form;
        /*
            S_BACKTO_EVENT
        */
        void S_BACKTO_EVENT(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_EVT_RESET);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_CHANGED_STATENAME
            ステートマシン名が変更された
        */
        void S_CHANGED_STATENAME(bool bFirst)
        {
            //
            if (bFirst)
            {
                statemachine_namechanged();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_CLOSING
            クローズ中
        */
        void S_CLOSING(bool bFirst)
        {
            //
            if (bFirst)
            {
                form_closing();
            }
        }
        /*
            S_CREATE
            ステートマシン生成
        */
        void S_CREATE(bool bFirst)
        {
            //
            if (bFirst)
            {
                create_statemachine();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_DOC_UNDER_SCR
            ソースフォルダの直下にdoc強制？
        */
        void S_DOC_UNDER_SCR(bool bFirst)
        {
            //
            if (bFirst)
            {
                use_doc_undersrc_select();
                srcpath_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_DRAW_TREE
            ツリー表示
        */
        void S_DRAW_TREE(bool bFirst)
        {
            //
            if (bFirst)
            {
                draw_tree();
            }
            //
            if (!HasNextState())
            {
                Goto(S_DRAW_TREE_RETURN);
            }
        }
        /*
            S_DRAW_TREE_RETURN
        */
        void S_DRAW_TREE_RETURN(bool bFirst)
        {
            ReturnState();
            NoWait();
        }
        /*
            S_DRAW_TREE_START
        */
        void S_DRAW_TREE_START(bool bFirst)
        {
            Goto(S_DRAW_TREE);
            NoWait();
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_EVENT
        */
        void S_EVENT(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_busy = false;
            }
            // branch
            if (m_evt == NewFormEvent.button_starterkit_dir_open) { Goto( S_KIT_DIR_OPEN ); }
            else if (m_evt == NewFormEvent.button_starterkit_dir_reset) { Goto( S_KIT_DIR_RESET ); }
            else if (m_evt == NewFormEvent.button_gendir_open) { Goto( S_OPENSRC ); }
            else if (m_evt == NewFormEvent.button_docdir_open) { Goto( S_OPENDOC ); }
            else if (m_evt == NewFormEvent.button_goold) { Goto( S_GO_PREV ); }
            else if (m_evt == NewFormEvent.button_create) { Goto( S_CREATE ); }
            else if (m_evt == NewFormEvent.treeview_selected) { Goto( S_TREEVIEW_SELECT ); }
            else if (m_evt == NewFormEvent.checkbox_usedoc_changed) { Goto( S_DOC_UNDER_SCR ); }
            else if (m_evt == NewFormEvent.checkbox_doc_changed) { Goto( S_SHOWHIDE_DOC ); }
            else if (m_evt == NewFormEvent.combobox_docpathusage_changed) { Goto( S_SELECT_DOCPATHUSAGE ); }
            else if (m_evt == NewFormEvent.statemachine_name_changed) { Goto( S_CHANGED_STATENAME ); }
            else if (m_evt == NewFormEvent.statamachine_checkbox_control_checked) { Goto( S_TREEVIEW_SELECT1 ); }
            else if (m_evt == NewFormEvent.statemachine_reset) { Goto( S_RESET_STATAMACHINE ); }
            else if (m_evt == NewFormEvent.gendir_path_textchanged) { Goto( S_SRCPATH_TEXTCHG ); }
            else if (m_evt == NewFormEvent.fromclosing) { Goto( S_CLOSING ); }
            //
            if (HasNextState())
            {
                NoWait();
                m_busy = true;
            }
        }
        /*
            S_EVENT1
        */
        void S_EVENT1(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_busy =false;
            }
            // branch
            if (m_evt == NewFormEvent.onload) { Goto( S_SET_LATEST_SETTING ); }
            //
            if (HasNextState())
            {
                NoWait();
                m_busy=true;
            }
        }
        /*
            S_EVT_RESET
        */
        void S_EVT_RESET(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_evt = NewFormEvent.none;
            }
            //
            if (!HasNextState())
            {
                Goto(S_EVENT);
            }
        }
        /*
            S_GO_PREV
            以前の生成機能へ
        */
        void S_GO_PREV(bool bFirst)
        {
            //
            if (bFirst)
            {
                go_previous_creator();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_INIT
        */
        void S_INIT(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_evt = NewFormEvent.none;
            }
            //
            if (!HasNextState())
            {
                Goto(S_EVENT1);
            }
            //
            if (HasNextState())
            {
                NoWait();
            }
        }
        /*
            S_KIT_DIR_OPEN
        */
        void S_KIT_DIR_OPEN(bool bFirst)
        {
            //
            if (bFirst)
            {
                var text = m_form.label_starterkitdir_path.Text;
                using (var dlg = new CommonOpenFileDialog())
                {
                    dlg.IsFolderPicker = true;
                    dlg.Title = "Select Folder";
                    if (!string.IsNullOrEmpty(text))
                    {
                        dlg.InitialDirectory = text;
                    }
                    var result = dlg.ShowDialog();
                    if (result== CommonFileDialogResult.Ok)
                    {
                        m_form.label_starterkitdir_path.Text =  dlg.FileName;
                    }
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_LOADTREEVIEW1);
            }
        }
        /*
            S_KIT_DIR_RESET
        */
        void S_KIT_DIR_RESET(bool bFirst)
        {
            //
            if (bFirst)
            {
                KITROOTPATH_Reset();
            }
            //
            if (!HasNextState())
            {
                Goto(S_LOADTREEVIEW1);
            }
        }
        /*
            S_LOADTREEVIEW
        */
        void S_LOADTREEVIEW(bool bFirst)
        {
            GoSubState(S_DRAW_TREE_START,S_SET_LATEST_KIT);
            NoWait();
        }
        /*
            S_LOADTREEVIEW1
        */
        void S_LOADTREEVIEW1(bool bFirst)
        {
            GoSubState(S_DRAW_TREE_START,S_BACKTO_EVENT);
            NoWait();
        }
        /*
            S_OPENDOC
        */
        void S_OPENDOC(bool bFirst)
        {
            //
            if (bFirst)
            {
                var text = m_form.textBox_xlsdir_path.Text;
                using (var dlg = new CommonOpenFileDialog())
                {
                    dlg.IsFolderPicker = true;
                    dlg.Title = "Select Folder";
                    if (!string.IsNullOrEmpty(text))
                    {
                        dlg.InitialDirectory = text;
                    }
                    var result = dlg.ShowDialog();
                    if (result== CommonFileDialogResult.Ok)
                    {
                        m_form.textBox_xlsdir_path.Text =  dlg.FileName;
                    }
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_OPENSRC
        */
        void S_OPENSRC(bool bFirst)
        {
            //
            if (bFirst)
            {
                var text = m_form.textBox_gendir_path.Text;
                using (var dlg = new CommonOpenFileDialog())
                {
                    dlg.IsFolderPicker = true;
                    dlg.Title = "Select Folder";
                    if (!string.IsNullOrEmpty(text))
                    {
                        dlg.InitialDirectory = text;
                    }
                    var result = dlg.ShowDialog();
                    if (result== CommonFileDialogResult.Ok)
                    {
                        m_form.textBox_gendir_path.Text =  dlg.FileName;
                    }
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_SRCPATH_TEXTCHG1);
            }
        }
        /*
            S_RESET_STATAMACHINE
            ステートマシン名リセット
        */
        void S_RESET_STATAMACHINE(bool bFirst)
        {
            //
            if (bFirst)
            {
                reset_statemachine();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_SELECT_DOCPATHUSAGE
            docパス利用方法定義
        */
        void S_SELECT_DOCPATHUSAGE(bool bFirst)
        {
            //
            if (bFirst)
            {
                select_doc_usage();
                srcpath_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_SET_LATEST_KIT
            最後に設定したキットを指定
        */
        void S_SET_LATEST_KIT(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_latest_kit();
            }
            //
            if (!HasNextState())
            {
                Goto(S_EVT_RESET);
            }
        }
        /*
            S_SET_LATEST_SETTING
            最後に使用した
            - kit パス
            - ステートマシン名
            - saveパス
            - docパス
            初回はデフォルト
        */
        void S_SET_LATEST_SETTING(bool bFirst)
        {
            //
            if (bFirst)
            {
                set_latest_settings();
                m_form.DialogResult = DialogResult.None;
            }
            //
            if (!HasNextState())
            {
                Goto(S_LOADTREEVIEW);
            }
        }
        /*
            S_SHOWHIDE_DOC
        */
        void S_SHOWHIDE_DOC(bool bFirst)
        {
            //
            if (bFirst)
            {
                show_hide_doc();
                srcpath_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_SRCPATH_TEXTCHG
            ソースパス変更
        */
        void S_SRCPATH_TEXTCHG(bool bFirst)
        {
            //
            if (bFirst)
            {
                srcpath_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_SRCPATH_TEXTCHG1
            ソースパス変更
        */
        void S_SRCPATH_TEXTCHG1(bool bFirst)
        {
            //
            if (bFirst)
            {
                srcpath_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            Goto(S_INIT);
            NoWait();
        }
        /*
            S_TREEVIEW_SELECT
            ツリー上で選択
        */
        void S_TREEVIEW_SELECT(bool bFirst)
        {
            //
            if (bFirst)
            {
                treeview_select();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }
        /*
            S_TREEVIEW_SELECT1
            ステートマシン名にControl強制ON/OFF
        */
        void S_TREEVIEW_SELECT1(bool bFirst)
        {
            //
            if (bFirst)
            {
                force_control_changed();
            }
            //
            if (!HasNextState())
            {
                Goto(S_BACKTO_EVENT);
            }
        }


        #endregion // [PSGG OUTPUT END]

        // write your code below
        #region my code
        public string DEFAULT_KITROOT
        {
            get
            {
                var kitroot = string.Empty;
                if (Application.ExecutablePath.Contains(@"\StateViewer\"))
                {
                    //Debug用
                    var psgg_rootdir =  (new DirectoryInfo(Path.Combine(Application.ExecutablePath,@"..\..\..\..\..\..\.."))).FullName;                     
                    var target = Path.Combine(psgg_rootdir,@"psgg-starter-kit\starterkit2");
                    kitroot = target; 
                }
                else
                { //Release
                    var install_dir = (new DirectoryInfo(Path.Combine(Application.ExecutablePath,@"..\"))).FullName;
                    var target      = Path.Combine(install_dir,@"starterkit2");
                    kitroot = target;
                }
                return kitroot;
            }
        }

        void KITROOTPATH_Reset()
        {
            m_form.label_starterkitdir_path.Text = DEFAULT_KITROOT;
            m_form.textBox_desc.Text = string.Empty;
            m_form.textBox_selectstarterkit.Text = string.Empty;

            node_reset_color();
        }

        void set_latest_settings()
        {
            var kitroot = RegistryWork.Get_templatedir();
            if (string.IsNullOrEmpty(kitroot))
            {
                kitroot = DEFAULT_KITROOT;
            }

            m_form.label_starterkitdir_path.Text = kitroot;
            m_form.textBox_statemachine.Text = RegistryWork.Get_statemchine();
            m_form.textBox_gendir_path.Text = RegistryWork.Get_gendir();
            m_form.textBox_xlsdir_path.Text = RegistryWork.Get_xlsdir();
            m_form.checkBox_control_name.Checked = RegistryWork.Get_force_controlname();

            statemachine_namechanged();

            //m_form.checkBox_specifydoc.Checked = RegistryWork.Get_docundersrc_xlsdir();
            //m_form.checkBox_xlsdir.Checked = RegistryWork.Get_free_xlsdir();

            m_form.comboBox_docpath.SelectedIndex = 0;

            select_doc_usage();
            //if (m_form.checkBox_specifydoc.Checked)
            //{
            //    use_doc_undersrc_select();
            //}
            //else
            //{
            //    //if (m_form.checkBox_xlsdir.Checked)
            //    {
            //        show_hide_doc();
            //    }
            //}

            srcpath_changed();
        }
        void use_doc_undersrc_select()
        {
            //var bOn = m_form.checkBox_specifydoc.Checked;
            //if (bOn)
            //{ 
            //    m_form.checkBox_xlsdir.Checked = false;

            //    m_form.textBox_xlsdir_path.Visible = true;
            //    m_form.button_xlsdir.Visible = false;
            //    m_form.textBox_xlsdir_path.ReadOnly = true;
            //    m_form.textBox_xlsdir_path.Text = Path.Combine(m_form.textBox_gendir_path.Text, "doc");

            //    m_form.textBox_xlsdir_eq_srcdir.Visible = false;
            //}
            //else
            //{
            //    m_form.textBox_xlsdir_path.Visible = false;
            //    m_form.button_xlsdir.Visible = false;
            //    m_form.textBox_xlsdir_eq_srcdir.Visible = true;
            //}
        }
        void show_hide_doc()
        {
            //var bOn = m_form.checkBox_xlsdir.Checked;
            //if (bOn)
            //{ 
            //    m_form.checkBox_specifydoc.Checked = false;
            //    m_form.textBox_xlsdir_path.Visible  = true;
            //    m_form.textBox_xlsdir_path.ReadOnly = false;
            //    m_form.button_xlsdir.Visible        = true;

            //    m_form.textBox_xlsdir_eq_srcdir.Visible = false;
            //}
            //else
            //{
            //    m_form.textBox_xlsdir_path.Visible = false;
            //    m_form.button_xlsdir.Visible = false;
            //    m_form.textBox_xlsdir_eq_srcdir.Visible = true;
            //}
        }
        void select_doc_usage()
        {
            var index = m_form.comboBox_docpath.SelectedIndex;
            if (index == 0)
            {
                m_form.textBox_xlsdir_path.Visible = true;
                m_form.button_xlsdir.Visible = false;
                m_form.textBox_xlsdir_eq_srcdir.Visible = false;
                m_form.textBox_xlsdir_path.ReadOnly = true;
                m_form.textBox_xlsdir_path.Text = m_form.textBox_gendir_path.Text;
            }
            else if (index == 1)
            {
                m_form.checkBox_xlsdir.Checked = false;

                m_form.textBox_xlsdir_path.Visible = true;
                m_form.button_xlsdir.Visible = false;
                m_form.textBox_xlsdir_path.ReadOnly = true;
                m_form.textBox_xlsdir_path.Text = Path.Combine(m_form.textBox_gendir_path.Text, "doc");

                m_form.textBox_xlsdir_eq_srcdir.Visible = false;
            }
            else {
                m_form.checkBox_specifydoc.Checked = false;
                m_form.textBox_xlsdir_path.Visible  = true;
                m_form.textBox_xlsdir_path.ReadOnly = false;
                m_form.button_xlsdir.Visible        = true;

                m_form.textBox_xlsdir_eq_srcdir.Visible = false;
            }
                
        }
        List<psgg_data> m_psgg_data_list;
        void draw_tree()
        {
            var basepath = m_form.label_starterkitdir_path.Text;

            var err = string.Empty;
            var psggfiles = PathUtil.CollectTraverse(basepath, "*.psgg",1000,out err);

            m_psgg_data_list = new List<psgg_data>();
            foreach(var f in psggfiles)
            {
                var pd = new psgg_data();
                pd.read(f);
                if (pd.m_settingini!=null)
                {
                    pd.m_index = m_psgg_data_list.Count;
                    m_psgg_data_list.Add(pd);
                }
            }

            var tv = m_form.treeView_starterkit;
            tv.Nodes.Clear();
            m_form.textBox_nothing.Visible = true;

            var bHasNode = false;
            try { 
                foreach(var pd in m_psgg_data_list)
                {
                    bHasNode = true;
                    var r = PathUtil.GetRelativePath(basepath,pd.m_filepath);
                    _create_nodes(tv,r,basepath,pd);
                }
                tv.ExpandAll();
            } catch //(SystemException e)
            {
                //
            }
            if (bHasNode)
            {
                m_form.textBox_nothing.Visible = false;
            }

        }
        void _create_nodes(TreeView tv, string r, string basepath, psgg_data pd)
        {
            var fullpath = Path.Combine(basepath,r);
            var rlist = r.Split('\\','/');
            var nodes = tv.Nodes;
            for(var s = 0; s<rlist.Length; s++)
            {
                var k = rlist[s];
                if (nodes.ContainsKey(k))
                {
                    // nothing to do
                }
                else
                {
                    if (s == rlist.Length -1)
                    {
                        var starterkit_name = m_lang_j_or_e ? pd.m_title_j : pd.m_title_e;// _get_starterkitname( fullpath  );
                        if (!string.IsNullOrEmpty(starterkit_name))
                        {
                            nodes.Add(k,starterkit_name);
                        }
                        else
                        {
                            nodes.Add(k,k);
                        }
                        nodes[k].Tag = pd.m_index;
                        pd.m_node = nodes[k]; //ノードを記録
                    }
                    else
                    { 
                        nodes.Add(k,k);
                        //nodes[k].Tag = k;
                    }
                }
                nodes = nodes[k].Nodes;
                continue;
            }
        }
        void set_latest_kit()
        {
            var kitpath = RegistryWork.Get_starterkit_path();
            var psggfile = Path.GetFileName(kitpath);

            var findindex = m_psgg_data_list.FindIndex(i=>i.m_filepath == kitpath);
            if (findindex >= 0)
            {
                m_pd = m_psgg_data_list[findindex];
                _show_starterkit_detail();

                m_form.treeView_starterkit.SelectedNode = m_pd.m_node;

                node_set_color();
            }

            m_form.treeView_starterkit.Focus();
        }
        psgg_data m_pd;
        void treeview_select()
        {
            var node = m_form.treeView_starterkit.SelectedNode;
            if (node == null || node.Tag == null) return;
            var pd_index = (int)node.Tag;
            if (pd_index < 0 || pd_index >= m_psgg_data_list.Count) return;

            m_pd =  m_psgg_data_list[pd_index];   // _treenode_fullpath(node);
            var file = m_pd.m_filepath;
            if (string.IsNullOrEmpty(file)) return;

            m_pd.read(file);

            _show_starterkit_detail();

            node_set_color();
        }
        void _show_starterkit_detail()
        {
            if (m_pd==null) return;

            m_form.textBox_selectstarterkit.Text = m_lang_j_or_e ? m_pd.m_title_j : m_pd.m_title_e;
            var desc = m_lang_j_or_e ? m_pd.m_detail_j : m_pd.m_detail_e;
            if (!string.IsNullOrEmpty(desc))
            {
                var statemachine = m_pd.get_statemachine_from_settingini();
                if (statemachine == null)
                {
                    MessageBox.Show("Please Check 'statemachine' name in settinig.ini sheet of PSGG. ");
                }
                else
                { 
                    desc = desc.Replace(statemachine, m_form.textBox_statemachine.Text);
                }
            }
            m_form.textBox_desc.Text = desc;

            m_form.checkBox_control_name.Enabled = !m_pd.m_special_condition_is_ue5_actor;

            _addTailControl();

        }
        void statemachine_namechanged()
        {
            if (m_form.checkBox_control_name.Checked)
            {
                _addTailControl();
            }
            var valid_header =@"[_a-zA-Z]";
            var valid_other = @"[_a-zA-Z0-9]";
            
            var name = m_form.textBox_statemachine.Text;
            if (name==null||name.Length == 0) return;

            var newname = string.Empty;
            for(var i = 0; i< name.Length; i++)
            {
                var c = name[i].ToString();
                if (i==0)
                {
                    if (!RegexUtil.IsMatch(valid_header, c))
                    {
                        continue;
                    }
                }
                else
                {
                    if (!RegexUtil.IsMatch(valid_other, c))
                    {
                        continue;
                    }
                }
                newname += c;
            }

            m_form.textBox_statemachine.Text = newname;
            _show_starterkit_detail();
        }

        private void _addTailControl()
        {
            var word = "Control";
            if (m_pd?.m_special_condition_is_ue5_actor ?? false)
            {
                word = "Actor";
            }

            if (!m_form.textBox_statemachine.Text.EndsWith(word))
            {
                m_form.textBox_statemachine.Text += word;
            }
        }

        void force_control_changed()
        {
            if (m_form.checkBox_control_name.Checked)
            {
                _addTailControl();
            }
        }
        void create_statemachine()
        {
            if (m_pd==null)
            {
                MessageBox.Show("Please Select Starter kit.");
                return;
            }
            if (string.IsNullOrEmpty( m_form.textBox_statemachine.Text))
            {
                MessageBox.Show("Please define the state-machine name.");
                return;
            }
            var statemachine = m_form.textBox_statemachine.Text;
            if (string.IsNullOrEmpty( m_form.textBox_gendir_path.Text))
            {
                MessageBox.Show("Please set the source folder");
                return;
            }
            var gendir = m_form.textBox_gendir_path.Text;
            var xlsdir = m_form.textBox_xlsdir_path.Text;
            //if (m_form.checkBox_specifydoc.Checked || m_form.checkBox_xlsdir.Checked)
            //{
            //    if (string.IsNullOrEmpty(m_form.textBox_xlsdir_path.Text))
            //    {
            //        MessageBox.Show("Please set the document folder");
            //        return;
            //    }
            //    xlsdir = m_form.textBox_xlsdir_path.Text;
            //}
            //生成
            var b = _create_new_files(
                m_pd,
                statemachine,
                gendir,
                xlsdir
                );

            if (!b) return;

            //記録
            RegistryWork.Set_templatedir(m_form.label_starterkitdir_path.Text);

            RegistryWork.Set_starterkit_path(m_pd.m_filepath);
            RegistryWork.Set_starterkit_root(m_form.label_starterkitdir_path.Text);

            RegistryWork.Set_statemchine(statemachine);
            RegistryWork.Set_gendir(gendir);
            RegistryWork.Set_xlsdir(xlsdir);

            // チェックボックス
            RegistryWork.Set_force_controlname(m_form.checkBox_control_name.Checked );
            RegistryWork.Set_docundersrc_xlsdir(m_form.checkBox_specifydoc.Checked);
            RegistryWork.Set_free_xlsdir(m_form.checkBox_xlsdir.Checked);

            m_form.Close();

            m_form.DialogResult = DialogResult.OK;
        }


        public bool HeadlessRun(string templatePsgg, string id, string outputDir, out string newPsggFile, out string newXlsxFile)
        {
            newPsggFile = null;
            newXlsxFile = null;

            var pd = new psgg_data();
            pd.read(templatePsgg);

            var sm = new NewWorkControl();
            sm.Headless = true;
            sm.m_pd = pd;
            // sm.m_bUE5Actor = pd.m_special_condition_is_ue5_actor; // This is done in _create_new_files

            // Determine starter kit root from templatePsgg
            // templatePsgg is like .../starterkit2/lang/template.psgg
            // Root should be .../starterkit2
            var starterKitPath = Path.GetDirectoryName(Path.GetDirectoryName(templatePsgg));

             // Reuse setting logic or duplicate
            var err = __create_new_files_setting_headless(sm, pd, id, outputDir, outputDir, starterKitPath);
            if (!string.IsNullOrEmpty(err)) {
                Console.WriteLine("Headless Error: " + err);
                return false;
            }

            sm.m_bUE5Actor = pd.m_special_condition_is_ue5_actor;
            sm.Run();

            if (sm.m_ok)
            {
                newPsggFile = sm.m_new_psgg;
                newXlsxFile = sm.m_new_excel;
                return true;
            }
            try { 
                File.WriteAllText(@"C:\Users\gea01\.gemini\antigravity\playground\infrared-einstein\headless_error.log", "Headless Run Failed: " + sm.m_err);
            } catch { } 
            return false;
        }

        private string __create_new_files_setting_headless(NewWorkControl sm, psgg_data m_pd, string statemachine, string gendir, string xlsdir, string starterKitPath)
        {
             // E_DEC_NAME
            sm.m_orgname = m_pd.get_statemachine_from_settingini();
            if (string.IsNullOrEmpty(sm.m_orgname)) { return "Not defined 'statemachine' in psgg file.";            }

            sm.m_newname = statemachine;
            if (string.IsNullOrEmpty(sm.m_newname)) { return "{0012AB81-917F-4A58-8EA4-C78153563745}";              }

            sm.m_newdocdir = xlsdir;
            if (string.IsNullOrEmpty(sm.m_newdocdir)) { return "{A3A3E84D-05E9-4603-A4B4-3F845ED1E1CB}"; }

            sm.m_newsrcdir = gendir;
            if (string.IsNullOrEmpty(sm.m_newsrcdir)) { return "{D70420E6-E6B7-4085-859D-E23494FF0765}"; }

            //E_DEC_ORGFILES
            sm.m_org_excel = null; //エクセルファイルは対象外

            sm.m_org_psgg  = m_pd.m_filepath;
            if (string.IsNullOrEmpty(sm.m_org_psgg) || !File.Exists(sm.m_org_psgg)) { return "{37F7384B-23B2-45B0-BC2B-AFB9F75C741E}"; }

            var helpwebpath = Path.Combine(Path.GetDirectoryName(m_pd.m_filepath), "helpweb.html");

            sm.m_org_helpweb = File.Exists(helpwebpath) ? helpwebpath : null ; //ヘルプウエブは対象外

            sm.m_org_genfile = m_pd.get_genfileFullpath_from_settingini();
            if (string.IsNullOrEmpty(sm.m_org_genfile) || !File.Exists(sm.m_org_genfile)) { return "{1B4182AD-A0F0-41E6-8AF3-D79AB7DE4B7E} " + sm.m_org_genfile; }

            sm.m_org_genhpp = m_pd.get_genhppfileFullpath_from_settingini() ;
            if (string.IsNullOrEmpty(sm.m_org_genhpp) || !File.Exists(sm.m_org_genhpp)) {
                sm.m_org_genhpp = null; 
            }

            sm.m_org_impfile = m_pd.get_impfileFullpath_from_settingini();
            if (!string.IsNullOrEmpty(sm.m_org_impfile) && !File.Exists(sm.m_org_impfile)) { return "{1D972EF8-D40D-436E-A853-DE050382E35A} " + sm.m_org_impfile; } //オプション。null時OK
            
            sm.m_org_macro = m_pd.get_macrofileFullpath_from_settingini();
            if (!string.IsNullOrEmpty(sm.m_org_macro) && !File.Exists(sm.m_org_macro)) { return "{EAF1B698-C414-491A-B1C2-E59CF8F00268} " + sm.m_org_macro; } //オプション。null時OK

            sm.m_starter_kit_path = starterKitPath;

            return null;
        }

        private bool _create_new_files(psgg_data m_pd, string statemachine, string gendir, string xlsdir)
        {
            var sm = new NewWorkControl();
            sm.m_pd = m_pd;
            var err = __create_new_files_settinig(sm, m_pd, statemachine, gendir, xlsdir);
            if (!string.IsNullOrEmpty(err)) {
                MessageBox.Show(err,"Error");
                return false;
            }

            sm.m_bUE5Actor = m_pd.m_special_condition_is_ue5_actor;

            sm.Run();

            var bOk = sm.m_ok;
            if (bOk)
            {
                m_form.m_target_psgg = sm.m_new_psgg;
                m_form.m_target_xlsx = sm.m_new_excel;
            }

            return bOk;
        }
        private string __create_new_files_settinig(NewWorkControl sm, psgg_data m_pd, string statemachine, string gendir, string xlsdir)
        {
            // E_DEC_NAME
            sm.m_orgname = m_pd.get_statemachine_from_settingini();
            if (string.IsNullOrEmpty(sm.m_orgname)) { return "Not defined 'statemachine' in psgg file.";            }

            sm.m_newname = statemachine;
            if (string.IsNullOrEmpty(sm.m_newname)) { return "{0012AB81-917F-4A58-8EA4-C78153563745}";              }

            sm.m_newdocdir = xlsdir;
            if (string.IsNullOrEmpty(sm.m_newdocdir)) { return "{A3A3E84D-05E9-4603-A4B4-3F845ED1E1CB}"; }

            sm.m_newsrcdir = gendir;
            if (string.IsNullOrEmpty(sm.m_newsrcdir)) { return "{D70420E6-E6B7-4085-859D-E23494FF0765}"; }

            //E_DEC_ORGFILES
            sm.m_org_excel = null; //エクセルファイルは対象外

            sm.m_org_psgg  = m_pd.m_filepath;
            if (string.IsNullOrEmpty(sm.m_org_psgg) || !File.Exists(sm.m_org_psgg)) { return "{37F7384B-23B2-45B0-BC2B-AFB9F75C741E}"; }

            var helpwebpath = Path.Combine(Path.GetDirectoryName(m_pd.m_filepath), "helpweb.html");

            sm.m_org_helpweb = File.Exists(helpwebpath) ? helpwebpath : null ; //ヘルプウエブは対象外

            sm.m_org_genfile = m_pd.get_genfileFullpath_from_settingini();
            if (string.IsNullOrEmpty(sm.m_org_genfile) || !File.Exists(sm.m_org_genfile)) { return "{1B4182AD-A0F0-41E6-8AF3-D79AB7DE4B7E} " + sm.m_org_genfile; }

            sm.m_org_genhpp = m_pd.get_genhppfileFullpath_from_settingini() ;
            if (string.IsNullOrEmpty(sm.m_org_genhpp) || !File.Exists(sm.m_org_genhpp)) {
                sm.m_org_genhpp = null; 
            }

            sm.m_org_impfile = m_pd.get_impfileFullpath_from_settingini();
            if (!string.IsNullOrEmpty(sm.m_org_impfile) && !File.Exists(sm.m_org_impfile)) { return "{1D972EF8-D40D-436E-A853-DE050382E35A} " + sm.m_org_impfile; } //オプション。null時OK
            
            sm.m_org_macro = m_pd.get_macrofileFullpath_from_settingini();
            if (!string.IsNullOrEmpty(sm.m_org_macro) && !File.Exists(sm.m_org_macro)) { return "{EAF1B698-C414-491A-B1C2-E59CF8F00268} " + sm.m_org_macro; } //オプション。null時OK

            sm.m_starter_kit_path = m_form.label_starterkitdir_path.Text;

            return null;
        }

        void reset_statemachine()
        {
            m_form.textBox_statemachine.Text = "HogeControl";
        }

        TreeNode m_colorednode;
        void node_set_color()
        {
            if (m_pd==null || m_pd.m_node==null) return;
            if (m_colorednode==m_pd.m_node)
            {
                return;
            }
            if (m_colorednode!=null)
            {
                m_colorednode.BackColor = Color.White;
            }
            m_pd.m_node.BackColor = Color.Yellow;
            m_colorednode = m_pd.m_node;
        }
        void node_reset_color()
        {
            m_colorednode = null;
        }
        void srcpath_changed()
        {
            var src = m_form.textBox_gendir_path.Text;
            //if (m_form.checkBox_specifydoc.Checked)
            //{
            //    m_form.textBox_xlsdir_path.Text = Path.Combine(src,"doc");
            //}
            //else if (m_form.checkBox_xlsdir.Checked==false)
            //{
            //    m_form.textBox_xlsdir_path.Text = src;
            //}

            if (m_form.comboBox_docpath.SelectedIndex == 0)
            {
                m_form.textBox_xlsdir_path.Text = src;
            }
            else if (m_form.comboBox_docpath.SelectedIndex == 1)
            {
                m_form.textBox_xlsdir_path.Text = Path.Combine(src,"doc");
            }


        }
        void go_previous_creator()
        {
            m_form.DialogResult = DialogResult.Retry;
            m_form.Close();
        }
        void form_closing()
        {
            if (m_form.DialogResult==DialogResult.None)
            { 
                m_form.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion
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

