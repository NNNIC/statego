using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G=stateview.Globals;

namespace stateview
{
    public partial class ItemEditNewForm : Form
    {
        public ItemEditControl m_pm;

        public ItemEditNewForm()
        {
            InitializeComponent();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            var itemname = textBox_name.Text.Trim();
            var dg = m_pm.m_dg;
            var row = m_pm.m_row;

            //1.name は アルファベット 数字 - _ から成る？
            var regstr = @"[a-zA-Z0-9\.\-_]+";
            if (RegexUtil.Get1stMatch(regstr,itemname) != itemname)
            {
                label_error.Text = "error!\nitem name has invalid chars.";
                return;
            }
            //2.重複?
            for (var r = 0; r < dg.Rows.Count; r++)
            {
                var n = dg[m_pm.CC_NAME, r].Value?.ToString();
                if (!string.IsNullOrEmpty(n) && n == itemname)
                {
                    label_error.Text = "error!\nitem name has been already existed.";
                    return;
                }
            }
            //3. 挿入
            m_pm.insert_item_to_dg(itemname,row, textBox_helpen.Text,textBox_helpjp.Text, textBox_method.Text);

            DialogResult = DialogResult.OK;

            Close();
        }

        private void ItemEditNewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
        }

        private void ItemEditNewForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
            label_error.Text = string.Empty;
        }
    }
}
