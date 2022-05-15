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

/*
    ルート /
    パス   /hoge/   <--- 最後は '/'で終わること
*/
namespace stateview
{
    internal class GroupNodeDir
    {
        //private string __target_path = "/";
        public string m_target_path {
            get { return G.m_target_pathdir; }
            set {
                G.m_target_pathdir = value;
                G.update_viewform_title();
            }
        }

        public Hashtable m_standard_state_list {get;private set; }    //通常表示ステート  key名のみ
        public Hashtable m_forsrc_state_list   {get;private set; }    //流入先としてのステート   key名のみ
        public Hashtable m_fordst_state_list   {get;private set; }    //流出先としてのステート   key名のみ

        public Dictionary<string,List<string>> m_subdir_list     {get; private set; } //直下フォルダとその配下のステート
        public Dictionary<string,string>       m_subdir_cmt_list {get; private set; } //直下フォルダとそのコメント
        public Dictionary<string,Point?>       m_subdir_pos_list {get; private set; } //直下フォルダとそのポジション
        public Dictionary<string,SS.STYLE>     m_style_list      {get; private set; } //ステートのスタイルの関連

        public Dictionary<string,Point?>       m_position_list {get; private set; } //ステートの位置

        public List<string> m_altstate_list { get; private set; } //グループ代替ステート

        private List<string> m_all_state_list { get { return G.state_working_list; } }

        private GroupNodeManager m_manager = new GroupNodeManager();

        public void Analize_self()
        {
            Analize(m_target_path);
        }

