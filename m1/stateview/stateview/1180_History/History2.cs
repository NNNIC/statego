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

/// <summary>
/// 履歴機能　2019.12.25
/// </summary>
namespace stateview
{
    public class History2
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

        //private static string TEMPFILE { get {
        //        return Path.Combine(Path.GetTempPath(), G.guid) + ".psgg.hisitory2";
        //    } }

        [Serializable]
        public class Item
        {
            public DateTime date;
            public string   action;
            public string   comment;

            public Dictionary<string, Dictionary<string, PointF> > fillter_state_location_list;

            public Dictionary<int, ExcelCellCacheItem> excel_cache;

            public string target_pathdir;
            public Point  topleft;

            public bool bModified_pos;
            public bool bModified_value;

            public Guid  guid;

            public Item()
            {
                guid = Guid.NewGuid();
            }
        }

        private static DateTime m_initialized_time;
        private static string   m_initaialzed_dirpath;

        private static List<Item> m_history_data_back = new List<Item>();
        private static List<Item> m_history_data_forward = new List<Item>();

        public static void Init()
        {
            m_history_data_back.Clear();
            m_history_data_forward.Clear();
        }

        private static Item new_item()
        {
            var item = new Item();

            item.date = DateTime.Now;
            item.action = G.last_action;

            item.excel_cache = clone_dic(G.m_excel_cell_cache_dic);
            item.fillter_state_location_list = clone_dic(G.fillter_state_location_list);

            item.target_pathdir = G.node_get_cur_dirpath();
            item.topleft = ViewUtil.GetViewTopLeft();
            
            return item;
        }

        private static void record(Item item)
        {
            m_history_data_back.Insert(0,item);
            m_history_data_forward.Clear();

            G.NoticeToUser("Histroy recorded ... " + item.action );

            historyrecord_from_update(item);
        }

        public static void SaveForce_modify_pos(string comment=null)
        {
            G.last_action = "mov," + comment;
            var item = new_item();
            item.action = "mov";
            item.comment = comment;
            item.bModified_pos = true;
            record(item);
        }
        public static void SaveForce_modify_value(string cmt)
        {
            G.last_action = "varied," + cmt;
            var item = new_item();
            item.action = "vry";
            item.comment = cmt;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_new(string  comment)
        {
            G.last_action = "new," + comment;
            var item = new_item();
            item.action = "new";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_paste(string comment)
        {
            G.last_action = "paste," + comment;
            var item = new_item();
            item.action = "pst";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_initialized(string comment)
        {
            G.last_action = "init,"+comment;
            var item = new_item();
            item.action = "ini";
            item.comment = comment;
            record(item);
            m_initialized_time = item.date;
            m_initaialzed_dirpath = G.node_get_cur_dirpath();
        }
        public static void SaveForce_grouping(string comment)
        {
            G.last_action = "group,"+comment;
            var item = new_item();
            item.action = "grp";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_ungrouping(string comment)
        {
            G.last_action = "ungroup,"+comment;
            var item = new_item();
            item.action = "ung";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_editgroup(string comment)
        {
            G.last_action = "editgroup,"+comment;
            var item = new_item();
            item.action = "edg";
            item.comment = comment;
            item.bModified_value = true;
            record(item);

        }
        public static void SaveForce_commentout(string comment)
        {
            G.last_action = "commentout,"+comment;
            var item = new_item();
            item.action = "co";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static void SaveForce_delete(string comment)
        {
            G.last_action = "deleted,"+comment;
            var item = new_item();
            item.action = "del";
            item.comment = comment;
            item.bModified_value = true;
            record(item);
        }
        public static int Get_hitstory_data_count()
        {
            return m_history_data_back.Count;
        }
        public static string Get_initial_dirpath()
        {
            return m_initaialzed_dirpath;
        }
        public static Item Get_initialized_item()
        {
            var item = m_history_data_back.Find(i=>i.date == m_initialized_time);
            if (item == null)
            {
                item = m_history_data_forward.Find(i=>i.date == m_initialized_time);
            }
            return item;
        }

        public static Item Back()
        {
            var item = _back();

            historyrecord_from_update(item);

            return item;
        }
        public static Item _back()
        {   
            var item = ListUtil.GetVal(m_history_data_back,0);
            if (item == null) return null;

            if (m_history_data_back.Count == 1) //最終一個は保持
            {
                return item; 
            }

            //if (item.target_pathdir != G.node_get_cur_dirpath())
            //{
            //    return item; //上位で処理
            //}
            if (m_history_data_forward.Count == 0 && m_history_data_back.Count>=2) //まだ一度もバックしていない
            {
                m_history_data_forward.Insert(0,item);        //最初のをforwardへ
                ListUtil.RemoveAt(ref m_history_data_back,0); //backから除外

                item = ListUtil.GetVal(m_history_data_back,0);//あらためてitem取得
                if (item.target_pathdir != G.node_get_cur_dirpath())
                {
                    return item; //上位で処理
                }
            }
            if (item==null) return null;

            if (m_history_data_back.Count > 1)
            { 
                m_history_data_forward.Insert(0,item);        //itemをforwardへ
                ListUtil.RemoveAt(ref m_history_data_back,0); //backから除外
            }
            return item;
        }

        public static Item Forward()
        {
            var item = ListUtil.GetVal(m_history_data_forward,0);
            if (item!=null && item.target_pathdir == G.node_get_cur_dirpath())
            { 
                ListUtil.RemoveAt(ref m_history_data_forward,0);
                if (item!=null)
                {
                    m_history_data_back.Insert(0,item);
                }
            }

            historyrecord_from_update(item);

            return item;
        }

        #region record track update
        static Item m_latest;
        static void historyrecord_from_update(Item latest = null)
        {
            m_latest = latest;
            var dg = G.m_history_record_panel.m_dgv;

            var number = 1;
            dg.Rows.Clear();
            for(var n = m_history_data_back.Count - 1; n >= 0; n--)
            {
                var item = m_history_data_back[n];
                dg.Rows.Insert(0,number.ToString(), item.action, item.comment);
                dg.Rows[0].Tag = item.guid;
                number++;
            }

            if (latest==null && m_history_data_forward.Count == 0)
            {
                dg.Rows[0].Selected = true;
                return;
            }

            for(var n = 0; n < m_history_data_forward.Count; n++)
            {
                var item = m_history_data_forward[n];
                dg.Rows.Insert(0,number.ToString(), item.action.ToString(), item.comment);
                dg.Rows[0].Tag = item.guid;

                number++;
            }
        
            if (latest!=null)
            { 
                for (var r = 0; r < dg.Rows.Count; r++)
                {
                    var tguid = (Guid)dg.Rows[r].Tag ;
                    if (tguid == latest.guid)
                    {
                        dg.Rows[r].Selected = true;
                        break;
                    }
                }
            }

            if ( dg.SelectedCells.Count > 0 ) { 
                // 対象行が非表示の場合は何もしない
                if ( !dg.SelectedCells[0].Visible ) {
                    return;
                }
                // 対象行が既に画面内に表示されている時は何もしない
                if ( dg.SelectedCells[0].Displayed ) {
                    return;
                }
                dg.FirstDisplayedScrollingRowIndex = dg.SelectedCells[0].RowIndex;
               
            }
        }
        #endregion


        #region clone
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
        #endregion

    }
}
