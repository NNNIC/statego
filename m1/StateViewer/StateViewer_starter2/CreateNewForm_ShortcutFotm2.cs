using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace StateViewer_starter2
{
    public partial class CreateNewForm_ShortcutFotm2 : Form
    {
        private Start2Form m_sf              { get { return Start2Form.m_form; } }
        private string m_syslang             { get { return m_sf.m_syslang;    } set { m_sf.m_syslang = value; } }

        public CreateNewForm_ShortcutFotm2()
        {
            InitializeComponent();
        }

        private void CreateNewForm_ShortcutFotm2_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;

            textBox_allinone.Visible      = false;
            textBox_srcwdoc.Visible       = false;
            pictureBox_allinone.Visible   = false;
            pictureBox_srcwithdoc.Visible = false;

            if (WORK.SrcDocFolderDefineType == SRC_DOC_FOLDER_DFEINE_TYPE.all_in_one_folder)
            {
                textBox_allinone.Visible      = true;
                pictureBox_allinone.Visible   = true;
            }
            else
            {
                textBox_srcwdoc.Visible       = true;
                pictureBox_srcwithdoc.Visible = true;
            }

            WordStorage.Res.ChangeAll(this,m_syslang);
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_g3clear_Click(object sender, EventArgs e)
        {
            textBoxSrcFolder.Text = string.Empty;
        }

        private void buttonOpenSrcFolderDialog_Click(object sender, EventArgs e)
        {
            var text = textBoxSrcFolder.Text;
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.Title = "Select Folder";
                if (!string.IsNullOrEmpty(text))
                { 
                    dlg.InitialDirectory = text;
                }
                else
                {
                    var xlsdir =  m_sf.get_xlsdir_candidate();
                    if (!string.IsNullOrEmpty(xlsdir))
                    {
                        dlg.InitialDirectory = xlsdir;
                    }
                }
                var result = dlg.ShowDialog();
                if (result== CommonFileDialogResult.Ok)
                {
                    textBoxSrcFolder.Text =  dlg.FileName;
                }
            }

        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSrcFolder.Text)) return;
            if (Directory.Exists(textBoxSrcFolder.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