        public void Analize_enter(string groupname)
        {
            int savedirty = G.Dirty_save();

            SaveNodePosition();
            SaveLoadIni.SaveTempIni();

            var path = GroupNodeUtil.pathcombine(m_target_path, groupname);

            G.NoticeToUser("Entering \"" + path + "\"");

            Analize(path);

            G.Dirty_restore(savedirty);
            G.bDirty_by_moved_group_only = true;
        }
        public bool Analize_leave()
        {
            int savedirty = G.Dirty_save();

            SaveNodePosition();
            SaveLoadIni.SaveTempIni();

            var path = GroupNodeUtil.path_normalize(m_target_path);
            if (path == "/") {
                G.NoticeToUser_warning(G.Localize("w_currentisroot")/*"The current path is the root."*/);
                return false;
            }
            var path2 = path.TrimEnd('/');
            var i = path2.LastIndexOf('/');
            path2 = path2.Substring(0,i);
            path2 = GroupNodeUtil.path_normalize(path2);

            G.NoticeToUser(string.Format("Leaving from \"{0}\" to \"{1}\"",path,path2));

            Analize(path2);

            G.Dirty_restore(savedirty);
            G.bDirty_by_moved_group_only = true;

            return true;
        }
        public void Analize(string path)
        {
            var trypath = path;
            for(var loop = 0; loop<= 100; loop++)
            {
                if (loop == 100) throw new SystemException("Unexpected! {6F99D535-36A2-4E17-BCBF-6C07FB4A744D}");

                if (analize2(trypath)) break;
                if (string.IsNullOrEmpty(trypath) ||  trypath=="/")
                {
                    throw new SystemException("Unexpected! {28C340A9-CA27-4A50-8C8C-5B368B79EB82}");
                }

                //
                var last = RegexUtil.Get1stMatch(@"[a-zA-Z_0-9]+\/$",trypath);
                trypath = trypath.Substring(0,trypath.Length - last.Length);
            }
            G.tabNodeTree.SetCurrent();

            G.group_input_src_list_wo_base_on_current = StateUtil.Collect_groupnode_inflow_states_on_current();
        }
        private bool analize2(string path)
        {

            AltState.DeleteAllAltStates(); //解析前に代替ステート削除　　※①

            m_manager.ReadAllStates();
            var nd = m_manager.FindByPath(path);
            if (nd == null) return false;

            m_target_path = GroupNodeUtil.path_normalize(path);

            //ターゲットディレクトリ直下のステート収集
            m_standard_state_list = new Hashtable();
            foreach(var s in nd.m_state_list)
            {
                m_standard_state_list[s] = true;
            }
            //グループになるフォルダ と そのフォルダ配下を収集
            m_subdir_list = nd.GetAllChildrenOnSubDir();

            //グループ名とコメントリスト
            m_subdir_cmt_list = new Dictionary<string, string>();
            foreach(var g in m_subdir_list.Keys)
            {
                var cmt = GetDirCommentByGroupname(g);
                m_subdir_cmt_list.Add(g,cmt);
            }
            //グループ名とポジション
            m_subdir_pos_list = new Dictionary<string, Point?>();
            foreach(var g in m_subdir_list.Keys)
            {
                var pos = GetDirPositionByGroupname(g);
                m_subdir_pos_list.Add(g,pos);
            }

            //代替ステート作成  excelに書き込む。ーー＞①で消す！　代替ステートは書き込みから除外もしておくこと。
            m_altstate_list = AltState.CreateAltState(m_subdir_list, m_subdir_cmt_list,m_subdir_pos_list,m_target_path);
                                                   // 本時点で、G.m_state_org_list, m_all_state_listに追加される。

            //流入先の収集
            m_forsrc_state_list = new Hashtable();
            foreach(var s in m_all_state_list)
            {
                if (IsStandardOrGroupStates(s)) continue; //表示ステートは調査除外
                var ns = G.excel_program.GetString(s,/*"nextstate"*/ G.STATENAME_nextstate); // NEXT
                if (ns!=null && IsStandardOrGroupStates(ns))
                {
                    m_forsrc_state_list[s] = true;
                }
                var lables = BranchUtil.GetLabelListFromState(s); // BRANCH
                if (lables!=null)
                {
                    foreach(var l in lables)
                    {
                        if (IsStandardOrGroupStates(l))
                        {
                            m_forsrc_state_list[s] = true;
                        }
                    }
                }
                var gs = G.excel_program.GetString(s, /*gosubstate*/ G.STATENAME_gosubstate);
                if (gs!=null && IsStandardOrGroupStates(gs))
                {
                    m_forsrc_state_list[s] = true;
                }
            }
            //流出先の収集
            m_fordst_state_list = new Hashtable();
            foreach(var i in m_all_state_list)
            {
                var s = (string)i;
                if (IsStandardOrGroupStates(s))
                {
                    var ns = G.excel_program.GetString(s,/*"nextstate"*/ G.STATENAME_nextstate); //NEXT STATE
                    if (ns!=null&& !IsStandardOrGroupStates(ns) && m_all_state_list.Contains(ns))
                    {
                        m_fordst_state_list[ns] = true;
                    }
                    var labels = BranchUtil.GetLabelListFromState(s);                           // BRANCH
                    if (labels!=null)
                    {
                        foreach(var l in labels)
                        {
                            if (l!=null && !IsStandardOrGroupStates(l) && m_all_state_list.Contains(l))
                            {
                                m_fordst_state_list[l] = true;
                            }
                        }
                    }
                    var gs = G.excel_program.GetString(s, /*"gosubstate"*/ G.STATENAME_gosubstate); //GOSUB 
                    if (gs!=null && !IsStandardOrGroupStates(gs) && m_all_state_list.Contains(gs))
                    {
                        m_fordst_state_list[gs] = true;
                    }
                }
            }
            //スタイル指定  ※スタイル＝標準ステート描画/グループ表示型/流入型/流出型　対象外
            // ※standardのみ表示制限(G.max_num_of_states)を掛ける
            m_style_list = new Dictionary<string, SS.STYLE>();
            int num_standard=0;
            foreach(var s in m_all_state_list)
            {
                SS.STYLE style = SS.STYLE.EXCLUDE;
                if (m_standard_state_list.ContainsKey(s))
                {
                    var bOk = true;
                    if (!string.IsNullOrEmpty(G.fillter_regextext))
                    {
                        bOk = RegexUtil.IsMatch(G.fillter_regextext,s);
                    }

                    if (bOk)
                    {
                        if (num_standard < G.max_num_of_states)
                        {
                            num_standard++;
                            style = SS.STYLE.NORMAL;
                        }
                        else
                        {
                            G.NoticeToUser_warning(G.Localize("w_exceededmaxfilter")/*"Exceeded max number of states in the fillter :"*/ + s);
                        }
                    }
                    else
                    {
                        G.NoticeToUser_warning(G.Localize("w_excludefilter") /*"Excluded by the filleter : " */ + s);
                    }
                }
                if (m_fordst_state_list.ContainsKey(s))
                {
                    style = SS.STYLE.FORDST;
                }
                if (m_forsrc_state_list.ContainsKey(s))
                {
                    style = SS.STYLE.FORSRC;
                }
                if (m_altstate_list.Contains(s))
                {
                    style = StateStyle.STYLE.FORGROUP;
                }
                m_style_list.Add(s,style);
            }

            //位置算出 グループは同じ位置に
            m_position_list = new Dictionary<string, Point?>();
            //var tmp_group_pos_list = new Dictionary<string,Point?>(); //仮のグループ位置

            foreach(var state in m_style_list.Keys)
            {
                var style = m_style_list[state];
                Point?   pos = null;
                switch(style)
                {
                case SS.STYLE.NORMAL: {
                        pos = G.get_excel_position(state);
                    }
                    break;
                case SS.STYLE.FORGROUP: {
                        if (!AltState.IsAltState(state)) throw new SystemException("Unexpected! {4A9B6D67-73CE-4A56-8733-7E15CF2A55CB}");
                        var  group = AltState.TrimAltStateName(state);
                        if (m_subdir_pos_list.ContainsKey(group))
                        {
                            pos = m_subdir_pos_list[group];
                        }
                    }
                    break;
                }
                m_position_list.Add(state,pos);
            }

            return true;
        }

