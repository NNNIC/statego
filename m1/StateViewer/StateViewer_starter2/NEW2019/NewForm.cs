using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace StateViewer_starter2.NEW2019
{
    public partial class NewForm : Form
    {
        public string m_target_xlsx;
        public string m_target_psgg;

        NewControl m_nc;
        bool m_lang_j_or_e
        {
            get {
                return RegistryWork.Get_lang() == "jp";
            }
        }
        public NewForm()
        {
            InitializeComponent();
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            var lang = m_lang_j_or_e ? "jpn" : "en";

            WordStorage.Res.ChangeAll(this,lang);            

            comboBox_docpath.Items.Clear();
            comboBox_docpath.Items.Add( WordStorage.Res.Get("cns_xlsdir_eq_srcdir",lang)); 
            comboBox_docpath.Items.Add( WordStorage.Res.Get("cns_doc_under_src",lang)); 
            comboBox_docpath.Items.Add( WordStorage.Res.Get("cns_free_xls",lang)); 

            m_nc = new NewControl();
            m_nc.m_form = this;
            m_nc.Start();
            m_nc.Update();
            m_nc.RunForEvent(NewFormEvent.onload);
        }

        private void button_starterkitdir_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_starterkit_dir_open);
        }

        private void button_starterkit_reset_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_starterkit_dir_reset);
        }

        private void button_gendir_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_gendir_open);
        }

        private void button_xlsdir_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_docdir_open);
        }

        private void button_old_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_goold );
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.button_create);
        }

        private void checkBox_xlsdir_CheckedChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.checkbox_doc_changed);
        }

        private void treeView_starterkit_AfterSelect(object sender, TreeViewEventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.treeview_selected);
        }

        private void textBox_statemachine_TextChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.statemachine_name_changed);
            //if (checkBox1.Checked)
            //{
            //    if (!textBox_statemachine.Text.EndsWith("Control"))
            //    {
            //        textBox_statemachine.Text += "Control";
            //    }

            //}
        }

        private void checkBox_control_name_CheckedChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.statamachine_checkbox_control_checked);
        }

        private void checkBox_specifydoc_CheckedChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.checkbox_usedoc_changed);
        }

        private void comboBox_docpath_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.combobox_docpathusage_changed);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.statemachine_reset);
        }

        private void textBox_gendir_path_TextChanged(object sender, EventArgs e)
        {
            m_nc.RunForEvent(NewFormEvent.gendir_path_textchanged);
        }

        private void NewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_nc.RunForEvent(NewFormEvent.fromclosing);
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void label_help_win_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",m_lang_j_or_e);
        }

    }
}
