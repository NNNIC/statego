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
    internal partial class Draw
    {
        internal void draw_bg(Graphics g)
        {
            draw_bg(g,G.bg_color);
        }
        internal void draw_bg(Graphics g,Color col)
        {
            using(var pen = new Pen(col))
            {
                g.FillRectangle(pen.Brush,m_g.VisibleClipBounds);
            }
        }
        internal void draw_grid(Graphics g)
        {
            using(var pen = new Pen(G.grid_color))
            {
                var rect = g.VisibleClipBounds;
                for(var y= 0f; y<rect.Height; y+=20f)
                {
                    g.DrawLine(pen,0.0f,y,rect.Width,y);
                }
                for(var x = 0f; x<rect.Width; x+=20f)
                {
                    g.DrawLine(pen,x,0,x,rect.Height);
                }
            }
        }
    }
}
