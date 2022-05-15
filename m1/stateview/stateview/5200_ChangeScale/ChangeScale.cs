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
    internal class ChangeScale
    {
        private static double m_savescale = -1;
        internal static void Change(bool refresh = true)
        {
            var scale = G.scale_percent;
            Func<int,int> ms = (_)=> {
                return (int)Math.Round(((float)_ * scale * 0.01f));
            };
            try {
                G.view_form.panel1.SuspendLayout();

#if xx
                var cp  = Cursor.Position;
                var loc = G.main_picturebox.Location;
                var cpop /*_on_main_picturebox*/ = G.main_picturebox.Parent.PointToClient(cp);
                var cp_to_loc = PointUtil.Sub_Point(loc,cpop);
                var cp_to_loc2 = new Point( ms(cp_to_loc.X), ms(cp_to_loc.Y));
                var loc2 = PointUtil.Add_Point(cpop,cp_to_loc2);
                G.main_picturebox.Location = loc2;
#endif

                if (m_savescale > 0)
                {
                    var loc = G.main_picturebox.Location;
                    var cp = Cursor.Position;
                    var cpob = G.vf_sc.GetPointerOnMainBmp(cp,m_savescale * 0.01f);

                    G.main_picturebox.Width  = ms(G.mainbitmap.Width); //(int)((float)G.mainbitmap.Width * scale * 0.01f);
                    G.main_picturebox.Height =  ms(G.mainbitmap.Height); //(int)((float)G.mainbitmap.Height * scale * 0.01f);

                    var sp = G.vf_sc.GetScreenPosFormPointOnImage(cpob);
                    var sp_to_cp = PointUtil.Sub_Point(cp,sp);
                    var loc2 = PointUtil.Add_Point(loc,sp_to_cp);
                    G.main_picturebox.Location = Point.Round( loc2 );

                    //G.NoticeToUser("sp_to_cp=" + sp_to_cp);
                }

                G.view_form.panel1.ResumeLayout();
                G.Scroll_mpb_changed();

                if (refresh)
                { 
                    G.main_picturebox.Refresh();
                }
                
                //G.NoticeToUser("loc " + loc + "=>" + loc2);
                //G.NoticeToUser("cpop = " + cpop + "(<< " +  cp +")");
                //G.NoticeToUser("cp_to_loc=" + cp_to_loc);
                //G.NoticeToUser("cp_to_loc2=" + cp_to_loc2);
               
            }
            catch { }

            m_savescale = scale;
        }

        internal static void Refresh()
        {
            G.main_picturebox.Refresh();
        }

        internal static void Change(int delta)
        {
            var scale = G.scale_percent;
            if (delta>0)
            {
                scale += 5;
            }
            if (delta < 0)
            {
                scale -= 5;
            }

            G.scale_percent = MathX.Clamp(scale, 5,200);

            Change();
        }
        internal static double CalcChange(int delta)
        {
            var scale = G.scale_percent;
            if (delta>0)
            {
                scale += 5;
            }
            if (delta < 0)
            {
                scale -= 5;
            }
            var newscale = MathX.Clamp(scale, 5,200);
            return newscale;
        }
        internal static void Change(int delta, int times)
        {
            var scale = G.scale_percent;
            if (delta>0)
            {
                scale += 5 * times;
            }
            if (delta < 0)
            {
                scale -= 5 * times;
            }

            G.scale_percent = MathX.Clamp(scale, 5,200);

            Change();
        }
        internal static void Change(double v,bool refresh=true)
        {
            G.scale_percent = MathX.Clamp(v, 5,200);
            Change(refresh);
        }
        internal static void Change(string s)
        {
            double o = 0;
            if (!double.TryParse(s,out o))
            {
                o = 100;
            }
            Change(o);
        }
    }
}
