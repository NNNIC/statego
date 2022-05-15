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

namespace stateview
{
    internal class ExcelProgram
    {
        //readonly string sheetname = "state-chart";
        const int NAME_COL  = 2;
        const int START_COL = 3;
        const int STATE_ROW = 2;

        int             m_maxrow;
        int             m_maxcol;

        internal void Init()
        {
            m_maxrow = G.m_excel_max_row;
            m_maxcol = G.m_excel_max_col;
        }

        internal int GetMaxRow()
        {
            return m_maxrow;
        }

        internal int GetMaxCol()
        {
            return m_maxcol;
        }

        internal ExcelCellCacheItem GetCell(int row, int col)
        {
            var key = ExcelCellCacheItem.make_key(row,col);
            if (G.m_excel_cell_cache_dic.ContainsKey(key))
            {
                return G.m_excel_cell_cache_dic[key];
            }
            return null;
        }

        internal string GetString(int row, int col)
        {
            var c = GetCell(row,col);
            if (c!=null)
            {
                return c.text;
            }
            return null;
        }

        #region statelist

        List<string> m_state_list = null;
        List<int>    m_state_col_list = null;
        /// <summary>
        /// ステートリストを取得。キャッシュ持ち
        /// </summary>
        internal List<string> GetStateList()
        {
            //if (m_state_list!=null) return m_state_list;

            m_state_list = new List<string>();
            m_state_col_list = new List<int>();
            for(var col = START_COL; col <= 10000; col++)
            {
                var s = GetString(STATE_ROW,col);
                if (s==null) continue;
                s = s.Trim();
                if (string.IsNullOrEmpty(s)) continue;
                if (StateUtil.IsValidStateName(s))
                {
                    m_state_list.Add(s);
                    m_state_col_list.Add(col);
                }
            }
            return m_state_list;
        }
        internal List<string> GetCapitalizedStateList()
        {
            var list = new List<string>();
            m_state_list.ForEach(i=>list.Add(i.ToUpper()));
            return list;
        }
        internal List<int> GetStateColList()
        {
            if (m_state_col_list==null)
            {
                var list = GetStateList();
            }
            return m_state_col_list;
        }

        internal List<string> GetNameList()
        {
            if (m_name_list == null)
            {
                m_name_list     = new List<string>();
                m_name_row_list = new List<int>();
                for(var row = 1; row <= m_maxrow; row++)
                {
                    var s = GetString(row,NAME_COL);
                    if (s==null) continue;
                    s = s.Trim();
                    if (String.IsNullOrEmpty(s)) continue;
                    var c = (int)(s.ToLower())[0];
                    if (c=='!' || c=='_' || (c>='a' && c<='z'))
                    {
                        m_name_list.Add(s);
                        m_name_row_list.Add(row);
                    }
                }
            }
            return m_name_list;
        }
        internal List<int> GetNameRowList()
        {
            if (m_name_list == null)
            {
                var x = GetNameList();
            }
            return m_name_row_list;
        }
        /// <summary>
        /// GetNameRowList中の最大ＲＯＷ
        /// </summary>
        /// <returns></returns>
        internal int GetNameMaxUsingNameRowList()
        {
            var list = m_name_row_list;
            int max = 0;
            foreach(var n in m_name_row_list)
            {
                max = Math.Max(max,n);
            }
            return max;
        }


        internal int GetCol(string state)
        {
            var index = m_state_list.FindIndex(i=>i == state);
            if (index < 0)
            {
                return -1;
            }
            var col = m_state_col_list[index];
            return col;
        }

        /// <summary>
        /// ステートと名前でセルを取得
        /// </summary>
        internal ExcelCellCacheItem GetCell(string state, string name)
        {
            var col = GetCol(state);
            var row = GetRow(name);
            return GetCell(row,col);
        }
        internal ExcelCellCacheItem GetCellWithBasestate(string state, string name)
        {
            var new_state = state;
            for (var loop = 0; loop < 10; loop++)
            {
                var val = GetCell(new_state, name);
                if (val!=null &&  !string.IsNullOrEmpty( val.text ))
                {
                    return val;
                }
                {
                    var next_cell = GetCell(new_state, G.STATENAME_basestate);
                    if (next_cell != null)
                    {
                        if (string.IsNullOrEmpty(next_cell.text))
                        {
                            return next_cell;
                        }
                        else
                        {
                            new_state = next_cell.text;
                            continue;
                        }
                    }
                    return val;
                }
            }
            throw new SystemException("Unexpected! {24BFCA15-92D1-4C74-87C1-E4FD1DBD2ED3}");
        }