        /// <summary>
        /// カレント直下のグループ
        /// </summary>
        public List<string> GetGroupsOnTargetDir()
        {
            if (m_subdir_list==null) return null;
            var list = new List<string>();
            foreach(var g in m_subdir_list.Keys)
            {
                list.Add(g);
            }
            return list;
        }
        /// <summary>
        /// 指定ステートのディレクトリパス
        /// </summary>
        public string GetDirPath(string state)
        {
            if (stateview.AltState.IsAltState(state)) throw new SystemException("Unexpected! {1DC7D5F0-38B0-4832-BA7D-35CDD3122504}");

            var nd = m_manager.FindByState(state);
            if (nd!=null)
            {
                return nd.m_pathdir;
            }
            return null;
        }

        public string GetDirPath_byGroupname(string groupname)
        {
            var path = GroupNodeUtil.pathcombine(m_target_path,groupname);
            return path;
        }

        /// <summary>
        /// ディレクトリ名（AKA グループノード名）取得
        /// </summary>
        public string GetDirName(string state)
        {
            var nd = m_manager.FindByState(state);
            if (nd!=null)
            {
                return nd.m_dirname;
            }
            return "";
        }
        /// <summary>
        /// 指定ステートがカレント直下のどのサブディレクトリに属するか？
        /// </summary>
        public string GetDirNameUnderCurrent(string state)
        {
            var nd = m_manager.FindByState(state);
            if (nd!=null)
            {
                string relpath;
                if (nd.GetRelPathdir(m_target_path,out relpath))
                {
                    if (string.IsNullOrEmpty(relpath)) return null;
                    var dir = RegexUtil.Get1stMatch(@".*?\/",relpath);
                    return dir.TrimEnd('/');
                }
            }
            return null;
        }

