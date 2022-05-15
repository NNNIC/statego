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
using System.Reflection;
using System.Net;

namespace stateview._5000_ViewForm.dialog
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
            Location = Cursor.Position;
        }

        private void labelWithConfirm_Click(object sender, EventArgs e)
        {
            G.option_convert_with_confirm = !G.option_convert_with_confirm;
            set_labelWithConfirm();
        }
        public void set_labelWithConfirm()
        {
            var id = G.option_convert_with_confirm ? "options_w_confirm"/*"with confirmation"*/ : "options_wo_confirm"/*"without confirmation"*/;
            labelWithConfirm.Text = G.Localize(id);
        }

        private void labelCpy2Clipboard_Click(object sender, EventArgs e)
        {
            G.option_copy_output_to_clipboard = !G.option_copy_output_to_clipboard;
            set_labelCpy2Clipboard();
        }
        public void set_labelCpy2Clipboard()
        {
            var id = G.option_copy_output_to_clipboard ? "options_enable_clipboard"/*"enable to use clipboard"*/ : "options_disable_clipboard"/*"desable to use clipboard"*/;
            labelCpy2Clipboard.Text = G.Localize(id);
        }

        private void label_ignorecase_Click(object sender, EventArgs e)
        {
            G.option_ignore_case_of_state = !G.option_ignore_case_of_state;
            set_labelIgnoreCase();
        }
        public void set_labelIgnoreCase()
        {
            var id = G.option_ignore_case_of_state ? "options_ignore_case"/*"ignore case of state"*/ : "options_not_ignore_case"/*"not ignore case of state"*/;
            label_ignorecase.Text = G.Localize(id);
        }

        private void label_savewith_excel_Click(object sender, EventArgs e)
        {
            G.psgg_header_info_save_mode_withexcel = !G.psgg_header_info_save_mode_withexcel;
            G.psgg_header_info_read_from_excel     =  G.psgg_header_info_save_mode_withexcel;  //連動しておく
            set_labelSaveWithExcel();    
        }
        public void set_labelSaveWithExcel()
        {
            if (G.psgg_file_w_data)
            {
                label_savewith_excel.Visible = true;
                var id = G.psgg_header_info_save_mode_withexcel ? "headerinfo_save_with_excel" : "headerinfo_save_psgg_only";
                label_savewith_excel.Text = G.Localize( id );
            }
            else
            {
                label_savewith_excel.Visible = false;
            }
        }

        private void label_custom_prefix_Click(object sender, EventArgs e)
        {
            G.option_use_custom_prefix = !G.option_use_custom_prefix;
            set_label_custom_prefix();
        }
        public void set_label_custom_prefix()
        {
            var id = G.option_use_custom_prefix ? "options_custom_prefix_yes" : "options_custom_prefix_no";
            label_custom_prefix.Text = G.Localize(id);
        }

        private void label_enable_mbr_Click(object sender, EventArgs e)
        {
            G.option_mrb_enable = !G.option_mrb_enable;
            stateview.RegistryWork.Set_item_mrb_enable(G.option_mrb_enable);
            set_label_enable_mbr();

        }
        public void set_label_enable_mbr()
        {
            var id = G.option_mrb_enable ? "options_mrb_enable_yes" : "options_mrb_enable_no";
            label_enable_mbr.Text = G.Localize(id);
        }

        private void label_historypanelonstart_Click(object sender, EventArgs e)
        {
            G.option_historypanel_onstart = !G.option_historypanel_onstart;
            stateview.RegistryWork.Set_item_historypanelonstart_enable(G.option_historypanel_onstart);
            set_label_historypanelonstart();
        }
        public void set_label_historypanelonstart()
        {
            var id = G.option_historypanel_onstart ? "options_historypanelonstart_yes" : "options_historypanelonstart_no";
            label_historypanelonstart.Text = G.Localize(id);
        }

        private void label_forceclose_ifviewchangeonly_Click(object sender, EventArgs e)
        {
            G.option_forceclose_ifviewchangeonly = !G.option_forceclose_ifviewchangeonly;
            stateview.RegistryWork.Set_item_forceclose_ifviewchangeonly(G.option_forceclose_ifviewchangeonly);
            set_label_forceclose_ifviewchangeonly();
        }
        public void set_label_forceclose_ifviewchangeonly()
        {
            var id = G.option_forceclose_ifviewchangeonly ? "options_forceclose_ifviewchangeonly_yes" : "options_forceclose_ifviewchangeonly_no";
            label_forceclose_ifviewchangeonly.Text = G.Localize(id);
        }

        private void label_setdefault_comment_Click(object sender, EventArgs e)
        {
            G.option_set_default_comment = !G.option_set_default_comment;
            set_setdefault_comment();
        }
        public void set_setdefault_comment()
        {
            var id = G.option_set_default_comment ? "options_setdefault_comment_yes":"options_setdefault_comment_no";
            label_set_default_comment.Text = G.Localize(id);
        }

        private void label_jump_if_statego_exist_Click(object sender, EventArgs e)
        {
            G.option_jump_if_statego_exist = !G.option_jump_if_statego_exist;
            set_jump_if_statego_exist();
            RegistryWork.Set_item_jump_if_statego_exist(G.option_jump_if_statego_exist);
        }
        public void set_jump_if_statego_exist()
        {
            var id = G.option_jump_if_statego_exist ? "options_jump_if_statego_exist_yes":"options_jump_if_statego_exist_no";
            label_jump_if_statego_exist.Text = G.Localize(id);
        }

        private void label_lexical_color_Click(object sender, EventArgs e)
        {
            G.option_lexical_color_onoff = !G.option_lexical_color_onoff;
            set_lexical_color_onoff();
            RegistryWork.Set_lexical_color_onoff(G.option_lexical_color_onoff);
            G.view_form.scintillaBoxTabFunc.RedrawWLexer();
        }
        public void set_lexical_color_onoff()
        {
            var id = G.option_lexical_color_onoff ? "options_lexical_color_on":"options_lexical_color_off";
            label_lexical_color.Text = G.Localize(id);
        }
    }
}
