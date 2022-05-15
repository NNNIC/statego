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

namespace stateview
{
    public class FocusTrackPanel
    {
        stateview._5000_MainForm.ViewForm m_vf { get { return G.view_form; } }

        public DataGridView m_dgv    { get { return G.view_form.dataGridViewFocusTrack;       } }
        public GroupBox     m_gb     { get { return G.view_form.groupBox_focustrackpanel;     } }
        public Label        m_help   { get {return G.view_form.label_focustrack_help;         } }
        public TextBox      m_helptb { get { return G.view_form.textBox_focustack_helptext;   } }

        //private FocusTrackPanelMoveControl m_movecontrol;
        CustomPanelControl m_customcontrol;

        public void onload()
        {
            m_gb.Parent = G.view_form;
            m_gb.Location = PointUtil.Add_Point(m_gb.Location, m_vf.panel1.Location);
            m_gb.BringToFront();
            m_dgv.Columns[2].Width = m_dgv.Width - m_dgv.Columns[0].Width - m_dgv.Columns[1].Width;


            m_customcontrol = new CustomPanelControl();
            m_customcontrol.m_dgv = m_dgv;
            m_customcontrol.m_gb = m_gb;
            m_customcontrol.Start();

            m_gb.MouseMove += M_gb_MouseMove;
            m_gb.MouseDown += M_gb_MouseDown;
            m_gb.MouseUp   += M_gb_MouseUp;
            m_gb.MouseEnter += M_gb_MouseEnter;
            m_gb.MouseLeave += M_gb_MouseLeave;


            m_help.MouseEnter += M_help_MouseEnter;
            m_help.MouseLeave += M_help_MouseLeave;
            m_helptb.Visible = false;

            var nl = Environment.NewLine;
            m_helptb.Text = "Ctrl + 'T' -- On or Off Panel " + nl + 
                            "'[' --- Track Back"   + nl +
                            "']' --- Track Forward";
                
                ;
            if (stateview.RegistryWork.Get_item_historypanelonstart_enable() == false)
            {
                m_gb.Visible = false;
            }
        }

        private void M_help_MouseLeave(object sender, EventArgs e)
        {
            m_helptb.Visible = false;
        }

        private void M_help_MouseEnter(object sender, EventArgs e)
        {
            m_helptb.Visible = true;
        }

        private void M_gb_MouseEnter(object sender, EventArgs e)
        {
            movecontrol();
        }

        private void M_gb_MouseLeave(object sender, EventArgs e)
        {
            movecontrol(false);
        }

        private void M_gb_MouseUp(object sender, MouseEventArgs e)
        {
            movecontrol(false);
        }

        private void M_gb_MouseDown(object sender, MouseEventArgs e)
        {
            //G.NoticeToUser("md " + m_c.ToString());
            //m_c++;

            movecontrol();
        }

        private void movecontrol(bool bQuit=false)
        {
            //bool busy = true;
            //m_movecontrol.m_quit= bQuit;
            //while (busy)
            //{
            //    m_movecontrol.Update();
            //    busy = m_movecontrol.m_busy;
            //}
            if (m_customcontrol!=null) m_customcontrol.Update();
        }

        private void M_gb_MouseMove(object sender, MouseEventArgs e)
        {
            movecontrol();
        }

        public void close()
        {
            m_gb.Visible = false;
        }
        public void open()
        {
            m_gb.Visible = true;
        }

        public void move()
        {
            //G.NoticeToUser("moving");
        }

        public void open_or_close()
        {
            m_gb.Visible = !m_gb.Visible;
        }

        public void timer_update()
        {
            if (m_customcontrol!=null) m_customcontrol.Update();
        }

        public void adjust_size()
        {
            if (m_customcontrol!=null) m_customcontrol.adjustloc();
        }
    }
}
