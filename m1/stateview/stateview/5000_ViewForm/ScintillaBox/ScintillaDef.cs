using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using G=stateview.Globals;
using ScintillaNET;

namespace stateview
{
    class ScintillaDef
    {
        Hashtable m_srcht;
        void readfile(string path)
        {
            m_srcht = null;
            if (!File.Exists(path))
            {
                G.NoticeToUser_warning("Cannot find Editor Define File:" + path);
                return;
            }
            var text = File.ReadAllText(path, Encoding.UTF8);
            if (string.IsNullOrEmpty(text))
            {
                G.NoticeToUser_warning("{A5DD367F-3CED-4DBA-9536-EF306F8677E4}" + path);
                return;
            }
            m_srcht = IniUtil.CreateHashtable(text); 
        }
        Hashtable m_valht;
        void create_defines()
        {
            m_valht = new Hashtable();
            if (m_srcht == null)
            {
                G.NoticeToUser("{6523979C-72F3-422A-8DEE-CC07B7DB6611}");
                return;
            }
            var ht = (Hashtable)m_srcht["defines"];
            if (ht == null)
            {
                G.NoticeToUser("{6523979C-72F3-422A-8DEE-CC07B7DB6611}");
                return;
            }
            foreach (var k in ht.Keys)
            {
                m_valht[k.ToString()] = interpreter(ht[k]);
            }
        }
        //
        Color m_bg_normal;
        Color m_bg_readonly;
        Color m_bg_select;
        void create_bg_values()
        {
            if (m_srcht == null)
            {
                G.NoticeToUser("{E4EDBD4D-9B7F-4AC3-8ED4-454AC3E32889}");
                return;
            }
            var ht = (Hashtable)m_srcht["bg-color"];
            if (ht == null)
            {
                G.NoticeToUser("{92F5DF4B-8E13-42D9-A60C-040F9AA259F0}");
                return;
            }
            m_bg_normal   = (Color)interpreter(ht["NORMAL"]);
            m_bg_readonly = (Color)interpreter(ht["READONLY"]);
            m_bg_select   = (Color)interpreter(ht["SELECT"]);
        }
        //
        void setup_editor_color(Scintilla e)
        {
            if (m_srcht == null)
            {
                G.NoticeToUser_warning("{308A51BD-0389-482F-BF9C-706A7A607570}");
                return;
            }

            e.SetSelectionBackColor(true, m_bg_select);

            e.StyleResetDefault();
            setup_editor_color2(e,true);
            e.StyleClearAll();

            setup_editor_color2(e,false);
        }

        private void setup_editor_color2(Scintilla e, bool bDefault)
        {
            foreach (var k in m_srcht.Keys)
            {
                var key = k.ToString();
                if (!key.StartsWith("Style.")) continue;

                if (bDefault)
                {
                    if (key != "Style.Default")
                    {
                        continue;
                    }
                }
                else
                {
                    if (
                        key == "Style.Default"
                        ||
                        key.Contains(m_lexer.ToString()) == false
                        )
                    {
                        continue;
                    }
                }

                var index = GetStyleIndex(key);
                if (index < 0) continue;

                var ht = (Hashtable)m_srcht[key];
                if (ht == null) continue;
                foreach (var k2 in ht.Keys)
                {
                    var val = interpreter(ht[k2]);
                    setStyle(e, (int)index, k2.ToString(), val);
                }
                if (!ht.ContainsKey("BackColor"))
                {
                    var bgcolor = e.ReadOnly ? m_bg_readonly : m_bg_normal;
                    setStyle(e, (int)index, "BackColor", bgcolor);
                }
            }
        }

        Lexer m_lexer;
        void decide_lexer()
        {
            m_lexer = Lexer.Null;

            var curlang = SettingIniUtil.GetLang();
            if (string.IsNullOrEmpty(curlang))
            {
                return;
            }
            curlang = curlang.ToLower();
            var htl = (Hashtable)m_srcht["lexer"];
            if (htl == null)
            {
                return;
            }
            foreach (var k in htl.Keys)
            {
                var lexer_fi = typeof(Lexer).GetField(k.ToString());
                if (lexer_fi == null)
                {
                    G.NoticeToUser("Scintilla Definition Warning ..{D7A432CD-03E7-4640-95E1-2329E40D66B4}" + k.ToString());
                    continue;
                }
                var lexer = (Lexer)lexer_fi.GetValue(null);

                var v = htl[k];
                if (v == null) continue;
                var val = v.ToString();
                if (string.IsNullOrEmpty(val)) continue;
                var tok = val.Split(',');
                foreach (var i in tok)
                {
                    if (string.IsNullOrEmpty(i)) continue;
                    var t = i.Trim();
                    if (t.ToLower() == curlang)
                    {
                        m_lexer = lexer;
                        break;
                    }
                }
            }
        }
        public void set_lexer_keywords(Scintilla editor)
        {
            editor.Lexer = m_lexer;
            if (editor.Lexer !=  Lexer.Null)
            {
                for (var n = 0; n < 10; n++)
                {
                    var v = get_keywords(n);
                    if (v == null) continue;
                    editor.SetKeywords(n, v.ToString());
                }
            }
        }
        private string get_keywords(int n=0)
        {
            var lang = SettingIniUtil.GetLang();
            if (string.IsNullOrEmpty(lang)) return null;
            lang = lang.ToLower();

            foreach (var k in m_srcht.Keys)
            {
                var key = k.ToString();
                if (!key.StartsWith("keywords-"))
                {
                    continue;
                }
                var ht = (Hashtable)m_srcht[k];
                if (ht == null)
                {
                    continue;
                }
                var targetlangsstr = ht["targetlang"]?.ToString();
                if (targetlangsstr == null) continue;
                var targetlangs = StringUtil.SplitTrimSpaces(targetlangsstr, ',');
                if (targetlangs == null) continue;
                targetlangs = StringUtil.ToLower(targetlangs);
                if (Array.IndexOf(targetlangs, lang)>=0)
                {
                    if (ht.ContainsKey(n.ToString()))
                    {
                        var v = ht[n.ToString()]?.ToString();
                        if (string.IsNullOrEmpty(v))
                        {
                            return null;
                        }
                        var words = v.Split(',', ' ', '\x0d', '\x0a','\t');
                        if (words == null)
                        {
                            return null;
                        }
                        words = StringUtil.ToTrimSpaces(words);

                        var all = string.Empty;
                        foreach (var w in words)
                        {
                            if (!string.IsNullOrEmpty(all))
                            {
                                all += " ";
                            }

                            if (!string.IsNullOrEmpty(w))
                            {
                                all += w;
                            }
                        }
                        return all;
                    }
                }
            }
            return null;
        }

