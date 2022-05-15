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

//便利クラス
public partial class FileDbUtil
{
    #region バージョン確認
    public static bool is_psggfile_ver1_0()
    {
        try {
            var vers = G.psgg_header_info_version;
            var fver = ParseUtil.ParseFloat(vers,-1);
            if (fver>=0 && fver <= 1) return true; //古い
         }
        catch (SystemException e)
        {
            G.NoticeToUser_warning("{7D4FEB05-7751-4450-8CC7-FE80F47FDC1A} " + e.Message);
        }
        return false;
    }
    public static bool is_psggfile_ver1_1()
    {
        try {
            var vers = G.psgg_header_info_version;
            if (vers == "1.1") return true;
         }
        catch (SystemException e)
        {
            G.NoticeToUser_warning("{390C26D1-98B5-404C-9E73-BF3DF916CFB0} " + e.Message);
        }
        return false;
    }
    public static bool is_psggfile_readfrom_excel()
    {
        //var val = G.psgg_header_info_read_from;
        //return (!string.IsNullOrEmpty(val) &&  val.ToLower().Contains("excel"));
        return G.psgg_header_info_read_from_excel;
    }
    public static bool is_psggfile_savemode_withexcel()
    {
        var val = G.psgg_header_info_save_mode;
        return (!string.IsNullOrEmpty(val) &&  val.ToLower().Contains("excel"));
    }
    public static bool is_psggfile_check_excel_writable()
    {
        var val = G.psgg_header_info_check_excel_writable;
        return (val == "yes");
    }
    public static void read_psgg_header_info()
    {
        string version;
        string file;
        string guid;
        string readfrom;
        string savemode;
        string check_excel_writable;

        var b = read_psgg_header_info(
            G.psgg_file,
            out version,
            out file,
            out guid,
            out readfrom,
            out savemode,
            out check_excel_writable
            );
        if (b)
        {
            G.psgg_header_info_version   = version;
            G.psgg_header_info_file      = file;
            G.psgg_header_info_guid      = guid;
            var psgg_header_info_read_from = readfrom;
            G.psgg_header_info_save_mode = savemode;
            G.psgg_header_info_check_excel_writable = check_excel_writable;
            
            G.psgg_header_info_read_from_excel =  !string.IsNullOrEmpty(psgg_header_info_read_from) &&  psgg_header_info_read_from.ToLower() == WordStorage.Store.excel;
            G.psgg_header_info_check_excel_writable_yes = G.psgg_header_info_check_excel_writable == "yes";
            G.psgg_header_info_save_mode_withexcel = is_psggfile_savemode_withexcel();
        }
    }
    public static bool read_psgg_header_info(
        string psggfile, 
        out string version,
        out string file,
        out string guid,
        out string readfrom,
        out string savemode,
        out string check_excel_writable
        )
    {
        version = null;
        file = null;
        guid = null;
        readfrom = null;
        savemode = null;
        check_excel_writable = null;

        try
        {
            var text = get_psgg_file_text_header(psggfile);
            if (string.IsNullOrEmpty(text)) return false;
            var ht = IniUtil.CreateHashtable(text);
            if (ht == null) return false;

            version  = IniUtil.GetValueFromHashtable("version",ht);
            file     = IniUtil.GetValueFromHashtable("file",ht);
            guid     = IniUtil.GetValueFromHashtable("guid",ht);
            readfrom = IniUtil.GetValueFromHashtable("read_from",ht);
            savemode = IniUtil.GetValueFromHashtable("save_mode",ht);
            check_excel_writable = IniUtil.GetValueFromHashtable("check_excel_writable",ht);

            return true;

        } catch (SystemException e)
        {
            G.NoticeToUser_warning("{F0FC890B-1EB3-4614-83D3-65684F8A33A2} " + e.Message);
        }
        return false;
    }

    public static string read_version_from_psgg()
    {
        try { 
            var text = get_psgg_file_text_header();
            if (string.IsNullOrEmpty(text)) return null;
            var ht = IniUtil.CreateHashtable(text);
            if (ht == null) return null;

            return IniUtil.GetValueFromHashtable("version",ht);
        }
        catch (SystemException e)
        {
            G.NoticeToUser_warning("{94224219-AEC6-4594-9223-0CE67512FC10} " + e.Message);
        }
        return null;
    }
    public static void set_psgg_header_info(string xlsfile,string guid, bool readfrom_excel_or_psgg, bool savemode_withexcel_or_psggonly, bool check_excel_writable )
    {
        // ref make_psgg_headerinfo_text
        G.psgg_header_info_file                 = Path.GetFileName(xlsfile);
        G.psgg_header_info_guid                 = guid;

        G.psgg_header_info_read_from_excel      = readfrom_excel_or_psgg;

        G.psgg_header_info_save_mode_withexcel  = savemode_withexcel_or_psggonly;
        G.psgg_header_info_check_excel_writable =  check_excel_writable ? "yes" : "no";
    }

