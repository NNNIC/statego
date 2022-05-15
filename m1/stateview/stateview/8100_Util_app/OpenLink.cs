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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>


namespace stateview
{
    public class OpenLink
    {
#if obs
        public static void Open(string path)
        {
            path = StringUtil.Get1stLineTrim(path);
            if (string.IsNullOrEmpty(path))
            {
                G.NoticeToUser_warning("Cannot open because path is null.");
                return;
            }

            if (path.Contains(":") || path.Contains("\\") || path.Contains("/"))
            {
                ExecUtil.execute_start(path, "");
                return;
            }


            if (Path.GetExtension(path).ToLower() == ".psgg")
            {
                if (CheckOpenSameDoc.ActivateWindow(Path.GetFileNameWithoutExtension(path)))
                {
                    return;
                }
            }

            if (!path.Contains("."))
            {
                path += ".psgg";
            }

            var timeout = false;
            var newpath = PathUtil.FindTraverseDownAndUp(G.load_file_dir, path,8000,out timeout);

            if (newpath == null && timeout)
            {
                G.NoticeToUser_warning("Cannot find file because timeout");
            }


            if (!string.IsNullOrEmpty(newpath))
            {
                ExecUtil.execute_start(newpath, "");
            }
            else
            {
                G.NoticeToUser_warning("Cannot open " + path);
            }
        }
#endif
        public static void Open(string path)
        {
            path = StringUtil.Get1stLineTrim(path);
            if (string.IsNullOrEmpty(path))
            {
                G.NoticeToUser_warning("Cannot open because path is null.");
                return;
            }

            if (path.Contains(":") || path.Contains("\\") || path.Contains("/"))
            {
                ExecUtil.execute_start(path, "");
                return;
            }

            if (!path.Contains("."))
            {
                path += ".psgg";
            }

            if (Path.GetExtension(path).ToLower() == ".psgg") //1回目
            {
                if (CheckOpenSameDoc.ActivateWindowByHash(path))
                {
                    return;
                }
            }

            var timeout = false;
            var newpath = PathUtil.FindTraverseDownAndUp(G.load_file_dir, path,1000,out timeout);

            if (newpath == null && timeout)
            {
                G.NoticeToUser_warning("Cannot find file because timeout : " + path);
                return;
            }
            else
            {
                if (Path.GetExtension(newpath).ToLower() == ".psgg") //２回目
                {
                    if (CheckOpenSameDoc.ActivateWindowByHash(newpath))
                    {
                        return;
                    }
                }
            }

            if (!string.IsNullOrEmpty(newpath))
            {
                ExecUtil.execute_start(newpath, "");
            }
            else
            {
                G.NoticeToUser_warning("Cannot open " + path);
            }
        }

    }
}
