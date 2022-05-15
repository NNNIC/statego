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

public class RectangleUtil
{
    public static RectangleF ToRectangleF(Rectangle r)
    {
        return (RectangleF)r;
    }

    public static RectangleF MakeRectangle(PointF a, PointF b)
    {
        return PointUtil.MakeRectangle(a,b);
    }

    public static PointF Center(RectangleF r)
    {
        var x = r.Location.X + r.Width * 0.5f;
        var y = r.Location.Y + r.Height * 0.5f;

        return new PointF(x,y);
    }
    public static Point Center(Rectangle r)
    {
        var x = r.Location.X + r.Width / 2;
        var y = r.Location.Y + r.Height / 2;

        return new Point(x,y);
    }

    /// <summary>
    /// 内部レクタングルが中央にくるローケーションを求める
    /// </summary>
    /// <param name="outr"></param>
    /// <param name="inr"></param>
    /// <returns></returns>
    public static Point CalcLocation_Centering_innerRectangle(Rectangle outr, Rectangle inr)
    {
        var outr_center = Center(outr);
        var loc = PointUtil.Add_XY(outr_center, - (inr.Width / 2), (inr.Height / 2));
        return loc; 
    }

    public static RectangleF MoveCenter(RectangleF r, Point center)
    {
        var t_center = Center(r);
        var diff = PointUtil.Add_XY(center,-t_center.X,-t_center.Y);
        var newlt = new PointF(r.Left  + diff.X, r.Top + diff.Y);
        var newrb = new PointF(r.Right + diff.X, r.Bottom + diff.Y);
        return MakeRectangle(newlt,newrb);
    }


    public static PointF BR(RectangleF r)
    {
        return new PointF(r.Right,r.Bottom);
    }
    public static PointF TL(RectangleF r)
    {
        return r.Location;
    }

    public static RectangleF AddMargin(RectangleF rect, float margin)
    {
        return PointUtil.MakeRectangle(new PointF(rect.Right,rect.Top), new PointF(rect.Left,rect.Bottom),margin);
    }

    public static RectangleF CombineF(RectangleF a, RectangleF b)
    {
        var min_x = MathX.Min(new float[] {a.Right,a.Left,b.Right,b.Left});
        var max_x = MathX.Max(new float[] {a.Right,a.Left,b.Right,b.Left});
        var min_y = MathX.Min(new float[] {a.Top,a.Bottom,b.Top,b.Bottom });
        var max_y = MathX.Max(new float[] {a.Top,a.Bottom,b.Top,b.Bottom });

        var tl = new PointF(min_x,min_y);
        var br = new PointF(max_x,max_y);

        return PointUtil.MakeRectangle(tl,br);
    }
    public static Rectangle Combine(Rectangle a, Rectangle b)
    {
        var min_x = MathX.Min(new float[] {a.Right,a.Left,b.Right,b.Left});
        var max_x = MathX.Max(new float[] {a.Right,a.Left,b.Right,b.Left});
        var min_y = MathX.Min(new float[] {a.Top,a.Bottom,b.Top,b.Bottom });
        var max_y = MathX.Max(new float[] {a.Top,a.Bottom,b.Top,b.Bottom });

        var tl = new PointF(min_x,min_y);
        var br = new PointF(max_x,max_y);

        return  Rectangle.Truncate(PointUtil.MakeRectangle(tl,br));
    }
    public static RectangleF DecrimentTopHeight(RectangleF a, float h)
    {
        var topleft  = new PointF(a.Left, a.Top-h);
        var botright = new PointF(a.Right,a.Bottom);

        return PointUtil.MakeRectangle(topleft, botright);
    }
    public static Rectangle AddTopHeight(Rectangle a, float h)
    {
        var topleft  = new PointF(a.Left, a.Top+h);
        var botright = new PointF(a.Right,a.Bottom);

        var r = PointUtil.MakeRectangle(topleft, botright);
        return Rectangle.Truncate(r);
    }
    public static Rectangle ClampMax(RectangleF i_rect, float w, float h)
    {
        var rect = i_rect;
        if (rect.X < 0)
        {
            rect.Width = rect.Width + rect.X;
            rect.X = 0;
        }
        if (rect.Y < 0)
        {
            rect.Height = rect.Height + rect.Y;
            rect.Y = 0;
        }
        if (rect.Right >= w)
        {
            rect.Width = rect.Width - (rect.Right  - w + 1);
        }
        if (rect.Bottom >= h)
        {
            rect.Height = rect.Height - (rect.Bottom - h + 1);
        }
        if (rect.Width  < 0) rect.Width  = 1;
        if (rect.Height < 0) rect.Height = 1;

        var newrect = Rectangle.Truncate(rect);

        return newrect;
    }
}
