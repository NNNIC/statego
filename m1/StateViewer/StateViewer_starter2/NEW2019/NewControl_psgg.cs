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
using Microsoft.WindowsAPICodePack.Dialogs;


namespace StateViewer_starter2.NEW2019
{
    public class psgg_data
    {
        public int    m_index; //登録リストのインデックス
        public string m_filepath;

        public string m_version;

        public string m_title_j;
        public string m_title_e;

        public string m_detail_j;
        public string m_detail_e;

        public string m_header;
        public string m_settingini;
        public int    m_settingini_index; //psgg_parts_list内のindex
        public string m_config;
        public int    m_config_index;     //psgg_parts_list内のindex

        public List<string> m_psgg_parts = new List<string>();

        public string m_src_enc; //ソースのエンコード

        //--
        public TreeNode m_node;

        //--
        internal void read(string f)
        {
            m_filepath = f;
            _read(f);
        }

        internal void read_output_psgg_parts()
        {
            m_psgg_parts = _read(m_filepath);
        }

        private List<string> _read(string f)
        {
            var psgg_parts = new List<string>();

            var s = File.ReadAllText(f, Encoding.UTF8);
            var index = 0;
            var parts = string.Empty;
            for(var loop = 0; loop<=100; loop++)
            {
                var index2 = s.IndexOf(WordStorage.Store.PSGG_MARK_PREFIX ,index+WordStorage.Store.PSGG_MARK_PREFIX.Length);
                if (index2>0)
                {
                    var buf = s.Substring(index, index2-index);
                    psgg_parts.Add(buf);
                    index = index2;
                    continue;
                }
                break;
            }
            if (index < s.Length )
            {
                var buf = s.Substring(index);
                psgg_parts.Add(buf);
            }

            //
            m_header = psgg_parts[0];
            m_version = IniUtil.GetValue("version",m_header);
            if (m_version == "1.1")
            {
                m_settingini_index = psgg_parts.FindIndex(
                    _=>_.Contains("sheet=setting.ini") && _.Contains(WordStorage.Store.PSGG_MARK_VARIOUS_SHEET)
                    );
                if (m_settingini_index >= 0)
                { 
                    m_settingini = psgg_parts[m_settingini_index];
                    if (!string.IsNullOrEmpty( m_settingini))
                    {
                        m_title_e = IniUtil.GetValue("en", "title",m_settingini);
                        m_title_j = IniUtil.GetValue("jpn","title",m_settingini);

                        m_detail_e = IniUtil.GetValue("en","detail",m_settingini);
                        m_detail_j = IniUtil.GetValue("jpn","detail",m_settingini);

                        m_src_enc =  IniUtil.GetValue("setting","src_enc",m_settingini); 　　
                    }
                }

                m_config_index = psgg_parts.FindIndex(
                    _=>_.Contains("sheet=config") && _.Contains(WordStorage.Store.PSGG_MARK_VARIOUS_SHEET)
                    );
                if (m_config_index >=0)
                {
                    m_config = psgg_parts[m_config_index];
                }
            }   
            
            return psgg_parts;              
        }
        
        public string get_statemachine_from_settingini()
        {
            return IniUtil.GetValue(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_statemachine, m_settingini);
        }
        public string get_genfileFullpath_from_settingini() //生成ファイルのフルパス psgg相対となっている
        {
            try { 
                var gen_src = IniUtil.GetValue(WordStorage.Store.settingini_group_setting,WordStorage.Store.settingini_setting_gensrc, m_settingini);
                var genrdir = IniUtil.GetValue(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_genrdir, m_settingini);
                var path = Path.Combine( Path.GetDirectoryName(m_filepath), genrdir, gen_src);
            
                var path2 = (new FileInfo(path).FullName);
                return path2;
            } catch (SystemException e)
            {
                return "{37522A6A-D605-4F45-A715-F28F699E99BD} " + e.Message;
            }
        }
        public string get_genhppfileFullpath_from_settingini() //生成ファイルのフルパス psgg相対となっている
        {
            try
            {
                var gen_hpp = IniUtil.GetValue(WordStorage.Store.settingini_group_setting,WordStorage.Store.settingini_setting_genhpp, m_settingini);
                var genrdir = IniUtil.GetValue(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_genrdir, m_settingini);
                var path = Path.Combine( Path.GetDirectoryName(m_filepath), genrdir, gen_hpp);

                var path2 = (new FileInfo(path).FullName);
                return path2;
            }
            catch (SystemException e)
            {
                return "{37522A6A-D605-4F45-A715-F28F699E99BD} " + e.Message;
            }
        }

        public string get_impfileFullpath_from_settingini() //実装ファイルのフルパス 旧仕様
        {
            try { 
                var sub_src = IniUtil.GetValue(WordStorage.Store.settingini_group_setting,WordStorage.Store.settingini_setting_sub_src, m_settingini);

                if (string.IsNullOrEmpty(sub_src))
                {
                    return null;  //なくてもよい
                }

                var genrdir = IniUtil.GetValue(WordStorage.Store.settingini_group_setupinfo,WordStorage.Store.settingini_setupinfo_genrdir, m_settingini);
                var path = Path.Combine( Path.GetDirectoryName(m_filepath), genrdir, sub_src);
            
                var path2 = (new FileInfo(path).FullName);
                return path2;
            } catch (SystemException e)
            {
                return "{E4B0A79A-86D6-4B55-9C29-05310D9F8E29} " + e.Message;
            }
        }
        public string get_macrofileFullpath_from_settingini() //マクロファイルのフルパス
        {
            try
            {
                var macrosrc = IniUtil.GetValue(WordStorage.Store.settingini_group_setting,WordStorage.Store.settingini_setting_macroini, m_settingini);
                if (string.IsNullOrEmpty(macrosrc))
                {
                    return null; //なくてもよい
                }
                var path     = Path.Combine( Path.GetDirectoryName(m_filepath), macrosrc);
                var path2 = (new FileInfo(path).FullName);
                return path2;
            }catch (SystemException e)
            {
                return "{B6EB1E56-4D71-437C-BD56-12B0F733A55B} " + e.Message;
            }
        }
    }
}
