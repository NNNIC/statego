using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using G = stateview.Globals;
using stateview;

namespace stateview
{
    public partial class ItemEditForm : Form
    {
        public ItemEditForm()
        {
            InitializeComponent();
        }

        ItemEditControl m_sm;
        public bool     m_ask_reload;

        private void ItemEditForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            m_ask_reload = false;
            m_sm = new ItemEditControl();
            m_sm.m_form = this;
            m_sm.m_mode = ItemEditControl.Mode.onload;
            m_sm.Run();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_sm!=null && m_sm.m_mode == ItemEditControl.Mode.none)
            {
                m_sm.m_mode = ItemEditControl.Mode.tick;
                m_sm.Start();
            }

            if (m_sm!=null && m_sm.m_mode == ItemEditControl.Mode.tick)
            {
                m_sm.Update();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            m_sm.m_row = e.RowIndex;
            m_sm.m_col = e.ColumnIndex;

            m_sm.m_evt = "cellclick";
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_edit";            
        }

        private void contextMenuStrip_main_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            m_sm.m_evt = "msm_closing";
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_insert";            
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_remove";            
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_up";            
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_down";            
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_cancel";            
        }

        private void checkOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_checkon";            
        }

        private void checkOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_checkoff";            
        }

        private void condtionChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_condchg";            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_insert";            
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_remove";            
        }

        private void upToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_up";            
        }

        private void downToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_down";            
        }

        private void cancelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_cancel";            
        }

        private void conditionChange2ToollStripMenuItem2_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_condchg";            
        }

        private void cancel2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_cancel";            
        }

        private void checkonToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_checkon";            
        }

        private void checkoffToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_checkoff";            
        }

        private void cancelToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "msm_cancel";            
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            m_sm.m_evt = "clickok";
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","edit-item-define-dlg",Globals.system_lang=="jpn");
        }

        private void button_export_Click(object sender, EventArgs e)
        {
            var sm = new ItemList_IO_Control();
            sm.m_form = this;
            sm.m_iec = m_sm;
            sm.m_import_or_export = false;
            sm.Run();
        }

        private void button_import_Click(object sender, EventArgs e)
        {
            var form = new _5700_ItemEditForm.ItemEditForm_importOptionForm();
            form.m_targetLocation = PointUtil.Add_XY( this.Location, this.Width / 2, this.Height / 2);
            if (form.ShowDialog()== DialogResult.OK)
            { 
                var sm = new ItemList_IO_Control();
                sm.m_form = this;
                sm.m_iec = m_sm;
                sm.m_import_or_export = true;
                sm.m_overwrite = form.checkBox_overwrite.Checked;
                sm.Run();
            }
        }









        //}
    }
}
