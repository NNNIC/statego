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
    /*Obsolete*/
    public class History
    {
        /*
            アクション度、
                    iniファイルコピー
                    excel cahceファイルコピー
                    シリアル化して出力

            バック時
                    iniファイルと exec cacheをデータへ反映
                    リドロー
        */

        private static string TEMPFILE { get {
                return Path.Combine(Path.GetTempPath(), G.userenv_guid) + ".psgg.hisitory";
            } }

        private static bool   m_bReqSave;  //RedrawOpt後にセーブを実行予約
        private static string m_reqaction; //セーブ時に

        /// <summary>
        /// 初期化  ファイルロード、リロードの度に必要
        /// </summary>
        public static void Init()
        {
            if (File.Exists(TEMPFILE))
            {
                try {
                    File.Delete(TEMPFILE);
                }
                catch
                {

                }
            }

            m_histroy_data = new List<Item>();
            m_next_index = 0;
        }

        [Serializable]
        public class Item
        {
            public DateTime date;
            public string   action;
            //public string   ini;
            public Dictionary<string, Dictionary<string, PointF> > fillter_state_location_list;

            public Dictionary<int, ExcelCellCacheItem> excel_cache;

            public string target_pathdir;
        }

        private static List<Item> m_histroy_data = new List<Item>();

        public static int m_next_index;

        private static void Record()
        {
            var item = new Item();
            item.date = DateTime.Now;
            item.action = G.last_action;

            item.excel_cache = clone_dic(G.m_excel_cell_cache_dic);
            //item.ini = (string)G.excel_config.m_configini.Clone();;
            item.fillter_state_location_list = clone_dic( G.fillter_state_location_list );

            if (m_histroy_data.Count != m_next_index) //Backで乱れた
            {
                for(var loop = 0; loop<=10000; loop++)
                {
                    if (loop == 10000) throw new SystemException("Unexpected! {173098AF-06B1-4582-B5C7-D1F1D4471EE4}");
                    if (m_next_index >= 0 &&  m_histroy_data.Count >= 0 && m_next_index < m_histroy_data.Count)
                    {
                        m_histroy_data.RemoveAt(m_histroy_data.Count-1);
                    }
                    else
                    {
                        break;
                    }
                }
                m_next_index = m_histroy_data.Count;
            }

            item.target_pathdir = G.m_target_pathdir;

            m_histroy_data.Add(item);
            m_next_index = m_histroy_data.Count;

            G.NoticeToUser("Histroy recorded ... " + item.action );
        }

        public static void Back()
        {
            if(m_next_index <= 0)
            {
                G.NoticeToUser_warning("Histroy is nothing. #1");
                return;
            }

            var index = m_next_index - 2;
            if(index >= m_histroy_data.Count || index < 0)
            {
                G.NoticeToUser_warning("Histroy is nothing. #2");
                return;
            }

            m_next_index = index + 1;

            Reload(index, "Rollbacking ... ");
        }

        public static void Forward()
        {
            if (m_next_index <= 0)
            {
                G.NoticeToUser_warning("Histroy is nothing. #3");
                return;
            }
            var index = m_next_index;
            if (index >= m_histroy_data.Count)
            {
                G.NoticeToUser_warning("Histroy is nothing. #4");
                return;
            }

            m_next_index++;

            Reload(index, "Forwarding ... ");
        }

        //public static bool HasHistoryGtOne()
        //{
        //    return m_histroy_data.Count > 1;
        //}

        private static void Reload(int index, string pre)
        {
            var item = m_histroy_data[index];

            G.m_target_pathdir = item.target_pathdir;

            G.m_excel_cell_cache_dic = clone_dic(item.excel_cache);

            //SaveLoadIni.LoadIniText((string)item.ini.Clone());

            G.state_location_list = null;
            G.fillter_state_location_list =  clone_dic( item.fillter_state_location_list );

            G.state_working_list_reflesh();

            //G.req_redraw_force();

            G.NoticeToUser(pre + item.action);
        }

        /// <summary>
        /// 【未使用】
        /// </summary>
        public static void ReqToSave(string action)
        {
            m_bReqSave = true;
            m_reqaction = action;
        }

        //public static void SaveAtRedraw()
        //{
        //    if (m_bReqSave)
        //    {
        //        m_bReqSave = false;
        //        G.last_action = m_reqaction;
        //        Record();
        //    }
        //}

        public static void SaveForce(string action)
        {
            m_reqaction   = action;
            G.last_action = m_reqaction;
            Record();
        }

        private static Dictionary<int, ExcelCellCacheItem> clone_dic(Dictionary<int, ExcelCellCacheItem> src)
        {
            var newdic = new Dictionary<int, ExcelCellCacheItem>();
            foreach(var p in src )
            {
                var cell = p.Value.Clone();
                newdic.Add(p.Key, cell);
            }
            return newdic;
        }

        private static Dictionary<string, Dictionary<string, PointF> > clone_dic(Dictionary<string, Dictionary<string, PointF> > src)
        {
            var newdic = new Dictionary<string, Dictionary<string, PointF> >();
            foreach(var p in src)
            {
                var tmp = new  Dictionary<string, PointF>();
                foreach(var q in p.Value)
                {
                    tmp.Add(q.Key,q.Value);
                }
                newdic.Add(p.Key, tmp);
            }
            return newdic;
        }
    }
}
