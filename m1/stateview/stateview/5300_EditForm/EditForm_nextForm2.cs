
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
    [Obsolete]
    public partial class EditForm_nextForm2:Form
    {
        public string m_text;

        public EditForm_nextForm2()
        {
            InitializeComponent();
        }

        private void EditForm_nextForm2_Load(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            this.Location = Cursor.Position;

            this.dataGridView1.Rows.Clear();
            foreach(var s in G.state_working_list)
            {
                var cmt = G.excel_program.GetString(s,G.STATENAME_statecmt);
                this.dataGridView1.Rows.Add(s,cmt);
            }
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.textBox1.Text = m_text;
        }

        private void dataGridView1_CellClick(object sender,DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            try {
                this.textBox1.Text = this.dataGridView1[0,e.RowIndex].Value.ToString();
            }catch { }
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            m_text = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click_1(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
