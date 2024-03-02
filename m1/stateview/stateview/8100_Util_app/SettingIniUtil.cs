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
using stateview;

namespace stateview
{
    /// <summary>
    /// Setup時に作成される Setting.iniから情報を取得する。
    /// </summary>
    class SettingIniUtil
    {
        //public static string GetTemplateFunc()
        //{
        //    var file = get_setting("templatefunc");
        //    if (string.IsNullOrEmpty(file))   return null;
        //    return Path.Combine(G.load_file_dir,file);
        //}
        public static string GetTemplateSrc()
        {
            var file = get_setting(WordStorage.Store.settingini_setting_template_src/*"templatesource"*/);
            return file;
        }
        public static string GetGeneratedSource()
        {
            var file = get_setting(WordStorage.Store.settingini_setting_gensrc/* "gensrc" */);
            if (string.IsNullOrEmpty(file)) return null;
			var dir = GetGenDir();
			if (string.IsNullOrEmpty(dir)) return null;
			return Path.Combine(dir,file);
        }
        public static string GetGeneratedHpp()
        {
            var file = get_setting(WordStorage.Store.settingini_setting_genhpp/* "genhpp" */);
            if (string.IsNullOrEmpty(file)) return null;
            var dir = GetGenDir();
            if (string.IsNullOrEmpty(dir)) return null;
            return Path.Combine(dir, file);
        }
        public static void CopyGeneratedSourceToClipboard()
        {
            try {
                var text = Converter.GetGeneratedSource();
                Clipboard.SetText(text);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected! {9EB471F0-029B-4925-9437-3A0ED24A2485}" + e.Message);
            }
        }
        /// <summary>
        /// sub_src in setting.ini
        /// </summary>
        public static string GetSourceForImplementing()
        {
            var src = get_subsrc();
            return src;
            //if (src!=null) return src;
            //return get_macroini();
        }
        public static string GetMacroIni()
        {
            var src = get_macroini();
            return src;
        }
        private static string get_subsrc()
        {
            var file = get_setting(WordStorage.Store.settingini_setting_sub_src/* "subsrc" */);
            if(string.IsNullOrEmpty(file))
                return null;
            var dir = GetGenDir();
            if(string.IsNullOrEmpty(dir))
                return null;
            return Path.Combine(dir,file);
        }

        private static string get_macroini()
        {
            var file = get_setting(WordStorage.Store.settingini_setting_macroini/* "macro_ini" */);
            if(string.IsNullOrEmpty(file))
                return null;
            var dir = GetIncDir();
            if(string.IsNullOrEmpty(dir))
                return null;
            return Path.Combine(dir,file);
        }