        public string GetDirCommentByState(string state)
        {
            var nd = m_manager.FindByState(state);
            if (nd!=null)
            {
                return nd.m_comment;
            }
            return null;
        }
        public string GetDirCommentByGroupname(string groupname)
        {
            if (string.IsNullOrEmpty(groupname)) throw new SystemException("Unexpected! {83B8BA1F-F90A-46A7-B0F1-F009107F67F2}");
            var path = GroupNodeUtil.pathcombine(m_target_path,groupname);
            var comment =  GetDirCommentByPath(path);
            if (string.IsNullOrEmpty(comment))
            {
                comment = G.nodegroup_comment_get(path);
            }
            return comment;
        }
        public void SetDirCommentByGroupname(string groupname, string comment)
        {
            if (string.IsNullOrEmpty(groupname)) throw new SystemException("Unexpected! {04F25E80-B1B9-4318-AF8B-71D7B965431F}");
            var path = GroupNodeUtil.pathcombine(m_target_path,groupname);
            var nd = m_manager.FindByPath(path);
            if (nd==null)  throw new SystemException("Unexpected! {75792E6B-022A-4E59-99B3-EC0584C314DD}");
            nd.m_comment = comment;

            G.nodegroup_comment_set(path,comment);
        }
        public string GetDirCommentByPath(string path)
        {
            var nd = m_manager.FindByPath(path);
            if (nd ==null) return null;
            return nd.m_comment;
        }
        public Point? GetDirPositionByGroupname(string groupname)
        {
            if (string.IsNullOrEmpty(groupname)) throw new SystemException("Unexpected! {E42FAA1E-DAC4-410B-9BDC-2726C0D0D7AB}");
            var path = GroupNodeUtil.pathcombine(m_target_path,groupname);
            var pos = GetDirPositionByPath(path);
            if (pos == null)
            {
                pos = G.nodegroup_pos_get(path);
            }
            return pos;
        }
        public Point? GetDirPositionByPath(string path)
        {
            var nd = m_manager.FindByPath(path);
            if (nd ==null) return null;
            return nd.m_pos;
        }
        public List<string> GetAllDisplayingStates()
        {
            if (m_style_list==null) return null;
            var list = new List<string>();
            foreach(var p in m_style_list)
            {
                var state = p.Key;
                var style = p.Value;
                if (style != StateStyle.STYLE.EXCLUDE)
                {
                    list.Add(state);
                }
            }
            return list;
        }
        public List<string> get_allstates_on_group(string groupname)
        {
            if (m_subdir_list!=null && m_subdir_list.ContainsKey(groupname))
            {
                return m_subdir_list[groupname];
            }
            return null;
        }

        //Grouping 対象のターゲットをグループに、場所を　clickstateの場所に
        public void MakeGroup(string addgroup,  List<string> target_state_list, string click_state, string comment)
        {
            if (target_state_list == null) return;
            if (!target_state_list.Contains(click_state)) return;


            var dd = G.get_draw_data(click_state);
            if (dd==null) throw new SystemException("unexpcted! {7AAF43F4-B144-4440-B504-11B8353EA971}");

            var pos = Point.Truncate( dd.wp_outframe_drect.Location);

            MakeGroup(addgroup, target_state_list, pos,comment);
        }

        public void MakeGroup(string addgroup,  List<string> target_state_list, Point pos , string comment)
        {
            if (target_state_list == null) return;

            var raw_list = new List<string>();
            foreach(var s in target_state_list)
            {
                if (AltState.IsAltState(s))
                {
                    var group = AltState.TrimAltStateName(s);
                    if (m_subdir_list.ContainsKey(group))
                    {
                        raw_list.AddRange(m_subdir_list[group]);
                    }
                }
                else
                {
                    raw_list.Add(s);
                }
            }

            //パス直下以外があれば、ユーザに対象外を外すように促す
            foreach(var st in raw_list)
            {
                var nd = m_manager.FindByState(st);
                if (nd==null) throw new SystemException("unexpected! {9EEC7E4E-E60F-447E-9E62-37B696D70293}");

                string relpath;
                if (!nd.GetRelPathdir(m_target_path,out relpath))
                {
                    G.NoticeToUser_warning(G.Localize("w_selectedsatecannotgroup")/*"Selected the STATEs cannot be made to group."*/);
                    return;
                }
            }
            //新ノードの作成
            var targetnd = m_manager.FindByPath(m_target_path);
            var newnd = targetnd.AddSubDir(addgroup);
            newnd.m_comment = comment;
            newnd.m_pos = pos;


            //移動
            foreach(var st in target_state_list)
            {
                if (AltState.IsAltState(st))
                {
                    var tmp_group = AltState.TrimAltStateName(st);

                    var frompath = GroupNodeUtil.pathcombine(m_target_path,tmp_group);
                    var topath   = newnd.m_pathdir;

                    m_manager.MoveSubDir( frompath,topath );
                }
                else
                {
                    m_manager.MoveState(st,newnd.m_pathdir);
                }
            }


            G.NoticeToUser(string.Format("Group\"{0}\" was created.",addgroup));

            //エクセル更新＆再分析＆エクセル更新　 ーーー＞このようにやらないと、コピー時に正しく反映されない。
            m_manager.WriteToExcelCache();
            Analize(m_target_path);
            m_manager.WriteToExcelCache();

            if (newnd.m_state_list==null || newnd.m_state_list.Count == 0)
            { 
                G.nodegroup_pos_set(newnd.m_pathdir,pos); //念のため
            }
        }
        //グループ解除
        public void MakeUngroup(string groupname)
        {
            if (string.IsNullOrEmpty(groupname)) return;

            var targetpath = GroupNodeUtil.pathcombine(m_target_path,groupname);

            var nd = m_manager.FindByPath(targetpath);
            if (nd == null) throw new SystemException("unexpected! {85077D62-49D9-4848-830E-5246D2FED518}");
            if (nd.m_parent==null) return; //親は対象外

            var p = nd.m_parent;

            //ステートは親に追加
            p.m_state_list.AddRange(nd.m_state_list);

            //ノードは、m_parentを親にして親に追加
            foreach(var cnd in nd.m_subdir_list)
            {
                cnd.m_parent = p;
                p.m_subdir_list.Add(cnd);
            }

            //自ノードを親から削除
            p.m_subdir_list.Remove(nd);

            G.NoticeToUser(string.Format("Group\"{0}\" was disassembled.",groupname));

            //エクセル更新＆再分析
            m_manager.WriteToExcelCache();
            Analize(m_target_path);

        }

