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
    public partial class SetSourceEditorFormcs:Form
    {
        string historyfile { get {
                return Path.Combine( Path.GetTempPath(), "psgg-sourceeditor-history.txt");
            } }

        public SetSourceEditorFormcs()
        {
            InitializeComponent();
        }

        private void SetSourceEditorFormcs_Load(object sender,EventArgs e)
        {
            var lang = SettingIniUtil.GetLang();
            var fw = SettingIniUtil.GetFramework();
            var text = lang;
            if (!string.IsNullOrEmpty(fw)) text += "(" + fw +")";

            label_lang.Text = text;


            textBox1.Text = G.external_source_editor;
            checkBox_jumpforVS2015.Checked = G.source_editor_vs2015_support;
            
            history_setup(); 

            WordStorage.Res.ChangeAll(this,G.system_lang);
        }
        void history_setup()
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
            
            listBox1.Items.Clear();
            list.ForEach(i => listBox1.Items.Add(i));
        }
        void history_record(string i_s)
        {
            var s = i_s.Trim();

            var list = new List<string>();
            for(var i = 0; i< listBox1.Items.Count; i++)
            {
                list.Add(listBox1.Items[i].ToString().Trim());
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


        //private void buttonExec_Click(object sender,EventArgs e)
        //{
        //    var file = TemplateUtil.GetCreatedFile();
        //    if (file!=null)
        //    {
        //        ExecUtil.execute_start2(string.Format("\"{0}\" \"{1}\"",textBox1.Text, file),G.load_file_dir);
        //    }
        //}

        private void buttonOK_Click(object sender,EventArgs e)
        {
            G.external_source_editor = textBox1.Text.Trim();
            G.source_editor_vs2015_support = checkBox_jumpforVS2015.Checked;
            history_record(G.external_source_editor);

            var option = (G.source_editor_vs2015_support ? WordStorage.Store.srceditcmd_option_vs2015 :"");

            RegistryWork.Set_srceditcmd(G.external_source_editor, SettingIniUtil.GetLangFramrwork_registName(), option);
            
            Close();
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void textBox2_TextChanged(object sender,EventArgs e)
        {
            
        }

        private void textBox2_DoubleClick(object sender,EventArgs e)
        {
            textBox1.Text = textBox2.Text;
        }

        private void textBox3_DoubleClick(object sender,EventArgs e)
        {
            textBox1.Text = textBox3.Text;
        }

        private void listBox1_DoubleClick(object sender,EventArgs e)
        {
            var i = listBox1.SelectedIndex;
            if (i>=0 && i < listBox1.Items.Count)
            {
                textBox1.Text = listBox1.Items[i].ToString();
            }
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","set-source-editor-dlg",Globals.system_lang=="jpn");
        }
    }
}
