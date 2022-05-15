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

namespace stateview
{
    public partial class ConfigForm:Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);


            //ReadExcelOptimize_checkBox.Checked = G.excel_read_optimized;

            bitmap_width_textBox.Text = G.bitmap_width.ToString();
            bitmap_height_textBox.Text = G.bitmap_height.ToString();

            //cb_statecmt.Checked = G.use_statecmt;
            //cb_thumbnail.Checked = G.use_thumbnail;
            //cb_contents.Checked = G.use_contents;

            //scaleTextBox.Text  = G.scale_percent.ToString();

            //ColorDefault_radioButton.Checked = !G.use_excel_color;
            //ColorExcel_radioButton.Checked   =  G.use_excel_color;

            cb_force_disp_out_pin.Checked = G.force_display_outpin;

            //MaxNumStates_textbox.Text = G.max_num_of_states.ToString();

            cb_save_by_uuid_order.Checked = G.save_by_uuid_order;
        }

        private void ConfigForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            //G.excel_read_optimized = ReadExcelOptimize_checkBox.Checked;

            //try {
            //    G.bitmap_width = int.Parse(bitmap_width_textBox.Text);
            //}
            //catch { }
            //try {
            //    G.bitmap_height = int.Parse(bitmap_height_textBox.Text);
            //}
            //catch { }

            //G.use_statecmt = cb_statecmt.Checked;
            //G.use_thumbnail = cb_thumbnail.Checked;
            //G.use_contents = cb_contents.Checked;

            //var scale = G.scale_percent;
            //if (double.TryParse(scaleTextBox.Text,out scale))
            //{
            //    if (scale > 0 && scale <= 400)
            //    {
            //        G.scale_percent = scale;
            //        try {
            //            G.main_picturebox.Width  = (int)((float)G.mainbitmap.Width * scale * 0.01f);
            //            G.main_picturebox.Height =  (int)((float)G.mainbitmap.Height * scale * 0.01f);

            //            G.main_picturebox.Refresh();
            //        }
            //        catch { }
            //    }
            //}

            //G.use_excel_color = ColorExcel_radioButton.Checked;

            //var maxstates = G.max_num_of_states;
            //if (int.TryParse(MaxNumStates_textbox.Text, out maxstates))
            //{
            //    G.max_num_of_states = maxstates;
            //}

            G.force_display_outpin = cb_force_disp_out_pin.Checked;

            G.save_by_uuid_order = cb_save_by_uuid_order.Checked;

            //SaveLoad.SaveTemp(); //コンフィグ記録
            //SaveLoadJson.SaveTempJson(); //コンフィグ記録
            SaveLoadIni.SaveTempIni();//コンフィグ記録
        }

        private void checkBox2_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void label4_Click(object sender,EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void checkBox5_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender,EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender,EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender,EventArgs e)
        {

        }

        //private void button1_Click(object sender,EventArgs e)
        //{
        //    var dg = new _6100_FillterForm.FillterForm();
        //    dg.ShowDialog();
        //}

        private void textBox1_TextChanged_2(object sender,EventArgs e)
        {

        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var cand_width  = 0;
            var cand_height = 0;
            try {
                cand_width = int.Parse(bitmap_width_textBox.Text);
            }
            catch { }
            try {
                cand_height = int.Parse(bitmap_height_textBox.Text);
            }
            catch {
                cand_width  = 0;
                cand_height = 0;
            }

            if (cand_height <= 0 || cand_width <= 0)
            {
                G.NoticeToUser_warning("{86412328-4010-48B8-9CE8-3B8351F0E768}");
                return;
            }
            if (cand_width != G.bitmap_width || cand_height!= G.bitmap_height)
            {
                G.bitmap_width =cand_width;
                G.bitmap_height = cand_height;


                SaveLoadIni.SaveTempIni();//コンフィグ記録
                G.view_form.SaveOnly();
                MessageBox.Show(G.Localize("cnff_affect_after_restart"));
                StartUtil.OpenNewOrLoad();
                Environment.Exit(0);
                Close();
            }
        }

        private void label_help_main_panel_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","config-dlg",G.system_lang=="jpn");
        }
    }
}
