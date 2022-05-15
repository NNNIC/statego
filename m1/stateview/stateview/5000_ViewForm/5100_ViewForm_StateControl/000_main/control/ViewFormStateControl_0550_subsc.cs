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

public partial class ViewFormStateControl {

    // IDLE2
    void subsc_init_idle2()
    {
        m_idleSc.Init();
    }

    void subsc_update_idle2()
    {
        m_idleSc.Update();
    }

    bool wait_subsc_idle2_done()
    {
        if (G.psgg_ask_upgrade==true)
        {
            return true;
        }
        if (G.psgg_open_upgrade==true)
        {
            return true;
        }

        return m_idleSc.IsDone();
    }

    //CHKONFCS
    void subsc_chkonfcs_init()
    {
        m_chkonfcsSc.Init();
    }

    void subsc_chkonfcs_update()
    {
        m_chkonfcsSc.Update();
    }

    bool subsc_chkonfcs_done()
    {
        return m_chkonfcsSc.IsDone();
    }

    // GROUPFOCUS
    void subsc_groupfcs_init()
    {
        m_groupFcsSc.Init();
    }
    void subsc_groupfcs_update()
    {
        m_groupFcsSc.Update();
    }
    bool subsc_groupfcs_done()
    {
        return m_groupFcsSc.IsDone();
    }

    //CHKONFCSGF
    void subsc_chkonfcsGF_init()
    {
        m_chkonfcsGFSc.Init();
    }
    void subsc_chkonfcsGF_update()
    {
        m_chkonfcsGFSc.Update();
    }
    bool subsc_chkonfcsGF_done()
    {
        return m_chkonfcsGFSc.IsDone();
    }

    //DANDGF
    void subsc_dandGF_init()
    {
        m_dandGFSc.Init();
    }
    void subsc_dandGF_update()
    {
        m_dandGFSc.Update();
    }
    bool subsc_dandGF_done()
    {
        if (m_dandGFSc.IsDone())
        {
            m_okCancel = m_dandGFSc.m_result == DAnDGFStateControl.RESULT.SUCCESS;
            if (m_okCancel) {
                var cmt = string.Empty;
                for(var n  = 0; n<3; n++)
                {
                    if (m_group_focus_list!=null && m_group_focus_list.Count > n)
                    {
                        if (!string.IsNullOrEmpty(cmt)) cmt += ",";
                        var state = m_group_focus_list[n];
                        if (stateview.AltState.IsAltState(state))
                        {
                            state ="g:" + stateview.AltState.TrimAltStateName(state);
                        }
                        cmt += state;
                    }
                }
                stateview.History2.SaveForce_modify_pos(cmt);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    //S_SLIDING
    void subsc_sliding_init()
    {
        m_slidingSC.Init();
    }
    void subsc_sliding_update()
    {
        m_slidingSC.Update();
    }
    bool subsc_sliding_wait()
    {
        return m_slidingSC.IsDone();
    }
}
