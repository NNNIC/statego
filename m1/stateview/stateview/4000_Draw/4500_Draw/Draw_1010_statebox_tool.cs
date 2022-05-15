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
        StateInfo get_state_info(string state)
        {
            //return G.state_info_list.Find(i=>i.state == state);
            return G.get_state_info(state);
        }

        //※グループ名対応
        DrawRect create_state_drect(StateInfo.CellData state_cell,StateInfo.CellData statecmt_cell,Color frame_col, Color bg_col, Color text_col, string groupname)
        {
            //以下はグループ表示で利用
            var dr = create_drect(state_cell,G.font_size);
            dr.linecolor_top   = frame_col; //G.stateframe_color;
            dr.linecolor_right = frame_col; //G.stateframe_color;
            dr.linecolor_left  = frame_col; //G.stateframe_color;

            dr.text = groupname;    //グループ名で上書き

            dr.linecolor_bot = bg_col; //G.stateframe_color;

            dr.bgcolor         = bg_col;  // G.statebg_color;
            dr.fontcolor       = text_col;//G.statechar_color;

            //check can be fit
            //SizeF size = new SizeF(dr.width,dr.height);
            //for(var loop = 0; loop <= 2; loop++)
            //{
            //    var textwidth = dr.width - (G.TEXTMARGIN.X * 2);
            //    size = DrawUtil.GetTextSize(G.maingraphs, dr.text + "X",dr.fontname,dr.fontsize,float.MaxValue);
            //    if (size.Width > textwidth )
            //    {
            //        dr.fontsize = (dr.fontsize * textwidth / size.Width);
            //        continue;
            //    }
            //    break;
            //}
            var size =_fit_text(ref dr);

            //make center
            dr.textmargin.Y = (dr.height - size.Height) / 2.0f;

            return dr;

        }
        SizeF _fit_text(ref DrawRect dr)
        {
            SizeF size = new SizeF(dr.width,dr.height);
            for(var loop = 0; loop <= 2; loop++)
            {
                var textwidth = dr.width - (G.TEXTMARGIN.X * 2);
                size = DrawUtil.GetTextSize(G.maingraphs, dr.text + "X",dr.fontname,dr.fontsize,float.MaxValue);
                if (size.Width > textwidth )
                {
                    dr.fontsize = (dr.fontsize * textwidth / size.Width);
                    continue;
                }
                break;
            }
            return size;
        }


        DrawRect create_state_drect(string state, StateInfo.CellData state_cell,StateInfo.CellData statecmt_cell,Color frame_col, Color bg_col, Color text_col)
        {
            var dr = create_drect(state_cell,G.font_size);
            dr.linecolor_top   = frame_col; //G.stateframe_color;
            dr.linecolor_right = frame_col; //G.stateframe_color;
            dr.linecolor_left  = frame_col; //G.stateframe_color;
            dr.linecolor_bot   = frame_col; //G.statebg_color;

            //if (G.option_delete_s_state_string)
            //{
            //    if (dr.text.StartsWith("S_"))
            //    {
            //        dr.text = dr.text.Substring(2);
            //    }
            //}

            if (!string.IsNullOrEmpty(G.excel_program.GetStringWithBasestate(state, "state-ref")))
            {
                dr.fontunderline = true;
            }

            if (statecmt_cell!=null)
            {
                dr.linecolor_bot = bg_col; //G.stateframe_color;
            }

            dr.bgcolor         = bg_col;  // G.statebg_color;
            dr.fontcolor       = text_col;//G.statechar_color;

            //check can be fit
            var size = _fit_text(ref dr);


            //make center
            dr.textmargin.Y = (dr.height - size.Height) / 2.0f;

            return dr;
        }


        //グループ対応
        DrawRect create_statecmt_drect(StateInfo.CellData state_cell,  StateInfo.CellData statecmt_cell, float w, Color frame_col, Color bg_col, Color text_col, string groupcomment)
        {
            StateInfo.CellData cell = statecmt_cell;
            if (cell == null)
            {
                cell = state_cell;
            }
            var fontsize = (G.comment_font_size>0  ?  G.comment_font_size : G.font_size);
            var dr = create_drect(cell,fontsize,w);
            if (dr!=null)
            {
                dr.linecolor_top   = bg_col;    // G.statebg_color;
                dr.linecolor_right = frame_col; // G.stateframe_color;
                dr.linecolor_bot   = frame_col; // G.stateframe_color;
                dr.linecolor_left  = frame_col; // G.stateframe_color;
                dr.bgcolor         = bg_col; // G.statebg_color;
                dr.fontcolor       = text_col;  // G.statechar_color;
                dr.fontsize       -= 1; //そのままだと、なぜかFitしない
                dr.text            = groupcomment;
            }
            return dr;
        }

        DrawRect create_statecmt_drect(StateInfo.CellData statecmt_cell, float w, Color frame_col, Color bg_col, Color text_col)
        {
            if (!G.use_statecmt) return null;
            
            var fontsize = (G.comment_font_size>0  ?  G.comment_font_size : G.font_size);
            var dr = create_drect(statecmt_cell,fontsize, w);
            if (dr!=null)
            {
                dr.linecolor_top   = bg_col;    // G.statebg_color;
                dr.linecolor_right = frame_col; // G.stateframe_color;
                dr.linecolor_bot   = frame_col; // G.stateframe_color;
                dr.linecolor_left  = frame_col; // G.stateframe_color;
                dr.bgcolor         = bg_col; // G.statebg_color;
                dr.fontcolor       = text_col;  // G.statechar_color;
                dr.fontsize       -= 1; //そのままだと、なぜかFitしない
                //dr.fontsize        = (G.comment_font_size>0  ?  G.comment_font_size : G.font_size);

                //dr.text = G.excel_program.GetStringWithBasestate(state, G.STATENAME_statecmt);
            }
            return dr;
        }
        DrawRect create_thumbnail_drect(Bitmap thumbnail_bmp, bool bNeedBottomLine , float w, Color frame_col, Color bg_col)
        {
            if (!G.use_thumbnail) return null;
            if (thumbnail_bmp==null) return null;

            try {

                var dr    = new DrawRect();
                dr.width  = w;
                dr.height = thumbnail_bmp.Height;
                dr.bmp    = thumbnail_bmp;

                dr.linecolor_top   = frame_col;//G.contentsframe_color;
                dr.linecolor_right = frame_col;//G.contentsframe_color;
                dr.linecolor_left  = frame_col;//G.contentsframe_color;
                dr.bgcolor         = bg_col;   //G.contentsbg_color;

                if (bNeedBottomLine)
                {
                    dr.linecolor_bot = frame_col;//G.contentsframe_color;
                }
                return dr;
            } catch (SystemException e)
            {
                G.NoticeToUser_warning(G.Localize("w_faildtodrawthumb")/*"Faild to draw thumbnai. " */+ e.Message);
                return null;
            }
        }
        DrawRect create_content_drect(StateInfo.CellData content_cell, bool bNeedTopLine, float w, Color frame_col, Color bg_col, Color text_col)
        {
            if (!G.use_contents) return null;

            var fontsize        = (G.contents_font_size > 0 ? G.contents_font_size : G.font_size);
            var dr = create_drect(content_cell,fontsize,w);
            if (dr==null) return null;

            //if (G.option_delete_bracket_string)
            //{
            //    dr.text = ContentUtil.Delete_regex_eachline(@"^\[.+?\]",dr.text);
            //}

            if (G.option_delete_thisstring)
            {
                if (!G.option_delete_bracket_string)
                {
                    dr.text = ContentUtil.Delete_regex_eachline(@"(?<=\])this\.",dr.text);
                    dr.text = ContentUtil.Delete_regex_eachline(@"(?<=\])\$this->",dr.text);
                }
                else
                { 
                    dr.text = ContentUtil.Delete_prefix_eachline("this.",dr.text);
                    dr.text = ContentUtil.Delete_prefix_eachline(@"$this->",dr.text);
                }
            }

            dr.height = Math.Min(dr.height,G.content_max_height);

            if (bNeedTopLine)
            {
                dr.linecolor_top = frame_col;// G.contentsframe_color;
            }
            dr.linecolor_right = frame_col;//G.contentsframe_color;
            dr.linecolor_bot   = frame_col;//G.contentsframe_color;
            dr.linecolor_left  = frame_col;//G.contentsframe_color;
            dr.bgcolor         = bg_col;   //G.contentsbg_color;

            dr.fontcolor       = text_col;
            //dr.fontsize       -= 1; //そのままだと、なぜかFitしない
            //dr.fontsize        = (G.contents_font_size > 0 ? G.contents_font_size : G.font_size);


            return dr;
        }

        DrawRect create_gosub_drect(StateInfo.CellData gosub_cell, bool bNeedTopLine, float w, Color frame_col, Color bg_col, Color text_col, bool bForce)
        {
            //if (!G.use_contents) return null;
            var dr = create_drect(gosub_cell,G.font_size, w);
            if (dr==null)
            {
                if (bForce)
                {
                    gosub_cell = new StateInfo.CellData()
                    {
                        text="?",
                        fontsize = G.font_size,
                        fontname = G.font_name,
                        height = G.font_size,
                        width = w,
                        fontcolor = text_col,
                        bgcolor = bg_col
                    };
                    dr = create_drect(gosub_cell,G.font_size, w);
                }
                else
                {
                    return null;
                }
            }

            //dr.height = Math.Min(dr.height,G.content_max_height);

            dr.linecolor_right = frame_col;//G.contentsframe_color;

            dr.linecolor_bot   = frame_col;//G.contentsframe_color;
            dr.linecolor_left  = frame_col;//G.contentsframe_color;
            dr.bgcolor         = bg_col;   //G.contentsbg_color;

            dr.fontcolor       = text_col;

            //dr.fontsize       -= 1; //そのままだと、なぜかFitしない

            //dr.text = "gosub " +  (G.option_delete_s_state_string && dr.text.StartsWith("S_") ?  dr.text.Substring(2) : dr.text) ;
            dr.text = "Gosub";
            var size = _fit_text(ref dr);

            dr.height = gosub_cell.fontsize*1.5f;
            dr.textmargin.Y = (dr.height - size.Height) / 2.0f;

            return dr;
        }


        DrawRect create_branch_drect(StateInfo.BranchItem branch_item, float w, bool bDrawText , Color frame_col, Color bg_col, Color text_col, bool bShortHeight  = false )
        {
            var dr = create_drect(branch_item.cell,G.font_size,w,bShortHeight);

            if (dr==null) dr = new DrawRect();

            dr.linecolor_top   = frame_col;//G.branchframe_color;
            dr.linecolor_right = frame_col;//G.branchframe_color;
            dr.linecolor_bot   = frame_col;//G.branchframe_color;
            dr.linecolor_left  = frame_col;//G.branchframe_color;
            dr.bgcolor         = bg_col;

            dr.text = bDrawText ?  branch_item.disp : "";
            if (G.option_delete_br_string)
            {
                if (dr.text.StartsWith("br_"))
                {
                    dr.text = dr.text.Substring(3);
                }
                else if (dr.text.StartsWith("if"))
                {
                    var s = dr.text.Substring(4);
                    if (!string.IsNullOrEmpty(s))
                    {
                        s = s.Substring(0,s.Length-1);
                    }
                    dr.text = s;
                }
                else if (dr.text.StartsWith("elif"))
                {
                    var s = dr.text.Substring(6);
                    if (!string.IsNullOrEmpty(s))
                    {
                        s = s.Substring(0, s.Length - 1);
                    }
                    dr.text = s;
                }
            }

            dr.fontcolor      = text_col;

            var size =_fit_text(ref dr);

            //make center
            dr.textmargin.Y = (dr.height - size.Height) / 2.0f;

            return dr;
        }

        //[Obsolete]
        //DrawRect create_frame_drect(float width, float height)
        //{
        //    var dr = new DrawRect();
        //    dr.width = width;
        //    dr.height = height;

        //    dr.linecolor_top   = G.outframe_color;
        //    dr.linecolor_right = G.outframe_color;
        //    dr.linecolor_bot   = G.outframe_color;
        //    dr.linecolor_left  = G.outframe_color;
        //    dr.bgcolor         = G.outframebg_color;

        //    return dr;
        //}

        DrawRect create_boundframe_drect(float width, float height,Color frame_color, Color bg_color)
        {
            var dr = new DrawRect();
            dr.width = width;
            dr.height = height;

            dr.linecolor_top   = frame_color;
            dr.linecolor_right = frame_color;
            dr.linecolor_bot   = frame_color;
            dr.linecolor_left  = frame_color;
            dr.bgcolor         = bg_color;

            return dr;
        }

        DrawRect create_outframe_drect(float width, float height)
        {
            var dr = new DrawRect();
            dr.width = width;
            dr.height = height;

            dr.linecolor_top   = Color.Transparent;
            dr.linecolor_right = Color.Transparent;
            dr.linecolor_bot   = Color.Transparent;
            dr.linecolor_left  = Color.Transparent;
            dr.bgcolor         = Color.Transparent;

            return dr;
        }

        DrawCircle create_input_dcircle(DrawStateData d, out PointF inputpos, Color frame_col, Color bg_col)
        {
            var x = d.state_pos.X - G.FRAMELINESIZE * 2 - G.POINTDIAMETER * 0.5f;
            var h = d.state_drect.height;/* + (d.statecmt_drect!=null ? d.statecmt_drect.height : 0);*/
            var y = d.state_pos.Y + h * 0.5f;

            inputpos = new PointF(x,y);

            var dc = new DrawCircle();
            dc.diameter 　= G.POINTDIAMETER;
            dc.linecolor  = frame_col; //G.inputpoframe_color;
            dc.bgcolor    = bg_col; //G.inputpo_color;
            dc.linewidth  = G.FRAMELINESIZE;

            return dc;
        }
        DrawCircle create_output_dcircle(DrawStateData d, out PointF outputpos, Color frame_col, Color bg_col)
        {
            var x = d.state_pos.X + d.state_drect.width + G.FRAMELINESIZE * 2 + G.POINTDIAMETER * 0.5f;
            var h = d.state_drect.height; /* + (d.statecmt_drect!=null ? d.statecmt_drect.height : 0);*/
            var y = d.state_pos.Y + h * 0.5f;

            outputpos = new PointF(x,y);

            var dc       = new DrawCircle();
            dc.diameter  = G.POINTDIAMETER;
            dc.linecolor = frame_col; // G.outputpoframe_color;
            dc.bgcolor   = bg_col;    // G.outputpo_color;
            dc.linewidth = G.FRAMELINESIZE;

            return dc;
        }

        DrawCircle create_gsout_dcircle(DrawStateData d, out PointF outputpos, Color frame_col, Color bg_col)
        {
            var x = d.gosub_pos.X + d.gosub_drect.width + G.FRAMELINESIZE * 2 + G.POINTDIAMETER * 0.5f;
            var h = d.gosub_drect.height;
            var y = d.gosub_pos.Y + h * 0.5f;

            outputpos = new PointF(x,y);

            var dc       = new DrawCircle();
            dc.diameter  = G.POINTDIAMETER;
            dc.linecolor = frame_col; // G.outputpoframe_color;
            dc.bgcolor   = bg_col;    // G.outputpo_color;
            dc.linewidth = G.FRAMELINESIZE;

            return dc;
        }

        DrawCircle create_bout_dcircle(DrawStateData d,int n, out PointF boutpos)
        {
            var dr = d.branch_drect_list[n];
            var dp = d.branch_pos_list[n];
            var  x = dp.X + dr.width + G.FRAMELINESIZE * 2 + G.POINTDIAMETER * 0.5f;
            var  y = dp.Y + dr.height * 0.5f;

            boutpos = new PointF(x,y);

            var dc       = new DrawCircle();
            dc.diameter  = G.POINTDIAMETER;
            dc.linecolor = G.outputpoframe_color;
            dc.bgcolor   = G.outputpo_color;
            dc.linewidth = G.FRAMELINESIZE;

            return dc;
        }

        DrawCircle create_bout_dcircle_for_group(PointF refpos,out PointF boutpos)
        {
            boutpos = refpos;

            var dc       = new DrawCircle();
            dc.diameter  = G.POINTDIAMETER;
            dc.linecolor = G.outputpoframe_color;
            dc.bgcolor   = G.outputpo_color;
            dc.linewidth = G.FRAMELINESIZE;

            return dc;
        }



        //===
        DrawRect create_drect(StateInfo.CellData cell,float fsize, float w, bool bShortHeight=false)
        {
            if (cell!=null && !string.IsNullOrEmpty(cell.text))
            {
                var dr = create_drect(cell, fsize, bShortHeight);
                dr.width  = w;
                return dr;
            }
            return null;
        }
        DrawRect create_drect(StateInfo.CellData cell, float fsize,  bool bShortHeight=false)
        {
            var dr = new DrawRect();

            var width  = cell.width;
            var height = bShortHeight ? 1 : cell.height;


            dr.width           = MathX.Clamp(width, 30f, 500f);  //MathX.Clamp( cell.width * (96f / 72f), 50f, 500f);
            dr.height          = height; //  height * (96f / 72f);//*(100f/72f);

            dr.linecolor_top   = cell.bgcolor;
            dr.linecolor_right = cell.bgcolor;
            dr.linecolor_bot   = cell.bgcolor;
            dr.linecolor_left  = cell.bgcolor;

            dr.bgcolor         = cell.bgcolor;

            dr.text            = cell.text;

            dr.fontname        = cell.fontname;
            dr.fontsize        = fsize; //cell.fontsize;
            dr.fontcolor       = cell.fontcolor;
            return dr;
        }

        DrawDeco create_deco(string typ,string dco)
        {
            var dr = new DrawDeco();
            dr.typ = DecoImage.GetTypImageData(typ);
            dr.dco = null; //to do!


            return dr;
        }

    }
}