        public static string GetGenDir()
        {
            var rdir = get_setupinfo( WordStorage.Store.settingini_setupinfo_genrdir /* "genrdir" */);
            if (string.IsNullOrEmpty(rdir)) return null;
            try {
			    var dir = DirUtil.Normalize( Path.Combine(G.load_file_dir,rdir) );
                return dir;
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected GetGenDir Error :" + e.Message);
                return null;
            }
        }
        public static string GetIncDir()
        {
            var rdir = get_setupinfo( WordStorage.Store.settingini_setupinfo_incrdir /* "incrdir" */);
            if (string.IsNullOrEmpty(rdir)) return null;
            try {
			    var dir = DirUtil.Normalize( Path.Combine(G.load_file_dir,rdir) );
                return dir;
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected GetGenDir Error :" + e.Message);
                return null;
            }
        }
        public static string GetCodeOutputStart()
        {
            var mark = get_setupinfo(WordStorage.Store.settingini_setupinfo_code_output_start /* "code_output_start" */);
            return mark;
        }
        public static string GetCodeOutputEnd()
        {
            var mark = get_setupinfo(WordStorage.Store.settingini_setupinfo_code_output_end /* "code_output_end" */);
            return mark;
        }
        public static string GetLang()
        {
            var lang = get_setupinfo( WordStorage.Store.settingini_setupinfo_lang);
            return lang;
        }
        public static string GetFramework()
        {
            var fw = get_setupinfo( WordStorage.Store.settingini_setupinfo_framework);
            return fw;
        }
        public static string GetStatemachine()
        {
            var sm = get_setupinfo( WordStorage.Store.settingini_setupinfo_statemachine);
            return sm;
        }
        public static string GetLangFramework()
        {
            var s = GetLang();
            var fw = GetFramework();
            if (!string.IsNullOrEmpty(fw))
            {
                s += "(" + fw + ")";
            }
            return s;
        }
        public static string GetLangFramrwork_registName()
        {
            var s = GetLangFramework();
            if (s!=null)
            {
                s = s.Replace(@"\","/");
            }
            return s;
        }
		public static string GetConverter()
		{
			var converter = get_setting( WordStorage.Store.settingini_setupinfo_converter /*converter*/);
            if (string.IsNullOrEmpty(converter))
            {
                return WordStorage.Store.default_converter_dll;
            }
			return converter;
		}
        public static string GetHelpweb()
        {
            /*
                helpwebファイルの優先順位
                １． http時
                ２． 直接参照可能時
                ３． ドキュメントフォルダ下時
                ４． キットパス下時
                ４． プログラムフォルダ時
            */

            var htmlfile = get_setting( WordStorage.Store.settingini_setting_helpweb );
            if (!string.IsNullOrEmpty(htmlfile))
            {
                if (htmlfile.StartsWith("http"))
                {
                    return htmlfile;
                }

                htmlfile = PathUtil.ExtractPathWithEnvVals(htmlfile);
                if (File.Exists(htmlfile))
                {
                    return htmlfile;
                }
                var path = Path.Combine(G.load_file_dir, htmlfile);
                if (File.Exists(path))
                {
                    return path;
                }
                path = Path.Combine(RegistryWork.Get_starterkit_root(), htmlfile);
                if (File.Exists(path))
                {
                    return path; 
                }
                path = Path.Combine(RegistryWork.Get_starterkit_root(), "..", htmlfile);
                if (File.Exists(path))
                {
                    return path;
                }
                //path = Path.Combine(GetKitPath(),@"..\..", htmlfile);
                //if (File.Exists(path))
                //{
                //    return path; 
                //}
                path = Path.Combine( PathUtil.GetThisAppPath(), htmlfile);
                if (File.Exists(path))
                {
                    return path;
                }
#if DEBUG
                path = Path.Combine(  PathUtil.ExtractPathWithEnvVals(  @"%ProgramFiles(x86)%\PSGG" ), htmlfile);
                if (File.Exists(path))
                {
                    return path;
                }
#endif
            }
            return null;
        }
        #region キットパス
        public static string GetKitPath()
        {
            var kitpath_in_setting = get_setting( WordStorage.Store.settingini_setting_kitpath );

            if (string.IsNullOrEmpty(kitpath_in_setting)) {
                return null;
            }
            //1. レジストリから
            var candpath = Path.Combine(RegistryWork.Get_starterkit_root(),"..",kitpath_in_setting);
            if (Directory.Exists(candpath))
            {
                return candpath;
            }
            //2. StateGoファイルフォルダ
            candpath = Path.Combine(PathUtil.GetThisAppPath(),kitpath_in_setting);
            if (Directory.Exists(candpath))
            {
                return candpath;
            }
                        
            //3. Program Filesフォルダ
#if DEBUG
            candpath = Path.Combine(  PathUtil.ExtractPathWithEnvVals(  @"%ProgramFiles(x86)%\PSGG" ), kitpath_in_setting);
            if (File.Exists(candpath))
            {
                return candpath;
            }
#endif
            return null;
        }
        #endregion



        //private static string SettingIni { get { return Path.Combine( G.load_file_dir, "setting.ini"); } }
        private static Hashtable Ht { get {
                if (_ht == null)
                {
                    //if (!File.Exists(SettingIni)) return null;
                    var text =  G.excel_convertsettings.m_setting_ini;   //File.ReadAllText(SettingIni,Encoding.UTF8);
                    _ht = IniUtil.CreateHashtable(text);
                }
                return _ht;
            } } static Hashtable _ht;

        private static string get_setting(string v)
        {
            if (G.excel_convertsettings!=null) {
                return IniUtil.GetValueFromHashtable( WordStorage.Store.settingini_group_setting ,v,Ht);
            }
            return null;
        }
        private static string get_setupinfo(string v)
        {
            return IniUtil.GetValueFromHashtable( WordStorage.Store.settingini_group_setupinfo ,v,Ht);
        }
        private static string get_addon(string v)
        {
            return IniUtil.GetValueFromHashtable( WordStorage.Store.settingini_group_addon,v, Ht);
        }
        public static Hashtable GetMacroHash()
        {
            var group = WordStorage.Store.settingini_group_macro;
            if (Ht!=null && Ht.ContainsKey(group))
            { 
                return (Hashtable)Ht[group];
            }
            return null;
        }
        public static string GetAncestorDir()
        {
            var radir = get_setupinfo( WordStorage.Store.settingini_setupinfo_ref_ancestor_dir);
            if (string.IsNullOrEmpty(radir)) return G.load_file_dir;

            var path = PathUtil.GetAncestorPath(G.load_file_dir,radir);
            return path;
        }
        public static string GetSrcEnc()
        {
            var enc = get_setting(WordStorage.Store.settingini_setting_src_enc);
            return enc;
        }

        //addon
        public static string GetAddonDir()
        {
            var dir = get_addon(WordStorage.Store.settingini_addon_dir);
            return dir;
        }
        public static string GetAddonFile()
        {
            var file = get_addon(WordStorage.Store.settingini_addon_file);
            return file;
        }

        #region 強制変更セーブ  言語名とプラットフォーム名の変更に利用　※他のデータの一緒にセーブされることに注意
        public static bool SetValForce(string category, string key, string value)
        {
            List<string> orderkey_liset;
            var ht = IniUtil.CreateHashtableWithOrderList(G.excel_convertsettings.m_setting_ini,out orderkey_liset);
            if (ht == null) return false;
            var newlinechars = StringUtil.FindNewLineChar(G.excel_convertsettings.m_setting_ini);
            IniUtil.SetValueFromHashtable(category,key,value,ref ht);
            var s = IniUtil.MakeOutput(ht,orderkey_liset,newlinechars);
            G.excel_convertsettings.m_setting_ini = s;
            _ht = null;
            return true;
        }
        /// <summary>
        /// 強制的にlang変更 ※セーブしないと残らない
        /// </summary>
        public static bool SetLangForce(string lang)
        {
            return SetValForce(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_lang,lang);
        }
        /// <summary>
        /// 強制的にframeworkを変更　※セーブしないと残らない
        /// </summary>
        public static bool SetFrameworkForce(string framework)
        {
            return SetValForce(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_framework,framework);
        }
        #endregion


        #region クローンオプション
        public static bool IsCloneExcnage_withUpperCamelWord() //rust用 ファイル名をupper camel文字列も変更の対象にする。
        {
            var s = get_setupinfo( WordStorage.Store.settingini_setupinfo_clone_exchange);
            return s == WordStorage.Store.settingini_setupinfo_clone_exchange_with_upper_camel_word;
        }
        #endregion

    }
}
