using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public partial class PathUtil
{

    public static bool m_halt;

    #region collect files using traverse under the folder
    /// <summary>
    /// 指定フォルダ以下をトラバースしてファイルを収集する
    /// </summary>
    public static List<string> CollectTraverse(string start, string file, int millisec_timeout, out string err)
    {
        if (millisec_timeout<=0) millisec_timeout = 60*1000; //60秒
        var timelimit = DateTime.Now + TimeSpan.FromMilliseconds(millisec_timeout);
        var file_files = CollectTraverse(start,file,timelimit, out err);

        return file_files;
    }

    public static List<string> CollectTraverse(string start, string file, DateTime timelimit, out string err)
    {
        err = string.Empty;
        bool bTimeout = false;
        var list = new List<string>();
        Action<DirectoryInfo> trv = null;
        var err2 = string.Empty;
        trv = (d) => {
            if (m_halt) return;
            if (bTimeout) return;
            if (d == null) return;

            if (DateTime.Now > timelimit) { bTimeout = true; return; }

            { 
                if (file.Contains("*"))
                {
                    var files = (new DirectoryInfo(d.FullName)).GetFiles(file);
                    foreach(var f in files)
                    {
                        list.Add(f.FullName);
                    }
                }
                else
                { 
                    var path = Path.Combine(d.FullName, file);
                    if (File.Exists(path))
                    {
                        list.Add(path);
                    }
                }
                try { 
                    foreach (var d2 in d.GetDirectories())
                    {
                        if (d2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                        if (d2.Attributes.HasFlag(FileAttributes.ReadOnly)) continue;
                        trv(d2);
                    }
                } catch (SystemException e)
                {
                    err2 = TRV_EXCEPTION;
                }
            }
        };

        try
        {
            trv(new DirectoryInfo(start));
        }
        catch
        {
            err = TRV_EXCEPTION;
            return list;
        }

        if (bTimeout)
        {
            err = TRV_TIMEOUTERROR;
            return list;
        }

        return list;
    }
    #endregion

    /// <summary>
    /// 検索ファイル：指定フォルダより下をトラバースし、次に親フォルダよりトラバースする。
    /// タイムアウトあり
    /// </summary>
    public static List<string> CollectTraverseDownAndUp(string start, string file, int millisec_timeout, out string err)
    {
        err = string.Empty;

        if (millisec_timeout <= 0) millisec_timeout = 60 * 1000;

        bool  bTimeout = false;
        var starttime = DateTime.Now;
        var timelimit = DateTime.Now + TimeSpan.FromMilliseconds(millisec_timeout);

        // 直下検索
        var list = CollectTraverse(start,file, timelimit, out err);
        if (err == TRV_TIMEOUTERROR )
        {
            bTimeout = true;
            return list;
        }

        // Dirを上方向に移動しながら、配下Dirを検索する。ただし、元のところは除く
        bool bTimeout2 = false;
        Action<DirectoryInfo,string> trv = null;
        string err2 = string.Empty;
        trv = (di, ignore) => {
            if (m_halt) return;
            if (bTimeout2) return;
            if (di==null) return;
            if (DateTime.Now > timelimit) { bTimeout2 = true; return; }

            var path = Path.Combine(di.FullName,file);
            if (File.Exists(path))
            {
                var f = Path.GetFullPath(path);
                list.Add(f);                
            }

            foreach (var di2 in di.GetDirectories())
            {
                if (di2.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                if (di2.Name == ignore) continue;
                var list2 = CollectTraverse(di2.FullName,file,timelimit, out err2);
                if (err2 == TRV_TIMEOUTERROR) { 
                    bTimeout2 = true;
                    return;
                }
                list2.ForEach(i=> {
                    if (!list.Contains(i)) list.Add(i);
                });
            }

            var parent2 = di.Parent;
            var dir2 = di.Name;

            trv(parent2,dir2);
        };

        var parent = (new DirectoryInfo(start)).Parent;
        var dir    = (new DirectoryInfo(start)).Name;

        trv(parent,dir);

        if (list == null || list.Count==0)
        {
            if (err==null)
            {
                err = TRV_UNKNOWN;
            }
        }

        if (bTimeout2)
        {
            err = TRV_TIMEOUTERROR;
        }

        return list;
    }

    public static bool CheckValid(string path)
    {
        var b = false;
        var fullpath = string.Empty;
        if (string.IsNullOrWhiteSpace(path)) {
            b = false;
        }
        else
        {
            try {
                fullpath = Path.GetFullPath(path);
                b = true;
            }
            catch {
                b = false;
            }
        }
        return b;
    }
}