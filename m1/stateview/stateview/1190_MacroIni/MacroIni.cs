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
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview
{
    public class MacroIni
    {
        Hashtable m_default_macro_ht; // settingsの[macro]部分
        Hashtable m_macro_ini_ht;     // __PREFIX__ControlMacro.iniファイル部分
        Hashtable m_gensrc_macro_ht;  // 生成先ソースに定義されたマクロ部分  :psgg-macro-start  :psgg-macro-end

        public void ReadMacroIni()
        {
            var filename = SettingIniUtil.GetMacroIni();
            if (!string.IsNullOrEmpty(filename))
            { 
                try
                {
                    var text = File.ReadAllText(filename, Encoding.UTF8);
                    m_macro_ini_ht      = IniUtil.CreateHashtable(text);
                }
                catch (SystemException e)
                {
                    G.NoticeToUser_warning("File Cannot read :" + filename +":" + e.Message);
                }
            }
            m_default_macro_ht  = SettingIniUtil.GetMacroHash();

            try
            {   //生成ソース内の マクロを収集する。 
                var macroinitext = string.Empty;
                var text = Converter.GetGeneratedSource();
                var lines = text.Split('\xa');

                bool bInMacro = false;
                foreach (var l in lines)
                {
                    if (!bInMacro)
                    {
                        if (l.Contains(WordStorage.Store.macro_start_mark))
                        {
                            bInMacro = true;
                            continue;
                        }
                    }
                    else
                    {
                        if (l.Contains(WordStorage.Store.macro_end_mark))
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
                    G.NoticeToUser_warning("Cannot find " + WordStorage.Store.macro_end_mark +" in "  + Converter.GetGeneratedSourceFileName());
                }
                else
                {
                    m_gensrc_macro_ht = IniUtil.CreateHashtable(macroinitext);
                }

            }
            catch (System.Exception e){
                G.NoticeToUser_warning("File Cannot read :" + Converter.GetGeneratedSourceFileName() + ":" + e.Message);
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
