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
    public partial class UpgradePsggSubForm : Form
    {
        public UpgradePsggSubForm()
        {
            InitializeComponent();
        }

        private void UpgradePsggSubForm_Load(object sender, EventArgs e)
        {
            textBox1.SelectedText = "";
        }
    }
}
