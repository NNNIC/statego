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

public partial class EditForm_ClickControl  {

    void bmpdlg_open()
    {
        if (!G.psgg_file_w_data)
        {
            G.NoticeToUser_warning("Please upgrade files.");
            return;
        }

        var dlg = new stateview._5300_EditForm.EditForm_bmpForm();

        dlg.m_pform = m_parentForm;

        dlg.m_bmp       =  m_bmp;
        //dlg.m_changebmp = m_outbmp;

        dlg.ShowDialog(m_parentForm);

        //m_outbmp = dlg.m_changebmp;
        if (dlg.m_del==true)
        {
            m_modifiedBmp = true;
            m_bmp = null;
        }
        else if (dlg.m_changebmp!=null)
        {
            m_modifiedBmp = true;
            m_bmp = dlg.m_changebmp;
        }
    }

    void stdlg_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_stateForm();
        dlg.m_parent = m_parentForm;
        dlg.m_text = m_text;
        var help =string.Empty;
        //if (G.help_program!=null) { help = G.help_program.GetHelp(m_name); }
        if (string.IsNullOrEmpty(help)) { help = G.help_program2.GetHelp(m_name); }
        //dlg.helpTextBox.Text = help;
        dlg.m_help = help;
        dlg.m_comment = conver_text_from_excel(m_cmt);
        dlg.m_bRef = m_bRef;
        dlg.m_ref = get_1st_line(m_ref);

        dlg.ShowDialog(m_parentForm);
        if (dlg.DialogResult == DialogResult.OK)
        {
            if (m_text != dlg.m_text)
            {
                m_needUpdateDgv = true; //data grid view全更新
            }

            m_text = dlg.m_text;
            m_cmt  = convert_text_to_excel(dlg.m_comment);
            m_ref  = get_1st_line(dlg.m_ref);

        }
    }
    //void redraw_items()
    //{
    //    m_parentForm.ShowItems();
    //}
    void nstdlg_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_nextForm3();
        dlg.m_text = m_text;
        dlg.m_parent = m_parentForm;
        dlg.ShowDialog(m_parentForm);
        dlg.label_centerfocus.Visible = false;
        if (dlg.DialogResult == DialogResult.OK)
        {
            m_text = dlg.m_text;
        }
    }
    void bstdlg_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_nextForm3();
        dlg.m_parent = m_parentForm;
        dlg.Text = "Set base state";
        dlg.m_basestate_mode = true;
        dlg.m_mystate =m_state;
        dlg.m_text = m_text;
        dlg.label_centerfocus.Visible = false;
        dlg.ShowDialog(m_parentForm);
        if (dlg.DialogResult == DialogResult.OK)
        {
            m_text = dlg.m_text;
        }
    }
    void gssdlg_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_nextForm3();
        dlg.Text = "Set gosub state";
        dlg.m_basestate_mode = false;
        dlg.m_mystate =m_state;
        dlg.m_text = m_text;
        dlg.label_centerfocus.Visible = false;
        dlg.ShowDialog(m_parentForm);
        if (dlg.DialogResult == DialogResult.OK)
        {
            m_text = dlg.m_text;
        }
    }

    bool? m_changeDirection_or_text;
    void brdlg_select_open()
    {
        m_changeDirection_or_text = null;
        var dlg = new stateview._5300_EditForm.EditForm_branchSelectForm();
        dlg.ShowDialog(m_parentForm);
        m_changeDirection_or_text = dlg.m_changeDist_or_text;
    }

    void brdlg_goto_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_branchGotoForm();
        dlg.m_text = m_text;
        dlg.ShowDialog(m_parentForm);

        if (dlg.DialogResult == DialogResult.OK)
        {
            m_text = convert_text_to_excel( dlg.m_text);
        }
    }

    void txtdlg_open()
    {
        var dlg = new stateview._5300_EditForm.EditForm_textForm();
        dlg.m_parent = m_parentForm;
        dlg.m_text = conver_text_from_excel( m_text );

        dlg.m_method = G.itemsInfo_program.GetMethod(m_name);

        dlg.Text = "Edit " + m_name;

        var help =string.Empty;
        //if (G.help_program!=null) { help = G.help_program.GetHelp(m_name); }
        if (string.IsNullOrEmpty(help)) { help = G.help_program2.GetHelp(m_name); }
        //dlg.helpTextBox.Text = help;
        dlg.m_help = help;
        dlg.m_bEnableComment = m_bCmt;
        dlg.m_comment = conver_text_from_excel(m_cmt);

        dlg.m_bEnableRef = m_bRef;
        dlg.m_ref = get_1st_line(m_ref);

        dlg.m_state = m_state;
        dlg.m_item = m_name;

        dlg.ShowDialog(m_parentForm);

        if (dlg.DialogResult == DialogResult.OK)
        {
            m_text = convert_text_to_excel( dlg.m_text);
            m_cmt  = convert_text_to_excel( dlg.m_comment);
            m_ref  = get_1st_line(dlg.m_ref);
        }
    }

    bool wait_close()
    {
        return true;
    }
}
