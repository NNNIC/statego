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
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview._5800_CopyCollection
{
    public partial class CCForm2PageForm : Form
    {
        public int NUM_COL   = -1;
        public int ORDER_COL = -1;
        public int UUID_COL  = -1;
        public int DATE_COL  = -1;
        public int TITLE_COL = -1;
        public int CMT_COL   = -1; 

        public CCForm2PageForm()
        {
            InitializeComponent();
        }

        CCForm2PageFormControl m_cc2c;

        DataGridView m_dg { get {  return dataGridView1; } }
        List<CopyCollection.WorkItem> m_page_items;
        public int                    m_cur_page_index = 0;
        private void CCForm2PageForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            m_cc2c = new CCForm2PageFormControl();
            m_cc2c.m_form = this;
            m_cc2c.Start();

            Location = Cursor.Position;

            setup_name_col();

            m_cur_page_index = 0;

            loaddata();
        }
        public void loaddata()
        {
            G.cc.read_work_date();
            m_page_items = G.cc.GetCollectionRootItems();
            setup_dg();
        }
        void setup_name_col()
        {
            NUM_COL   = DataGridViewUtil.GetColumnIndex(dataGridView1,"DisplayNum");
            ORDER_COL = DataGridViewUtil.GetColumnIndex(dataGridView1,"ListOrder");
            
            UUID_COL  = DataGridViewUtil.GetColumnIndex(dataGridView1,"UUID");
            DATE_COL  = DataGridViewUtil.GetColumnIndex(dataGridView1,"LastUpdate");
            TITLE_COL = DataGridViewUtil.GetColumnIndex(dataGridView1,"Title");
            CMT_COL   = DataGridViewUtil.GetColumnIndex(dataGridView1,"Comment");
        }
        void setup_dg()
        {
            m_dg.Rows.Clear();
            for(var r = 0; r < m_page_items.Count; r++)
            {
                var i = m_page_items[r];
                m_dg.Rows.Add();
                m_dg[NUM_COL,r].Value   = r.ToString();
                m_dg[ORDER_COL,r].Value = i.disporder.ToString();
                m_dg[UUID_COL,r].Value  = i.uuid;
                m_dg[DATE_COL,r].Value  = i.createtime.ToLocalTime().ToString();
                m_dg[TITLE_COL,r].Value = i.name;
                m_dg[CMT_COL,r].Value   = i.comment;
            }
            setfocus_by_cur_page_index();
        }
        public void setfocus_by_cur_page_index()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if ( m_cur_page_index < 0) // ||  m_cur_item_index >=  dataGridView1.Rows.Count )
                {
                    m_cur_page_index = 0;
                }
                else if (m_cur_page_index >= dataGridView1.Rows.Count)
                {
                    m_cur_page_index = dataGridView1.Rows.Count - 1;
                }
               dataGridView1.Rows[m_cur_page_index].Selected = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_cc2c!=null) m_cc2c.Update();
        }

        #region マウスイベント
        public bool m_mouse_up_down = true;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            m_mouse_up_down = false;            
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            m_mouse_up_down = true;
        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            m_mouse_up_down = true;
        }
        #endregion

        #region メニューイベント
        public bool m_bMenuEditTitle;
        private void editTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuEditTitle = true;
        }
        public bool m_bMenuEditComment;
        private void editCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuEditComment = true;
        }
        public bool m_bMenuAddNew;
        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuAddNew = true;
        }

        public bool m_bMenuDelete;
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuDelete = true;
        }

        public bool m_bMenuUp;
        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuUp = true;
        }

        public bool m_bMenuDown;
        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuDown = true;
        }

        public bool m_bMenuCancel;
        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuCancel = true;
        }
        #endregion
    }
}
