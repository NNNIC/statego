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

namespace StateViewer_starter
{
    public partial class StartForm:Form
    {
        public static StartForm m_form;

        public string m_version;
        public string m_target_xlsx;

        

        public StartForm()
        {
            m_form = this;
            InitializeComponent();
        }

        private void StartForm_Load(object sender,EventArgs e)
        {
            labelVer.Text = m_version;

            DialogResult = DialogResult.None;
            m_target_xlsx = null;

            comboBoxHistory.Items.Clear();
            var historylist = Starter.GetHistory();
            if (historylist!=null)
            {
                foreach(var i in historylist)
                {
                    comboBoxHistory.Items.Add(i);
                    if (string.IsNullOrEmpty(comboBoxHistory.Text))
                    {
                        comboBoxHistory.Text = i;
                    }
                }
            }

            WORK.SetupRadioButtons();

            
        }

        private void buttonCreate_Click(object sender,EventArgs e)
        {
            try {
                WORK.CreateFiles();
            } catch (SystemException e2) {
                MessageBox.Show("Unexpected.\n" + e2.Message);
            }
        }

        private void buttonOpenExisting_Click(object sender,EventArgs e)
        {
            openFileDialog1.Filter = @"Excel Files(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
            //openFileDialog1.InitialDirectory = 
            {
                var historylist = Starter.GetHistory();
                if (historylist!=null && historylist.Length>0)
                {
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName( historylist[0] );
                }
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                m_target_xlsx = openFileDialog1.FileName;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonOpenInHistroy_Click(object sender,EventArgs e)
        {
            var file = comboBoxHistory.Text;
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                m_target_xlsx = file;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void listBox_title_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox_title.SelectedIndex;
            //textBoxDetail.Text = WORK.GetDetail(index);
            WORK.SetDetail(index);
        }

        private void comboBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = comboBoxHistory.SelectedIndex;
            if (index >= 0 && index < comboBoxHistory.Items.Count)
            {
                labelSelectOnHistory.Text = comboBoxHistory.Items[index].ToString();

                labelFileNameOnHistory.Text =  "";
                try {
                    var file = Path.GetFileName(labelSelectOnHistory.Text);
                    var path = Path.GetFileName( Path.GetDirectoryName(labelSelectOnHistory.Text) );
                    labelFileNameOnHistory.Text = path +"/" + file;
                } catch { }
            }
        }

        private void textBoxPrefix_TextChanged(object sender, EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        private void textBoxExcelFolder_TextChanged(object sender, EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        private void textBoxGenerateFolder_TextChanged(object sender, EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        private void buttonXlsFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir = get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxExcelFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonGenerateFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir = get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxGenerateFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private string get_xlsdir_candidate()
        {
            var historylist = Starter.GetHistory();
            if (historylist==null || historylist.Length ==0) return null;
            try {
                var path = historylist[0];
                var di = new DirectoryInfo( Path.GetDirectoryName(path));
                return di.Parent.FullName;
            }
            catch { }
            return null;

        }

        private void label1_Click(object sender,EventArgs e)
        {

        }

        private void labelSelectOnHistory_Click(object sender, EventArgs e)
        {

        }
    }
}
