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
    public partial class ItemEditInputForm : Form
    {
        public ItemEditControl m_pm;
        int m_row { get { return m_pm.m_row; } }
        int m_col { get { return m_pm.m_col; } }

        string m_itemname {
            get { return m_pm.m_dg[m_pm.CC_NAME,m_row].Value?.ToString(); }
            set { m_pm.m_dg[m_pm.CC_NAME,m_row].Value =value; } 
        }
        string m_helptexte {
            get { return m_pm.m_dg[m_pm.CC_HELPEN,m_row].Value?.ToString(); }
            set { m_pm.m_dg[m_pm.CC_HELPEN,m_row].Value = value; }
        }
        string m_helptextj {
            get { return m_pm.m_dg[m_pm.CC_HELPJP,m_row].Value?.ToString(); }
            set { m_pm.m_dg[m_pm.CC_HELPJP,m_row].Value = value; }
        }
        string m_method {
            get { return m_pm.m_dg[m_pm.CC_METHOD, m_row].Value?.ToString(); }
            set { m_pm.m_dg[m_pm.CC_METHOD,m_row].Value = value; }
        }

        public ItemEditInputForm()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ItemEditInputForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void ItemEditInputForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            textBox_name.Text = m_itemname;
            textBox_helpen.Text = m_helptexte;
            textBox_helpjp.Text = m_helptextj;
            textBox_method.Text = m_method;

            label_error.Text = string.Empty;

            if (m_pm.m_info.is_readonly(m_itemname))
            {
                textBox_name.ReadOnly = true;
            }

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            var itemname = textBox_name.Text.Trim();
            var dg = m_pm.m_dg;
            var row = m_pm.m_row;

            //1.name は アルファベット 数字 - _ から成る？
            var regstr = @"[a-zA-Z0-9\.\-_]+";
            if (RegexUtil.Get1stMatch(regstr,itemname) != itemname)
            {
                label_error.Text = "error!\nthe item name has invalid chars.";
                return;
            }

            //2.重複?
            for(var r = 0; r< dg.Rows.Count; r++)
            {
                var n = dg[m_pm.CC_NAME,r].Value?.ToString();
                if (r != row && !string.IsNullOrEmpty(n) && n == itemname)
                {
                    label_error.Text = "error!\nthe item name has been already existed.";
                    return;
                }
            }
            
            //3. 反映

            m_itemname = itemname;
            m_helptexte = textBox_helpen.Text;
            m_helptextj = textBox_helpjp.Text;
            m_method = textBox_method.Text;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            var cnum = 9;
            cnum = ExcelColorPickForm.ShowDialog(8,this);
        }
    }
}
