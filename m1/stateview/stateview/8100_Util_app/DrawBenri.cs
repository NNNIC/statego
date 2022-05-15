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

public class DrawBenri
{
    static Draw m_draw { get { return G.draw; } }
    public static void draw_opt()
    {
        G.node_set_curdir(G.node_get_cur_dirpath()); //フィルター

        var new_state_list = G.node_get_display_all();  // 全表示ノード
        new_state_list.Sort((a,b)=>G.excel_program.GetUUID(a).CompareTo(G.excel_program.GetUUID(b))); //ソート
        G.state_list = new_state_list;

        StateWork.create_state_info();

        StateUtil.Collect_all_input_output_info(out G.state_input_src_list, out G.state_output_dst_list);

        m_draw.ClearCache();
        m_draw.Add_First();

        //StateBox用の描画データ作成
        for(var i = 0;i < G.state_list.Count;i++)
        {
            var state = G.state_list[i];
            var style = G.node_get_style(state);
            m_draw.create_draw_state_data(state,style);
        }

        //仮の位置とDrawリストへ描画データ積み上げ
        var pos = G.STARTPOS;
        var group_pos_list = new Dictionary<string, PointF>();
        for(var i = 0;i < G.state_list.Count;i++)
        {
            var state = G.state_list[i];

            PointF? lopos = null;

            if (G.node_get_style(state) == SS.STYLE.NORMAL) // ノーマルのみエクセルデータからロード
            {
                lopos = LocUtil.Get_lo_position_from_excel(state); 
            }
            if (lopos == null)
            {
                lopos = LocUtil.Get_lo_position(state); //レイアウトファイルからのロード
            }
            if (lopos == null) //ノーマル以外も
            {
                lopos = LocUtil.Get_lo_position_from_excel(state); //エクセルデータからロード
            }
            if (lopos!=null) 
            {
                pos = (PointF)lopos;
            }

            if (G.node_get_style(state) == StateStyle.STYLE.NORMAL)
            {
                G.UpdateExcelPos(state, pos);
            }

            //コピー先の場所？
            var copydstpos = CopyRenameStateSave.get_new_position(state,true);
            if (copydstpos!=null)
            {
                pos = (PointF)copydstpos;
            }

            var d = m_draw.draw_statebox(state,pos);

            if (pos.X + (d.size.Width + 100) * 2 > G.main_picturebox.Bounds.Width)
            {
                pos.X = G.STARTPOS.X;
                pos.Y = G.STARTPOS.Y + 100;
            }

            pos = PointUtil.Add_X(pos, d.size.Width +100 );
        }

        //矢印作成
        G.draw.arrow_start_goal_cache_create();
        for(var i = 0; i<G.state_list.Count; i++)
        {
            var state = G.state_list[i];
            m_draw.create_draw_arrow_data(state);
        }
        G.draw.arrow_start_goal_cache_destroy();

        //矢印をDrawListへ描画データ積み上げ
        for(var i = 0; i<G.state_list.Count; i++)
        {
            var state = G.state_list[i];
            m_draw.draw_arrow(state);
        }

        G.drawlistMain.execute(G.maingraphs);

        if (G.vf_sc!=null &&  G.vf_sc.m_center_focus_group!=null) //フォーカス先に先に移動
        {
            var grouppath = G.vf_sc.m_center_focus_group;
            var groupnode_name = stateview.GroupNodeUtil.get_last_path(grouppath).Trim('/');
            if (!string.IsNullOrEmpty(groupnode_name))
            {
                var posx = G.node_get_pos( AltState.MakeAltStateName(groupnode_name));
                if (posx != null && G.scale_percent != 0)
                {
                    var scale = ((double)G.scale_percent * 0.01f);
                    var pos2 = (Point)posx;

                    var ph = (double)G.view_form.panel1.Size.Height; 
                    var pw = (double)G.view_form.panel1.Size.Width; 

                    var e  = scale;

                    var sx =  (pos2.X * e) - pw * 0.5f;  
                    var sy =  (pos2.Y * e) - pw * 0.5f;

                    G.view_form.panel1.AutoScrollPosition = new Point((int)sx,(int)sy);
                }

            }
        }


        G.main_picturebox.Refresh();

        //GC
        GC.Collect(System.GC.MaxGeneration);
        GC.WaitForPendingFinalizers();

    }

    public static void draw_focuses(List<string> focus_list)
    {
        G.drawlistFocus.clear();
        if (focus_list!=null)
        { 
            foreach(var st in focus_list)
            {
                G.draw.draw_focus(st,Color.FromArgb(50,255,0,0));
                G.draw.draw_arrow_focus(st);
            }
            G.drawlistFocus.execute(G.maingraphs);
        }
    }

    public static void draw_groupnode_focus(string groupnode)
    {
        G.drawlistFocus.clear();
        var refstate_groupnode = G.draw.draw_focus_groupnode(groupnode,Color.FromArgb(50,255,0,0));

        G.draw.draw_arrow_focus_groupnode(groupnode);

        G.drawlistFocus.execute(G.maingraphs);
        //return refstate_groupnode;
    }
}
