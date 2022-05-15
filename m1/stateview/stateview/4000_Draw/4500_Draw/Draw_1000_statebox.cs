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
        int    DWPRI_STATE { get { return G.DWPRI_STATE; } } //ステート部分描画用のプライオリティ
        int    DWPRI_ARROW { get { return G.DWPRI_ARROW; } } //矢印部分描画用のプライオリティ
        PointF PADDING    { get { return G.PADDING; } } //TLを0,0としたときの外枠位置
        PointF TEXTMARGIN { get { return G.TEXTMARGIN; } }
        float  GAP { get {return G.GAP; } }
        float  LINEWIDTH { get { return G.FRAMELINESIZE; } }

        Dictionary<string,DrawStateData> m_draw_state_cache
        {
            get { return G.m_draw_data_list;  }
            set { G.m_draw_data_list = value; }
        }

        internal Draw()
        {
            m_draw_state_cache = new Dictionary<string, DrawStateData>();
        }

        internal void create_draw_state_data(string state, SS.STYLE style)
        {
            DrawStateData d = null;

            if (!m_draw_state_cache.ContainsKey(state))
            {
                d        = new DrawStateData();
                d.state  = state;
                var si   = get_state_info(state);
                if (si==null) return;

                // draw rect 収集
                /*
                    class DrawRect

                    TLを 0,0として
                    枠情報 (X,Y,W,H)
                    Text枠情報(X,Y,W,H)
                    Text内容

                    描画用の Draw(X,Y)で絶対値描画

                */

                //var groupname = G.get_node_dirname(state);
                //var groupcomment = G.get_node_comment(state);
                //if (style == SS.STYLE.FORGROUP) {
                //    if (string.IsNullOrEmpty(groupname)) groupname = "?";
                //    if (string.IsNullOrEmpty(groupcomment)) groupcomment = "?";
                //}
                //else {
                //    groupname = null;     //判定に利用するため
                //    groupcomment = null; //判定に利用するため
                //}

                float w;

                Func<DS.PARTS, Color> getcol = (p) => { return DS.GetColor(style, p, si.state); };

                if (style == SS.STYLE.FORGROUP)
                {
                    if (!AltState.IsAltState(state)) throw new SystemException("Unexpected! {90681DDC-B1D9-4F41-8E43-489F17788965}");
                    var groupname =  AltState.TrimAltStateName(state);//G.node_get_groupname(state);
                    var groupcomment = G.node_get_comment_by_groupname(groupname);
                    if (string.IsNullOrEmpty(groupname)) groupname = "?";
                    if (string.IsNullOrEmpty(groupcomment)) groupcomment = "";

                    d.state_drect         =  create_state_drect(si.state_cell,si.state_cmt_cell, getcol(DS.PARTS.STATEFRAME), getcol(DS.PARTS.STATEBG), getcol(DS.PARTS.STATETEXT),groupname);
                    w = d.state_drect.width;
                    d.statecmt_drect      =  create_statecmt_drect(si.state_cell, si.state_cmt_cell,w, getcol(DS.PARTS.STATEFRAME), getcol(DS.PARTS.STATEBG), getcol(DS.PARTS.STATETEXT),groupcomment);
                }
                else
                {
                    d.state_drect         = SS.Mask(style,SS.PARTS.STATE)     ? create_state_drect(state,si.state_cell,si.state_cmt_cell, getcol(DS.PARTS.STATEFRAME), getcol(DS.PARTS.STATEBG), getcol(DS.PARTS.STATETEXT))   : null;
                    w = d.state_drect.width;
                    
                    d.statecmt_drect      = SS.Mask(style,SS.PARTS.STATE_CMT) ? create_statecmt_drect(si.state_cmt_cell,w, getcol(DS.PARTS.STATEFRAME), getcol(DS.PARTS.STATEBG), getcol(DS.PARTS.STATETEXT))   : null;
                }

                d.thumbnail_drect     = SS.Mask(style,SS.PARTS.THUMBNAIL) ? create_thumbnail_drect(si.thumbnail_bmp,si.content_cell==null,w,  getcol(DS.PARTS.CONTENTFRAME), getcol(DS.PARTS.CONTENTBG)) : null;
                d.content_drect       = SS.Mask(style,SS.PARTS.CONTENT)   ? create_content_drect(si.content_cell,si.thumbnail_bmp==null,w  ,  getcol(DS.PARTS.CONTENTFRAME), getcol(DS.PARTS.CONTENTBG), getcol(DS.PARTS.CONTENTTEXT) ) : null;
                d.gosub_drect         = SS.Mask(style,SS.PARTS.BRANCH)    ? create_gosub_drect(si.gosub_cell ,true,w  ,  getcol(DS.PARTS.GOSUBFRAME), getcol(DS.PARTS.GOSUBBG), getcol(DS.PARTS.GOSUBTEXT), si.state_typ == WordStorage.Store.state_typ_gosub ) : null;
                d.num_of_branches     = SS.Mask(style,SS.PARTS.BRANCH)    ? si.num_of_branches                                               : 0;
                d.branch_drect_list   = new DrawRect[d.num_of_branches];
                for(var i = 0; i< d.num_of_branches; i++)
                {
                    d.branch_drect_list[i] = create_branch_drect(si.branch_list[i],w, SS.Mask(style,SS.PARTS.MULTIBRANCH), getcol(DS.PARTS.BRANCHFRAME), getcol(DS.PARTS.BRANCHBG), getcol(DS.PARTS.BRANCHTEXT), style == SS.STYLE.FORGROUP);
                }

                // 配置

                d.frame_pos = PADDING;
                d.state_pos = PointUtil.Add_XY(d.frame_pos,LINEWIDTH + GAP, LINEWIDTH + GAP);
                var prev_drect = d.state_drect;
                var prev_pos   = d.state_pos;

                if (d.statecmt_drect!=null)
                {
                    d.statecmt_pos =  PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                    prev_drect = d.statecmt_drect;
                    prev_pos   = d.statecmt_pos;
                }

                if (d.thumbnail_drect!=null)
                {
                    d.thumbnail_pos = PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                    prev_drect = d.thumbnail_drect;
                    prev_pos   = d.thumbnail_pos;
                }
                if (d.content_drect!=null)
                {
                    d.content_pos = PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                    prev_drect = d.content_drect;
                    prev_pos   = d.content_pos;
                }
                if (d.gosub_drect != null)
                {
                    d.gosub_pos = PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                    prev_drect = d.gosub_drect;
                    prev_pos   = d.gosub_pos;
                }
                if (d.branch_drect_list!=null)
                {
                    d.branch_pos_list     = new PointF[d.num_of_branches];

                    if (d.num_of_branches>0)
                    {
                        if (SS.Mask(style,SS.PARTS.MULTIBRANCH))
                        {
                            for(var i = 0; i<d.num_of_branches; i++)
                            {
                                d.branch_pos_list[i] = PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                                prev_drect = d.branch_drect_list[i];
                                prev_pos   = d.branch_pos_list[i];
                            }
                        }
                        else //ブランチをシングルに ※グループ表示用
                        {
                            d.branch_pos_list[0] = PointUtil.Add_Y(prev_pos,prev_drect.height + GAP);
                            prev_drect = d.branch_drect_list[0];
                            prev_pos   = d.branch_pos_list[0];
                            for(var i = 1; i<d.num_of_branches; i++)
                            {
                                d.branch_pos_list[i] = d.branch_pos_list[0];
                            }
                        }
                    }
                }

                //入出力ポイント
                d.input_dcircle       = create_input_dcircle(d,  out d.input_pos,  getcol(DS.PARTS.SRCPOFRAME), getcol(DS.PARTS.SRCPOBG) );
                d.output_dcircle      = create_output_dcircle(d, out d.output_pos, getcol(DS.PARTS.DSTPOFRAME), getcol(DS.PARTS.DSTPOBG) );

                //gosub出力ポイント
                if (d.gosub_drect!=null)
                {
                    d.gsout_dcircle   = create_gsout_dcircle(d, out d.gsout_pos, getcol(DS.PARTS.DSTPOFRAME), getcol(DS.PARTS.DSTPOBG) );
                }

                //出力ピンの表示制御　分岐がない場合かつ値がなければ透明
                if (si.num_of_branches!=0)
                {
                    if (!G.force_display_outpin)
                    {
                        if (string.IsNullOrEmpty(si.nextstate))
                        {
                            d.output_dcircle.bgcolor = Color.Transparent;
                            d.output_dcircle.linecolor = Color.Transparent;
                        }
                    }
                }

                // typ==start時入力ピンは透明
                if (si.state_typ==WordStorage.Store.state_typ_start/*"start"*/)
                {
                    d.input_dcircle.bgcolor = Color.Transparent;
                    d.input_dcircle.linecolor = Color.Transparent;
                }


                // typ==end || subreturn || stop 時出力ピンは透明
                if (si.state_typ==WordStorage.Store.state_typ_end || si.state_typ ==WordStorage.Store.state_typ_subreturn || si.state_typ == WordStorage.Store.state_typ_stop)
                {
                    d.output_dcircle.bgcolor = Color.Transparent;
                    d.output_dcircle.linecolor = Color.Transparent;
                }



                //state以外は、入出力ポイントを強制透明
                //if (!string.IsNullOrEmpty(si.state) && si.state[0]!='S')
                if (
                    !string.IsNullOrEmpty(si.state) &&                           // 先頭がＳ 以外で　かつ AltStateではない場合 
                    (
                        si.state[0]!='S'
                        &&
                        !AltState.IsAltState(si.state)
                    ))
                {
                    d.input_dcircle.bgcolor = Color.Transparent;
                    d.input_dcircle.linecolor = Color.Transparent;

                    d.output_dcircle.bgcolor = Color.Transparent;
                    d.output_dcircle.linecolor = Color.Transparent;

                    //gostate
                    if (d.gsout_dcircle!=null)
                    { 
                        d.gsout_dcircle.bgcolor = Color.Transparent;
                        d.gsout_dcircle.linecolor = Color.Transparent;
                    }
                }


                if (d.num_of_branches>0)
                {
                    d.bout_dcircle_list   = new DrawCircle[d.num_of_branches];
                    d.bout_pos            = new PointF[d.num_of_branches];
                    if (SS.Mask(style,SS.PARTS.MULTIBRANCH))
                    {
                        for(var i = 0; i< d.num_of_branches; i++)
                        {
                            d.bout_dcircle_list[i] = create_bout_dcircle(d,i,out d.bout_pos[i]);
                        }
                    }
                    else
                    {   //グループ時、outputと同じ位置にする。
                        for(var i = 0; i< d.num_of_branches; i++)
                        {
                            d.bout_dcircle_list[i] = create_bout_dcircle_for_group(d.output_pos,out d.bout_pos[i]);
                        }
                    }
                }

                //外枠
                var frame_y = prev_pos.Y + prev_drect.height + GAP + LINEWIDTH;
                var frame_height = frame_y - d.frame_pos.Y;
                var frame_width  = LINEWIDTH + GAP + w + GAP + LINEWIDTH;

                d.frame_drect = create_boundframe_drect(frame_width,frame_height,getcol(DS.PARTS.BOUNDFRAME), getcol(DS.PARTS.BOUNDBG) );

                //大外枠
                var outframe_x = d.frame_pos.X-G.PADDING.X;
                var outframe_y = d.frame_pos.Y-G.PADDING.Y;
                var outframe_width  = d.frame_drect.width  + G.PADDING.X * 2;
                var outframe_height = d.frame_drect.height + G.PADDING.Y * 2;
                d.outframe_pos = new PointF(outframe_x,outframe_y);
                d.outframe_drect = create_outframe_drect(outframe_width,outframe_height);

                m_draw_state_cache[state] = d;

                //矢印の準備
                d.output_arrow             = new DrawArrow();
                d.output_arrow_color       = Color.White;
                d.output_arrow_focus_Color = Color.Red;

                d.gsout_arrow              = new DrawArrow();
                d.gsout_arrow_color        = Color.White;
                d.gsout_arrow_focus_Color  = Color.Red;
                if (d.num_of_branches>0)
                {
                    d.bout_arrow_list             = new DrawArrow[d.num_of_branches];
                    d.bout_arrow_color_list       = new Color[d.num_of_branches];
                    d.bout_arrow_focus_color_list = new Color[d.num_of_branches];

                    for(var i = 0; i<d.bout_arrow_list.Length; i++)
                    {
                        d.bout_arrow_list[i]             = new DrawArrow();
                        d.bout_arrow_color_list[i]       = Color.White;
                        d.bout_arrow_focus_color_list[i] = Color.Red;
                    }
                }

                //出力先情報
                d.nextstate = si.nextstate;
                d.gosubstate = si.gosub_cell!=null ? si.gosub_cell.text : null;
                if (d.num_of_branches>0)
                {
                    d.bout_value_list = new string[d.num_of_branches];
                    d.bout_state_list = new string[d.num_of_branches];
                    for(var i = 0; i<d.num_of_branches; i++)
                    {
                        if (ListUtil.IsValidIndex(si.branch_list,i))
                        {
                            d.bout_value_list[i] = si.branch_list[i].value;
                            d.bout_state_list[i] = si.branch_list[i].nextstate;
                        }
                    }
                }

                //飾り 2019.10
                d.decoimage = create_deco(si.state_typ,si.state_dco);

            }
        }

        internal DrawStateData draw_statebox(string state, PointF pos, bool bLocal=false)
        {
            //キャッシュ読み取り
            DrawStateData d  = (DrawStateData)m_draw_state_cache[state];

            //描画
            if (bLocal)
            {
                d.draw_local(pos,m_drawlistMain,DWPRI_STATE);
            }
            else
            {
                d.draw(m_drawlistMain,DWPRI_STATE,pos);
            }
            return d;
        }

        internal void draw_focus(string state,Color col)
        {
            DrawStateData d  = null;
            if (state!=null &&  m_draw_state_cache.ContainsKey(state))
            { 
                d = (DrawStateData)m_draw_state_cache[state];
            }
            if (d!=null)
            {
                d.draw_focus(m_drawlistFocus,DWPRI_STATE,col);
            }
        }

        internal string draw_focus_groupnode(string groupnode, Color col) //参照したステート名を返す ※altstate対応へ 2018.6.23
        {
            var altstate = AltState.MakeAltStateName(groupnode);
            DrawStateData d  = (DrawStateData)m_draw_state_cache[altstate];
            if (d!=null)
            {
                d.draw_focus(m_drawlistFocus,DWPRI_STATE,col);
            }
            return altstate;
        }

        internal void draw_arrow_focus_groupnode(string groupnode)
        {
            //var states = G.node_get_allstates_on_groupnode(groupnode);
            //if (states == null) return;
            //foreach(var s in states)
            //{
            //    draw_arrow_focus(s);
            //}
            var state = AltState.MakeAltStateName(groupnode);
            draw_arrow_focus(state);
        }
