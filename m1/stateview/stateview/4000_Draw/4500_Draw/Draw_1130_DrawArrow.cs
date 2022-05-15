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
        internal class DrawArrow
        {
            internal List<PointF> list;

            internal void draw(DrawList dlist, int pri, Color? c = null)
            {
                var tlist = list;
                var col   = Color.White;
                if (c!=null) col = (Color)c;
                dlist.add(pri, g=> {
                    DrawUtil.DrawLine(g,list,col,G.LINE_WIDTH,DrawUtil.LineType.BEZIR);
                });

                //dlist.add(pri,(g)=> {
                //    DrawUtil.DrawCircle_LineAndFill(
                //        g,
                //        pos,diameter,linewidth,linecolor,bgcolor
                //        );
                //});
            }
        }
    }
}
