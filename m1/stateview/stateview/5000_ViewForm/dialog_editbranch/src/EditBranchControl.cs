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

public partial class EditBranchControl  {
	
    public stateview._5000_ViewForm.dialog_editbranch.EditBranchForm m_form;	

    public enum MENUEVENT
    {
        NONE,
        EDIT,
        SELECT,
        UP,
        DOWN,
        DELETE,
        CANCEL,
        ENDEDIT,

        NEW_API,
        NEW_IF,
        NEW_ELSEIF,
        NEW_ELSE,

        IF,
        ELSEIF,
        ELSE
    }
    public MENUEVENT m_menuevent;
    public bool m_bClicked;


    //bool? m_onItem_or_blank;
    //int   m_onItem_onMode_blank_onCmt = 0; // on_item=1 onMode=2 blank=3 onCmt=4 
    enum Where
    {
        none,
        onItem,
        onMode,
        onCmt,
        onHeader,
        brank
    }
    Where m_where;
    int   m_focus_row;
    Point m_save_pos;
    string m_select_branch_string;
    void check_point()
    {
        m_where =  Where.none;
        //m_onItem_or_blank = null;
        m_focus_row = -1;

        if (!m_bClicked) return; 

        var scpos = Cursor.Position;
        var pos = m_form.PointToClient(scpos);
        var bound_w_margin = RectangleUtil.AddMargin(m_form.dataGridView1.Bounds,100f);
        if (!bound_w_margin.Contains(pos))
        {
            return; //無視
        }

        m_where =  Where.brank; //blankとする

        m_save_pos = pos;
        var row = -1;
        var col0bound = m_form.dataGridView1.GetColumnDisplayRectangle(0, true);
        var col1bound = m_form.dataGridView1.GetColumnDisplayRectangle(1, true);
        var col2bouns = m_form.dataGridView1.GetColumnDisplayRectangle(2, true);

        var colAllbound = RectangleUtil.Combine(col0bound,RectangleUtil.Combine(col1bound,col2bouns));

        var headerheight = m_form.dataGridView1.ColumnHeadersHeight;

        if (colAllbound.Contains(pos))
        {
            var rpos = m_form.dataGridView1.PointToClient(scpos);
            if (rpos.Y < headerheight)
            {
                m_where = Where.onHeader;
            }
            else for(var r = 0;r < m_form.dataGridView1.Rows.Count;r++)
            {
                var rect = m_form.dataGridView1.GetRowDisplayRectangle(r,true);
                if(rect.Contains(rpos))
                {
                    row = r;
                    if (col1bound.Contains(pos))
                    {
                        m_where =  Where.onItem;
                    }
                    else if (col2bouns.Contains(pos))
                    {
                        m_where = Where.onCmt;
                    }
                    else
                    {
                        m_where =  Where.onMode;
                    }
                    break;
                }
            }
        }
        m_focus_row = row;
    }
    void br_OnItem(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_where == Where.onItem )
            { 
                SetNextState(st);
            }
        }   
    }
    void br_OnCmt(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_where == Where.onCmt )
            { 
                if (G.name_list.Contains(G.STATENAME_branchcmt))
                { 
                    SetNextState(st);
                }
            }
        }   
    }

    void br_OnModeItem(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_where ==  Where.onMode)
            {
                SetNextState(st);
            }
        }
    }
    void br_Blank(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_where ==  Where.brank)
            {
                SetNextState(st);
            }
        }
    }
    void br_NotAbove(Action<bool> st)
    {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }

    #region item_menu
    void show_item_menu()
    {
        var ms = m_form.MenuStrip_Item;
        G.brancApiCollector.ModifyBranchEditorForm_MenuItem(m_form.MenuItem_ItemSlect,cb_select_branch_item);
        //if (ListUtil.IsValidIndex(m_form.m_tmp_list, m_focus_row))
        //{
        //    ms.Items[0].Enabled = (m_form.m_tmp_list[m_focus_row].mode != BranchUtil.APIMODE.ELSE);
        //}
        ms.Show(m_form, PointUtil.Add_XY(m_save_pos,-10,-10) );
    }
    void cb_select_branch_item(string s)
    {
        m_menuevent = MENUEVENT.SELECT;
        m_select_branch_string = s;
    }
    void check_item_menu()
    {
        if (m_menuevent!= MENUEVENT.NONE) return;
        var bounds = m_form.MenuStrip_Item.Bounds;
        var sub = Get_ItemSlectBounds();
        if (sub!=null)
        {
            bounds = RectangleUtil.Combine(bounds,(Rectangle)sub);
        }
        if (!bounds.Contains(Cursor.Position))
        {
            m_form.MenuStrip_Item.Hide();
            m_menuevent = MENUEVENT.CANCEL;
        }
    }
    Rectangle? Get_ItemSlectBounds()
    {
        if(!m_form.MenuItem_ItemSlect.DropDown.Visible) return null;
        return m_form.MenuItem_ItemSlect.DropDown.Bounds;
    }
    #endregion

    #region cmt_menu
    void show_cmt_menu()
    {
        var ms = m_form.MenuStrip_Cmt;
        ms.Show(m_form, PointUtil.Add_XY(m_save_pos,-10,-10) );
    }
    void check_cmt_menu()
    {
        if (m_menuevent!= MENUEVENT.NONE) return;
        var bounds = m_form.MenuStrip_Cmt.Bounds;
        if (!bounds.Contains(Cursor.Position))
        {
            m_form.MenuStrip_Cmt.Hide();
            m_menuevent = MENUEVENT.CANCEL;
        }
    }
    #endregion
    
    #region blank menu
    void show_blank_menu()
    {
        var ms = m_form.MenuStrip_Blank;
        G.brancApiCollector.ModifyBranchEditorForm_MenuItem(m_form.MenuItem_BlankSelect,cb_select_branch_item);

        var newmenu = m_form.MenuItem_New;

        newmenu.DropDown.Items[0].Visible = BranchUtil.EnabledIF; //IF
        newmenu.DropDown.Items[1].Visible = BranchUtil.EnabledIF; //ELSE IF
        newmenu.DropDown.Items[2].Visible = BranchUtil.EnabledIF; //ELSE

        if (BranchUtil.EnabledIF && check_valid_automode() )
        {
            if (m_form.m_tmp_list.Count == 0)
            {
                // IF のみON
                newmenu.DropDown.Items[1].Visible = false; //ELSE IF
                newmenu.DropDown.Items[2].Visible = false; //ELSE
                newmenu.DropDown.Items[3].Visible = false; //br_
            }
            else if (m_form.m_tmp_list[0].mode == BranchUtil.APIMODE.IF)
            {
                newmenu.DropDown.Items[0].Visible = false;  //IFは使えない
                newmenu.DropDown.Items[2].Visible = m_form.m_tmp_list.TrueForAll(i=>i.mode!= BranchUtil.APIMODE.ELSE); //ELSE未使用時に ELSE表示
                newmenu.DropDown.Items[3].Visible = false; //br_
            } 
        }

        // Automode off時のみBR可能。 デフォルトで automode onだから、br_は非表示となる。
        m_form.MenuItem_BlankSelect.Visible = !G.option_editbranch_automode;
        m_form.MenuItem_ItemSlect.Visible   = !G.option_editbranch_automode;;
        newmenu.DropDown.Items[3].Visible   = !G.option_editbranch_automode;; //Br_

        ms.Show(m_form, PointUtil.Add_XY( m_save_pos, -10, -10));
    }
    void check_blank_menu()
    {
        if (m_menuevent != MENUEVENT.NONE) return;
        var bounds = m_form.MenuStrip_Blank.Bounds;
        var sub1 = Get_BlankSlectBounds();
        if (sub1!=null)
        {
            bounds = RectangleUtil.Combine(bounds,(Rectangle)sub1);
        }
        var sub2 = Get_NewSelectBounds();
        if (sub2 != null)
        {
            bounds = RectangleUtil.Combine(bounds, (Rectangle)sub2);
        }

        if (!bounds.Contains(Cursor.Position))
        {
            m_form.MenuStrip_Blank.Hide();
            m_menuevent = MENUEVENT.CANCEL;
        }
    }
    Rectangle? Get_BlankSlectBounds()
    {
        if(!m_form.MenuItem_BlankSelect.DropDown.Visible) return null;
        return m_form.MenuItem_BlankSelect.DropDown.Bounds;
    }
    Rectangle? Get_NewSelectBounds()
    {
        if (!m_form.MenuItem_New.DropDown.Visible) return null;
        return m_form.MenuItem_New.DropDown.Bounds;
    }
    #endregion

    void br_Edit(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.EDIT) SetNextState(st);
        }
    }
    void br_Select(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.SELECT) SetNextState(st);
        }
    }
    void br_Up(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.UP) SetNextState(st);
        }
    }
    void br_Down(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.DOWN) SetNextState(st);
        }
    }
    void br_Delete(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.DELETE) SetNextState(st);
        }
    }
    void br_NewAPI(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.NEW_API) SetNextState(st);
        }
    }
    void br_NewIF(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.NEW_IF) SetNextState(st);
        }
    }
    void br_NewELSEIF(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.NEW_ELSEIF) SetNextState(st);
        }
    }
    void br_NewELSE(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.NEW_ELSE) SetNextState(st);
        }
    }
    void br_Cancel(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_menuevent == MENUEVENT.CANCEL) SetNextState(st);
        }
    }
    DataGridViewCell m_focus_cell;
    void redraw()
    {
        m_form.Draw_data();
        update_checkbox_automode_color();
    }
    void reselect()
    {
        var list = m_form.m_tmp_list;
        try {
            if (m_focus_row >=0 && m_focus_row < list.Count) {
                m_focus_cell                     = m_form.dataGridView1[1,m_focus_row];
                m_form.dataGridView1.CurrentCell = m_focus_cell;
            }
        } catch { }
    }
    #region edit
    DataGridViewSelectionMode m_savemode;
    void edit_start()
    {
        m_form.dataGridView1.ReadOnly = false;
        m_focus_cell = m_form.dataGridView1[1, m_focus_row];
        m_form.dataGridView1.CurrentCell = m_focus_cell;
        m_focus_cell.ReadOnly = false;
        m_menuevent = MENUEVENT.NONE;

        m_savemode = m_form.dataGridView1.SelectionMode;
        m_form.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

        m_form.dataGridView1.BeginEdit(false);
    }
    bool edit_done()
    {
        return m_menuevent == MENUEVENT.ENDEDIT;
    }
    void edit_post()
    {
        m_form.dataGridView1.SelectionMode = m_savemode;

        m_focus_cell.ReadOnly = true;
        m_form.dataGridView1.ReadOnly = true;

        var list = m_form.m_tmp_list;
        var index = m_focus_row;
        var p = ListUtil.IsValidIndex(list, index) ? list[index] : null;

        if (p==null) return;

        var val = m_focus_cell.Value != null ? m_focus_cell.Value.ToString().Trim() : string.Empty;
        var valtype = BranchUtil.GetValueType(p.mode);
        switch (valtype)
        {
            case BranchUtil.VALTYPE.API:
                {
                    if (BranchUtil.IsValid_API_BR(val))
                    {
                        p.api = val;
                    }
                    else
                    {
                        G.NoticeToUser_warning(G.Localize("w_theinputtextisinvalid") /*  "The input text is invalid."*/);
                    }
                }
                break;
            case BranchUtil.VALTYPE.CONDITION:
                {
                    p.condition = val;
                }
                break;
        }

        list[index] = p;
    }
    #endregion
    #region editbox

    stateview._5000_ViewForm.dialog_editbranch.EditBranchEditorForm m_editorform;
    void editbox_start()
    {
        m_editorform = new stateview._5000_ViewForm.dialog_editbranch.EditBranchEditorForm();
        m_editorform.DialogResult = DialogResult.None;
        var list = m_form.m_tmp_list;
        var index = m_focus_row;
        var p = ListUtil.IsValidIndex(list, index) ? list[index] : null;
        if (p == null)
        {
            m_editorform.DialogResult = DialogResult.Cancel;
            G.NoticeToUser_warning("{CBA74651-9CAE-4081-A9BB-DDDDDAF9BF2E}");
            return;
        }
        var val = string.Empty;
        var cmt = p.comment;
        var valtype = BranchUtil.GetValueType(p.mode);
        switch (valtype)
        {
            case BranchUtil.VALTYPE.API: val = p.api; break;
            case BranchUtil.VALTYPE.CONDITION: val = p.condition; break;
            default: G.NoticeToUser_warning("{5C153EC8-2E01-4E24-A6B8-3901FEAE808E}"); break;
        }
        m_editorform.m_bConditon = BranchUtil.GetValueType(p.mode) == BranchUtil.VALTYPE.CONDITION;
        m_editorform.m_bOnlyComment = p.mode == BranchUtil.APIMODE.ELSE; 
        m_editorform.m_text = G.decode_branch_special_newlinechar( val );
        m_editorform.m_comment = cmt;
        m_editorform.m_state = m_form.m_state;
        m_editorform.m_brcond_index = index;
        m_editorform.Show();
        m_editorform.Location = new Point(m_form.Location.X, Cursor.Position.Y);
    }
    bool editbox_done()
    {
        return m_editorform.DialogResult != DialogResult.None;
    }
    void editbox_post()
    {
        if (m_editorform.DialogResult == DialogResult.OK)
        {
            var list = m_form.m_tmp_list;
            var index = m_focus_row;
            var p = ListUtil.IsValidIndex(list, index) ? list[index] : null;

            if (p == null) return;

            p.comment = m_editorform.m_comment;

            var val = G.encode_branch_special_newlinechar( m_editorform.m_text );//m_focus_cell.Value != null ? m_focus_cell.Value.ToString().Trim() : string.Empty;
            var valtype = BranchUtil.GetValueType(p.mode);
            switch (valtype)
            {
                case BranchUtil.VALTYPE.API:
                    {
                        if (BranchUtil.IsValid_API_BR(val))
                        {
                            p.api = val;
                        }
                        else
                        {
                            G.NoticeToUser_warning(G.Localize("w_theinputtextisinvalid") /*  "The input text is invalid."*/);
                        }
                    }
                    break;
                case BranchUtil.VALTYPE.CONDITION:
                    {
                        p.condition = val;
                    }
                    break;
            }

        }
    }
    //string decode_newchar(string s)
    //{
    //    if (string.IsNullOrEmpty(s)) return s;
    //    return s.Replace(G.branch_special_newlinechar, Environment.NewLine);
    //}
    //string encode_newchar(string s)
    //{
    //    if (string.IsNullOrEmpty(s)) return s;
    //    return s.Replace(Environment.NewLine, G.branch_special_newlinechar);
    //}
    #endregion

    #region edit-cmt
    void editcmt_start()
    {
        m_form.dataGridView1.ReadOnly    = false;
        m_focus_cell                     = m_form.dataGridView1[2,m_focus_row];
        m_form.dataGridView1.CurrentCell = m_focus_cell;
        m_focus_cell.ReadOnly            = false;
        m_menuevent                      = MENUEVENT.NONE;

        m_savemode = m_form.dataGridView1.SelectionMode;
        m_form.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;

        m_form.dataGridView1.BeginEdit(false);
    }
    bool editcmt_done()
    {
        return m_menuevent == MENUEVENT.ENDEDIT;
    }
    void editcmt_post()
    {
        m_form.dataGridView1.SelectionMode = m_savemode;
        m_focus_cell.ReadOnly = true;
        m_form.dataGridView1.ReadOnly = true;
        
        var list = m_form.m_tmp_list;
        var index = m_focus_row;
        var p = ListUtil.IsValidIndex(list, index) ? list[index] : null;

        if (p==null) return;

        var val = m_focus_cell.Value != null ? m_focus_cell.Value.ToString().Trim() : string.Empty;
        p.comment = val;
    }

    #endregion

    void select_ow_start()
    {
        var index = m_focus_row;
        var list = m_form.m_tmp_list;
        if (ListUtil.IsValidIndex(list,index))
        {
            list[index].api = m_select_branch_string;
        }
    }

    void up_start()
    {
        var index = m_focus_row;
        var list = m_form.m_tmp_list;
        if (index > 0 && index < list.Count)
        {
            var dstindex = index -1;
            Action moveup = () =>{
                var save = list[index];
                list.RemoveAt(index);
                list.Insert(dstindex,save);
                m_focus_row = dstindex;
            };

            if (check_valid_automode()) //automode時は制限
            {
                //ELSE IF時は、index == 1時は、移動後に IF-> ELSE IF , ELSE IF->IF　とする。
                //             それ以上は、変更のみ
                //IFとELSE時は、UPさせない。
                var target_mode = list[index].mode;
                if (target_mode == BranchUtil.APIMODE.ELSEIF)
                {
                    moveup();
                    if (index == 1)
                    {
                        list[0].mode = BranchUtil.APIMODE.IF;
                        list[1].mode = BranchUtil.APIMODE.ELSEIF;
                    }
                    return;
                }
                G.NoticeToUser_warning("Cannot move up in auto mode.");
                return;
            }
            else { 
                moveup();
            }
        }        
    }

    void down_start()
    {
        var index = m_focus_row;
        var list = m_form.m_tmp_list;
        if (index >= 0 && index < list.Count-1)
        {
            var dstindex = index + 1;
            Action movedown = () => { 
                var save = list[index];
                list.RemoveAt(index);
                list.Insert(dstindex,save);
                m_focus_row = dstindex;
            };

            if (check_valid_automode())//automode時は制限
            {
                //IF時は、移動後に IF-> ELSE IF, ELSE IF-> IFとする。
                //ELSE IFの直下がELSEだったら移動不可
                //上記以外のELSE IFは移動可
                var target_mode = list[index].mode;
                if (target_mode == BranchUtil.APIMODE.IF)
                {
                    movedown();
                    list[0].mode = BranchUtil.APIMODE.IF;
                    list[1].mode = BranchUtil.APIMODE.ELSEIF;
                    return;
                }
                if (target_mode == BranchUtil.APIMODE.ELSEIF && list[dstindex].mode == BranchUtil.APIMODE.ELSEIF)
                {
                    movedown();
                    return;
                }
                G.NoticeToUser_warning("Cannot move dowin in auto mode.");
                return;
            }
            else
            {
                movedown();
            }
        }        
    }
    void delete_start()
    {
        var index = m_focus_row;
        var list = m_form.m_tmp_list;
        if (ListUtil.IsValidIndex(list,index))
        {
            var valid_automode = check_valid_automode();
            list.RemoveAt(index);

            if (valid_automode)
            {
                if (list.Count>0 && list[0].mode == BranchUtil.APIMODE.ELSEIF)
                {
                    list[0].mode = BranchUtil.APIMODE.IF;
                }
                return;
            }
        }
    }
    void new_api_start()
    {
        var list = m_form.m_tmp_list;
        list.Add(new BranchUtil.APILABEL() { mode = BranchUtil.APIMODE.API, api = "br_NEWBRANCH", label = "?" , condition = "?"});
        m_focus_row = list.Count - 1;
    }
    void new_if_start()
    {
        var list = m_form.m_tmp_list;
        list.Add(new BranchUtil.APILABEL() { mode = BranchUtil.APIMODE.IF, api = BranchUtil.BRANCHAPI_IF, label = "?", condition = "?"  });
        m_focus_row = list.Count - 1;
    }
    void new_elseif_start()
    {
        var list = m_form.m_tmp_list;
        var item = new BranchUtil.APILABEL() { mode= BranchUtil.APIMODE.ELSEIF, api = BranchUtil.BRANCHAPI_ELSEIF , label = "?", condition = "?" };
        if (check_valid_automode()) //有効なAUTOMODEか？
        {
            if (list[list.Count-1].mode == BranchUtil.APIMODE.ELSE) // 最終要素のELSEが既に使用済み、よってELSEの手前にいれる。
            {
                list.Insert(list.Count-1,item);
                m_focus_row = list.Count - 2;
                set_default_condition(ref list, m_focus_row);
                return;
            }
        }
        list.Add(item);
        m_focus_row = list.Count - 1;
        set_default_condition(ref list, m_focus_row);
    }

    private void set_default_condition(ref List<BranchUtil.APILABEL> list, int row)
    {
        var src_row = row - 1;
        if (src_row < 0 || src_row >= list.Count) return;
        if (row < 0 || row >= list.Count) return;

        var src_cond = list[src_row].condition;
        list[row].condition = src_cond;

        var src_cmt = list[src_row].comment;
        list[row].comment = src_cmt;
    }

    void new_else_start()
    {
        var list = m_form.m_tmp_list;
        list.Add(new BranchUtil.APILABEL() { mode = BranchUtil.APIMODE.ELSE, api = BranchUtil.BRANCHAPI_ELSE, label = "?", condition = "?" });
        m_focus_row = list.Count - 1;
    }

    void edit_new()
    {
        //var list = m_form.m_tmp_list;
        //m_focus_row = list.Count-1;
    }

    void select_new_start()
    {
        //var index = m_focus_row;
        var list = m_form.m_tmp_list;
        list.Add(new BranchUtil.APILABEL() { mode = BranchUtil.APIMODE.API, api = m_select_branch_string, label="?" , condition = "?" });
        m_focus_row = list.Count -1;
    }
    #region MODE_ITEM_MENU
    void show_mode_menu()
    {
        m_menuevent = MENUEVENT.NONE;

        var rowmode = m_form.dataGridView1[0,m_focus_row].Value.ToString();
        var ms = m_form.MenuStrip_Mode;
        ms.Enabled = rowmode != m_form.MODE_APISTR;
        ms.Show(m_form, PointUtil.Add_XY(m_save_pos, -10, -10));
    }
    bool show_mode_menu_done()
    {
        if (m_menuevent != MENUEVENT.NONE) return true;

        var bounds = m_form.MenuStrip_Mode.Bounds;
        if (!bounds.Contains(Cursor.Position))
        {
            return true;
        }
        return false;
    }
    void show_mode_menu_post()
    {
        m_form.MenuStrip_Mode.Hide();
        if (m_menuevent == MENUEVENT.NONE) return;

        var list = m_form.m_tmp_list;

        var p = ListUtil.IsValidIndex(list,m_focus_row) ? m_form.m_tmp_list[m_focus_row] : null;
        if (p==null) return;
        switch (m_menuevent)
        {
            case MENUEVENT.IF:     p.mode = BranchUtil.APIMODE.IF;     p.api = BranchUtil.GetBranchAPI(p.mode); break;
            case MENUEVENT.ELSEIF: p.mode = BranchUtil.APIMODE.ELSEIF; p.api = BranchUtil.GetBranchAPI(p.mode); break;
            case MENUEVENT.ELSE:   p.mode = BranchUtil.APIMODE.ELSE;   p.api = BranchUtil.GetBranchAPI(p.mode); break;
        }
        m_form.m_tmp_list[m_focus_row] = p;
    }
    #endregion

    public void Update_templist()
    {
        try {
            var num = m_focus_row;
            var modestr = m_form.dataGridView1[0,num].Value.ToString();
            var mode = m_form.GetMode(modestr); //モード名は form内で設定される -- 変更可能にするため。

            var val  = m_form.dataGridView1[1,num].Value.ToString();
        
            var p = m_form.m_tmp_list[num];
            p = BranchUtil.GetApiLabelFromDisplayValue(mode,val,p);    
            m_form.m_tmp_list[num] = p;
        } catch (SystemException e)
        {
            G.NoticeToUser_warning("Unexpected! {0D4B24D1-CCFE-4EB1-937F-EBD5D7B56D5F} " + e.Message);
        }                  
    }

    bool check_valid_automode() // auto modeの形式でリストに並んでいるか？ 
    {
        var list = m_form.m_tmp_list;

        // オプション auto modeはONでなければ、FALSE
        if (G.option_editbranch_automode==false) return false; 

        // リストの中身がないときは TRUE
        if (list==null || list.Count==0) return true;

        // リストの先頭がIFでなければ FALSE
        if (list[0].mode != BranchUtil.APIMODE.IF) return false;

        // 要素がIFのみであれば　TRUE
        if (list.Count == 1) return true;

        //以降の要素を走査
        for(var n = 1; n < list.Count; n++)
        {
            var mode = list[n].mode;
            if (mode == BranchUtil.APIMODE.ELSEIF) continue;// ELSE IFであればOK 次の要素を検索する
            if (mode == BranchUtil.APIMODE.ELSE && n == list.Count-1) // ELSE要素が最後にあれば、続行
            {
                continue;
            }
            return false;//上記以外は FALSE
        }
        return true; //検証を全クリアで TRUE
    }

    void update_checkbox_automode_color()
    {
        var col_ok = Color.Black;
        var col_ng = Color.LightGray;
        m_form.checkBox_automode.ForeColor = check_valid_automode() ? col_ok : col_ng;
    }
}
