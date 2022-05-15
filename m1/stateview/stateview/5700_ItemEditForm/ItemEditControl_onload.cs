using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl  {

    readonly string CN_INDEX = "index";
    readonly int    CL_INDEX = 50;

    readonly string CN_NAME  = "name";
    readonly int    CL_NAME  = 100;

    readonly string CN_ATTR = "attr";
    readonly int    CL_ATTR = 60;

    readonly string CN_COND = "condition";
    readonly int    CL_COND = 80;


    //readonly string CN_BKCLR = "backcolor";
    //readonly int    CL_BKCLR = 60;

    readonly string CN_S = "S";
    readonly int    CL_S =  30;

    readonly string CN_METHOD="input_method";
    readonly int    CL_METHOD= 120;

    readonly string CN_HELPEN = "help_en";
    readonly int    CL_HELPEN =  200;
    
    readonly string CN_HELPJP = "help_jp";
    readonly int    CL_HELPJP =  200;

    public readonly string CHECKMARK = "✔";
    public readonly string SEPARATORMARK = "-separator-";
    public readonly string NEWMARK="new";

    Dictionary<string, int> m_cellname_dic;

    //Cell Collumn 
    public int CC_INDEX { get {  return m_cellname_dic[CN_INDEX]; } }
    public int CC_ATTR { get { return m_cellname_dic[CN_ATTR]; } }
    public int CC_NAME { get { return m_cellname_dic[CN_NAME]; } }
    public int CC_COND { get { return m_cellname_dic[CN_COND]; } }
    //public int CC_BKCLR { get { return m_cellname_dic[CN_BKCLR]; } }
    public int CC_S0 { get { return m_cellname_dic[CN_S + "0"]; } }
    public int CC_S1 { get { return m_cellname_dic[CN_S + "1"]; } }
    public int CC_S2 { get { return m_cellname_dic[CN_S + "2"]; } }

    public int CC_METHOD { get { return m_cellname_dic[CN_METHOD]; } }

    public int CC_HELPEN { get { return m_cellname_dic[CN_HELPEN]; } }
    public int CC_HELPJP { get { return m_cellname_dic[CN_HELPJP]; } }

    public int[] CC_S_LIST { get { return new int[] { CC_S0,CC_S1,CC_S2  };  } }

    public enum Cond
    {
        none,
        exclusion,
        share
    }
    public enum Attr
    {
        none,
        read_only,
        hide
    }

    Info get_info()
    {
        var info = new Info();
        info.load();

        return info;
    }

    Help get_help()
    {
        var help = new Help();
        help.load();

        return help;
    }

    void make_topheader()
    {
        m_cellname_dic = new Dictionary<string, int>();
        m_dg.RowHeadersVisible = false;
        m_dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        m_dg.Columns.Add(CN_INDEX, CN_INDEX); m_cellname_dic.Add(CN_INDEX, m_dg.Columns.Count-1);
        m_dg.Columns.Add(CN_NAME, CN_NAME);   m_cellname_dic.Add(CN_NAME,  m_dg.Columns.Count - 1);
        m_dg.Columns.Add(CN_ATTR, CN_ATTR);   m_cellname_dic.Add(CN_ATTR,  m_dg.Columns.Count-1);
        m_dg.Columns.Add(CN_COND, CN_COND);   m_cellname_dic.Add(CN_COND,  m_dg.Columns.Count - 1);
        //m_dg.Columns.Add(CN_BKCLR,CN_BKCLR);  m_cellname_dic.Add(CN_BKCLR, m_dg.Columns.Count - 1);

        for (var i = 0; i < m_info.ALLS.Count; i++)
        {
            var s = CN_S + i.ToString();
            m_dg.Columns.Add(s, s);
            m_cellname_dic.Add(s, m_dg.Columns.Count - 1);
        }

        m_dg.Columns.Add(CN_METHOD, CN_METHOD); m_cellname_dic.Add(CN_METHOD, m_dg.Columns.Count - 1);

        m_dg.Columns.Add(CN_HELPEN, CN_HELPEN); m_cellname_dic.Add(CN_HELPEN, m_dg.Columns.Count - 1);
        m_dg.Columns.Add(CN_HELPJP, CN_HELPJP); m_cellname_dic.Add(CN_HELPJP, m_dg.Columns.Count - 1);

        // 全部センタリング
        for (var c = 0; c < m_dg.Columns.Count; c++)
        {
            m_dg.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            m_dg.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
        }

        //width
        m_dg.Columns[ CC_INDEX/*m_cellname_dic[CN_INDEX]*/ ].Width = CL_INDEX;
        m_dg.Columns[ CC_NAME/*m_cellname_dic[CN_NAME]*/  ].Width = CL_NAME;
        m_dg.Columns[ CC_ATTR/*m_cellname_dic[CN_COND]*/ ].Width  = CL_ATTR;
        m_dg.Columns[ CC_COND/*m_cellname_dic[CN_COND]*/ ].Width  = CL_COND;
        m_dg.Columns[ CC_METHOD].Width = CL_METHOD;
        //m_dg.Columns[ CC_BKCLR/*m_cellname_dic[CN_COND]*/ ].Width = CL_BKCLR;


        foreach(var sx in m_info.ALLS)
        {
            m_dg.Columns[ m_cellname_dic[sx] ].Width = CL_S;
        }

        m_dg.Columns[ CC_HELPEN/*m_cellname_dic[CN_HELPEN]*/ ].Width  = CL_HELPEN;
        m_dg.Columns[ CC_HELPJP/*m_cellname_dic[CN_HELPJP]*/ ].Width  = CL_HELPJP;
      

    }

    void make_toprows()
    {
        var ids = m_info.ALLSTATEIDS;
        for (var n = 0; n < ids.Count; n++)
        {
            var id = ids[n];
            m_dg.Rows.Add();

            setitem(n, CN_INDEX, "-");
            setitem(n, CN_COND, Cond.exclusion.ToString());
            setitem(n, CN_NAME, id);

            var sx = m_info.STATELOC_get(id);
            setitem(n, sx, CHECKMARK);
        }
    }

    void make_borader()
    {
        m_dg.Rows.Add(SEPARATORMARK);
    }

    void setitem(int row, string colname, string s)
    {
        var col = m_cellname_dic[colname];
        m_dg[col, row].Value = s;    
    }
    void setbackcoloritem(int row, string colname, int backcolor)
    {
        var col = m_cellname_dic[colname];
        var bc = Libxl.GetColor(backcolor);
        m_dg[col,row].Style.BackColor = bc;
    }
    void make_itemrows()
    {
        var row_itemname_dic = G.psgg_file_w_data ? /*新PSGG*/FileDb_GetRowNameDic() :  /*従来式*/Excel_GetRowNameDic();
        //var row_backcolor_dic = Excel_GetRowBackColorDic();
        var maxrow = G.psgg_file_w_data ? /*新PSGG*/FileDb_MaxRow() : /*従来式*/Excel_MaxRow();

        for (var row = 1; row < maxrow+1; row++)
        {
            var dgrow = m_dg.Rows.Count;
            m_dg.Rows.Add();

            setitem(dgrow, CN_INDEX, row.ToString());

            //if (row_backcolor_dic.ContainsKey(row)) {
            //    var cnum = row_backcolor_dic[row];
            //    setbackcoloritem(dgrow, CN_BKCLR, cnum);
            //    setitem(dgrow,CN_BKCLR,cnum.ToString());
            //}

            var itemname = string.Empty;

            if (row_itemname_dic.ContainsKey(row))
            {
                itemname = row_itemname_dic[row];
            }
            
            if (!string.IsNullOrEmpty(itemname))
            {
                setitem(dgrow, CN_NAME, itemname);

                
                var attr = m_info.get_attr(itemname);
                var cond = m_info.get_cond(itemname);
                var sarg = m_info.get_sarg(itemname);                

                setitem(dgrow, CN_ATTR, attr.ToString());
                setitem(dgrow, CN_COND, cond.ToString());

                //clear all S
                foreach (var sx in m_info.ALLS)
                {
                    setitem(dgrow, sx, "");
                }

                //readonlyは指定のままに
                if (m_info.is_readonly(itemname))
                {
                    if (cond == Cond.exclusion)
                    {
                        var ts = "S0";
                        if (!string.IsNullOrEmpty(sarg) && m_info.ALLS.Contains(sarg))
                        {
                            ts = sarg.Trim('(',')');
                        }
                        setitem(dgrow, ts, CHECKMARK);
                    }
                    else if (cond == Cond.share)
                    {
                        if (!string.IsNullOrEmpty(sarg))
                        {
                            m_info.ALLS.ForEach(ts =>
                            {
                                if (RegexUtil.IsMatch(sarg, ts))
                                {
                                    setitem(dgrow, ts, CHECKMARK);
                                }
                            });
                        }
                        else
                        {
                            if (itemname.StartsWith("!")) //システム系
                            {
                                m_info.ALLS.ForEach(ts =>
                                {
                                    setitem(dgrow, ts, CHECKMARK);
                                });
                            }

                        }

                    }
                } //end readonly
                else
                {
                    var list = m_info.S_list(itemname);
                    if (list == null || list.Count == 0)
                    {
                        list = new List<string>() { "S0" };
                    }
                    list.ForEach(ts => {
                        setitem(dgrow, ts, CHECKMARK);
                    });
                }
            }
        }

        //readonly
        for(var r = 0; r < m_dg.Rows.Count; r++)
        {
            var n = m_dg[CC_NAME,r].Value?.ToString();
            if (n!=null)
            {
                var b = m_info.is_readonly(n);
                if (!b)
                {
                    if (m_dg[CC_INDEX,r].Value?.ToString()=="-") b =true;
                }

                if (b)
                {
                    for(var c = 0;c<m_dg.Columns.Count; c++)
                    {
                        if (c == CC_ATTR)
                        {
                            m_dg[c,r].Style.ForeColor= Color.Red;
                        }
                    }
                }
            }
        }

        //help
        for(var r = 0; r<m_dg.Rows.Count; r++)
        {
            var n = m_dg[CC_NAME,r].Value?.ToString();
            if (n==null) continue;

            var he = m_help.Get(n,false);
            var hj = m_help.Get(n,true);

            m_dg[CC_HELPEN,r].Value = he;
            m_dg[CC_HELPJP,r].Value = hj;
        }

        //inpmethod
        for(var r = 0; r<m_dg.Rows.Count; r++)
        {
            var n = m_dg[CC_NAME,r].Value?.ToString();
            if (n==null)continue;

            var method = m_info.METHOD_get(n);
            m_dg[CC_METHOD,r].Value = method;
        }   
        
        m_dg.Columns[CC_INDEX].SortMode = DataGridViewColumnSortMode.NotSortable;
        m_dg.Columns[CC_NAME].SortMode = DataGridViewColumnSortMode.NotSortable;
        m_dg.Columns[CC_METHOD].SortMode = DataGridViewColumnSortMode.NotSortable;
        m_dg.Columns[CC_HELPEN].SortMode = DataGridViewColumnSortMode.NotSortable;
        m_dg.Columns[CC_HELPJP].SortMode = DataGridViewColumnSortMode.NotSortable;
       
    }
}

