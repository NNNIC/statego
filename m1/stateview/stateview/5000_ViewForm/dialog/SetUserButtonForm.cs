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

namespace stateview._5000_ViewForm.dialog
{
    public partial class SetUserButtonForm : Form
    {
        public SetUserButtonForm()
        {
            InitializeComponent();
        }

        private void SetUserButtonForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            this.DialogResult = DialogResult.None;
            textBoxTitle.Text = G.userbutton_title;
            textBoxCommand.Text = G.userbutton_command;
            checkBoxCallAfter.Checked = G.userbutton_callafterconvert;
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            var tmp =string.Empty;
            try {
                tmp = textBoxCommand.Text.Trim();
                if (string.IsNullOrEmpty(tmp))
                {
                    tmp = G.load_file_name;
                }

                var path = Path.GetDirectoryName(   Path.Combine(G.load_file_dir, tmp) );
                openFileDialog1.InitialDirectory = path;
            }
            catch (SystemException e2)
            {
                G.NoticeToUser_warning("Unexpected! {2B6C04D3-DB5E-49B5-85DC-A44CA2499C20}| " + e2.Message);
            }

            openFileDialog1.FileName = tmp;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var retivepath = PathUtil.GetRelativePath(G.load_file_dir, openFileDialog1.FileName);
                textBoxCommand.Text = retivepath;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            G.userbutton_title = textBoxTitle.Text;
            G.userbutton_command = textBoxCommand.Text;
            G.userbutton_callafterconvert = checkBoxCallAfter.Checked;
            G.view_form.UpdateUserButton();
            Close();
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","set-user-custom-button-dlg",Globals.system_lang=="jpn");
        }
    }
}
