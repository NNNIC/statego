package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class MacroUtil
{
    var m_default_macro_ht:system.collections.generic.Dictionary<String, Dynamic>;
    var m_macro_ini_ht:system.collections.generic.Dictionary<String, Dynamic>;
    var m_gensrc_macro_ht:system.collections.generic.Dictionary<String, Dynamic>;
    public function ReadAllMacroSettings(psggItem:lib.util.PsggDataFileUtil_Item, doc_path:String):Void
    {
        var filename:String = psggItem.get_setting_String_String(lib.wordstrage.Store.settingini_group_setting, lib.wordstrage.Store.settingini_setting_macroini);
        if (!system.Cs2Hx.IsNullOrEmpty(filename))
        {
            try
            {
                var path:String = psgg.HxFile.Combine_String_String(psggItem.GetIncDir(doc_path), filename);
                var text:String = psgg.HxFile.ReadAllText_String_Encoding(path, system.text.Encoding.UTF8);
                m_macro_ini_ht = lib.util.IniUtil.CreateHashtable(text);
            }
            catch (__ex:Dynamic)
            {
                system.Console.WriteLine("{4F39CB16-1508-444A-A57B-63961C3ABFE4}\nFile Cannot read :" + system.Cs2Hx.NullCheck(filename) + ":");
            }
        }
        m_default_macro_ht = (function():system.collections.generic.Dictionary<String, Dynamic> return psggItem.get_setting(lib.wordstrage.Store.settingini_group_macro))();
        try
        {
            var macroinitext:String = "";
            var path:String = psggItem.GetGeneratedSource(doc_path);
            var enc:system.text.Encoding = system.Cs2Hx.IsNullOrEmpty(psggItem.GetSrcEnc()) ? system.text.Encoding.UTF8 : psgg.HxEncoding.GetEncoding_String(psggItem.GetSrcEnc());
            var text:String = psgg.HxFile.ReadAllText_String_Encoding(path, enc);
            var lines:Array<String> = system.Cs2Hx.Split(text, [ 10 ]);
            var bInMacro:Bool = false;
            for (l in lines)
            {
                if (!bInMacro)
                {
                    if (system.Cs2Hx.StringContains(l, lib.wordstrage.Store.macro_start_mark))
                    {
                        bInMacro = true;
                        continue;
                    }
                }
                else
                {
                    if (system.Cs2Hx.StringContains(l, lib.wordstrage.Store.macro_end_mark))
                    {
                        bInMacro = false;
                        continue;
                    }
                }
                if (bInMacro)
                {
                    macroinitext += system.Cs2Hx.NullCheck(l) + system.Cs2Hx.NullCheck(system.Environment.NewLine);
                }
            }
            if (bInMacro)
            {
                system.Console.WriteLine("Cannot find " + system.Cs2Hx.NullCheck(lib.wordstrage.Store.macro_end_mark) + " in " + system.Cs2Hx.NullCheck(psggItem.GetGeneratedSourceFileName()));
            }
            else
            {
                m_gensrc_macro_ht = lib.util.IniUtil.CreateHashtable(macroinitext);
            }
        }
        catch (__ex:Dynamic)
        {
            system.Console.WriteLine("{28F2C476-7C8B-4C75-8115-A5E543DABAB1}\nFile Cannot read :" + system.Cs2Hx.NullCheck(psggItem.GetGeneratedSourceFileName()));
        }
    }
    public function GetValue(key:String):String
    {
        if (m_gensrc_macro_ht != null && m_gensrc_macro_ht.ContainsKey(key))
        {
            return (function():String return m_gensrc_macro_ht.GetValue_TKey(key))();
        }
        if (m_macro_ini_ht != null && m_macro_ini_ht.ContainsKey(key))
        {
            return (function():String return m_macro_ini_ht.GetValue_TKey(key))();
        }
        if (m_default_macro_ht != null && m_default_macro_ht.ContainsKey(key))
        {
            return (function():String return m_default_macro_ht.GetValue_TKey(key))();
        }
        return null;
    }
    public function new()
    {
    }
}
