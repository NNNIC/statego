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
    public class StateUtil
    {
        /// <summary>
        /// ステート名作成
        /// 数字部分を１増やす
        /// 無い場合は１が追加
        /// </summary>
        public static string MakeIncName(string name)
        {
            var numstr = RegexUtil.Get1stMatch("[0-9]+$",name);
            if (!string.IsNullOrEmpty(numstr))
            {
                var prename = name.Substring(0,name.Length -  numstr.Length);

                var num = ParseUtil.ParseInt(numstr);
                num++;
                if (numstr.Length>1 && numstr[0]=='0')
                {
                    return prename + num.ToString( "D" + numstr.Length.ToString() );
                }
                else
                {
                    return prename + num.ToString();
                }
            }
            return name +"1";
        }

        /// <summary>
        /// 新規ステート名
        /// </summary>
        public static string MakeNewName(string refname)
        {
            if (!G.excel_program.GetStateList().Contains(refname)) //そもそもリファレンス名が使われていない
            {
                return refname;//使ってよし！
            }

            var newstatename = refname;
            for (var n = 0; n < 10000; n++)
            {
                newstatename = StateUtil.MakeIncName(newstatename);  //_newname(refname,n);
                if (!G.excel_program.GetStateList().Contains(newstatename))
                {
                    return newstatename;
                }
            }
            throw new Exception("Unexpexted! {6A1C9666-7E98-4D22-A6D9-546508A8E7D2}");
        }
        /// <summary>
        /// 新規ステート名 大文字小文字を同一視
        /// </summary>
        public static string MakeNewName_w_ignore_case(string refname)
        {
            var newstatename = refname;
            for (var n = 0; n < 10000; n++)
            {
                newstatename = StateUtil.MakeIncName(newstatename);  //_newname(refname,n);
                if (!G.excel_program.GetCapitalizedStateList().Contains(newstatename))
                {
                    return newstatename;
                }
            }
            throw new Exception("Unexpexted! {6A1C9666-7E98-4D22-A6D9-546508A8E7D2}");
        }

        /// <summary>
        /// ステート名を作成
        /// 後部に "_ex"を追加
        /// </summary>
        public static string MakeExtraName(string name)
        {
            return name + "_ex";
        }
        public static bool IsValidStateName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            return RegexUtil.Get1stMatch(@"[_a-zA-Z]+_[_a-zA-Z0-9]*",name)==name;
        }
        public static void Refactor(string src, string dst)
        {
            if (!G.state_working_list.Contains(src)) throw new SystemException("Unexpected! {3EABFD45-DAB7-4757-9E0F-05D298EFC3D9}");
            if (G.state_working_list.Contains(dst))  throw new SystemException("Unexpected! {7B0CB650-0AC5-4E32-87D3-1D4F2D6CC064}");

            foreach(var s in G.state_working_list)
            {
                //var nextstate = G.excel_program.GetString(s,G.STATENAME_nextstate);
                //if (!string.IsNullOrEmpty(nextstate) && nextstate == src)
                //{
                //    G.excel_program.SetString(s,G.STATENAME_nextstate,dst);
                //}
                foreach(var name in new string[] { G.STATENAME_nextstate, G.STATENAME_basestate, G.STATENAME_gosubstate })
                {
                    var val = G.excel_program.GetString(s, name);
                    if (!string.IsNullOrEmpty(val) && val == src)
                    {
                        G.excel_program.SetString(s, name, dst);
                    }
                }

                var brstr =  G.excel_program.GetString(s,G.STATENAME_branch);
                var blist =BranchParse.Parse(brstr);
                if (blist == null) continue;
                var bModified = false;
                for(var i = 0; i<blist.Length; i++)
                {
                    var item = blist[i];
                    if (item.nextstate == src)
                    {
                        item.value = BranchParse.Replace1stParameter(item.value,dst);
                        bModified = true;
                    }
                }
                if (bModified)
                {
                    var newbranch = string.Empty;
                    for(var i = 0; i<blist.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(newbranch)) newbranch += "\n";
                        newbranch += blist[i].value;
                    }
                    G.excel_program.SetString(s,G.STATENAME_branch,newbranch);
                }
            }
            G.excel_program.Rename(src,dst);
        }
        public static void ContentTab_write(string state)
        {
            var contents = string.Empty;
            var si = G.get_state_info(state);
            if (si != null && si.content_cell != null)
            {
                contents = si.content_cell.text;
                contents = StringUtil.ConverNewLineCharForDisplay(contents);
            }

            var vf = G.view_form;
            //vf.textBoxTabState.Text    = state ;

            //vf.textBoxTabCmt.Text      = G.excel_program.GetStringMultiLine(state,G.STATENAME_statecmt)     ;
            //vf.textBoxTabContents.Text = contents;
            //vf.textBoxTabNext.Text     = G.excel_program.GetString(state,G.STATENAME_nextstate) ;
            //vf.textBoxTabBranch.Text   = G.excel_program.GetStringMultiLine(state,G.STATENAME_branch)    ;

            ////var dd = G.get_draw_data(state);
            //var bmp = G.excel_program.GetBmp(state,G.STATENAME_thumbnail);
            //if (bmp!=null)
            //{
            //    vf.pictureBoxTabThumb.Image = bmp;
            //    vf.pictureBoxTabThumb.Visible = true;
            //    vf.pictureBoxTabThumb.Refresh();
            //}

            //コンバーター準備
            Converter.Prepare();

            //ファンクのみ
            var s = Converter.GetFuncSrc(state);
            G.view_form.scintillaBoxTabFunc.Text = ScrambleText.get(s);//G.view_form.textBoxTabFunc.Text = ScrambleText.get(s);
            G.current_func_src = s;
            G.view_form.label_Linenum.Text = G.Localize("psrc_line") + " " + FindLine.get_find_matched_lines();

            G.view_form.tabControl.SelectedTab = G.view_form.tabPageFunc;

            //フィルターテンプレートの追加 stateとstate-typ処理済みのテンプレート
            G.current_func_template = FilteredTemplate.Make(state);
            G.view_form.m_show_filltered = true; //最初に表示
                                                 //テスト　G.view_form.textBoxTabFunc.Text = G.current_func_template;
            G.view_form.panel1.Focus(); //フォーカス戻す
        }
        public static void ContentTab_clear()
        {
            //var vf = G.view_form;
            //vf.textBoxTabState.Text = string.Empty;
            //vf.textBoxTabCmt.Text   = string.Empty;
            //vf.textBoxTabBranch.Text= string.Empty;
            //vf.textBoxTabNext.Text  = string.Empty;

            //vf.pictureBoxTabThumb.Image = null;
            //vf.pictureBoxTabThumb.Visible = false;
            //vf.pictureBoxTabThumb.Refresh();
        }
        public static int FindUnsedColumn()
        {
            if (G.state_working_col_list==null) throw new SystemException("Unexpected! {80C71047-CC71-4191-9B1E-79617969B2FE}");
            if (G.state_working_col_list.Count <= 1) return -1; //無し

            var newlist = new List<int>(G.state_working_col_list); //コピー
            newlist.Sort();

            var i = newlist[0];
            for(var si = i; si<newlist[newlist.Count-1]; si++)
            {
                if (!newlist.Contains(si)) return si;
            }

            return -1;
        }
        public static string MakeExportIni(string state)
        {
            var s= string.Empty;
            if (!IsValidStateName(state)) {
                G.NoticeToUser_warning("State name is invalid :" + state + ": {3EE29A3A-5880-401D-B653-F1A37755C070}");
                return s;
            }
            var nl = Environment.NewLine;
            var namelist = G.excel_program.GetNameList();
            foreach(var name in namelist)
            {
                var value = G.excel_program.GetString(state,name);
                if (string.IsNullOrEmpty(value)) continue;
                /*
                   stateが外部ステート時、posを表示位置に変更させる
                */
                if (name == G.STATENAMESYS_pos)
                {
                    value = change_pos_if_external_state(state, value);
                }

                s += name + "=@@@" + nl;
                s += value + nl;
                s += "@@@" + nl;                 
            }
            return s;
        }
        private static string change_pos_if_external_state(string state, string orgvalue)
        {//stateが外部ステート時、posを表示位置に変更させる
            var style = G.node_get_style(state);
            if (style == SS.STYLE.FORDST || style == SS.STYLE.FORSRC)
            {
                var posx = LocUtil.Get_lo_position(state);
                if (posx!=null)
                {
                    var pos= Point.Round( ((PointF)posx) );
                    return pos.X.ToString() + "," + pos.Y.ToString();
                }
            }
            return orgvalue;
        }

        public static string MakeExportListIni(List<string> statelist)
        {
            var s = string.Empty;
            if (statelist == null || statelist.Count == 0) {
                G.NoticeToUser_warning("Unexped! {3CB18EA7-100F-4119-958B-3539D3D24826}");
                return s;
            }
            var nl = Environment.NewLine;
            foreach(var state in statelist)
            {
                if (!string.IsNullOrEmpty(s)) s+= nl;
                s+= string.Format("[{0}]",state) + nl;
                s+= MakeExportIni(state);
            }
            return s;
        }
        public static Dictionary< string, Dictionary<string, string> > MakeImportIni(string s)
        {
            try {
                var dicdic = new Dictionary<string, Dictionary<string, string>>();
                var ht = IniUtil.CreateHashtable(s);
                foreach(var cat in ht.Keys)
                {
                    var ht2 = (Hashtable)ht[cat.ToString()];
                    if (ht2==null) {
                        G.NoticeToUser_warning("Unexpected! {857DCF0D-672B-42F4-80CF-B3A10B5A7A53}");
                        continue;
                    }
                    var dic = new Dictionary<string,string>();
                    foreach(var name in ht2.Keys)
                    {
                        var v = ht2[name];
                        if (v==null) continue;
                        dic.Add(name.ToString(),v.ToString());
                    }
                    dicdic.Add(cat.ToString(),dic);
                }
                return dicdic;
            } catch (SystemException e)
            {
                G.NoticeToUser_warning(e.Message + "Unexpected! {62E834D8-3FEE-4013-90B1-8C9AA0260286}");
            }
            return null;
        }
        public static Dictionary<string, Dictionary<string, string>> Rename(Dictionary<string, Dictionary<string, string>> dicdic, string src_state, string to_state)
        {
            //内部のステート名を全変更
            // 1. state
            // 2. nextstate,basestate,gosubstate
            // 3. branch
            foreach(var key in dicdic.Keys)
            {
                var dic = dicdic[key];
                Func<string,string> getdic = (k)=> {
                    if (dic.ContainsKey(k))  return dic[k];
                    return null;
                };

                var statevalue = getdic(G.STATENAME_state);
                if (statevalue == src_state)
                {
                    dic[G.STATENAME_state] = to_state;
                }

                //var nextstatevalue = getdic(G.STATENAME_nextstate);
                //if (nextstatevalue == src_state)
                //{
                //    dic[G.STATENAME_nextstate] = to_state;
                //}

                foreach(var name in new string[] { G.STATENAME_nextstate, G.STATENAME_basestate, G.STATENAME_gosubstate })
                {
                    var val = getdic(name);
                    if (val == src_state)
                    {
                        dic[name] = to_state;
                    }
                }

                var branchvalue = getdic(G.STATENAME_branch);
                var brcondvalue = getdic(G.STATENAME_brcond);
                var branchcmt   =getdic(G.STATENAME_branchcmt);
                var apilist = BranchUtil.GetApiAndLabelListFromString(branchvalue,brcondvalue,branchcmt);
                if (apilist!=null)
                {
                    foreach(var a in apilist)
                    {
                        if (a.label == src_state)
                        {
                            a.label = to_state;
                        }
                    }
                    BranchUtil.MakeBranchStringFromApiAndLabelList(apilist,out branchvalue, out brcondvalue, out branchcmt);
                    dic[G.STATENAME_branch] = branchvalue;
                }
            }
            //diddicのキー
            var newdicdic = new Dictionary<string, Dictionary<string, string>>();
            foreach(var k in dicdic.Keys)
            {
                var newkey = k;
                if (k == src_state)
                {
                    newkey = to_state;
                }
                newdicdic.Add(newkey,dicdic[k]);
            }
            return newdicdic;
        }

        /// <summary>
        /// 流入している全てのステートと流出している全てのステートを収集する。
        /// ※最適化のため同時に収集
        /// G.state_input_src_list と G.state_output_src_listになる
        /// </summary>
        public static void Collect_all_input_output_info(out Dictionary<string, List<InOutBaseData>> input_src_dic, out Dictionary<string, List<InOutBaseData>> output_dst_dic)
        {
            var allstates = G.excel_program.GetStateList();

            //流入用辞書
            var srcdic = new Dictionary<string, List<InOutBaseData>>();
            Action<string,string,InOutBaseData.ATTRIB,int> add_srcdic = (_src,_dst, attr,br_index)=> {
                if (AltState.IsAltState(_src) || AltState.IsAltState(_dst))
                {
                    return; //
                }

                var key = _dst;
                List<InOutBaseData> list = null;
                if (srcdic.ContainsKey(key))
                {
                    list = srcdic[key];
                }            
                else
                {
                    list = new List<InOutBaseData>();
                    srcdic.Add(key,list);
                }
                if (list.Find(i=>(i.state == _src && i.attrib == attr && i.branch_index == br_index))==null)
                {
                    list.Add(new InOutBaseData() { target_state = key, state = _src, attrib = attr,branch_index = br_index });
                }
                srcdic[key] = list;
            };
            //流出用辞書
            var dstdic = new Dictionary<string, List<InOutBaseData>>();
            Action<string,string, InOutBaseData.ATTRIB,int> add_dstdic = (_src,_dst, attr, br_index)=> {
                var key = _src;
                List<InOutBaseData> list = null;
                if (dstdic.ContainsKey(key))
                {
                    list = dstdic[key];
                }            
                else
                {
                    list = new List<InOutBaseData>();
                    dstdic.Add(key,list);
                }
                if (list.Find(i=>(i.state == _dst && i.attrib == attr && i.branch_index == br_index))==null)
                {
                    list.Add(new InOutBaseData() { target_state = key, state = _dst, attrib = attr,branch_index = br_index });
                }
                dstdic[key] = list;
            };
    
            foreach(var s in allstates)
            {
                var nextstate = G.excel_program.GetStringWithBasestate(s,G.STATENAME_nextstate);
                var gosub = G.excel_program.GetString(s,G.STATENAME_gosubstate);
                var bdlist = BranchUtil.GetApiAndLabelListFromState(s);
                var basestate = G.excel_program.GetString(s,G.STATENAME_basestate);

                if (IsValidStateName(nextstate) && allstates.Contains(nextstate))
                {
                    add_srcdic(s,nextstate, InOutBaseData.ATTRIB.nextstate,-1);
                    add_dstdic(s,nextstate, InOutBaseData.ATTRIB.nextstate,-1);
                }
                if (IsValidStateName(gosub) && allstates.Contains(gosub))
                {
                    add_srcdic(s,gosub, InOutBaseData.ATTRIB.gosub,-1);
                    add_dstdic(s,gosub, InOutBaseData.ATTRIB.gosub,-1);
                }
                for(var i = 0; i < bdlist.Count; i++)
                {
                    var bd = bdlist[i];
                    var dst = bd.label;
                    if (IsValidStateName(dst) && allstates.Contains(dst))
                    {
                        add_srcdic(s,dst, InOutBaseData.ATTRIB.branch, i);
                        add_dstdic(s,dst, InOutBaseData.ATTRIB.branch, i);
                    }
                }
                if (IsValidStateName(basestate) && allstates.Contains(basestate))
                {
                    add_srcdic(s,basestate, InOutBaseData.ATTRIB._base, -1);
                    add_dstdic(s,basestate, InOutBaseData.ATTRIB._base, -1);
                }
            }

            input_src_dic  = srcdic;
            output_dst_dic = dstdic;
        }

        /// <summary>
        /// 表示中のグループノードの流入先を収集する
        /// Analizeの最後に実行
        /// </summary>
        public static Dictionary<string, List<string>> Collect_groupnode_inflow_states_on_current()
        {
            /*
                １. カレント上の全グループノードを収集
                ２．各グループのどごとに、流入ステートを収集

            　　※以下は、各グループごとに
                
            　　３．G.state_input_src_listのベース以外を対象として
                ４．グループ内のノードを除去
                ５．結果辞書に登録
            */

            var outdic = new Dictionary<string, List<string>>();

            var grouplist = new List<string>();
            G.state_working_list.ForEach(i=> { if (AltState.IsAltState(i)) grouplist.Add(AltState.TrimAltStateName(i));} );
            if (grouplist.Count == 0 ) return outdic;

            foreach(var g in grouplist)
            {
                var allstate_in_thegroup = G.node_get_allstates_on_groupnode(g);
                if (allstate_in_thegroup == null) continue;
                var tmplist = new List<string>();
                allstate_in_thegroup.ForEach(i=> {
                    var srclist = DictionaryUtil.Get(G.state_input_src_list,i);
                    if (srclist==null) return;
                    srclist.ForEach(j =>{
                        if (j.attrib == InOutBaseData.ATTRIB._base) return;
                        if (allstate_in_thegroup.Contains(j.state) ) return;
                        tmplist.Add(j.state);
                    });
                });
                outdic.Add(g,tmplist);
            }

            return outdic;
        }
        /// <summary>
        /// 指定ステートはベースステートとして参照されているか？
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool IsBaseState(string state)
        {
            if (G.state_output_dst_list!=null)
            {
                foreach(var k in G.state_output_dst_list.Keys)
                {
                    var list = G.state_output_dst_list[k];
                    foreach(var d in list)
                    {
                        if (d.attrib == InOutBaseData.ATTRIB._base)
                        {
                            if (d.state == state)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// ステートリストの中にベースステートがあるか？
        /// </summary>
        public static bool HasBaseStateInList(List<string> statelist)
        {
            if (statelist == null || statelist.Count == 0) return false;
            if (G.state_output_dst_list!=null)
            {
                foreach(var k in G.state_output_dst_list.Keys)
                {
                    var list = G.state_output_dst_list[k];
                    foreach(var d in list)
                    {
                        if (d.attrib == InOutBaseData.ATTRIB._base)
                        {
                            if ( statelist.Contains( d.state))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static bool DeleteGroupList(List<string> states_on_group)
        {
            if (states_on_group!=null)
            {
                foreach(var state in states_on_group)
                {
                    if (AltState.IsAltState(state))
                    {
                        var templist = G.node_get_allstates_on_groupnode(AltState.TrimAltStateName(state));
                        if (templist!=null)
                        {
                            foreach(var s2 in templist)
                            {
                                G.excel_program.Delete(s2);
                            }
                        }
                    }
                    else
                    {
                        G.excel_program.Delete(state);
                    }
                }
                return true;

            }
            return false;
        }

        public static void CommentoutGroupList(List<string> states_on_group)
        {
            Action<string> co = (st)=> {
                var newname = st;
                if (st.StartsWith("S_") || st.StartsWith("E_"))
                {
                    newname = "C_" + st.Substring(2);
                } 
                else
                {
                    return;
                }
                if (G.excel_program.CheckExists(newname))
                {
                    newname = MakeNewName(newname);
                }
                G.excel_program.Rename(st, newname);
            };

            if (states_on_group!=null)
            {
                foreach(var state in states_on_group)
                {
                    if (AltState.IsAltState(state))
                    {
                        var templist = G.node_get_allstates_on_groupnode(AltState.TrimAltStateName(state));
                        if (templist!=null)
                        {
                            foreach(var s2 in templist)
                            {
                                co(s2);
                            }
                        }
                    }
                    else
                    {
                        co(state);
                    }
                }
            }
        }
    }
}
