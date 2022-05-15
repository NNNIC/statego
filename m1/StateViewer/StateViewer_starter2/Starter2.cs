using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StateViewer_starter2
{
    public class Starter2
    {
        public string m_githash;
        public string m_version;
        public string m_buildtime;

        public string m_target_psgg;
        public string m_target_xls;

        public bool   m_bNewFiles; //作成したのは 新規のファイル。このフラグで、FileDB作成へ誘導する。

        Start2Form m_startform;

        //2019 新版 新規作成用  m_bNewFilesはPSGGの旧版で実装用で、この下のは新版用
        public bool m_bNew2019Files;
        public string m_new_target_psgg;
        public string m_new_target_xls;

        static HistoryUtil m_hist;
        public void Init()
        {
            m_hist = new HistoryUtil();
                 
            m_target_xls = null;
            m_startform = new Start2Form(this);

            m_startform.ShowDialog();

            if (m_startform.DialogResult == DialogResult.OK)
            {
                //
            }

            m_bNew2019Files = m_startform.m_bNew2019Files;
            m_new_target_psgg = m_startform.m_new_target_psgg;
            m_new_target_xls = m_startform.m_new_target_xlsx;
        }

        public static void UpdateHistroy(string file) { m_hist.UpdateHistory(file);     }
        public static string[] GetHistory()           { return m_hist.GetHistory();     } 
    }
}
