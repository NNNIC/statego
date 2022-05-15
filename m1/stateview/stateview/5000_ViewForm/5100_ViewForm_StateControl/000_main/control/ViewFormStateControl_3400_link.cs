﻿//<<<include=using.txt
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

// Open Link
public partial class ViewFormStateControl {

    void statemenu_link()
    {
        var s = G.linkItemsOnStateMenu.ClickedLink;
        if (string.IsNullOrEmpty(s)) {
            G.NoticeToUser_warning("Unexpected! {771F0CA0-FEF3-483E-BD67-FDF8DBD1150D}");
            return;
        }

        OpenLink.Open(s);
    }
}
