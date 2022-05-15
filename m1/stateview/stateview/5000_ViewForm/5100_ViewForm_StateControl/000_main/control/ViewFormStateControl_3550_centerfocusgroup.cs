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
    public string m_center_focus_group;
    //public bool m_center_focus_same_dirpath

    public void center_group_obs(string grouppath)
    {
        //G.latest_focuse_state = m_focus_state = grouppath;
        m_groupnode_name = stateview.GroupNodeUtil.get_last_path(grouppath).Trim('/');
        if (string.IsNullOrEmpty(m_groupnode_name)) return;
        focus_draw_groupnode();

        G.set_scalepercent_with_textbox(100);

        var posx = G.node_get_pos( AltState.MakeAltStateName(m_groupnode_name)); //位置

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


        G.tabNodeTree.CreateAndSetCurrent();
        G.tabNodeTree.SelectState(grouppath);      
    }
    public void center_group(string grouppath)
    {
        //G.latest_focuse_state = m_focus_state = grouppath;
        m_groupnode_name = stateview.GroupNodeUtil.get_last_path(grouppath).Trim('/');
        if (string.IsNullOrEmpty(m_groupnode_name)) return;
        focus_draw_groupnode();

        var dd = G.get_draw_data(AltState.MakeAltStateName(m_groupnode_name));
        if (dd!=null)
        {
            var pos = dd.wp_frame_drect.Location;
            ViewUtil.SetViewCenter(pos.X, pos.Y);
        }
#if obs
        var posx = G.node_get_pos( AltState.MakeAltStateName(m_groupnode_name)); //位置

        if (posx != null && G.scale_percent != 0)
        {
            var scale = ((double)G.scale_percent * 0.01f);
            var pos = (Point)posx;

            var ph = (double)G.view_form.panel1.Size.Height; 
            var pw = (double)G.view_form.panel1.Size.Width; 

            var e  = scale;

            var sx =  (pos.X * e) - pw * 0.5f;  
            var sy =  (pos.Y * e) - pw * 0.5f;

            G.view_form.panel1.AutoScrollPosition = new Point((int)sx,(int)sy);
        }
#endif
        G.tabNodeTree.CreateAndSetCurrent();
        G.tabNodeTree.SelectState(grouppath);      
    }

}
