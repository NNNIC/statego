using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl
{

    //      グローバルアクセス       G.itemsInfo_program
    public class Info
    {
        string NL { get { return Environment.NewLine; } }

        readonly string CAT_STATELOC = "stateloc";
        readonly string CAT_ITEMCOND = "itemcond";
        readonly string CAT_INPMETHOD= "inpmethod";
        readonly string CAT_S = "S";
        readonly int S_MAX = 30;

        public List<string> ALLS
        {
            get {
                var catht = m_ht[CAT_STATELOC] as Hashtable;
                if (catht != null)
                {
                    var list = new List<string>();
                    foreach (var k in catht.Keys)
                    {
                        var v = catht[k].ToString().Trim();
                        if (!list.Contains(v))
                        {
                            list.Add(v);
                        }
                    }
                    list.Sort();
                    return list;
                }
                return null;
            }
        }
        public List<string> ALLSTATEIDS
        {
            get
            {
                var catht = m_ht[CAT_STATELOC] as Hashtable;
                if (catht != null)
                {
                    var list = new List<string>();
                    foreach (var k in catht.Keys)
                    {
                        var v = k.ToString();
                        if (!list.Contains(v))
                        {
                            list.Add(v);
                        }
                    }
                    list.Sort();
                    return list;
                }
                return null;
            }
        }

        /*
            本テンプレートは、item_infoがnull時に利用される。
            

        */


        string TEMPLATE
        {
            get
            {
                return

@"[" + CAT_STATELOC + "]" + NL +
@"S_=S0" + NL +
@"E_=S1" + NL +
@"C_=S2" + NL +
@"" + NL +
@"[" + CAT_ITEMCOND + "]" + NL +      // p0:ro|mod p1:share|exc p2:exc時のデフォルト
@"thumbnail=read_only,share,S.+" + NL +   //ro=readonly  m=modifiable h=hide
@"state=read_only,share,S.+" + NL +       //exc=exclusion share=share
@"nextstate=read_only,exclusion,S0" + NL +
@"basestate=read_only,share,S.+" + NL +
@"gosubstate=read_only,exclusion,S0" + NL +
@"return=read_only,exclusion,S0" + NL +
@"embed=read_only,exclusion,S1" + NL +
@"branch=read_only,exclusion,S0" + NL +
@"brcond=read_only,exclusion,S0" + NL +
@"" + NL + 
@"[" + CAT_INPMETHOD + "]" + NL +
@"nowait=@@@" +NL +
@"*select" + NL +
@"nowait" + NL +
@"@@@" + NL +
@"" + NL +
//@"[S0]" + NL +  //S0に属するitems   冗長につき削除   
//@"[S1]" + NL +  //S1に属するitems
//@"[S2]" + NL +  //S2に属するitems

@"";
            }
        }

        Hashtable m_ht;

        public void load()
        {
            m_ht = IniUtil.CreateHashtable(G.excel_convertsettings.m_items_ini);
            if (m_ht == null || m_ht.Count == 0)
            {
                m_ht = IniUtil.CreateHashtable(TEMPLATE);
            }
        }

        #region STATELOC
        public string STATELOC_get(string id)
        {
            return IniUtil.GetValueFromHashtable(CAT_STATELOC, id, m_ht);
        }
        #endregion

        #region GET
        public bool is_readonly(string item)
        {
            if (item!=null && item.StartsWith("!")) return true;

            Attr attr;
            Cond cond;
            string sarg;

            if (get_attr_cond_sarg(item, out attr, out cond, out sarg))
            {
                return attr == Attr.read_only;
            }

            return false;
        }
        public bool is_hide(string item) //システム系と指定はhide
        {
            if (item.StartsWith("!")) return true;

            Attr attr;
            Cond cond;
            string sarg;

            if (get_attr_cond_sarg(item, out attr, out cond, out sarg))
            {
                return attr == Attr.hide;
            }

            return false; //デフォルトは false
        }
        public Attr get_attr(string item)
        {
            if (item.StartsWith("!"))
            {
                return Attr.read_only;
            }

            Attr attr; Cond cond; string sarg;

            if (get_attr_cond_sarg(item, out attr, out cond, out sarg))
            {
                return attr;
            }
            return Attr.none;
        }
        public Cond get_cond(string item)
        {
            if (item.StartsWith("!"))
            {
                return Cond.share;
            }

            Attr attr; Cond cond; string sarg;

            if (get_attr_cond_sarg(item, out attr, out cond, out sarg))
            {
                return cond;
            }

            return Cond.exclusion;
        }
        public string get_sarg(string item)
        {
            Attr attr;
            Cond cond;
            string sarg;

            if (get_attr_cond_sarg(item, out attr, out cond, out sarg))
            {
                return sarg;
            }

            return null;
        }
        public string get_sarg_errorrecover(string item)
        {
            var sarg = get_sarg(item);
            if (!string.IsNullOrEmpty(sarg)) return sarg;

            if (!is_readonly(item))
            {
                return "S0";
            }

            return null;
        }
        public bool get_attr_cond_sarg(string item_name, out Attr attr, out Cond cond, out string sarg)
        {
            attr = Attr.none;
            cond = Cond.none;
            sarg = null;

            var s = IniUtil.GetValueFromHashtable(CAT_ITEMCOND, item_name, m_ht);
            if (string.IsNullOrEmpty(s))
            {
                var parts = item_name.Split('-');
                if (parts != null && parts.Length > 1)
                {
                    s = IniUtil.GetValueFromHashtable(CAT_ITEMCOND, parts[0], m_ht);
                }
                if (string.IsNullOrEmpty(s))
                {
                    return false;
                }
            }

            var tokens = s.Split(',');
            if (tokens.Length > 0)
            {
                attr = EnumUtil.Parse(tokens[0], Attr.none);
            }
            if (tokens.Length > 1)
            {
                cond = EnumUtil.Parse(tokens[1], Cond.none);
            }
            if (tokens.Length > 2)
            {
                sarg = tokens[2];
            }
            return true;
        }
        #endregion

        #region SET
        public void set_attr(string itemname, Attr attr)
        {
            Attr iattr; Cond icond; string isarg;
            get_attr_cond_sarg(itemname, out iattr, out icond, out isarg);
            set_attr_cond_sarg(itemname, attr, icond, isarg, ref m_ht);
        }
        public void set_cond(string itemname, Cond cond)
        {
            Attr iattr; Cond icond; string isarg;
            get_attr_cond_sarg(itemname, out iattr, out icond, out isarg);
            set_attr_cond_sarg(itemname, iattr, cond, isarg, ref m_ht);
        }
        public void set_sarg(string itemname, string sarg)
        {
            Attr iattr; Cond icond; string isarg;
            get_attr_cond_sarg(itemname, out iattr, out icond, out isarg);
            set_attr_cond_sarg(itemname, iattr, icond, sarg, ref m_ht);
        }
        public void set_attr_cond_sarg(string item_name, Attr attr, Cond cond, string sarg, ref Hashtable ht)
        {
            var s = attr.ToString() + "," + cond.ToString() ;
            if (sarg != null)
            {
                s += "," + sarg;
            }
            IniUtil.SetValueFromHashtable(CAT_ITEMCOND, item_name, s, ref m_ht);
        }
        #endregion

        #region SX
        public List<string> S_list(string itemname)
        {
            var sarg = get_sarg(itemname);
            var list = new List<string>();
            for (var n = 0; n < S_MAX; n++)
            {
                var cat = CAT_S + n.ToString();
                if (RegexUtil.Get1stMatch(sarg,cat) == cat)
                {
                    list.Add(cat);
                }
            }
            if (list.Count == 0) return null;
            return list;
        }
        public void S_set(string itemname, List<string> slist)
        {
            if (slist ==null || slist.Count==0) return;
            if (slist.Count==1)
            {
                set_sarg(itemname,slist[0]);
                return;
            }
            var sarg = string.Empty;
            foreach(var sx in slist)
            {
                if (sarg != string.Empty) sarg += "|";
                sarg += "(" + sx + ")";
            }

            set_sarg(itemname,sarg);
        }
        #endregion

        public bool UpdateIni(ItemEditControl pm) // true : need reload
        {

            for(var r = 0; r <pm.m_dg.Rows.Count; r++)
            {
                var index = pm.m_dg[pm.CC_INDEX,r].Value?.ToString();
                if ( !(ParseUtil.ParseInt(index) >= 1 || index == pm.NEWMARK) )
                {
                    continue;
                }
                var itemname = pm.m_dg[pm.CC_NAME,r].Value?.ToString();
                if (string.IsNullOrEmpty(itemname)) continue;

                var attr = pm.m_dg[pm.CC_ATTR,r].Value?.ToString();
                if (attr!=null)
                {
                    var enumattr = EnumUtil.Parse(attr,Attr.none);

                    if (enumattr == Attr.read_only) continue; //変更しない

                    set_attr(itemname,enumattr);
                }

                var cond = pm.m_dg[pm.CC_COND,r].Value?.ToString();
                if (cond!=null)
                {
                    var condenum = EnumUtil.Parse(cond,Cond.none);
                    set_cond(itemname,condenum);
                }

                var sarg = string.Empty;
                foreach(var sx in ALLS)
                {
                    var v = pm.m_dg[pm.m_cellname_dic[sx],r].Value?.ToString();
                    if (v == pm.CHECKMARK)
                    {
                        if (sarg != string.Empty)
                        {
                            sarg += "|(" + sx + ")";
                        }
                        else
                        {
                            sarg += "(" + sx + ")";
                        }
                    }
                }
                set_sarg(itemname, sarg);

            }

            for(var r = 0; r <pm.m_dg.Rows.Count; r++)
            {
                var index = pm.m_dg[pm.CC_INDEX,r].Value?.ToString();
                var itemname = pm.m_dg[pm.CC_NAME,r].Value?.ToString();
                if (string.IsNullOrEmpty(itemname)) continue;

                var method = pm.m_dg[pm.CC_METHOD,r].Value?.ToString();
                if (method==null) method= string.Empty;
                METHOD_set(itemname,method);
            }

            var newht = new Hashtable();
            newht[CAT_STATELOC] = m_ht[CAT_STATELOC]; // ステートロケートはそのま

            var itemcond = (Hashtable)m_ht[CAT_ITEMCOND]; //アイテム条件

            var newitemcond = new Hashtable();
            foreach (var k in itemcond.Keys)
            {
                var v = itemcond[k].ToString();
                if (string.IsNullOrEmpty(v)) continue;
                if (v.StartsWith(Attr.read_only.ToString())) //read-onlyを格納
                {
                    newitemcond.Add(k, v);
                    continue;
                }
                if (!(v.Contains(Cond.exclusion.ToString()) && v.Contains("S0"))) //デフォルト以外を格納
                {
                    newitemcond.Add(k, v);
                    continue;
                }
            }
            newht[CAT_ITEMCOND] = newitemcond;

            newht[CAT_INPMETHOD] = m_ht[CAT_INPMETHOD]; //入力メソッドはそのまま

            bool bNeedload = false;
            var newini = IniUtil.MakeOutput(newht);
            if (StringUtil.MakeCompareLineString(newini) != StringUtil.MakeCompareLineString(G.excel_convertsettings.m_items_ini))
            {
                G.excel_convertsettings.m_items_ini = newini;

                if (G.psgg_file_w_data)
                {
                    FileDbUtil.WriteItemsInfo();  //新PSGGファイル
                }
                else
                { 
                    ExcelSaveOneSheet.WriteItemsInfo(); //従来式
                }
                bNeedload = true;
            }
            return bNeedload;
        }

        #region method
        public string METHOD_get(string id)
        {
            return IniUtil.GetValueFromHashtable(CAT_INPMETHOD, id, m_ht);
        }
        public void METHOD_set(string id, string s)
        {
            IniUtil.SetValueFromHashtable(CAT_INPMETHOD,id,s, ref m_ht);
        }
        #endregion
    }

}