        /// <summary>
        /// ステートと名前で文字列取得
        /// </summary>
        internal string GetString(string state, string name)
        {
            var c = GetCell(state,name);
            if (c!=null)
            {
                return c.text;
            }
            return null;
        }

        /// <summary>
        /// [[hoge]]時、に中を展開する
        /// </summary>
        internal string GetStringIfSubname(string state, string name)
        {
            var val = GetString(state, name);
            if (!string.IsNullOrEmpty(val) && RegexUtil.IsMatch(@"\[\[.+\]\]",val))
            {
                var s = val.Substring(2);
                s = s.Substring(0,s.Length - 2);
                if (!string.IsNullOrEmpty(val))
                {
                    var val2 = GetString(state, s);
                    if (!string.IsNullOrEmpty(val2))
                    {
                        val = val2;
                    }
                }
            }
            return val;
        }

        internal string GetStringWithBasestate(string state, string name)
        {
            var new_state = state;
            for (var loop = 0; loop < 10; loop++)
            {
                var val = GetString(new_state, name);
                if (string.IsNullOrEmpty(val))
                {
                    var next_state = GetString(new_state, G.STATENAME_basestate);
                    if (string.IsNullOrEmpty(next_state))
                    {
                        return val;
                    }
                    else
                    {
                        new_state = next_state;
                        continue;
                    }
                }
                return val;
            }
            throw new SystemException("Unexpected! {05B42BC0-CFF9-430B-BC0C-F400519222BA}");
        }

        /// <summary>
        /// state-typを確認して、Gosubが必須かを得る
        /// </summary>
        internal bool IsMandatoryGosub(string state)
        {
            var typ = GetString(state, G.STATENAME_statetyp);
            return (typ == WordStorage.Store.state_typ_gosub || typ == WordStorage.Store.state_typ_loop);
        }



        /// <summary>
        /// ステートと名前で文字列取得　複数行対応
        /// </summary>
        internal string GetStringMultiLine(string state, string name)
        {
            var c  = GetString(state,name);
            if (c==null) return null;
            return c.Replace("\x0a",Environment.NewLine);
        }

        /// <summary>
        /// ステートのuuid取得
        /// 未定義時は　－１
        /// </summary>
        internal double GetUUID(string state)
        {
            var s = GetString(state, G.STATENAMESYS_uuid);
            if (string.IsNullOrEmpty(s)) return -1;
            double o = 0f;
            if (double.TryParse(s,out o))
            {
                return o;
            }
            return -1;
        }
        /// <summary>
        /// ステートのuuid設定
        /// </summary>
        internal void SetUUID(string state, double id)
        {
            SetString(state,G.STATENAMESYS_uuid,id.ToString());
        }
        List<string> m_name_list = null;
        List<int>    m_name_row_list = null;
        /// <summary>
        /// 名前の行を取得　キャッシュ持ち
        /// </summary>
        internal int GetRow(string name)
        {
            GetNameList();
            var idx = m_name_list.FindIndex(i=>i==name);
            if (idx < 0) return -1;
            return m_name_row_list[idx];
        }

        internal Bitmap GetBmp(string state, string name)
        {
            var col = GetCol(state);
            var row = GetRow(name);

            var i = G.excel_pictures.GetItem(row,col);
            if (i!=null)
            {
                return i.bmp;
            }
            return null;
        }

        #endregion

        //#region 項目nameで指定されたファイルのBMPを取得
        //internal Bitmap GetFileThumbnailBmp(string state, string name)
        //{
        //    var file = GetString(state,name);
        //    if (string.IsNullOrEmpty(file)) return null;
        //    var path = Path.Combine( G.thumbnailDir,file);
        //    if (!File.Exists(path)) return null;
        //    return new Bitmap(path);
        //}
        //#endregion

