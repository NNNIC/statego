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

public partial class DAnDGFStateControl   {

    void ovlbmp_create()
    {
        //フォーカスステートリストから全体を囲むRectangleを作成
        RectangleF? rect = null;
        foreach(var s in m_group_focus_list)
        {
            var d = G.get_draw_data(s);
            if (d==null) continue;
            var r = d.wp_outframe_drect;
            if (r!=null)
            {
                if (rect==null)
                {
                    rect = (RectangleF)r;
                }
                else
                {
                    rect = RectangleUtil.CombineF((RectangleF)rect,(RectangleF)r);
                }
            }
        }
        m_allrect   = (RectangleF)rect;////RectangleUtil.AddMargin( (RectangleF)rect, m_margin );
        m_bitmap    = new Bitmap((int)m_allrect.Width,(int)m_allrect.Height);
        m_graphics  = Graphics.FromImage(m_bitmap);
    }
    void ovldraw_do()
    {
        G.drawlistOverlay.clear();
        foreach(var s in m_group_focus_list)
        {
            try { 
                var d = G.get_draw_data(s);
                //相対位置に変換

                var pos = PointUtil.Sub_Point( d.wp_outframe_drect.Location , m_allrect.Location);
                d.draw_local(pos,G.drawlistOverlay,G.DWPRI_STATE);
                G.draw.draw_bg(m_graphics,Color.Red);
                G.drawlistOverlay.execute(m_graphics);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{BB526230-0EBB-462A-8A26-50D924CEA4AC} " + e.Message);
            }
        }
    }
    void ovlpb_setbmp()
    {
        G.overlay_picturebox.Image  = m_bitmap;
        G.overlay_picturebox.Width  = (int)( (double)m_bitmap.Width  * G.scale);
        G.overlay_picturebox.Height = (int)( (double)m_bitmap.Height * G.scale);
    }

    PointF m_ovlyRelatedPos;
    void ovlpb_show() {
        G.overlay_picturebox.Parent = G.main_picturebox;

        G.overlay_picturebox.Location = Point.Truncate(m_allrect.Location);

        var cpos = m_parent.GetPointerOnMainBmp(m_start);
        //var cpos =  m_chkonfcsSc.m_savePosOnMainBmp;//GetPosOnMainPB(m_chkonfcsSc.m_savePos); // GetPointerOnMainBmp();
        m_ovlyRelatedPos = PointUtil.Sub_Point(m_allrect.Location,cpos);

        G.overlay_picturebox.Show();
    }

    void move_update()
    {
        var cpos = m_parent.GetPointerOnMainBmp();
        var loc = PointUtil.Add_Point(cpos,m_ovlyRelatedPos);
        var nextr = new RectangleF(loc.X,loc.Y,m_allrect.Width,m_allrect.Height);

        if (G.mainbmprect.Contains(nextr))
        {
            G.overlay_picturebox.Location = Point.Truncate(m_parent.GetPosOnMainPB(loc));
        }
    }

    bool wait_mouseany()
    {
        if (G.mouse_down_or_up)
        {
            if (G.mouse_event == stateview.MouseEventId.MOVE || G.mouse_event == stateview.MouseEventId.NONE)
            {
                //G.log += "mouse event none" + Environment.NewLine;
                return false;
            }
        }
        //G.log += "mouse event !!" + Environment.NewLine;
        return true;
    }

    void br_IsDrop(Action<int,bool> st)
    {
        if (G.mouse_event == stateview.MouseEventId.MOUSEUP)
        {
            SetNextState(st);
        }
    }

    void br_NotAbove(Action<int,bool> st)
    {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }

    void ovlpb_hide() {
        G.overlay_picturebox.Hide();
    }

    void statebox_redraw()
    {
        var pbloc =  G.overlay_picturebox.Location;
        var bmppos = PointUtil.Multiply( pbloc , (float)1/(float)G.scale);
        var diff = PointUtil.Sub_Point(bmppos, m_allrect.Location);

        foreach(var s in m_group_focus_list)
        {
            var d = G.m_draw_data_list[s];
            var newloc = PointUtil.Add_Point(d.wp_outframe_drect.Location,diff);
            d.set_offset(newloc); //移動ステートのみ
            G.UpdateExcelPos(s,newloc);
        }

        //main draw listの再生成
        G.drawlistMain.clear();
        G.draw.Add_First();

        //再Draw
        foreach(var p in G.m_draw_data_list)
        {
            var state = p.Key;
            var dd = p.Value;
            G.draw.draw_statebox(state,dd.offset);
        }

        //矢印作成
        G.draw.arrow_start_goal_cache_create();
        for(var i = 0; i<G.state_list.Count; i++)
        {
            var state = G.state_list[i];
            G.draw.create_draw_arrow_data(state);
        }
        G.draw.arrow_start_goal_cache_destroy();

        //矢印をDrawListへ描画データ積み上げ
        for(var i = 0; i<G.state_list.Count; i++)
        {
            var state = G.state_list[i];
            G.draw.draw_arrow(state);
        }

        G.drawlistMain.execute(G.maingraphs);

        //再フォーカス
        DrawBenri.draw_focuses(m_group_focus_list);

        G.main_picturebox.Refresh();

        //stateview.SaveLoad.SaveTemp();
        //stateview.SaveLoadJson.SaveTempJson();
        stateview.SaveLoadIni.SaveTempIni();
    }
    void def_Success()
    {
        m_result = RESULT.SUCCESS;
    }
    void def_Cancel()
    {
        m_result = RESULT.CANCEL;
    }
    void destroy_all()
    {
        G.overlay_picturebox.Hide();
        m_graphics.Dispose();
        m_bitmap.Dispose();
    }
}
