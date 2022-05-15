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

using stateview;

public partial class FreeArrowStateControl  {
    void draw_arrow ()
    {
        var l = new List<PointF>();
        var s = GetPosOnFreeArrowPB(m_start);
        l.Add(s);

        var g = GetPosOnFreeArrowPB(m_goal);
        l.Add(g);

        DrawUtil.DrawLine(m_g,l,Color.Red,2, DrawUtil.LineType.STRAIGHT);

        m_pb.Visible = true;

        m_pb.Refresh();
    }
}
