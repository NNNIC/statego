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
    internal class DrawList
    {
        internal class Item
        {
            internal Action<Graphics> func;
            internal int    priority;
        }

        internal Hashtable m_hash = new Hashtable();

        internal void add(int priority, Action<Graphics> func)
        {
            var i = new Item();
            i.func     = func;
            i.priority = priority;

            if (m_hash.ContainsKey(priority))
            {
                var nh = (List<Item>)m_hash[priority];
                nh.Add(i);
                m_hash[priority] = nh;
            }
            else
            {
                var nh = new List<Item>();
                nh.Add(i);
                m_hash[priority] = nh;
            }
        }

        internal void clear()
        {
            m_hash.Clear();
        }

        internal void execute(Graphics g)
        {
            for(var p = 0; p<=10000; p++)
            {
                if (m_hash.ContainsKey(p))
                {
                    var nh = (List<Item>)m_hash[p];
                    foreach(var i in nh)
                    {
                        i.func(g);
                    }
                }
            }
        }
    }
}