        //指定ステートのすべて値を取得する
        internal Dictionary<string,string> GetAllString(string state)
        {
            var dic = new Dictionary<string,string>();
            var col = GetCol(state);
            for(var i=0;i<m_name_list.Count;i++)
            {
                var name=m_name_list[i];
                var row = m_name_row_list[i];
                var str = GetString(row,col);
                dic.Add(name,str);
            }
            return dic;
        }

        //値を変更

        internal void SetStringIfExist(string state, string name, string val)
        {
            var col = GetCol(state);
            var row = GetRow(name);
            if (col >= 0 && row >= 0)
            {
                SetString(state, name, val);
            }
        }
        internal void SetString(string state, string name, string val)
        {            
            //リファレンスとして name 部分を使う。

            var col = GetCol(state);
            var row = GetRow(name);

            if (row < 0)
            {
                G.NoticeToUser_warning( G.Localize("w_undefinednameincache")/*"Undefined Name in Excel Cache :" */ + name);
                return;
            }

            if (col < 0 )
            {
                G.NoticeToUser_warning( G.Localize("w_undefinedstateincache") /*"Undefined State in Excel Cache :" */ + state);
                return;
            }

            if (GetString(row,col)==val) //同じ値
            {
                return;
            }

            var refitem = G.get_excel_cell_cache(GetRow(name),NAME_COL);
            if (refitem==null)
            {
                G.NoticeToUser_warning("Unexpected! {4E70F191-2862-482C-8101-A629AA794D9A}");
                return;
            }

            var ec = G.get_excel_cell_cache(row,col);

            var citem = refitem.Clone();
            citem.row = row;
            citem.col = col;
            citem.text = val;
            citem.modified= true;
            citem.newitem = (ec==null);

            if (G.m_excel_cell_cache_dic.ContainsKey(citem.key))
            {
                G.m_excel_cell_cache_dic[citem.key] = citem;
            }
            else
            {
                G.m_excel_cell_cache_dic.Add(citem.key,citem);
            }

            if (name == G.STATENAMESYS_pos || name == G.STATENAMESYS_dir)
            {
                G.bDirty_by_edited_pos_only = true;
            }
            else
            {
                G.bDirty_by_modified_value = true;
            }

            /*G.bDirty = true;*/ G.update_viewform_title();
        }

        //一気に値を変更
        internal void SetAllString(string state, Dictionary<string, string> dic)
        {
            foreach(var name in dic.Keys)
            {
                var val = dic[name];
                var org = GetString(state,name);

                if (string.IsNullOrEmpty(val)) val = string.Empty;
                if (string.IsNullOrEmpty(org)) org = string.Empty;

                if (
                    val != org
                    )
                { 
                    SetString(state,name,val);
                }
            }
        }
        //新規ステート　リファレンスあり
        internal string NewState(string refstate, string targetname, string dirpath)
        {
            if (!CheckExists(refstate)) throw new SystemException("Unexpected! {10799900-14A1-4510-9E9A-B712C4EED926}");
            var newstate = get_new_statename(targetname);
            Copy(refstate,newstate);

            foreach(var n in m_name_list)
            {
                if (n[0] == '!') continue; //system use
                if (n == G.STATENAME_state) continue;

                SetString(newstate,n,string.Empty);
            }

            SetString(newstate,G.STATENAME_statecmt,"new state");

            DirPathExcelUtil.set_dirpath(newstate,dirpath);

            return newstate;
        }

        //新規ステート リファレンスなし 名前強制なし
        internal string NewState(string targetname,string dirpath)
        {
            var newstate = get_new_statename(targetname);
            NewState_forceName(newstate,dirpath);
            return newstate;
        }

        //新規ステート　リファレンスなし 名前強制
        internal string NewState_forceName(string newname, string dirpath,bool bAlt = false)
        {
            if (G.state_working_list.Contains(newname)) throw new SystemException("Unexpected! {C7347B3E-6F01-4D07-9D60-205AD8748165}");

            var savedirty = 0; // ALT時はDirtyをセーブ
            if (bAlt)
            {
                savedirty = G.Dirty_save();
            }

            GenerateNewState(newname,bAlt);

            var cmt = "new state";
            if (newname.StartsWith("E_")) cmt = "new embed code";
            else if (newname.StartsWith("C_")) cmt = "new comment";

            if (G.option_set_default_comment)
            {
                SetString(newname,G.STATENAME_statecmt,cmt);
            }

            DirPathExcelUtil.set_dirpath(newname,dirpath);

            if (bAlt) // ALT時は前のDirtyへ
            {
                G.Dirty_restore(savedirty);
            }

            return newname;
        }

