using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class ColorUtil
{
    public static string ToRRGGBB(Color col)
    {
        return col.R.ToString("X2") + col.G.ToString("X2") + col.B.ToString("X2");
    }
    public static Color FromRRGGBB(string str)
    {
        var r = Convert.ToInt32(str.Substring(0,2),16);
        var g = Convert.ToInt32(str.Substring(2,2),16);
        var b = Convert.ToInt32(str.Substring(4,2),16);
        return Color.FromArgb(r,g,b);
    }
    public static Color SetAlpha(Color col, int a)
    {
        return Color.FromArgb(a,col);
    }
}
