//<<<include=using.txt
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DS=stateview.DesignSpec;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using SS=stateview.StateStyle;
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

        public void calc(int w, int h, out int tw, out int th)
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

        public void draw()
        {
            try {
                if (m_changebmp != null)
                {
                    pictureBox1.Image = m_changebmp;
                    //pictureBox1.Width = m_changebmp.Width;
                    //pictureBox1.Height = m_changebmp.Height;
                }
                else
                {
                    pictureBox1.Image = m_bmp;
                    //pictureBox1.Width = m_bmp.Width;
                    //pictureBox1.Height = m_bmp.Height;
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
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

            var picpath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var path = Path.Combine(picpath, "StateGo");
            if (Directory.Exists(path))
            {
                openFileDialog1.InitialDirectory = path;
            }
            else
            {
                openFileDialog1.InitialDirectory = picpath;
            }

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
                    try { bmp.Dispose(); } catch { }
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

        private void ai_button_Click(object sender, EventArgs e)
        {

        }
        private static string EscapeCommandLineArgument(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return "\"\"";
            // ダブルクォートをエスケープ
            arg = arg.Replace("\\", "\\\\").Replace("\"", "`");
            return $"\"{arg}\"";
        }

        private void copy_button_Click(object sender, EventArgs e)
        {
            try
            {
                // m_changebmp があればそれを、なければ m_bmp をコピー
                if (m_changebmp != null)
                {
                    Clipboard.SetImage(m_changebmp);
                }
                else if (m_bmp != null)
                {
                    Clipboard.SetImage(m_bmp);
                    MessageBox.Show("Copied to clipboard", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No image", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region aicontrol
        Editor_bmpForm_aiControl m_aicontrol = null;
        private void ai2button_Click(object sender, EventArgs e)
        {
            m_aicontrol = new Editor_bmpForm_aiControl();
            m_aicontrol.m_form = this;
            m_aicontrol.Start();
        }
        private void ai2Button_update()
        {
            if (m_aicontrol != null)
            {
                if (!m_aicontrol.IsEnd())
                {
                    m_aicontrol.Update();
                }
                else
                {
                    m_aicontrol = null;
                }
            }
        }
        public void waiting_text_update()
        {
            if (waiting_textBox.Visible == false) return;

            // waiting_textBox.BackColor を白から青へ変化させる。
            // そして、青から白へ戻す。

            var c = waiting_textBox.ForeColor;

            // 白 (255,255,255) から 青 (0,0,255) へ向かって変化
            // 青成分を増やし、赤・緑成分を減らす
            int r = c.R;
            int g = c.G;
            int b = c.B;

            // 青に近づける
            if (r > 0) r = (int)(r * 0.9);
            if (g > 0) g = (int)(g * 0.9);
            if (b < 255) b = (int)(b + (255 - b) * 0.1);

            // もし十分青くなったら、白に戻す
            if (r < 10 && g < 10 && b > 245)
            {
                r = 255;
                g = 255;
                b = 255;
            }

            waiting_textBox.ForeColor = Color.FromArgb(r, g, b);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            waiting_text_update();
            ai2Button_update();
        }
        public void enable_allbuttons(bool ena)
        {
            load_button.Enabled = ena;
            paste_button.Enabled = ena;
            delete_button.Enabled = ena;
            ok_button.Enabled = ena;
            cancel_button.Enabled = ena;
            ai_button.Enabled = ena;
            copy_button.Enabled = ena;
        }
        #endregion

    }
}
