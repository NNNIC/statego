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
    public partial class OkForm:Form
    {
        public OkForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void OkForm_Load(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }

        private void OkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult== DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
