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

public partial class ItemList_IO_Control  {
   
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

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from ItemList_IO_Control.xlsx.    psgg-file:ItemList_IO_Control.psgg
        /*
            E_0002
        */
        public bool m_overwrite;
        /*
            E_FORM
        */
        public stateview.ItemEditForm m_form;
        public ItemEditControl           m_iec;
        /*
            E_INPUT
        */
        public bool m_import_or_export;
        /*
            S_0001
            new state
        */
        void S_0001(bool bFirst)
        {
            // branch
            if (m_overwrite) { Goto( S_OVERWRITE ); }
            else { Goto( S_INSERT ); }
        }
        /*
            S_BRANCH
        */
        void S_BRANCH(bool bFirst)
        {
            // branch
            if (m_import_or_export) { Goto( S_MAKE_IMPORT_HT ); }
            else { Goto( S_NOTICE3 ); }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_INSERT
            未登録アイテムを挿入
        */
        void S_INSERT(bool bFirst)
        {
            var b = import_new_items();
            // branch
            if (b) { Goto( S_NOTICE2 ); }
            else { Goto( S_NOTICE1 ); }
        }
        /*
            S_MAKE_EXPORT
        */
        void S_MAKE_EXPORT(bool bFirst)
        {
            //
            if (bFirst)
            {
                var s = make_export();
                Clipboard.SetText(s);
            }
            //
            if (!HasNextState())
            {
                Goto(S_NOTICE);
            }
        }
        /*
            S_MAKE_IMPORT_HT
        */
        void S_MAKE_IMPORT_HT(bool bFirst)
        {
            var b = make_ht_from_import();
            // branch
            if (b) { Goto( S_0001 ); }
            else { Goto( S_NOTICE1 ); }
        }
        /*
            S_NOTICE
        */
        void S_NOTICE(bool bFirst)
        {
            //
            if (bFirst)
            {
                var s = G.Localize("ief_exported");
                G.NoticeToUser(s);
                MessageBox.Show(s);
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_NOTICE1
        */
        void S_NOTICE1(bool bFirst)
        {
            //
            if (bFirst)
            {
                var s = "{474D6FD0-8566-4B43-A347-6AAE7800D49D}\nunexpected.";
                G.NoticeToUser_warning(s);
                MessageBox.Show(s);
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_NOTICE2
        */
        void S_NOTICE2(bool bFirst)
        {
            //
            if (bFirst)
            {
                var s = G.Localize("ief_imported");
                G.NoticeToUser(s);
                MessageBox.Show(s);
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_NOTICE3
        */
        void S_NOTICE3(bool bFirst)
        {
            var bOK=false;
            //
            if (bFirst)
            {
                var s = G.Localize("ief_export_notice");
                if (MessageBox.Show(s,"CAUTION",MessageBoxButtons.YesNo)== DialogResult.Yes)
                {
                    bOK=true;
                }
            }
            // branch
            if (bOK) { Goto( S_MAKE_EXPORT ); }
            else { Goto( S_END ); }
        }
        /*
            S_OVERWRITE
            既存アイテムに対して上書き
        */
        void S_OVERWRITE(bool bFirst)
        {
            var b = overwrite_exist_items();
            // branch
            if (b) { Goto( S_INSERT ); }
            else { Goto( S_NOTICE1 ); }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_BRANCH);
            }
        }


    #endregion // [PSGG OUTPUT END]

    #region エクスポート

    readonly string M_EXPORT_GUID = "{80D41E22-B478-473C-A68F-FBFB5D9B28BA}";
    readonly string G_NAMEINFO    = "$__NAMEINFO__$";
    readonly string G_HELP        = "$__HELP__$";
    readonly string G_INPMETHOD   = "#__INPMETHOD__$";
    readonly string I_NAMELIST    = "namelist";
    readonly string I_NAMEROWLIST = "namerowlist";
    readonly string I_HELPJPN     = "$jpn";
    readonly string I_HELPEN      = "$en";


    string make_export()
    {
        var ht_nameinfo = new Hashtable();
        ht_nameinfo.Add(I_NAMELIST   , CsvUtil.ToCSV( G.excel_program.GetNameList()) );
        ht_nameinfo.Add(I_NAMEROWLIST, CsvUtil.ToCSV(G.excel_program.GetNameRowList()));

        var ht_help= new Hashtable();
        foreach(var n in G.excel_program.GetNameList())
        {
            HashtableUtil.SetForce(ht_help, n + I_HELPJPN, G.help_program2.GetHelp(n,true));
            HashtableUtil.SetForce(ht_help, n + I_HELPEN , G.help_program2.GetHelp(n,false));
        }

        var ht_inpmethod= new Hashtable();
        foreach(var n in G.excel_program.GetNameList())
        {
            HashtableUtil.SetForce(ht_inpmethod,n,G.itemsInfo_program.GetMethod(n));
        }
        
        var ht_total = new Hashtable();
        ht_total.Add(G_NAMEINFO, ht_nameinfo);
        ht_total.Add(G_HELP, ht_help);
        ht_total.Add(G_INPMETHOD,ht_inpmethod);

        var s = IniUtil.MakeOutput(ht_total,Environment.NewLine);

        s = ";"+ M_EXPORT_GUID + Environment.NewLine + s;

        return s;
    }
    string make_export_ng() //使わず
    {
        Func<string,string> nml = (_) => { if (_==null) return string.Empty; return _; };

        var ht = new Hashtable();
        var dg = m_form.dataGridView1;
        var maxindex = 0;

        for(var row = 0; row < dg.Rows.Count; row++)
        {
            var index_val = dg[0,row].Value?.ToString();
            if (RegexUtil.IsMatch(@"^[0-9]+$",index_val))
            {
                var index = int.Parse(index_val);
                var name = dg[1,row].Value?.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    maxindex = Math.Max(index,maxindex);
                    var input_method_val = dg[7,row].Value?.ToString();
                    var help_en_val      = dg[8,row].Value?.ToString();
                    var help_jp_val      = dg[9,row].Value?.ToString();

                    ht["$" + index_val + "_name"]         = nml(name);
                    ht["$" + index_val + "_input_method"] = nml(input_method_val);
                    ht["$" + index_val + "_help_en"]      = nml(help_en_val);
                    ht["$" + index_val + "_help_jp"]      = nml(help_jp_val);
                }
            }
        }
         
        ht["@__MAXINDEX__@"] = maxindex.ToString();

        var nl = Environment.NewLine;
        var s = ";{6583826A-E287-4D19-BD0F-9CB8F3A20684}" + nl;
        s    += IniUtil.MakeOutput(ht,nl);

        return s;
    }
    #endregion
    #region インポート
    Hashtable m_ht;
    string[]  m_namelist;
    int[]     m_namerowlist;
    List<string> m_name_by_row_order_list;
    bool make_ht_from_import()
    {
        try { 
            var s = Clipboard.GetText();
            if (string.IsNullOrEmpty(s)) return false;
            if (!s.StartsWith(";" + M_EXPORT_GUID)) return false;
            m_ht = IniUtil.CreateHashtable(s);
            if (m_ht == null) return false;
            m_namelist = IniUtil.GetValueFromHashtable(G_NAMEINFO, I_NAMELIST, m_ht).Split(','); 
            m_namerowlist = ParseUtil.ParseIntArray(IniUtil.GetValueFromHashtable(G_NAMEINFO, I_NAMEROWLIST, m_ht));

            //m_name_by_row_orderの作成
            m_name_by_row_order_list = new List<string>();
            for(var row = 0; row < 1000; row++)
            {
                var index = Array.IndexOf( m_namerowlist, row);
                if (index >= 0)
                {
                    m_name_by_row_order_list.Add(m_namelist[index]);
                }
                else
                {
                    m_name_by_row_order_list.Add(null);
                }
            }
            return true;
        } catch (SystemException e)
        {

        }
        return false;
    }
    bool overwrite_exist_items()
    {
        Func<string,string> nml = (_) => { if (_==null) return string.Empty; return _; };
        var dg = m_form.dataGridView1;
        for(var row = 0; row < dg.Rows.Count; row++)
        {
            var index_val = dg[0,row].Value?.ToString();
            if (RegexUtil.IsMatch(@"^[0-9]+$",index_val))
            {
                var index = int.Parse(index_val);
                var name = dg[1,row].Value?.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    var imported_methodval = IniUtil.GetValueFromHashtable(G_INPMETHOD,name,m_ht);
                    var imported_help_en   = IniUtil.GetValueFromHashtable(G_HELP,name + I_HELPEN, m_ht);
                    var imported_help_jpn  = IniUtil.GetValueFromHashtable(G_HELP,name + I_HELPJPN,m_ht);

                    if (!string.IsNullOrEmpty(imported_methodval)) {   dg[7,row].Value = imported_methodval; }
                    if (!string.IsNullOrEmpty(imported_help_en))   {   dg[8,row].Value = imported_help_en;   }
                    if (!string.IsNullOrEmpty(imported_help_jpn))  {   dg[9,row].Value = imported_help_jpn;  }
                }
            }
        }
        return true;

    }
    //bool import_new_items()
    //{
    //    //追加分を抽出
    //    var add_list = new List<string>(m_namelist);
    //    foreach(var n in G.name_list)
    //    {
    //        if (add_list.Contains(n))
    //        {
    //            add_list.Remove(n);
    //        }
    //    }
    //    var dst_org_list = G.name_list; //出力先のオリジナルリスト




    //}
    bool import_new_items()
    {
        var sm = new ItemList_IO_InsertControl();
        sm.m_iec = m_iec;
        sm.m_src_list = new List<string>( m_namelist);
        sm.get_method = (n)  => IniUtil.GetValueFromHashtable(G_INPMETHOD,n,m_ht);
        sm.get_helpen = (n)  => IniUtil.GetValueFromHashtable(G_HELP,n + I_HELPEN, m_ht);
        sm.get_helpjpn = (n) => IniUtil.GetValueFromHashtable(G_HELP,n + I_HELPJPN,m_ht); 
        sm.m_form = m_form;
        sm.Run();

        return true;
    }
    bool import_new_items_ng()
    {
        var check_list = new List<string>(m_namelist);
        foreach(var n in G.name_list)
        {
            if (check_list.Contains(n))
            {
                check_list.Remove(n);
            }
        }

        //find branch in dg
        var dg = m_form.dataGridView1;
        var branchrow = -1;
        for(var row = 0; row < dg.Rows.Count; row++)
        {
            var val = dg[1,row].Value?.ToString();
            if (val == G.STATENAME_branch)
            {
                branchrow = row;
            }
        }

        if (branchrow == -1) {
            G.NoticeToUser_warning("{D1BCA0D8-3C9E-4267-8025-3362417A665D}");
            return false;
        }

        var insert_row = branchrow --;
        if (insert_row <0) insert_row = 0;
        
        for(var i = check_list.Count - 1; i >= 0; i--)
        {
            var n = check_list[i];
            var imported_methodval = IniUtil.GetValueFromHashtable(G_INPMETHOD,n,m_ht);
            var imported_help_en   = IniUtil.GetValueFromHashtable(G_HELP,n + I_HELPEN, m_ht);
            var imported_help_jpn  = IniUtil.GetValueFromHashtable(G_HELP,n + I_HELPJPN,m_ht);

            m_iec.insert_item_to_dg(n,insert_row,imported_help_en, imported_help_jpn, imported_methodval);
        }    
        return true;
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

