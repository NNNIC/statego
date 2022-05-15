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

namespace stateview._5800_CopyCollection
{
    public partial class CCBmpForm : Form
    {
        public CCBmpForm()
        {
            InitializeComponent();
        }

        public CopyCollection.WorkItem m_item;
        Bitmap m_bmp;

        private void CCBmpForm_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            DialogResult = DialogResult.None;
            var bmppath = !string.IsNullOrEmpty(m_item.iconpng_path) ? m_item.iconpng_path : m_item.cappng_path;
            m_bmp = new Bitmap(bmppath);;
            pictureBox1.Image = m_bmp;

            this.button_USECAP.Visible = !string.IsNullOrEmpty(m_item.iconpng_path);
        }

        private void CCBmpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.None)
            {
                DialogResult = DialogResult.Cancel;
               
            }
            if (DialogResult == DialogResult.Cancel)
            {
                if (m_bmp != null) m_bmp.Dispose();
            }
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            m_item.bitmap.Dispose();
            m_item.bitmap = null;
            m_item.bitmap = m_bmp;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button_LOAD_Click(object sender, EventArgs e)
        {
            if ( openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var newbmp = default(Bitmap);
                var newbmpfilepath = openFileDialog1.FileName;
                try {
                    var tmp = new Bitmap(newbmpfilepath);
                    newbmp = new Bitmap(tmp);
                    tmp.Dispose();
                    tmp = null;
                }
                catch (SystemException e2)
                {
                    newbmp = null;
                    G.NoticeToUser("{DEE6A3D9-22CE-42D8-ACE2-7EDEEF41D448}" + e2.Message);
                }
                if (newbmp!=null)
                {
                    m_bmp.Dispose();
                    m_bmp = newbmp;
                    pictureBox1.Image = m_bmp;
                }
            }
        }

        private void button_PASTE_Click(object sender, EventArgs e)
        {
            try {
                using (var bmp = Clipboard.GetImage())
                {
                    if (bmp != null)
                    {
                        m_bmp.Dispose();
                        m_bmp = null;
                        m_bmp = new Bitmap(bmp);
                        pictureBox1.Image = m_bmp;
                    }
                }
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning("{193968FC-4664-4EA6-A95E-EACBC4526B65}" + e2.Message);
            }
        }

        private void button_USECAP_Click(object sender, EventArgs e)
        {
            var newbmp = default(Bitmap);
            try {
                var cappath = m_item.cappng_path;
                var tmp = new Bitmap(cappath); //ファイルがオープンしたままになる問題あり。
                newbmp = new Bitmap(tmp);
                tmp.Dispose();
                tmp = null;
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning("{35000178-F912-4EA9-8F80-D880DC171379}" + e2.Message);
                newbmp = null;
            }
            if (newbmp!=null)
            {
                m_bmp.Dispose();
                m_bmp = null;
                m_bmp = newbmp;
                pictureBox1.Image = m_bmp;
            }
        }
    }
}