        internal string NewState_import(Dictionary<string,string> dic)
        {
            Func<string,string> get = k => {return dic.ContainsKey(k) ? dic[k] : null;};

            var state = get(G.STATENAME_state);
            if (string.IsNullOrEmpty(state)) throw new SystemException("Unexpected! {2DF6FA3A-C621-4793-BCFE-977FB7A5A51D}");

            GenerateNewState(state);

            foreach(var k in dic.Keys)
            {
                if (k==G.STATENAME_state) continue;
                if (k==G.STATENAMESYS_uuid) continue;
                SetString(state,k,dic[k]);
            }
            return state;
        }



        //コピーステート
        internal string Copy(string state)
        {
            var newstate = get_new_statename(state);
            Copy(state,newstate);
            return newstate;
        }

        //先頭から見ていって開いている場所にコピーする。
        internal void Copy(string state, string newstate)
        {
            if (m_state_list.Contains(newstate))
            {
                throw new Exception("Unexpected! {550181BA-D664-4448-A0BA-77D07A0EC790}");
            }

            //空きを探す
            int newcol = StateUtil.FindUnsedColumn();
            if (newcol < 0) //ないときはそこへ
            {
                m_maxcol++;
                newcol = m_maxcol;
            }

            var newuuid = get_new_uuid().ToString();

            foreach(var name in m_name_list)
            {
                var sitem = GetCell(state,name);
                if (sitem==null) continue;
                var ditem = sitem.Clone();
                ditem.modified = true;

                ditem.col = newcol;
                if (name == G.STATENAME_state) ditem.text = newstate;
                if (name == G.STATENAMESYS_uuid) ditem.text = newuuid;
                var key = ditem.key;

                if (G.m_excel_cell_cache_dic.ContainsKey(key))
                {
                    G.m_excel_cell_cache_dic[key] = ditem;
                }
                else
                {
                    G.m_excel_cell_cache_dic.Add(key,ditem);
                }
            }

            m_state_list.Add(newstate);
            m_state_col_list.Add(newcol);

            G.state_working_list     = m_state_list;
            G.state_working_col_list = m_state_col_list;
        }

        //先頭から見ていって開いている場所にコピーする。
        internal void GenerateNewState(string newstate, bool bAlt=false)
        {
            if (m_state_list.Contains(newstate))
            {
                throw new Exception("Unexpected! {550181BA-D664-4448-A0BA-77D07A0EC790}");
            }

            //空きを探す
            int newcol = StateUtil.FindUnsedColumn();
            if (newcol < 0) //ないときはそこへ
            {
                m_maxcol++;
                newcol = m_maxcol;
            }

            var newuuid = get_new_uuid().ToString();

            foreach(var name in m_name_list)
            {
                var row = GetRow(name);
                var sitem = GetCell(row,NAME_COL);
                if (sitem==null) continue;
                var ditem = sitem.Clone();
                ditem.modified = true;
                ditem.text = string.Empty;
                ditem.col = newcol;
                ditem.bAlt = bAlt;

                if (name == G.STATENAME_state) ditem.text = newstate;
                if (name == G.STATENAMESYS_uuid) ditem.text = newuuid;
                var key = ditem.key;

                if (G.m_excel_cell_cache_dic.ContainsKey(key))
                {
                    G.m_excel_cell_cache_dic[key] = ditem;
                }
                else
                {
                    G.m_excel_cell_cache_dic.Add(key,ditem);
                }
            }

            m_state_list.Add(newstate);
            m_state_col_list.Add(newcol);

            G.state_working_list     = m_state_list;
            G.state_working_col_list = m_state_col_list;
        }

