using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace WordStorage
{
    public class Store
    {
        #region registry
        public static readonly string        rgst_key              = @"Software\psgg\v01";

        public static readonly string        rgst_lang             = "lang";

        public static readonly string        rgst_keysub_save      = "save";
        public static readonly string        rgst_item_templatedir = "templatedir";
        public static readonly string        rgst_item_templateidx = "templateidx";

        /*Obsolete*/public static readonly string        rgst_item_prefix      = "prefix";

        public static readonly string        rgst_item_xlsdir      = "xlsdir";
        public static readonly string        rgst_item_gendir      = "gendir";
        public static readonly string        rgst_item_srceditcmd  = "srceditcmd";
        public static readonly string        rgst_item_srceditcmd_option  = "srceditcmd_option"; //利用中のオプション名　オプションがカンマ区切り
        public static readonly string        rgst_item_usecmneditor = "use_cmn_editor";

        public static readonly string        rgst_item_findhist    = "findhist";

        public static readonly string        rgst_item_starterkitpath  = "starterkitpath"; //最後に使用したスタートキットの psggファイルへのフルパス

        public static readonly string        rgst_item_starterkit_root = "starterkit_root";//最後に使用したスタートキットのルートのフルパスが入る。 ※starterkit2のパス名の前まで

        public static readonly string        rgst_item_statemachine = "statemachine";
        public static readonly string        rgst_item_force_control = "force_control";
        public static readonly string        rgst_item_docundersrc_xlsdir = "docundersrc_xlsdir";
        public static readonly string        rgst_item_free_xlsdir = "free_xlsdir";

        public static readonly string        rgst_item_serialcode = "serialcode";
        public static readonly string        rgst_item_serial_latestdate = "seriallatestdate";

        public static readonly string        rgst_item_running_timestamp ="runningtimestamp";

        public static readonly string        rgst_item_editform_desc_onoff = "editform_desc_onoff";

        public static readonly string        rgst_item_mrb_enable = "mrb_enable";
        public static readonly string        rgst_item_historypanelonstart_enable = "historypanelonstart_enable";
        public static readonly string        rgst_item_forceclose_ifviewchangeonly = "forceclose_ifviewchangeonly";
        public static readonly string        rgst_item_option_set_default_comment = "option_set_default_comment";

        public static readonly string        rgst_item_execbatch_editor = "execbatch_editor";

        public static readonly string        rgst_item_jump_if_statego_exist = "jump_if_statego_exist";

        public static readonly string        rgst_item_texteditor_zoom    = "texteditor_zoom" ;
        public static readonly string        rgst_item_srctabpanel_zoom   = "srctabpanel_zoom";

        public static readonly string        rgst_item_lexical_color_onoff = "lexical_color_onoff";

        public static readonly string        rgst_item_editbranch_useeditbox = "editbranch_useeditbox";

        #endregion

        #region srceditcmd option
        public static readonly string        srceditcmd_option_vs2015 = "vs2015";
        #endregion

        #region sheet name
        public static readonly string         sheetchart = "state-chart";
        public static readonly string         sheetconfig  = "config";
        public static readonly string         sheettempsrc = "template-source";
        public static readonly string         sheettempfunc= "template-statefunc";
        public static readonly string         sheetsetting = "setting.ini";
        public static readonly string         sheethelp    = "help";
        public static readonly string         sheetitems   = "itemsinfo";
        #endregion

        #region psgg header
        public static readonly string         psgg  = "psgg";
        public static readonly string         excel = "excel"; 
        #endregion

        #region settinig ini names
        public static readonly string         settingini_group_setting   = "setting";
        public static readonly string         settingini_group_setupinfo = "setupinfo";
        public static readonly string         settingini_group_macro     = "macro";
        public static readonly string         settingini_group_jpn       = "jpn";
        public static readonly string         settingini_group_en        = "en";
        public static readonly string         settingini_group_addon     = "addon";

        //[Obsolete]
        //public static readonly string         settingini_setting_converter = "converter";
        //廃止 public static readonly string         settingini_setting_viewbat   = "viewbat";

        public static readonly string         settingini_setting_psgg      = "psgg";

        public static readonly string         settingini_setting_xls       = "xls";
        public static readonly string         settingini_setting_sub_src   = "sub_src";
        public static readonly string         settingini_setting_gensrc    = "gen_src";
        public static readonly string         settingini_setting_genhpp    = "gen_hpp";
        public static readonly string         settingini_setting_macroini  = "macro_ini";

        public static readonly string         settingini_setting_manager_src = "manager_src";
        public static readonly string         settingini_setting_manager_dir = "manager_dir";

        public static readonly string         settingini_setting_template_src  = "template_src";
        public static readonly string         settingini_setting_template_func = "template_func";

        public static readonly string         settingini_setting_help          = "help";
        public static readonly string         settingini_setting_helpweb       = "helpweb";
        public static readonly string         settingini_setting_kitpath       = "kitpath";
        public static readonly string         settingini_setting_src_enc       = "src_enc";

        public static readonly string         settingini_setupinfo_converter   = "converter";
        public static readonly string         settingini_setupinfo_lang        = "lang";
        public static readonly string         settingini_setupinfo_framework   = "framework";
        public static readonly string         settingini_setupinfo_statemachine= "statemachine";

        public static readonly string         settingini_setupinfo_prefix      = "prefix";
        public static readonly string         settingini_setupinfo_xlsdir      = "xlsdir";
        public static readonly string         settingini_setupinfo_gendir      = "gendir";
        public static readonly string         settingini_setupinfo_genrdir     = "genrdir";
        public static readonly string         settingini_setupinfo_incrdir     = "incrdir";
        public static readonly string         settingini_setupinfo_ref_ancestor_dir = "ref_ancestor_dir";
        public static readonly string         settingini_setupinfo_code_output_start = "code_output_start";
        public static readonly string         settingini_setupinfo_code_output_end   = "code_output_end";

        public static readonly string         settingini_setupinfo_clone_exchange    = "clone_exchange";
        public static readonly string         settingini_setupinfo_clone_exchange_with_upper_camel_word = "with_upper_camel_word"; //clone exchangeの値

        public static readonly string         settingini_lang_title            = "title";
        public static readonly string         settingini_lang_detail           = "detail";

        public static readonly string         settingini_addon_dir             = "dir";
        public static readonly string         settingini_addon_file            = "file";


        #endregion
        #region setting converting word

        [Obsolete]public static readonly string settingini_convertword_prefix       = "__PREFIX__";

        public static readonly string           settingini_convertword_statemachine = "__PREFIX__Control";
                                                
        public static readonly string           settingini_convertword_xlsdir       = "__XLSDIR__";
        public static readonly string           settingini_convertword_gendir       = "__GENDIR__";
        public static readonly string           settingini_convertword_genrdir      = "__GENRDIR__";

        public static readonly string           settingini_convertword_starterkit   = "__STARTERKIT__";
        #endregion

        #region lang dir lang.txt
        public static readonly string         langdir_langtxt                  = "lang.txt";
        #endregion

        #region converter defaulr
        public static readonly string         default_converter_dll            = "psggConverterLib.dll";
        #endregion

        #region psgg-macro
        public static readonly string macro_start_mark = ":psgg-macro-start";
        public static readonly string macro_end_mark   = ":psgg-macro-end";
        #endregion

        #region psgg file
        public static readonly string psgg_ver = "1.1";
        public static readonly string PSGG_MARK_PREFIX           = "------#======*<Guid(";
        public static readonly string PSGG_MARK_POSTFIX          = ")>*======#------";
        public static readonly string PSGG_MARK_STATECHART_SHEET = "------#======*<Guid(D13821FE-FA27-4B04-834C-CEC1E5670F48)>*======#------";
        public static readonly string PSGG_MARK_VARIOUS_SHEET    = "------#======*<Guid(70C5A739-223A-457D-8AEE-1A0E2050D5AE)>*======#------";
        public static readonly string PSGG_MARK_BITMAP_DATA      = "------#======*<Guid(4DC98CBA-6257-4E26-A454-A53F85BC234C)>*======#------";
        public static readonly string PSGG_MARK_VARIOUS_BEGIN    = "###VARIOUS-CONTENTS-BEGIN###";
        public static readonly string PSGG_MARK_VARIOUS_END      = "###VARIOUS-CONTENTS-END###";
        public static readonly string PSGG_MARK_BITMAP_BEGIN     = "###BITMAP-DATA-BEGIN###";
        public static readonly string PSGG_MARK_BITMAP_END       = "###BITMAP-DATA-END###";
        #endregion

        #region state typ
        public static readonly string state_typ_start     = "start";
        public static readonly string state_typ_end       = "end";
        public static readonly string state_typ_gosub     = "gosub";
        public static readonly string state_typ_substart  = "substart";
        public static readonly string state_typ_subreturn = "subreturn";
        public static readonly string state_typ_loop      = "loop";
        public static readonly string state_typ_stop      = "stop";
        public static readonly string state_typ_pass      = "pass";
        public static readonly string state_typ_base      = "base";
        #endregion

    }

    public class Res
    {
        private static Dictionary<string,string[]> __dic = null;
        private static Dictionary<string,string[]> m_dic {get {
                if (__dic==null)
                {
                    __dic = CsvUtil.CreateDictionary(Properties.Resources.stringtable_txt);
                }
                return __dic;
            } }

        public static string Get(string id, string lang)
        {
			var val = CsvUtil.Get(m_dic,id,lang);
            var s = string.IsNullOrEmpty(val) ? id : val;

            if (s.Contains(@"\n")) s = s.Replace(@"\n", Environment.NewLine);
            for(var loop = 0; loop<1000; loop++)
            {
                var match = RegexUtil.Get1stMatch(@"\\x[0-9a-fA-F]{2}",s);
                if (!string.IsNullOrEmpty(match))
                {
                    var num = int.Parse(match.Substring(2),System.Globalization.NumberStyles.HexNumber);
                    s = s.Replace(match,new string((char)num,1));
                }
                else
                {
                    break;
                }
            }
            return s;
        }

        #region For Winfows form 
        public static void ChangeAll(Object o, string lang)
        {
            _changeAll(o,lang);
        }
        private static void _changeAll(Object o, string lang)
        {
            if (o==null) return;

            _settext(o,lang);

            var type = o.GetType();
            if (type.AssemblyQualifiedName.Contains("ScintillaBox"))
            {
                return;  
            }
            

            var allfields = type.GetFields(BindingFlags.GetField | BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach(var f in allfields)
            {
                var ft = f.FieldType;
                if (ft.IsSubclassOf(typeof(Control)) || ft.IsSubclassOf(typeof(Component)))
                {
                    var fo = f.GetValue(o);
                    _changeAll(fo,lang);
                }
            }
        }


        private static void _settext(Object o, string lang)
        {
            if (o==null) return;
            var type = o.GetType();
            if ( (
                type.IsSubclassOf(typeof(Control)) 
                ||
                type.IsSubclassOf(typeof(Component))
                ) &&
                (!(o is WebBrowser) )  
                )
            {
                var ptag = type.GetProperty("Tag");
                if (ptag!=null)
                {
                    var ido = ptag.GetValue(o);
                    if (ido!=null)
                    { 
                        var id = ido.ToString();
                        if (!string.IsNullOrEmpty(id)) { 
                            var s = Get(id,lang);
                            if (!string.IsNullOrEmpty(s))
                            { 
                                var ptext = type.GetProperty("Text");
                                if (ptext!=null)
                                {
                                    ptext.SetValue(o,s);
                                }
                            }
                        }
                    }
                }
            }
        }



        #endregion

    }

}
