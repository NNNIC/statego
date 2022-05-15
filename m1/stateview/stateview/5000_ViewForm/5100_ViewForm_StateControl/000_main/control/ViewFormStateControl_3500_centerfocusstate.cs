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

// 指定ステートの中央表示＆フォーカス
public partial class ViewFormStateControl {
    public string m_center_focus_state;
    public bool   m_center_focus_same_dirpath = false;
    public bool   m_center_focus_wo_cursor;  //フォーカス後にカーソルを移動しない。
    
    //キー操作用
    public KEYEXEC m_keyexec = KEYEXEC.none;

    public bool    m_keyopen_in_or_out = false; //in or outメニューのどちらかを開ける？
    public InOutBaseData m_keyopen_out_item;    //outメニューを開く際のリファレンス　attribとbranchindexを根拠に

    //中央表示
    public void center_state(string state)
    {
        G.latest_focuse_state = m_focus_state = state;
        focus_draw();
        //m_group_focus_list = new List<string>();
        //m_group_focus_list.Add(state);


        var dd = G.get_draw_data(state);
        if (dd!=null) { 
            var pos = dd.wp_frame_drect.Location;
            ViewUtil.SetViewCenter(pos.X,pos.Y);
        }
#if obs
        G.set_scalepercent_with_textbox(100);

        var posx = G.node_get_pos(state); //位置

        if (posx != null && G.scale_percent != 0)
        {
            var rev_scale = 1f / ((double)G.scale_percent * 0.01f);
            var pos = (Point)posx;

            var vw = (double)G.view_form.panel1.ClientRectangle.Width;
            var vh = (double)G.view_form.panel1.ClientRectangle.Height;

            pos = PointUtil.Add_XY(pos, -(int)(vw / 2f * rev_scale), -(int)(vh / 2f * rev_scale));
            pos = PointUtil.ClampXY(pos, 0, int.MaxValue, 0, int.MaxValue);

            G.view_form.panel1.AutoScrollPosition = pos;
        }
#endif

        G.tabNodeTree.CreateAndSetCurrent();
        G.tabNodeTree.SelectState(state);      

        StateUtil.ContentTab_write(state);
    }

    public bool IsFocusing() //フォーカス中
    {
        if (
            CheckState(S_IsMouseDown) 
            ||
            CheckState(S_CheckMouseGN)
            ||
            CheckState(S_CheckMouseGF)
            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsFocusing_A_STATE()
    {
        return CheckState(S_IsMouseDown);
    }
    public bool IsFocusing_A_GROUP()
    {
        return CheckState(S_CheckMouseGN);
    }
    public bool IsFocusing_PLALER_STATES()
    {
        return CheckState(S_CheckMouseGF);
    }
    public List<string> GetFocusingStates()
    {
        var list = new List<string>();
        if (IsFocusing_A_STATE())
        {
            list.Add(G.latest_focuse_state);
        }
        else if (IsFocusing_A_GROUP())
        {
            var group = G.vf_sc.m_groupnode_name;
            var altstate = AltState.MakeAltStateName(group);
            list.Add(altstate);
        }
        else if (IsFocusing_PLALER_STATES())
        {
            if (m_group_focus_list!=null)
            {
                list.AddRange(m_group_focus_list);
            }
        }
        return list;
    }
}
