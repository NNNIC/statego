using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stateview._5000_ViewForm.dialog
{
    public partial class OKCancelForm:Form
    {
        public OKCancelForm()
        {
            InitializeComponent();
        }

        private void OKCancelForm_Load(object sender,EventArgs e)
        {
            DialogResult = DialogResult.None;
        }

        private void button1_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKCancelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
