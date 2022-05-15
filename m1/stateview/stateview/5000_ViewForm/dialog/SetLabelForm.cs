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

namespace stateview._5000_ViewForm.dialog
{
    public partial class SetLabelForm : Form
    {
        public SetLabelForm()
        {
            InitializeComponent();
        }

        private void SetLabelForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            textBox1.Text = G.label_text;
            checkBoxShow.Checked = G.label_show;
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            G.label_text = textBox1.Text;
            G.label_show = checkBoxShow.Checked;

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","set-label-dlg",Globals.system_lang=="jpn");
        }
    }
}
