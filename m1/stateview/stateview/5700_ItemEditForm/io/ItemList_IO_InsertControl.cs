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

public partial class ItemList_IO_InsertControl  {
   
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
//  psggConverterLib.dll converted from ItemList_IO_InsertControl.xlsx.    psgg-file:ItemList_IO_InsertControl.psgg
    /*
        E_ADDLIST
    */
    List<string> m_add_list;
    /*
        E_FORM
    */
    public stateview.ItemEditForm m_form;
    public ItemEditControl           m_iec;
    /*
        E_IMPORTFUNC
    */
    public Func<string,string> get_method;
    public Func<string,string> get_helpen;
    public Func<string,string> get_helpjpn;
    /*
        E_REFERENCE
        出力先のオリジナルリスト
        コピー元リスト
    */
    public List<string> m_dst_org_list;
    public List<string> m_src_list;
    /*
        S_COLLECT_NAMES_SRC_BTW
        ソースリスト上のnam1とname2の間の追加分を取得
    */
    List<string> m_instertimtes_btw;
    void S_COLLECT_NAMES_SRC_BTW(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_instertimtes_btw = new List<string>();
            var index_n1 = m_src_list.IndexOf(m_dst_name1);
            var index_n2 = m_src_list.IndexOf(m_dst_name2);
            Action<int,int> make_insert_btw = (s,g) =>
            {
                if (g - s > 1)
                {
                    for(var i = s; i<=g; i++)
                    {
                        var tmp = m_src_list[i];
                        if (m_add_list.Contains(tmp))
                        {
                            m_instertimtes_btw.Add(tmp);
                            m_add_list.Remove(tmp);
                        }
                    }
                }
            };
            if (index_n1 >= 0 && index_n2 >= 0)
            {
                var start_index = Math.Min(index_n1,index_n2);
                var goal_index = Math.Max(index_n1, index_n2);
                make_insert_btw(start_index,goal_index);
            }
            else if (index_n1 < 0 && index_n2 >= 0)
            {
                var start_index = 0;
                var goal_index = index_n2;
                make_insert_btw(start_index,goal_index);
            }
        }
        //
        if (!HasNextState())
        {
            Goto(S_INSERT_BTW);
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_INSERT_BTW
        送り先へ挿入
    */
    void S_INSERT_BTW(bool bFirst)
    {
        //
        if (bFirst)
        {
            insert_items();
        }
        //
        if (!HasNextState())
        {
            Goto(S_LOOP_INC_AND_CHECK);
        }
    }
    /*
        S_INSERT_ITEMS_REST
        残りを挿入
    */
    void S_INSERT_ITEMS_REST(bool bFirst)
    {
        //
        if (bFirst)
        {
            insert_rest_items();
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_LOOP_INC_AND_CHECK
    */
    void S_LOOP_INC_AND_CHECK(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_i++;
        }
        var bOK = m_i < m_dst_org_list.Count;
        // branch
        if (bOK) { Goto( S_TWONAMES_SRC ); }
        else { Goto( S_INSERT_ITEMS_REST ); }
    }
    /*
        S_LOOP_INIT
    */
    int m_i = 0;
    void S_LOOP_INIT(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_i = 0;
        }
        //
        if (!HasNextState())
        {
            Goto(S_TWONAMES_SRC);
        }
    }
    /*
        S_PREPARE
        準備
    */
    void S_PREPARE(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_dst_org_list = new List<string>(G.name_list);
            m_add_list = new List<string>(m_src_list);
            foreach(var n in m_dst_org_list)
            {
                if (m_add_list.Contains(n))
                {
                    m_add_list.Remove(n);
                }
            }
        }
        //
        if (!HasNextState())
        {
            Goto(S_LOOP_INIT);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_PREPARE);
        }
    }
    /*
        S_TWONAMES_SRC
        dstリストから２つの隣り合う名前を取得
    */
    string m_dst_name1 = null;
    string m_dst_name2 = null;
    void S_TWONAMES_SRC(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_dst_name1 = ListUtil.GetVal(m_dst_org_list,m_i);
            m_dst_name2 = ListUtil.GetVal(m_dst_org_list,m_i + 1);
        }
        //
        if (!HasNextState())
        {
            Goto(S_COLLECT_NAMES_SRC_BTW);
        }
    }


	#endregion // [PSGG OUTPUT END]
    void insert_items()
    {
        if (m_instertimtes_btw==null || m_instertimtes_btw.Count==0) return;
        var insert_row = get_row_by_name(m_dst_name2);
        if (insert_row < 0) return;
        insert_items_in_row(insert_row,m_instertimtes_btw);
    }
    void insert_rest_items()
    {
        if (m_add_list==null || m_add_list.Count==0) return;
        var insert_row = get_row_by_name(G.STATENAME_branch);
        if (insert_row < 0) return;
        insert_items_in_row(insert_row,m_add_list);
    }
    void insert_items_in_row(int insert_row, List<string> insertitems)
    {
        insert_row--;
        if (insert_row <0) insert_row = 0;
        for(var i = m_instertimtes_btw.Count - 1; i >= 0; i--)
        {
            var n = m_instertimtes_btw[i];
            var imported_methodval = get_method(n);
            var imported_help_en   = get_helpen(n);
            var imported_help_jpn  = get_helpjpn(n);

            m_iec.insert_item_to_dg(n,insert_row,imported_help_en, imported_help_jpn, imported_methodval);
        }
    }
    int get_row_by_name(string name)
    {
        if (string.IsNullOrEmpty(name)) return -1;
        var dg = m_form.dataGridView1;
        for(var row = 0; row<dg.Rows.Count; row++ )
        {
            var tmp = dg[1,row].Value?.ToString();
            if (tmp == name) return row; 
        }
        return -1;
    }
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

