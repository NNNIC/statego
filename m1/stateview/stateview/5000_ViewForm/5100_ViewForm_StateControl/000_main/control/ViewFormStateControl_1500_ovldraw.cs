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
    void ovldraw_do()
    {
        G.drawlistOverlay.clear();
        var d = G.m_draw_data_list[m_focus_state];
        d.draw_local(new PointF(0,0),G.drawlistOverlay,G.DWPRI_STATE);

        G.draw.draw_bg(G.overlaygraphs,Color.Red);
        G.drawlistOverlay.execute(G.overlaygraphs);
    }
}
