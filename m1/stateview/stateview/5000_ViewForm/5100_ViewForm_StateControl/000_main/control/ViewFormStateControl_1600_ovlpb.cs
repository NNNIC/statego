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
    void ovlpb_setbmp() {
        G.overlay_picturebox.Image = G.overlaybitmap;
        G.overlay_picturebox.Width  = (int)( (double)G.overlaybitmap.Width  * G.scale);
        G.overlay_picturebox.Height = (int)( (double)G.overlaybitmap.Height * G.scale);
    }
    PointF m_ovlyRelatedPos;
    void ovlpb_show() {
        var d = G.m_draw_data_list[m_focus_state];
        G.overlay_picturebox.Parent = G.main_picturebox;

        G.overlay_picturebox.Location = Point.Truncate( GetPosOnMainPB(d.offset) );

        var cpos =  m_chkonfcsSc.m_savePosOnMainBmp;//GetPosOnMainPB(m_chkonfcsSc.m_savePos); // GetPointerOnMainBmp();
        m_ovlyRelatedPos = PointUtil.Sub_Point(d.offset,cpos);

        G.overlay_picturebox.Show();
    }
    void ovlpb_show_cc() {
        //var d = G.m_draw_data_list[m_focus_state];
        G.overlay_picturebox.Parent = G.main_picturebox;

        G.overlay_picturebox.Location = Point.Truncate( GetPosOnMainPB( G.point_on_bmp ) );

        //var cpos =  m_chkonfcsSc.m_savePosOnMainBmp;//GetPosOnMainPB(m_chkonfcsSc.m_savePos); // GetPointerOnMainBmp();
        m_ovlyRelatedPos = new Point( - G.overlay_picturebox.Width / 2  , - G.overlay_picturebox.Height / 2); //PointUtil.Sub_Point(d.offset,cpos);

        G.overlay_picturebox.Show();
    }
    void ovlpb_hide() {
        G.overlay_picturebox.Hide();
    }
}