    private static string get_psgg_file_text_header()
    {
        return  get_psgg_file_text_header(G.psgg_file);
        //var text = File.ReadAllText(G.psgg_file,Encoding.UTF8);
        //var index = text.IndexOf(StateViewer_filedb.FileDb.MARK_STATECHART_SHEET);
        //if (index > 0)
        //{
        //    text = text.Substring(0,index);
        //}
        //return text;
    }
    private static string get_psgg_file_text_header(string file)
    {
        var text = File.ReadAllText(file,Encoding.UTF8);
        var index = text.IndexOf(StateViewer_filedb.FileDb.MARK_STATECHART_SHEET);
        if (index > 0)
        {
            text = text.Substring(0,index);
        }
        return text;
    }
    #endregion



    #region

    public static void read_psgg()
    {
        G.file_db = new StateViewer_filedb.FileDb();
        G.file_db.ReadPsgg(G.psgg_file, G.userenv_guid);
    }
    public static void read_excel(string file=null)
    {
        if (file == null) file = G.load_file;

        G.file_db = new StateViewer_filedb.FileDb();
        G.file_db.LoadExcel(file, G.userenv_guid);
    }
    public static void read_excel_force(string file)
    {
        ExcelDll.Enabled = true;
        read_excel(file);
        ExcelDll.Enabled = false;
    }
    public static void write_internal_files()
    {
        G.file_db.write_filedb_all();
    }
    public static int max_row()
    {
        return G.file_db.m_state_chart.getMaxRow();             
    }
    public static int max_col()
    {
        return G.file_db.m_state_chart.getMaxCol();
    }
    public static string get_val(int row, int col)
    {
        return G.file_db.m_state_chart.getVal(row,col);
    }
    public static bool set_val(int row, int col, string val)
    {
        return G.file_db.m_state_chart.setVal(row,col,val);
    }
    public static int get_row_by_name(string name)
    {
        return G.file_db.m_state_chart.getRowByName(name);
    }
    public static Bitmap get_bmp_by_hash(string hash)
    {
        return DictionaryUtil.Get(G.file_db.m_state_chart.hash_bmp_dic,hash);   
    }
    public static string get_name_by_row(int row)
    {
        return G.file_db.m_state_chart.getNameByRow(row);
    }
    #endregion

    #region FileDB ファイル更新  FileはSaveの高速化のため
    public static string get_sheet_config_val()   { return G.file_db.m_sheet_config_val;            } public static void set_sheet_config_val(string s)   { G.file_db.m_sheet_config_val=s;             } public static void write_sheet_config()  { G.file_db.write_filedb_config_sheet();  }
    public static string get_sheet_tempsrc_val()  { return G.file_db.m_sheet_template_source_val;   } public static void set_sheet_tempsrc_val(string s)  { G.file_db.m_sheet_template_source_val = s;  } public static void write_sheet_tempsrc() { G.file_db.write_filedb_tempsrc_sheet(); }
    public static string get_sheet_tempfunc_val() { return G.file_db.m_sheet_template_func_val;     } public static void set_sheet_tempfunc_val(string s) { G.file_db.m_sheet_template_func_val=s;      } public static void write_sheet_tempfunc(){ G.file_db.write_filedb_tempfunc_sheet();}
    public static string get_sheet_setting_val()  { return G.file_db.m_sheet_setting_ini_val;       } public static void set_sheet_setting_val(string s)  { G.file_db.m_sheet_setting_ini_val = s;      } public static void write_sheet_setting() { G.file_db.write_filedb_setting_sheet(); }
    public static string get_sheet_help_val()     { return G.file_db.m_sheet_help_val;              } public static void set_sheet_help_val(string s)     { G.file_db.m_sheet_help_val = s;             } public static void write_sheet_help()    { G.file_db.write_filedb_help_sheet();    }
    public static string get_sheet_items_val()    { return G.file_db.m_sheet_items_val;             } public static void set_sheet_items_val(string s)    { G.file_db.m_sheet_items_val = s;            } public static void write_sheet_items()   { G.file_db.write_filedb_items_sheet();   }
    
