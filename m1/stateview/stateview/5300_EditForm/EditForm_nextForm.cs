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

namespace stateview._5300_EditForm
{
    public partial class EditForm_nextForm:Form
    {
        public string m_text;

        public EditForm_nextForm()
        {
            InitializeComponent();
        }

        private void EditForm_nextForm_Load(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            this.Location = Cursor.Position;

            var list = new List<string>( G.state_working_list);
            list.Sort();
            this.listBox1.Items.AddRange(list.ToArray());

            this.textBox1.Text = m_text;

        }

        private void listBox1_DoubleClick(object sender,EventArgs e)
        {
            var s = this.listBox1.Text;
            this.textBox1.Text = s;
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            m_text = this.textBox1.Text;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
