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


/*
    コピーコレクション　フォーム２

    ※ DoDragDropは DragからDropの終了まで待つ。これが面倒な理由

    Mouse Down --> イベント発生
    Mouse Up   --> イベント発生

    MouseDown Holdのまま Xミリ秒
*/


namespace stateview._5800_CopyCollection
{
    public partial class CCForm2 : Form
    {
        CCForm2Control m_c2c;

        public CCForm2()
        {
            InitializeComponent();
        }

        private void CCForm2_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            setup_name_col();

            m_c2c = new CCForm2Control();
            m_c2c.m_form = this;
            m_c2c.Start();

            Location = Cursor.Position;
            if (m_mode == MODE.none) m_mode = MODE.cache;
            if (m_mode == MODE.collection) radioButtonCollection.Checked = true;
            else if (m_mode == MODE.cache) radioButtonCache.Checked = true;
            else if (m_mode == MODE.trash) radioButtonTrash.Checked = true;

            m_cur_page_idx = 0;
            m_cur_item_index = 0;

            loaddata();

            Width = 240;

            if (G.cc.m_read_only)
            {
                radioButtonPreset.Checked = true;
                radioButtonCache.Enabled = false;
                radioButtonTrash.Enabled = false;
                radioButtonCollection.Enabled = false;
                textBox_readonlyinfo.Visible = true;
            }
            else
            {
                textBox_readonlyinfo.Visible = false;
                textBox_readonlyinfo.Enabled = false;

                if (!Directory.Exists( G.cc.m_preset_fullpath ))
                {
                    radioButtonPreset.Enabled = false;
                }
                else
                {
                    radioButtonPreset.Checked = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_c2c.Update();
        }

        #region datagridviewの表示
        int m_cur_page_idx = 0;
        public int m_cur_item_index {
            get;
            set;
        } = 0;
        List<CopyCollection.WorkItem>   m_pageitems;
        public CopyCollection.WorkItem  m_pageitem;

        public enum MODE {
            none,
            preset,
            collection,
            cache,
            trash
        }
        public MODE m_mode = MODE.none;

        public int NUM_COL   = -1;
        public int ORDER_COL = -1;
        public int ICON_COL  = -1;
        public int UUID_COL  = -1;
        public int DATE_COL  = -1;
        public int CMT_COL   = -1; 
        public int CNT_COL   = -1;

        int ICON_SIZE_INDEX  = 0;
        int[] ICON_SIZE_LIST = new int[] { 30,80,150 };
        int ICON_WIDTH  { get { return ICON_SIZE_LIST[ICON_SIZE_INDEX]; } }
        int ICON_HEIGHT { get { return ICON_SIZE_LIST[ICON_SIZE_INDEX]; } }

        void setup_name_col()
        {
            NUM_COL   = DataGridViewUtil.GetColumnIndex(dataGridView1,"DisplayNum");
            ORDER_COL = DataGridViewUtil.GetColumnIndex(dataGridView1,"ListOrder");
            ICON_COL  = DataGridViewUtil.GetColumnIndex(dataGridView1,"IconImage");
            UUID_COL  = DataGridViewUtil.GetColumnIndex(dataGridView1,"UUID");
            DATE_COL  = DataGridViewUtil.GetColumnIndex(dataGridView1,"LastUpdate");
            CMT_COL   = DataGridViewUtil.GetColumnIndex(dataGridView1,"Comment");
            CNT_COL   = DataGridViewUtil.GetColumnIndex(dataGridView1,"CopyCounter");
        }

        public void loaddata()
        {
            try {

                G.cc.read_work_date();

                change_mode(m_mode);
                if (m_pageitems==null) return;

                if (m_cur_page_idx <0 || m_cur_page_idx >= m_pageitems.Count)
                {
                    m_cur_page_idx = 0;
                }
                this.listBox_collection.Items.Clear();
                for(var n = 0; n < m_pageitems.Count; n++)
                {
                    var pageitem = m_pageitems[n];
                    if (pageitem!=null && pageitem.name != null)
                    {
                        listBox_collection.Items.Add(pageitem.name);
                    }
                }
                if (m_pageitems.Count > 0 && listBox_collection.Items!=null && listBox_collection.Items.Count > m_cur_page_idx )
                {
                    listBox_collection.SelectedIndex = m_cur_page_idx;
                }
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.MultiSelect = false;
                dataGridView1.Refresh();
                //createdata_by_combobox_select(true);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{9B78FE3B-46BB-4C36-96F2-2544C4E5EF63}"+ e.Message);
            }
        }

        public void setfocus_by_cur_index()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if ( m_cur_item_index < 0) // ||  m_cur_item_index >=  dataGridView1.Rows.Count )
                {
                    m_cur_item_index = 0;
                }
                else if (m_cur_item_index >= dataGridView1.Rows.Count)
                {
                    m_cur_item_index = dataGridView1.Rows.Count - 1;
                }
               dataGridView1.Rows[m_cur_item_index].Selected = true;
            }
        }

