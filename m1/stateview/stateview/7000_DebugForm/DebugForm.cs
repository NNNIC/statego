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


namespace stateview._7000_DebugForm
{
    public partial class DebugForm:Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender,EventArgs e)
        {
#if DEBUG
            textBox1.Text = G.log;
            textBoxPointerX.Text = G.point_on_viewform.X.ToString();
            textBoxPointerY.Text = G.point_on_viewform.Y.ToString();

            textBoxOnPictureboxX.Text   = G.point_on_pixcturebox.X.ToString();
            textBoxOnPictureboxY.Text   = G.point_on_pixcturebox.Y.ToString();

            textBoxOnBmpX.Text = G.point_on_bmp.X.ToString();
            textBoxOnBmpY.Text = G.point_on_bmp.Y.ToString();

            OvlyPbTextBoxX.Text = G.view_form.OverlayPictureBox.Location.X.ToString();
            OvlyPbTextBoxY.Text = G.view_form.OverlayPictureBox.Location.Y.ToString();

            textBoxScrollX.Text = G.view_form.hScrollBar1.Value.ToString();
            textBoxScrollY.Text = G.view_form.vScrollBar1.Value.ToString();

            textBox_scrollHMax.Text = G.view_form.hScrollBar1.Maximum.ToString();
            textBox_scrollVMax.Text = G.view_form.vScrollBar1.Maximum.ToString();

            textBox_scrollLargChangeH.Text = G.view_form.hScrollBar1.LargeChange.ToString();
            textBox_scrollLargChangeV.Text = G.view_form.vScrollBar1.LargeChange.ToString();

            textBoxPictureBoxSizeX.Text = G.view_form.MainPictureBox.Size.Width.ToString();
            textBoxPictureBoxSizeY.Text = G.view_form.MainPictureBox.Size.Height.ToString();

            textBox_panelSW.Text = G.view_form.panel1.Size.Width.ToString();
            textBox_panelSH.Text = G.view_form.panel1.Size.Height.ToString();

            //var vr = ViewUtil.GetViewRectangle_wo_limit();
            //textBox_vrsizeX.Text = vr.Width.ToString();
            //textBox_vrsizeY.Text = vr.Height.ToString();

            textBox_panelscrollMaxH.Text = G.view_form.panel1.HorizontalScroll.Maximum.ToString();
            textBox_panelscrollMaxV.Text = G.view_form.panel1.VerticalScroll.Maximum.ToString();


#endif
        }

        private void button1_Click(object sender,EventArgs e)
        {
            G.log = string.Empty;
            textBoxAppend.Text = string.Empty;
        }

        private void DebugForm_Load(object sender,EventArgs e)
        {
            //this.Visible = false;
        }

        private void DebugForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void loc_set()
        {
            var x = ParseUtil.ParseInt(textBox_mplocX.Text);
            var y = ParseUtil.ParseInt(textBox_mplocY.Text);

            G.view_form.MainPictureBox.Location = new Point(x,y);
        }

        private void button_LocReset_Click(object sender, EventArgs e)
        {
            textBox_mplocX.Text = "0";
            textBox_mplocY.Text = "0";
            loc_set();
        }

        private void button_LocSet_Click(object sender, EventArgs e)
        {
            loc_set();
        }
    }
}
