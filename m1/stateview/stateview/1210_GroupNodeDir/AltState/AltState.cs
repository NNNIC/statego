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

/*
    グループ内全部のステートを代表するステートを AltStateとする。(Alternative State)



 */


namespace stateview
{
    public class AltState
    {
        static readonly string  _MARK_ = "____altnative_state____";
        public static string GetAltMark() { return  _MARK_; }
        public static string MakeAltStateName(string groupname)
        {
            return _MARK_ + groupname;
        }
        public static string TrimAltStateName(string altstatename)
        {
            if (altstatename.IndexOf(_MARK_)==0)
            {
                return altstatename.Substring(_MARK_.Length);
            }
            G.NoticeToUser_warning("Unexpected! {C0AF9DA9-9E2A-4446-8A95-49C920984B2A}");
            return altstatename;
        }
        public static bool IsAltState(string altstatename)
        {
            return (!string.IsNullOrEmpty(altstatename) &&  altstatename.IndexOf(_MARK_)==0);
        }
        public static void DeleteAllAltStates()
        {
            var saveDirty = G.Dirty_save(); // G.bDirty;

            for(var loop = 0; loop<=10000; loop++)
            {
                if (loop == 10000) throw new SystemException("Unexpected! {257FA223-0690-440B-9EC1-0DB5089AFE8B}");
                bool bNeedLoop = false;
                for(var i = 0; i<G.state_working_list.Count; i++)
                {
                    var s = G.state_working_list[i];
                    if (IsAltState(s))
                    {
                        var col = G.state_working_col_list[i];

                        G.excel_program.Delete(col);

                        G.state_working_list_reflesh();

                        bNeedLoop = true;
                        break;
                    }
                }
                if (!bNeedLoop) break;
            }

            //G.bDirty = saveDirty;
            G.Dirty_restore(saveDirty);
        }

        public static List<string> CreateAltState(
            Dictionary<string,List<string>> subdir_list,
            Dictionary<string,string>       subdir_cmt_list,
            Dictionary<string, Point?>      subdir_pos_list,
            string targetpath)
        {
            var glist = new List<string>();
            var saveDirty = G.Dirty_save();
            {
                foreach(var group in subdir_list.Keys)
                {
                    var list = subdir_list[group];

                    var brlist = new List<string>(); //分岐先
                    foreach(var s in list)
                    {
                        var ns = G.excel_program.GetString(s,G.STATENAME_nextstate);
                        if (!string.IsNullOrEmpty(ns) &&  !list.Contains(ns))
                        {
                            brlist.Add(ns);
                        }

                        var labes = BranchUtil.GetLabelListFromState(s);
                        if (labes!=null)
                        {
                            foreach(var l in labes)
                            {
                                if (!string.IsNullOrEmpty(l) && !list.Contains(l))
                                {
                                    brlist.Add(l);
                                }
                            }
                        }
                        var gs = G.excel_program.GetString(s,G.STATENAME_gosubstate);
                        if (!string.IsNullOrEmpty(gs) && !list.Contains(gs))
                        {
                            brlist.Add(gs);
                        }
                    }

                    var statename = MakeAltStateName(group);
                    var brstr = string.Empty;
                    foreach(var l in brlist)
                    {
                        if (!string.IsNullOrEmpty(brstr)) {
                            brstr += "\x0a";
                        }
                        brstr += string.Format("br_dummy_({0})",l);
                    }

                    G.excel_program.NewState_forceName(statename,targetpath, true);

                    var cmt = string.Empty;
                    if (subdir_cmt_list!=null && subdir_cmt_list.ContainsKey(group))
                    {
                        cmt = subdir_cmt_list[group];
                    }
                    G.excel_program.SetString(statename,G.STATENAME_statecmt,cmt);
                    G.excel_program.SetString(statename,G.STATENAME_branch,brstr);

                    Point? pos = null;
                    if (subdir_pos_list!=null && subdir_pos_list.ContainsKey(group))
                    {
                        pos = subdir_pos_list[group];
                    }
                    if (pos == null) pos = new Point(100,100);

                    var path = GroupNodeUtil.pathcombine(targetpath,group);
                    path = GroupNodeUtil.path_normalize(path);
                    DirPathExcelUtil.set_diritems(statename,path,(Point)pos,cmt);

                    glist.Add(statename);
                }
            }
            /*G.bDirty = saveDirty;*/ G.Dirty_restore(saveDirty);
            G.update_viewform_title();
            return glist;
        }

        public static List<string> ExractStateList_forAltstate(List<string> state_list)
        {
            var outlist = new List<string>();
            foreach(var i in state_list)
            {
                if (IsAltState(i))
                {
                    var list = G.node_get_allstates_on_groupnode(TrimAltStateName(i));
                    outlist.AddRange(list);
                }
                else
                {
                    outlist.Add(i);
                }
            }
            return outlist;
        }

        //代替ステートが行先となったとき、適切なものを選択する。
        public static string FindAppropriateDestination(string state)
        {
            if (!IsAltState(state)) return state;
            var group = TrimAltStateName(state);
            var list = G.node_get_allstates_on_groupnode(group);
            if (list == null || list.Count==0) return null;

            list.Sort((a,b)=> {
                var path_a = G.node_get_dirpath(a);
                var path_b = G.node_get_dirpath(b);
                return path_a.CompareTo(path_b);
            });

            return list[0];
        }
    }
}
