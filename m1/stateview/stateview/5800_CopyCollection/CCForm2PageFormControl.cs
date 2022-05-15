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
using System.Collections.Specialized;
using System.Threading;
using stateview;

public partial class CCForm2PageFormControl  {

    public stateview._5800_CopyCollection.CCForm2PageForm m_form;       
    DataGridView m_dg    { get { return m_form.dataGridView1;    } }
    int ORDER_COL        { get { return m_form.ORDER_COL;        } }
    int UUID_COL         { get { return m_form.UUID_COL;         } }
    int TITLE_COL        { get { return m_form.TITLE_COL;        } }
    int CMT_COL          { get { return m_form.CMT_COL;          } }
    int m_cur_pageitem_index { get { return m_form.m_cur_page_index; } set { m_form.m_cur_page_index = value; } }

    bool m_mouse_up_down  { get { return m_form.m_mouse_up_down;   } }

    bool m_bMenuEditTitle   { get { return m_form.m_bMenuEditTitle;   } set { m_form.m_bMenuEditTitle   = value; } }
    bool m_bMenuEditComment { get { return m_form.m_bMenuEditComment; } set { m_form.m_bMenuEditComment = value; } }
    bool m_bMenuAddNew      { get { return m_form.m_bMenuAddNew;      } set { m_form.m_bMenuAddNew      = value; } }
    bool m_bMenuDelete      { get { return m_form.m_bMenuDelete;      } set { m_form.m_bMenuDelete      = value; } }
    bool m_bMenuUp          { get { return m_form.m_bMenuUp;          } set { m_form.m_bMenuUp          = value; } }
    bool m_bMenuDown        { get { return m_form.m_bMenuDown;        } set { m_form.m_bMenuDown        = value; } }

    CopyCollection.WorkItem m_pageitem { get { return  G.cc.GetCollectionItem();  } }

    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (IsEnd()) break;
		}
	}

	#region    // [PSGG OUTPUT START] indent(4) $/./$
