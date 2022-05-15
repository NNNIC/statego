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

public partial class ViewFormStateControl {
    string m_focus_state;
    void focus_set()
    {
        m_focus_state = m_state_under_pointer;
        G.latest_focuse_state = m_focus_state;
    }
    void focus_reset()
    {
        m_state_under_pointer = null;
        m_group_focus_list = null;
        m_group_focus_click_state = null;
        m_groupnode_name = null;
        m_focus_state = null;
    }
    void focus_draw()
    {
        G.drawlistFocus.clear();
        G.draw.draw_focus(m_focus_state,Color.FromArgb(50,255,0,0));
        G.draw.draw_arrow_focus(m_focus_state);
        G.drawlistFocus.execute(G.maingraphs);
        G.main_picturebox.Refresh();
    }
    void focus_group_draw()
    {
        G.drawlistFocus.clear();
        G.draw.draw_focus(m_focus_state,Color.FromArgb(50,255,0,0));
        G.draw.draw_arrow_focus(m_focus_state);
        G.drawlistFocus.execute(G.maingraphs);
        G.main_picturebox.Refresh();
    }
    void focus_erase()
    {
        G.drawlistMain.execute(G.maingraphs);
        G.main_picturebox.Refresh();
    }

    void focus_setoneGF_state()
    {
        var focus_state = m_group_focus_list[0];
        m_focus_state = focus_state;
        G.latest_focuse_state = m_focus_state;
        if (m_needtrack)
        { 
            m_needtrack = false;
            FocusTrack.Record(focus_state);
        }
    }
    void focus_setoneGF_group()
    {
        var focus_state = m_group_focus_list[0];
        m_groupnode_name = AltState.TrimAltStateName(focus_state);   
        if (m_needtrack)
        { 
            m_needtrack = false;
            FocusTrack.Record(focus_state);
        }
    }
    void focus_set_2state()
    {
        if (m_group_focus_list!=null)
        { 
            m_group_focus_list.Clear();
        }
        else
        {
            m_group_focus_list = new List<string>();
        }
        m_group_focus_list.Add(m_focus_state);
        m_group_focus_list.Add(m_state_under_pointer);
        m_focus_state = m_state_under_pointer;

        FocusTrack.Record(m_group_focus_list);
    }

    void focus_add_stateGF()
    {
        m_group_focus_list.Add(m_state_under_pointer);
        m_focus_state = m_state_under_pointer;
        FocusTrack.Record(m_group_focus_list);
    }
    void focus_remove_stateGF()
    {
        if (m_group_focus_list.Contains(m_state_under_pointer))
        {
            m_group_focus_list.Remove(m_state_under_pointer);
            m_focus_state = null;
        }
    }
    void draw_focuses()
    {
        G.drawlistMain.execute(G.maingraphs);
        DrawBenri.draw_focuses(m_group_focus_list);
        G.main_picturebox.Refresh();

        FocusTrack.Record(m_group_focus_list);
    }

}
