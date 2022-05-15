using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// DLL共用使用を考えて、staticを使わない

public class HistoryUtil //ファイルのヒストリ
{
    #region history
    public void UpdateHistory(string file)
    {
        if (string.IsNullOrEmpty(file)) return;

        var list = _gethistorylist();
        if (list == null) list = new List<string>();

        var fullpath = Path.GetFullPath(file);
        if (!File.Exists(fullpath)) return;

        var i = list.IndexOf(fullpath);
        if (i>=0) list.RemoveAt(i);

        list.Insert(0, Path.GetFullPath(file));

        File.WriteAllLines(_historyfile(),list.ToArray());
    }
    public string[] GetHistory()
    {
        var historyfile = _historyfile();
        if (historyfile==null) return null;

        var list = _gethistorylist();
        if (list ==null) return null;

        return list.ToArray();
    }
    private string _historyfile()
    {
        var tmpfolder = Path.GetTempPath();//Environment.GetEnvironmentVariable("TMP");
        if (tmpfolder==null) return null;

        var historyfile = Path.Combine(tmpfolder,"pssgEditorHistory.txt");

        return historyfile;
    }
    private List<String> _gethistorylist()
    {
        var historyfile = _historyfile();
        if (string.IsNullOrEmpty(historyfile)) return null;
            
        string[] lines = null;
        if (File.Exists(historyfile))
        {
            lines = File.ReadAllLines(historyfile,Encoding.UTF8);
        }
        var list = new List<string>();
        if (lines!=null) foreach(var l in lines)
        {
            var l2 = l.Trim();
            if (string.IsNullOrEmpty(l2)) continue;

            //if (File.Exists(l2))
            {
                list.Add(l2);
            }
        }

        if (list.Count == 0) return null;

        return list;
    }
    #endregion
}
