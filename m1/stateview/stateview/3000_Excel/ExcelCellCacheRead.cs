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
using System.Runtime.InteropServices;

namespace stateview
{
    internal partial class ExcelCellCacheRead
    {
        string sheetname     { get { return G.sheetchart;    } }
        [Obsolete]
        string sheetlayout   { get { return G.sheetlayout;   } }

        string sheetconfig   { get { return G.sheetconfig;   } }
        string sheettempsrc  { get { return G.sheettempsrc;  } }
        string sheettempfunc { get { return G.sheettempfunc; } }
        string sheetsetting  { get { return G.sheetsetting;  } }
        string sheethelp     { get { return G.sheethelp;     } }
        string sheetitems    { get { return G.sheetitems;    } }

        int    NAME_COL      { get { return G.NAME_COL;    } }
        int    STATE_ROW     { get { return G.STATE_ROW;   } }

        private Hashtable GetValidRowAndRead(ExcelDll ed) //項目欄col=2を走査して、有効なRowのみをハッシュテーブルに格納する 且つ キャッシュに格納
        {
            var tbl = new Hashtable();
            // range内の冒頭行をカラム方向に走査して、col==NAME_COLにあたるindexを求める
            var name_col_index=1;
            for(;name_col_index<=ed.MaxCol();name_col_index++)
            {
                var cd = readcell(ed,1,name_col_index);
                if (cd!=null && cd.col == NAME_COL) break;
            }

            for(var row_index = 1; row_index<=ed.MaxRow() ;row_index++)
            {
                var cd = readcell(ed,row_index,name_col_index);
                if (cd==null) continue;
                if (!string.IsNullOrWhiteSpace(cd.text))
                {
                    tbl[cd.row] = cd;
                }
            }
            return tbl;
        }
        private Hashtable GetValidColAndRead(ExcelDll ed) //state行row=2を走査して、有効なColのみをハッシュテーブルに格納する
        {
            var tbl = new Hashtable();

            // range内の冒頭カラムを行方向に走査して、row==SATTE_ROWにあたるindexを求める
            var state_row_index = G.STATE_ROW;
            //for(;state_row_index<=ed.MaxRow();state_row_index++)
            //{
            //    var cd = readcell(ed,state_row_index,1);
            //    if (cd!=null && cd.row == STATE_ROW) break;
            //}

            //rangeのstate_row_index行をカラム方向に走査
            for(var col_index = 1; col_index <= ed.MaxCol() ; col_index++)
            {
                var cd = readcell(ed,state_row_index,col_index);
                if (cd==null) continue;
                if (!string.IsNullOrWhiteSpace(cd.text))
                {
                    tbl[cd.col] = cd;
                }
            }
            return tbl;
        }

        private ExcelCellCacheItem readcell(ExcelDll ed, int row_index,int col_index)
        {
            //var firstcell = wd.GetStr(1,1];
            var row = row_index;
            var col = col_index;
            var key = ExcelCellCacheItem.make_key(row,col);

            if (G.m_excel_cell_cache_dic.ContainsKey(key))
            {
                return G.m_excel_cell_cache_dic[key];
            }

            var str = ed.GetStr(row_index, col_index);
            if (string.IsNullOrEmpty(str))
            {
                str = string.Empty;
            }
            var cd = new ExcelCellCacheItem();

            cd.row  = row_index;
            cd.col  = col_index;
            cd.text = str;

            //if (cell.Value2!=null)
            //{
            //    cd.text = cell.Value2.ToString();
            //}
            //if (!string.IsNullOrEmpty(cd.text))
            //{
            //    //cd.fontname = (string)cell.Font.Name;
            //    //cd.fontsize = (float)cell.Font.Size;
            //    //cd.fontcolor = ColorTranslator.FromOle((int)cell.Font.Color);

            //    //cd.width  = (float)cell.Width;
            //    //cd.height = (float)cell.Height;
            //    //cd.bgcolor = ColorTranslator.FromOle((int)cell.Interior.Color);
            //}

            key = cd.key;
            G.m_excel_cell_cache_dic.Add(key,cd);

            //Marshal.ReleaseComObject(cell);
            //cell=null;

            return cd;
        }