        //リネーム
        internal void Rename(string state, string newstate)
        {
            if (m_state_list.Contains(state) && !m_state_list.Contains(newstate))
            {
                var item = GetCell(state,G.STATENAME_state);
                item.text  = newstate;
                item.modified = true;

                var index = m_state_list.IndexOf(state);
                m_state_list[index] = newstate;

                G.state_working_list     = m_state_list;
                G.state_working_col_list = m_state_col_list;
                return;
            }
            throw new Exception("Renemaed name error!");
        }
        //ステート名存在確認
        internal bool CheckExists(string state)
        {
            return m_state_list.IndexOf(state) >= 0;
        }
        //削除
        internal void Delete(string state)
        {
            //if (m_state_list.Count <= 1)
            //{
            //    G.NoticeToUser_warning("Must leave A state.");
            //    return;
            //}

            G.bDirty_by_modified_value = true;

            if (!CheckExists(state))
            {
                G.NoticeToUser_warning( G.Localize("w_thestatewasnotexistbug") /* "The state was not exist !!! Maybe, it is a bug!!" */);
                return;
            }

            var col= GetCol(state);
            foreach(var name in m_name_list)
            {
                var row = GetRow(name);
                var item = GetCell(row,col);
                if (item!=null)
                {
                    item.modified = true;
                    item.text = string.Empty;
                    
                    G.m_excel_cell_cache_dic[item.key] = item;
                }

                var pitem = G.excel_pictures.GetItem(row, col);
                if (pitem != null)
                {
                    pitem.bmp = null;
                    pitem.modifed = true;
                }
            }


            var index = m_state_list.IndexOf(state);
            m_state_list.RemoveAt(index);
            m_state_col_list.RemoveAt(index);

            G.state_working_list = m_state_list;
            G.state_working_col_list = m_state_col_list;
        }
        internal void Delete(int col)
        {
            for(var loop = 0; loop<=10000; loop++)
            {
                if (loop == 10000) throw new SystemException("Unexpected! {BCA352CB-EF57-4F1E-85E8-161179946F07}");
                var bNeedLoop = false;
                foreach(var p in G.m_excel_cell_cache_dic)
                {
                    var i = p.Value;
                    if (i.col == col)
                    {
                        if (!string.IsNullOrEmpty(i.text))
                        {
                            i.text = string.Empty;
                            G.m_excel_cell_cache_dic[i.key] = i;
                            bNeedLoop = true;
                            break;
                        }
                    }
                }
                if (!bNeedLoop) break;
            }
        }

        //
        int get_new_uuid()
        {
            var maxuuid = get_max_uuid();
            return maxuuid+1;
        }
        public int get_max_uuid()
        {
            var maxuuid = 0;
            foreach(var state in m_state_list)
            {
                int n;
                if (int.TryParse(GetString(state, G.STATENAMESYS_uuid),out n))
                {
                    maxuuid = Math.Max(maxuuid,n);
                }
            }
            return maxuuid;
        }
        string get_new_statename(string refname)
        {
            return StateUtil.MakeNewName(refname);
        }

        #region branch便利
        public int    GetBranchIndexCount(string state)
        {
            var brlist = BranchUtil.GetApiAndLabelListFromState(state);
            if (brlist!=null)
            {
                return   brlist.Count;
            }
            else
            {
                return 0;
            }
        }
        public string GetBranchLabel(string state,int index)
        {
            var brlist = BranchUtil.GetApiAndLabelListFromState(state);
            if (brlist!=null)
            {
                var bi = ListUtil.GetVal(brlist,index);
                if (bi!=null)
                {
                    return bi.label;
                }
            }
            throw new SystemException("{86620F61-DA6F-481B-9ACA-1AA89904E31F}");
        }
        public void SetBranchLabel(string state, int index, string label)
        {
            var brlist = BranchUtil.GetApiAndLabelListFromState(state);
            if (brlist!=null)
            {
                var bi = ListUtil.GetVal(brlist,index);
                if (bi!=null)
                {
                    bi.label = label;
                    BranchUtil.SetBranchByApiAndLabelList(state,brlist);
                    return;
                }
            }
            throw new SystemException("{2FC71A1C-DE32-4C01-953F-4409ABC45C0A}");
        }
        #endregion
    }
}
