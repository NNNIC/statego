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
using System.Runtime.InteropServices;
namespace stateview
{
    [Obsolete]
    internal class ExcelLayout
    {
        //internal string m_layoutjson;

        //internal void Init(Excel.Worksheet sheet)
        //{
        //    m_layoutjson = string.Empty;
        //    try {
        //        var cell = (Excel.Range)sheet.Cells[1,1];
        //        m_layoutjson = (string)cell.Value2;
        //        Marshal.ReleaseComObject(cell);
        //    }
        //    catch { }
        //}

        //Excel.Range m_cell = null;
        //internal void Update(Excel.Worksheet sheet)
        //{
        //    try {
        //        m_cell = (Excel.Range)sheet.Cells[1,1];

        //        m_cell.Value2 = m_layoutjson;
        //    }
        //    catch { }
        //}

        //internal void UpdateDestroy()
        //{
        //    if (m_cell!=null)
        //    {
        //        Marshal.ReleaseComObject(m_cell);
        //    }
        //}



    }
}
