using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

    #region FIND FILE
    public const string TRV_TIMEOUTERROR = "::timeout!!";
    public const string TRV_EXCEPTION    = "::exception!!";
    public const string TRV_UNKNOWN      = "::unknown!!";
    public static string FindTraverse(string start, string file, int millisec_timeout=-1)
    {
        if (millisec_timeout==-1) millisec_timeout = 60*1000; //60秒
        var timelimit = DateTime.Now + TimeSpan.FromMilliseconds(millisec_timeout);

        var find = FindTraverse(start,file,timelimit);

        return find;
    }

    public static string FindTraverse(string start, string file, DateTime timelimit) // *.xxに対応
    {
        bool bTimeout = false;
        string find = null;
        Action<DirectoryInfo> trv = null;
        trv = (d) => {
            if (bTimeout) return;
            if (d == null) return;
            if (find != null) return;

            if (DateTime.Now > timelimit) { bTimeout = true; return; }

            { 
                var di = new DirectoryInfo(d.FullName);
                var filist = di.GetFiles(file);
                if (filist!=null && filist.Length>0)
                {
                    find = filist[0].FullName;
                    return;
                }
                foreach (var d2 in d.GetDirectories())
                {
                    if (d2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                    if (d2.Attributes.HasFlag(FileAttributes.ReadOnly)) continue;
                    trv(d2);
                    if (find != null) return;
                }
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
    #endregion
    /// <summary>
    /// 環境変数 %hoge%を展開する 
    /// </summary>
    public static string ExtractPathWithEnvVals(string p)
    {
        var path = p;
        for(var loop = 0; loop<=100; loop++)
        {
            var env = RegexUtil.Get1stMatch(@"%.+?%",path);
            if (string.IsNullOrEmpty(env)) return path;
            var env2 = env.Trim('%');
            var val = Environment.GetEnvironmentVariable(env2);
            path = path.Replace(env,val);
        }
        throw new SystemException("Unexpected! {D834DC40-1A29-4078-91FC-13B232DC3440}");
    }

    public static string[] CollectFiles(string dir)
    {
        if (!Directory.Exists(dir))
        {
            throw new SystemException("{1C510D15-C222-4F02-811C-B250E43E2C27}");
        }
        var list = new List<string>();
        Action<DirectoryInfo> trv = null;
        trv = (di)=> {
            foreach(var fi in di.GetFiles())
            {
                if (fi.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                list.Add(fi.FullName);
            }
            foreach(var di2 in di.GetDirectories())
            {
                if (di2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                trv(di2);
            }
        };

        trv(new DirectoryInfo(dir));

        return list.ToArray();
    }
    public static string[] CollectFiles(string dir, string contain_string)
    {
        if (!Directory.Exists(dir))
        {
            throw new SystemException("{1C510D15-C222-4F02-811C-B250E43E2C27}");
        }
        var list = new List<string>();
        Action<DirectoryInfo> trv = null;
        trv = (di)=> {
            foreach(var fi in di.GetFiles())
            {
                if (fi.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                if (fi.FullName.Contains(contain_string))
                {
                    list.Add(fi.FullName);
                }
            }
            foreach(var di2 in di.GetDirectories())
            {
                if (di2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                trv(di2);
            }
        };

        trv(new DirectoryInfo(dir));

        return list.ToArray();
    }
        


    /// <summary>
    /// アスタリスク(*)が途中に入ったパスとマッチする最初のパスを返す
    /// 以下の場合のみ対象
    ///    x:\abc\*\def\ghi.txt
    ///    *はひとつのフォルダで、連続した複数ではない
    ///    *は [^\\]+として正規化表現
    ///    エラーはthrowされる。
    /// </summary>
    public static string FindMatchPathFileWithAstariskPath(string p)
    {
        if (string.IsNullOrEmpty(p))
        {
            throw new SystemException("{9D1FD86F-BFF1-42BA-B052-5708E601810D}");
        }
        var index = p.IndexOf(@"\*\");
        var firstpart = p.Substring(0,index);
        if (!Directory.Exists(firstpart))
        {
            throw new SystemException("{9EC68C87-6107-46E9-B097-EF841DA90D3F}");
        }
        var secondpart = p.Substring(index + 3);
        if (string.IsNullOrEmpty(secondpart))
        {
            throw new SystemException("{6194BD6E-D04E-4007-95D7-0B30DC889B78}");
        }
        var di = new DirectoryInfo(firstpart);
        foreach(var dt in di.GetDirectories())
        {
            if (dt.Attributes.HasFlag(FileAttributes.Hidden)) continue;
            var tmppath = Path.Combine(dt.FullName , secondpart);
            if (File.Exists(tmppath))
            {
                return tmppath;
            }
        }
        throw new SystemException("{D9639029-D995-4C6D-B950-881410690343}");
    }
    public static string FindMatchPathFileWithAstariskPath_obs(string p)
    {
        var index = p.IndexOf("\\*");
        var firstpart = p.Substring(0,index);
        if (!Directory.Exists(firstpart))
        {
            throw new SystemException("{9EC68C87-6107-46E9-B097-EF841DA90D3F}");
        }
        //var secondpart = p.Substring(index+3);
        var file = Path.GetFileNameWithoutExtension(p);
        var list = CollectFiles(firstpart,file);
        var regex ="^" +  p.Replace(@"\",@"\\").Replace(":",@"\:").Replace("(",@"\(").Replace(")",@"\)").Replace("*",@"[^\\]+") + "$";
        foreach(var l in list)
        {
            if (RegexUtil.IsMatch(regex, l))
            {
                return l;
            }
        }
        throw new SystemException("{5FAB37AC-0E29-427E-B2E2-B0B8A78EA53A}");
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
    public static string PathToFolderNameHeaderString(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;
        var list = PathToList(path);
        var hs = string.Empty;
        foreach(var l in list)
        {
            var c = l.Length > 0 ? l[0] : '-';
            hs += c.ToString();
        }     
        return hs;  
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

    /*
        実行ファイルパス
        Args[0]より
    */
    public static string GetThisAppPath()
    {
        var args = System.Environment.GetCommandLineArgs();
        if (args.Length>0)
        {
            return Path.GetFullPath(Path.GetDirectoryName(args[0]));
        }
        return null;
    }

    public static bool CheckValid(string path)
    {
        var b = false;
        if (string.IsNullOrWhiteSpace(path)) {
            b = false;
        }
        else
        {
            try {
                var fullpath = Path.GetFullPath(path);
                b = true;
            }
            catch {
                b = false;
            }
        }
        return b;
    }

    public static string GetFullPath(string p)
    {
        var full = Path.GetFullPath(p);
        if (full != null && full.Length >= 2 && full[1]==':')
        {
            var c = full[0].ToString().ToLower();
            return c + full.Substring(1);
        }
        return full;
    }
}


