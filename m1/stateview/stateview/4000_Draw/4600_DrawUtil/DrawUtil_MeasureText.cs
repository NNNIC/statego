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
        public class TextItem
        {
            public string[] lines;
            public float[]  yposlist;   //linesのy位置リスト

            public float    width;
            public float    height;

            public string   fontname;
            public float    fontsize;
            public bool     fontunderline;
        }

        public static TextItem MeasureTextHeight(
            string text,
            float width,
            float  linespace,
            Graphics g,
            string fontname,
            float fontsize
            )
        {
            if (fontsize <= 0) fontsize = 1;

            using(var font = new Font(fontname,fontsize)) {

                var linelist = new List<string>();
                var yposlist = new List<float>();

                var s = string.Empty;
                var y =  0f;
                var height = 0f;

                for(var i = 0; i<text.Length; i++)
                {
                    var c = text[i];
                    var bBreak = c=='\n';
                    var news = bBreak ? s : s+c;
                    var size = g.MeasureString(ScrambleText.get( news ),font);
                    height = size.Height;

                    if (bBreak || size.Width > width)
                    {
                        if (!string.IsNullOrEmpty(s))
                        {
                            linelist.Add(s); //前の文字でBreak
                            yposlist.Add(y);
                            s = c=='\n' ? "" : ""+c;
                            y += height + linespace; //次の行
                        }
                    }
                    else
                    {
                        s = news;
                    }
                }
                if (!string.IsNullOrEmpty(s))
                {
                    var size = g.MeasureString(ScrambleText.get( s ),font);
                    linelist.Add(s);
                    yposlist.Add(y);
                }
                y += height + linespace;
                yposlist.Add(y); //計算を楽にするために

                var item = new TextItem();
                item.lines = linelist.ToArray();
                item.yposlist = yposlist.ToArray();

                item.width  = width;
                item.height = y;

                item.fontname = fontname;
                item.fontsize = fontsize;

                return item;
            }
        }

        public static void DrawTextItem(
            Graphics g,
            TextItem item,
            PointF loc,
            Color color,
            float heighlimt = float.MaxValue
        )
        {
            if (item==null || item.lines==null || item.lines.Length==0)
            {
                return;
            }

            Func<Font> createfont = ()=> {
                if (item.fontunderline)
                {
                    return new Font(item.fontname, item.fontsize, FontStyle.Underline);
                }
                else
                {
                    return new Font(item.fontname, item.fontsize);
                }
            };
            using (var pen = new Pen(color, item.fontsize))
            using (var font = createfont())
            {
                for (var i = 0; i < item.lines.Length; i++)
                {
                    var text = item.lines[i];
                    var ypos = item.yposlist[i];
                    var ypos2 = i + 1 < item.yposlist.Length ? item.yposlist[i + 1] : ypos + item.fontsize;

                    if (i != 0 && ypos2 > heighlimt) break;

                    var pos = PointUtil.Add_Y(loc, ypos);

                    var scrambletext= ScrambleText.get(text);
                    g.DrawString(scrambletext, font, pen.Brush, pos);

                }
            }
        }

        public static void DrawTextMeasureAndDraw(
            Graphics g,
            string text,
            string fontname,
            Color  fontcolor,
            float  fontsize,
            bool   fontunderline,

            RectangleF rect
        )
        {
            if (string.IsNullOrEmpty(text)) return;
            if (string.IsNullOrEmpty(fontname)) return;

            var item = MeasureTextHeight(text,rect.Width,G.line_space ,g,fontname,fontsize);
            item.fontunderline = fontunderline;
            DrawTextItem(g,item,rect.Location,fontcolor,rect.Height);
        }



    }
}
