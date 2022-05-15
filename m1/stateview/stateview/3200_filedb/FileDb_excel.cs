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


namespace StateViewer_filedb
{
    public partial class FileDb
    {
        /* See エクセル消滅作戦.pptx
        セーブエクセル・ロードエクセル　　　　　　    －－エクセルファイルに対して
        ライトFileDB・リードFileDB　－－内部FileDBに対して


        ■ エクセルロード・セーブ
          1. LoadExcel(エクセルファイル)  -- 現フォーマットを固定とする。 
          2. SaveExcel(エクセルファイル)  -- 既存も、新規も
　　　　*/


        // (1,1)に格納された各シート内容
        public string  m_sheet_config_val;
        public string  m_sheet_template_source_val;
        public string  m_sheet_template_func_val;
        public string  m_sheet_setting_ini_val;
        public string  m_sheet_help_val;
        public string  m_sheet_items_val;

        // state chart sheetにあたるもの
        public state_chart    m_state_chart { get; private set;}

        #region LOAD SAVE EXCEL
        public void LoadExcel(string excelfile,string guid)
        {
            if (!File.Exists(excelfile))
            {
                G.NoticeToUser_warning("File Not Found : " + excelfile);
                return;
            }

            initdb_folders(guid);

            m_sheet_config_val            = loadsheet_1_1(excelfile, G.sheetconfig);
            m_sheet_template_source_val   = loadsheet_1_1(excelfile, G.sheettempsrc);
            m_sheet_template_func_val= loadsheet_1_1(excelfile, G.sheettempfunc);
            m_sheet_setting_ini_val       = loadsheet_1_1(excelfile, G.sheetsetting);
            m_sheet_help_val              = loadsheet_1_1(excelfile, G.sheethelp);
            m_sheet_items_val             = loadsheet_1_1(excelfile, G.sheetitems);

            m_state_chart = new state_chart();
            m_state_chart.InitExcel( excelfile );

            //write_filedb_all();

            //SaveExcel(); //実験
        }
        private string loadsheet_1_1(string excelfile, string sheetname)
        {
            var s = string.Empty;
            var ed = new ExcelDll();
            ed.Load(excelfile);
            
            if (ed.SetSheet(sheetname))
            {
                s = ed.GetStr(1,1);
                if (s==null) return string.Empty;
            }
            ed.Dispose();
            ed = null;

            return StringUtil.ConvertNewLineChar(s,Environment.NewLine);
        }

        public void SaveExcel(string excelfile)
        {
            //var excelfile = G.load_file;   //Path.Combine(m_temp_folder,"temp.xlsx");
            m_state_chart.create_excelfile( excelfile );

            SaveSheet(excelfile,G.sheetconfig,   m_sheet_config_val);
            SaveSheet(excelfile,G.sheettempsrc,  m_sheet_template_source_val);
            SaveSheet(excelfile,G.sheettempfunc, m_sheet_template_func_val);
            SaveSheet(excelfile,G.sheetsetting,  m_sheet_setting_ini_val);
            SaveSheet(excelfile,G.sheethelp,     m_sheet_help_val);
            SaveSheet(excelfile,G.sheetitems,    m_sheet_items_val);
            
        }
        private void SaveSheet(string excelfile,string sheetname, string val)
        {
            if (string.IsNullOrEmpty(val)) return;

            var newval = StringUtil.ConvertNewLineForExcel(val);
            var ed = new ExcelDll();
            ed.Load(excelfile);
            if (!ed.SetSheet(sheetname))
            {
                ed.NewSheetForce(sheetname);
            }
            ed.SetSheet(sheetname);
            ed.SetStr(1,1,newval);

            ed.SetSheet(G.sheetchart); //フォーカスはこれ

            ed.Save();
            ed.Dispose();
            ed = null;
        }

        private void initdb_folders(string guid)
        {
            m_guid = guid;

            if (Directory.Exists(m_dbroot))
            {
                var b = false;
                try { 
                    Directory.Delete(m_dbroot,true);
                    b =  true;
                } catch {
                    b =false;
                }
                if (!b)
                {
                    FileUtil.DeleteAllFiles(m_dbroot);
                }
            }
            Directory.CreateDirectory(m_dbroot);

            Directory.CreateDirectory(m_statechart_folder);
            Directory.CreateDirectory(m_statechart_manager_folder);
            Directory.CreateDirectory(m_statechart_data_folder);
            Directory.CreateDirectory(m_statechart_bmp_folder);

            Directory.CreateDirectory(m_various_folder);
            Directory.CreateDirectory(m_temp_folder);
        }
        #endregion
    }
}


