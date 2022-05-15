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
    void move_update_obs()
    {
        var cpos = GetPointerOnMainBmp();
        var loc = PointUtil.Add_Point(cpos,m_ovlyRelatedPos);
        loc = GetPosOnMainPB(loc);
        G.overlay_picturebox.Location = Point.Truncate(loc);
    }
    void move_update()
    {
        var cpos = GetPointerOnMainBmp();
        var loc = PointUtil.Add_Point(cpos,m_ovlyRelatedPos);
        var dd = G.get_draw_data(m_focus_state);
        var p_bound = new RectangleF(loc.X,loc.Y, dd.wp_outframe_drect.Width,dd.wp_outframe_drect.Height);

        var bChange = false;
        var fcpos = cpos;
        if (p_bound.Left  < 0 ) { bChange = true;  fcpos.X = -m_ovlyRelatedPos.X; }
        if (p_bound.Top   < 0 ) { bChange = true;  fcpos.Y = -m_ovlyRelatedPos.Y; }
        if (p_bound.Right  > G.bitmap_width)  { bChange = true;  fcpos.X = G.bitmap_width  - dd.wp_outframe_drect.Width  - m_ovlyRelatedPos.X; }
        if (p_bound.Bottom > G.bitmap_height) { bChange = true;  fcpos.Y = G.bitmap_height - dd.wp_outframe_drect.Height - m_ovlyRelatedPos.Y; }

        if (bChange)
        { 
            var cloc = G.vf_sc.GetScreenPosFormPointOnImage(fcpos);
            Cursor.Position = Point.Truncate(cloc);

            var loc2 = PointUtil.Add_Point(fcpos,m_ovlyRelatedPos);
            G.overlay_picturebox.Location = Point.Truncate(loc2);
        }
        else
        { 
            loc = GetPosOnMainPB(loc);
            G.overlay_picturebox.Location = Point.Truncate(loc);
        }
    }

    void move_update_cc()
    {
        var cpos = GetPointerOnMainBmp();
        var loc = PointUtil.Add_Point(cpos,m_ovlyRelatedPos);
        //var dd = G.get_draw_data(m_focus_state);
        var ccbmp = G.m_cc_dropbmp;
        var p_bound = new RectangleF(loc.X,loc.Y, ccbmp.Width,ccbmp.Height);

        var bChange = false;
        var fcpos = cpos;
        if (p_bound.Left  < 0 ) { bChange = true;  fcpos.X = -m_ovlyRelatedPos.X; }
        if (p_bound.Top   < 0 ) { bChange = true;  fcpos.Y = -m_ovlyRelatedPos.Y; }
        if (p_bound.Right  > G.bitmap_width)  { bChange = true;  fcpos.X = G.bitmap_width  - ccbmp.Width  - m_ovlyRelatedPos.X; }
        if (p_bound.Bottom > G.bitmap_height) { bChange = true;  fcpos.Y = G.bitmap_height - ccbmp.Height - m_ovlyRelatedPos.Y; }

        if (bChange)
        { 
            var cloc = G.vf_sc.GetScreenPosFormPointOnImage(fcpos);
            Cursor.Position = Point.Truncate(cloc);

            var loc2 = PointUtil.Add_Point(fcpos,m_ovlyRelatedPos);
            G.overlay_picturebox.Location = Point.Truncate(loc2);
        }
        else
        { 
            loc = GetPosOnMainPB(loc);
            G.overlay_picturebox.Location = Point.Truncate(loc);
        }
    }

}