        public void Setup(Scintilla editor,bool readonly_color_is_normal /*リードオンリーでもノーマルカラー*/)
        {
            var inifile = Path.Combine(PathUtil.GetThisAppPath(), "ini", "editor_def.ini");
            if (!File.Exists(inifile))
            {
                return;
            }
            readfile(inifile);
            create_defines();
            create_bg_values();
            decide_lexer();
            if (readonly_color_is_normal)
            {
                m_bg_readonly = m_bg_normal;
            }
            setup_editor_color(editor);
            set_lexer_keywords(editor);
        }

        public void SetColors(Scintilla editor)
        {
            setup_editor_color(editor);
        }

        // --- utilities
        object interpreter(object  o)
        {
            if (o == null)
            {
                return null;
            }

            if (!(o is string))
            {
                return o;
            }

            var v = (string)o;
            if (v == null)
            {
                return null;
            }
            v = v.Trim();
            if (string.IsNullOrEmpty(v))
            {
                return null;
            }
            // RGB(NN,NN,NN)
            if (RegexUtil.IsMatch(@"RGB\(.+\)", v))
            {
                var nums = v.Substring(4);
                nums = nums.Substring(0, nums.Length - 1);
                var intlist = CsvUtil.ToIntList(nums);
                if (intlist == null || intlist.Length!=3 || Array.FindIndex(intlist, i => i < 0 || i > 255) >= 0)
                {
                    G.NoticeToUser("{0F6A2C30-A54C-4681-BC12-B3695D2C1C6E}");
                    return null;
                }
                return Color.FromArgb(255, intlist[0], intlist[1], intlist[2]);
            }
            if (v.StartsWith("Color."))
            {
                var name = v.Substring(6);
                var pi = typeof(Color).GetProperty(name);
                if (pi == null)
                {
                    G.NoticeToUser("{C8AB17F5-2F35-449F-A814-3372C37A2D41}");
                    return null;
                }
                var a = pi.GetValue(null);
                return a;
            }
            if (RegexUtil.IsMatch(@"^\#[0-9A-Fa-f]{6}",v))
            {
                var r = Convert.ToInt32(v.Substring(1, 2), 16);
                var g = Convert.ToInt32(v.Substring(3, 2), 16);
                var b = Convert.ToInt32(v.Substring(5, 2), 16);

                return Color.FromArgb(255, r, g, b);
            }
            if (v.StartsWith("\"")) //文字列
            {
                return v.Trim('\"');
            }
            if (RegexUtil.IsMatch(@"^[0-9]+$",v))
            {
                var a = ParseUtil.ParseInt(v);
                return a;
            }
            if (v == "true") return true;
            if (v == "false") return false;
            if (m_valht != null && m_valht.ContainsKey(v))
            {
                return m_valht[v];
            }
            return v;
        }
        int GetStyleIndex(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return -1;
            }
            name = name.Trim();
            if (string.IsNullOrEmpty(name))
            {
                return -1;
            }

            if (!name.StartsWith("Style."))
            {
                return -1;
            }

            if (name == "Style.Default")
            {
                return Style.Default;
            }


            var tok = name.Split('.');
            if (tok.Length < 3)
            {
                return -1;
            }
            var lang = tok[1];
            var val  = tok[2];
            //{
            //    var info = typeof(ScintillaNET.Style.Cpp).AssemblyQualifiedName;
            //    G.NoticeToUser(info);
            //}

            var type = Type.GetType("ScintillaNET.Style+"+lang+", ScintillaNET");
            if (type == null)
            {
                return -1;
            }
            var v = type.GetField(val);
            if (v == null)
            {
                return -1;
            }
            var a = v.GetValue(null);
            if (a == null)
            {
                return -1;
            }
            return (int)a;
        }
        void setStyle(Scintilla e, int index, string name, object val)
        {
            try
            {
                var style = e.Styles[index];
                var pi = typeof(ScintillaNET.Style).GetProperty(name);
                if (pi == null)
                {
                    G.NoticeToUser("{2B84A316-DEBD-4264-A584-25273BCF330A}");
                    return;
                }
                pi.SetValue(style, val);
            }
            catch (SystemException e2)
            {
                G.NoticeToUser_warning("{3E4649CB-5322-4B17-9746-9F1B6BCBBA11}" + e2.Message);
            }
        }
        
    }
}
