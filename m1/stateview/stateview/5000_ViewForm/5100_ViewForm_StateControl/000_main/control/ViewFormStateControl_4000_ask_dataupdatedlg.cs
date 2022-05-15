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

using stateview;

// 指定ステートの中央表示＆フォーカス
public partial class ViewFormStateControl {

    private void br_isAskDataUpgrade(Action<int,bool> state) {
        if (HasNextState()) return;
        if (G.psgg_ask_upgrade==true)
        {
            SetNextState(state);
        }
    }


    stateview._5000_ViewForm.dialog.OKCancelForm m_askup_dlg;
    private void ask_update_dlg_start()
    {
        m_askup_dlg = new stateview._5000_ViewForm.dialog.OKCancelForm();
        m_askup_dlg.textBox1.Text = WordStorage.Res.Get("pvdc_ask",G.system_lang);//"データバージョンが１．０です。\nアップグレードしますか？";
        m_askup_dlg.Location = PointUtil.Add_XY(G.view_form.Location, G.view_form.Width / 2, G.view_form.Height / 2 - m_askup_dlg.Height );
        m_askup_dlg.Show(G.view_form);
    }
    private bool ask_update_dlg_done()
    {
        if (m_askup_dlg.DialogResult == DialogResult.None) return false;
        G.psgg_ask_upgrade = false;
        G.psgg_open_upgrade = m_askup_dlg.DialogResult == DialogResult.OK;
        return true;
    }

    void br_isOpenDataUpgrade(Action<int,bool> state)
    {
        if (HasNextState()) return;
        if (G.psgg_open_upgrade==true)
        {
            SetNextState(state);
        }
    }

    stateview._5000_ViewForm.dialog.UpgradePsggForm m_upgrade_dlg;
    void upgrade_dlg_start()
    {
        m_upgrade_dlg = new stateview._5000_ViewForm.dialog.UpgradePsggForm();
        m_upgrade_dlg.Show(G.view_form);
    }
    bool upgrade_dlg_done()
    {
        if ( m_upgrade_dlg.DialogResult == DialogResult.None ) return false;
        m_okCancel = m_upgrade_dlg.DialogResult == DialogResult.OK;
        return true;
    }

    stateview._5000_ViewForm.dialog.OkForm m_restart_msg_dlg;
    void restart_msg_start()
    {
        m_restart_msg_dlg = new stateview._5000_ViewForm.dialog.OkForm();
        m_restart_msg_dlg.textBox1.Text = WordStorage.Res.Get("pvdc_restart",G.system_lang);
        m_restart_msg_dlg.Show(G.view_form);
    }
    bool restart_msg_done()
    {
        if (m_restart_msg_dlg.DialogResult == DialogResult.None) return false;
        return true;
    }
    void restart()
    {
        StartUtil.OpenNewOrLoad();

        Environment.Exit(0);
    }
}   