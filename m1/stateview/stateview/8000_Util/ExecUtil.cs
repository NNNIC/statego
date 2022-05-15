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

using System.Diagnostics;

public class ExecUtil
{
    public static void execute(string s,string dir)
    {
        try {
            var p = new Process();
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.StartInfo.WorkingDirectory =dir;
            p.StartInfo.Arguments = @"/c " + s;
            p.Start();
        } catch (System.Exception e)
        {
            G.log+="Execute command error : " + e.Message +"\n";
            G.NoticeToUser_warning("Failed to execute command :" + s);
        }
    }
    public static void execute_and_wait(string s,string dir)
    {
        try {
            var p = new Process();
            p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WorkingDirectory =dir;
            p.StartInfo.Arguments = @"/c " + s;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
        } catch (System.Exception e)
        {
            G.log+="Execute command error : " + e.Message +"\n";
            G.NoticeToUser_warning("Failed to execute command :" + s);
        }
    }

    public static void execute_wo_args(string s,string dir)
    {
        try {
            var p = new Process();
            p.StartInfo.FileName = s;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory =dir;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        } catch (System.Exception e)
        {
            G.log+="Execute command error : " + e.Message +"\n";
            G.NoticeToUser_warning("Failed to execute command :" + s);
        }
    }
    public static void execute_w_args(string s,string args, string dir)
    {
        try {
            var p = new Process();
            p.StartInfo.FileName = s;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory =dir;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
        } catch (System.Exception e)
        {
            G.log+="Execute command error : " + e.Message +"\n";
            G.NoticeToUser_warning("Failed to execute command :" + s);
        }
    }
    public static void execute_w_args_and_wait(string s,string args, string dir)
    {
        try {
            var p = new Process();
            p.StartInfo.FileName = s;
            p.StartInfo.Arguments = args;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WorkingDirectory =dir;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
        } catch (System.Exception e)
        {
            G.log+="Execute command error : " + e.Message +"\n";
            G.NoticeToUser_warning("Failed to execute command :" + s);
        }
    }

    public static void execute_start(string s, string dir)
    {
        execute("start \"\" \"" + s +"\"", dir);
    }

    public static void execute_start2(string s, string dir)
    {
        execute("start \"\" " + s, dir);
    }

    public static void execute_as_batch(string s, string dir)
    {
        var path = Path.Combine( Path.GetTempPath(), "psgg_execute");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        var file = Path.GetFileName( Path.GetTempFileName() ) + ".bat";
        var fullpathfile = Path.Combine(path,file);
        var batchtext = "chcp 65001" + Environment.NewLine +
                        "cd /d \"" + dir +"\"" + Environment.NewLine +
                        "cmd /c " + s  + Environment.NewLine
                        //+ "pause" + Environment.NewLine
                        +"exit"
                        ;
        var enc = new System.Text.UTF8Encoding(false);
        File.WriteAllText(fullpathfile,batchtext,enc );

        G.NoticeToUser("batch : " + fullpathfile);


#if !obs
        using (var process = new System.Diagnostics.Process())
        {
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.FileName  = System.Environment.GetEnvironmentVariable("ComSpec");

            process.StartInfo.Arguments = "/C \"" + fullpathfile + "\"";
            process.StartInfo.ErrorDialog = true;
            
            if (!process.Start())
            {
                G.NoticeToUser("Filed to rub the batch");
            }
            process.WaitForExit(500);
            
        }
#else
        using (var process = new System.Diagnostics.Process())
        {
            process.StartInfo.UseShellExecute = true;
            //process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.RedirectStandardInput = false;
            process.StartInfo.WorkingDirectory = path;
            process.StartInfo.FileName  = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32\schtasks";
            process.StartInfo.Arguments = "/create /tn \"openeditor\" /tr +\"" + fullpathfile + "\" /sc once";
            process.StartInfo.ErrorDialog = true;
            
            if (!process.Start())
            {
                G.NoticeToUser("Filed to rub the batch");
            }
            process.WaitForExit(500);
        }

#endif
    }

}
