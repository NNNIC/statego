using WordStorage;
using System;
namespace stateview
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

        public static string key_save_w_langfw(string langfw)
        {
            if (string.IsNullOrEmpty(langfw)) langfw = "unknown";
            return key_save + "\\" + langfw;
        }

        //source editor command
        public static void Set_srceditcmd(string command, string langfw,string option=null)
        {
            RegistryUtil.SetStr(key_save_w_langfw(langfw),Store.rgst_item_srceditcmd,command);
            if (option==null) option = string.Empty;
            RegistryUtil.SetStr(key_save_w_langfw(langfw),Store.rgst_item_srceditcmd_option, option);
            //if (option!=null) RegistryUtil.SetStr("option"/*key_save_w_langfw(langfw)*/,Store.rgst_item_srceditcmd_option,option);
        }
        public static string Get_srceditcmd(string langfw)
        {
            return RegistryUtil.GetStr(key_save_w_langfw(langfw) ,Store.rgst_item_srceditcmd);
        }
        public static string Get_srceditcmd_option(string langfw)
        {
            return RegistryUtil.GetStr(key_save_w_langfw(langfw) ,Store.rgst_item_srceditcmd_option);
            //return RegistryUtil.GetStr("option"/*key_save_w_langfw(langfw)*/ ,Store.rgst_item_srceditcmd_option);
        }
        public static string[] Get_srcedit_all_langfw()
        {
            return RegistryUtil.List(key_save);
        }
        public static void Set_use_cmn_editor(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_usecmneditor,b.ToString());
        }
        public static bool Get_use_cmn_editor()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_usecmneditor);
            var b = ParseUtil.ParseBool(s, false);
            return b;
        }


        //find text history
        public static void Set_findhistory(string history)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_findhist, history);
        }
        public static string Get_findhistory()
        {
            return RegistryUtil.GetStr(key_save, Store.rgst_item_findhist);
        }

        //register serial
        public static void Set_serial(string ser)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_serialcode, ser);
        }
        public static string Get_serial()
        {
            return RegistryUtil.GetStr(key_save, Store.rgst_item_serialcode);
        }
        public static void Set_serial_checkdate(DateTime date)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_serial_latestdate, date.ToString());
        }
        public static DateTime Get_serial_checkdate()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_serial_latestdate);
            try
            {
                return DateTime.Parse(s);
            }
            catch {

            }
            return DateTime.MinValue;
        }

        //latest running date
        public static void Set_running_timestamp()
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_running_timestamp, DateTime.Now.ToString());
        }
        public static DateTime Get_running_timestamp()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_running_timestamp);
            try
            {
                return DateTime.Parse(s);
            }
            catch {

            }
            return DateTime.MinValue;
        }

        //edit form show/hide description. 説明ONOFF
        public static void Set_editform_desc_onoff(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_editform_desc_onoff, b.ToString() );
        }
        public static bool Get_editform_desc_onoff()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_editform_desc_onoff);
            try
            {
                return bool.Parse(s);
            }
            catch
            {
            }
            return true; //デフォルト表示
        }

        public static void Set_item_mrb_enable(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_mrb_enable, b.ToString() );
        }
        public static bool Get_item_mrb_enable()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_mrb_enable);
            try
            {
                return bool.Parse(s);
            }
            catch
            {
            }
            return false; //デフォルト表示
        }

        public static void Set_item_historypanelonstart_enable(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_historypanelonstart_enable, b.ToString() );
        }
        public static bool Get_item_historypanelonstart_enable()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_historypanelonstart_enable);
            try
            {
                return bool.Parse(s);
            }
            catch
            {
            }
            return true; //デフォルト表示
        }

        public static void Set_item_forceclose_ifviewchangeonly(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_forceclose_ifviewchangeonly, b.ToString() );
        }
        public static bool Get_item_forceclose_ifviewchangeonly()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_forceclose_ifviewchangeonly);
            try
            {
                return bool.Parse(s);
            }
            catch
            {
            }
            return false; //デフォルト表示
        }

        public static void Set_item_jump_if_statego_exist(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_jump_if_statego_exist, b.ToString() );
        }
        public static bool Get_item_jump_if_statego_exist()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_jump_if_statego_exist);
            try
            {
                return bool.Parse(s);
            }
            catch
            {
            }
            return false; //デフォルトは自動遷移なし
        }
        //ステート生成時のコメントON/OFF
        public static void Set_item_option_set_default_comment(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_option_set_default_comment, b.ToString() );
        }
        public static bool Get_item_option_set_default_comment()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_option_set_default_comment);
            try {
                return bool.Parse(s);
            }
            catch
            {
            }
            return true; //デフォルトON
        }

        // スタートキットパス
        public static void Set_starterkit_root(string root)
        {
            RegistryUtil.SetStr(key_save,Store.rgst_item_starterkit_root, root);
        }
        public static string Get_starterkit_root()
        {
            return RegistryUtil.GetStr(key_save,Store.rgst_item_starterkit_root);
        }

        // ソースエディタのバッチ起動 ON/OFF
        public static void Set_execbatch_editor(bool bUse)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_execbatch_editor, bUse.ToString() );
        }
        public static bool Get_execbatch_editor()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_execbatch_editor);
            try {
                return bool.Parse(s);
            }
            catch
            {
            }
            return false; //デフォルトOFF
        }

        //Text Editor フォントサイズ
        public static void Set_texteditor_zoom(int zoom)
        {
            if (zoom != int.MinValue)
            {
                RegistryUtil.SetInt(key_save, Store.rgst_item_texteditor_zoom, zoom);
            }
        }
        public static int Get_texteditor_zoom()
        {
            var i = RegistryUtil.GetInt(key_save, Store.rgst_item_texteditor_zoom, int.MinValue);
            return i; //エラー時は int.minvalue
        }

        //パネルのソースタブのフォントサイズ
        public static void Set_srctabpanel_zoom(int zoom)
        {
            RegistryUtil.SetInt(key_save, Store.rgst_item_srctabpanel_zoom, zoom );
        }
        public static int Get_srctabpanel_zoom()
        {
            var i = RegistryUtil.GetInt(key_save, Store.rgst_item_srctabpanel_zoom, int.MinValue);
            return i; //エラー時は int.minvalue
        }

        //字句解析により色設定 ON/OFF
        public static void Set_lexical_color_onoff(bool b)
        {
            RegistryUtil.SetStr(key_save, Store.rgst_item_lexical_color_onoff, b.ToString() );
        }
        public static bool Get_lexical_color_onoff()
        {
            var s = RegistryUtil.GetStr(key_save, Store.rgst_item_lexical_color_onoff);
            try {
                return bool.Parse(s);
            }
            catch
            {
            }
            return true; //デフォルトON
        }
    }
}