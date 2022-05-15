
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

namespace stateview._5300_EditForm
{
    public partial class EditForm_nextForm3:Form
    {
        public ViewFormStateControl.BranchInfo m_brinfo;
        public string m_text;
        public bool m_basestate_mode;
        public string m_mystate;
        public Form m_parent;
        public EditForm_nextForm3Control m_sm;

        public EditForm_nextForm3()
        {
            InitializeComponent();
        }

        private void EditForm_nextForm3_Load(object sender,EventArgs e)
        {
            m_sm = new EditForm_nextForm3Control();
            m_sm.m_form = this;
            m_sm.RunEvent_init();
            m_sm.RunEvent(EditForm_nextForm3Control.EVENT.onload);

            

            //WordStorage.Res.ChangeAll(this,G.system_lang);

            //this.DialogResult = DialogResult.None;
            //if (!FormUtil.SetCenterInForm(this,m_parent))//this.Location = Cursor.Position;
            //{
            //    this.Location = Cursor.Position;
            //}

            //this.dataGridView1.Rows.Clear();
            //foreach(var s in G.state_working_list)
            //{
            //    if (stateview.AltState.IsAltState(s)) continue;
            //    var path = G.node_get_dirpath(s);
            //    var cmt  = G.excel_program.GetString(s,G.STATENAME_statecmt);
            //    this.dataGridView1.Rows.Add(path,s,cmt);
            //}
            //this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //this.textBox1.Text = m_text;

            //m_center_focus = false;
        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            try {
                this.textBox1.Text = this.dataGridView1[1,e.RowIndex].Value.ToString();
            }catch { }
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            if (m_basestate_mode)
            {
                if (m_mystate == this.textBox1.Text)
                {
                    G.NoticeToUser_warning("Cannot set self state.");
                    return;
                }
            }
            m_text = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = string.Empty;
        }

        private void EditForm_nextForm3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
        }

        public bool m_center_focus;
        private void label_centerfocus_Click(object sender, EventArgs e)
        {
            G.vf_sc.m_center_focus_state = textBox1.Text;
            m_center_focus = true;
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void radioButtonNone_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNone.Checked)
            { 
                m_sm.RunEvent( EditForm_nextForm3Control.EVENT.rb_none);
            }
        }

        private void radioButtonGosub_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonGosub.Checked)
            {
                m_sm.RunEvent( EditForm_nextForm3Control.EVENT.rb_gosub);
            }
        }

        private void radioButtonSubStart_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSubStart.Checked)
            {
                m_sm.RunEvent( EditForm_nextForm3Control.EVENT.rb_substart);
            }
        }

        private void radioButtonSubReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSubReturn.Checked)
            {
                m_sm.RunEvent( EditForm_nextForm3Control.EVENT.rb_subreturn);
            }
        }

        private void radioButtonBaseState_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonBaseState.Checked)
            {
                m_sm.RunEvent( EditForm_nextForm3Control.EVENT.rb_basestate);
            }
        }

        private void focus_button_Click(object sender, EventArgs e)
        {
            G.vf_sc.m_center_focus_state = textBox1.Text;
            m_center_focus = true;
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
