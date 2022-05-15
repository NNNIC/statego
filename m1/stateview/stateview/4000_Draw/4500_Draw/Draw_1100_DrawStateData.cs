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
        internal partial class DrawStateData
        {
            internal string       state;
            internal string       state_typ; //ステートタイプ 2019.10
            internal int          cell_col;

            internal PointF       offset;

            #region 基本部分
            internal PointF       state_pos;
            internal DrawRect     state_drect;

            internal PointF       statecmt_pos;
            internal DrawRect     statecmt_drect;

            internal PointF       thumbnail_pos;
            internal DrawRect     thumbnail_drect;

            internal PointF       content_pos;
            internal DrawRect     content_drect;

            internal PointF[]     branch_pos_list;
            internal DrawRect[]   branch_drect_list;

            internal PointF       input_pos;
            internal DrawCircle   input_dcircle;

            internal PointF       output_pos;
            internal DrawCircle   output_dcircle;

            internal PointF[]     bout_pos;
            internal DrawCircle[] bout_dcircle_list;

            internal PointF       frame_pos;
            internal DrawRect     frame_drect;

            internal PointF       outframe_pos;
            internal DrawRect     outframe_drect;

            #region gosubstate    //2019.1.4
            internal PointF       gosub_pos;
            internal DrawRect     gosub_drect;

            internal PointF       gsout_pos;      //gosubstate output
            internal DrawCircle   gsout_dcircle;
            #endregion

            #region bmp trp and dco
            internal DrawDeco     decoimage;  //state-typとstate-dcoにもとづく
            #endregion

            internal SizeF        size {  //ステート全体のサイズ　フォーカスで利用
                get {
                    //return new SizeF(frame_drect.width + 2 * G.POINTDIAMETER, frame_drect.height );
                    return new SizeF(outframe_drect.width, outframe_drect.height);
                }
            }

            internal int          num_of_branches;

            internal void         set_offset(PointF pos)
            {
                offset = pos;
            }


            internal void         draw(DrawList dlist, int pri, PointF? i_offset = null)
            {
                if (i_offset!=null)
                {
                    offset = (PointF)i_offset;
                }
                draw_local(offset,dlist,pri);
            }
            internal void  draw_local(PointF pos, DrawList dlist, int pri) // 移動時用描画にも使われる
            {
                //書く順序
                // frame -> state -> thumb -> cont -> branc -> input -> output -> branch cir
                frame_drect.draw( PointUtil.Add_Point(pos,frame_pos), dlist, pri);

                state_drect.draw( PointUtil.Add_Point(pos,state_pos), dlist, pri);

                if (statecmt_drect!=null)  statecmt_drect.draw ( PointUtil.Add_Point(pos,statecmt_pos) , dlist, pri);

                if (thumbnail_drect!=null) thumbnail_drect.draw( PointUtil.Add_Point(pos,thumbnail_pos), dlist, pri);
                if (content_drect!=null)   content_drect.draw  ( PointUtil.Add_Point(pos, content_pos) , dlist, pri);
                if (gosub_drect!=null)     gosub_drect.draw    ( PointUtil.Add_Point(pos, gosub_pos)   , dlist, pri);
                if (branch_drect_list!=null)
                {
                    for(var n = 0; n< num_of_branches; n++)
                    {
                        branch_drect_list[n].draw( PointUtil.Add_Point( pos, branch_pos_list[n] ), dlist, pri);
                    }
                }
                input_dcircle.draw (PointUtil.Add_Point(pos,input_pos) ,dlist,pri);
                output_dcircle.draw(PointUtil.Add_Point(pos,output_pos),dlist,pri);
                if (gosub_drect!=null) gsout_dcircle.draw(PointUtil.Add_Point(pos,gsout_pos),dlist,pri);
                for(var i = 0; i < num_of_branches; i++)
                {
                    bout_dcircle_list[i].draw(PointUtil.Add_Point(pos,bout_pos[i]) ,dlist,pri);
                }

                var abs_frame_pos = PointUtil.Add_Point( pos, frame_pos);
                decoimage.draw( new RectangleF(abs_frame_pos.X,abs_frame_pos.Y,frame_drect.width,frame_drect.height), dlist,pri);

            }
            internal void draw_focus(DrawList dlist, int pri, Color col) //フォーカス描画に利用
            {
                var pos = offset;
                var newframe_pos = outframe_pos;
                var newframe = outframe_drect.clone();
                newframe.bgcolor = col;
                newframe.draw(PointUtil.Add_Point(pos,newframe_pos),dlist,pri);
            }

            #endregion

            #region 矢印  ※offsetに基づいて作成される

            internal DrawArrow    output_arrow;
            internal Color        output_arrow_color;
            internal Color        output_arrow_focus_Color;

            internal DrawArrow    gsout_arrow;
            internal Color        gsout_arrow_color;
            internal Color        gsout_arrow_focus_Color;

            internal DrawArrow[]  bout_arrow_list;
            internal Color    []  bout_arrow_color_list;
            internal Color    []  bout_arrow_focus_color_list;

            //[Obsolete]
            //internal void drawArrow_obs(DrawList dlist, int pri, Color? c= null)
            //{
            //    if (output_arrow!=null)    output_arrow.draw(dlist,pri,c);
            //    if (bout_arrow_list!=null)
            //    {
            //        for(var i = 0; i<num_of_branches; i++)
            //        {
            //            if (bout_arrow_list[i]!=null)
            //            {
            //                bout_arrow_list[i].draw(dlist,pri,c);
            //            }
            //        }
            //    }
            //}
            internal void drawArrow(DrawList dlist, int pri, bool bFocus=false)
            {
                if (output_arrow!=null)    output_arrow.draw(dlist,pri, (bFocus ? output_arrow_focus_Color : output_arrow_color) );
                if (gsout_arrow!=null)     gsout_arrow.draw(dlist,pri,  (bFocus ? gsout_arrow_focus_Color : gsout_arrow_color) );
                if (bout_arrow_list!=null)
                {
                    for(var i = 0; i<num_of_branches; i++)
                    {
                        if (bout_arrow_list[i]!=null)
                        {
                            var c = bFocus ? bout_arrow_focus_color_list[i] : bout_arrow_color_list[i];
                            bout_arrow_list[i].draw(dlist,pri,c);
                        }
                    }
                }
            }

            #endregion
        }

    }
}
