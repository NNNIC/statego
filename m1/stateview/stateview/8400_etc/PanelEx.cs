using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stateview._5000_MainForm
{
    class PanelEx : Panel
    {
        public int m_wheel_delta;
        protected override void WndProc(ref Message m)
        {
            //m_wheel_delta = 0;
            //Console.WriteLine("Event = {0}", m.Msg);
            if (m.Msg == 0x020A)
            {
                //Console.WriteLine("Mouse Wheel");
                //base.WndProc(ref m);
                m_wheel_delta  += (int)(Int64)m.WParam >> 16;
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
