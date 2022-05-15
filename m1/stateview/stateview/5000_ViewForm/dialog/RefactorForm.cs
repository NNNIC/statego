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
    public partial class RefactorForm:Form
    {
        private List<string> m_all_states { get {  return G.state_working_list; } }

        public string m_name;
        public string m_comment;
        public string m_branch;

        public RefactorForm()
        {
            InitializeComponent();
        }

        private void RefactorForm_Load(object sender,EventArgs e)
        {
            DialogResult = DialogResult.None;
            textBoxName.Text    = m_name;
            textBoxComment.Text = m_comment;
            textBoxBranch.Text  = m_branch;
        }

        private void buttonOK_Click(object sender,EventArgs e)
        {
            var newname = textBoxName.Text.Trim();
            if (string.IsNullOrEmpty(newname))
            {
                G.NoticeToUser_warning(G.Localize("w_theinputnameisnothing") /* "The input name is nothing."*/);
                return;
            }
            if (!StateUtil.IsValidStateName(newname))
            {
                G.NoticeToUser_warning(G.Localize("w_theinputnameisinvalid")/* "The input name is invalid."*/);
                return;
            }
            //if (newname == m_name)
            //{
            //    G.NoticeToUser_warning("The input name is same.");
            //    return;
            //}
            if (newname!=m_name && m_all_states.Contains(newname))
            {
                G.NoticeToUser_warning(G.Localize("w_theinputnameexisted") /* "The input name has already existed." */);
                return;
            }
            m_name    = newname;
            m_comment = textBoxComment.Text;
            m_branch  = textBoxBranch.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void RefactorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
