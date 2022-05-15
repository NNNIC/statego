using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stateview._5300_EditForm
{
    public partial class EditForm_textForm_selectForm : Form
    {
        public EditForm_textForm.methodproc m_mp;
        public Point m_startlocation;

        public EditForm_textForm_selectForm()
        {
            InitializeComponent();
        }

        private void EditForm_textForm_selectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void EditForm_textForm_selectForm_Load(object sender, EventArgs e)
        {
            Location = m_startlocation;
            DialogResult = DialogResult.None;

            dataGridView1.Rows.Add("0"," - empty -"); 
            for(var i = 0; i < m_mp.m_params.Count; i++)
            {
                var val = m_mp.m_params[i];
                if (!string.IsNullOrEmpty(val))
                { 
                    dataGridView1.Rows.Add( (i+1).ToString(), val);
                }
            }

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows!=null && this.dataGridView1.SelectedRows.Count>0)
            {
                var row = this.dataGridView1.SelectedRows[0];
                if (row == null) return;
                if (row.Index == 0)
                {
                    textBox1.Text = string.Empty;
                }
                else
                {
                    textBox1.Text = this.dataGridView1[1,row.Index].Value.ToString();
                }
            }
        }
    }
}
