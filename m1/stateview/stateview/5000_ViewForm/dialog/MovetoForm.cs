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
    public partial class MovetoForm:Form
    {
        public string m_pathdir;

        public MovetoForm()
        {
            InitializeComponent();
        }

        private void MovetoForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
            var dg = dataGridView1;
            dg.Columns["Path"].HeaderText = G.Localize("mvf_path"); 
            dg.Columns["Comment"].HeaderText = G.Localize("mvf_comment"); 

            DialogResult = DialogResult.None;

            textBox1.Text = m_pathdir;
            var pathlist = G.node_get_all_pathdir();
            foreach(var p in pathlist)
            {
                var cmt = G.node_get_comment_by_pathdir(p);
                dg.Rows.Add(p,cmt);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender,DataGridViewCellEventArgs e)
        {
            try {
                var dg = dataGridView1;
                var s = dg[0,e.RowIndex].Value.ToString();
                textBox1.Text = s;
            } catch { }
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            if (textBox1.Text == m_pathdir)
            {
                G.NoticeToUser_warning(G.Localize("w_specifiedgroupissame")/* "The specified group is same."*/);
                return;
            }
            m_pathdir = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
