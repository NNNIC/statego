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
using System.Reflection;

public  class FormUtil
{
    public static bool SetCenterInForm(Form target, Form parentform)  //ターゲットフォームを指定フォームの真ん中に表示する。
    {
        if (target == null || parentform == null) {
            //G.NoticeToUser_warning("{8E12995E-F706-4DDE-812A-4E11C0901EF0}");
            return false;
        }
        var parent_center = PointUtil.Add_XY(parentform.Location, parentform.Size.Width * 0.5f, parentform.Size.Height * 0.5f);
        var target_topleft = PointUtil.Add_XY(parent_center, -target.Size.Width * 0.5f, -target.Size.Height * 0.5f);

        target.Location = Point.Round( target_topleft );
        return true;
    }

}
