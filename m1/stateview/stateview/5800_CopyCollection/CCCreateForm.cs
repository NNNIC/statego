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
using G=stateview.Globals;

namespace stateview._5800_CopyCollection
{
    public partial class CCCreateForm : Form
    {
        public CCCreateForm()
        {
            InitializeComponent();
        }

        private void CCCreateForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this, G.system_lang);

            var psggfile = Path.GetFullPath(G.psgg_file);
            var path = Path.GetDirectoryName(psggfile);

            while(!string.IsNullOrEmpty(path))
            {
                comboBox1.Items.Add(path);
                var newpath = Path.GetFullPath(path + @"\..");
                if (newpath == path)
                {
                    break;
                }
                path = newpath;
            }
            comboBox1.TextChanged += ComboBox1_TextChanged;
            comboBox1.Text = comboBox1.Items[0].ToString();

            textBox1.Select(0,0);

            comboBox1.Select();

            DialogResult = DialogResult.None;

        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            textBox_path.Text = comboBox1.Text;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            var targetpath = comboBox1.Text;
            if (!Directory.Exists(targetpath))
            {
                throw new Exception("{EBCA7F7B-B917-48ED-9914-F92B5283DA52}");
            }


            var referpath = string.Empty;
            if (!string.IsNullOrEmpty(G.cc.m_copycollectionFolder_fullpath) && Directory.Exists(G.cc.m_copycollectionFolder_fullpath))
            {
                if (G.cc.m_read_only== false)
                {
                    MessageBox.Show("作業フォルダは既に存在します");
                    return;
                }
                else
                {
                    referpath = G.cc.m_copycollectionFolder_fullpath;
                }
            }

            if (string.IsNullOrEmpty(referpath))
            {
                G.cc.create_workfolder(targetpath);
                G.cc.create_collection_sample_folder("Sample1");
                G.cc.create_collection_sample_folder("Sample2");
                G.cc.create_collection_sample_folder("Sample3");
            }
            else
            {
                G.cc.copy_workfolder_from_kit(referpath,targetpath);
            }

            MessageBox.Show("作業フォルダを以下の通り作成しました\n" + G.cc.m_copycollectionFolder_fullpath);

            DialogResult = DialogResult.OK;

            Close();
            

        }
    }
}