#if obs
        internal void create_draw_arrow_data(string state)
        {
            if (m_draw_state_cache==null  || !m_draw_state_cache.ContainsKey(state) ) return;

            var d = (DrawStateData)m_draw_state_cache[state];

            Func<RectangleF,PointF> gtctr = (r)=> {
                var x = r.Location.X + G.POINTDIAMETER * 0.5f;
                var y = r.Location.Y + G.POINTDIAMETER * 0.5f;
                return new PointF(x,y);
            };

            try {
                if (d.nextstate_data!=null && d.nextstate_data.wp_input_dcircle!=null)
                {
                    d.output_arrow.list        = ArrowFlowUtil.Create(d,d.nextstate_data,null, gtctr(d.wp_output_dcircle),gtctr(d.nextstate_data.wp_input_dcircle));
                    d.output_arrow_color       = G.config_line_data.GetColor(d.nextstate,false);
                    d.output_arrow_focus_Color = G.config_line_data.GetColor(d.nextstate,true);
                }
            } catch { }

            for(var i = 0; i<d.num_of_branches; i++)
            {
                try {
                    if (d.bout_state_list[i]!=null&& d.bout_state_data_list[i].wp_input_dcircle!=null)
                    {
                        d.bout_arrow_list[i].list        = ArrowFlowUtil.Create(d,d.bout_state_data_list[i],i,gtctr(d.wp_bout_dcircle_list[i]) , gtctr(d.bout_state_data_list[i].wp_input_dcircle) );
                        d.bout_arrow_color_list[i]       = G.config_line_data.GetColor(d.bout_value_list[i],false);
                        d.bout_arrow_focus_color_list[i] = G.config_line_data.GetColor(d.bout_value_list[i],true);
                    }
                }
                catch { }
            }
        }
