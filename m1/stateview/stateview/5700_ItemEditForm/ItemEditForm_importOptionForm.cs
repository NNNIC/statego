using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G = stateview.Globals;
using stateview;


namespace stateview._5700_ItemEditForm
{
    public partial class ItemEditForm_importOptionForm : Form
    {
        public ItemEditForm_importOptionForm()
        {
            InitializeComponent();
        }

        public Point m_targetLocation;

        private void ItemEditForm_importOptionForm_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            Location = m_targetLocation;

            WordStorage.Res.ChangeAll(this,G.system_lang);
        }

        private void ItemEditForm_importOptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
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
    }
}
