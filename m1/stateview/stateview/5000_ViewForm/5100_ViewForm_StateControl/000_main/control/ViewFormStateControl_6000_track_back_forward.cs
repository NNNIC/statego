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
    private void br_TrackShow(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TRACK_SHOW)
        {
            SetNextState(st);
        }
    }
    private void br_TrackBack(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TRACK_BACK)
        {
            SetNextState(st);
        }
    }
    private void br_TrackForward(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TRACK_FORWARD)
        {
            SetNextState(st);
        }
    }
    public enum TRACKRESULT
    {
        none,
        go_focusState,
        go_focusGroupNode,
        go_groupFocus1,
    }
    public TRACKRESULT m_trackresult;
    public string       m_track_focused_pathdir = null;       //同じ行動をさけるため 前の記録
    public List<string> m_track_focused_states = new List<string>(); //同じ行動をさけるため 前の記録
    private void statemenu_trackback()
    {
        Func<FocusTrack.Item> _get = () => FocusTrack.Back();
        statemenu_track_common(_get);
    }

    private void statemenu_track_common(Func<FocusTrack.Item> _get)
    {
        m_trackresult = TRACKRESULT.none;
        bool bNeedCheckCurFocus = true;
        FocusTrack.Item item = null;

        while (bNeedCheckCurFocus == true)
        {
            bNeedCheckCurFocus = false;
            item = _get();
            if (item == null)
            {
                G.NoticeToUser("Cannot find  tracking data");
                return;
            }
            if (item.is_equal(m_track_focused_pathdir, m_track_focused_states))
            {
                bNeedCheckCurFocus = true;
            }
        }

        track_work(item);

        m_track_focused_pathdir = item.pathdir; //現状を記録
        m_track_focused_states  = item.states;

        if (FocusTrack.GetDataCount() == 1)
        {
            G.NoticeToUser("No tracking data");
            return;
        }
    }

    private void track_work(FocusTrack.Item item)
    {
        m_trackresult = TRACKRESULT.none;
        if (G.node_get_cur_dirpath() != item.pathdir) //グループへ
        {
            G.node_set_curdir(item.pathdir);
            statebox_optdraw();
        }
        if (item.states != null && item.states.Count == 1 && StateUtil.IsValidStateName(item.states[0]))
        {
            var state = item.states[0];
            if (G.state_working_list.Contains(state))
            { 
                m_state_under_pointer = state;
                if (AltState.IsAltState(state))
                {
                    m_trackresult = TRACKRESULT.go_focusGroupNode;
                }
                else if (StateUtil.IsValidStateName(state))
                { 
                    m_trackresult = TRACKRESULT.go_focusState;
                }
            }
        }
        else if (item.states!=null && item.states.Count >= 2)
        {
            if (m_group_focus_list==null)
            { 
                m_group_focus_list = new List<string>();
            }
            else
            { 
                m_group_focus_list.Clear();
            } 

            foreach(var state in item.states)
            {
                if (G.state_working_list.Contains(state))
                {
                    m_group_focus_list.Add(state);
                }
            }
            if (m_group_focus_list!=null && m_group_focus_list.Count > 0)
            {
                m_trackresult = TRACKRESULT.go_groupFocus1;
            }
        }
        if (item.scale > 0)
        {
            G.set_scalepercent_with_textbox(item.scale * 100f);
        }
        //ViewUtil.SetViewTopLeft(item.topleft.X,item.topleft.Y);
        if (item.states!=null && item.states.Count>0)
        {
            var s = item.states[0];
            ViewUtil.SetInViewIfOut(s);
        }
        else
        {
            ViewUtil.SetViewTopLeft(item.topleft.X,item.topleft.Y);
        }
    }

    private void statemenu_trackforward()
    {
        Func<FocusTrack.Item> _get = () => FocusTrack.Forward();
        statemenu_track_common(_get);
    }
    void br_trackresult_go_focusstate(Action<int, bool> st)
    {
        if (m_trackresult ==  TRACKRESULT.go_focusState)
        {
            SetNextState(st);
        }
    }
    void br_trackresult_go_focusgroupnode(Action<int, bool> st)
    {
        if (m_trackresult ==  TRACKRESULT.go_focusGroupNode)
        {
            SetNextState(st);
        }
    }
    void br_trackresult_go_focusstates(Action<int, bool> st)
    {
        if (m_trackresult ==  TRACKRESULT.go_groupFocus1)
        {
            SetNextState(st);
        }
    }
    private void track_focus()
    {
        FocusTrack.Record(G.latest_focuse_state);
    }
    private void track_focus_groupnode()
    {
        FocusTrack.Record(m_state_under_pointer);
    }
    private void track_focus_states()
    {
        if (m_needtrack)
        {
            m_needtrack = false;
            if (m_group_focus_list!=null && m_group_focus_list.Count >= 2)
            {
               FocusTrack.Record(m_group_focus_list);
            }
        }
    }
}
