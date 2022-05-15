using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace StateViewer_starter2
{
    public partial class WORK
    {
        private static Start2Form m_form { get { return Start2Form.m_form;   } }
        private static bool eng_or_jpn   { get { return m_form.m_eng_or_jpn; } }

        private static CreateNewForm           m_cf { get { return m_form.m_createform; } }
        private static CreateNewDetailTextFrom m_df { get { return m_form.m_detailform; } }

        //public class LangItem
        //{
        //    public string lang; //program language
        //    public string folder;

        //    public LangItem(string _lang, string f)
        //    {
        //        lang = _lang;
        //        folder = f;
        //    }
        //}

        public class SettingItem
        {
            public string lang; //program language
            public string path;

            public string title;
            public string detail;

            //public string converter;

            //public string viewbat;
            public string psgg;

            public string xls;

            public string manager_src;
            public string manager_dir;
            public string sub_src;
			public string gen_src;
            public string macro_ini;

            public string template_src;
            public string template_func;

            public string help;
            public string helpweb;

            public string src_enc = "utf-8";
            public Encoding SRC_ENCODING { get {
                    try {
                        if (!string.IsNullOrEmpty(src_enc))
                        {
                            return Encoding.GetEncoding(src_enc);
                        }
                    } catch {
                    }
                    return Encoding.UTF8;
                } } //ソースセーブ用

            //string syslang {get { return WordStorage.Store.settingini_group_jpn;} } // = "jpn";

            public SettingItem(string _path, Hashtable ht, string _syslang=null)
            {
                string setting = WordStorage.Store.settingini_group_setting; //"setting";

                path = _path;

                lang         = IniUtil.GetValueFromHashtable(_syslang, WordStorage.Store.settingini_setupinfo_lang /*"lang"*/,ht);

                title        = IniUtil.GetValueFromHashtable(_syslang, WordStorage.Store.settingini_lang_title /*"title"*/,ht);
                detail       = IniUtil.GetValueFromHashtable(_syslang, WordStorage.Store.settingini_lang_detail/*"detail"*/,ht);

                //converter    = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_converter/*"converter"*/,ht);

                psgg         = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_psgg/*"psgg"*/,ht);
                             
                xls          = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_xls/*"xls"*/,ht);
                             
                manager_src  = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_manager_src/*"manager_src"*/,ht);
                manager_dir  = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_manager_dir/*"manager_dir"*/,ht);
                             
                sub_src      = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_sub_src/*"sub_src"*/ ,ht);
				gen_src      = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_gensrc /*"gen_src"*/ ,ht);

                macro_ini    = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_macroini /*"macro_ini"*/ ,ht);

                template_src = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_template_src/*"template_src"*/,ht);
                template_func= IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_template_func/*"template_func"*/,ht);

                help         = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_help/*"help"*/,ht);
                helpweb      = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_helpweb/*"helpweb"*/,ht);

                src_enc      = IniUtil.GetValueFromHashtable(setting, WordStorage.Store.settingini_setting_src_enc/*"src_inc"*/,ht);
            } 
        }

        public static string SETTINGINI { get { return WordStorage.Store.sheetsetting; } }

        public static string TEMPLATEPATH {
            get {
                    if (__TEMPLATEPATH == null)
                    {
                        //Debug用
                        if (Application.ExecutablePath.Contains(@"\StateViewer\"))
                        {
                            var psgg_editor_rootdir =  (new DirectoryInfo(Path.Combine(Application.ExecutablePath,@"..\..\..\..\..\.."))).FullName;                     
                            var target = Path.Combine(psgg_editor_rootdir,@"mastering\public\v01\starterkit");
                            __TEMPLATEPATH = target; 
                        }
                        else
                        {
                            var install_dir = (new DirectoryInfo(Path.Combine(Application.ExecutablePath,@"..\"))).FullName;
                            var target      = Path.Combine(install_dir,@"starterkit");
                            __TEMPLATEPATH = target;
                        }
                    }
                    return __TEMPLATEPATH;
            }
            set {
                __TEMPLATEPATH = value;
            }
        }
        private static string __TEMPLATEPATH = null;
        public static void TEMPLATEPATH_Reset() {__TEMPLATEPATH = null; }
        public static void InitSection()
        {
            var rg_tempdir = RegistryWork.Get_templatedir();
            if (!string.IsNullOrEmpty(rg_tempdir))
            {
                WORK.TEMPLATEPATH = rg_tempdir;
            }

            var rg_prefix = RegistryWork.Get_prefix();
            if (!string.IsNullOrEmpty(rg_prefix))
            {
                m_form.textBoxPrefix.Text = rg_prefix;
                m_cf.textBoxPrefix.Text = rg_prefix;
            }

            var rg_statemachine = RegistryWork.Get_statemchine();
            if (!string.IsNullOrEmpty(rg_statemachine))
            {
                m_cf.textBoxStateMachineName.Text = rg_statemachine;
            }

            var rg_xlsdir = RegistryWork.Get_xlsdir();
            if (!string.IsNullOrEmpty(rg_xlsdir))
            {
                m_form.textBoxExcelFolder.Text = rg_xlsdir;
                m_cf.textBoxDocFolder.Text = rg_xlsdir;
            }

            var rg_gendir = RegistryWork.Get_gendir();
            if (!string.IsNullOrEmpty(rg_gendir))
            {
                m_form.textBoxGenerateFolder.Text = rg_gendir;
                m_cf.textBoxGenFolder.Text = rg_gendir;
            }

        }
        public static void SetupCreateSection()
        {
            m_form.labelReadFrom.Text = "Read from " + TEMPLATEPATH;
            m_cf.textBoxReadFromPath.Text = TEMPLATEPATH;
        }

        #region Program Languageによって値変更
        //public static string            SELECTED;
        //public static string            SELECTED_LANG_DIR;
        public static List<SettingItem> SETTINGS;

        public static string STARTERKIT_INFO; 

        public static void Setup_for_lang(string system_language="jpn")
        {
            var cf = m_cf;
            var df = m_df;
            

            //SELECTED = program_language;
            SETTINGS = new List<SettingItem>();
            //SELECTED_LANG_DIR = TEMPLATEPATH;   //LANGDATA[program_language].folder;

            m_form.listBox_title.Items.Clear();
            m_form.textBoxDetail.Text = string.Empty;

            // create new
            cf.listBox_title.Items.Clear();
            df.textBoxStarterKitDescription.Text = string.Empty;
            

            if (!Directory.Exists(TEMPLATEPATH))
            {
                m_form.listBox_title.Items.Add(""); //null set to avoid from crash.
                cf.listBox_title.Items.Add("");     //null set to avoid from crash.
                return;
            }
            var dis = (new DirectoryInfo(TEMPLATEPATH).GetDirectories());
            if (dis == null || dis.Length == 0) {
                m_form.listBox_title.Items.Add(""); //null set to avoid from crash.
                cf.listBox_title.Items.Add("");     //null set to avoid from crash.
                return;
            }

            foreach (var di in dis)
            {
                var path = Path.Combine(di.FullName, SETTINGINI);
                if (string.IsNullOrEmpty(path)) continue;
                
                try {
                    var ht = IniUtil.CreateHashtable(File.ReadAllText(path,Encoding.UTF8));
                    if (ht == null) continue;

                    var item = new SettingItem(di.FullName,ht, system_language);
                    SETTINGS.Add(item);

                } catch { }
            }

            var titles = GetTitleList();
            if (titles!=null)
            {
                foreach(var s in titles)
                {
                    if (!string.IsNullOrEmpty(s)) {
                        m_form.listBox_title.Items.Add(s);
                        cf.listBox_title.Items.Add(s);
                    }
                }
                if (m_form.listBox_title.Items.Count>0)
                {
                    m_form.listBox_title.SetSelected(0,true);
                    cf.listBox_title.SetSelected(0,true);
                }
            }
            try {
                var starterkit_file = Path.Combine(TEMPLATEPATH, "starter-kit.txt");
                var text = File.ReadAllText(starterkit_file,Encoding.UTF8);
                var lines = text.Split('\x0a');
                STARTERKIT_INFO = lines[0];
            } catch
            {
                STARTERKIT_INFO = "unknown";
            }
        }
        public static string[] GetTitleList()
        {
            if (SETTINGS == null) return null;
            var list = new List<string>();
            foreach(var i in SETTINGS)
            {
                list.Add(i.title);
            }
            return list.ToArray();
        }

        #endregion

        #region INPUT TEXTにより内容を変更
        public static string DETAIL  = null;
        /*Obsolete*/public static string PREFIX  = null;
        public static string XLSDIR  = null;
        public static string GENDIR {
            set;
            get;
        }
        public static string GENRDIR = null; //GENDIRのXLSDIRからの相対位置 

        public static string STATEMACHINE = null; // PREFIXの代わり

        public static SettingItem SELECT_SETTING; //選択されたセット

        public static void setup_setting(int index, bool bUndefined = false)
        {
            if (SETTINGS==null || SETTINGS.Count <= index || index < 0)
            {
                return;
            }
            
            SELECT_SETTING = SETTINGS[index];

            DETAIL = SELECT_SETTING.detail;
            
            UpdateByInputText(bUndefined);
        }
        public static void UpdateByInputText(bool bUndefined = false)
        {
            PREFIX = m_form.textBoxPrefix.Text = m_cf.textBoxPrefix.Text.Trim();
            XLSDIR = m_form.textBoxExcelFolder.Text = m_cf.textBoxDocFolder.Text.Trim();
            GENDIR = m_form.textBoxGenerateFolder.Text = m_cf.textBoxGenFolder.Text.Trim();

            STATEMACHINE = m_cf.textBoxStateMachineName.Text.Trim();

            UpdateByInputText2(bUndefined);
        }

        public static void UpdateByInputText2(bool bUndefined = false)
        {
            GENRDIR = PathUtil.GetRelativePath(XLSDIR, GENDIR);

            var str = DETAIL;
            str = convert_var_in_text(str, bUndefined);
            if (m_form != null) m_form.textBoxDetail.Text = str;
            if (m_df != null) m_df.textBoxStarterKitDescription.Text = str;
        }
        #endregion

        private static string convert_var_in_text(string text, bool bUndefined =false)
        {
            if (string.IsNullOrEmpty(text)) return null;

            var prefix = PREFIX;
            var xlsdir = XLSDIR;
            var gendir = GENDIR;
            var genrdir = GENRDIR;
            var statemachine = STATEMACHINE;
            var starterkit = STARTERKIT_INFO;

            if (bUndefined)
            {
                statemachine = prefix = xlsdir = gendir = genrdir = null;
            }

            var str = text;
            if (!string.IsNullOrEmpty(str))
            {
                //str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_prefix, prefix);// str.Replace(WordStorage.Store.settingini_convertword_prefix/*"__PREFIX__"*/, PREFIX);
                str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_statemachine, statemachine);
                str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_xlsdir, xlsdir);//str.Replace(WordStorage.Store.settingini_convertword_xlsdir/*"__XLSDIR__"*/, XLSDIR);
                str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_gendir, gendir);//str.Replace(WordStorage.Store.settingini_convertword_gendir/*"__GENDIR__"*/, GENDIR);
                str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_genrdir, genrdir);//str.Replace(WordStorage.Store.settingini_convertword_genrdir/*"__GENRDIR__"*/,GENRDIR);
                str = convert_var_in_text_sub(str, WordStorage.Store.settingini_convertword_starterkit, starterkit);//str.Replace(WordStorage.Store.settingini_convertword_starterkit/*"__STARTERKIT__"*/,STARTERKIT);
            }
            return str;
        }
        private static string modify_helpweb(string str, SettingItem stg)
        {
            var word = RegexUtil.Get1stMatch(@"\nhelpweb\=.*\n",str);
            if (string.IsNullOrEmpty(word)) return str;
            word = word.Trim();

            var val = RegexUtil.Get1stMatch(@"\=.*$",word); //.TrimStart('=').Trim();
            if (!string.IsNullOrEmpty(val)) { 
                val = val.TrimStart('=').Trim();
            }
            if (val.Contains(":")) return str; // http: や c:等の場合とみなす。 そのまま
            //定義されていないとみなして

            //stg.path　starterkit以降を取得
            var path = RegexUtil.Get1stMatch(@"\\[^\\]*?\\[^\\]*?$",stg.path);
            if (!string.IsNullOrEmpty(path))
            {
                path = path.Substring(1);

                var newword = "helpweb=" + Path.Combine( path,val);
                str = str.Replace(word,newword);
            }
            return str;     
        }

        private static string convert_var_in_text_sub(string text, string target, string repval)
        {
            if (string.IsNullOrEmpty(repval))
            {
                repval = WordStorage.Res.Get("S09",(eng_or_jpn ? "en" : "jpn"));
            }
            if (!string.IsNullOrEmpty(repval))
            {
                return text.Replace(target, repval);
            }
            return text;
        }
    }
}
