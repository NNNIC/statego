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

public partial class FreeArrowStateControl  {

    PointF GetPointerOnMainBmp()
    {
        var pos = (PointF)G.main_picturebox.PointToClient(Cursor.Position);
        pos = PointUtil.Multiply(pos, (float)(1.0f/G.scale));
        return pos;
    }

    PointF GetPosOnMainPB(PointF point_at_image)
    {
        var pos = PointUtil.Multiply(point_at_image, (float)G.scale);
        return pos;
    }

    void disposeBmp_g()
    {
        if (m_g!=null)
        {
            m_g.Dispose();
            m_g = null;
        }

        if (m_bmp!=null)
        {
            m_bmp.Dispose();
            m_bmp = null;
        }
    }

    PointF GetPosOnFreeArrowPB(PointF point_at_image)
    {
        var p = GetPosOnMainPB(point_at_image);
        return PointUtil.Sub_Point( p , m_pb.Location);
    }
}
