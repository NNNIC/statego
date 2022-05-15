using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using G = stateview.Globals;

namespace stateview._5000_ViewForm.dialog
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var s = textBox1.Text;
            if (string.IsNullOrEmpty(s)) return;
            var ext = Path.GetExtension(s);
            if (string.IsNullOrEmpty(ext))
            {
                s += ".psgg";
            }
            else if (ext.ToLower() != ".psgg")
            {
                s = s.Substring(0,s.Length - ext.Length) + ".psgg";
            }

            OpenLink.Open(s);
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","search-statego-dlg",Globals.system_lang=="jpn");
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender,null);
            }
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
        }
    }
}
