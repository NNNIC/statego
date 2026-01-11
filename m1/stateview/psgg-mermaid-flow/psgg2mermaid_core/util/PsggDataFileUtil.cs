using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace lib.util
{
    public class PsggDataFileUtil
    {
        public class Item
        {
            // バッファをプレーン分解（結合すれば元ファイル）
            public string m_header_buf;
            public string m_chart_buf;
            public string m_config_buf;
            public string m_tmpsrc_buf;
            public string m_tmpfnc_buf;
            public string m_setting_buf;
            public string m_help_buf;
            public string m_iteminf_buf;
            public string m_bitmap_buf;

            //ソース取得
            public string m_tmpsrc; //ソーステンプレート
            public string m_tmpfnc; //関数テンプレート

            //headerから要素取得関数
            public string get_header(string key) {
                var v = IniUtil.GetValue(key, m_header_buf);
                return (v != null ? v.ToString() : null);
            }

            public string get_config(string key)
            {
                var v = IniUtil.GetValue(key, m_config_buf);
                return (v != null ? v.ToString() : null);
            }
            public string get_setting(string group, string key) { return IniUtil.GetValue(group,key,m_setting_buf); }
            public object get_setting(string group) { return IniUtil.GetValue(group, m_setting_buf); }
            public string get_help(string group, string key) { return IniUtil.GetValue(group, key, m_help_buf); }
            public string get_iteminf(string group, string key) { return IniUtil.GetValue(group,key, m_iteminf_buf); }

            //            
            private Dictionary<string, object> m_chart_ht;
            private List<string> m_chart_state_list;
            private List<string> m_chart_name_list;
            private Dictionary<string, string> m_state_dic;  //state=>state_id
            private Dictionary<string, string> m_name_dic;  //name =>name_id


            // チャートアクセス用
            private void chart_init()
            {
                if (m_chart_ht != null) return;
                m_chart_ht = IniUtil.CreateHashtable(m_chart_buf);
                m_chart_state_list = get_staterow_list();
                m_chart_name_list  = get_namecol_list();

                m_state_dic = new Dictionary<string, string>();
                var stateids = get_stateid_list();
                foreach (var id in stateids)
                {
                    var s = get_chart("id_state_dic", id);
                    if (!string.IsNullOrEmpty(s))
                    {
                        m_state_dic.Add(s, id);
                    }
                }
                m_name_dic = new Dictionary<string, string>();
                var nameids = get_nameid_list();
                foreach (var id in nameids)
                {
                    var n = get_chart("id_name_dic", id);
                    if (!string.IsNullOrEmpty(n))
                    {
                        m_name_dic.Add(n, id);
                    }
                }
            }

            private const int    NAME_COL     = 2;
            private const int    STATE_ROW    = 2;
            private const int    START_COL    = 3;
            public string get_chart_val(int row, int col) // base 1
            {
                chart_init();

                var state = ListUtil.Get(m_chart_state_list, col-1);
                var name  = ListUtil.Get(m_chart_name_list, row-1);

                if (row == STATE_ROW)
                {
                    return state;
                }
                if (col == NAME_COL)
                {
                    return name;
                }

                var v = get_val(state, name);
                return v;
            }
            public string get_chart(string group, string key) {
                chart_init();
                return IniUtil.GetValueFromHashtable(group,key,m_chart_ht); 
            }
            public string get_chart(string key) { 
                chart_init();
                var v = IniUtil.GetValueFromHashtable(key, m_chart_ht);
                return v != null ? v.ToString() : null;

            }
            private List<string> get_staterow_list()
            {
                var val = get_chart("stateid_list");
                var id_list = CsvUtil.GetALineString(val);
                var state_list = new List<string>();
                foreach (var i in id_list)
                {
                    var v = string.Empty;
                    if (!string.IsNullOrEmpty(i))
                    {
                        v = get_chart("id_state_dic", i);
                    }
                    state_list.Add(v);
                }
                return state_list;
            }
            private List<string> get_namecol_list()
            {
                var val = get_chart("nameid_list");
                var id_list = CsvUtil.GetALineString(val);
                var name_list = new List<string>();
                foreach (var i in id_list)
                {
                    var v = string.Empty;
                    if (!string.IsNullOrEmpty(i))
                    {
                        v = get_chart("id_name_dic", i);
                    }
                    name_list.Add(v);
                }
                return name_list;
            }
            private List<string> get_stateid_list()
            {
                var val = get_chart("stateid_list");
                var list = CsvUtil.GetALineString(val);
                return list;
            }
            private List<string> get_nameid_list()
            {
                var val = get_chart("nameid_list");
                var list = CsvUtil.GetALineString(val);
                return list;
            }
            private string get_val(string state, string name)
            {
                if (string.IsNullOrEmpty(state) || string.IsNullOrEmpty(name))
                {
                    return null;
                }

                var sid = DictionaryUtil.Get(m_state_dic, state);
                var nid = DictionaryUtil.Get(m_name_dic, name);
                if (nid == null || sid == null)
                {
                    return null;
                }
                var v = get_chart(sid, nid);
                return v;
            }
            public string GetVal(string state, string name)
            {
                chart_init();
                return get_val(state, name);
            }
            public List<string> GetAllStates()
            {
                chart_init();
                var list = new List<string>();
                foreach (var k in m_state_dic.Keys)
                {
                    list.Add(k);
                }
                return list;
            }
            public List<string> GetAllNames()
            {
                chart_init();
                var list = new List<string>();
                foreach (var k in m_name_dic.Keys)
                {
                    list.Add(k);
                }
                return list;
            }
            //
            public string GetGeneratedSource(string doc_path)
            {
                var gensrc  = get_setting(wordstrage.Store.settingini_group_setting, wordstrage.Store.settingini_setting_gensrc);
                var genrdir = get_setting(wordstrage.Store.settingini_group_setupinfo, wordstrage.Store.settingini_setupinfo_genrdir);
                var path2 = Path.Combine(doc_path, genrdir, gensrc);
                var path3 = Path.GetFullPath(path2);
                return path3;
            }
            public string GetGenDir(string doc_path)
            {
                return Path.GetDirectoryName(GetGeneratedSource(doc_path));
            }
            public string GetGeneratedSourceFileName()
            {
                var gensrc  = get_setting(wordstrage.Store.settingini_group_setting, wordstrage.Store.settingini_setting_gensrc);
                return gensrc;
            }
            public string GetIncDir(string doc_path)
            {
                var rdir = get_setting(wordstrage.Store.settingini_group_setupinfo, wordstrage.Store.settingini_setupinfo_incrdir);
                if (string.IsNullOrEmpty(rdir)) return null;
                var path2 = Path.Combine(doc_path, rdir);
                return Path.GetFullPath(path2);
            }
            public string GetCodeOutputStart()
            {
                return get_setting(wordstrage.Store.settingini_group_setupinfo, wordstrage.Store.settingini_setupinfo_code_output_start);
            }
            public string GetCodeOutputEnd()
            {
                return get_setting(wordstrage.Store.settingini_group_setupinfo, wordstrage.Store.settingini_setupinfo_code_output_end);
            }
            public string GetSrcEnc()
            {
                return get_setting(wordstrage.Store.settingini_group_setting, wordstrage.Store.settingini_setting_src_enc);
            }
            public string GetStatemachine()
            {
                return get_setting(wordstrage.Store.settingini_group_setupinfo, wordstrage.Store.settingini_setupinfo_statemachine);
            }

            List<string> m_editor_name_list;  //StateGo Editor と同扱い
            List<int>    m_editor_row_list;   //StateGo Editor と同扱い
            List<string> m_editor_state_list; //StateGo Editor と同扱い
            List<int>    m_editor_col_list;   //StateGo Editor と同扱い
            void editor_list_init()
            {
                chart_init();
                if (m_editor_name_list != null) return;
                m_editor_name_list     = new List<string>();
                m_editor_row_list = new List<int>();

                //for(var row = 1; row <= 1000; row++)
                var row = 0;
                while(row <= 1000)
                {
                    row++;
                    
                    var s = get_chart_val(row,NAME_COL);
                    if (s==null) continue;
                    s = s.Trim();
                    if (String.IsNullOrEmpty(s)) continue;
                    var c = (int)(s.ToLower())[0];
                    if (c=='!' || c=='_' || (c>='a' && c<='z'))
                    {
                        m_editor_name_list.Add(s);
                        m_editor_row_list.Add(row);
                    }
                }
                //
                m_editor_state_list = new List<string>();
                m_editor_col_list = new List<int>();
                //for(var col = START_COL; col <= 10000; col++)
                var col = START_COL - 1;
                while(col <= 10000)
                {
                    col++;

                    var s = get_chart_val(STATE_ROW,col);
                    if (s==null) continue;
                    s = s.Trim();
                    if (string.IsNullOrEmpty(s)) continue;
                    if (StateUtil.IsValidStateName(s))
                    {
                        m_editor_state_list.Add(s);
                        m_editor_col_list.Add(col);
                    }
                }
            }
            public List<string> GetNameList()
            {
                editor_list_init();
                return m_editor_name_list;
            }
            public List<int> GetNameRowList()
            {
                editor_list_init();
                return m_editor_row_list;
            }
            public List<string> GetStateList()
            {
                editor_list_init();
                return m_editor_state_list;
            }
            public List<int> GetStateColList()
            {
                editor_list_init();
                return m_editor_col_list;
            }
        }

        public static Item ReadPsgg(string path)
        {
            var item = new Item();

            var buf = File.ReadAllText(path, Encoding.UTF8);
            var list = new List<string>();
            while (buf != null && buf.Length > 1)
            {
                var index = buf.IndexOf(wordstrage.Store.PSGG_MARK_PREFIX, 1);
                if (index < 0)
                {
                    break;
                }
                var pick = buf.Substring(0, index);
                list.Add(pick);
                buf = buf.Substring(index);
            }
            if (buf != null && buf.Length > 0)
            {
                list.Add(buf);
            }

            var i = 0;
            foreach (var listitem in list)
            {
                if (i == 0)
                {
                    item.m_header_buf = listitem;
                }
                else if (listitem.IndexOf(wordstrage.Store.PSGG_MARK_STATECHART_SHEET) >= 0)
                {
                    item.m_chart_buf = listitem;
                }
                else if (listitem.IndexOf(wordstrage.Store.PSGG_MARK_VARIOUS_SHEET) >= 0) 
                {
                    if (listitem.IndexOf("sheet=config") >= 0 ) 
                    {
                        item.m_config_buf = listitem;
                    }
                    else if (listitem.IndexOf("sheet=template-source") >= 0 ) 
                    {
                        item.m_tmpsrc_buf = listitem;
                    }
                    else if (listitem.IndexOf("sheet=template-statefunc") >= 0) 
                    {
                        item.m_tmpfnc_buf = listitem;
                    }
                    else if (listitem.IndexOf("sheet=setting.ini") >= 0) 
                    {
                        item.m_setting_buf = listitem;
                    }
                    else if (listitem.IndexOf("sheet=help") >= 0) 
                    {
                        item.m_help_buf = listitem;
                    }
                    else if (listitem.IndexOf("sheet=itemsinfo") >= 0) 
                    {
                        item.m_iteminf_buf = listitem;
                    }
                } 
                else if (listitem.IndexOf(wordstrage.Store.PSGG_MARK_BITMAP_DATA) >= 0) 
                {
                    item.m_bitmap_buf = listitem;
                }
                i++;
            }
            Func<string, string> get_tmp = (s) => {
                var begin = wordstrage.Store.PSGG_MARK_VARIOUS_BEGIN;
                var end   = wordstrage.Store.PSGG_MARK_VARIOUS_END;
                var si = s.IndexOf(begin);
                if (si < 0) return null;
                var s1 = s.Substring(si + begin.Length);
                var ei = s1.IndexOf(end);
                if (ei < 0) return null;
                return s1.Substring(0,ei);
            };
            item.m_tmpsrc = get_tmp(item.m_tmpsrc_buf);
            item.m_tmpfnc = get_tmp(item.m_tmpfnc_buf);

            return item;
        }
    }
}
