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
    public partial class EditForm_branchGotoForm:Form
    {
        public string m_text;
        public BranchParse.Item[] m_items;
        public bool               m_modified;

        public EditForm_branchGotoForm()
        {
            InitializeComponent();
        }

        private void EditForm_branchGotoForm_Load(object sender,EventArgs e)
        {
            this.Location = Cursor.Position;
            m_items = BranchParse.Parse(m_text);

            if (m_items!=null)
            {
                for(var i = 0; i<m_items.Length;i++)
                {
                    this.dataGridView1.Rows.Add();
                    this.dataGridView1[0,i].Value = i.ToString();
                    this.dataGridView1[1,i].Value = m_items[i].api;
                    this.dataGridView1[2,i].Value = m_items[i].nextstate;
                }
            }
            m_modified = false;
        }

        private void dataGridView1_DoubleClick(object sender,EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            var row = e.RowIndex;
            var state = this.dataGridView1[2,row].Value.ToString();
            var dlg = new _5300_EditForm.EditForm_nextForm3();
            dlg.m_text = state;
            dlg.ShowDialog(this);
            if (dlg.DialogResult == DialogResult.OK)
            {
                m_modified = true;
                this.dataGridView1[2,row].Value = dlg.m_text;
            }
        }

        private void EditForm_branchGotoForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if (m_modified && DialogResult == DialogResult.OK)
            {
                m_text =  string.Empty;
                for(var i = 0; i <m_items.Length ; i++)
                {
                    var item = m_items[i];
                    item.value = BranchParse.Replace1stParameter(item.value,this.dataGridView1[2,i].Value.ToString());
                    if (!string.IsNullOrEmpty(m_text)) m_text += "\n";
                    m_text += item.value;   
                }
            }
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