#else
        //矢印用キャッシュ
        private List<RectangleF> m_arrow_start_goal_cache;
        internal void arrow_start_goal_cache_create()
        {
            m_arrow_start_goal_cache = new List<RectangleF>();
        }
        internal void arrow_start_goal_cache_destroy()
        {
            m_arrow_start_goal_cache = null;
        }
        
        private bool arrow_start_goal_cache_check_if_exist(PointF start, PointF goal)
        {
            var findrect = RectangleUtil.MakeRectangle(Point.Truncate(start), Point.Truncate(goal));
            if (m_arrow_start_goal_cache.Contains(findrect))
            {
                return true; //存在
            }

            //格納して 非存在を知らせる
            m_arrow_start_goal_cache.Add(findrect);  
            return false;
        }

        //描画
        internal void create_draw_arrow_data(string state)
        {
            if (m_draw_state_cache==null  || !m_draw_state_cache.ContainsKey(state) ) return;


            Func<string,bool> isGroup = (s)=> {
                return G.node_get_style(s) == SS.STYLE.FORGROUP;
            };

            Func<string,string,bool> isSameGroup = (a,b)=>
            {
                return G.node_get_style(a) == SS.STYLE.FORGROUP
                       &&
                       G.node_get_style(b) == SS.STYLE.FORGROUP
                       &&
                       G.node_get_groupname(a) == G.node_get_groupname(b);
            };

            var d = (DrawStateData)m_draw_state_cache[state];

            Func<RectangleF,PointF> gtctr = (r)=> {
                var x = r.Location.X + G.POINTDIAMETER * 0.5f;
                var y = r.Location.Y + G.POINTDIAMETER * 0.5f;
                return new PointF(x,y);
            };

            try {
                if (d.nextstate_data!=null && d.nextstate_data.wp_input_dcircle!=null)
                {
                    if (isSameGroup(state, d.nextstate)) {
                        //描画しない
                    }
                    else {
                        var start = gtctr(d.wp_output_dcircle);
                        var goal = gtctr(d.nextstate_data.wp_input_dcircle);
                        if (!arrow_start_goal_cache_check_if_exist(start,goal))
                        { 
                            d.output_arrow.list        = ArrowFlowUtil.Create(d,d.nextstate_data,null, start,goal);
                            d.output_arrow_color       = G.line_color.GetColor(d.nextstate,false);
                            d.output_arrow_focus_Color = G.line_color.GetColor(d.nextstate,true);
                        }
                    }
                }
            } catch { }

            try {
                if (d.gsout_data!=null && d.gsout_data.wp_input_dcircle!=null)
                {
                    if (isSameGroup(state, d.gosubstate)) {
                        //描画しない
                    }
                    else {
                        var start = gtctr(d.wp_gsout_dcircle);
                        var goal  = gtctr(d.gsout_data.wp_input_dcircle);
                        if (!arrow_start_goal_cache_check_if_exist(start,goal))
                        { 
                            d.gsout_arrow.list        = ArrowFlowUtil.Create(d,d.gsout_data,null, start, goal);
                            d.gsout_arrow_color       = G.line_color.GetColor(d.gosubstate,false);
                            d.gsout_arrow_focus_Color = G.line_color.GetColor(d.gosubstate,true);
                        }
                    }
                }
            } catch { }

            for(var i = 0; i<d.num_of_branches; i++)
            {
                try {
                    if (d.bout_state_list[i]!=null&& d.bout_state_data_list[i].wp_input_dcircle!=null)
                    {
                        if (isSameGroup(state,d.bout_state_list[i])) {
                            //描画しない
                        }
                        else
                        {
                            var start = gtctr(d.wp_bout_dcircle_list[i]);
                            var goal  = gtctr(d.bout_state_data_list[i].wp_input_dcircle);
                            if (!arrow_start_goal_cache_check_if_exist(start,goal))
                            { 
                                d.bout_arrow_list[i].list        = ArrowFlowUtil.Create(d,d.bout_state_data_list[i],i,start , goal );
                                d.bout_arrow_color_list[i]       = G.line_color.GetColor(d.bout_value_list[i],false);
                                d.bout_arrow_focus_color_list[i] = G.line_color.GetColor(d.bout_value_list[i],true);
                            }
                        }
                    }
                }
                catch { }
            }
        }

#endif
        internal void draw_arrow(string state)
        {
            //キャッシュ読み取り
            DrawStateData d  = (DrawStateData)m_draw_state_cache[state];
            if (d!=null)
            {
                d.drawArrow(m_drawlistMain,DWPRI_ARROW);
            }
        }
        internal void draw_arrow_focus(string state)
        {
            //キャッシュ読み取り
            if (state!=null && m_draw_state_cache.ContainsKey(state)) {
                DrawStateData d  = (DrawStateData)m_draw_state_cache[state];
                if (d!=null)
                {
                    d.drawArrow(m_drawlistFocus,DWPRI_ARROW,true);
                }
            }
        }
    }

}
