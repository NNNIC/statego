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
        internal class DrawDeco
        {
            internal DecoImage.Data typ;  //state-typに基づくビットマップ及び位置情報
            internal DecoImage.Data dco;  //state-dcoに基づくビットマップ及び位置情報

            internal void draw(RectangleF frect, DrawList dlist, int pri)
            {
                if (typ==null) return;
                var pos = typ.GetBmpPos(frect);
                                
                dlist.add(pri+typ.pri_add,(g)=> {
                    DrawUtil.DrawBmp(g,pos,typ.bitmap);
                });
            }
        }
    }
}
