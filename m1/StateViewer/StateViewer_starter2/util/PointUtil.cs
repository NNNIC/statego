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

public class PointUtil
{
    public static Point Add_X(Point a, int x)
    {
        return new Point(a.X + x, a.Y);
    }
    public static PointF Add_X(PointF a, float x)
    {
        return new PointF(a.X + x, a.Y);
    }

    public static Point Add_Y(Point a, int y)
    {
        return new Point(a.X , a.Y + y);
    }
    public static PointF Add_Y(PointF a, float y)
    {
        return new PointF(a.X , a.Y + y);
    }
    public static PointF Add_XY(PointF a, float x, float y)
    {
        return new PointF(a.X+x , a.Y + y);
    }
    public static Point Add_XY(Point a, int x, int y)
    {
        return new Point(a.X+x , a.Y + y);
    }


    public static Point Add_Point(Point a, Point b)
    {
        return new Point(a.X + b.X, a.Y + b.Y);
    }
    public static PointF Add_Point(PointF a, PointF b)
    {
        return new PointF(a.X + b.X, a.Y + b.Y);
    }

    public static Point Sub_Point(Point a, Point b)
    {
        return new Point(a.X - b.X, a.Y - b.Y);
    }
    public static PointF Sub_Point(PointF a, PointF b)
    {
        return new PointF(a.X - b.X, a.Y - b.Y);
    }
    public static bool IsEqual(Point a, Point b, int epsilon=1)
    {
        var s = Sub_Point(a,b);
        return (   Math.Abs(s.X) <= epsilon && Math.Abs(s.Y) <=epsilon );
    }
    public static bool IsEqual(PointF a, PointF b, float epsilon=1)
    {
        var s = Sub_Point(a,b);
        return (   Math.Abs(s.X) <= epsilon && Math.Abs(s.Y) <=epsilon );
    }


    public static float Len_Point(Point a, Point b)
    {
        var d = Sub_Point(a,b);
        return  (float)Math.Sqrt((double)(d.X * d.X + d.Y * d.Y));
    }
    public static float Len_Point(PointF a, PointF b)
    {
        var d = Sub_Point(a,b);
        return  (float)Math.Sqrt((double)(d.X * d.X + d.Y * d.Y));
    }
    public static Point Abs(Point a)
    {
        return new Point(Math.Abs(a.X), Math.Abs(a.Y));
    }
    public static PointF Abs(PointF a)
    {
        return new PointF(Math.Abs(a.X), Math.Abs(a.Y));
    }
    public static Point Center(Point a, Point b)
    {
        return new Point( (a.X + b.X) / 2, (a.Y + b.Y) /2 );
    }
    public static PointF Center(PointF a, PointF b)
    {
        return new PointF( (a.X + b.X) / 2, (a.Y + b.Y) /2 );
    }
    public static Point Mod_X(Point a, int x)
    {
        return new Point(x,a.Y);
    }
    public static PointF Mod_X(PointF a, float x)
    {
        return new PointF(x,a.Y);
    }
    public static Point Mod_Y(Point a, int y)
    {
        return new Point(a.X,y);
    }
    public static PointF Mod_Y(PointF a, float y)
    {
        return new PointF(a.X,y);
    }

    public static PointF Multiply(PointF a, float m)
    {
        return new PointF(a.X * m, a.Y *m);
    }
    public static Point Multiply(Point a, float m)
    {
        return new Point((int)((float)a.X * m),(int)((float)a.Y *m));
    }
    public static bool Validate(Point a, int w, int h)
    {
        var x = a.X;
        var y = a.Y;

        return (
            x>=0 && y>=0
            &&
            x<=w && y<=h
            );
    }

    public static RectangleF MakeRectangle(PointF a, PointF b)
    {
        var x0 = a.X;
        var y0 = a.Y;
        var x1 = b.X;
        var y1 = b.Y;

        if (x0 > x1) {
            var c = x0;
            x0 = x1;
            x1 = c;
        }

        if (y0 > y1)
        {
            var c = y0;
            y0 = y1;
            y1 = c;
        }
        return new RectangleF(x0,y0, x1-x0,y1-y0);
    }
    public static RectangleF MakeRectangle(PointF a, PointF b, float margin)
    {
        var x0 = a.X;
        var y0 = a.Y;
        var x1 = b.X;
        var y1 = b.Y;

        if (x0 > x1) {
            var c = x0;
            x0 = x1;
            x1 = c;
        }

        if (y0 > y1)
        {
            var c = y0;
            y0 = y1;
            y1 = c;
        }

        x0 -= margin;
        x1 += margin;

        y0 -= margin;
        y1 += margin;

        return new RectangleF(x0,y0, x1-x0,y1-y0);
    }



    public static bool IsContain(PointF pos, PointF center, float radius)
    {
        var len = Len_Point(pos,center);
        return len <= radius;
    }

    public static Point? Parse(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        var ns = s.TrimStart('(').TrimEnd(')');
        if (string.IsNullOrEmpty(ns)) return null;

        var l = ns.Split(',');
        if (l.Length!=2) return null;

        var x = ParseUtil.ParseInt(l[0],-1);
        var y = ParseUtil.ParseInt(l[1],-1);

        if (x==-1) return null;

        return new Point(x,y);
    }
}
