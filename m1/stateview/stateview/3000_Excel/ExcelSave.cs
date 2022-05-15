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
    public class ExcelSave
    {
        #region SAVE高速化
        private static Dictionary<int, ExcelCellCacheItem> m_save_cache_dic;
        public static void CreateCache()
        {
            m_save_cache_dic = G.clone_excel_cell_cache_dic();
        }

        public static bool update(ExcelDll ed, ExcelCellCacheItem item, ref int count )
        {
            var key = item.key;
            if (m_save_cache_dic.ContainsKey(key))
            {
                var save_item = m_save_cache_dic[key];
                if (save_item.isEqualText(item)) return false;
            }
            else
            {
                // 追加があっても 空白文時は対象外
                if (string.IsNullOrWhiteSpace(item.text)) return false;
            }

            //G.NoticeToUser(string.Format("{0},{1}:{2}",item.col,item.row,item.text));
            // 異なる、または、新規だった場合以下を実行

            ed.SetStr(item.row,item.col,item.text);
            ed.CopyFormat(item.row,2, item.row, item.col);

            count++;

            return true;
        }
        public static IEnumerator Save_co()
        {
            var save_count = 0;

            //m_cells = new List<Excel.Range>();

            var file = G.load_file;

            var ed = new ExcelDll();
            ed.Load(file);
            ed.SetSheet(G.sheetchart);
            //var sheet = ed.GetSheet();

            G.NoticeToUser_woNewLine("Checking update ");
            //bool updated = false;
            var loop = 0;
            foreach(var k in G.m_excel_cell_cache_dic.Keys)
            {
                var cd = G.m_excel_cell_cache_dic[k];

                if (cd.bAlt)
                {
                    cd = cd.Clone();
                    cd.text = string.Empty; //入力なし
                }

                var updated = update(ed,cd,ref save_count);
                if (updated && (save_count % 20 == 1))
                {
                    G.NoticeToUser_woNewLine("*");
                }
                if (loop % 100 == 1) {
                    G.NoticeToUser_woNewLine(".....");
                    yield return null;
                }
                loop++;
            }

            ed.WriteSheet();

            G.NoticeToUser(" done");

            if (G.name_list!=null && G.name_list.Contains(G.STATENAME_thumbnail))
            {
                G.NoticeToUser("Saving pictures.");
                G.excel_pictures.WriteToExcel(ed);
            }

            //Config
            ed.NewSheetForce(G.sheetconfig);

            G.NoticeToUser("Updating Config");
            yield return null;

            G.excel_config.Update(ed);

            //template-src
            ed.NewSheetForce(G.sheettempsrc);
            //sheet = ed.GetSheet();

            G.NoticeToUser("Updating template source");
            yield return null;

            G.excel_convertsettings.Update_template_src(ed);

            //template-func
            ed.NewSheetForce(G.sheettempfunc);
            //sheet = ed.GetSheet();

            G.NoticeToUser("Updating template function");
            yield return null;

            G.excel_convertsettings.Update_template_func(ed);

            //setteing-ini
            ed.NewSheetForce(G.sheetsetting);
            //sheet = ed.GetSheet();

            G.NoticeToUser("Updating setting");
            yield return null;

            G.excel_convertsettings.Update_setting_ini(ed);

            //help
            ed.NewSheetForce(G.sheethelp);
            G.NoticeToUser("Updating help");
            yield return null;
            G.excel_convertsettings.Update_help_ini(ed);

            //items
            ed.NewSheetForce(G.sheetitems);
            G.NoticeToUser("Updating items info");
            yield return null;
            G.excel_convertsettings.Update_items_ini(ed);

            // !! SAVE
            G.NoticeToUser("Saving Excel");
            yield return null;

            ed.SetSheet(G.sheetchart); //state chartを前面に。
            if (ed.Save())
            {
                //G.bDirty = false;
                G.Dirty_clear_all();
                G.update_viewform_title();
            }

            ed.Dispose();
            ed = null;

            //Cache 更新
            CreateCache();

        }
        #endregion

        #region PSGG FILE W DATA 用
        public static bool update_byFileDb(ExcelCellCacheItem item, ref int count )
        {
            var key = item.key;
            if (m_save_cache_dic.ContainsKey(key))
            {
                var save_item = m_save_cache_dic[key];
                if (save_item.isEqualText(item)) return false;
            }
            else
            {
                // 追加があっても 空白文時は対象外
                if (string.IsNullOrWhiteSpace(item.text)) return false;
            }

            FileDbUtil.set_val(item.row, item.col, item.text);

            count++;

            return true;
        }

        public static IEnumerator Save_by_FileDb_co()
        {
            var save_count = 0;

            G.NoticeToUser_woNewLine("Checking update ");
            yield return null;

            var loop = 0;
            foreach(var k in G.m_excel_cell_cache_dic.Keys)
            {
                var cd = G.m_excel_cell_cache_dic[k];

                if (cd.bAlt)
                {
                    cd = cd.Clone();
                    cd.text = string.Empty; //入力なし
                }

                var updated = update_byFileDb(cd,ref save_count);
                loop++;
            }

            G.NoticeToUser(" done");
            yield return null;

            if (G.name_list!=null && G.name_list.Contains(G.STATENAME_thumbnail))
            {
                FileDbUtil.delete_unused_bmp(); //管理ファイルから不使用のビットマップ削除
                G.excel_pictures.WriteToFileDB();
            }

            //更新部分をFileDBファイルに反映
            FileDbUtil.write_filedb_manager_and_dirty_statedata_only();

            //Config
            G.excel_config.SaveForce_byFileDB();

            //template-src
            G.excel_convertsettings.Update_template_src_byFileDB();

            //template-func
            G.excel_convertsettings.Update_template_func_byFileDB();

            //setteing-ini
            G.excel_convertsettings.Update_setting_ini_byFileDB();

            //help
            G.excel_convertsettings.Update_help_ini_byFileDB();

            //items
            G.excel_convertsettings.Update_items_ini_byFileDB();

            // !! SAVE
            G.NoticeToUser("Saving PSGG FILE ....");
            yield return null;

            if (FileDbUtil.create_psgg())
            {
                G.Dirty_clear_all();//G.bDirty = false;
                G.update_viewform_title();
            }

            //with excel
            if (G.psgg_header_info_save_mode_withexcel)
            {
                G.NoticeToUser("Saving EXCEL FILE ....");
                FileDbUtil.create_excel();
            }

            //Cache 更新
            CreateCache();
        }
#endregion
    }
}