        internal IEnumerator ReadCellsAndBmps_CO()
        {
            var file = G.load_file;

            //G.status = "Start Read Excel ";
            G.NoticeToUser("Start to read Excel");
            yield return null;

            G.m_excel_cell_cache_dic = new Dictionary<int, ExcelCellCacheItem>();

            if (!File.Exists(file))
            {
                G.NoticeToUser_warning(G.Localize("w_filenotfoundcache") /* "File not found : "*/ + file );
                yield break;
            }

            var ed =  new ExcelDll(); // new ExcelWork();
            if (!ed.Load(file))
            {
                G.NoticeToUser_warning("Unexpected!");
                yield break;
            }
            ed.SetSheet(sheetname);
            //var sheet = wk.GetSheet();

            //if (sheet == null)
            //{
            //    wk.Dispose();
            //    yield break;
            //}

            //var range = sheet.UsedRange;

            //var row_start = range.Row;
            var row_len   = ed.MaxRow();
            //var col_start = range.Column;
            var col_len   = ed.MaxCol();

            G.m_excel_max_row = ed.MaxRow();//row_start + row_len;
            G.m_excel_max_col = ed.MaxCol();//col_start + col_len;

            //G.status = "Check Valid Row ";
            G.NoticeToUser("Checking valid rows");
            yield return null;
            Hashtable valid_row = GetValidRowAndRead(ed);

            //G.status = "Check Valid Col ";
            G.NoticeToUser("Checking valid columns");
            yield return null;
            Hashtable valid_col = GetValidColAndRead(ed);

            if(false) //if (G.excel_read_optimized) //ヘッダ項目のみセルを読み込み、他はテキストのみとする。　セル自体は項目部分をクローン。文字情報のみステート列から取得する。
            {

                //Object[,] objs = null;
                //try {
                //    objs = (Object[,])range.Value2;
                //}
                //catch {
                //}

                //for (var row_index = 1; row_index <= row_len ; row_index++)
                //{
                //    var row = row_index;
                //    if (!valid_row.ContainsKey(row)) continue;
                //    var ref_row_cell = (ExcelCellCacheItem)valid_row[row];

                //    //G.status = "Read Excel Row : " + row_index + "/" + row_len;
                //    G.NoticeToUser("Reading excel row : " + row_index + " of " + (row_len-1));
                //    yield return null;

                //    for(var col_index = 1; col_index <= col_len ; col_index++)
                //    {
                //        var col = col_index;
                //        if (!valid_col.ContainsKey(col)) continue;
                //        var ref_col_cell = (ExcelCellCacheItem)valid_col[col];

                //        //Object obj = null;
                //        //try {
                //        //    obj = objs[row_index + 1,col_index + 1]; // index base 1
                //        //}
                //        //catch { }
                //        //if (obj == null) continue;

                //        var text = ed.GetStr(row,col);
                //        //if (text==null) text = "?";

                //        var cd = ref_row_cell.Clone(); // nameの行のセルをクローン
                //        cd.row  = row;
                //        cd.col  = col;
                //        cd.text = null;
                //        if (!G.m_excel_cell_cache_dic.ContainsKey(cd.key))
                //        {
                //            //cd.fontname  = ref_col_cell.fontname; //ステート列の文字要素のみ上書き
                //            //cd.fontcolor = ref_col_cell.fontcolor;
                //            //cd.fontsize  = ref_col_cell.fontsize;
                //            cd.text      = text;

                //            G.m_excel_cell_cache_dic.Add(cd.key,cd);
                //        }
                //    }
                //}
            }
            else
            {
                for (var row_index = 1; row_index <= row_len ; row_index++)
                {
                    //if (valid_row!=null && !valid_row.ContainsKey(row_index)) continue;

                    //G.status = "Read Excel Row : " + (row_index + 1) + "/" + row_len;
                    G.NoticeToUser("Reading excel row : " + row_index + " of " + row_len);

                    //yield return null;

                    for(var col_index = 1; col_index <= col_len ; col_index++)
                    {
                        readcell(ed,row_index,col_index);
                    }
                }
            }
            //Marshal.ReleaseComObject(range);
            //range = null;

            //G.status = "Read Pictures ";
            G.NoticeToUser("Reading pictures.");
            yield return null;
            G.excel_pictures = new ExcelPictures();
            G.excel_pictures.Init(ed);

            ed.Dispose();
            ed = null;

            ed = new ExcelDll();
            ed.Load(file);

            // -- -- --
            // layoutシートのみ
            // -- -- --

            //G.NoticeToUser("Reading layout sheet -obs");
            //yield return null;

            //wk.SetSheet(sheetlayout);
            //sheet = wk.GetSheet();

            //G.excel_layout = new ExcelLayout();
            //G.excel_layout.Init(sheet);

            G.NoticeToUser("Reading config sheet");
            yield return null;

            var bOKconfigsheet = ed.SetSheet(sheetconfig);

            G.excel_config = new ExcelConfig();
            if (bOKconfigsheet) {
                G.excel_config.Init(ed);
            }
            else
            {
                G.NoticeToUser("Skipped reading config sheet.");
            }

            G.NoticeToUser("Reading template source sheet");
            yield return null;

            G.excel_convertsettings = new ExcelConvertSettings();

            //if (SettingIniUtil.GetTemplateSrc()!=null)
            {
                if (ed.SetSheet(sheettempsrc))
                {
                    if (ed.GetError()==null)
                    {
                        //sheet = ed.GetSheet();
                        G.excel_convertsettings.Init_template_src(ed);
                    }
                }
            }
            G.NoticeToUser("Reading template func sheet");
            yield return null;

            ed.SetSheet(sheettempfunc);
            G.excel_convertsettings.Init_template_func(ed);

            G.NoticeToUser("Reading setting sheet");
            yield return null;

            ed.SetSheet(sheetsetting);
            G.excel_convertsettings.Init_setting_ini(ed);

            ed.SetSheet(sheethelp);
            G.excel_convertsettings.Init_help_ini(ed);

            if (ed.SetSheet(sheetitems))
            {
                G.excel_convertsettings.Init_items_ini(ed);
            }
            else
            {
                G.excel_convertsettings.Init_items_ini(null);
            }


            ed.Dispose();
            ed = null;
        }
        /*
            新PSGG FILE DATA用
            ※ 互換性を持たせる。
        */
        internal void ReadCellsAndBmpFromPsggFile()
        {
            FileDbUtil.read_psgg();
            FileDbUtil.write_internal_files();

            var fdb = G.file_db;

            G.m_excel_cell_cache_dic = new Dictionary<int, ExcelCellCacheItem>();
            
            int row_len = G.m_excel_max_row = FileDbUtil.max_row();
            int col_len = G.m_excel_max_col = FileDbUtil.max_col();

            for(var row_index= 1; row_index <= row_len; row_index++ )
            {
                for(var col_index = 1; col_index <= col_len; col_index++)
                {
                    var key = ExcelCellCacheItem.make_key(row_index,col_index);
                    var val = FileDbUtil.get_val(row_index, col_index);
                    if (string.IsNullOrEmpty(val)) continue;

                    var cd = new ExcelCellCacheItem();
                    cd.row  = row_index;
                    cd.col  = col_index;
                    cd.text = val;

                    DictionaryUtil.Set( G.m_excel_cell_cache_dic, key,cd); 
                }
            }

            G.excel_pictures = new ExcelPictures();
            G.excel_pictures.Init_by_filedb();

            var bOKconfigsheet = !string.IsNullOrEmpty(FileDbUtil.get_sheet_config_val() );

            G.excel_config = new ExcelConfig();
            if (bOKconfigsheet)
            {
                G.excel_config.Init_byFileDB();    
            }
            else
            {
                G.NoticeToUser("Skipped reading config sheet.");
            }

            G.excel_convertsettings = new ExcelConvertSettings();
            G.excel_convertsettings.Init_template_src_byFileDB();
            G.excel_convertsettings.Init_template_func_byFileDB();
            G.excel_convertsettings.Init_setting_ini_byFileDB();
            G.excel_convertsettings.Init_help_ini_byFileDB();
            G.excel_convertsettings.Init_items_ini_byFileDB();
        }