//  psggConverterLib.dll converted from CCForm2PageFormControl.xlsx.    psgg-file:CCForm2PageFormControl.psgg
    /*
        S_AddNew
    */
    void S_AddNew(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_add_new();
        }
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }
    /*
        S_CHECK_MODE1
    */
    void S_CHECK_MODE1(bool bFirst)
    {
        //
        if (bFirst)
        {
            select_work();
        }
        //
        if (!HasNextState())
        {
            Goto(S_OpenMenu);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_CHECK_MOUSE_DOWN
    */
    void S_CHECK_MOUSE_DOWN(bool bFirst)
    {
        //
        if (bFirst)
        {
            mouse_init();
        }
        var b = mouse_is_down();
        if (b==false) return;
        //
        if (!HasNextState())
        {
            Goto(S_TICK);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_Delete
    */
    void S_Delete(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_delete();
        }
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }
    /*
        S_Down
    */
    void S_Down(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_down();
        }
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }
    /*
        S_EditComment
    */
    void S_EditComment(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_edit_comment();
        }
        if (!menu_edit_done()) return;
        menu_edit_comment_post();
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }
    /*
        S_EditTitle
    */
    void S_EditTitle(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_edit_title();
        }
        if (!menu_edit_done()) return;
        menu_edit_title_post();
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_OpenMenu
    */
    void S_OpenMenu(bool bFirst)
    {
        //
        if (bFirst)
        {
            open_menu();
        }
        if (!done_menu()) return;
        // branch
        if (m_bMenuEditTitle) { Goto( S_EditTitle ); }
        else if (m_bMenuEditComment) { Goto( S_EditComment ); }
        else if (m_bMenuAddNew) { Goto( S_AddNew ); }
        else if (m_bMenuDelete) { Goto( S_Delete ); }
        else if (m_bMenuUp) { Goto( S_Up ); }
        else if (m_bMenuDown) { Goto( S_Down ); }
        else { Goto( S_PAS000 ); }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_PAS000
    */
    void S_PAS000(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_CHECK_MOUSE_DOWN);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_CHECK_MOUSE_DOWN);
        NoWait();
    }
    /*
        S_TICK
    */
    void S_TICK(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_CHECK_MODE1);
        }
    }
    /*
        S_Up
    */
    void S_Up(bool bFirst)
    {
        //
        if (bFirst)
        {
            menu_up();
        }
        //
        if (!HasNextState())
        {
            Goto(S_PAS000);
        }
    }


    #endregion // [PSGG OUTPUT END]

    #region マウス
    bool m_bHold;
    bool m_bClick;
    void mouse_init()
    {
        m_bHold  = false;
        m_bClick = false;

    }
    bool mouse_is_down()
    {
        return m_mouse_up_down == false;
    }

    bool check_mousedown_menu()
    {
        /*        
        var bOk = m_prev_item == m_select_item;
        m_prev_item = m_select_item;

        if (m_bClick)
        {
            if (bOk)
            {
                return true;
            }
        }
        return false;
        */
        return m_bClick;
    }

    bool check_mousedown_hold()
    {
        return m_bHold;
    }

    CopyCollection.WorkItem m_select_pageitem;
    void select_work()
    {
        if (m_dg==null) return;
        var selectrow = m_dg.SelectedRows!=null && m_dg.SelectedRows.Count>0 ? m_dg.SelectedRows[0] : null;
        if (selectrow == null) return;
        m_cur_pageitem_index = selectrow.Index;

        var uuid      = m_dg[UUID_COL,m_cur_pageitem_index].Value.ToString();
        m_select_pageitem = G.cc.FindItem(uuid);
    }
    #endregion


    #region メニュー
    Point m_cotextmenu_location;
    void open_menu()
    {
        m_bMenuEditTitle   = false;
        m_bMenuEditComment = false;
        m_bMenuAddNew      = false;
        m_bMenuDelete      = false;
        m_bMenuUp          = false;
        m_bMenuDown        = false;

        m_cotextmenu_location = Cursor.Position;
        m_form.contextMenuStrip1.Show(m_cotextmenu_location);
    }
    bool done_menu()
    {
        var bouds = RectangleUtil.AddMargin( m_form.contextMenuStrip1.Bounds , 5);
        if (!bouds.Contains(Cursor.Position))
        {
            m_form.contextMenuStrip1.Visible = false;
        }

        return m_form.contextMenuStrip1.Visible == false;
    }
    bool m_menu_edit_done;
    void menu_edit_title()
    {
        G.NoticeToUser_warning("menu_edit_title");
        m_dg.SelectionMode = DataGridViewSelectionMode.CellSelect;
        m_menu_edit_done = false;
        m_dg[TITLE_COL,m_cur_pageitem_index].ReadOnly = false;
        m_dg[TITLE_COL,m_cur_pageitem_index].Selected = true;
        m_dg.CellLeave += M_dg_CellLeave;
        m_dg.CellEndEdit += M_dg_CellEndEdit;
        m_dg.BeginEdit(true);
    }

    private void M_dg_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        m_menu_edit_done = true;
    }

    void menu_edit_title_post()
    {
        m_dg.CellLeave -= M_dg_CellLeave;
        m_dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        m_select_pageitem.name = m_dg[TITLE_COL,m_cur_pageitem_index].Value.ToString();
        G.cc.UpdateFolderItem(m_select_pageitem);
        m_form.loaddata();
    }
    void menu_edit_comment()
    {
        G.NoticeToUser_warning("menu_edit_comment");
        m_dg.SelectionMode = DataGridViewSelectionMode.CellSelect;
        m_menu_edit_done = false;
        m_dg[CMT_COL,m_cur_pageitem_index].ReadOnly = false;
        m_dg[CMT_COL,m_cur_pageitem_index].Selected = true;
        m_dg.CellLeave += M_dg_CellLeave;
        m_dg.BeginEdit(true);
    }
    void menu_edit_comment_post()
    {
        m_dg.CellLeave -= M_dg_CellLeave;
        m_dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        m_select_pageitem.comment = m_dg[CMT_COL,m_cur_pageitem_index].Value.ToString();
        G.cc.UpdateFolderItem(m_select_pageitem);
        m_form.loaddata();
    }
    bool menu_edit_done()
    {
        return m_menu_edit_done;
    }

    private void M_dg_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
        m_menu_edit_done = true;
    }
    void menu_add_new()
    {
        var disporder = double.MinValue;
        {//表示順を求める。最後にする。
            if (m_dg.Rows.Count > 0)
            {
                disporder = ParseUtil.ParseDouble( m_dg[ORDER_COL, m_dg.Rows.Count-1].Value.ToString(), double.MinValue);
                if (disporder != double.MinValue)
                {
                    disporder += 1;
                }
            }
        }
        G.cc.page_add(disporder);
        m_cur_pageitem_index = m_dg.Rows.Count;
        m_form.loaddata();
    }
    void menu_delete()
    {
        if (m_cur_pageitem_index >= 0 && m_cur_pageitem_index < m_dg.Rows.Count && m_dg.Rows.Count>1) //※最後の１つは消せない。
        {
            var rpos = m_dg.PointToClient(m_cotextmenu_location);
            var bounds = m_dg.GetRowDisplayRectangle(m_cur_pageitem_index,false);
            if (bounds.Contains(rpos))
            {
                G.cc.page_del(m_select_pageitem);
                m_form.loaddata();
                return;
            }
        }
        G.NoticeToUser_warning("Delete Failure because out of range");            
    }
    void menu_up()
    {
        if (
            (m_cur_pageitem_index==0)
            ||
           (m_dg.Rows.Count<=1)
            ||
           (m_cur_pageitem_index>=m_dg.Rows.Count)
            )
        {
            G.NoticeToUser_warning("Error up");
            return;
        }
        var up1_item = get_item(m_cur_pageitem_index-1);
        var up2_item = get_item(m_cur_pageitem_index-2);
        var disporder_1 = up1_item!=null ? up1_item.disporder :  m_select_pageitem.disporder - 1;
        var disporder_2 = up2_item!=null ? up2_item.disporder :  disporder_1 - 1;
        var newdisporder = (disporder_1 + disporder_2) * 0.5f;
        m_select_pageitem.disporder = newdisporder;

        update_all_items_on_this_page();

        m_cur_pageitem_index = m_cur_pageitem_index - 1;
        m_form.loaddata();
    }
    void menu_down()
    {
        if (m_cur_pageitem_index + 1 >= m_dg.Rows.Count)
        {
            G.NoticeToUser_warning("Error down");
            return;
        }
        var down1_item = get_item(m_cur_pageitem_index + 1);
        var down2_item = get_item(m_cur_pageitem_index + 2);
        var disporder_1 = down1_item!=null ? down1_item.disporder : m_select_pageitem.disporder + 1;
        var disporder_2 = down2_item!=null ? down2_item.disporder : disporder_1 + 1 ;
        var newdisporder = (disporder_1 + disporder_2) * 0.5f;
        m_select_pageitem.disporder = newdisporder;

        update_all_items_on_this_page();

        m_cur_pageitem_index = m_cur_pageitem_index + 1;
        m_form.loaddata();
    }
    #endregion

    #region  Utility
    string get_uuid(int row)
    {
        if (row < 0 || m_dg.Rows.Count <= row) return null;
        return m_dg[UUID_COL,row].Value.ToString();
    }
    CopyCollection.WorkItem get_item(int row)
    {
        var uuid = get_uuid(row);
        var item = G.cc.FindItem(uuid,m_pageitem);
        return item;
    }
    void update_all_items_on_this_page()
    {
        var items = G.cc.GetItemsIfFolder( G.cc.GetCollectionItem());
        foreach(var i in items)
        {
            G.cc.UpdateItem(i, false);
        }
    }
    #endregion
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

