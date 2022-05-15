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


namespace stateview._5000_ViewForm.dialog_editbranch
{
    public partial class EditBranchForm:Form
    {
        public string m_state;

        private List<BranchUtil.APILABEL> m_org_list;
        public  List<BranchUtil.APILABEL> m_tmp_list;
        

        EditBranchControl m_ebc;

        public EditBranchForm()
        {
            InitializeComponent();
        }

        private void EditBranchForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this, G.system_lang);

            this.Location = Cursor.Position;

            this.dataGridView1.Font = new Font("MS Gothic",10);

            m_ebc = new EditBranchControl();
            m_ebc.m_form = this;
            m_ebc.Start();

            m_org_list = BranchUtil.GetApiAndLabelListFromState(m_state);
            m_tmp_list = BranchUtil.GetApiAndLabelListFromState(m_state); //clone

            var ms = ((ToolStripMenuItem)(MenuStrip_Blank.Items[0])).DropDown;

            MODE_APISTR = ms.Items[3].ToString(); //GUIで定義した値を取得する
            MODE_IFSTR = ms.Items[0].ToString();
            MODE_ELSEIFSTR = ms.Items[1].ToString();
            MODE_ELSESTR = ms.Items[2].ToString();

            Draw_data();

            if (!G.name_list.Contains(G.STATENAME_branchcmt))
            {
                this.dataGridView1.Columns[2].HeaderText = "na (Need 'branch-cmt')";
            }

            this.checkBox_automode.Checked = G.option_editbranch_automode;

            this.dataGridView1.Columns["Mode"].HeaderText =G.Localize("ebf_mode");
            this.dataGridView1.Columns["API"].HeaderText =G.Localize("ebf_cond");
            this.dataGridView1.Columns["Comment"].HeaderText =G.Localize("ebf_cmt");
        }

        public string MODE_APISTR;
        public string MODE_IFSTR;
        public string MODE_ELSEIFSTR;
        public string MODE_ELSESTR;

        public string GetModeString(BranchUtil.APIMODE mode)
        {
            switch (mode)
            {
                case BranchUtil.APIMODE.API:     return MODE_APISTR;
                case BranchUtil.APIMODE.IF:      return MODE_IFSTR;
                case BranchUtil.APIMODE.ELSEIF:  return MODE_ELSEIFSTR;
                case BranchUtil.APIMODE.ELSE:    return MODE_ELSESTR;
                default:                         return "none";
            }
        }
        public BranchUtil.APIMODE GetMode(string s)
        {
            if (s == MODE_APISTR)    return BranchUtil.APIMODE.API;
            if (s == MODE_IFSTR)     return BranchUtil.APIMODE.IF;
            if (s == MODE_ELSEIFSTR) return BranchUtil.APIMODE.ELSEIF;
            if (s == MODE_ELSESTR)   return BranchUtil.APIMODE.ELSE;

            return BranchUtil.APIMODE.NONE;
        }

        public void Draw_data()
        {
            dataGridView1.Rows.Clear();
            foreach(var p in m_tmp_list)
            {
                var modestr = GetModeString(p.mode);
                string valstr = BranchUtil.GetDisplayValue(p);
                valstr = string.IsNullOrEmpty(valstr) ? valstr : valstr.Replace(G.branch_special_newlinechar, "");
                string cmtstr = p.comment;
                dataGridView1.Rows.Add(modestr, valstr, cmtstr);
            }
        }

        private void dataGridView1_Click(object sender,EventArgs e)
        {
            m_ebc.m_bClicked = true;
        }
        private void EditBranchForm_Click(object sender,EventArgs e)
        {
            m_ebc.m_bClicked = true;
        }

        private void timer1_Tick(object sender,EventArgs e)
        {
            m_ebc.update();

            m_ebc.m_bClicked = false;
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NONE;
        }

        private void MenuItem_Edit_Click(object sender,EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.EDIT;
        }

        private void MenuItem_Up_Click(object sender,EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.UP;
        }

        private void MenuItem_Down_Click(object sender,EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.DOWN;
        }

        //private void MenuItem_New_Click(object sender,EventArgs e)
        //{
        //    m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NEW;
        //}

        DataGridViewCell savecell = null;
        private void dataGridView1_CellEndEdit(object sender,DataGridViewCellEventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.ENDEDIT;
            savecell = dataGridView1.CurrentCell;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (savecell!=null && savecell.RowIndex >=0 && savecell.ColumnIndex >= 0)
            {
                dataGridView1.CurrentCell =savecell;
                savecell = null;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender,EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.DELETE;
        }

        private void MenuItem_EditCmt_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.EDIT;

        }
        private void MenuItem_UpCmt_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.UP;
        }
        private void MenuItem_DownCmt_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.DOWN;
        }

        private void MenuItem_DeleteCmt_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.DELETE;
        }


        private void buttonOK_Click(object sender,EventArgs e)
        {
            BranchUtil.NormalizeLabelListForBranch(m_tmp_list);
            if (BranchUtil.UpdateBranchByApiAndLabelList(m_state,m_tmp_list))
            {
                G.req_redraw();
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                G.NoticeToUser_warning(G.Localize("w_nothingchanged")/*"Nothing changed!"*/);   
            }
        }

        private void buttonCancel_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonReset_Click(object sender,EventArgs e)
        {
            m_tmp_list = BranchUtil.GetApiAndLabelListFromState(m_state);
            Draw_data();
        }

        private void EditBranchForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void aPIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NEW_API;
        }

        private void iFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NEW_IF;
        }

        private void eLSEIFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NEW_ELSEIF;
        }

        private void eLSEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.NEW_ELSE;
        }

        private void iFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.IF;
        }

        private void eLSEIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.ELSEIF;
        }

        private void eLSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_ebc.m_menuevent = EditBranchControl.MENUEVENT.ELSE;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                e.Handled = false;
        }

        private void checkBox_automode_CheckedChanged(object sender, EventArgs e)
        {
            G.option_editbranch_automode = checkBox_automode.Checked;
        }

        private void checkBox_automode_Click(object sender, EventArgs e)
        {
        }

        private void label_help_notice_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","edit-branch-dlg",G.system_lang=="jpn");
        }
    }
}
