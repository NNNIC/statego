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

public partial class FreeArrowStateControl {
    void update_pb ()
    {
        var margin = 20;

        var start = GetPosOnMainPB( m_start);
        var goal  = GetPosOnMainPB( m_goal);

        var rect = PointUtil.MakeRectangle(start,goal,margin);

        m_pb.Visible = false;

        m_pb.Location = Point.Truncate(rect.Location);
        m_pb.Width    = (int)rect.Width;
        m_pb.Height   = (int)rect.Height;

        disposeBmp_g();
        m_bmp = new Bitmap((int)rect.Width,(int)rect.Height);
        m_g = Graphics.FromImage(m_bmp);
        m_pb.Image = m_bmp;
    }
}
