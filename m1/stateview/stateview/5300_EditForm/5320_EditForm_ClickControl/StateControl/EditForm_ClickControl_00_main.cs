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


public partial class EditForm_ClickControl  {

    public stateview._5300_EditForm.EditForm m_parentForm;
    public string m_state;
    public string m_name;
    public string m_text;
    public Bitmap m_bmp;
    public bool   m_modifiedBmp=false;

    public bool   m_bCmt = false;
    public string m_cmt;

    public bool   m_bRef = false;
    public string m_ref;

    public bool   m_needUpdateDgv = false;

    public void Start()
    {
        sc_start(S_START);
    }

    public void Update()
    {
        sc_update();
    }

    public bool IsDone()
    {
        return m_sm.CheckState(S_END);
    }
}
