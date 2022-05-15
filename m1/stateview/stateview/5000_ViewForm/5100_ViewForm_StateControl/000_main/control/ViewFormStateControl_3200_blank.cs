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

// 2018.4.28
// 注意
//     本ファイルは他の分類方法とは異なり、Group Nodeに関連する部分を集約しています。
//     ・・・命令カテゴリによるファイル分割手法は、ステート数が増えると管理不可能だと感じた。

public partial class ViewFormStateControl {

    public static ViewFormStataMenuBlankItem m_viewFormStataMenuBlankItem;

    // at S_IDLE2
    void br_isClickOnBlank(Action<int,bool> st)
    {
        if (HasNextState()) return;
        if (m_idleSc.m_result == IdleStateControl.RESULT.CLICK_ONBLANK)
        {
            SetNextState(st);
        }
    }

    // at S_ShowBlankMenu
    public Point m_pos_at_menu_on_bmp;
    void statemenuBlank_init()
    {
        m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.UNKNOWN;
        G.view_form.blankMenu.Show(G.view_form,  G.view_form.PointToClient(PointUtil.Add_XY(Cursor.Position,-20,-10)));
        m_pos_at_menu_on_bmp = G.point_on_bmp;
        ((ToolStripDropDownMenu)(G.view_form.newStateToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.newStateToolStripMenuItem.DropDown)).ShowImageMargin = false;

        { //ステートタイプ表示 ON/OFF
            var b = G.template_has_statetyp;
            G.view_form.normalToolStripMenuItem.Visible   = b;
            G.view_form.loopToolStripMenuItem.Visible     = b; 
            G.view_form.gosubToolStripMenuItem.Visible    = b;
            G.view_form.substartToolStripMenuItem.Visible = b;
            G.view_form.subendToolStripMenuItem.Visible   = b;
            //G.view_form.startToolStripMenuItem.Visible    = b;
            //G.view_form.endToolStripMenuItem.Visible      = b;    
            G.view_form.pASSToolStripMenuItem.Visible     = b;
            G.view_form.sTOPToolStripMenuItem.Visible     = b;
            
            if (b)
            {
                G.view_form.startToolStripMenuItem.Visible =  !G.excel_program.GetStateList().Contains("S_START");
                G.view_form.endToolStripMenuItem.Visible =  !G.excel_program.GetStateList().Contains("S_END");
            }
            else
            {
                G.view_form.startToolStripMenuItem.Visible = false;
                G.view_form.endToolStripMenuItem.Visible   = false;
            }
        }



        {
            //クリップボードにステート名があり、ベースとして使用可能かを検査
            var b2 = false;
            var s = Clipboard.GetText();
            if (StateUtil.IsValidStateName(s) &&  !AltState.IsAltState(s) && G.excel_program.CheckExists(s) && s.StartsWith("S_"))
            {
                //typ指定なしであること
                var typ = G.excel_program.GetString(s, G.STATENAME_statetyp);
                if (string.IsNullOrEmpty(typ))
                {
                    //ベースがない事
                    var basestate = G.excel_program.GetString(s, G.STATENAME_basestate);
                    if (string.IsNullOrEmpty(basestate))
                    {
                        b2 = true;
                    }
                }
            }

            G.view_form.pasteUsingBaseModeToolStripMenuItem.Visible = b2;
        }


        ((ToolStripDropDownMenu)(G.view_form.stateToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.stateToolStripMenuItem.DropDown)).ShowImageMargin = false;

        ((ToolStripDropDownMenu)(G.view_form.historyToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.historyToolStripMenuItem.DropDown)).ShowImageMargin = false;

        ((ToolStripDropDownMenu)(G.view_form.trackToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.trackToolStripMenuItem.DropDown)).ShowImageMargin = false;

        ((ToolStripDropDownMenu)(G.view_form.focusToolStripMenuItem1.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.focusToolStripMenuItem1.DropDown)).ShowImageMargin = false;




    }
    bool statemenuBlank_wait()
    {
        var sm = G.view_form.blankMenu.Bounds;
        if (G.view_form.newStateToolStripMenuItem.DropDown!=null && G.view_form.newStateToolStripMenuItem.DropDown.Visible)
        {
            var ddb = G.view_form.newStateToolStripMenuItem.DropDown.Bounds;
            var nm = RectangleUtil.Combine(sm,ddb);
            sm = nm;
        }
        if (G.view_form.stateToolStripMenuItem.DropDown!=null && G.view_form.stateToolStripMenuItem.DropDown.Visible)
        {
            var ddb = G.view_form.stateToolStripMenuItem.DropDown.Bounds;
            var nm = RectangleUtil.Combine(sm,ddb);
            sm = nm;
        }
        if (G.view_form.historyToolStripMenuItem.DropDown!=null && G.view_form.historyToolStripMenuItem.DropDown.Visible)
        {
            var ddb = G.view_form.historyToolStripMenuItem.DropDown.Bounds;
            var nm = RectangleUtil.Combine(sm,ddb);
            sm = nm;
        }
        if (G.view_form.trackToolStripMenuItem.DropDown!=null && G.view_form.trackToolStripMenuItem.DropDown.Visible)
        {
            var ddb = G.view_form.trackToolStripMenuItem.DropDown.Bounds;
            var nm = RectangleUtil.Combine(sm,ddb);
            sm = nm;
        }
        if (G.view_form.focusToolStripMenuItem1.DropDown!=null && G.view_form.focusToolStripMenuItem1.DropDown.Visible)
        {
            var ddb = G.view_form.focusToolStripMenuItem1.DropDown.Bounds;
            var nm = RectangleUtil.Combine(sm,ddb);
            sm = nm;
        }

        var pad = 10;
        var rec = new Rectangle(sm.Left-pad,sm.Top-pad,sm.Width + 2 * pad,sm.Height + 2 * pad);

        if (rec.Contains(Cursor.Position))
        {
            return m_viewFormStataMenuBlankItem != ViewFormStataMenuBlankItem.UNKNOWN;
        }
        else
        {
            G.view_form.blankMenu.Close();
            if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.UNKNOWN)
            {
                m_viewFormStataMenuBlankItem =  ViewFormStataMenuBlankItem.CANCEL;
            }
            return true;
        }
    }
    void br_Leave(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.LEAVE)
        {
            SetNextState(st);
        }
    }
    void br_NewState(Action<int,bool> st)
    {
        //if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEWSTATE)
        //{
        //    SetNextState(st);
        //}
        if (
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEW_STATE
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEW_EMBED
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEW_COMMENT
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_LOOP
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_GOSUB
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_SUBSTART
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_SUBRETURN
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_START
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_END
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_PASS
            ||
            m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.TYP_STOP
        )
        {
            SetNextState(st);
        }
    }
    void br_HistoryBack(Action<int,bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.HISTORY_BACK)
        {
            SetNextState(st);
        }
    }
    void br_HistoryShow(Action<int,bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.HISTORY_SHOW)
        {
            SetNextState(st);
        }
    }
    void br_HistoryForward(Action<int,bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.HISTORY_FORWARD)
        {
            SetNextState(st);
        }
    }
    void br_ImportClipboard(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.IMPORT_CLIPBOARD)
        {
            SetNextState(st);
        }
    }
    void br_ImportClipboard_wo_outflow(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.IMPORT_CLIPBOARD_WO_OUTFLOW)
        {
            SetNextState(st);
        }
    }
    void br_ImportClipboard_as_base(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.IMPORT_AS_BASESTATE)
        {
            SetNextState(st);
        }
    }
    void br_FocusHead(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.FOCUS_HEAD)
        {
            SetNextState(st);
        }
    }
    void br_FocusTail(Action<int, bool> st)
    {
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.FOCUS_TAIL)
        {
            SetNextState(st);
        }
    }

    //at S_Leave
    void statemenu_leave()
    {
        m_center_focus_group = G.node_get_cur_dirpath();
        
        G.node_leave_group();
        G.view_form.tabControl.SelectedTab = G.view_form.tabPageMain;

        try { 
            FocusTrack.Record_curpath();
            FocusTrack.Record( AltState.MakeAltStateName( GroupNodeUtil.get_last_path(m_center_focus_group).Replace("/",""))  );
        } catch (SystemException e)
        {
            G.NoticeToUser("{4A11393F-DCEE-497E-AD23-C289CA686E94} " + e.Message);
        }
        //History.ReqToSave("Left a path");
    }

    //at S_NewState
    void statemenu_newstate()
    {
        string S_LOOP     = "S_LOP000";
        string S_GOSUB    = "S_GSB000";
        string S_SUBSTART = "S_SBS000";
        string S_RETURN   = "S_RET000";
        string S_START    = "S_START";
        string S_END      = "S_END";
        string S_PASS     = "S_PAS000";
        string S_STOP     = "S_STP000";
        string[] ALL_TYPSTATE = new string[] {
            S_LOOP,
            S_GOSUB,
            S_SUBSTART,
            S_RETURN,
            S_START,
            S_END,
            S_PASS,
            S_STOP
        };

        var refname = "S_0000";
        if (!string.IsNullOrEmpty(G.latest_focuse_state))
        {
            if (!G.latest_focuse_state.Contains("/"))
            { 
                if ( // state-typのどれでもない
                    Array.TrueForAll( ALL_TYPSTATE, i=>!G.latest_focuse_state.StartsWith(i))
                    )
                {
                    refname = G.latest_focuse_state;
                }
            }
            else
            {
#if DEBUG
                G.NoticeToUser("Debug: reference node has /");
#endif

            }
        }

        var state_typ_val = string.Empty;
        string state_cmt  = null;
        switch(m_viewFormStataMenuBlankItem)
        {
            case ViewFormStataMenuBlankItem.TYP_LOOP:      refname=S_LOOP;     state_typ_val = WordStorage.Store.state_typ_loop;      state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_GOSUB:     refname=S_GOSUB;    state_typ_val = WordStorage.Store.state_typ_gosub;     state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_SUBSTART:  refname=S_SUBSTART; state_typ_val = WordStorage.Store.state_typ_substart;  state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_SUBRETURN: refname=S_RETURN;   state_typ_val = WordStorage.Store.state_typ_subreturn; state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_START:     refname=S_START;    state_typ_val = WordStorage.Store.state_typ_start;     state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_END:       refname=S_END;      state_typ_val = WordStorage.Store.state_typ_end;       state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_PASS:      refname=S_PASS;     state_typ_val = WordStorage.Store.state_typ_pass;      state_cmt = ""; break;
            case ViewFormStataMenuBlankItem.TYP_STOP:      refname=S_STOP;     state_typ_val = WordStorage.Store.state_typ_stop;      state_cmt = ""; break;
        }

        var header = string.Empty;
        if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEW_COMMENT)
        {
            header = "C_";
        }
        else if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.NEW_EMBED)
        {
            header = "E_";
        }
        else
        {
            header = "S_";
        }
        refname = header + (refname.Length > 2 ?  refname.Substring(2) : refname);

        //var newstate = G.excel_program.NewState(G.state_org_list[0],"S_0",G.node_get_cur_dirpath());
        var newstate = G.excel_program.NewState(refname,G.node_get_cur_dirpath());

        if (!string.IsNullOrEmpty(state_typ_val))
        { 
            G.excel_program.SetString(newstate,G.STATENAME_statetyp,state_typ_val);
        }
        if (state_typ_val == WordStorage.Store.state_typ_loop || state_typ_val == WordStorage.Store.state_typ_gosub)
        {
            G.excel_program.SetString(newstate,G.STATENAME_gosubstate,"?");//未定
        }

        if (state_cmt!=null)
        {
            G.excel_program.SetString(newstate,G.STATENAME_statecmt, state_cmt);
        }

        if (G.option_set_default_comment==false) //コメント削除
        {
            G.excel_program.SetString(newstate,G.STATENAME_statecmt, "");
        }

        var pos = m_pos_at_menu_on_bmp;//GetPointerOnMainBmp();
        stateview.CopyRenameStateSave.set_new_state_psition(pos,newstate);

        G.UpdateExcelPos(newstate,pos,true);

        //History.ReqToSave("Created a new state");
        History2.SaveForce_new(newstate);
    }

    //at S_HistoryBack
    History2.Item m_saved_item;
    void statemenu_historyback()
    {
        var item = History2.Back();
        if (item == null) return;
        statemenu_historywork(item,false);
        m_saved_item = item;
    }

    private void statemenu_historywork(History2.Item item, bool bForward_or_back)
    {
        //if (item.target_pathdir != G.node_get_cur_dirpath())
        //{
        //    G.node_set_curdir(item.target_pathdir);
        //    G.NoticeToUser("History back .. path adjust ..." + item.action);
        //}
        //else
        {
            G.m_target_pathdir = item.target_pathdir;

            G.m_excel_cell_cache_dic = clone_dic(item.excel_cache);

            G.state_location_list = null;
            G.fillter_state_location_list = clone_dic(item.fillter_state_location_list);

            G.state_working_list_reflesh();

            if (item == History2.Get_initialized_item())
            {
                G.Dirty_clear_all();
            }
            else
            {
                if (bForward_or_back) //前進
                {
                    if (item.bModified_pos)   G.bDirty_by_edited_pos_only = true;
                    if (item.bModified_value) G.bDirty_by_modified_value  = true;
                }
                else
                {                    // 後進
                    if (m_saved_item!=null)
                    {
                        if (m_saved_item.bModified_pos)   G.bDirty_by_edited_pos_only = true;
                        if (m_saved_item.bModified_value) G.bDirty_by_modified_value  = true;
                    }
                }
            }

            G.NoticeToUser("History back .." + item.action);
        }
    }


    //at S_HistoryForward
    void statemenu_historyforward()
    {
        var item = History2.Forward();
        if (item == null) return;
        statemenu_historywork(item,true);
        m_saved_item = item;
    }
    public bool statemenu_importclipboard(bool wo_outflow)
    {
        return CopyAndPasteWork.import(m_pos_at_menu_on_bmp, wo_outflow);
    }
    public void paste_as_base()
    {
        var basestate = Clipboard.GetText(); //メニュー表示で検査済み
        var newstate = G.excel_program.NewState(basestate,G.node_get_cur_dirpath());
        G.excel_program.SetString(newstate,G.STATENAME_basestate,basestate);
        var pos = m_pos_at_menu_on_bmp;
        stateview.CopyRenameStateSave.set_new_state_psition(pos,newstate);
        G.UpdateExcelPos(newstate,pos,true);
        History2.SaveForce_new(newstate);
    }
    #region clone
    private static Dictionary<int, ExcelCellCacheItem> clone_dic(Dictionary<int, ExcelCellCacheItem> src)
    {
        var newdic = new Dictionary<int, ExcelCellCacheItem>();
        foreach(var p in src )
        {
            var cell = p.Value.Clone();
            newdic.Add(p.Key, cell);
        }
        return newdic;
    }
    private static Dictionary<string, Dictionary<string, PointF> > clone_dic(Dictionary<string, Dictionary<string, PointF> > src)
    {
        var newdic = new Dictionary<string, Dictionary<string, PointF> >();
        foreach(var p in src)
        {
            var tmp = new  Dictionary<string, PointF>();
            foreach(var q in p.Value)
            {
                tmp.Add(q.Key,q.Value);
            }
            newdic.Add(p.Key, tmp);
        }
        return newdic;
    }
    #endregion

    #region OuterMenu
    void statemenuOuter_init()
    {
        m_viewFormStataMenuBlankItem = ViewFormStataMenuBlankItem.UNKNOWN;
        G.view_form.OuterMenu.Show(G.view_form,  G.view_form.PointToClient(PointUtil.Add_XY(Cursor.Position,-20,-10)));
        m_pos_at_menu_on_bmp = G.point_on_bmp;
        ((ToolStripDropDownMenu)(G.view_form.newStateToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.newStateToolStripMenuItem.DropDown)).ShowImageMargin = false;
    }
    bool statemenuOuter_wait()
    {
        var sm = G.view_form.OuterMenu.Bounds;
        var pad = 10;
        var rec = new Rectangle(sm.Left-pad,sm.Top-pad,sm.Width + 2 * pad,sm.Height + 2 * pad);

        if (rec.Contains(Cursor.Position))
        {
            return m_viewFormStataMenuBlankItem != ViewFormStataMenuBlankItem.UNKNOWN;
        }
        else
        {
            G.view_form.OuterMenu.Close();
            if (m_viewFormStataMenuBlankItem == ViewFormStataMenuBlankItem.UNKNOWN)
            {
                m_viewFormStataMenuBlankItem =  ViewFormStataMenuBlankItem.CANCEL;
            }
            return true;
        }
    }
    void br_isOuter(Action<int, bool> st)
    {
         var pos = G.point_on_bmp;
         if (
            pos.X < 0 || pos.X > G.bitmap_width
            ||
            pos.Y < 0 || pos.Y > G.bitmap_height
            ) {
            SetNextState(st);
        }
    }

    #endregion
}
