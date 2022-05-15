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
using System.Reflection;

/*
    dllのdebugのため、
        BRKGS  -- gen source
        BRKGF  -- gen func
        BRKP   -- prepare
    を活用せよ
*/

namespace stateview
{
    public class Converter
    {
        public const bool BRKGS = false; // dllのdebugのため、Breakpoint at generate souce
        public const bool BRKGF = false; // dllのdebugのため、Breakpoint at generate function
        public const bool BRKP  = false; // dllのdebugのため、Breakpoint at prepare 

        #region 準備
        public static dynamic psggConverterLib { get {
            if (!_psggConverterChecked)
            {
                _psggConverterChecked = true;
                try {
                    var converter = SettingIniUtil.GetConverter();
                    var typename1 = Path.GetFileNameWithoutExtension(converter) + ".Convert";
                    var path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),converter);
                    if (!File.Exists(path)) return null;
                    var dll = Assembly.LoadFrom(path);
                    _psggConverter =  Activator.CreateInstance(dll.GetType( typename1 /*"psggConverterLib.Convert"*/));
                    if (_psggConverter == null)
                    {
                        _psggConverter =  Activator.CreateInstance(dll.GetType("psggConverterLib.Convert")); //ファイル名のみの変更対応
                    }

                } catch (SystemException e){
                    G.NoticeToUser_warning("Converter Error : " + e.Message);
                }
            }
            return _psggConverter;
        } }
        private static bool    _psggConverterChecked = false;
        private static dynamic _psggConverter = null;
        #endregion

        #region PSGG Setup
        private static void _psggConverter_setup()
        {
            psggConverterLib.template_src = G.excel_convertsettings.m_template_src;
            psggConverterLib.template_func = G.excel_convertsettings.m_template_func;
            Func<int,int,string> getChartFunc = (r,c)=> {
                return G.excel_program.GetString(r,c);
            };
            psggConverterLib.getChartFunc = getChartFunc;

            G.macro_ini.ReadMacroIni(); //最新を使用
            Func<string,string> getMacroValue = (k) => {
                return G.macro_ini.GetValue(k);
            };
            psggConverterLib.getMacroValueFunc = getMacroValue;

            psggConverterLib.setting_ini = G.excel_convertsettings.m_setting_ini;

            psggConverterLib.name_list      = G.excel_program.GetNameList();
            psggConverterLib.name_row_list  = G.excel_program.GetNameRowList();
            psggConverterLib.state_list     = G.excel_program.GetStateList();
            psggConverterLib.state_col_list = G.excel_program.GetStateColList();

            psggConverterLib.XLSDIR         = G.load_file_dir;
            psggConverterLib.GENDIR         = SettingIniUtil.GetGenDir();

            var incdir                      = SettingIniUtil.GetIncDir();
            //if (string.IsNullOrEmpty(incdir))
            //{ 
            //    incdir = G.load_file_dir;
            //}
            psggConverterLib.INCDIR         = incdir;

            psggConverterLib.MARK_START     = SettingIniUtil.GetCodeOutputStart();
            psggConverterLib.MARK_END       = SettingIniUtil.GetCodeOutputEnd();
            psggConverterLib.TGTFILE        = SettingIniUtil.GetGeneratedSource(); //セッティングより

            var enc = SettingIniUtil.GetSrcEnc();
            psggConverterLib.ENC            = string.IsNullOrEmpty(enc) ? "utf-8" : enc;

            psggConverterLib.STATEMACHINE   = SettingIniUtil.GetStatemachine(); //

            //psggファイルの相対
            var psggfile = G.psgg_file;
            var genfile = G.gen_file;
            var psggrelfile = PathUtil.GetRelativePath(Path.GetDirectoryName(genfile), psggfile);
            psggConverterLib.PSGGFILE = psggrelfile;

            //編集禁止マーク
            psggConverterLib.USE_DONOTEDIT_MARK    = G.option_use_donotedit_mark;
            psggConverterLib.DONOTEDIT_MARK_COLMNS = CsvUtil.ToIntList(G.option_donotedit_mark_columns);
            psggConverterLib.DONOTEDIT_MARK        = G.option_donotedit_mark;

            //分岐コンディション内の改行コード
            psggConverterLib.BRANCHEDIT_NEWLINECHAR = G.branch_special_newlinechar;

        }
        #endregion

        // Function !
        public static string GetFuncSrc(string state)
        {
            if (psggConverterLib==null) return "na";

            psggConverterLib.BRKGF          = BRKGF;  //breakpoint

            try {
                var s = psggConverterLib.CreateFunc(state);
                return s;
            }
            catch (SystemException e)
            {
                return "Error : " + e.Message;
            }
        }

        public static void Convert()
        {
            if (!string.IsNullOrEmpty(G.excel_convertsettings.m_template_src))
            {
                convert_internal();
            }
            else
            {
                convert_w_insertmode();
            }
        }

        private static void convert_external()
        {
			var command = string.Empty;
            if (!string.IsNullOrEmpty(G.external_command))
			{
				command = G.external_command;
				if (command.Contains("%1")) command = command.Replace("%1", G.load_file_name);

                G.NoticeToUser("Executing command : " + G.external_command);
    	        ExecUtil.execute(command, G.load_file_dir);
			}
            else
            {
                G.NoticeToUser_warning("Command does not exist : " + G.external_command);
            }
        }

        // Gen Source 
        private static void convert_internal()
        {
            if (psggConverterLib==null)
            {
                G.NoticeToUser_warning(G.Localize("w_internalcoverterdoesnotexit") /*"the internal converter does not exist"*/);
                return;
            }

            _psggConverter_setup();

            psggConverterLib.BRKGS          = BRKGS; //breakpoint

            var excel  = G.load_file_name;
            var gendir = SettingIniUtil.GetGenDir();

            G.NoticeToUser("Executing converter : " + SettingIniUtil.GetConverter());

            try {
                psggConverterLib.GenerateSource(excel,gendir);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Internal Converter Error : " + ExceptionUtil.GetDetail(e));
            }
        }
        static void convert_w_insertmode()
        {
            if (psggConverterLib == null)
            {
                G.NoticeToUser_warning(G.Localize("w_internalcoverterdoesnotexit")/*"the internal converter does not exist"*/);
                return;
            }

            _psggConverter_setup();

            psggConverterLib.BRKGS = BRKGS; //breakpoint

            var excel = G.load_file_name;
            var gendir = SettingIniUtil.GetGenDir();

            G.NoticeToUser("Executing converter : " + SettingIniUtil.GetConverter());

            try
            {
                psggConverterLib.InsertOutputToFile(excel, SettingIniUtil.GetGeneratedSource(), gendir);
            }
            catch (SystemException e)
            {
                G.NoticeToUser_warning("Internal Converter Error : " + ExceptionUtil.GetDetail(e));
            }
        }
        public static string GetGeneratedSource()
        {
            try {
            string enc    = psggConverterLib.ENC;
            string file = GetGeneratedSourceFileName();
            var text = File.ReadAllText(file,Encoding.GetEncoding(enc));
            return text;
            } catch (SystemException  e)
            {
                return "GetGeneratedSource Unexpected! {176391BA-BC1D-475F-9F5A-5F4477A5DF66}," + ExceptionUtil.GetDetail(e);
            }
        }

        public static void Prepare()
        {
            if (psggConverterLib==null)
            {
                G.NoticeToUser_warning(G.Localize("w_preparedosenotexist")/*"the prepare does not exist"*/);
                return;
            }

            _psggConverter_setup();

            //psggConverterLib.template_src = G.excel_convertsettings.m_template_src;
            //psggConverterLib.template_func = G.excel_convertsettings.m_template_func;
            //Func<int,int,string> getChartFunc = (r,c)=> {
            //    return G.excel_program.GetString(r,c);
            //};
            //psggConverterLib.getChartFunc = getChartFunc;

            //G.macro_ini.ReadMacroIni(); //最新を使用
            //Func<string,string> getMacroValue = (k) => {
            //    return G.macro_ini.GetValue(k);
            //};
            //psggConverterLib.getMacroValueFunc = getMacroValue;

            //psggConverterLib.name_list      = G.excel_program.GetNameList();
            //psggConverterLib.name_row_list  = G.excel_program.GetNameRowList();
            //psggConverterLib.state_list     = G.excel_program.GetStateList();
            //psggConverterLib.state_col_list = G.excel_program.GetStateColList();

            //var incdir                      = SettingIniUtil.GetIncDir();
            //if (string.IsNullOrEmpty(incdir))
            //{ 
            //    incdir = G.load_file_dir;
            //}
            //psggConverterLib.INCDIR         = incdir;

            psggConverterLib.BRKP           = BRKP; //breakpoint

#if DEBUG
            //G.NoticeToUser("Preparating converter : " + SettingIniUtil.GetConverter());
#endif   
            try {
                psggConverterLib.Prepare();
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Prepare Error : " + ExceptionUtil.GetDetail(e) );
            }
        }
        public static string GetGeneratedSourceFileName()
        {
            var file = psggConverterLib.OUTPUT;
            if (string.IsNullOrEmpty(file))
            {
                file = SettingIniUtil.GetGeneratedSource();
            }
            return Path.Combine(SettingIniUtil.GetGenDir(),file);
        }
        /// <summary>
        /// フィルターテンプレートを作成するためのテンプレート上書き。コンバートの前に呼び出す
        /// </summary>
        public static void change_temp_func(string template)
        {
            psggConverterLib.template_func = template;
        }
    }
}
