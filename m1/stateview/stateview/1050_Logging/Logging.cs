using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using G=stateview.Globals;

public static class Logging
{
    static string m_dir
    {
        get
        {
            return PathUtil.ExtractPathWithEnvVals(@"%USERPROFILE%\AppData\LocalLow\psgg");
        }
    }
    static readonly string m_prefix = "psgglog_";

    static string m_prefix_w_filename
    {
        get
        {
            return m_prefix + Path.GetFileNameWithoutExtension(G.load_file) + "@";
        }
    }


    static string m_file;

    static void decide_filenames()
    {
        try
        {
            if (!Directory.Exists(m_dir))
            {
                Directory.CreateDirectory(m_dir);
            }
            var files = (new DirectoryInfo(m_dir)).GetFiles("*.txt");

            //delete yesterday files
            var delfiles = new List<string>();
            foreach (var fi in files)
            {
                if (fi.Name.StartsWith(m_prefix))
                {
                    if (DateTime.Now - fi.CreationTime > TimeSpan.FromDays(1))
                    {
                        delfiles.Add(fi.FullName);
                    }
                }
            }

        }
        catch
        { }
        m_file = Path.Combine(m_dir, m_prefix_w_filename + DateTime.Now.ToBinary().ToString() + ".txt" );
    }


    static StreamWriter m_sw;
    static bool m_initilized = false;
    static void init()
    {
        if (m_initilized)
        {
            return;
        }
        m_initilized = true;

        decide_filenames();

        try
        {
            if (File.Exists(m_file))
            {
                File.Delete(m_file);
            }

            var NL = Environment.NewLine;
            var msg = "# STATEGO LOG : " + DateTime.Now.ToString() + NL;
            msg += "# VERSION:" + G.version + NL;

            var dir = Path.GetDirectoryName(m_file);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(m_file, msg, Encoding.UTF8);

            m_sw = File.AppendText(m_file);
        }
        catch { }
    }

    public static void Write(string s)
    {
        init();
        if (m_sw == null) return;
        try
        {
            m_sw.Write(s);
            m_sw.Flush();
        }
        catch
        { }
    }
    public static void WriteLine(string s)
    {
        init();
        if (m_sw == null) return;
        try
        {
            var now = DateTime.Now;
            var time = now.Hour.ToString("00") + ":" + now.Minute.ToString("00") + ":" + now.Minute.ToString("00") + "." + now.Millisecond.ToString("000");
            s = time + " " + s;
            m_sw.WriteLine(s);
            m_sw.Flush();
        }
        catch
        { }
    }
}
