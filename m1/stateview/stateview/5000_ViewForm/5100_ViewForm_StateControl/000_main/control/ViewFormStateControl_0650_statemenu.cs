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

public enum ViewFormStateMenuItem
{
    UNKNOWN,
    EDITFULL,
    EDITBRANCH,
    ADDBRANCH,

    REMOVE_COMMENT,

    COPY,
    DELETE,
    RENAME,
    REFACTOR,
    MOVETO,

    CHANGE_STATE,
    CHANGE_EMBED,
    CHANGE_COMMENT,

    LINK,
    EXPORTCLIPBAORD,
    COPY_STATENAME,
    VIEWEXSTATE,
    OPENSRC,
    CANCEL,

    SET_STOP,
    RESET_STOP,
    SET_BASE,
    RESET_BASE,

    SET_PASS,
    RESET_PASS,

    FOCUS,

}

public enum ViewFormStateMenuGFItem
{
    UNKNOWN,
    GROUPING,
    DELETE,
    UNGROUPING,
    EXPORTCLIPBOARD,
    MOVETO,
    ALIGN_HORIZONTALLY,
    ALIGN_VERTICALLY,
    COMMENT_OUT,
    CANCEL
}
public enum ViewFormStateMenuGNItem
{
    UNKNOWN,
    ENTER,
    UNGROUPING,
    EDIT,
    MOVETO,
    COPY,
    DELETE,
    EXPORTCLIPBOARD,
    CANCEL
}
public enum ViewFormStataMenuBlankItem
{
    UNKNOWN,
    LEAVE,
    NEWSTATE,//廃止へ

    NEW_STATE,
    NEW_EMBED,
    NEW_COMMENT,

    TYP_LOOP,
    TYP_GOSUB,
    TYP_SUBSTART,
    TYP_SUBRETURN,
    TYP_START,
    TYP_END,
    TYP_PASS,
    TYP_STOP,

    HISTORY_SHOW,
    HISTORY_BACK,
    HISTORY_FORWARD,

    IMPORT_CLIPBOARD,
    IMPORT_CLIPBOARD_WO_OUTFLOW,
    IMPORT_AS_BASESTATE,

    TRACK_SHOW,
    TRACK_BACK,
    TRACK_FORWARD,

    FOCUS_HEAD,
    FOCUS_TAIL,

    CANCEL
}

public partial class ViewFormStateControl {

    //public static ViewFormStateMenuItem m_viewFormStateMenuItem;
    public static ViewFormStateMenuItem m_viewFormStateMenuItem {
        get;
        set;
    }

    void statemenu_init()
    {
        if (m_focus_state != GetStateOnPointer())
        {
            m_viewFormStateMenuItem = ViewFormStateMenuItem.CANCEL;
        }
        else
        {
            m_viewFormStateMenuItem = ViewFormStateMenuItem.UNKNOWN;

            G.linkItemsOnStateMenu.Refresh(m_focus_state);
            G.linkItemsOnStateMenu.ModifyMenuItemInViewForm();

            var state_typ = G.excel_program.GetString(m_focus_state,G.STATENAME_statetyp);

            //Branch
            var item = G.view_form.StateMenu.Items.Find("editBranchToolStripMenuItem",false);
            if (item!=null && item.Length == 1)
            { 
                //G.view_form.StateMenu.Items[1].Enabled = (!string.IsNullOrEmpty(m_focus_state) && m_focus_state[0]=='S');

                //branchメニューアイテムは、 S_ で、 state-typがnullまたはbaseの時
                item[0].Visible = (!string.IsNullOrEmpty(m_focus_state) && m_focus_state[0]=='S' && (string.IsNullOrEmpty(state_typ)|| state_typ==WordStorage.Store.state_typ_base )   );
            }
            //外部ステートのフォーカス移動            
            item = G.view_form.StateMenu.Items.Find("viewThisExstateToolStripMenuItem",false);
            if (item!=null && item.Length == 1)
            {
                var style = G.node_get_style(m_focus_state);
                item[0].Visible = style != SS.STYLE.NORMAL;
            }
            //リンク先
            item = G.view_form.StateMenu.Items.Find("MenuItemLink",false);
            if (item!=null && item.Length==1)
            {
                var reflink = G.excel_program.GetStringIfSubname(m_focus_state,"state-ref");
                item[0].Visible = !string.IsNullOrEmpty(reflink);
            }
            //コメント削除
            {
                item = G.view_form.StateMenu.Items.Find("removeCommentToolStripMenuItem",false);
                if (item!=null && item.Length == 1)
                {
                    var cmt = G.excel_program.GetString(m_focus_state,G.STATENAME_statecmt);
                    item[0].Visible = !string.IsNullOrEmpty(cmt);                                       
                }
            }

            statemenu_init_focus();

            G.view_form.StateMenu.Show(G.view_form,  G.view_form.PointToClient(PointUtil.Add_XY(Cursor.Position,-20,-10)));

            ((ToolStripDropDownMenu)(G.view_form.changeToolStripMenuItem.DropDown)).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)(G.view_form.changeToolStripMenuItem.DropDown)).ShowImageMargin = false;

