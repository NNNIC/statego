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

    //
    // excelに書きこむ!dir に関する　ユーティリティ
    public class DirPathExcelUtil
    {
        public static string get_dirstr(string state)
        {
            return G.excel_program.GetString(state,G.STATENAMESYS_dir);
        }
        public static string get_dirpath(string state)
        {
            if (string.IsNullOrEmpty(state)) return "/";

            string path;
            Point? pos;
            string comment;

            if (get_diritems(state, out path, out pos, out comment))
            {
                return path;
            }
            //上記以外はルート
            return "/";
        }
        public static string get_dirpath_dirdata(string dirdata)
        {
            if (string.IsNullOrEmpty(dirdata))
            {
                G.NoticeToUser_warning("{A7323C84-8CEB-4803-A6D0-C8284CEB49D4} dir data is empty.");
                return "/";
            }

            string path;
            Point? pos;
            string comment;
            if (get_diritems_from_dirdata(dirdata, out path, out pos, out comment))
            {
                return path;
            }

            G.NoticeToUser_warning("{F38A746D-6CB5-4343-8A6D-F7D2FFB31F1F}");
            return "/";
        }
        public static string get_dircomment(string state)
        {
            string path;
            Point? pos;
            string comment;

            if (get_diritems(state, out path, out pos, out comment))
            {
                return comment;
            }
            return null;
        }
        public static Point? get_dirpos(string state)
        {
            string path;
            Point? pos;
            string comment;

            if (get_diritems(state, out path, out pos, out comment))
            {
                return pos;
            }
            return null;
        }
        public static Point? get_dirpos_dirdata(string dirdata)
        {
            if (string.IsNullOrEmpty(dirdata))
            {
                G.NoticeToUser_warning("{BA1DD4D2-1A87-415A-89E3-68EAD284190C}");
                return null;
            }
            string path;
            Point? pos;
            string comment;
            if (get_diritems_from_dirdata(dirdata, out path, out pos, out comment))
            {
                return pos;
            }
            return null;
        }
        public static bool get_diritems(string state,out string path, out Point? pos, out string comment)
        {
            path = null;
            pos  = null;
            comment = null;
            var s = get_dirstr(state);

            return get_diritems_from_dirdata(s,out path, out pos, out comment);

            //if (string.IsNullOrEmpty(s)) return false;
            //var l = s.Split('\x0a');
            //if (l.Length>=2)
            //{
            //    path = GroupNodeUtil.path_normalize(l[0]);
            //    pos  = PointUtil.Parse(l[1]);
            //    if (l.Length >= 3)
            //    {
            //        comment = l[2].Trim();
            //        comment = oneline2multi(comment);
            //    }
            //    return true;
            //}
            //return false;
        }
        public static bool get_diritems_from_dirdata(string dirdata, out string path, out Point? pos, out string comment)
        {
            path = null;
            pos  = null;
            comment = null;

            var s = dirdata;
            if (string.IsNullOrEmpty(s)) return false;
            var l = s.Split('\x0a');
            if (l.Length>=2)
            {
                path = GroupNodeUtil.path_normalize(l[0]);
                pos  = PointUtil.Parse(l[1]);
                if (l.Length >= 3)
                {
                    comment = l[2].Trim();
                    comment = oneline2multi(comment);
                }
                return true;
            }
            return false;
        }
        public static void set_diritems(string state, string path, Point pos, string comment)
        {
            //comment = multi2oneline(comment);
            //var val = path + "\x0a" + string.Format("({0},{1})",pos.X,pos.Y) + "\x0a" + comment;
            var val = makedata_diritems(path,pos,comment);
            G.excel_program.SetString(state,G.STATENAMESYS_dir,val);
        }
        public static string makedata_diritems(string path, Point pos, string comment)
        {
            comment = multi2oneline(comment);
            var val = path + "\x0a" + string.Format("({0},{1})",pos.X,pos.Y) + "\x0a" + comment;
            return val;
        }
        public static void set_dirpath(string state, string path)
        {
            var pos = get_dirpos(state);
            if (pos == null) pos = new Point(100,100);
            set_diritems(state, path, (Point)pos, get_dircomment(state));
        }
        private static string multi2oneline(string s)
        {
            var newlinechar = StringUtil.FindNewLineChar(s);
            if (string.IsNullOrEmpty(newlinechar))
            {
                return s;
            }
            return s.Replace(newlinechar,@"\n");
        }
        private static string oneline2multi(string s)
        {
            return s.Replace(@"\n",Environment.NewLine);
        }


        public static string makedata_dirpath(string orgdata, string path)
        {
            string opath;
            Point? opos;
            string ocomment;
            if (!get_diritems_from_dirdata(orgdata,out opath, out opos, out ocomment))
            {
                G.NoticeToUser_warning("{BF75C96C-7B3B-4173-B909-9707DCF4F75E}");
                return null;
            }
            var opos2 = (Point)opos;
            var newdata = makedata_diritems(path,opos2,ocomment);
            return newdata;
        }
        public static string makedata_dirpos(string orgdata, Point pos)
        {
            string opath;
            Point? opos;
            string ocomment;
            if (!get_diritems_from_dirdata(orgdata,out opath, out opos, out ocomment))
            {
                G.NoticeToUser_warning("{BF75C96C-7B3B-4173-B909-9707DCF4F75E}");
                return null;
            }
            var newdata = makedata_diritems(opath,pos,ocomment);
            return newdata;
        }

    }
}
