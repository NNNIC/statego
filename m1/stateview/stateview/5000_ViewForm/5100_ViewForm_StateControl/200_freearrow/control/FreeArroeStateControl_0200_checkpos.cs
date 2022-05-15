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

    bool m_samepos;

    public string m_new_desitination_state;
    //string m_branchpoint_label;

    PointF m_start;
    PointF m_goal;

    void checkpos()
    {
        m_start = m_save_branchInfo.m_branchpoint_pos;
        _checkIsOnBranch();

        var posonpb = GetPosOnMainPB(m_goal);

        var moveamount = PointUtil.Len_Point(posonpb,m_save_pos);
        m_save_pos = posonpb;
        m_samepos = (moveamount < 3f);
    }


    void _checkIsOnBranch()
    {
        float maxRadius = 100; //

        m_new_desitination_state  = null;

        if (G.m_draw_data_list==null) return;

        var pos = m_parent.GetPointerOnMainBmp();
        m_goal = pos;


        foreach(var p in G.m_draw_data_list)
        {
            var sd = p.Value;
            var st = p.Key;
            if (sd == null || sd.wp_input_dcircle==null ) continue;
            var center = RectangleUtil.Center(sd.wp_input_dcircle);
            if (PointUtil.IsContain(pos,center,maxRadius)) //"nextstate"
            {
                m_goal = center;
                m_new_desitination_state = st;
                maxRadius = Math.Min(maxRadius,PointUtil.Len_Point(pos,center));
            }
        }
    }


}
