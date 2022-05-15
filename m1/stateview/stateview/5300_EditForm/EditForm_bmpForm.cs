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

namespace stateview._5300_EditForm
{
    public partial class EditForm_bmpForm:Form
    {
        public Bitmap m_bmp;
        public Bitmap m_changebmp;
        public bool?  m_del;
        public Form m_pform;

        public EditForm_bmpForm()
        {
            InitializeComponent();
        }

        private void EditForm_bmpForm_Load(object sender,EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            //System.Diagnostics.Debugger.Launch();
            //Location = Cursor.Position;

            if (!FormUtil.SetCenterInForm(this,m_pform))
            {
                Location = Cursor.Position;
            }

            //if (m_changebmp!=null)
            //{
            //    draw();
            //    return;
            //}
            m_del = null;

            if (m_bmp!=null)
            {
                int w,h;
                calc(m_bmp.Width, m_bmp.Height,out w, out h);

                if (m_bmp.Width == w && m_bmp.Height == h)
                {
                    m_changebmp = null;
                }
                else
                {
                    m_changebmp = new Bitmap(w,h);
                    using(var g = Graphics.FromImage(m_changebmp))
                    {
                        g.DrawImage(m_bmp,0,0,m_changebmp.Width,m_changebmp.Height);
                    }
                }
                draw();
                return;
            }
        }

        private void calc(int w, int h, out int tw, out int th)
        {
            var rs = G.thumbnail_size;

            if (rs <= 0) throw new SystemException("Unexpected! {3B8C1AB0-5F91-44D5-9099-79578CC752AB}");

            tw = th = rs;
            if (h > w)
            {
                var r = (double)rs / h;
                tw = (int)(w * r);
            }
            else
            {
                var r = (double)rs / w;
                th = (int)(h * r);
            }
        }

        private void draw()
        {
            try {
                if (m_changebmp != null)
                {
                    pictureBox1.Image  = m_changebmp;
                    pictureBox1.Width  = m_changebmp.Width;
                    pictureBox1.Height = m_changebmp.Height;
                }
                else
                {
                    pictureBox1.Image  = m_bmp;
                    pictureBox1.Width  = m_bmp.Width;
                    pictureBox1.Height = m_bmp.Height;
                }
                pictureBox1.Refresh();
                pictureBox1.Show();
                m_del = null;
            } catch (SystemException e) {
                G.NoticeToUser_warning(G.Localize("w_faildtodrawtumbnail")/* "Faild to draw thumbnail. "*/ + e.Message );
            }
        }

        //private void convert()
        //{
        //    //return;
        //    var hofw = (double)m_temp.Height / m_temp.Width;
        //    var outw = (double)G.thumbnail_size;
        //    var outh = (double)G.thumbnail_size;
        //    if (hofw > 1.0f) // 縦が長い　故に縦をmaxとする
        //    {
        //        var r = (double)outh/ m_temp.Height;
        //        outw = m_bmp.Width * r;
        //    }
        //    else
        //    {
        //        var r = (double)outw/m_temp.Width;
        //        outh = m_bmp.Height * r;
        //    }

        //    var bmp2 = new Bitmap((int)outw,(int)outh);
        //    {
        //        using(var g = Graphics.FromImage(m_temp))
        //        {
        //            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            g.DrawImage(m_temp,0,0,bmp2.Width,bmp2.Height);
        //        }
        //        try { m_temp.Dispose(); } catch { }
        //        m_temp = null;
        //        m_temp = bmp2;
        //    }
        //}

        private void ok_button_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            this.Close();
        }

        private void load_button_Click(object sender,EventArgs e)
        {
            openFileDialog1.Filter = @"Image files|*.bmp;*.png;*.gif;*.jpg";
            if (openFileDialog1.ShowDialog()== DialogResult.OK)
            {
                try {
                    using(var loadbmp = (Bitmap)Image.FromFile(openFileDialog1.FileName))
                    {
                        if (m_changebmp!=null)
                        {
                            m_changebmp.Dispose();
                            m_changebmp = null;
                        }
                        int w,h;
                        calc(loadbmp.Width,loadbmp.Height,out w, out h);
                        m_changebmp = new Bitmap(w,h);
                        using(var g = Graphics.FromImage(m_changebmp))
                        {
                            g.DrawImage(loadbmp,0,0,m_changebmp.Width,m_changebmp.Height);
                        }
                    }
                    draw();
                }
                catch (SystemException ex){
                    G.NoticeToUser_warning(G.Localize("w_failedtoloadimage")/* "Faild to load image. "*/ + ex.Message);
                }
            }
        }

        private void paste_button_Click(object sender,EventArgs e)
        {
            try { 
                using(var bmp = Clipboard.GetImage())
                {
                    if (m_changebmp!=null)
                    {
                        m_changebmp.Dispose();
                        m_changebmp = null;
                    }
                    int w,h;
                    calc(bmp.Width,bmp.Height,out w, out h);
                    m_changebmp = new Bitmap(w,h);
                    using(var g = Graphics.FromImage(m_changebmp))
                    {
                        g.DrawImage(bmp,0,0,m_changebmp.Width,m_changebmp.Height);
                    }
                    draw();
                }
            }
            catch (SystemException ex)
            {
                G.NoticeToUser_warning(G.Localize("w_faildtopateimage") /* "Faild to paste image. "*/ + ex.Message);
            }
        }

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (m_bmp != null)
            {
                m_del = true;
                pictureBox1.Hide();
            }
        }
    }
}