        public void createdata_by_combobox_select(bool bForce)
        {
            if (!bForce)
            {
                if (listBox_collection.SelectedIndex == m_cur_page_idx)
                {
                    return;
                }
            }

            m_cur_page_idx = listBox_collection.SelectedIndex;
            if (m_cur_page_idx < 0) return;

            SuspendLayout();

            dataGridView1.Rows.Clear();
            m_pageitem = m_pageitems[m_cur_page_idx];
            var items = G.cc.GetItemsIfFolder(m_pageitem);

            if (items != null) for(var r = 0; r < items.Count; r++)
            {
                var i = items[r];
                var cmt = i.comment;
                if (string.IsNullOrEmpty(cmt) || cmt=="?")
                {
                    cmt = i.name;
                }

                dataGridView1.Rows.Add();
                dataGridView1[NUM_COL,r].Value   = r.ToString();
                dataGridView1[ORDER_COL,r].Value = i.disporder.ToString();
                dataGridView1[ICON_COL,r].Value  = i.bitmap;
                dataGridView1[UUID_COL,r].Value  = i.uuid;
                dataGridView1[DATE_COL,r].Value  = i.createtime;
                dataGridView1[CMT_COL,r].Value   = cmt;
                dataGridView1[CNT_COL,r].Value   = i.copycount;

                dataGridView1.Rows[r].Height = ICON_HEIGHT;
            }
            dataGridView1.Columns[ICON_COL].Width = ICON_WIDTH;

            textBoxFolderComment.Text = m_pageitem.comment;

            setfocus_by_cur_index();

            ResumeLayout();
        }
        #endregion

        private void listBox_collection_SelectedIndexChanged(object sender, EventArgs e)
        {
            createdata_by_combobox_select(true);
        }

        private void labelIconSize_Click(object sender, EventArgs e)
        {
            ICON_SIZE_INDEX = (ICON_SIZE_INDEX+1) % 3;
            string[] labels = { "S","M", "L" };
            labelIconSize.Text = labels[ICON_SIZE_INDEX];
            createdata_by_combobox_select(true);
        }

