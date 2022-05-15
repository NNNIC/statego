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
    public partial class EditForm_branchSelectForm:Form
    {
        public bool? m_changeDist_or_text = null;

        public EditForm_branchSelectForm()
        {
            InitializeComponent();
        }

        private void EditForm_branchSelectForm_Load(object sender,EventArgs e)
        {
            this.Location = Cursor.Position;
        }

        private void button1_Click(object sender,EventArgs e)
        {
            m_changeDist_or_text = true;
            Close();
        }

        private void button2_Click(object sender,EventArgs e)
        {
            m_changeDist_or_text = false;
            Close();
        }
    }
}
