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

namespace stateview._6990_aboutForm
{
    public partial class AboutForm:Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://nnnic.github.io");
        }

        private void button1_Click(object sender,EventArgs e)
        {
            Close();
        }

        private void AboutForm_Load(object sender,EventArgs e)
        {
            this.labelBuildTime.Text = G.buildtime;
            this.labelGitHash.Text   = G.githash;
            this.labelVersion.Text   = G.version;
            this.textBoxAddon.Text   = G.about_addon_text;
            this.textBoxAddon.BorderStyle  = BorderStyle.None;
            this.textBox_openfile.Text     = (G.psgg_file_w_data  ? G.psgg_file :  G.load_file) + " ver." + G.psgg_header_info_version;
            this.textBox_fileguid.Text     = G.guid;
            this.textBox_runtimeguid.Text  = G.userenv_guid;
            this.label_lang.Text = SettingIniUtil.GetLangFramework();

            if (Converter.psggConverterLib!=null)
            {
                try {
                    this.labelCvtVersion.Text   = Converter.psggConverterLib.VERSION();
                    this.labelCvtHash.Text      = Converter.psggConverterLib.GITHASH();
                    this.labelCvtBuildTime.Text = Converter.psggConverterLib.BUILDTIME();
                    this.labelCvtCopyright.Text = Converter.psggConverterLib.COPYRIGHT();
                    this.labelCvtDepot.Text     = Converter.psggConverterLib.DEPOT();
                } catch { }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox_openfile_DoubleClick(object sender, EventArgs e)
        {
            Clipboard.SetText(G.load_file);
            MessageBox.Show("Copy this file name to the clip board");
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel_privacypolicy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://nnnic.github.io/exepolicy/exepolicy.htm");
        }

        private void linkLabel_oss_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dlg = new stateview._6990_aboutForm.OssForm();
            dlg.Show();
        }
    }
}
