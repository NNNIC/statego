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

// 水平、垂直にそろえる。
public partial class ViewFormStateControl {
    void br_AlignHorizontal(Action<int,bool> st )
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.ALIGN_HORIZONTALLY)
        {
            SetNextState(st);
        }
    }
    void br_AlignVertical(Action<int,bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.ALIGN_VERTICALLY)
        {
            SetNextState(st);
        }
    }
    void br_CommentOut(Action<int,bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.COMMENT_OUT)
        {
            SetNextState(st);
        }
    }

    void align_horizontal()
    {
        //クリックしているのは？ m_state_under_pointer
        //対象は m_group_focus_list
        if (string.IsNullOrEmpty( m_state_under_pointer )) return;
        if (m_group_focus_list==null || m_group_focus_list.Count == 0) return;

        var focus_d =  DictionaryUtil.Get(G.m_draw_data_list,m_state_under_pointer);
        if (focus_d == null) return;

        var target_y = focus_d.offset.Y;
        foreach(var s in m_group_focus_list)
        {
            var d = DictionaryUtil.Get( G.m_draw_data_list,s);
            if (d!=null)
            {
                var loc = PointUtil.Mod_Y(d.offset,target_y);
                d.set_offset(loc);
                G.UpdateExcelPos(s,loc);
            }
        }
        G.node_save_position();

        align_redraw();

        History2.SaveForce_modify_pos("align H");
    }
    void align_vertical()
    {
        if (string.IsNullOrEmpty( m_state_under_pointer )) return;
        if (m_group_focus_list==null || m_group_focus_list.Count == 0) return;

        var focus_d =  DictionaryUtil.Get(G.m_draw_data_list,m_state_under_pointer);
        if (focus_d == null) return;

        var target_x = focus_d.offset.X;
        foreach(var s in m_group_focus_list)
        {
            var d = DictionaryUtil.Get( G.m_draw_data_list,s);
            if (d!=null)
            {
                var loc = PointUtil.Mod_X(d.offset,target_x);
                d.set_offset(loc);
                G.UpdateExcelPos(s,loc);
            }
        }
        G.node_save_position();

        align_redraw();

        History2.SaveForce_modify_pos("align V");
    }

    void align_redraw()
    {

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

        stateview.SaveLoadIni.SaveTempIni();
    }
    void comment_out_gf()
    {
        if (m_group_focus_list==null || m_group_focus_list.Count == 0) return;
        StateUtil.CommentoutGroupList(m_group_focus_list);
        History2.SaveForce_commentout("Comment out multiple states");
    }
}   