        public void WriteToExcelCache()
        {
            m_manager.WriteToExcelCache();
        }

        //グループ名変更
        public void Rename_groupname(string from, string to)
        {
            if (!m_subdir_list.ContainsKey(from)) throw new SystemException("Unexpected ! {9C3DB417-B3C0-4C78-83BA-4781E1588282}");
            if (m_subdir_list.ContainsKey(to))    throw new SystemException("Unexpected ! {1FE1B17E-1119-4703-9493-322C9E6CE8A6}");

            if (!m_manager.RenameSubDir(m_target_path,from,to)) throw new SystemException("Unexpected ! {36A05014-A99C-4D3F-A2E9-CEB17A694788}");

        }

        //グループ移動
        public void Moveto_group(List<string> state_list, string pathdir)
        {
            if (state_list==null || state_list.Count==0) throw new SystemException("Unexpected! {D6A4FC20-5755-4B4E-8F55-B63B0B3CF379}");
            if (string.IsNullOrEmpty(pathdir) || !CheckExist_pathdir(pathdir)) throw new SystemException("Unexpected! {E02C0608-9FD5-4F2A-A832-BC2D09A92686}");

            //var nd = m_manager.FindByPath(pathdir);
            //var pos = (PointF)(Point)nd.m_pos;

            //var state_list2 = AltState.ExractStateList_forAltstate(state_list);

            //foreach(var s in state_list2)
            //{
            //    if (!m_manager.MoveState(s,pathdir))
            //    {
            //        throw new SystemException("Unexpected! {45360575-2AEE-4AE9-816B-67C35202C1FE}");
            //    }
            //    if (G.m_draw_data_list.ContainsKey(s))
            //    {
            //        var d = G.m_draw_data_list[s];
            //        d.set_offset(pos); //再描画時にグループが移動するのを防止
            //    }
            //}

            foreach(var s in state_list)
            {
                if (AltState.IsAltState(s))
                {
                    var groupname = AltState.TrimAltStateName(s);
                    if (string.IsNullOrEmpty(groupname)) throw new SystemException("Unexpected! {B4DCAD84-B03A-469B-82D0-E95BB7708BC2}");
                    var srcpath = GroupNodeUtil.pathcombine(m_target_path,groupname);
                    //var dstpath = GroupNodeUtil.pathcombine(pathdir,groupname);
                    if (!m_manager.MoveSubDir(srcpath,pathdir))
                    {
                        throw new SystemException("Unexpected! {68558E1D-AF8C-4037-8E86-003F352BC433}");
                    }
                }
                else
                {
                    if (!m_manager.MoveState(s,pathdir))
                    {
                        throw new SystemException("Unexpected! {16E7DDF2-2D81-43CA-B994-39D574B59C34}");
                    }
                }

            }

            SaveLoadIni.SaveTempIni();//ロケーション一時退避

            //エクセル更新＆再分析
            m_manager.WriteToExcelCache();
            Analize(m_target_path);
        }
        public void Moveto_group_group(string src_dirpath, string dst_pathdir)
        {
            if (!CheckExist_pathdir(src_dirpath)) throw new SystemException("Unexpected! {ABEA833A-D1DD-4F58-9746-EC4E99398849}");
            if (!CheckExist_pathdir(dst_pathdir)) throw new SystemException("Unexpected! {6073B46B-AF76-4AE0-A345-C4D6B80ACF41}");

            if (dst_pathdir.Contains(src_dirpath))
            {
                G.NoticeToUser_warning(G.Localize("w_targetcannotbemoved")/*"Target can not be moved."*/);
                return;
            }

            m_manager.MoveSubDir(src_dirpath,dst_pathdir);

            ////src_dirpath傘下のステート
            //var src_state_list = G.node_get_allstates_by_dirpath(src_dirpath);
            //var lastpath = GroupNodeUtil.get_last_path(src_dirpath);
            //lastpath = lastpath.Trim('/');
            //var newdstpath = GroupNodeUtil.path_normalize(dst_pathdir) + lastpath + "/";

            //foreach(var i in src_state_list)
            //{
            //    if (m_manager.MoveSubDir(src_dirpath,dst_pathdir))
            //    {
            //    }
            //}
        }
        //グループコピー
        public void Copy_group(string src, string dst)
        {
            if (!CheckExist_pathdir(GetDirPath_byGroupname(src))) throw new SystemException("Unexpected! {E48BF261-280E-4CF0-9FD9-E727C56ACBBF}");
            if ( CheckExist_pathdir(GetDirPath_byGroupname(dst))) throw new SystemException("Unexpected! {2F170EF5-FF7F-4498-B41F-712615422191}");

            var list_src = get_allstates_on_group(src);
            var list_dst = new List<string>();
            foreach(var state in list_src)
            {
                var newname = G.excel_program.Copy(state);
                list_dst.Add(newname);
            }

            //この時点では、分岐がソース側を見ている。
            Func<string,string> getnewname = (s) => {
                if (!string.IsNullOrEmpty(s))
                {
                    var index = list_src.IndexOf(s);
                    if (index >= 0)
                    {
                        var s2 = list_dst[index];
                        return s2;
                    }
                }
                return s;
            };

            foreach(var state in list_dst)
            {

                //var ns = G.excel_program.GetString(state,G.STATENAME_nextstate);
                //var ns2 = getnewname(ns);
                //G.excel_program.SetString(state,G.STATENAME_nextstate,ns2);

                foreach(var name in new string[] { G.STATENAME_nextstate, G.STATENAME_basestate, G.STATENAME_gosubstate })
                {
                    var ns = G.excel_program.GetString(state, name);
                    if (string.IsNullOrEmpty(ns)) continue;
                    var ns2 = getnewname(ns);
                    G.excel_program.SetString(state, name, ns2);
                }

                var list = BranchParse.Parse(G.excel_program.GetString(state,G.STATENAME_branch));
                if (list == null || list.Length==0) continue;

                var str = string.Empty;
                foreach(var i in list)
                {
                    var s2 = getnewname(i.nextstate);
                    if (!string.IsNullOrEmpty(str))
                    {
                        str += "\x0a";
                    }
                    str += BranchParse.Replace1stParameter(i.value,s2);
                }

                G.excel_program.SetString(state,G.STATENAME_branch,str);

                

            }
            // 所属グループ名を変更
            foreach(var state in list_dst)
            {
                var dir = DirPathExcelUtil.get_dirpath(state);//  G.excel_program.GetString(state,"!dir");
                var diff = dir.Substring(m_target_path.Length); //先頭がグループになる。
                var newpost = RegexUtil.Replace1stMatch(diff,@"^[a-zA-Z_0-9]+\/",dst + "/");
                var newdir = m_target_path + newpost;
                DirPathExcelUtil.set_dirpath(state,newdir);
            }
        }

