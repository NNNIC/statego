using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using G=stateview.Globals;

namespace stateview._5000_ViewForm.dialog
{
    public partial class SetStarterKitRootForm : Form
    {
        public SetStarterKitRootForm()
        {
            InitializeComponent();
        }

        private void SetStarterKitRootForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
            textBox1.Text = RegistryWork.Get_starterkit_root();
        }

        private void button_folder_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.Title = "Select Folder";
                if (!string.IsNullOrEmpty(text))
                {
                    dlg.InitialDirectory = text;
                }
                var result = dlg.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    textBox1.Text = dlg.FileName;
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var cand = textBox1.Text;
            if (!string.IsNullOrEmpty(cand))
            {
                if (Directory.Exists(cand))
                {
                    RegistryWork.Set_starterkit_root(cand);
                    Close();
                }
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_Find_Click(object sender, EventArgs e)
        {
            var path = Path.Combine( PathUtil.GetThisAppPath() , @"..\starterkit2");
            if (Directory.Exists(path))
            {
                textBox1.Text = path;
                return;
            }
            path = PathUtil.ExtractPathWithEnvVals(@"%ProgramFiles(x86)%\PSGG\starterkit2");
            
            if (Directory.Exists(path))
            {
                textBox1.Text = path;
                return;
            }

            MessageBox.Show(G.Localize( "cc_canotfind") );
        }
    }
}
