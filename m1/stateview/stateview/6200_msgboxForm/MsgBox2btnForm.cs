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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>
using System.Reflection;

namespace stateview._6200_msgboxForm
{
    public partial class MsgBox2btnForm : Form
    {
        Action<DialogResult> m_cb;

        public MsgBox2btnForm()
        {
            InitializeComponent();
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
        }

        private void MsgBox2btnForm_Load(object sender, EventArgs e)
        {

        }

        private void MsgBox2btnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.Cancel;

            if (m_cb != null)
            {
                m_cb(DialogResult);
            }

            m_form = null;
        }

        static MsgBox2btnForm m_form;
        public static void Show(string text, string cap, string oktext, string canceltext, Action<DialogResult> cb)
        {
            if (m_form != null) throw new SystemException("{B7117046-3F2E-48EA-B3BF-77FB951DB0A8}");
            m_form = new MsgBox2btnForm();
            m_form.Text = cap;
            m_form.textBox_text.Text = text;
            m_form.button_ok.Text = oktext;
            m_form.button_cancel.Text = canceltext;
            m_form.m_cb = cb;
            m_form.Show(G.view_form);
        }
    }
}
