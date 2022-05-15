using WordStorage;
using System;
namespace PSGGEditor
{
    public class RegistryWork
    {
        public static string key_save { get {
                return Store.rgst_key + "\\" + Store.rgst_keysub_save;
        } }

        public static string key_save_w_langfw(string langfw)
        {
            if (string.IsNullOrEmpty(langfw)) langfw = "unknown";
            return key_save + "\\" + langfw;
        }

        //source editor command
        public static void Set_srceditcmd(string command, string langfw)
        {
            RegistryUtil.SetStr(key_save_w_langfw(langfw),Store.rgst_item_srceditcmd,command);
        }
        public static string Get_srceditcmd(string langfw)
        {
            return RegistryUtil.GetStr(key_save_w_langfw(langfw) ,Store.rgst_item_srceditcmd);
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
    }
}