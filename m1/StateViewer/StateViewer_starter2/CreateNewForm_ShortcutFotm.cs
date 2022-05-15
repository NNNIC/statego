using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateViewer_starter2
{
    public partial class CreateNewForm_ShortcutFotm : Form
    {
        private Start2Form m_sf              { get { return Start2Form.m_form; } }
        private string m_syslang             { get { return m_sf.m_syslang;    } set { m_sf.m_syslang = value; } }

        public CreateNewForm_ShortcutFotm()
        {
            InitializeComponent();
        }

        private void button_all_in_one_Click(object sender, EventArgs e)
        {
            WORK.SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.all_in_one_folder;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_src_with_doc_Click(object sender, EventArgs e)
        {
            WORK.SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.src_with_doc_folder;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CreateNewForm_ShortcutFotm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CreateNewForm_ShortcutFotm_Load(object sender, EventArgs e)
        {
            WORK.SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.none;
            DialogResult = DialogResult.None;

            WordStorage.Res.ChangeAll(this,m_syslang);
        }

        private bool OpenSetFolderDlg()
        {
            var dlg = new CreateNewForm_ShortcutFotm2();
            var dr = dlg.ShowDialog(this);
            if (dr== DialogResult.OK)
            {
                return true;
            }
            return false;
        }

        private void label_setdetail_Click(object sender, EventArgs e)
        {
            WORK.SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.none;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            WORK.SrcDocFolderDefineType = SRC_DOC_FOLDER_DFEINE_TYPE.none;
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