        //指定パスの存在確認
        public bool CheckExist_pathdir(string pathdir)
        {
            foreach(var s in G.state_working_list)
            {
                if (AltState.IsAltState(s)) continue;
                var path = GetDirPath(s);
                if (path == pathdir) return true;
                if (path.StartsWith(pathdir)) return true;
            }
            return false;
        }

        //コピー先用グループ名作成
        public string Make_groupname_for_copy(string src)
        {
            var dst = src;
            for(var loop = 0; loop <= 10000; loop++)
            {
                dst = StateUtil.MakeIncName(dst);
                if (!CheckExist_pathdir(GetDirPath_byGroupname(dst)))
                {
                    return dst;
                }
            }
            throw new SystemException("Unexpected! {6F772AE7-B5D1-4522-BB73-2549EDE4FDA1}");
        }

        public void Delete_group(string src)
        {
            if (!CheckExist_pathdir(GetDirPath_byGroupname(src))) throw new SystemException("Unexpected! {E48BF261-280E-4CF0-9FD9-E727C56ACBBF}");
            var list_src = get_allstates_on_group(src);

            foreach(var state in list_src)
            {
                G.excel_program.Delete(state);
            }
        }

        //全パスの取得
        public List<string> GetAll_pathdir()
        {
            var ht = new Hashtable();
            foreach(var s in G.state_working_list)
            {
                if (AltState.IsAltState(s)) continue;
                var path = GetDirPath(s);
                if (string.IsNullOrEmpty(path)) throw new SystemException("Unexpected! {AD8CAA67-16B2-4F42-ADAB-E9FEB2E83925}");
                ht[path] = true;
            }
            var list = new List<string>();
            foreach(var k in ht.Keys) list.Add(k.ToString());
            return list;
        }
        //
        public Point? GetPosition(string state)
        {
            if (m_position_list.ContainsKey(state))
            {
                var pos =  m_position_list[state];
                return  pos;
            }
            return null;
        }
        //
        public void SaveNodePosition() //nodedirのポジションを更新
        {
            foreach(var group in GetGroupsOnTargetDir())
            {
                var path = GroupNodeUtil.pathcombine(m_target_path,group);
                if (!GroupNodeUtil.check_path(path)) throw new SystemException("unexpected! {960B2A49-A6D6-40F8-9F8E-06369FBC0570}");

                var altstate = AltState.MakeAltStateName(group);
                var dd = G.get_draw_data(altstate);
                if (dd==null) continue;

                var nd = m_manager.FindByPath(path);
                if (nd==null) continue;

                nd.m_pos =Point.Truncate(dd.offset);
            }
            m_manager.WriteToExcelCache();
        }

