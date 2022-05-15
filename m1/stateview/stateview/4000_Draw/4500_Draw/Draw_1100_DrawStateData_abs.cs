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
            #region world point converted ワールドポイント換算
            internal RectangleF    wp_state_rect {
                get {
                    return new RectangleF(offset.X + state_pos.X, offset.Y + state_pos.Y, state_drect.width, state_drect.height);
                }
            }

            internal RectangleF?   wp_statecmt_rect {
                get {
                    if (statecmt_drect==null) return null;
                    return new RectangleF(offset.X + statecmt_pos.X, offset.Y + statecmt_pos.Y, statecmt_drect.width, statecmt_drect.height);
                }
            }

            internal RectangleF?   wp_thumbnail_rect {
                get {
                    if (thumbnail_drect==null) return null;
                    return new RectangleF(offset.X + thumbnail_pos.X, offset.Y + thumbnail_pos.Y, thumbnail_drect.width, thumbnail_drect.height);
                }
            }

            internal RectangleF?   wp_content_rect {
                get {
                    if (content_drect==null) return null;
                    return new RectangleF(offset.X + content_pos.X, offset.Y + content_pos.Y, content_drect.width, content_drect.height);
                }
            }

            internal RectangleF? wp_gosub_rect {
                get {
                    if (gosub_drect==null) return null;
                    return new RectangleF(offset.X + gosub_pos.X, offset.Y + gosub_pos.Y, gosub_drect.width, gosub_drect.height);
                }
            }

            internal RectangleF[]  wp_branch_rect_list
            {
                get {
                    if (branch_drect_list==null || branch_drect_list.Length==0) return null;
                    var list = new RectangleF[branch_drect_list.Length];

                    for(var i = 0; i<list.Length;i++)
                    {
                        var p  = branch_pos_list[i];
                        var dr = branch_drect_list[i];
                        if (dr==null)
                        {
                            list [i] = default(RectangleF);
                            continue;
                        }
                        list[i] = new RectangleF(offset.X + p.X, offset.Y + p.Y, dr.width, dr.height);
                    }
                    return list;
                }
            }

            internal RectangleF    wp_input_dcircle
            {
                get {
                    var d = input_dcircle.diameter;
                    var r = d * 0.5f;
                    return new RectangleF(offset.X + input_pos.X - r, offset.Y + input_pos.Y - r,  d , d);
                }
            }
            private const float INPOINT_MARGIN_RATIO = 3.0f; //ポイント部分の反応域の拡大率定義
            internal RectangleF    wp_input_dcircle_w_margin //wp_input_dcircleにマージン設定。反応域を拡大するため
            {
                get {
                    var d = input_dcircle.diameter * INPOINT_MARGIN_RATIO;
                    var r = d * 0.5f;
                    return new RectangleF(offset.X + input_pos.X - r, offset.Y + input_pos.Y - r,  d , d);
                }
            }


            private const float OUTPOINT_MARGIN_RATIO = 3.0f; //ポイント部分の反応域の拡大率定義
            internal RectangleF    wp_output_dcircle
            {
                get {
                    var d = output_dcircle.diameter;
                    var r = d * 0.5f;
                    return new RectangleF(offset.X + output_pos.X - r, offset.Y + output_pos.Y - r,  d , d);
                }
            }
            internal RectangleF    wp_output_dcircle_w_margin //wp_output_dcircleにマージン設定。反応域を拡大するため
            {
                get {
                    var d = output_dcircle.diameter * OUTPOINT_MARGIN_RATIO;
                    var r = d * 0.5f; 
                    return new RectangleF(offset.X + output_pos.X - r, offset.Y + output_pos.Y - r,  d , d);
                }
            }


            internal RectangleF wp_gsout_dcircle //gosub用
            {
                get {
                    var d = gsout_dcircle.diameter;
                    var r = d * 0.5f;
                    return new RectangleF(offset.X + output_pos.X - r, offset.Y + gsout_pos.Y - r,  d , d);
                }
            }
            internal RectangleF wp_gsout_dcircle_w_margin //gosub用
            {
                get {
                    var d = gsout_dcircle.diameter * OUTPOINT_MARGIN_RATIO;
                    var r = d * 0.5f;
                    return new RectangleF(offset.X + output_pos.X - r, offset.Y + gsout_pos.Y - r,  d , d);
                }
            }

            internal RectangleF[]   wp_bout_dcircle_list
            {
                get {
                    if (bout_dcircle_list==null || bout_dcircle_list.Length==0) return null;
                    var list = new RectangleF[bout_dcircle_list.Length];
                    for(var i = 0; i<list.Length; i++)
                    {
                        var p = bout_pos[i];
                        var c = bout_dcircle_list[i];
                        var d = c.diameter;
                        var r = d * 0.5f;
                        list[i] = new RectangleF(offset.X + p.X - r, offset.Y + p.Y -r, d,d);
                    }
                    return list;
                }
            }
            internal RectangleF[]   wp_bout_dcircle_w_margin_list //wp_bout_dcircle_listの反応域拡大のため
            {
                get {
                    if (bout_dcircle_list==null || bout_dcircle_list.Length==0) return null;
                    var list = new RectangleF[bout_dcircle_list.Length];
                    for(var i = 0; i<list.Length; i++)
                    {
                        var p = bout_pos[i];
                        var c = bout_dcircle_list[i];
                        var d = c.diameter  * OUTPOINT_MARGIN_RATIO;
                        var rw = d * 0.5f;
                        var rh = ( d > G.state_height ? G.state_height : d) * 0.5f; //高さ部分がG.state_height より大きいとNG
                        list[i] = new RectangleF(offset.X + p.X - rw, offset.Y + p.Y -rh, d,rh*2);
                    }
                    return list;
                }
            }


            internal RectangleF    wp_frame_drect
            {
                get {
                    return new RectangleF(offset.X + frame_pos.X, offset.Y + frame_pos.Y, frame_drect.width, frame_drect.height);
                }
            }

            internal RectangleF    wp_outframe_drect
            {
                get {
                    return new RectangleF(offset.X + outframe_pos.X, offset.Y + outframe_pos.Y, outframe_drect.width, outframe_drect.height);
                }
            }
            #endregion
        }

    }
}