    public static void set_hash_bitmap_dic(Dictionary<string,Bitmap> dic)
    {
        G.file_db.m_state_chart.SetHashBmpDic(dic);        
    }

    public static void write_filedb_manager_and_dirty_statedata_only()
    {
        G.file_db.write_filedb_manager_and_dirty_statedata_only();
    }
    public static void write_filedb_bmp_files()
    {
        G.file_db.write_filedb_bmp_files();
    }
    public static void write_filedb_all_files()
    {
        G.file_db.write_filedb_all();
    }
    #endregion

    #region psgg ファイル作成
    public static bool create_psgg()  { return G.file_db.CreatePsgg(G.psgg_file); }
    public static void create_excel_ifneeded() {
        if (G.psgg_header_info_save_mode_withexcel) create_excel();
    }
    public static void create_excel() {
        if (!G.psgg_file_w_data) throw new SystemException("{61EA638E-4485-40E8-A4ED-C74697DBDB89}");
        ExcelDll.Enabled = true;  //ここだけ許可
        { 
            G.file_db.SaveExcel(G.load_file);
        }
        ExcelDll.Enabled = false;
    }
    #endregion

    #region 
    public static void rename_name_by_row(int row, string name)
    {
        G.file_db.m_state_chart.RenameNAME(row,name);
    }
    public static void remove_row(int row)
    {
        G.file_db.m_state_chart.RemoveRow(row);
    }
    public static void insert_row(int row)
    {
        G.file_db.m_state_chart.InsertRow(row);
    }
    public static void copy_row_empty_src(int srcrow, int dstrow) //コピー後、ソース行はカラに。
    {
        G.file_db.m_state_chart.CopyRowAndEmptySrc(srcrow, dstrow);
    }
    #endregion

    #region ExcelSaveOneSheet の置き換え
    public static void WriteTemplateSource()
    {
        set_sheet_tempsrc_val(G.excel_convertsettings.m_template_src); //FileDB内格納
        write_sheet_tempsrc();                                         //FileDb内部ファイル書き出し(create_psgg高速化のため)
        create_psgg();                                                 //psgg作成
        create_excel_ifneeded();
    }
    public static void WriteTemplateFunction()
    {
        set_sheet_tempfunc_val(G.excel_convertsettings.m_template_func);  //FileDB内格納
        write_sheet_tempfunc();                                           //FileDb内部ファイル書き出し(create_psgg高速化のため)
        create_psgg();                                                    //psgg作成
        create_excel_ifneeded();
    }
    public static void WriteSettings()
    {
        set_sheet_setting_val(G.excel_convertsettings.m_setting_ini);    //FileDB内格納
        write_sheet_setting();                                           //FileDb内部ファイル書き出し(create_psgg高速化のため)
        create_psgg();                                                   //psgg作成
        create_excel_ifneeded();
    }
    public static void WriteHelp()
    {
        set_sheet_help_val(G.excel_convertsettings.m_help_ini);    //FileDB内格納     
        write_sheet_help();                                        //FileDb内部ファイル書き出し(create_psgg高速化のため)
        create_psgg();                                             //psgg作成
        create_excel_ifneeded();
    }
    public static void WriteItemsInfo()
    {
        set_sheet_items_val(G.excel_convertsettings.m_items_ini); //FileDB内格納     
        write_sheet_items();                                      //FileDb内部ファイル書き出し(create_psgg高速化のため)
        create_psgg();                                            //psgg作成
        create_excel_ifneeded();
    }
    #endregion
    #region Bitmap
    public static string GetBitmapString(string hash)
    {
        return G.file_db.m_state_chart.GetBitmapString(hash);
    }
    public static void RegisterFieDbBitmap(string hash, string data)
    {
        G.file_db.m_state_chart.RegisterBitmap(hash,data);
    }
    public static void write_filedb_specified_bmp_files(List<string> hashlist)
    {
        G.file_db.write_filedb_specified_bmp_files(hashlist);
    }
    public static void delete_unused_bmp() //管理リストからの削除
    {
        G.file_db.m_state_chart.RemoveUnusedBitmap();
    }
    #endregion

    #region setting等に与える一時パス
    public static string GetTempPath()
    {
        return G.file_db.GetTempFolder();
    }
    #endregion
}