        internal IEnumerator ReadCellsAndBmpFromPsggFile_co(bool bForceExcel=false)
        {
            if (bForceExcel)
            {
                G.NoticeToUser("Start to read Excel by FileDB");
                yield return null;

                ExcelDll.Enabled = true; //特別許可
                { 
                    FileDbUtil.read_excel();
                }
                ExcelDll.Enabled = false;
            }
            else
            { 
                G.NoticeToUser("Start to read PSGG");
                yield return null;

                FileDbUtil.read_psgg();
            }

            G.NoticeToUser("Start to write internal files");
            yield return null;

            // async write internal files
            var task = Task.Run( ()=> { 
                FileDbUtil.write_internal_files();
            });

            var n = 0;
            while(true)
            {
                n++;
                if (n % 2==0) G.NoticeToUser_woNewLine("....");

                if (task.IsCanceled || task.IsCompleted || task.IsFaulted) break;
                yield return null;
            }
            G.NoticeToUser(".");

            var fdb = G.file_db;

            G.m_excel_cell_cache_dic = new Dictionary<int, ExcelCellCacheItem>();
            
            int row_len = G.m_excel_max_row = FileDbUtil.max_row();
            int col_len = G.m_excel_max_col = FileDbUtil.max_col();

            for(var row_index= 1; row_index <= row_len; row_index++ )
            {
                for(var col_index = 1; col_index <= col_len; col_index++)
                {
                    var key = ExcelCellCacheItem.make_key(row_index,col_index);
                    var val = FileDbUtil.get_val(row_index, col_index);
                    if (string.IsNullOrEmpty(val)) continue;

                    var cd = new ExcelCellCacheItem();
                    cd.row  = row_index;
                    cd.col  = col_index;
                    cd.text = val;

                    DictionaryUtil.Set( G.m_excel_cell_cache_dic, key,cd); 
                }
            }

            G.NoticeToUser("Reading pictures.");
            yield return null;

            G.excel_pictures = new ExcelPictures();
            G.excel_pictures.Init_by_filedb();

            var bOKconfigsheet = !string.IsNullOrEmpty(FileDbUtil.get_sheet_config_val() );

            G.excel_config = new ExcelConfig();
            if (bOKconfigsheet)
            {
                G.excel_config.Init_byFileDB();    
            }
            else
            {
                G.NoticeToUser("Skipped reading config sheet.");
            }

            G.excel_convertsettings = new ExcelConvertSettings();
            G.excel_convertsettings.Init_template_src_byFileDB();
            G.excel_convertsettings.Init_template_func_byFileDB();
            G.excel_convertsettings.Init_setting_ini_byFileDB();
            G.excel_convertsettings.Init_help_ini_byFileDB();
            G.excel_convertsettings.Init_items_ini_byFileDB();
        }

        //internal IEnumerator ReadCellsAndBmpFromExcel_toFileDb_co()
        //{
        //    if (!G.psgg_file_w_data) throw new SystemException("{E9C47487-9B28-4EBC-B428-9B293960153F}");
        //    G.NoticeToUser("Start to read Excel for FileDb");
        //    yield return null;

        //    FileDbUtil.read_excel();

        //}
    }
}
