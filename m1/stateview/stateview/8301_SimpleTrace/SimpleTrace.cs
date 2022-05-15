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

/*
    StateGoで、単純なトレースを実現する
    2019.7.27 暫定実装中
*/

namespace stateview
{
    public class SimpleTrace
    {
        private const  string   m_target_file       = @"%LOCALAPPDATA%\Unity\Editor\Editor.log";
        private static Encoding m_target_file_enc = Encoding.UTF8;
        private const  int      m_max_readsize      = 1028 * 10;
        private const  int      m_read_padding      = 100;

        private const string    m_trace_regex = @"\*psgg\-trace\:.+?\*"; 
        private const string    m_trace_header= @"*psgg-trace:";

        public static void exec() {
            try
            {
                _exec();
            }
            catch (SystemException e)
            {
                G.NoticeToUser_warning("{0B2930FB-4B83-40C5-BDAF-D29675C485DA}" + e.Message);
            }
        }

        private static void _exec()
        {
            var file = PathUtil.ExtractPathWithEnvVals(m_target_file);

            var find_state = string.Empty;
            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read,FileShare.ReadWrite)) {

                var offset = fs.Length > m_max_readsize ? (int)fs.Length - m_max_readsize : 0;
                var buf = new byte[m_max_readsize + m_read_padding];
                fs.Seek(offset, SeekOrigin.Begin);
                fs.Read(buf,0,m_max_readsize);
                var text = m_target_file_enc.GetString(buf);
                if (!string.IsNullOrEmpty(text)) {
                    var find = text.LastIndexOf(m_trace_header);
                    if (find>=0)
                    {
                        var findstr = RegexUtil.Get1stMatch(m_trace_regex, text.Substring(find));
                        if (!string.IsNullOrEmpty(findstr))
                        {
                            find_state = findstr.Substring(m_trace_header.Length).Trim('*');
                        }
                    }
                }

            }
            
            if (!string.IsNullOrEmpty(find_state)) { 
                G.NoticeToUser("TRACE STATE:" + find_state);
                G.vf_sc.m_center_focus_state = find_state; //セットしたらフォーカス＆中央
            }
            else
            {
                G.NoticeToUser("TRACE STATE: - none -");
            }
        }

    }
}
