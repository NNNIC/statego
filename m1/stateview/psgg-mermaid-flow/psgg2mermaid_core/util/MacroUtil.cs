using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HT = System.Collections.Generic.Dictionary<string, object>;

namespace lib.util
{
    public class MacroUtil
    {
        HT m_default_macro_ht; // settingsの[macro]部分
        HT m_macro_ini_ht;     // __PREFIX__ControlMacro.iniファイル部分
        HT m_gensrc_macro_ht;  // 生成先ソースに定義されたマクロ部分  :psgg-macro-start  :psgg-macro-end

        public void ReadAllMacroSettings(PsggDataFileUtil.Item psggItem, string doc_path)
        {
            var filename = psggItem.get_setting(wordstrage.Store.settingini_group_setting, wordstrage.Store.settingini_setting_macroini);  //SettingIniUtil.GetMacroIni();
            if (!string.IsNullOrEmpty(filename))
            { 
                try
                {
                    var path = Path.Combine(psggItem.GetIncDir(doc_path), filename);
                    var text = File.ReadAllText(path, Encoding.UTF8);
                    m_macro_ini_ht      = IniUtil.CreateHashtable(text);
                }
                catch 
                {
                    Console.WriteLine("{4F39CB16-1508-444A-A57B-63961C3ABFE4}\nFile Cannot read :" + filename +":");
                }
            }
            m_default_macro_ht = (HT)psggItem.get_setting(wordstrage.Store.settingini_group_macro);//   SettingIniUtil.GetMacroHash();

            try
            {   //生成ソース内の マクロを収集する。 
                var macroinitext = string.Empty;
                var path = psggItem.GetGeneratedSource(doc_path);
                var enc = string.IsNullOrEmpty(psggItem.GetSrcEnc()) ? Encoding.UTF8 : Encoding.GetEncoding(psggItem.GetSrcEnc());
                var text = File.ReadAllText(path, enc);
                var lines = text.Split('\xa');

                bool bInMacro = false;
                foreach (var l in lines)
                {
                    if (!bInMacro)
                    {
                        if (l.Contains(wordstrage.Store.macro_start_mark))
                        {
                            bInMacro = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (l.Contains(wordstrage.Store.macro_end_mark))
                        {
                            bInMacro = false;
                            continue;
                        }
                    }
                    
                    if (bInMacro)
                    {
                        macroinitext += l + Environment.NewLine;
                    }
                }

                if (bInMacro)
                {
                    Console.WriteLine("Cannot find " + wordstrage.Store.macro_end_mark +" in "  + psggItem.GetGeneratedSourceFileName());
                }
                else
                {
                    m_gensrc_macro_ht = IniUtil.CreateHashtable(macroinitext);
                }

            }
            catch 
            {
                Console.WriteLine("{28F2C476-7C8B-4C75-8115-A5E543DABAB1}\nFile Cannot read :" + psggItem.GetGeneratedSourceFileName() );
            }

            
        }

        public string GetValue(string key)
        {
            //生成ソース内のマクロ確認
            if (m_gensrc_macro_ht != null && m_gensrc_macro_ht.ContainsKey(key))  
            {
                return (string)m_gensrc_macro_ht[key];
            }
            //Iniファイル内のマクロ確認
            if (m_macro_ini_ht!=null && m_macro_ini_ht.ContainsKey(key))
            {
                return (string)m_macro_ini_ht[key];
            }
            //Settingの[macro]確認
            if (m_default_macro_ht!=null && m_default_macro_ht.ContainsKey(key))
            {
                return  (string)m_default_macro_ht[key];
            }
            return null;
        }
   
    }
}
