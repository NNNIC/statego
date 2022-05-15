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


/*
    IN OUT MENU
*/
public partial class ViewFormStateControl {

    void br_isClickBranch(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.CLICK_ON_BRANCH)
        {
            SetNextState(st);
        }
    }
    string m_inoutmenu_state;
    bool   m_inoutmenu_done;
    bool   m_inoutmenu_editor;
    bool   m_inoutmenu_clear;
    bool   m_inoutmenu_insert;
    void inoutmenu_init()
    {
        m_inoutmenu_state  = null;
        m_inoutmenu_done   = false;
        m_inoutmenu_editor = false;
        m_inoutmenu_clear  = false;
        m_inoutmenu_insert = false;

        var menu = G.view_form.inouttMenu;

        var loc = GetScreenPosFormPointOnImage(m_branchInfo.m_branchpoint_pos);
        var loc2 = G.view_form.PointToClient(Point.Round(loc));
        loc2 = PointUtil.Add_XY(loc2,-2,-2);

        if (_inoutmenu_create())
        { 
            menu.Show(G.view_form, loc2);
        }
        else
        {
            //error
            m_inoutmenu_done = true;
        }
    }

    bool inoutmenu_done()
    {
        var bounds = G.view_form.inouttMenu.Bounds;
        var margin = 20.0f;
        var newbounds = RectangleUtil.AddMargin(bounds,margin);
        if (!newbounds.Contains(Cursor.Position))
        {
            G.view_form.inouttMenu.Close();
            m_inoutmenu_done = true;
        }

        return m_inoutmenu_done;
    }

    internal void inoutmenu_closed()
    {
        m_inoutmenu_done = true;
    }
    bool _inoutmenu_create()
    {
        var state = m_branchInfo.m_branchpoint_state;
        if (string.IsNullOrEmpty(state)) return false;

        if (AltState.IsAltState(state) && m_branchInfo.m_branchpoint_inputpoint) //グループノードの流入は別
        {
            return _inoutmenu_create_src_group();    
        }
        else
        {
            return _inoutmenu_create_state();
        }

    }
    bool _inoutmenu_create_state()
    {
        var menu  = G.view_form.inouttMenu;
        var state = m_branchInfo.m_branchpoint_state;

        if (string.IsNullOrEmpty(state)) return false;
        if (!StateUtil.IsValidStateName(state)) return false;

        //収集 
        var list = new List<string>();
        // １．入力
        if (m_branchInfo.m_branchpoint_inputpoint)
        {
            if (AltState.IsAltState(state))
            {
                var grouppath = GroupNodeUtil.pathcombine(G.node_get_cur_dirpath(),AltState.TrimAltStateName(state));
                var allstate_on_group = G.node_get_allstates_by_dirpath(grouppath);
                if (allstate_on_group!=null)
                {
                    allstate_on_group.ForEach(
                        s=> {
                            var srclist = G.state_input_src_list_allstates_wo_base(s);
                            if (srclist!=null)
                            {
                                foreach(var s2 in srclist)
                                {
                                    var s2path = G.node_get_dirpath(s2);
                                    if (!s2path.StartsWith(grouppath))
                                    {
                                        ListUtil.AddValIfNotExist(ref list,s2);
                                    }
                                }
                            }

                        }
                    );
                }
                
            }
            else
            { 
                var inputlist = DictionaryUtil.Get(G.state_input_src_list,state);
                if (inputlist!=null && inputlist.Count > 0)
                {
                    inputlist.ForEach(i=> {
                        var s = i.state;
                        if (!AltState.IsAltState(s) && StateUtil.IsValidStateName(s))
                        { 
                            if (i.attrib != InOutBaseData.ATTRIB._base)
                            { 
                                ListUtil.AddValIfNotExist(ref list,s);
                            }
                        }
                    });
                }
            }
        }
        // ２．出力
        Action<string, InOutBaseData.ATTRIB,int> collect = (st,a,index) => {
            var outlist = DictionaryUtil.Get(G.state_output_dst_list,st);
            if (outlist!=null && outlist.Count > 0)
            {
                if (index < 0)
                { 
                    outlist.ForEach(i=> {
                        var s = i.state;
                        if (!AltState.IsAltState(s) && StateUtil.IsValidStateName(s))
                        { 
                            if (i.attrib == a)
                            {
                                list.Add(s);
                            }
                        }
                    });
                }
                else
                {
                    outlist.ForEach(i=> {
                            if (i.attrib == a && i.branch_index == index)
                            {
                                list.Add(i.state);
                            }
                        });
                }
            }
        };
        if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==1) //1:nextstate
        {
            List<string> targststates;
            if (AltState.IsAltState(state))
            {
                var labels = BranchUtil.GetLabelListFromState(state);
                targststates = labels;
                //targststates = G.node_get_allstates_by_dirpath( GroupNodeUtil.pathcombine(G.node_get_cur_dirpath(), AltState.TrimAltStateName(state)));
                foreach(var tmpstate in targststates)
                {
                    ListUtil.AddValIfNotExist(ref list,tmpstate);
                }
            }
            else
            { 
                collect(state,InOutBaseData.ATTRIB.nextstate,-1);
            }
        }
        if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==2) //2:branch
        {
            collect(state,InOutBaseData.ATTRIB.branch,(int)m_branchInfo.m_branchpoint_branch_index);        
        }
        if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==3) //3:gosub
        {
            collect(state,InOutBaseData.ATTRIB.gosub,-1);
        }
        
        // 出力は、最後にEditorを追加
        if (!m_branchInfo.m_branchpoint_inputpoint && !AltState.IsAltState(state))
        {
            list.Add("#edit");
        }

        #region 挿入・ペースト・クリア
        // IN に 挿入追加  ※ペーストは未定
        if (m_branchInfo.m_branchpoint_inputpoint && !AltState.IsAltState(state))
        {
            list.Add("#insert");
        }

        // OUTメニューに 挿入とクリア追加 ※ペーストは未定
        if (!m_branchInfo.m_branchpoint_inputpoint && !AltState.IsAltState(state))
        {
            list.Add("#insert");
            list.Add("#clear");
        }
        #endregion

        //メニュー構成
        if (list.Count == 0)
        {
            return  false;
        }

        menu.Items.Clear();
        for(var n = 0; n <list.Count; n++)
        {
            var s = list[n];
            menu.Items.Add( s );
            var item = menu.Items[n];
            item.Click += _inoutmenu_Item_Click;
            if (s=="#edit")
            {
                item.Text = G.Localize("iom_godialog");// "指定ダイアログへ";
                item.Tag =s;
            }
            else if (s=="#insert")
            {
                item.Text = G.Localize("iom_insert");//"新規ステート挿入";
                item.Tag = s;
            }
            else if (s=="#clear")
            {
                item.Text = G.Localize("iom_clear"); //"流出先クリア";
                item.Tag = s;
            }
            else
            {
                var cmt = G.excel_program.GetString(s,G.STATENAME_statecmt);
                if (!string.IsNullOrEmpty(cmt))
                {
                    item.ToolTipText = cmt;
                }
                var group = G.node_get_groupname(s);
                if (!string.IsNullOrEmpty(group))
                {
                    if (!string.IsNullOrEmpty(item.ToolTipText)) item.ToolTipText += "\n";
                    item.ToolTipText += "group:" + group;
                }
                if (!string.IsNullOrEmpty(item.ToolTipText)) item.ToolTipText += "\n";
                item.ToolTipText += G.Localize("inout_ctrl_copy");

            }
        }
        return true;
    }
    private bool _inoutmenu_create_src_group()
    {
        var menu  = G.view_form.inouttMenu;
        var group = AltState.TrimAltStateName( m_branchInfo.m_branchpoint_state );

        var list = DictionaryUtil.Get(G.group_input_src_list_wo_base_on_current,group);
        if (list == null || list.Count==0) return false;

        menu.Items.Clear();
        for(var n = 0; n <list.Count; n++)
        {
            var s = list[n];
            menu.Items.Add( s );
            var item = menu.Items[n];
            item.Click += _inoutmenu_Item_Click;
            var cmt = G.excel_program.GetString(s,G.STATENAME_statecmt);
            if (!string.IsNullOrEmpty(cmt))
            {
                item.ToolTipText = cmt;
            }
            var groupstr = G.node_get_groupname(s);
            if (!string.IsNullOrEmpty(groupstr))
            {
                if (!string.IsNullOrEmpty(item.ToolTipText)) item.ToolTipText += "\n";
                item.ToolTipText += "group:" + groupstr;
            }
            if (!string.IsNullOrEmpty(item.ToolTipText)) item.ToolTipText += "\n";
            item.ToolTipText += G.Localize("inout_ctrl_copy");
        }
        return true;
    }

    private void _inoutmenu_Item_Click(object sender, EventArgs e)
    {
        var item = (ToolStripItem)sender;
        if (item!=null)
        { 
            if (item.Tag!=null)
            {
                var tag = item.Tag.ToString();
                if      (tag == "#edit")   m_inoutmenu_editor = true;
                else if (tag == "#clear")  m_inoutmenu_clear  = true;
                else if (tag == "#insert") m_inoutmenu_insert = true;
            } 
            else
            { 
                m_inoutmenu_state = item.Text;
            }
        }
        m_inoutmenu_done = true;
    }

    void br_hasInOutState(Action<int,bool> st)
    {
        if (!string.IsNullOrEmpty(m_inoutmenu_state))
        {
            SetNextState(st);
        }
    }
    void inoutmenu_focus_state()
    {
        G.vf_sc.m_center_focus_state = m_inoutmenu_state;
        FocusTrack.Record(m_inoutmenu_state);
    }
    void br_goInOutDialog(Action<int,bool> st)
    {
        if (m_inoutmenu_editor)
        {
            SetNextState(st);
        }
    }
    void br_clearDst(Action<int,bool> st)
    {
        if (m_inoutmenu_clear)
        {
            SetNextState(st);
        }
    }
    void br_insertNew(Action<int,bool> st)
    {
        if (m_inoutmenu_insert)
        {
            SetNextState(st);
        }
    }
    void br_goFocus(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
        {
            SetNextState(st);
        }
    }
    void inout_copy_name()
    {
        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
        {
            Clipboard.SetText(m_inoutmenu_state);
        }
    }
    void inout_menu_clear_dst()
    {
        if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub == 1) //nextstate
        {
            G.excel_program.SetString(m_branchInfo.m_branchpoint_state,G.STATENAME_nextstate,"");
        }
        else if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub == 2) // branch
        {
            var s  = G.excel_program.GetStringWithBasestate(m_branchInfo.m_branchpoint_state,/*"branch"*/G.STATENAME_branch);
            var s2 = DTBranchUtil.SetLebel(s,(int)m_branchInfo.m_branchpoint_branch_index,"");
            G.excel_program.SetString(m_branchInfo.m_branchpoint_state,/*"branch"*/G.STATENAME_branch,s2);
        }
        else if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub == 3) // gosub
        {
            G.excel_program.SetString(m_branchInfo.m_branchpoint_state,G.STATENAME_gosubstate,"?");
        }
        History2.SaveForce_modify_value("Clear the outflow.");
    }
    void inout_menu_insert()
    {
        var newstate = G.excel_program.NewState(m_branchInfo.m_branchpoint_state,G.node_get_cur_dirpath());
        if (m_branchInfo.m_branchpoint_inputpoint) //流入側
        {
            //前ステートに対して、元ステートを指定してるnextstate, gosub, branchを変更する。
            foreach(var s in G.excel_program.GetStateList())
            {
                //nextstate
                var nextstate = G.excel_program.GetString(s,G.STATENAME_nextstate);
                if (nextstate == m_branchInfo.m_branchpoint_state)
                {
                    G.excel_program.SetString(s,G.STATENAME_nextstate,newstate);
                }
                //branch
                var brlist = BranchUtil.GetApiAndLabelListFromState(s);
                if (brlist!=null)
                {
                    for (var i = 0; i< brlist.Count; i++)
                    {
                        var bi = brlist[i];
                        if (bi.label == m_branchInfo.m_branchpoint_state)
                        {
                            bi.label = newstate;
                        }
                    }
                    BranchUtil.SetBranchByApiAndLabelList(s,brlist);
                }
                //gosub
                var gosubstate = G.excel_program.GetString(s,G.STATENAME_gosubstate);
                if (gosubstate == m_branchInfo.m_branchpoint_state)
                {
                    G.excel_program.SetString(s,G.STATENAME_gosubstate,newstate);
                }            
            }
            //新ステートのnextstateを元ステートにする
            G.excel_program.SetString(newstate, G.STATENAME_nextstate, m_branchInfo.m_branchpoint_state);
            //ポジションを 左へ G.state_width バッファ
            {
                float diff = 0;
                var dd = DictionaryUtil.Get(G.m_draw_data_list,m_branchInfo.m_branchpoint_state);
                if (dd !=null)
                {
                    diff = dd.output_pos.Y;
                }

                var pos = m_branchInfo.m_branchpoint_pos;//  (PointF)(Point)G.node_get_pos(m_branchInfo.m_branchpoint_state);
                pos = PointUtil.Add_XY(pos, -(G.state_width + 50),-diff);
                G.UpdateExcelPos_w_clamp(newstate,pos,true);
            }

            DrawBenri.draw_opt();

            History2.SaveForce_new("Insert a new state.");
            FocusTrack.Record(newstate);

            m_focus_state = newstate;
            G.latest_focuse_state = m_focus_state;

            focus_draw();

            return;
        }
        //流出側
        if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub==1) // nextstate
        {
            G.excel_program.SetString(newstate,G.STATENAME_nextstate, m_branchInfo.m_branchpoint_label);
            G.excel_program.SetString(m_branchInfo.m_branchpoint_state, G.STATENAME_nextstate, newstate);
        }
        else if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub == 2)
        {
            G.excel_program.SetString(newstate,G.STATENAME_nextstate, m_branchInfo.m_branchpoint_label);

            if (m_branchInfo.m_branchpoint_branch_index!=null)
            {
                var index =(int)m_branchInfo.m_branchpoint_branch_index;
                G.excel_program.SetBranchLabel(m_branchInfo.m_branchpoint_state,index,newstate);
            }
        }
        else if (m_branchInfo.m_branchpoint_isNextStateOrBranchOrGosub == 3)
        {
            G.excel_program.SetString(newstate,G.STATENAME_nextstate, m_branchInfo.m_branchpoint_label);
            G.excel_program.SetString(m_branchInfo.m_branchpoint_state, G.STATENAME_gosubstate, newstate);             
        }
        //位置は、out point posからバッファ
        {
            float diff = 0;
            var dd = DictionaryUtil.Get(G.m_draw_data_list,m_branchInfo.m_branchpoint_state);
            if (dd !=null)
            {
                diff = dd.output_pos.Y;
            }

            var pos = m_branchInfo.m_branchpoint_pos;
            pos = PointUtil.Add_XY(pos, 50,-diff);
            G.UpdateExcelPos_w_clamp(newstate,pos,true);

            DrawBenri.draw_opt();

            History2.SaveForce_new("Insert a new state.");
            FocusTrack.Record(newstate);

            m_focus_state = newstate;
            G.latest_focuse_state = m_focus_state;

            focus_draw();

        }
    }
}