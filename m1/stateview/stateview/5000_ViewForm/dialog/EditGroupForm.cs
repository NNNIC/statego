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
    public partial class EditGroupForm:Form
    {
        public string m_groupname;
        public string m_comment;

        public EditGroupForm()
        {
            InitializeComponent();
        }

        private void EditGroupForm_Load(object sender,EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(m_groupname)) throw new SystemException("Unexpected! {D5C68571-1D6B-42D3-A1B2-9389B428042E}");

            WordStorage.Res.ChangeAll(this,G.system_lang);

            this.Location = Cursor.Position;

            DialogResult = DialogResult.None;
            textBoxGroupName.Text = m_groupname;
            textBoxComment.Text   = m_comment;
        }

        private void buttonOK_Click(object sender,EventArgs e)
        {
            var gn = textBoxGroupName.Text.Trim();
            bool bOk=GroupNameUtil.IsOkNameForRename(gn,m_groupname);

            if (bOk==true)
            {
                DialogResult = DialogResult.OK;
                m_groupname = gn;
                m_comment   = textBoxComment.Text.Trim();
                Close();
            }
            else
            {
                G.NoticeToUser("Invalid Groupname!",Color.Red);
            }
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditGroupForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void label_help_notice_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","group-dlg",G.system_lang=="jpn");
        }
    }
}
