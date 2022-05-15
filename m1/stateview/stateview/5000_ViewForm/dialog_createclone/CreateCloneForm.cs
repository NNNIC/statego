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

namespace stateview._5000_ViewForm.dialog_createclone
{
    public partial class CreateCloneForm : Form
    {
        public string NEWNAME_SIGN = "_NewName_";

        public CreateCloneForm()
        {
            InitializeComponent();
        }

        private void CreateCloneForm_Load(object sender, EventArgs e)
        {
            textBox_this.Text = G.load_file_name_woext;
            textBox_new.Text = NEWNAME_SIGN + G.load_file_name_woext;
            textBox_docdir.Text = Path.GetFullPath(G.load_file_dir);
            textBox_srcdir.Text = Path.GetFullPath(G.gen_dir);

            WordStorage.Res.ChangeAll(this,G.system_lang);
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            var sm = new CreateCloneControl();
            sm.m_form = this;
            sm.Run();
        }

        private void button_docdir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox_docdir.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_docdir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button_srcdir_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBox_srcdir.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_srcdir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","clone-dlg",Globals.system_lang=="jpn");
        }

        private void label_copyabove_Click(object sender, EventArgs e)
        {
            textBox_srcdir.Text = textBox_docdir.Text;
        }
    }
}
