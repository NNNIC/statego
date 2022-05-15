using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordStorage;

namespace StateViewer_starter2
{
    public class RegistryWork
    {
        public static string key_save { get {
                return Store.rgst_key + "\\" + Store.rgst_keysub_save;
        } }

        // language (jp or en)
        public static void Set_lang(string lang)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_lang,lang);
        }
        public static string Get_lang()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_lang);
        }

        // template dir
        public static void Set_templatedir(string path)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_templatedir,path);
        }
        public static string Get_templatedir()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_templatedir);
        }

        // template idx
        public static void Set_templateidx(int idx)
        {
            RegistryUtil.SetInt(key_save,Store.rgst_item_templateidx,idx);
        }
        public static int Get_templateidx()
        {
            return RegistryUtil.GetInt(key_save,Store.rgst_item_templateidx);
        }

        // prefix
        /*Obsolete*/public static void Set_prefix(string prefix)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_prefix,prefix);
        }
        /*Obsolete*/public static string Get_prefix()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_prefix);
        }

        // statemachine
        public static void Set_statemchine(string statemachin)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_statemachine, statemachin);
        }
        public static string Get_statemchine()
        {
            return RegistryUtil.GetStr(key_save, Store.rgst_item_statemachine);
        }

        // xlsdir
        public static void Set_xlsdir(string path)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_xlsdir,path);
        }
        public static string Get_xlsdir()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_xlsdir);
        }
        
        // gendir
        public static void Set_gendir(string path)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_gendir,path);
        }
        public static string Get_gendir()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_gendir);
        }

        // control名強制
        public static void Set_force_controlname(bool b)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_force_control,b ? "1" : "0" );
        }
        public static bool Get_force_controlname()
        {
            var s = RegistryUtil.GetStr(key_save,Store.rgst_item_force_control);
            if (s=="0") return false;
            return true; //デフォルト強制
        }

        // スタートキットのパス
        public static void Set_starterkit_path(string path)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_starterkitpath, path);
        }
        public static string Get_starterkit_path()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_starterkitpath);
        }
        public static void Set_starterkit_root(string root)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_starterkit_root, root);
        }
        public static void Get_starterkit_root()
        {
            RegistryUtil.GetStr(key_save,Store.rgst_item_starterkit_root);
        }

        // xlsdirは src直下のdocの中
        public static void Set_docundersrc_xlsdir(bool b)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_docundersrc_xlsdir,b ? "1" : "0" );
        }
        public static bool Get_docundersrc_xlsdir()
        {
            var s = RegistryUtil.GetStr(key_save,Store.rgst_item_docundersrc_xlsdir);
            return s == "1";
        }

        // xlsdirは自由に設定
        public static void Set_free_xlsdir(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_free_xlsdir, b ? "1":"0");
        }
        public static bool Get_free_xlsdir()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_free_xlsdir);
            return s == "1";
        }
    }
}
