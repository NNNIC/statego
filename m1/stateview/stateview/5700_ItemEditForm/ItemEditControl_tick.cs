using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl
{
    bool is_col(string colname)
    {
        return m_cellname_dic[colname] == m_col;
    }

    void cellclick_init()
    {
        m_row = -1;
        m_itemname = string.Empty;

        var cells = m_dg.SelectedCells;
        if (cells.Count>0)
        {
            m_row = cells[0].RowIndex;
        }
        if (m_row>0)
        {
            m_itemname = m_dg[CC_NAME,m_row].Value?.ToString();
        }
    }
    #region inputform
    ItemEditInputForm m_inpform;
    void open_input_start()
    {
        m_inpform = new ItemEditInputForm();
        m_inpform.m_pm = this;
        m_inpform.DialogResult = DialogResult.None;
        m_inpform.Show(m_form);
    }

    bool open_input_done()
    {
        return m_inpform.DialogResult != DialogResult.None;
    }
    #endregion

    void open_contextmenu()
    {
        if (m_col == CC_INDEX)
        {
            m_form.contextMenuStrip_index.Show(Cursor.Position);
        }
        else if (m_col == CC_COND)
        {
            m_form.contextMenuStrip_cond.Show(Cursor.Position);
        }
        else if (Array.IndexOf(CC_S_LIST,m_col) >=0)
        {
            m_form.contextMenuStrip_checkonoff.Show(Cursor.Position);
        }
        else
        {
            m_form.contextMenuStrip_main.Show(Cursor.Position);
        }
    }

    bool is_modifiable_row()
    {
        if (string.IsNullOrEmpty(m_itemname)) return false;

        var index = m_dg[CC_INDEX,m_row].Value?.ToString();
        if (  !(ParseUtil.ParseInt(index) > 0 || index == NEWMARK) )
        {
            return false;
        }

        //var n = m_dg[CC_NAME,m_row].Value?.ToString();
        //if (!string.IsNullOrEmpty(m_itemname))
        //{
        //    var b = m_info.is_readonly(m_itemname);
        //    return !b;
        //}
        //return false;

        return true;
    }

    void cond_chg()
    {
        if (m_col == CC_COND)
        {
            var s = m_dg[CC_COND,m_row].Value?.ToString();
            var cond = EnumUtil.Parse(s,Cond.none);
            if (cond== Cond.none) {
                cond = Cond.exclusion;
            }
            else if (cond == Cond.exclusion)
            {
                cond = Cond.share;
            }
            else
            {
                cond = Cond.exclusion;
            }

            if (cond == Cond.exclusion)
            {
                while(get_count_checked_on_dg()>1)
                {
                    del_checked_last_on_dg();
                }
            }
            m_dg[CC_COND,m_row].Value = cond.ToString();
        }
    }

    void check_on()
    {
        if ( Array.IndexOf(CC_S_LIST,m_col) < 0)  return;
        var cond = get_cond_on_dg(m_itemname);
        if ( cond == Cond.exclusion)
        {
            //clear
            Array.ForEach(CC_S_LIST,(sx)=> {
                m_dg[sx,m_row].Value = "";
            });
            //set
            m_dg[m_col,m_row].Value = CHECKMARK;
        }
        else if (cond == Cond.share)
        {
            //set
            m_dg[m_col,m_row].Value = CHECKMARK;
        }
    }

    void check_off()
    {
        if ( Array.IndexOf(CC_S_LIST,m_col) < 0)  return;
        var cond = get_cond_on_dg(m_itemname);
        if (cond == Cond.none) return;

        m_dg[m_col,m_row].Value = string.Empty;
    }

    Cond get_cond_on_dg(string item)
    {
        var s = m_dg[CC_COND,m_row].Value?.ToString();
        return EnumUtil.Parse(s,Cond.none);
    }

    int get_count_checked_on_dg()
    {
        int c = 0;
        foreach(var s in CC_S_LIST)
        {
            if (m_dg[s, m_row].Value?.ToString() == CHECKMARK)
            {
                c++;
            }
        }
        return c;
    }
    void del_checked_last_on_dg()
    {
        for(var n = CC_S_LIST.Length - 1; n>=0; n--)
        {
            if (m_dg[CC_S_LIST[n] , m_row].Value?.ToString() == CHECKMARK)
            {
                m_dg[CC_S_LIST[n] , m_row].Value = string.Empty;
                break;
            }
        }
    }
    #region new form
    ItemEditNewForm m_newform;
    void open_new_start()
    {
        if (m_col != CC_INDEX)
        {
            m_newform = null;
            return;
        }
        m_newform = new ItemEditNewForm();
        m_newform.m_pm = this;
        m_newform.DialogResult = DialogResult.None;
        m_newform.Show(m_form);
    }
    bool open_new_done()
    {
        return m_newform==null || m_newform.DialogResult != DialogResult.None;
    }
    #endregion

    #region REMOVE
    DialogResult m_result;
    bool m_askdone;
    void ask_delete_start()
    {
        m_askdone = false;
        m_result = DialogResult.None;
        MsgWinUtil.Show2btnMsg("Are you ok to delete ?",(r)=> { m_result = r; m_askdone = true;});
    }
    bool ask_delete_done()
    {
        return m_askdone;
    }
    bool ask_delete_result()
    {
        return m_result == DialogResult.OK;
    }
    void remove_row()
    {
        m_dg.Rows.RemoveAt(m_row);
    }
    #endregion

    #region UP DOWN
    void up_row()
    {
        var dst_row = m_row - 1;
        if (dst_row>=0)
        {
            swap_row(dst_row,m_row);
        }
    }
    void down_row()
    {
        var dst_row = m_row + 1;
        if (dst_row < m_dg.Rows.Count)
        {
            swap_row(dst_row, m_row);
        }
    }

    public class DgCellItem
    {
        public string text;
        public Color backcolor;
    }
    void swap_row(int a, int b)
    {
        Func<int, List<DgCellItem>> get_row_date = (r) => {
            var list = new List<DgCellItem>();
            for(var c = 0; c<m_dg.Columns.Count; c++)
            {
                var item = new DgCellItem();

                //text
                var v = m_dg[c,r].Value?.ToString();
                if (v==null) v = string.Empty;
                v = v.Trim();
                item.text = v;

                //bgcolor;
                var bc = m_dg[c,r].Style.BackColor;
                item.backcolor = bc;

                list.Add(item);
            }
            return list;
        };

        Action<int, List<DgCellItem>> set_row_data = (r,list) => {
            for(var c = 0; c<m_dg.Columns.Count; c++)
            {
                var item = list[c];
                m_dg[c,r].Value = list[c].text;
                m_dg[c,r].Style.BackColor = list[c].backcolor;
            }
        };

        var save_a = get_row_date(a);
        var save_b = get_row_date(b);

        set_row_data(a,save_b);
        set_row_data(b,save_a);
    }
    #endregion

}

