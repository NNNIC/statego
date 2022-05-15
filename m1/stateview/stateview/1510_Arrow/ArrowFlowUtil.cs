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

public class ArrowFlowUtil
{
    internal static List<PointF> Create(DStateData start_st, DStateData goal_st, int? branch_index, PointF start, PointF goal)
    {
        var ctr = new ArrowFlowStateControl2();
        ctr.Begin(start_st,goal_st, branch_index, start, goal);
        ctr.Calc();
        return ctr.GetResult();
    }
}
