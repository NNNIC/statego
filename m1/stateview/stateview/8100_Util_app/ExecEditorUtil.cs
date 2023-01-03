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

using System.Reflection;

namespace stateview
{
    public class ExecEditorUtil
    {
        public static void Exec_obs(string file)
        {
            if (!File.Exists(file))
            {
                G.NoticeToUser_warning("Failt to open editor for a file.");
                return;
            }

            if (!string.IsNullOrEmpty(G.external_source_editor))
            {
                ExecUtil.execute_start2(string.Format("\"{0}\" \"{1}\"",G.external_source_editor, file),G.load_file_dir);
            }
            else
            {
                ExecUtil.execute_start(file,G.load_file_dir);
            }
        }
		public static void Exec(string fullpath, int? lineparam = null)
		{
			var file = Path.GetFileName(fullpath);
			var dir  = Path.GetDirectoryName(fullpath);
			var cmdline = string.Empty;
			if (string.IsNullOrEmpty(G.external_source_editor))
			{
				cmdline = FindWindowsAppUtil.FindAssociatedCommand(fullpath);
				if (cmdline.Contains("%1"))
				{
					cmdline = cmdline.Replace("%1", file);
				}
				else
				{
					cmdline = cmdline + " " + file;
				}
			}
			else
			{
                //if (G.external_source_editor.ToLower().Contains(@"\code.exe")) //VS CODE時は、特別な処理
                //{
                //    file = RegexUtil.IsMatch(@"^.:",file) ? file.Substring(2) : file;
                //}

				if (G.external_source_editor.Contains("%1"))
				{
					cmdline = G.external_source_editor.Replace("%1", file);
				}
				else
				{
					cmdline = string.Format(" \"{0}\" {1} ", G.external_source_editor, file);
				}
			}

            if (cmdline.Contains("%2"))
            {
                var line = lineparam==null ? 1: (int)lineparam;
                cmdline = cmdline.Replace("%2", line.ToString());
            }

            cmdline = PathUtil.ExtractPathWithEnvVals(cmdline);

			ExecUtil.execute_start2(cmdline,dir);

            //if (lineparam!=null && G.source_editor_vs2015_support && cmdline.Contains("devenv.exe"))
            if (G.source_editor_vsXXXX_support)
            {
                exec_vs2015jump(dir,fullpath,(int)lineparam);
            }

        }
        private static void exec_vs2015jump(string dir, string src, int line)
        {
            //devenv.exeが稼働中か？
            var b = false;
            foreach(var pi in System.Diagnostics.Process.GetProcesses())
            {
                if (pi.ProcessName.Contains("devenv"))
                {
                    b = true;
                    break;
                }
            }
            if (!b) return;

            var appdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var cmd = "\"" + Path.Combine(appdir,"tools","VisualStudioOpenFile.exe") + "\"  \"" + src +"\" " + line.ToString();
            ExecUtil.execute_start2(cmd,dir);
        }

        //private static string resolve_path(string cmd) // パス無のexeは、toolsフォルダから起動する
        //{
        //    if (string.IsNullOrEmpty(cmd)) return cmd;
        //    if (cmd[0]=='\"') return cmd; // 先頭文字がダブルクォート時は対象外
        //    var cmdtok = cmd.Split(' ');
        //    var exefile = cmdtok[0];
        //    if (exefile.Contains(@"\")) return cmd; // パスがあると対象外
        //    var path = Path.Combine(G.appDir,"tools",exefile);
        //    if (Path.GetExtension(path)=="") path += ".exe";
        //    if (!File.Exists(path)) return cmd;

        //    return  cmd.Replace(exefile,  "\"" + path + "\"");
        //}
    }
}
