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
    public partial class MsgBoxForm : Form
    {
        Action m_cb;

        public MsgBoxForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void MsgBoxForm_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
        }

        private void MsgBoxForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None) DialogResult = DialogResult.OK;
            if (m_cb != null) m_cb();
            m_form = null;
        }

        static MsgBoxForm m_form;
        public static void Show(string text, string cap, string oktext, Action cb)
        {
            if (m_form != null) throw new SystemException("{B7117046-3F2E-48EA-B3BF-77FB951DB0A8}");
            m_form = new MsgBoxForm();
            m_form.Text = cap;
            m_form.textBox1.Text = text;
            m_form.button1.Text = oktext;
            m_form.m_cb = cb;
            m_form.Show(G.view_form);
        }

    }
}
