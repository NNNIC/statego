using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public static class MathX
{
    internal static double Clamp(double x, double a, double b)
    {
        var ret = x;
        if (x < a) ret = a;
        if (x > b) ret = b;

        return ret;
    }
    internal static float Clamp(float x, float a, float b)
    {
        var ret = x;
        if (x < a) ret = a;
        if (x > b) ret = b;

        return ret;
    }
    internal static int Clamp(int x, int a, int b)
    {
        var ret = x;
        if (x < a) ret = a;
        if (x > b) ret = b;

        return ret;
    }

    internal static float Lerp(float a, float b, float t)
    {
        if (a==b) return a;
        return (b-a) * t + a; 
    }

    internal static PointF Lerp(PointF a, PointF b, float t)
    {
        return new PointF( Lerp(a.X,b.X,t), Lerp(a.Y, b.Y, t) );
    }

    internal static float Min(float[] array) 
    {
        var a = array[0];
        for(var i = 1; i<array.Length; i++)
        {
            a = Math.Min(a,array[i]);
        }
        return a;
    }
    internal static float Max(float[] array)
    {
        var a = array[0];
        for(var i = 1; i<array.Length; i++)
        {
            a = Math.Max(a,array[i]);
        }
        return a;
    }
}
