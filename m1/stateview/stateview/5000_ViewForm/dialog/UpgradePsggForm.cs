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
    public partial class UpgradePsggForm : Form
    {
        public UpgradePsggForm()
        {
            InitializeComponent();
        }

        private void UpgradePsggForm_Load(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            WordStorage.Res.ChangeAll(this,G.system_lang);
            setup_checkbox();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UpgradePsggForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            { 
                DialogResult = DialogResult.Cancel;
            }
        }

        private void button_upgrade_Click(object sender, EventArgs e)
        {

            var msg = WordStorage.Res.Get("pfud_askbakup",G.system_lang);
            if (MessageBox.Show(msg,"Confirmation",MessageBoxButtons.YesNo)== DialogResult.No)
            {
                return;
            }

            if (radioButtonDelExcel.Checked)
            {
                G.psgg_header_info_read_from_excel         = false;
                //G.psgg_header_info_read_from_               = "psgg";

                this.checkBox_save_withexcel.Checked       = false;
                this.checkBox_check_excel_writable.Checked = false;
                this.checkBox_excelbackup.Checked          = false;

                if (!File.Exists(G.load_file))
                {
                    G.NoticeToUser_warning("The excel file does not exist.");
                    return;
                }

                if (FileUtil.IsReadOnly(G.load_file))
                {
                    G.NoticeToUser_warning("The excel file has readonly attribute. The file can not be deleted.");
                    MessageBox.Show(G.Localize("pvdc_readonly") );
                    return;
                }
            }
            else
            {
                //G.psgg_header_info_read_from               = "excel";
                G.psgg_header_info_read_from_excel = true;
            }

            G.psgg_header_info_file                 = G.load_file_name;
            G.psgg_header_info_guid                 = G.guid;
            G.psgg_header_info_save_mode_withexcel  = this.checkBox_save_withexcel.Checked;
            G.psgg_header_info_check_excel_writable = this.checkBox_check_excel_writable.Checked ? "yes" : "no";

            var sm = new psggVerUpdateControl();

            sm.m_parent     = this;
            sm.m_needbackup = true;//checkBox_excelbackup.Checked;

            sm.Run();

            DialogResult =  sm.m_success ?  DialogResult.OK : DialogResult.Cancel;

            if (sm.m_success)
            {
               if (radioButtonDelExcel.Checked)
                {
                    File.Delete(G.load_file);
                }
                Close();
            }
        }

        private void radioButtonUseExcel_CheckedChanged(object sender, EventArgs e)
        {
            setup_checkbox();
        }

        void setup_checkbox()
        {
            var b = radioButtonUseExcel.Checked;
            checkBox_excelbackup.Enabled = b;
            checkBox_save_withexcel.Enabled = b;
            checkBox_check_excel_writable.Enabled = b;
        }
    }
}
