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
    public partial class DrawUtil
    {
        static float ARROWHEAD_WIDTH = G.ARROWHEAD_WIDTH;
        static float ARROWHEAD_HEIGHT = G.ARROWHEAD_HEIGHT;

        internal static void DrawBox_LineAndFill(
            Graphics g,
            PointF   point,
            float    width,
            float    height,
            float    lineWidth,
            Color    lineColor,
            Color    fillColor
        )
        {
            using (var pen = new Pen(fillColor, 1))
            {
                g.FillRectangle(pen.Brush, point.X, point.Y, width, height);
            }

            using (var pen = new Pen(lineColor, lineWidth))
            {
                g.DrawRectangle(pen,point.X,point.Y,width,height);
            }
        }
        internal static void DrawBox_LineAndFill(
            Graphics g,
            PointF   point,
            float    width,
            float    height,
            float    lineWidth,
            Color    lineColorTop,
            Color    lineColorRight,
            Color    lineColorBot,
            Color    lineColorLeft,
            Color    fillColor
        )
        {
            using (var pen = new Pen(fillColor, 1))
            {
                g.FillRectangle(pen.Brush, point.X, point.Y, width, height);
            }

            /*
               a - b
               d - c
            */

            var a = point;
            var b = PointUtil.Add_X(point, width);
            var c = PointUtil.Add_Y(b    , height);
            var d = PointUtil.Add_Y(point, height);

            using (var pen = new Pen(lineColorTop   , lineWidth)) g.DrawLine(pen,a,b );
            using (var pen = new Pen(lineColorRight , lineWidth)) g.DrawLine(pen,b,c );
            using (var pen = new Pen(lineColorBot   , lineWidth)) g.DrawLine(pen,c,d );
            using (var pen = new Pen(lineColorLeft  , lineWidth)) g.DrawLine(pen,d,a );
        }

        public static void DrawText_obs(
            Graphics g,
            string   text,
            string   fontname,
            Color    color,
            float    size,

            RectangleF rect
        )
        {
            if (string.IsNullOrEmpty(text)) return;
            if (string.IsNullOrEmpty(fontname)) return;
            using (var pen = new Pen(color, size))
            using (var font = new Font(fontname,size))
            {
                g.DrawString(ScrambleText.get( text ), font, pen.Brush, rect);
            }
        }
        public static void DrawText(
            Graphics g,
            string   text,
            string   fontname,
            Color    color,
            float    size,
            bool     underline,

            RectangleF rect
        )
        {
            DrawTextMeasureAndDraw(g,text,fontname,color,size,underline,rect);
        }

         public static void DrawBoxText_LineAndFill(
            Graphics g,
            string text,
            string fontname,
            Color  fontcolor,
            float  fontsize,
            bool   fontunderline,

            PointF point,
            float  width,
            float  height,
            float  lineWidth,
            Color  lineColor,
            Color  fillColor
        )
        {
            DrawBox_LineAndFill(g,point,width,height,lineWidth,lineColor,fillColor);

            if (string.IsNullOrEmpty(text)) return;

            var textpoint = PointUtil.Add_Point(point, G.TEXTMARGIN);
            var textwidth = width  - G.TEXTMARGIN.X * 2;
            var textheight= height - G.TEXTMARGIN.Y * 2;
            var rect = new RectangleF(textpoint.X, textpoint.Y,textwidth,textheight);
            DrawText(g,text,fontname,fontcolor,fontsize, fontunderline,rect);
        }

        public static void DrawBoxText_LineAndFill(
            Graphics g,
            string text,
            string fontname,
            Color  fontcolor,
            float  fontsize,
            bool   fontunderline,

            RectangleF rect,

            float  lineWidth,
            Color  lineColor,
            Color  fillColor
        )
        {
            DrawBoxText_LineAndFill(g,text,fontname,fontcolor,fontsize, fontunderline, rect.Location,rect.Width,rect.Height,lineWidth,lineColor,fillColor);
        }

         public static void DrawBoxText_LineAndFill(
            Graphics g,
            string text,
            string fontname,
            Color  fontcolor,
            float  fontsize,
            bool   fontunderline,

            PointF point,
            float  width,
            float  height,
            float  lineWidth,
            Color  lineColorTop,
            Color  lineColorRight,
            Color  lineColorBot,
            Color  lineColorLeft,
            Color  fillColor,

            PointF textmargin
        )
        {
            DrawBox_LineAndFill(g,point,width,height,lineWidth,

                lineColorTop,
                lineColorRight,
                lineColorBot,
                lineColorLeft,

                fillColor);

            if (string.IsNullOrEmpty(text)) return;
            text = text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            var textpoint = PointUtil.Add_Point(point, textmargin);
            var textwidth = width  - textmargin.X * 2;// + fontsize; // 入らない場合があるから　１文字分増やす
            var textheight= height - textmargin.Y * 2;
            var rect = new RectangleF(textpoint.X, textpoint.Y,textwidth,textheight);
            DrawText(g,text,fontname,fontcolor,fontsize, fontunderline,rect);
        }

         public static void DrawCircle_LineAndFill(
            Graphics   g,
            RectangleF rect,
            float      linesize,
            Color      linecolor,
            Color      fillcolor
        )
        {
            using (var pen = new Pen(fillcolor, linesize))
            {
                g.FillEllipse(pen.Brush,rect);
            }
            using (var pen = new Pen(linecolor, linesize))
            {
                g.DrawEllipse(pen,rect);
            }
        }
         public static void DrawCircle_LineAndFill(
            Graphics  g,
            PointF    pos,
            float     diameter,
            float     linesize,
            Color     linecolor,
            Color     fillcolor
        )
        {
            var r = diameter * 0.5f;
            var rect = new RectangleF(pos.X - r,pos.Y - r,diameter,diameter);
            DrawCircle_LineAndFill(g,rect,linesize,linecolor,fillcolor);
        }

        public static SizeF GetTextSize_obs(
            Graphics g,
            string text,
            string fontname,
            float  fontsize,
            float  width
            )
        {
            if (string.IsNullOrEmpty(text)) return new SizeF(0,0);

            SizeF sz = default(Size);
            using (var font = new Font(fontname, fontsize))
            using (var sf = new StringFormat(StringFormat.GenericTypographic))
            {
                sz = g.MeasureString(ScrambleText.get( text ), font,int.MaxValue,sf);
            }

            return sz;
        }
        public static SizeF GetTextSize(
            Graphics g,
            string text,
            string fontname,
            float  fontsize,
            float  width
            )
        {
            if (string.IsNullOrEmpty(text)) return new SizeF(0,0);

            SizeF sz = default(Size);
            using(var font = new Font(fontname,fontsize))
            {
                sz = g.MeasureString(ScrambleText.get( text ),font,int.MaxValue);
            }

            return sz;
        }
        /// <summary>
        /// ちょっと重い
        /// </summary>
        public static SizeF GetTextSize2(
            Graphics g,
            string text,
            string fontname,
            float  fontsize,
            float  width
            )
        {
            if (string.IsNullOrEmpty(text)) return new SizeF(0,0);

            var item = MeasureTextHeight(text,width,G.line_space,g,fontname,fontsize);
            return new SizeF(item.width,item.height);
        }


        public static void DrawBmp(
            Graphics g,
            PointF   pos,
            Bitmap   bmp
            )
        {
            if (bmp!=null)
            {
                g.DrawImage(bmp,pos);
            }
        }

        public static void DrawLine(Graphics g, List<PointF> plist, Color color, float size, LineType type)
        {
            if (plist == null) return;
            switch(type)
            {
                case LineType.BEZIR:    DrawLine__bezir(g,plist,color,size);     break;
                default:                DrawLine__straight(g,plist,color, size); break;
            }
        }
        private static void DrawLine__straight(Graphics g,  List<PointF> plist, Color color, float size)
        {
            if (plist.Count<=1) return;
            using (var pen = Pen_create_arrow(color, size))
            {
                g.DrawLines(pen,plist.ToArray());
            }
        }
        private static void DrawLine__bezir(Graphics g, List<PointF> list, Color color, float size)
        {
            //ポイントリストをベジェ曲線用に分解する。
            //ベジェ曲線にするには、４点ずつの指定となる。
            //最初の４点は、開始点ー次点　次点ー後半分となる
            //その後は、前半点-次点　次点-後半点
            //最後の４点は、前半点-次点　次点ー最終点

            if (list.Count < 1) return;
            if (list.Count==2)
            {
                DrawLine__bezir(g,list[0],list[1],color,size);
                return;
            }

            var nlist= new List<PointF>();

            for(var i = 0; ;i++)
            {
                PointF b0,b1,b2,b3;
                if (i==0)
                {
                    b0 = list[0];
                    b1 = list[1];
                    b2 = list[1];
                    b3 = list.Count==3 ? list[2] : PointUtil.Center(list[1],list[2]);

                    nlist.Add(b0);
                    nlist.Add(b1);
                    nlist.Add(b2);
                    nlist.Add(b3);

                    if (list.Count == 3) break;
                }
                else if (i==list.Count-3) //最後
                {
                    b0 = PointUtil.Center(list[i],list[i+1]);
                    b1 = list[i+1];
                    b2 = list[i+1];
                    b3 = list[i+2];
                    nlist.Add(b0);
                    nlist.Add(b1);
                    nlist.Add(b2);
                    nlist.Add(b3);
                    break;
                }
                else
                {
                    b0 = PointUtil.Center(list[i],list[i+1]);
                    b1 = list[i+1];
                    b2 = list[i+1];
                    b3 = PointUtil.Center(list[i+1],list[i+2]);
                    nlist.Add(b0);
                    nlist.Add(b1);
                    nlist.Add(b2);
                    nlist.Add(b3);
                }
            }

            using (var pen_head = Pen_create_arrow(color,size))
            using (var pen = Pen_create(color,size))
            {
                for(var i = 0; i<nlist.Count-4;i+=4)
                {
                    //g.DrawBezier(pen,nlist[i],nlist[i+1],nlist[i+2],nlist[i+3]);
                    _drawBezier_fix(g,pen,nlist[i],nlist[i+1],nlist[i+2],nlist[i+3]);
                }
                var j = nlist.Count-4;
                //g.DrawBezier(pen_head,nlist[j],nlist[j+1],nlist[j+2],nlist[j+3]);
                _drawBezier_fix(g,pen_head,nlist[j],nlist[j+1],nlist[j+2],nlist[j+3]);
            }
        }
        private static void DrawLine__bezir(Graphics g, PointF a, PointF b, Color color, float size)
        {
            if (a.X == b.X)
            {
                using(var pen_head = Pen_create_arrow(color,size))
                {
                    g.DrawLine(pen_head,a,b);
                }
                return;
            }
            var c = PointUtil.Center(a,b);
            var p1 = a;
            var p2 = PointUtil.Mod_Y(c,a.Y);
            var p3 = PointUtil.Mod_Y(c,b.Y);
            var p4 = b;

            using(var pen_head = Pen_create_arrow(color,size))
            {
                //g.DrawBezier(pen_head,p1,p2,p3,p4);
                _drawBezier_fix(g,pen_head,p1,p2,p3,p4);
            }

        }

        private static Pen   Pen_create(Color color, float size)
        {
            return new Pen(color,size);
        }
        private static Pen   Pen_create_arrow(Color color, float size)
        {
            var pen = Pen_create(color, size);
            //pen.EndCap = LineCap.ArrowAnchor;
            pen.CustomEndCap = new AdjustableArrowCap(ARROWHEAD_WIDTH,ARROWHEAD_HEIGHT);
            return pen;
        }

        //Because Microsoft bug https://stackoverflow.com/questions/27975378/outofmemoryexception-on-drawing-wide-lines-with-winforms
        private static void _drawBezier_fix(Graphics g,Pen pen, PointF p1, PointF p2, PointF p3, PointF p4)
        {
            var b1c = Math.Abs(p1.X - p2.X) >= 0.1f || Math.Abs(p1.Y - p2.Y) > 0.1f;
            var b2c = Math.Abs(p3.X - p4.X) >= 0.1f || Math.Abs(p3.Y - p4.Y) > 0.1f;
            if (b1c && b2c) {
                g.DrawBezier(pen, p1, p2, p3, p4);
            }
            else
            {
                g.DrawLine(pen, p1, p4);
            }
        }

    }
}
