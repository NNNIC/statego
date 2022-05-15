//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview._5300_EditForm._5360_useExtEditorControl
{
    public partial class ConfirmPickOutForm : Form
    {
        public string m_state;

        public ConfirmPickOutForm()
        {
            InitializeComponent();
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_Reject_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ConfirmPickOutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;
        }

        string m_orgtext = null;
        private void ConfirmPickOutForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
            m_orgtext = textBox1.Text;
        }

        private void button_state_converter_Click(object sender, EventArgs e)
        {
            var lowerCamel = StringUtil.convert_to_camel_word(m_state,false);
            var upperCamel = StringUtil.convert_to_camel_word(m_state,true);

            var newtext = textBox1.Text.Replace(m_state, "[[state]]");
            var newtext2 = newtext.Replace(lowerCamel, "[[state>>lc]]");
            var newtext3 = newtext2.Replace(upperCamel, "[[state>>uc]]");

            textBox1.Text = newtext3;
        }

        private void button_reset_Click(object sender, EventArgs e)
        {
            textBox1.Text = m_orgtext;
        }
    }
}
