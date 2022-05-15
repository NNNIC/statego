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

namespace stateview
{
    public class StateView
    {
        public string m_version;
        public string m_githash;
        public string m_buildtime;
        public string m_milestone;
        public string m_milestonetxt;
        _5000_MainForm.ViewForm m_mf;

        bool m_bAlive;
        public void Init(string xlsFile,string psggFile=null)
        {
            //System.Diagnostics.Debugger.Launch();

            G.psgg_file = psggFile; //load_fileより先に
            if (string.IsNullOrEmpty(G.psgg_file))
            {
                var tmp = Path.Combine( Path.GetDirectoryName(xlsFile), Path.GetFileNameWithoutExtension(xlsFile) + ".psgg");
                if (File.Exists(tmp))
                {
                    G.psgg_file = tmp;
                }
            }

            if (G.psgg_file!=null && File.Exists(G.psgg_file))
            {
                FileDbUtil.read_psgg_header_info(); // G.psg_header_info_xxxxx が設定される。

                G.psgg_file_w_data = FileDbUtil.is_psggfile_ver1_1();
                if (G.psgg_file_w_data)
                {
                    G.guid = G.psgg_header_info_guid;
                }
            }
            ExcelDll.Enabled = !(G.psgg_file_w_data); // ver1.1では Excel Read/Writeを閉じる。 ※意図的に使う場合はある。IMPORT/EXPORT時　開発時のエラーを優先する目的もある。

            G.load_file = xlsFile;

            m_mf = new _5000_MainForm.ViewForm();

            G.version = m_version;
            G.githash = m_githash;
            G.buildtime= m_buildtime;
            G.milestone = m_milestone;
            G.milestonetxt = m_milestonetxt;

            m_mf.FormClosed += M_mf_FormClosed;
            m_mf.Show();
            m_bAlive = true;
        }

        public bool IsAlive()
        {
            return m_bAlive;
        }

        private void M_mf_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_bAlive = false;
        }
    }
}