            ((ToolStripDropDownMenu)(G.view_form.stateToolStripMenuItem1.DropDown)).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)(G.view_form.stateToolStripMenuItem1.DropDown)).ShowImageMargin = false;

            //ステートのstate-typはデフォルト全OFF
            for(var n = 0; n < G.view_form.stateToolStripMenuItem1.DropDownItems.Count; n++)
            {
                G.view_form.stateToolStripMenuItem1.DropDownItems[n].Visible = false;
            }

            if (m_focus_state[0] == 'S')
            {
                if (string.IsNullOrEmpty(state_typ))
                {
                    G.view_form.setStopToolStripMenuItem.Visible = true;
                    G.view_form.setPassToolStripMenuItem.Visible = true;
                    //G.view_form.setBaseToolStripMenuItem.Visible = true;
                }
                else if (state_typ == WordStorage.Store.state_typ_stop)
                {
                    G.view_form.resetStopToolStripMenuItem.Visible = true;
                }
                else if (state_typ == WordStorage.Store.state_typ_pass)
                {
                    G.view_form.resetPassToolStripMenuItem.Visible = true;
                }
                //else if (state_typ == WordStorage.Store.state_typ_base)
                //{
                //    G.view_form.resetBaseToolStripMenuItem.Visible = true;
                //}
            }
        }
    }
    bool wait_statemenu_done()
    {
        Rectangle sm;
        var m1 = G.view_form.StateMenu.Bounds;
        var m2 = G.brancApiCollector.GetBounds();
        var m3 = G.linkItemsOnStateMenu.GetBounds();
        var m4 = G.view_form.stateToolStripMenuItem1.DropDown.Bounds;

        if (m2!=null)
        {
            m1 = RectangleUtil.Combine(m1, (Rectangle)m2 );
        }
        if (m3!=null)
        {
            m1 = RectangleUtil.Combine(m1, (Rectangle)m3);
        }
        if (m4!=null)
        {
            m1 = RectangleUtil.Combine(m1, (Rectangle)m4);
        }

        if (G.view_form.changeToolStripMenuItem.DropDown.Visible)
        {
            m1 = RectangleUtil.Combine(m1, G.view_form.changeToolStripMenuItem.DropDown.Bounds);
        }

        wait_statemenu_focus_bounds(ref m1);

        sm = m1;

        var pad = 10;
        var rec = new Rectangle(sm.Left-pad,sm.Top-pad,sm.Width + 2 * pad,sm.Height + 2 * pad);

        if (rec.Contains(Cursor.Position))
        {
            return m_viewFormStateMenuItem != ViewFormStateMenuItem.UNKNOWN;
        }
        else
        {
            G.view_form.StateMenu.Close();
            if (m_viewFormStateMenuItem == ViewFormStateMenuItem.UNKNOWN)
            { 
                m_viewFormStateMenuItem = ViewFormStateMenuItem.CANCEL;
            }
            return true;
        }
    }
    void br_OpenEditFull(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.EDITFULL)
        {
            SetNextState(st);
        }
    }
    void br_EditBranch(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.EDITBRANCH)
        {
            SetNextState(st);
        }
    }
    void br_AddBranch(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.ADDBRANCH)
        {
            SetNextState(st);
        }
    }
    void br_Copy(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.COPY)
        {
            SetNextState(st);
        }
    }
    void br_Delete(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.DELETE)
        {
            SetNextState(st);
        }
    }
    void br_Rename(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.RENAME)
        {
            SetNextState(st);
        }
    }
    void br_Refactor(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.REFACTOR)
        {
            SetNextState(st);
        }
    }
    void br_Link(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.LINK)
        {
            SetNextState(st);
        }
    }
    void br_ExportClipboard_SM(Action<int, bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.EXPORTCLIPBAORD)
        {
            SetNextState(st);
        }
    }
    void br_CopyStateName(Action<int, bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.COPY_STATENAME)
        {
            SetNextState(st);
        }
    }
    void statemenu_copy()
    {
        var newname = G.excel_program.Copy(m_focus_state);
        var pos = stateview.CopyRenameStateSave.calc_new_dst_position(m_focus_state,newname,true);
        if (pos!=null) G.UpdateExcelPos(newname,(PointF)pos,true);
        History.ReqToSave("Copied a state");
   }

    //stateview._5150_DirForm.dialog.DirReformDeleteConfirmDialog m_delconfDlg;
    stateview._5000_ViewForm.dialog.OKCancelForm m_delconfDlg;
    void statemenu_delete_init()
    {
        var nl = Environment.NewLine;
        m_delconfDlg = new stateview._5000_ViewForm.dialog.OKCancelForm(); //new stateview._5150_DirForm.dialog.DirReformDeleteConfirmDialog();
        m_delconfDlg.Text = G.Localize("del_Confirmation");
        m_delconfDlg.textBox1.Text = G.Localize("del_Is_it_ok_to_delete") + nl + nl + m_focus_state;
        m_delconfDlg.Show(G.view_form);
        m_delconfDlg.Location = Cursor.Position;

    }
    bool wait_statemenu_delete_done()
    {
        if (m_delconfDlg.DialogResult == DialogResult.None) return false;
        if (m_delconfDlg.DialogResult == DialogResult.OK)
        {
            G.excel_program.Delete(m_focus_state);
            m_okCancel = true;
            //History.ReqToSave("Delete a state");
            History2.SaveForce_delete(m_focus_state);
        }
        else
        {
            m_okCancel = false;
        }
        return true;
    }
    bool check_hasbase()
    {
        return StateUtil.IsBaseState(m_focus_state);
    }
    void statemenu_delete_base_init()
    {
        var nl = Environment.NewLine;
        m_okcancelForm = new stateview._5000_ViewForm.dialog.OKCancelForm();
        m_okcancelForm.DialogResult = DialogResult.None;
        m_okcancelForm.textBox1.Text = "このステートはベースとして利用されています。" + nl + m_focus_state + nl +"強硬しますか？";
        m_okcancelForm.Show(G.view_form);
        m_okcancelForm.Location = Cursor.Position;
    }
    bool wait_statemenu_delete_base_done()
    {
        if (m_okcancelForm.DialogResult == DialogResult.None) return false;
        m_okCancel = m_okcancelForm.DialogResult == DialogResult.OK;
        return true;
    }
    #region Refactor
    stateview._5000_ViewForm.dialog.RefactorForm m_refactDlg;
    void statemenu_refactor()
    {
        m_refactDlg = new stateview._5000_ViewForm.dialog.RefactorForm();
        m_refactDlg.m_name = m_focus_state;
        m_refactDlg.m_comment = G.excel_program.GetString(m_focus_state,G.STATENAME_statecmt);
        m_refactDlg.m_branch  = G.excel_program.GetString(m_focus_state,G.STATENAME_branch);
        m_refactDlg.Show(G.view_form);
        m_refactDlg.Location = Cursor.Position;
    }
    bool wait_statemenu_refactor_done()
    {
        if (m_refactDlg.DialogResult == DialogResult.None) return false;
        if (m_refactDlg.DialogResult == DialogResult.OK)
        {
            var newstate = m_refactDlg.m_name;
            var newcmt   = m_refactDlg.m_comment;
            var newbranch= m_refactDlg.m_branch;
            if (m_focus_state != newstate)
            {
                StateUtil.Refactor(m_focus_state,newstate);
                stateview.CopyRenameStateSave.calc_new_dst_position(m_focus_state,newstate,false);
            }
            G.excel_program.SetString(newstate,G.STATENAME_statecmt,newcmt);
            G.excel_program.SetString(newstate,G.STATENAME_branch,newbranch);
            m_okCancel = true;

            History.ReqToSave("Changed state name, comment or branch");
        }
        else
        {
            m_okCancel = false;
        }
        return true;
    }

    #region Focus
    void statemenu_init_focus()
    {
        var input_dic = DictionaryUtil.Get(G.state_input_src_list,m_focus_state);
        var output_dic = DictionaryUtil.Get(G.state_output_dst_list,m_focus_state);
        if (output_dic!=null && output_dic.Count>0)
        {
            statemenu_init_focus_add_dropitems(G.view_form.nextStateToolStripMenuItem, output_dic, InOutBaseData.ATTRIB.nextstate);
            statemenu_init_focus_add_dropitems(G.view_form.branchToolStripMenuItem, output_dic, InOutBaseData.ATTRIB.branch);
            statemenu_init_focus_add_dropitems(G.view_form.goSubToolStripMenuItem1, output_dic, InOutBaseData.ATTRIB.gosub);
            statemenu_init_focus_add_dropitems(G.view_form.baseToolStripMenuItem, output_dic, InOutBaseData.ATTRIB._base);
        }
        if (input_dic!=null && input_dic.Count>0)
        {
            statemenu_init_focus_add_dropitems(G.view_form.inputToolStripMenuItem, input_dic, InOutBaseData.ATTRIB.nextstate, InOutBaseData.ATTRIB.branch, InOutBaseData.ATTRIB.gosub);
            statemenu_init_focus_add_dropitems(G.view_form.baseRefferdToolStripMenuItem, input_dic, InOutBaseData.ATTRIB._base);
        }
        if (input_dic==null && output_dic==null)
        {
            G.view_form.focusToolStripMenuItem.Visible = false;
        }
        else
        {
            G.view_form.focusToolStripMenuItem.Visible = true;
        }

        ((ToolStripDropDownMenu)(G.view_form.focusToolStripMenuItem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(G.view_form.focusToolStripMenuItem.DropDown)).ShowImageMargin = false;

    }
    void statemenu_init_focus_add_dropitems(ToolStripMenuItem menuitem, List<InOutBaseData> list,  InOutBaseData.ATTRIB attr, InOutBaseData.ATTRIB? attr2 = null, InOutBaseData.ATTRIB? attr3 = null)
    {
        var finds = list.FindAll(i=>i.attrib == attr);
        if (attr2!=null)
        {
            var finds2 = list.FindAll(i=>i.attrib == (InOutBaseData.ATTRIB)attr2);
            if (finds ==null) finds = new List<InOutBaseData>();
            finds.AddRange(finds2);
        }
        if (attr3!=null)
        {
            var finds3 = list.FindAll(i=>i.attrib == (InOutBaseData.ATTRIB)attr3);
            if (finds ==null) finds = new List<InOutBaseData>();
            finds.AddRange(finds3);
        }

        menuitem.Visible = true;
        if (finds == null || finds.Count==0)
        {
            menuitem.Visible = false;
            return;
        }

        menuitem.DropDownItems.Clear();
        foreach(var i in finds)
        {
            if (stateview.StateUtil.IsValidStateName(i.state) && !AltState.IsAltState(i.state))
            { 
                menuitem.DropDownItems.Add(i.state);
            }
        }
        for(var i = 0; i < menuitem.DropDownItems.Count; i++)
        {
            menuitem.DropDownItems[i].Click += statemenu_init_focus_dropitems_click;
            var s = menuitem.DropDownItems[i].Text;
            var cmt = G.excel_program.GetString(s,G.STATENAME_statecmt);
            if (!string.IsNullOrEmpty(cmt))
            {
                cmt += Environment.NewLine;
            }
            cmt += G.Localize("inout_ctrl_copy");
            {
                menuitem.DropDownItems[i].ToolTipText = cmt;
            }
        }

        ((ToolStripDropDownMenu)(menuitem.DropDown)).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)(menuitem.DropDown)).ShowImageMargin = false;
    }
    string m_statemenu_focus_state;
    void statemenu_init_focus_dropitems_click(object sender, EventArgs e)
    {
        try
        {
            var tm = sender as ToolStripMenuItem;
            m_statemenu_focus_state = tm.Text;
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.FOCUS;
        } catch (SystemException e2)
        {
            G.NoticeToUser_warning("{2AAB2A25-C320-410C-933F-CB8B9F123185}" + e2.Message);
        }
    }

    void wait_statemenu_focus_bounds(ref Rectangle ref_m)
    {
        Rectangle m = ref_m;

        Func<ToolStripMenuItem, Rectangle?> getbound = (t) => {
            if (t.DropDown.Visible)
            {
                return t.DropDown.Bounds;
            }
            return null;
        };

        Rectangle? m_focus     = getbound(G.view_form.focusToolStripMenuItem);
        Rectangle? m_gosub     = getbound(G.view_form.goSubToolStripMenuItem1);
        Rectangle? m_nextstate = getbound(G.view_form.nextStateToolStripMenuItem);
        Rectangle? m_branch    = getbound(G.view_form.branchToolStripMenuItem);
        Rectangle? m_input     = getbound(G.view_form.inputToolStripMenuItem);
        Rectangle? m_base      = getbound(G.view_form.baseToolStripMenuItem);
        Rectangle? m_refbase   = getbound(G.view_form.baseRefferdToolStripMenuItem);

        Action<Rectangle?> addrec = (r) => {
            if (r!=null)
            {
                m = RectangleUtil.Combine(m,(Rectangle)r);
            }
        };

        addrec(m_focus);
        addrec(m_gosub);
        addrec(m_nextstate);
        addrec(m_branch);
        addrec(m_input);
        addrec(m_base);
        addrec(m_refbase);

        ref_m = m;
    }
    void br_Focus(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.FOCUS)
        {
            SetNextState(st);
        }
    }
    void statemenu_focus_state()
    {
        if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
        {
            Clipboard.SetText(m_statemenu_focus_state);
            m_okCancel = false;
        }
        else
        { 
            G.vf_sc.m_center_focus_state = m_statemenu_focus_state;
            m_okCancel = true;
        }
    }

    #endregion

    #region コメント削除
    void statemenu_remove_comment()
    {
        var cmt = G.excel_program.GetString(m_focus_state,G.STATENAME_statecmt);
        if (!string.IsNullOrEmpty(cmt))
        {
            G.excel_program.SetString(m_focus_state,G.STATENAME_statecmt,"");
            History2.SaveForce_modify_value("Remove the state comment");
        }
    }
    void br_RemoveComment(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.REMOVE_COMMENT)
        {
            SetNextState(st);
        }
    }
    #endregion


    #endregion

    #region Moveto
    stateview._5000_ViewForm.dialog.MovetoForm m_movetoDlg;
    void statemenu_moveto_init()
    {
        m_movetoDlg = new stateview._5000_ViewForm.dialog.MovetoForm();
        m_movetoDlg.m_pathdir = G.node_get_dirpath(m_focus_state);
        m_movetoDlg.Show(G.view_form);
        m_movetoDlg.Location = Cursor.Position;
    }
    bool wait_statemenu_moveto_done()
    {
        if (m_movetoDlg.DialogResult == DialogResult.None) return false;
        if (m_movetoDlg.DialogResult == DialogResult.OK)
        {
            var pathdir = m_movetoDlg.m_pathdir;
            G.node_moveto_group(m_focus_state,pathdir);
            m_okCancel = true;
            History.ReqToSave("Moved a state into a group");
        }
        else
        {
            m_okCancel = false;
        }
        return true;
    }
    #endregion
    void statemenu_exportsm()
    {
        G.ExportToClipboard(new List<string> { m_focus_state });
    }
    
    #region GF

    public static ViewFormStateMenuGFItem m_viewFormStateMenuGFItem;

    void statemenuGF_init()
    {
        var state = GetStateOnPointer();
        if (m_group_focus_list!=null && m_group_focus_list.Contains(state)) {
            m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.UNKNOWN;
            G.view_form.stateMenuGF.Show(G.view_form,  G.view_form.PointToClient(PointUtil.Add_XY(Cursor.Position,-20,-10)));
        }
        else {
            m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.CANCEL;
        }
    }
    bool statemenuGF_wait()
    {
        var sm = G.view_form.stateMenuGF;
        var pad = 10;
        var rec = new Rectangle(sm.Left-pad,sm.Top-pad,sm.Width + 2 * pad,sm.Height + 2 * pad);

        if (rec.Contains(Cursor.Position))
        {
            return m_viewFormStateMenuGFItem != ViewFormStateMenuGFItem.UNKNOWN;
        }
        else
        {
            G.view_form.stateMenuGF.Close();
            m_viewFormStateMenuGFItem = ViewFormStateMenuGFItem.CANCEL;
            return true;
        }
    }
    stateview._5000_ViewForm.dialog.InputGroupNameForm m_inputgroupnameForm;
    stateview._5000_ViewForm.dialog.OKCancelForm m_okcancelForm;
    void gfstatemenu_grouping_init()
    {
        m_inputgroupnameForm = new stateview._5000_ViewForm.dialog.InputGroupNameForm();//　　　.dialog.OKCancelForm();
        m_inputgroupnameForm.m_groupname_list = G.node_get_groupsOnTargetDir();

        //グループ名作成
        var cand_groupname = "hoge";
        var list = G.node_get_groupsOnTargetDir();
        for(var loop=0; loop<=1000; loop++)
        {
            if (loop==1000) { G.NoticeToUser_warning("{E7A609A7-9C4D-4B00-93F3-DD69E75C1992}"); break; }
            if (list.Contains(cand_groupname))
            {
                cand_groupname = StateUtil.MakeNewName(cand_groupname);
            }
            else
            {
                break;
            }
        }
        m_inputgroupnameForm.m_groupname = cand_groupname;
 
        //ダイアログ表示
        m_inputgroupnameForm.Show(G.view_form);
        m_inputgroupnameForm.Location = Cursor.Position;
    }
    bool gfstatemenu_grouping_wait()
    {
        var ret = m_inputgroupnameForm.DialogResult != DialogResult.None;
        if (!ret) return false;

        if (m_inputgroupnameForm.DialogResult == DialogResult.OK)
        {
            var group = m_inputgroupnameForm.m_groupname;
            var comment = m_inputgroupnameForm.m_comment;
            G.node_grouping(group,m_group_focus_list,m_group_focus_click_state,comment);
            m_request_redrawNodeTreeView = true;
            m_okCancel = true;

            //History.ReqToSave("Made a group");
            History2.SaveForce_grouping("Make g:" + group);
        }
        else
        {
            m_okCancel = false;
        }

        return ret;
    }
    void gfstatemenu_moveto_init()
    {

    }
    bool gfstatemenu_moveto_wait()
    {
        return true;
    }
    void gfstatemenu_ungrouping_init()
    {
        m_okcancelForm = new stateview._5000_ViewForm.dialog.OKCancelForm();
        m_okcancelForm.Show(G.view_form);
        m_okcancelForm.Location = Cursor.Position;
    }
    bool gfstatemenu_ungrouping_wait()
    {
        return m_okcancelForm.DialogResult != DialogResult.None;
    }
    void gfstatemenu_delete_init()
    {
        m_okcancelForm = new stateview._5000_ViewForm.dialog.OKCancelForm();
        m_okcancelForm.Text = G.Localize("del_Confirmation");
        m_okcancelForm.textBox1.Text = G.Localize("del_focusing_nodes");
        m_okcancelForm.Show(G.view_form);
        m_okcancelForm.Location = Cursor.Position;
    }
    bool gfstatemenu_delete_wait()
    {
        if (m_okcancelForm.DialogResult != DialogResult.None)
        {
            m_okCancel = m_okcancelForm.DialogResult == DialogResult.OK;
            if (m_okCancel)
            {
                //if (m_group_focus_list!=null)
                //{
                //    foreach(var state in m_group_focus_list)
                //    {
                //        if (AltState.IsAltState(state))
                //        {
                //            var templist = G.node_get_allstates_on_groupnode(AltState.TrimAltStateName(state));
                //            if (templist!=null)
                //            {
                //                foreach(var s2 in templist)
                //                {
                //                    G.excel_program.Delete(s2);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            G.excel_program.Delete(state);
                //        }
                //    }
                //    //History.ReqToSave("Deleted multiple states");
                //    History2.SaveForce_delete("Deleted multiple states");
                //}
                var b = StateUtil.DeleteGroupList(m_group_focus_list);
                if (b)
                {
                    History2.SaveForce_delete("Deleted multiple states");
                }
            }

            return true;
        }
        return false;
    }
    public void gfstatemenu_export_clipboard()
    {
        var s = string.Empty;
        if (m_group_focus_list==null || m_group_focus_list.Count==0) {
            G.NoticeToUser_warning("{D82CE107-6F76-499A-8AE5-EF972812A3AB}");
            return;
        }

        var list = new List<string>();
        foreach(var state in m_group_focus_list)
        {
            if (AltState.IsAltState(state))
            {
                var templist = G.node_get_allstates_on_groupnode(AltState.TrimAltStateName(state));
                list.AddRange(templist);
            }
            else
            {
                list.Add(state);
            }
        }
        G.ExportToClipboard(list,m_group_focus_list);
    }
    void br_Grouping(Action<int,bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.GROUPING)
        {
            SetNextState(st);
        }
    }
    void br_DeleteGF(Action<int,bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.DELETE)
        {
            SetNextState(st);
        }
    }
    void br_Ungrouping(Action<int, bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.UNGROUPING)
        {
            SetNextState(st);
        }
    }
    void br_ExportClipboard(Action<int, bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.EXPORTCLIPBOARD)
        {
            SetNextState(st);
        }
    }
    void br_Moveto(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.MOVETO)
        {
            SetNextState(st);
        }
    }
    void br_Change(Action<int,bool> st)
    {
        if (
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_STATE
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_EMBED
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_COMMENT
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_STOP
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_STOP
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_BASE
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_BASE
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_PASS
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_PASS
            )
        {
            SetNextState(st);
        }
    }
    void br_MovetoGF(Action<int,bool> st)
    {
        if (m_viewFormStateMenuGFItem == ViewFormStateMenuGFItem.MOVETO)
        {
            SetNextState(st);
        }
    }
    #endregion

    #region open src
    void br_OpenSrc(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.OPENSRC)
        {
            SetNextState(st);
        }
    }
    void statemenu_opensrc()
    {
        G.view_form.OpenEditorWithCreateFile();
    }
    #endregion

    #region view ex state
    void br_ViewExState(Action<int,bool> st)
    {
        if (m_viewFormStateMenuItem == ViewFormStateMenuItem.VIEWEXSTATE)
        {
            SetNextState(st);
        }
    }
    void statemenu_view_exstate()
    {
        G.vf_sc.m_center_focus_state = G.vf_sc.m_focus_state;
    }
    #endregion

    #region 種別変更
    void statemenu_change()
    {
        m_okCancel = false;
        var state = m_focus_state;

        if (
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_STATE
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_EMBED
            ||
            m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_COMMENT
            )
        {

            var header = string.Empty;
            if (m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_STATE)
            {
                header = "S_";
            }
            else if (m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_EMBED)
            {
                header = "E_";
            }
            else if (m_viewFormStateMenuItem == ViewFormStateMenuItem.CHANGE_COMMENT)
            {
                header = "C_";
            }
            if (string.IsNullOrEmpty(state) || state.Length <= 2 ) {
                G.NoticeToUser_warning("{D2C3BFF6-0C09-46AA-AF0A-70AADC164863}");
                return;
            }
            if (!string.IsNullOrEmpty(header))
            { 
                if (state.StartsWith(header))
                {
                    G.NoticeToUser("It's same. Don't need change.");
                    return; //SAME !!
                }
                state = header + state.Substring(2);

                for(var loop = 0; loop<=1000; loop++)
                {
                    if (loop==1000) throw new SystemException("{FB32F83E-8D0D-4B73-8818-642431464D9F}");
                    if (!G.state_working_list.Contains(state)) break;
                    state = StateUtil.MakeNewName(state);
                }

                StateUtil.Refactor(m_focus_state,state);
                stateview.CopyRenameStateSave.calc_new_dst_position(m_focus_state,state,false);

                m_okCancel = true;
                History.ReqToSave("Changed name in state");
            }
        }
        else
        {
            string state_typ = null;
            if (m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_STOP)
            {
                var next = G.excel_program.GetString(state,G.STATENAME_nextstate);
                var branch = G.excel_program.GetString(state,G.STATENAME_branch);
                if (string.IsNullOrEmpty(next) && string.IsNullOrEmpty(branch))
                { 
                    state_typ = WordStorage.Store.state_typ_stop;
                }
                else
                {
                    G.NoticeToUser_warning(G.Localize("w_cannot_set_stop")/*"'Stop' cannot be adopted because either nextstate nor branch exists."*/);
                }
            }
            else if (m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_BASE)
            {
                state_typ = WordStorage.Store.state_typ_base;
            }
            else if ( 
                m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_STOP
                ||
                m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_BASE
                ||
                m_viewFormStateMenuItem == ViewFormStateMenuItem.RESET_PASS
                )
            {
                state_typ = string.Empty;
            }
            else if (m_viewFormStateMenuItem == ViewFormStateMenuItem.SET_PASS)
            {
                //branchはあってはならいあ。state-xxはあってもよい。
                //!xxxxはあってもよい
                //それ以外があるとNG
                var bOk = true;
                var dic = G.excel_program.GetAllString(state);
                if (dic!=null) {
                    foreach (var k in dic.Keys)
                    {
                        var v = dic[k];
                        if (k.StartsWith(G.STATENAME_state)) continue;
                        if (k.StartsWith("!")) continue;
                        if (k == G.STATENAME_nextstate) continue;
                        if (!string.IsNullOrEmpty(v))
                        {
                            bOk = false;
                            break;
                        }
                    }
                }
                else
                {
                    bOk = false;
                }

                if (bOk)
                {
                    state_typ = WordStorage.Store.state_typ_pass;
                }
                else
                {
                    G.NoticeToUser_warning(G.Localize("w_cannot_set_pass")/* "'Pass' cannot be adopted because irrelevant values exists."*/);
                }
            }


            if (state_typ!=null)
            { 
                G.excel_program.SetString(state,G.STATENAME_statetyp,state_typ);
                m_okCancel = true;
                History2.SaveForce_modify_value("Changed state typ");
            }
        }

    }
    #endregion

    #region ステート名をコピー
    void statemenu_copystatename()
    {
        Clipboard.SetText(m_state_under_pointer);
    }
    #endregion
}
