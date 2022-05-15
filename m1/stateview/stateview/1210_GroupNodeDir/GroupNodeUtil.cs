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

namespace stateview
{
    public class GroupNodeUtil
    {
        public static string path_normalize(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return "/";
            }
            var path2 = path.Trim();
            if (string.IsNullOrEmpty(path2))
            {
                return "/";
            }
            if (path2 == "/") return path2;
            if (!path2.StartsWith("/"))
            {
                path2 = "/" + path2;
            }
            if (!path2.EndsWith("/"))
            {
                path2 += "/";
            }
            return path2;
        }
        public static string pathcombine(string path1, string path2)
        {
            var a = path_normalize(path1);
            var b = path_normalize(path2);

            if (a=="/") return b;
            var c = a.Substring(0,a.Length-1) + b;

            if (!check_path(c)) throw new SystemException("!unexpected {FAD7212C-9A67-487C-A5C2-9366621312EF}");

            return c;
        }
        public static bool check_path(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            if (!path.StartsWith("/")) return false;
            if (!path.EndsWith("/")) return false;
            if (path.Contains("//")) return false;

            foreach(var c in path)
            {
                if ((int)c <= (int)' ') return false;
            }

            return true;
        }
        /// <summary>
        /// 一つ手前の親パスを得る
        ///
        /// 例：入力  /a/b/c/    出力 /a/b/
        ///
        /// </summary>
        public static string get_parent_path(string path)
        {
            var path1 = path_normalize(path);
            if (path1=="/") return path1;

            var lastmatch = get_last_path(path1);
            var l = path1.Length - (lastmatch.Length - 1);
            var outpath = l > 0 ? path1.Substring(0,l) : "/";

            return outpath;
        }

        /// <summary>
        /// 最後尾のパスを得る
        ///
        /// 例：入力 /a/b/c/  出力  /c/
        /// </summary>
        public static string get_last_path(string path)
        {
            var path1 = path_normalize(path);
            if (path1=="/") return path1;

            var lastmatch = RegexUtil.Get1stMatch(@"\/[^\/]+?\/$",path1);
            if (string.IsNullOrEmpty(lastmatch)) {
                throw new SystemException("Unexpected! {DBB14430-B28F-46AD-9A86-246761B75981}");
            }
            return lastmatch;
        }


        /// <summary>
        /// ターゲットパスが直下のグループノードか？
        /// ex1) target_dirpath = "/hoge/hehe/";  
        ///     path = "/hoge/";
        ///     ※　pathの直下でかつ一つだけ
        ///     結果 true
        ///     
        /// ex3) target_dirpath = "/hoge/hehe/gg/"; 
        ///     path = "/hoge/";
        ///     結果 false
        ///
        /// ex4) target_dirpath = "/vv/"; 
        ///     path = "/hoge/";
        ///     結果 false
        ///      
        /// </summary>
        public static bool is_groupnode_just_under_path(string itarget_dirpath, string ipath)
        {
            var target_path = path_normalize(itarget_dirpath);
            var path = path_normalize(ipath);

            if (!target_path.StartsWith(path)) return false; //冒頭が一致しない
            var rest = target_path.Substring(path.Length);
            // 
            //   rest = "xxxx/"
            //
            if (RegexUtil.Get1stMatch(@"[^\/]+?\/$",rest)==rest) // 正規表現と一致
            {
                return true;
            }
            return false;
        }

        public static string get_groupnode_just_under_path(string itarget_dirpath, string ipath)
        {
            var target_path = path_normalize(itarget_dirpath);
            var path = path_normalize(ipath);

            if (!target_path.StartsWith(path)) return null; //冒頭が一致しない
            var rest = target_path.Substring(path.Length);
            // 
            //   rest = "xxxx/xxx/"
            //
            var upath = RegexUtil.Get1stMatch(@"[^\/]+?\/",rest);
            //
            //  upath = "xxxx/"
            //
            return upath.Trim('/');
        }


        // G.nodegroup_comment_listを更新　 LOAD後に想定
        public static void NormalizeGroupNodeCommentPosList()
        {
            //材料１ G.nodegroup_comment_list
            //材料２ G.excel_program.GetStateList(); エクセルシート(本体)からのstateリスト
            //材料３ 上記のエクセルシート上の!dirからコメントを得る


            var new_comment_list = new Dictionary<string,string>();
            var new_pos_list     = new Dictionary<string,Point>();
            foreach(var state in G.excel_program.GetStateList()) // 全ステート確認
            {
                string path,comment; 
                Point? pos;
                if (DirPathExcelUtil.get_diritems(state,out path, out pos, out comment))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        if (!new_comment_list.ContainsKey(path))
                        {
                            new_comment_list.Add(path,comment);
                            var posx = pos == null ? new Point(int.MinValue,int.MinValue) : (Point)pos;
                            new_pos_list.Add(path,posx);
                        }
                    }
                }
            }

            //パスを精査する。
            var LOOPMAX=1000;
            for(var loop = 0; loop<=LOOPMAX; loop++)
            {
                var bNeedLoop = false;
                foreach(var path in new_comment_list.Keys)
                {
                    var tmpath = path;
                    for(var loop2=0; loop2<LOOPMAX; loop2++)
                    {
                        var parent = get_parent_path(tmpath);
                        if (!new_comment_list.ContainsKey(parent)) //親が new_comment_listに登録されていな時
                        {
                            //nodegroup_comment_listを走査
                            var parentcomment = G.nodegroup_comment_get(parent);
                            var parentpos     = G.nodegroup_pos_get(parent);
                            new_comment_list.Add(parent,parentcomment);
                            new_pos_list.Add(parent,parentpos!=null ? (Point)parentpos : new Point(int.MinValue, int.MinValue) );
                            bNeedLoop = true;
                            break;
                        }
                        if (parent == "/") break;
                    }
                    if (bNeedLoop) break;
                }
                if (!bNeedLoop) break;
            }
            G.nodegroup_comment_list_set( new_comment_list);
            G.nodegroup_pos_list_set( new_pos_list );
        }
    }
}
