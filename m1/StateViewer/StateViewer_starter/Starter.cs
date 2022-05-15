using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StateViewer_starter
{
    [Obsolete]
    public class Starter
    {
        public string m_version;
        public string m_target_xls;

        StartForm m_startform;
        public void Init()
        {
            m_target_xls = null;
            m_startform = new StartForm();
            m_startform.m_version = m_version;

            m_startform.ShowDialog();

            if (m_startform.DialogResult == DialogResult.OK)
            {
                m_target_xls = m_startform.m_target_xlsx;
            }
        }

        #region history
        public static void UpdateHistory(string file)
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
        public static string[] GetHistory()
        {
            var historyfile = _historyfile();
            if (historyfile==null) return null;

            var list = _gethistorylist();
            if (list ==null) return null;

            return list.ToArray();
        }
        private static string _historyfile()
        {
            var tmpfolder = Environment.GetEnvironmentVariable("TMP");
            if (tmpfolder==null) return null;

            var historyfile = Path.Combine(tmpfolder,"StateViewerHistory.txt");

            return historyfile;
        }
        private static List<String> _gethistorylist()
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
}
