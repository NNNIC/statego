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

    public PointF GetPointerOnMainBmp()
    {
        var pos = (PointF)G.main_picturebox.PointToClient(Cursor.Position);
        pos = PointUtil.Multiply(pos, (float)(1.0f/G.scale));
        return pos;
    }
    public PointF GetPointerOnMainBmp(Point pos_on_sc)
    {
        var pos = (PointF)G.main_picturebox.PointToClient(pos_on_sc);
        pos = PointUtil.Multiply(pos, (float)(1.0f/G.scale));
        return pos;
    }
    public PointF GetPointerOnMainBmp(Point pos_on_sc, double scale) //※セーブしたスケールで計算するため。Change Scaleで使用
    {
        var pos = (PointF)G.main_picturebox.PointToClient(pos_on_sc);
        pos = PointUtil.Multiply(pos, (float)(1.0f/scale));
        return pos;
    }
    public PointF GetPosOnMainPB(PointF point_at_image)
    {
        var pos = PointUtil.Multiply(point_at_image, (float)G.scale);
        return pos;
    }
    public PointF GetPanelPos(PointF point_on_image)
    {
        var scpoint = GetScreenPosFormPointOnImage(point_on_image);
        var panelpoint = G.view_form.panel1.PointToClient(Point.Round(scpoint));
        return panelpoint;
    }

    public PointF GetScreenPosFormPointOnImage(PointF point_on_image)
    {
        var pos_on_main_pb = GetPosOnMainPB(point_on_image);
        var scpos = G.main_picturebox.PointToScreen(Point.Round(pos_on_main_pb));
        return scpos;
    }

    public string GetStateOnPointer()
    {
        var pos = GetPointerOnMainBmp();

        //ステート検索
        //  フォーカスを先に
        if (!string.IsNullOrEmpty(m_focus_state) && G.m_draw_data_list.ContainsKey(m_focus_state))
        {
            var sd = G.m_draw_data_list[m_focus_state];
            if (sd.wp_frame_drect.Contains(pos))
            {
                return m_focus_state;
            }
        }

        foreach (var p in G.m_draw_data_list)
        {
            var sd = p.Value;
            if (sd==null) continue;
            if (sd.wp_frame_drect.Contains(pos))
            {
                return p.Key;
            }
        }

        return null;
    }
    public string GetStateOnPointer_outframe()
    {
        var pos = GetPointerOnMainBmp();

        //ステート検索
        foreach (var p in G.m_draw_data_list)
        {
            var sd = p.Value;
            if (sd==null) continue;
            if (sd.wp_outframe_drect.Contains(pos))
            {
                return p.Key;
            }
        }
        return null;
    }
    public void Check_state_under_pointer()
    {
        m_state_under_pointer = null;
        if (G.m_draw_data_list==null) return;

        var pos = GetPointerOnMainBmp();
        for(var i = G.state_list.Count-1 ; i>=0; i--)
        {
            var state = G.state_list[i];
            if (G.m_draw_data_list.ContainsKey(state))
            {
                var sd = G.m_draw_data_list[state];
                if (sd==null) continue;
                if (sd.wp_frame_drect.Contains(pos))
                {
                    m_state_under_pointer = state;
                    break;
                }
            }
        }
    }

    public bool IsBusy()
    {
        if (CheckState(S_Idle2)) return false;
        if (CheckState(S_IsMouseDown)) return false;
        if (CheckState(S_CheckMouseGN)) return false;
        if (CheckState(S_CheckMouseGF)) return false;
  
        return true;
    }

    public bool isCtrlKey()
    {
        return (Control.ModifierKeys & Keys.Control) == Keys.Control;
    }


}
