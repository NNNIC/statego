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

using stateview;

public partial class ViewFormStateControl {
    void statebox_redraw()
    {
        var cpos = GetPointerOnMainBmp();
        var loc  = PointUtil.Add_Point(cpos,m_ovlyRelatedPos);
        var d = G.m_draw_data_list[m_focus_state];

        d.set_offset(loc); //移動ステートのみ
        G.UpdateExcelPos(m_focus_state, loc);

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

        G.main_picturebox.Refresh();

        SaveLoadIni.SaveTempIni();

        History2.SaveForce_modify_pos(m_focus_state);
    }

    void statebox_totaldraw()
    {
        stateview.Flow.main_skip_load_flow();
    }


    Draw m_draw { get { return G.draw; } }
    public void statebox_optdraw()
    {
        DrawBenri.draw_opt();
        m_isReqRedraw = false;

        //if (m_request_redrawNodeTreeView)
        {
            m_request_redrawNodeTreeView = false;
            G.tabNodeTree.CreateAndSetCurrent();
        }

        //stateview.SaveLoadJson.SaveTempJson();
        stateview.SaveLoadIni.SaveTempIni();

        G.view_form.panel1.Focus();
        //History.SaveAtRedraw();

    }

}
