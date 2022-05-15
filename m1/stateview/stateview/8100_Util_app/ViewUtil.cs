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
    public class ViewUtil
    {
        /// <summary>
        /// 表示域のRectangle取得
        /// </summary>
        public static Rectangle GetViewRectangle()
        {
            var view_form = G.view_form;
            var panel = G.view_form.panel1;

            var pb = G.main_picturebox;
            var bmp = G.mainbitmap;

            var s  = G.scale;
            var ds = 1f / s;

            double left = -((double)pb.Location.X) * ds;
            double top = -((double)pb.Location.Y) * ds;
            double right = panel.Right;
            double bot = panel.Bottom;
            //if (pb.Location.X < 0)
            //{
            //    left = -((double)pb.Location.X) * ds;
            //}
            //if (pb.Location.Y < 0)
            //{
            //    top = -((double)pb.Location.Y) * ds;
            //}

            double right_r = bmp.Width - left;      　 //ビットマップの実端までPBの位置
            double right_v = left +  panel.Width * ds;//PB内で表示可能なビットマップ位置
            right = Math.Min(right_r,right_v);

            double bot_r = bmp.Height - top;          //ビットマップの実端までのPB位置
            double bot_v = top + panel.Height * ds;  //PB内で表示可能なビットマップ位置
            bot = Math.Min(bot_r,bot_v);

            return new Rectangle((int)left,(int)top,(int)(right - left), (int)(bot-top));
        }
        /// <summary>
        /// 表示域のRectangle取得、但しbitmapのリアル制限なし。  panel1で表示されるWidth,Heightを得るため
        /// </summary>
        public static Rectangle GetViewRectangle_wo_limit()
        {
            var view_form = G.view_form;
            var panel = G.view_form.panel1;

            var pb = G.main_picturebox;
            var bmp = G.mainbitmap;

            var s  = G.scale;
            var ds = 1f / s;

            double left = -((double)pb.Location.X) * ds;
            double top  = -((double)pb.Location.Y) * ds;
            double right = panel.Right;
            double bot = panel.Bottom;

            double right_r = bmp.Width - left;      　//ビットマップの実端までPBの位置
            double right_v = left +  panel.Width * ds;//PB内で表示可能なビットマップ位置

            double bot_r = bmp.Height - top;         //ビットマップの実端までのPB位置
            double bot_v = top + panel.Height * ds;  //PB内で表示可能なビットマップ位置

            return new Rectangle((int)left,(int)top,(int)(right_v - left), (int)(bot_v-top));
        }
#if obs
        public static Rectangle GetViewRectangle_wo_limit()
        {
            try {
                var view_form = G.view_form;
                var panel = G.view_form.panel1;

                var pb = G.main_picturebox;
                var bmp = G.mainbitmap;

                var s  = G.scale;
                var ds = 1f / s;

                double left = 0f;
                double top = 0f;
                double right = panel.Right;
                double bot = panel.Bottom;
                if (pb.Location.X < 0)
                {
                    left = -((double)pb.Location.X) * ds;
                }
                if (pb.Location.Y < 0)
                {
                    top = -((double)pb.Location.Y) * ds;
                }

                double right_r = bmp.Width - left;      　 //ビットマップの実端までPBの位置
                double right_v = left +  panel.Width * ds;//PB内で表示可能なビットマップ位置

                double bot_r = bmp.Height - top;          //ビットマップの実端までのPB位置
                double bot_v = top + panel.Height * ds;  //PB内で表示可能なビットマップ位置

                return new Rectangle((int)left,(int)top,(int)(right_v - left), (int)(bot_v-top));
            }
            catch {
                return new Rectangle();
            }

        }
#endif

        /// <summary>
        /// 表示域の左端を指定 x,yはビットマップ位置
        /// </summary>
        public static void SetViewTopLeft(double x, double y)
        {
            G.scroll.SetViewTopLeft(x,y);

            //G.scroll.SetViewTopLeft(x,y);

            //var panel = G.view_form.panel1;
            //var pb = G.main_picturebox;
            //double s  = G.scale;
            //double ds = 1f / s;

            //var lx = (-x) * s;
            //var ly = (-y) * s;
            
            ////pb.Location= new Point((int)lx,(int)ly);
            //panel.AutoScrollPosition = new Point(-(int)lx,-(int)ly);
        }

        /*
        SetScrollTopLeft_at_0to1(x,y)
        x,yは0～1に正規化した値
        トップレフトを指定した値にする。
        */
        public static void SetScrollTopLeft_at_0to1(double rx, double ry)
        {
            G.scroll.SetScrollTopLeft_at_0to1(rx,ry);
        }

        public static Point GetViewTopLeft()
        {
            var rect = GetViewRectangle();
            return new Point(rect.X,rect.Y);
        }

        public static void SetViewCenter(double x, double y)
        {
            G.scroll.SetViewCenter(x,y);

            //var rect_wo_limit = GetViewRectangle_wo_limit();
            //double left = x - rect_wo_limit.Width  * 0.5f;
            //double top  = y - rect_wo_limit.Height * 0.5f;
            //SetViewTopLeft(left,top);
        }


        /// <summary>
        ///  (0,0)に最も近いステート　（注：ALTステートあり）
        /// </summary>
        public static string GetNearestState(string regex=null)
        {
            if (G.state_working_list==null) return null;
            string nearest_state    = null;
            double nearest_squarlen = double.MaxValue;

            foreach(var s in G.state_working_list)
            {
                if (regex!=null && !RegexUtil.IsMatch(regex,s))
                {
                    continue;
                }

                var rectx = G.get_draw_wp_outframe(s);
                if (rectx==null) continue;
                var rect = (RectangleF)rectx;
                var sqlen = rect.Top * rect.Top + rect.Left * rect.Left;
                if (sqlen < nearest_squarlen)
                {
                    nearest_state = s;
                    nearest_squarlen = sqlen;
                } 
            }
            return nearest_state;
        }
        /// <summary>
        /// (0,0)に最も遠いステート　（注：ALTステートあり）
        /// </summary>
        public static string GetFarestState(string regex = null)
        {
            if (G.state_working_list==null) return null;
            string farest_state    = null;
            double farest_squarlen = 0;

            foreach(var s in G.state_working_list)
            {
                if (regex!=null && !RegexUtil.IsMatch(regex,s))
                {
                    continue;
                }
                var rectx = G.get_draw_wp_outframe(s);
                if (rectx==null) continue;
                var rect = (RectangleF)rectx;
                var sqlen = rect.Top * rect.Top + rect.Left * rect.Left;
                if (sqlen > farest_squarlen)
                {
                    farest_state = s;
                    farest_squarlen = sqlen;
                } 
            }
            return farest_state;
        }

        /// <summary>
        /// ポインタをステート上に置く
        /// </summary>
        public static void SetPointerOnState(string s)
        {
            if (G.vf_sc==null) return;

            var rx = G.get_draw_wp_frame(s);
            if (rx == null)  return;
            var r = (RectangleF)rx;
            var fontheight = G.font_size;

            var pos = PointUtil.Add_XY(r.Location, r.Width  * 0.5f, fontheight * 0.6f);
            var pos_onsc = G.vf_sc.GetScreenPosFormPointOnImage(pos);

            Cursor.Position = Point.Truncate(pos_onsc);

        }

        /// <summary>
        /// ブランチ上にポインタを置く
        /// </summary>
        public static void SetPointerOnBranch(bool in_or_out, InOutBaseData item)
        {
            var state = item.target_state;
            if (in_or_out)//inflow
            {
                var dd = G.get_draw_data(state);
                if (dd == null) return;
                var r = dd.wp_input_dcircle;
                var pos = RectangleUtil.Center(r);
                var pos_onsc = G.vf_sc.GetScreenPosFormPointOnImage(pos);
                Cursor.Position = Point.Truncate(pos_onsc);
                return;
            }
            //outflow
            if (item.attrib == InOutBaseData.ATTRIB.nextstate)
            {
                var dd = G.get_draw_data(state);
                if (dd == null) return;
                var r = dd.wp_output_dcircle;
                var pos = RectangleUtil.Center(r);
                var pos_onsc = G.vf_sc.GetScreenPosFormPointOnImage(pos);
                Cursor.Position = Point.Truncate(pos_onsc);
                return;
            } 
            if (item.attrib ==  InOutBaseData.ATTRIB.gosub)
            {
                var dd = G.get_draw_data(state);
                if (dd == null) return;
                var r = dd.wp_gsout_dcircle;
                var pos = RectangleUtil.Center(r);
                var pos_onsc = G.vf_sc.GetScreenPosFormPointOnImage(pos);
                Cursor.Position = Point.Truncate(pos_onsc);
                return;
            }
            if (item.attrib == InOutBaseData.ATTRIB.branch)
            {
                var dd = G.get_draw_data(state);
                if (dd == null) return;
                var index = item.branch_index;
                if (dd.num_of_branches > index)
                {
                    var r = dd.wp_bout_dcircle_list[index];
                    var pos = RectangleUtil.Center(r);
                    var pos_onsc = G.vf_sc.GetScreenPosFormPointOnImage(pos);
                    Cursor.Position = Point.Truncate(pos_onsc);
                    return;
                }
            }
            return;
        }
        public static bool CheckInView(string state)
        {
            var rect = GetViewRectangle();
            var dd = G.get_draw_data(state);
            if (dd==null) return false;
            var state_rect = new Rectangle((int)dd.offset.X,(int)dd.offset.Y, (int)G.state_width, (int)G.font_size);
            return rect.Contains(state_rect);
        }
        public static void SetInViewIfOut(string state)
        {
            if (CheckInView(state)) return;
            var rect = GetViewRectangle();
            var dd = G.get_draw_data(state);
            if (dd==null) return;
            //var state_rect = new Rectangle((int)dd.offset.X,(int)dd.offset.Y, (int)G.state_width, (int)G.font_size);
            
            //var topleft = calc_topleft_if_center(rect, state_rect);
            
            //SetViewTopLeft(topleft.X, topleft.Y);

            G.scroll.SetViewCenter(dd.offset.X,dd.offset.Y);
        }
        private static Point calc_topleft_if_center(Rectangle rect, Rectangle target)
        {
            var target_center = RectangleUtil.Center(target);
            var x = default(double);
            var y = default(double);
            var half_w = (double)(rect.Width - target.Width) * 0.5f;
            if (half_w < 0)
            {
                x = (double)target_center.X - (double)rect.Width * 0.5f;
            }
            else
            {
                x = (double)target.Left - (double)half_w;
            }

            var half_h = (double)(rect.Height - target.Height) * 0.5f;
            if (half_h < 0)
            {
                y = (double)target_center.Y - (double)rect.Height * 0.5f;
            }
            else
            {
                y = (double)target.Top - (double)half_h;
            }

            return new Point((int)x,(int)y);
        }
        public static bool CheckInViewScreenPosition(Point sc_pos)
        {
            var bmp_pos = G.vf_sc.GetPointerOnMainBmp(sc_pos);
            var viewrect = GetViewRectangle();
            return viewrect.Contains( Point.Truncate(bmp_pos) );
        }

        public static PointF GetCenterInView()
        {
            var rect = GetViewRectangle();
            return RectangleUtil.Center(rect);
        }

        public static bool IsBusy()
        {
            return G.vf_sc==null || G.vf_sc.IsBusy();
        }
    }
}
