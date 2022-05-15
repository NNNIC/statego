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
        Bitmap   m_bmp;
        Graphics m_g;

        DrawList m_drawlistMain  { get { return G.drawlistMain;  } }
        DrawList m_drawlistFocus { get { return G.drawlistFocus; } }

        internal Graphics create_bitmap(out Bitmap bmp)
        {
            m_bmp = new Bitmap(G.bitmap_width,G.bitmap_height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            m_g = Graphics.FromImage(m_bmp);
            bmp = m_bmp;
            return m_g;
        }

        internal void destroy_bitmap()
        {
            m_g.Dispose();
            m_g = null;

            m_bmp.Dispose();
            m_bmp = null;
        }

        internal void Add_First()
        {
            m_drawlistMain.clear();
            m_drawlistMain.add(0,draw_bg);              // 0  -- bg
            m_drawlistMain.add(1,draw_grid);            // 1  -- grid
                                                        // 100 -- arrow
                                                        // 1000～ --- 各ステート
                                                        //
        }

        internal void ClearCache()
        {
            m_draw_state_cache.Clear();
        }
    }
}
