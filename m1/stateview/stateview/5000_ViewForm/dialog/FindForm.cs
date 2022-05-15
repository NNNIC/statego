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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>

namespace stateview._5000_ViewForm.dialog
{
    public partial class FindForm : Form
    {
        FindFormControl m_sm;

        public FindForm()
        {
            InitializeComponent();
        }

        public void ChangeText()
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            ChangeText();

            m_sm = new FindFormControl();
            m_sm.m_form = this;
            m_sm.Start();

            // combox_text.Items の値を設定
            var s = RegistryWork.Get_findhistory();
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Trim();
                if (!string.IsNullOrEmpty(s))
                {
                    var lines = s.Split('\x0a');
                    combox_text.Items.Clear();
                    foreach(var i in lines)
                    {
                        var v = i.Trim();
                        if (string.IsNullOrEmpty(v)) continue;
                        combox_text.Items.Add(v);
                    }
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_sm.Update();
            
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            m_sm.event_add(m_sm.E_BTNFIND);
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var s = string.Empty;
            foreach(var i in combox_text.Items)
            {
                var v = i.ToString().Trim();
                if (string.IsNullOrEmpty(v)) continue;
                if (!string.IsNullOrEmpty(s)) s+="\x0a";
                s+=v;
            }
            RegistryWork.Set_findhistory(s);

            if (this.Visible)
            { 
                if (!G.ABORT)
                {
                    e.Cancel = true;
                    this.Visible = false;
                }
            }
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","find-dlg",Globals.system_lang=="jpn");
        }

        private bool m_bOnecFound = false;
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
                var state = dataGridView1[0,e.RowIndex].Value.ToString();
                if (!G.vf_sc.IsValidStateforCenterFocus()) {

                    label_result.Visible = false;
                    if (m_bOnecFound== false) //一度も実行されてない。リフレッシュを実行する。
                    {
                        Flow.main_skip_load_flow();
                        label_result.Visible = true;
                        label_result.Text = "Do it again after refreshing completed.";
                    }
                    return;
                } //アイドル中以外できない。
                G.vf_sc.m_center_focus_state = state;
                m_bOnecFound = true;
            } catch { }
        }

        private void combox_text_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    m_sm.event_add(m_sm.E_BTNFIND);
            //    e.IsInputKey = false;
            //    return;
            //}
            //if (_work_onoffkey(e.KeyCode)) { e.IsInputKey = false; return; }
        }

        private void FindForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (_work_onoffkey(e.KeyCode)) return;
        }
        bool _work_onoffkey(Keys key)
        {
            if (key == Keys.F && ((Control.ModifierKeys & Keys.Control) == Keys.Control))
            {
                G.find_form.Visible = false;
                //e.IsInputKey = false;
                return true;
            }
            return false;
        }

        private void FindForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (_work_onoffkey(e.KeyCode))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                m_sm.event_add(m_sm.E_BTNFIND);
                e.Handled = true;
                return;
            }
        }

        private void FindForm_Activated(object sender, EventArgs e)
        {
            combox_text.Focus();
        }
    }
}
