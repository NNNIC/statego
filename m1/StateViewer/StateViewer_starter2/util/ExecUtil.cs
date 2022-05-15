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
        } catch //(System.Exception e)
        {
            //G.log+="Execute command error : " + e.Message +"\n";
            //G.NoticeToUser_warning("Failed to execute command :" + s);
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
        } catch //(System.Exception e)
        {
            //G.log+="Execute command error : " + e.Message +"\n";
            //G.NoticeToUser_warning("Failed to execute command :" + s);
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
        } catch //(System.Exception e)
        {
            //G.log+="Execute command error : " + e.Message +"\n";
            //G.NoticeToUser_warning("Failed to execute command :" + s);
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

}
