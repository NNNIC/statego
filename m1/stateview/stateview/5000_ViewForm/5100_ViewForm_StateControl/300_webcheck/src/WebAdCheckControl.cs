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

public partial class WebAdCheckControl  {

	public static bool m_req  = false;
    public static bool m_done = false;

    bool wait_req()
    {
        return m_req;
    }
    
    /*
        複数回オープンする現象に対応するため、
        タイマーを設ける
    */
    DateTime timer = DateTime.MinValue;

    void watch_url()
    {
        if (timer > DateTime.Now) return;

        if (G.view_form.webBrowserAdd!=null && G.view_form.webBrowserAdd.Url!=null)
        {
            var url = G.view_form.webBrowserAdd.Url.AbsoluteUri;
            if (  url.IndexOf("about") < 0 && url.IndexOf(G.web_base)!=0 )
            {
                //G.NoticeToUser(G.view_form.webBrowserAdd.Url.AbsoluteUri);
                //ExecUtil.execute_start(G.view_form.webBrowserAdd.Url.AbsoluteUri,"");
                ExecUtil.execute_start(G.web_info_js, "");
                G.view_form.webBrowserAdd.Url = new Uri(G.web_info);

                timer = DateTime.Now.AddSeconds(1f); 
            }
        }

    }

    bool wait_done()
    {
        return m_done;
    }


}
