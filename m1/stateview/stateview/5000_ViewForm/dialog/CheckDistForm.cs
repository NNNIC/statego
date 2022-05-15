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
    public partial class CheckDistForm:Form
    {
        public CheckDistForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CheckDistForm_Load(object sender,EventArgs e)
        {
            DialogResult = DialogResult.None;
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CheckDistForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
