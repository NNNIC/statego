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
    void set_dst ()
    {
        if (!AltState.IsAltState(m_new_desitination_state)) // 行先がaltstate時はここで処理しない
        { 
            var new_dest = AltState.FindAppropriateDestination(m_new_desitination_state);
            if (string.IsNullOrEmpty(new_dest))
            {
                G.NoticeToUser_warning(G.Localize("w_cannotfindappropiatedestgroup")/*"Can not find an appropriate desitiona in the group."*/);
            }
            else
            {
                var bi = m_save_branchInfo;
                if (bi.m_branchpoint_isNextStateOrBranchOrGosub==1/*true*/)
                {
                    G.excel_program.SetString(bi.m_branchpoint_state,/*"nextstate"*/G.STATENAME_nextstate,new_dest);
                }
                else if (bi.m_branchpoint_isNextStateOrBranchOrGosub==2/*false*/)
                {
                    var s  = G.excel_program.GetStringWithBasestate(bi.m_branchpoint_state,/*"branch"*/G.STATENAME_branch);
                    var s2 = DTBranchUtil.SetLebel(s,(int)bi.m_branchpoint_branch_index,new_dest);
                    G.excel_program.SetString(bi.m_branchpoint_state,/*"branch"*/G.STATENAME_branch,s2);
                }
                else if (bi.m_branchpoint_isNextStateOrBranchOrGosub==3/*gosub*/)
                {
                    var s  = G.excel_program.GetStringWithBasestate(bi.m_branchpoint_state,/*"branch"*/G.STATENAME_gosubstate);
                    G.excel_program.SetString(bi.m_branchpoint_state,/*"gosubstate"*/G.STATENAME_gosubstate,new_dest);
                }


                History2.SaveForce_modify_value("Changed an arrow direction");

                G.NoticeToUser("Connect to " + new_dest);
            }
            m_parent.ReqRedraw();
        }
        else
        {
            //AltState時は、ここを出た後解決へ
        }
    }
}
