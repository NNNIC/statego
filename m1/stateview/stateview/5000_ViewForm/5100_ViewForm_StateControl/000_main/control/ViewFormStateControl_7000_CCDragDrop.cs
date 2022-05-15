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


// トラックバックとフォワード処理用
public partial class ViewFormStateControl {
    void br_isCCDrop(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (G.m_cc_dragdrop == Globals.CCDD.dragenter)
        {
            SetNextState(st);
        }
    }
    void br_isCCDragEnter(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.CC_DRAGENTER)
        {
            SetNextState(st);
        }
    }
    bool wait_cc_dd()
    {
        if (G.m_cc_dragdrop == G.CCDD.dragdrop || G.m_cc_dragdrop == G.CCDD.none)
        {
            m_okCancel = G.m_cc_dragdrop == G.CCDD.dragdrop;
            return true;
        }
        return false;
    }
    void cc_copy_clipboard()
    {
        m_okCancel = false;
        var folder = Path.GetDirectoryName(G.m_cc_droppath);
        var path = Path.Combine( folder, CopyCollection.DATA_FILE);
        if  (File.Exists(path))
        {
            try {
                var s = File.ReadAllText(path,Encoding.UTF8);
                Clipboard.SetText(s);
                m_pos_at_menu_on_bmp = Point.Truncate( G.vf_sc.GetPointerOnMainBmp() );
                m_keyexec = stateview.KEYEXEC.paste_wo_outflow;
                m_okCancel = true;
            }
             catch(SystemException e) {
                G.NoticeToUser_warning("No Copy Data : "+e.Message);
            }
            G.cc.copydata_path = folder;
        }
    }

}
