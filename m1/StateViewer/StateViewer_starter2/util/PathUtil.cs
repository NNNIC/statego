using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public partial class PathUtil
{
    public static string GetRelativePath(string baseUri,　string targetUri)
    {
        if (string.IsNullOrEmpty(baseUri) || string.IsNullOrEmpty(targetUri)) return null;
        try {
            var u1 = new Uri(baseUri + @"\");
            var u2 = new Uri(targetUri+ @"\");

            var relativeUri = u1.MakeRelativeUri(u2);

            var relativePath = relativeUri.ToString();

            relativePath = relativePath.Replace('/', '\\');

            //Console.WriteLine(relativePath);

            var r = relativePath.TrimEnd('\\');
            if (string.IsNullOrEmpty(r))
            {
                return ".";
            }
            return r;
        } catch
        {
            return string.Empty;
        }
    }

    public static string GetAncestorPath(string start, string name)
    {
        if (string.IsNullOrEmpty(start)) return string.Empty;

        var fullsatart = Path.GetFullPath(start);
        fullsatart = fullsatart.Replace('/','\\');
        var index = fullsatart.LastIndexOf("\\" + name + "\\");
        if (index < 0) return string.Empty;

        return fullsatart.Substring(0,index) + "\\" + name;
    }

    public static string ShortenPath(string path, int n)
    {
        if (path.Length <= n)//指定文字数以下の場合は、そのまま
        {
            return path;
        }

        var fn = Path.GetFileName(path);
        var p = Path.GetDirectoryName(path);        
        var d = p[0]+p[1];
        if (fn.Length >= n) return d + @"..\" + fn; // ファイル名部分だけで 指定文字数を超える場合は、ドライブ名＋"..." + ファイル名
        
        // 合成する。
        var fnx = @"..\" + fn;
        var p2 = p.Substring(0, n - fnx.Length) + fnx;

        return p2;
    }

    public static string GetAcculateFileName(string path)
    {
        try { 
            var fn = Path.GetFileName(path).ToLower();
            var di = new DirectoryInfo( Path.GetDirectoryName(path) );
            foreach(var f in di.GetFiles())
            {
                if (f.Name.ToLower() == fn)
                {
                    return f.FullName;
                }
            }
        } catch {
        }
        return null;
    }

    public const string TRV_TIMEOUTERROR = "::timeout!!";
    public const string TRV_EXCEPTION    = "::exception!!";
    public const string TRV_UNKNOWN      = "::unknown";
    public static string FindTraverse(string start, string file, DateTime timelimit)
    {
        bool bTimeout = false;
        string find = null;
        Action<DirectoryInfo> trv = null;
        trv = (d) => {
            if (bTimeout) return;
            if (d == null) return;
            if (find != null) return;

            if (DateTime.Now > timelimit) { bTimeout = true; return; }

            var path = Path.Combine(d.FullName, file);
            if (File.Exists(path))
            {
                find = path;
                return;
            }
            foreach (var d2 in d.GetDirectories())
            {
                if (d2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                if (d2.Attributes.HasFlag(FileAttributes.ReadOnly)) continue;
                trv(d2);
                if (find != null) return;
            }
        };

        try
        {
            trv(new DirectoryInfo(start));
        }
        catch
        {
            return TRV_EXCEPTION;
        }

        if (bTimeout)
        {
            return TRV_TIMEOUTERROR;
        }

        return find;
    }

    /// <summary>
    /// 検索ファイル：指定フォルダより下をトラバースし、次に親フォルダよりトラバースする。
    /// タイムアウトあり
    /// </summary>
    public static string FindTraverseDownAndUp(string start, string file, int millisec_timeout, out bool bTimeout)
    {
        bTimeout = false;
        var starttime = DateTime.Now;
        var timelimit = DateTime.Now + TimeSpan.FromMilliseconds(millisec_timeout);

        // 直下検索
        var find = FindTraverse(start,file, timelimit);
        if (find == TRV_TIMEOUTERROR || find==TRV_EXCEPTION)
        {
            bTimeout = true;
            return null;
        }
        if (find!=null) return find;


        // Dirを上方向に移動しながら、配下Dirを検索する。ただし、元のところは除く
        bool bTimeout2 = false;
        Action<DirectoryInfo,string> trv = null;
        trv = (di, ignore) => {
            if (bTimeout2) return;
            if (di==null) return;
            if (find !=null) return;

            if (DateTime.Now > timelimit) { bTimeout2 = true; return; }

            var path = Path.Combine(di.FullName,file);
            if (File.Exists(path))
            {
                find = path;
                return;
            }

            foreach (var di2 in di.GetDirectories())
            {
                if (di2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                if (di2.Name == ignore) continue;

                find = FindTraverse(di2.FullName,file,timelimit);
                if (find == TRV_TIMEOUTERROR || find == TRV_EXCEPTION) {
                    bTimeout2 = true;
                    return;
                }
                if (find!=null) return;
            }

            var parent2 = di.Parent;
            var dir2 = di.Name;

            trv(parent2,dir2);
        };

        var parent = (new DirectoryInfo(start)).Parent;
        var dir    = (new DirectoryInfo(start)).Name;

        trv(parent,dir);

        if (bTimeout2)
        {
            bTimeout = true;
            return null;
        }

        return find;
    }

    /*
        パス名をリストに分解する。
        例） c:\a\b\c\d.txt
        [0] = c:
        [1] = a
        [2] = b
        [3] = c
        [4] = d.txt
    */
    public static string[] PathToList(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        return path.Split('\\');
    }
    /*
       PathToListの逆
    */
    public static string ListToPath(string[] list)
    {
        if (list == null) return null;
        var s = string.Empty;
        foreach(var i in list)
        {
            if (!string.IsNullOrEmpty(s)) s+=@"\";
            s += i;
        }
        return s;
    }
}

