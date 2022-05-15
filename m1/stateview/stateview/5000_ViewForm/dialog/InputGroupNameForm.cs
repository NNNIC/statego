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
    public partial class InputGroupNameForm:Form
    {
        public string m_groupname = "hoge";
        public List<string> m_groupname_list;
        public string m_comment;

        public InputGroupNameForm()
        {
            InitializeComponent();
        }

        private void InputGroupNameForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            DialogResult = DialogResult.None;

            if (m_groupname_list!=null)
            {
                groupname_comboBox.Items.AddRange(m_groupname_list.ToArray());
            }

            groupname_comboBox.Text = m_groupname;
            textBoxComment.Text = m_comment;
        }

        private void button1_Click(object sender,EventArgs e)
        {
            var gn  = groupname_comboBox.Text;

            bool bOk=GroupNameUtil.IsOkNameForNewOrAdd(gn);

            if (bOk)
            {
                DialogResult = DialogResult.OK;
                m_groupname = gn;
                m_comment = textBoxComment.Text;
                Close();
            }
            else
            {
                if (string.IsNullOrEmpty(gn)) gn = "(null)";
                G.NoticeToUser_warning( G.Localize("w_errorgroupname") /* "Error groupname : " */+ gn);
            }
        }

        private void button2_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void groupname_comboBox_SelectedIndexChanged(object sender,EventArgs e)
        {
            var comment = G.node_get_comment_by_groupname(groupname_comboBox.Text);
            textBoxComment.Text = comment;
        }

        private void groupname_comboBox_SelectedValueChanged(object sender,EventArgs e)
        {
            var comment = G.node_get_comment_by_groupname(groupname_comboBox.Text);
            textBoxComment.Text = comment;
        }

        private void label_help_notice_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","group-dlg",G.system_lang=="jpn");
        }
    }
}