        //Tree向け
        public GroupNodeManager.NodeData GetNodeRoot()
        {
            return m_manager.GetRootData();
        }

        //ツリー表示用データ ※Analizeの結果から作成
        public void GetCurrentStateDrawTreeData(out string highlight_node,out List<string> highlight_states, out List<string> highlight_groups, out List<string> srcdst_states)
        {
            highlight_node = m_target_path;

            highlight_states = new List<string>();
            foreach(var k in m_standard_state_list.Keys) highlight_states.Add((string)k);

            highlight_groups = new List<string>();
            foreach(var k in m_subdir_list.Keys) highlight_groups.Add( GroupNodeUtil.pathcombine(m_target_path,(string)k));

            srcdst_states = new List<string>();
            foreach(var k in m_forsrc_state_list.Keys) srcdst_states.Add((string)k);
            foreach(var k in m_fordst_state_list.Keys) srcdst_states.Add((string)k);
        }

        // 行先ノードがグループ内の場合、そのaltstateを返す。
        public string NormalizeStateForAlt(string state)
        {
            foreach(var group in m_subdir_list.Keys)
            {
                var list = m_subdir_list[group];
                if (list==null || list.Count == 0) continue;
                if (list.Contains(state))
                {
                    return AltState.MakeAltStateName(group);
                }
            }
            return state;
        }

        // ---  tool for this class   ---

        private bool IsStandardOrGroupStates(string state)
        {
            if (m_standard_state_list!=null &&  m_standard_state_list.ContainsKey(state)) return true;
            if (m_subdir_list!=null)
            {
                foreach(var p in m_subdir_list)
                {
                    var list = p.Value;
                    if (list!=null && list.Contains(state))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
