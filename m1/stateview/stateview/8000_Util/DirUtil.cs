using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

public class DirUtil
{
	public static string Normalize(string dir)
	{
		try { 
			return (new DirectoryInfo(dir)).FullName;
		} catch
		{
			return null;
		}
	}

    public static string GetApplicationDir()
    {
        var apppath = Assembly.GetExecutingAssembly().Location;
        return Path.GetDirectoryName(apppath);
    }

    public static string GetUserProfDir()
    {
        return Environment.GetEnvironmentVariable("USERPROFILE");
    }

    public static bool IsInProgramFiles(string dir)
    {
        var fullpath = Path.GetFullPath(dir);
        var programfiles = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
        if (string.IsNullOrEmpty(programfiles)) return false;

        return (fullpath.StartsWith(programfiles));
    }

    /// <summary>
    /// フォルダの変化を知るためのハッシュ作成
    /// </summary>
    public static int MakeChangeHash(string dir)
    {
        var s = string.Empty;
        Action<DirectoryInfo> trv = null;
        trv = (d)=> {
            if (d==null) return;

            s += d.FullName;
            s += d.LastWriteTimeUtc.ToString();

            foreach(var fi in d.GetFiles())
            {
                s+=fi.FullName;
                s+=fi.LastWriteTimeUtc.ToString();
            }
            foreach(var d2 in d.GetDirectories())
            {
                trv(d2);
            }
        };

        trv(new DirectoryInfo(dir));

        return s.GetHashCode();
    }
}