        #region ラジオボタン モード変更
        int m_save_selected_idx=-1;
        private void radioButtonCollection_CheckedChanged(object sender, EventArgs e)
        {
            var targetmode = MODE.none;
            if (radioButtonCollection.Checked)
            {
                targetmode = MODE.collection;
            }
            else if (radioButtonCache.Checked)
            {
                targetmode = MODE.cache;
            }
            else if (radioButtonTrash.Checked)
            {
                targetmode = MODE.trash;
            }
            else if (radioButtonPreset.Checked)
            {
                targetmode = MODE.preset;
            }

            if (targetmode != MODE.none) 
            {
                if (m_mode != targetmode)
                {
                    if (m_mode == MODE.collection)
                    {
                        m_save_selected_idx = listBox_collection.SelectedIndex;
                    }
                    m_mode = targetmode;
                    m_cur_item_index = 0;

                    if (targetmode == MODE.collection)
                    {
                        m_cur_page_idx = m_save_selected_idx;
                    }
                    else
                    {
                        m_cur_page_idx = 0;
                    }
                }
                loaddata();
            }
        }
        private void change_mode(MODE mode)
        {
            SuspendLayout();

            buttonEdit.Visible = false;
            button_delall.Visible = false;
            listBox_collection.Visible = false;
            textBoxFolderComment.Visible = false;

            if (mode == MODE.collection)
            {
                m_mode = mode;
                m_pageitems = G.cc.GetCollectionRootItems();

                buttonEdit.Visible = true;
                listBox_collection.Visible = true;
                textBoxFolderComment.Visible = true;
            }
            else if (mode == MODE.cache)
            {
                m_mode = mode;
                var tempitem = G.cc.GetTempItem();
                m_pageitems = new List<CopyCollection.WorkItem>() { tempitem};

                button_delall.Visible = true;

            }
            else if (mode == MODE.trash)
            {
                m_mode = mode;
                var trashitem = G.cc.GetTrashItem();
                m_pageitems = new List<CopyCollection.WorkItem>() { trashitem };

                button_delall.Visible = true;
            }
            else if (mode == MODE.preset)
            {
                m_mode = mode;
                var presetitem = G.cc.FindItem(CopyCollection.PRESET_DIR);
                m_pageitems = new List<CopyCollection.WorkItem>() { presetitem };

                if (!G.cc.m_read_only)
                {
                    button_delall.Visible = true;
                }
            }
            else
            {
                throw new SystemException("{50F12B2A-FBA3-48C7-BB7B-3B5BE48B17D7}");
            }

            ResumeLayout();
        }
        #endregion

        #region マウスイベント
        public bool m_mouse_up_down = true;
        public int  m_mousedown_start;
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            m_mousedown_start = Environment.TickCount; //milisecond
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
        public bool m_bMenuCopy;
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuCopy = true;
        }

        public bool m_bMenuPaste;
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuPaste = true;
        }

        public bool m_bMenuDelete;
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuDelete = true;
        }

        public bool m_bMenuCut;
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuCut = true;
        }

        public bool m_bMenuEdit;
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuEdit = true;
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

        public bool m_bMenuEditBitmap;
        private void bitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuEditBitmap = true;
        }

        public bool m_bMenuEditTitle;
        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //G.NoticeToUser("titleToolStripMenuItem_Click");
            m_bMenuEditTitle = true;
        }

        public bool m_bMenuExplorer;
        private void explorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_bMenuExplorer = true;
        }

        #endregion

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var dlg = new _5800_CopyCollection.CCForm2PageForm();
            dlg.ShowDialog();
            loaddata();
        }


        private void CCForm2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Visible = false;
            G.view_form.m_cc2 = null;
        }

        #region 強制更新
        public void force_update()
        {
            m_c2c.m_ForceUpdate = true;
        }
        public bool force_update_flg()
        {
            return m_c2c.m_ForceUpdate;
        }
        #endregion

        private void button_delall_Click(object sender, EventArgs e)
        {
            if (m_mode == MODE.trash)
            {
                var items = G.cc.GetTrashItems();
                if (items == null || items.Count == 0)
                {
                    return;
                }
                if (MessageBox.Show(G.Localize( "cc_deleteall"),G.Localize("cc_confirm"),MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    var count = items.Count;
                    for(var n = 0; n<count; n++)
                    {
                        items = G.cc.GetTrashItems();
                        if (items == null || items.Count ==0)
                        {
                            break;
                        }
                        G.cc.DeleteItem(items[0]);
                    }
                    loaddata();
                }
            }
            else if (m_mode == MODE.cache)
            {
                var items = G.cc.GetTempItems();
                if (items == null || items.Count == 0)
                {
                    return;
                }
                if (MessageBox.Show(G.Localize("cc_allmove")/*"All items will move to the trash page."*/,G.Localize("cc_confirm"),MessageBoxButtons.OKCancel)== DialogResult.OK)
                {
                    var count = items.Count;
                    for(var n = 0; n<count; n++)
                    {
                        items = G.cc.GetTempItems();
                        if (items == null || items.Count ==0)
                        {
                            break;
                        }
                        var trashdir = G.cc.GetTrashItem().folder;
                        var newitem = G.cc.CloneItem(items[0], trashdir);
                        G.cc.DeleteItem(items[0]);
                    }
                    loaddata();
                }
            }
        }

    }
}
