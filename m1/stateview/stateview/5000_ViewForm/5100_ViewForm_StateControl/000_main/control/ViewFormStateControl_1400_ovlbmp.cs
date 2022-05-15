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

public partial class ViewFormStateControl {
    void ovlbmp_create(Bitmap bmp = null)
    {
        if (G.overlaybitmap!=null)
        {
            G.overlay_picturebox.Image = null;
            G.overlaygraphs.Dispose();
            G.overlaybitmap.Dispose();
            G.overlaybitmap = null;
        }
        if (bmp==null)
        {
            var d = G.m_draw_data_list[m_focus_state];
            G.overlaybitmap = new Bitmap((int)d.outframe_drect.width,(int)d.outframe_drect.height);
        }
        else
        {
            G.overlaybitmap = new Bitmap(bmp);
        }
        G.overlaygraphs = Graphics.FromImage(G.overlaybitmap);
    }
}
