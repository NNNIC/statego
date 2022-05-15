
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

using System.Diagnostics;

namespace stateview._5000_ViewForm.dialog
{
    public partial class ExternalCommendForm : Form
    {
        public ExternalCommendForm()
        {
            InitializeComponent();
        }

        //private void buttonClose_Click(object sender, EventArgs e)
        //{
        //    G.external_command = textBox1.Text;0
        //    Close();
        //}

        private void buttonExec_Click(object sender, EventArgs e)
        {
            //var p = new Process();
            //p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            //p.StartInfo.UseShellExecute = true;
            //p.StartInfo.WorkingDirectory =Path.GetDirectoryName(G.load_file);
            //p.StartInfo.Arguments = @"/c " + textBox1.Text;
            //p.Start();
            ExecUtil.execute(textBox1.Text,Path.GetDirectoryName(G.load_file));
        }

        private void ExternalCommendForm_Load(object sender, EventArgs e)
        {
            Location = Cursor.Position;
            textBox1.Text = G.external_command;

            //if (!G.use_external_command)
            //{
            //    buttonSaveClose.Enabled = false;
            //}
            //else
            //{
            //    checkBoxUseExternal.Checked = true;
            //}
            checkBoxUseExternal.Checked = G.use_external_command;
            enableInputCommand(checkBoxUseExternal.Checked);
        }

        private void ExternalCommendForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void checkBoxUseExternal_CheckedChanged(object sender,EventArgs e)
        {
            var b = checkBoxUseExternal.Checked;
            enableInputCommand(b);
        }

        private void enableInputCommand(bool b)
        {
            buttonExec.Enabled = b;
            textBox1.Enabled = b;
        }

        private void buttonSaveClose_Click(object sender,EventArgs e)
        {
            G.use_external_command = checkBoxUseExternal.Checked;
            if (G.use_external_command)
            {
                G.external_command = textBox1.Text;
            }
            Close();
        }
    }
}
