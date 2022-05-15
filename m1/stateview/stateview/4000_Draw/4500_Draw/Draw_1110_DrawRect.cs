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
        internal class DrawRect
        {
            internal float  width;
            internal float  height;

            internal Color  linecolor_top;
            internal Color  linecolor_right;
            internal Color  linecolor_left;
            internal Color  linecolor_bot;

            internal Color  bgcolor;

            internal string text;
            internal string fontname;
            internal float  fontsize;
            internal bool   fontunderline = false;
            internal Color  fontcolor;

            internal PointF textmargin = new PointF(0,0);

            internal Bitmap bmp;

            internal void draw(PointF pos, DrawList dlist, int pri)
            {
                var c = (DrawRect)this.MemberwiseClone();
                dlist.add(pri,(g)=> {
                    DrawUtil.DrawBoxText_LineAndFill(g,c.text,c.fontname,c.fontcolor,c.fontsize,c.fontunderline,
                        pos,
                        c.width,c.height,G.FRAMELINESIZE,

                        c.linecolor_top,
                        c.linecolor_right,
                        c.linecolor_bot,
                        c.linecolor_left,

                        c.bgcolor,

                        c.textmargin
                    );

                    DrawUtil.DrawBmp(g,pos,c.bmp);
                });
            }

            internal DrawRect clone()
            {
                return (DrawRect)this.MemberwiseClone();
            }
        }
    }
}
