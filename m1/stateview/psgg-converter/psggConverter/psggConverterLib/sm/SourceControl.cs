using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

//using psggConverterLib;

public partial class SourceControl  {

    public psggConverterLib.Convert G;
    public bool IsEnd() { return CheckState(S_END);}

    public enum MODE
    {
        UNKNOWN,
        INIT,
        CVT,
        INSERT
    }
    public MODE mode;

    public string m_insert_template_src; //インサート時のテンプレートソース
    public string m_insert_output;       //インサート時の出力バッファ

    #region check mode
    void br_INIT(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (mode == MODE.INIT) SetNextState(st);
        }
    }
    void br_CVT(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (mode == MODE.CVT) SetNextState(st);
        }
    }
    void br_INSERT(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (mode == MODE.INSERT) SetNextState(st);
        }
    }
    #endregion

    #region initialize
    public string m_excel;
    public string m_gendir;
    void load_setting()
    {
        G.TEMSRC = null;
        G.TEMFUNC = null;

        if (!string.IsNullOrEmpty(G.template_src)) {
            var lines = StringUtil.SplitTrimEnd(G.template_src,'\x0a');
            if (lines == null)
            {
                throw new SystemException("Unexpected! {F794458F-407A-490F-9666-B96369567B4C}");
            }
            foreach(var i in lines)
            {
                //                012345678
                if (i.StartsWith(":output="))
                {
                    G.OUTPUT = i.Substring(8).Trim();
                }
                //                012345
                if (i.StartsWith(":enc="))
                {
                    G.ENC = i.Substring(5).Trim();
                }
                //                0123456
                if (i.StartsWith(":lang="))
                {
                    G.LANG= i.Substring(6).Trim();
                }
                //                0123456789
                if (i.StartsWith(":tempsrc=")) //共通のテンプレートソースを使用 __PREFIX__があれば prefixの値に入れ替え
                {
                    G.TEMSRC = i.Substring(9).Trim();
                }
                //                01234567890
                if (i.StartsWith(":tempfunc=")) //共通のテンプレート関数を使用
                {
                    G.TEMFUNC = i.Substring(10).Trim();
                }
                //                012345678
                if (i.StartsWith(":prefix="))
                {
                    G.PREFIX = i.Substring(8).Trim();
                }
                //
                if (i.StartsWith(":end"))
                {
                    break;
                }
            }
        }
        if (!string.IsNullOrEmpty(G.TEMSRC))
        {
            try
            {
                G.template_src = File.ReadAllText(Path.Combine(G.XLSDIR,G.TEMSRC),Encoding.UTF8);
                if (!string.IsNullOrEmpty(G.PREFIX))
                {
                    G.template_src = G.template_src.Replace("__PREFIX__",G.PREFIX);
                }
            } catch (SystemException e)
            {
                throw new SystemException("Error! Template Sourec File not found! " + e.Message);
            }
        }
        if (!string.IsNullOrEmpty(G.TEMFUNC))
        {
            try
            {
                G.template_func = File.ReadAllText(Path.Combine(G.XLSDIR,G.TEMFUNC),Encoding.UTF8);
                if (!string.IsNullOrEmpty(G.PREFIX))
                {
                    G.template_func = G.template_func.Replace("__PREFIX__",G.PREFIX);
                }
            } catch (SystemException e)
            {
                throw new SystemException("Error! Template Function File not found! " + e.Message);
            }
        }
    }
    void need_check_again()
    {
        m_bYesNo =false;
        if (!string.IsNullOrEmpty(G.TEMSRC))
        { 
            G.TEMSRC_save = G.TEMSRC;
            G.TEMSRC = null;
            m_bYesNo = true; // need to check again
        }
        if (!string.IsNullOrEmpty(G.TEMFUNC))
        {
            G.TEMFUNC_save = G.TEMFUNC;
            G.TEMFUNC = null;
        }
    }
    void set_lang()
    {
        if (G.LANG=="vba")
        {
            G.COMMENTLINE_FORMAT = "' {%0}";
        }
    }
    //void read_settinig_ini() 
    //{
    //   SettingIniWork.Init(G.setting_ini); 
    //}
    #endregion
    #region creating source
    string m_src = string.Empty;
    void write_header()
    {
        Func<int,string> sp = (i) => (new string(' ',i));
        if (!string.IsNullOrEmpty(G.PSGGFILE))
        {
            m_src = sp(m_indent) +  G.GetComment(sp(12) + "psggConverterLib.dll converted from psgg-file:") + G.PSGGFILE + G.NEWLINECHAR + G.NEWLINECHAR;
        }
        else
        { 
            m_src = sp(m_indent) +  G.GetComment(sp(12) + "psggConverterLib.dll converted from " +  m_excel + ".") + G.NEWLINECHAR + G.NEWLINECHAR;
        }
    }
    void escape_to_char()
    {
        //バッファ内の\xXXを変換
        var res = string.Empty;
        Func<int,string> getstr4 = (i) => {
            if (i<m_src.Length-4)
            {
                return m_src.Substring(i,4);
            }
            return null;
        };
        for(var index = 0; index < m_src.Length; index++ )
        {
            var c = m_src[index];
            if (c=='\\')
            {
                var sample = getstr4(index);
                if (!string.IsNullOrEmpty(sample))
                { 
                    if (RegexUtil.IsMatch(@"\\x[0-9a-fA-F]{2}",sample))
                    {
                        var code = Convert.ToInt32(sample.Substring(2),16);
                        c  = (char)code;
                        index += 3;
                    }
                }
            }
            res += c.ToString();
        }
        m_src = res;
    }
    void write_file()
    {
        var path = Path.Combine(m_gendir,G.OUTPUT);

        var dir  = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllText(path,m_src,Encoding.GetEncoding(G.ENC));
    }
    void write_insertbuf()
    {
        m_insert_output = m_src;
    }
    #endregion
    #region create_source
    string m_contents1=string.Empty;
    string m_contents2=string.Empty;
    void create_contents1()
    {
        var state_list = new List<string>( G.state_list);
        state_list.Sort();
        var s = string.Empty;
        foreach(var state in state_list)
        {
            s += state + "," + G.NEWLINECHAR;
        }
        m_contents1 = s;
    }
    void create_contents2()
    {
        var state_list = new List<string>(G.state_list);
        state_list.Sort();
        var s = string.Empty;
        foreach(var state in state_list)
        {
            s += G.CreateFuncInternal(state) + G.NEWLINECHAR;
        }
        m_contents2 = s;
    }
    string create_regex_contents(string regex, string macrobuf=null)
    {
        var state_list = new List<string>();
        G.state_list.ForEach(i=> {
            var a = RegexUtil.Get1stMatch(regex,i);
            if (!string.IsNullOrEmpty(a))
            {
                state_list.Add(i);
            }
        });
        state_list.Sort();
        var s = string.Empty;
        foreach (var state in state_list)
        {
            var o = G.CreateFuncInternal(state,macrobuf);
            if (!string.IsNullOrEmpty(o))
            {
                s += o + G.NEWLINECHAR;
            }
        }
        return s;
    }
    #endregion
    #region line convert

    string       m_targetsrc        = null;
    List<string> m_resultlist       = null;
    List<string> m_lines            = null;
    bool         m_needCheckAgain   = false;
    bool         m_bHeadColonIsCode = false;
    int          m_line_index       = 0;
    string       m_line             = null;
    bool         m_bContinue        = false;
    bool         m_bOkNg            = false;
    bool         m_bYesNo           = false;
    void setup_buffer_lc()
    {
        m_targetsrc = G.template_src;
    }

    void setup_buffer_lc_insert()
    {
        m_targetsrc = m_insert_template_src;
    }

    void setup_split_lc()
    {
        m_resultlist = new List<string>();
        m_lines      = StringUtil.SplitTrimEnd(m_targetsrc,'\x0a');
        m_line_index = 0;
        m_needCheckAgain = false;
    }
    void checkcount_lc()
    {
        m_bOkNg = m_line_index < m_lines.Count;
    }
    void lines_to_buf()
    {
        m_targetsrc = StringUtil.LineToBuf(m_resultlist, G.NEWLINECHAR);
    }
    void br_OK(Action<bool> st)
    {
        if (m_bOkNg)
        {
            SetNextState(st);
        }
    }
    void br_NG(Action<bool> st)
    {
        if (!m_bOkNg)
        {
            SetNextState(st);
        }
    }
    void check_again_lc()
    {
        m_bYesNo = m_needCheckAgain;
    }
    void br_YES(Action<bool> st)
    {
        if (m_bYesNo)
        {
            SetNextState(st);
        }
    }
    void br_NO(Action<bool> st)
    {
        if (!m_bYesNo)
        {
            SetNextState(st);
        }
    }
    void bind_src_lc()
    {
        m_src += m_targetsrc;
        m_src += G.NEWLINECHAR;
    }
    void set_check_again()
    {
        m_needCheckAgain = true;
    }
    void next_lc()
    {
        m_line_index++;
    }
    void getline_lc()
    {
        m_line = m_lines[m_line_index];
        m_bContinue = false;
    }
    void is_end_lc()
    {
        if (m_line.StartsWith(":end"))
        {
            m_bHeadColonIsCode = true;
            m_bContinue        = true;
        }
    }
    void br_CONTINUE(Action<bool> st)
    {
        if (m_bContinue)
        {
            SetNextState(st);
        }
    }
    void br_NOTABOVE(Action<bool> st)
    {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }
    void is_comment()
    {
        if (!m_bHeadColonIsCode)
        {
            if (m_line.StartsWith(":"))
            {
                m_bContinue = true;
            }
        }
    }
    void is_contents_1_lc()
    {
#if obs
        if (m_line.Contains(G.CONTENTS1))
        {
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,G.CONTENTS1,m_contents1);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
#endif
        var match = RegexUtil.Get1stMatch(G.CONTENTS1PTN,m_line);
        if (!string.IsNullOrEmpty(match) )
        {
            var macro = string.Empty;
            if (match.Contains("->@"))
            {
                var index = match.IndexOf("->@");
                if (index >= 0)
                {
                    macro = match.Substring(index + 3).Trim('$');
                }
            }
            var replacevalue = G.get_line_macro_value(macro,m_contents1);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,match,replacevalue);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
    }
    void is_contents_2_lc()
    {
        if (m_line.Contains(G.CONTENTS2))
        {
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,G.CONTENTS2,m_contents2);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
    }
    void is_regex_contents_lc()
    {
        var match = RegexUtil.Get1stMatch(G.REGEXCONT,m_line);
        if (!string.IsNullOrEmpty(match))
        {
            var regex = match.Trim().Substring(2); //"$/"を除去
            regex = regex.Substring(0,regex.Length-2);  // 行末の "/$"を除去
            var c = create_regex_contents(regex);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line, match, c);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
    }
    void is_regex_contents2_lc()
    {
        var match = RegexUtil.Get1stMatch(G.REGEXCONT2,m_line);
        if (!string.IsNullOrEmpty(match))
        {
            //1 正規表現部分の取得
            var regex = RegexUtil.Get1stMatch(@"\$\/.+\/->#",match); // $/正規表現/->#    
            regex = regex.Substring(2);
            regex = regex.Substring(0,regex.Length - 4);

            //マクロ名の取得
            var macroname = RegexUtil.Get1stMatch(@"#.+\$$",match);  // #macro$
            macroname = macroname.TrimEnd('$');

            var macrobuf = G.getMacroValueFunc(macroname);
            if (string.IsNullOrEmpty(macrobuf)) {
                throw new SystemException("Macro is not defined. : " + macroname);
            }
            var c = create_regex_contents(regex,macrobuf);

            var tmplines = StringUtil.ReplaceWordsInLine(m_line, match, c);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
    }
    void is_prefix_lc()
    {
        if (m_line.Contains(G.PREFIXMACRO))
        {
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,G.PREFIXMACRO,G.PREFIX);
            m_resultlist.AddRange(tmplines);
            m_bContinue = true;
        }
    }
    psggConverterLib.MacroWork m_mw;
    void is_include_lc()
    {
        if (m_mw == null)
        {
            m_mw = new psggConverterLib.MacroWork();
        }
        m_mw.Init();
        m_mw.CheckMacro(m_line);
        if (m_mw.IsValid() && m_mw.IsInclude())
        {
            var matchstr = m_mw.GetMatchStr();
            var file     = m_mw.GetIncludFilename();
            var enc      = m_mw.GetIncludeFileEnc();
            var text     = IncludeFile.readfile(G, matchstr, file, enc);

            m_resultlist.Add(G.GetComment(" #start include -" + file));

            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlist.AddRange(tmplines);

            m_resultlist.Add(G.GetComment(" #end include -" + file));

            m_bContinue = true;
        }

    }
    void is_macro_lc()
    {
        if (m_mw.IsValid() && !m_mw.IsInclude() )
        {
            var matchstr= m_mw.GetMatchStr();
            var text = string.Empty;
            var macroname = m_mw.GetMacroname();
            if (string.IsNullOrEmpty(macroname))
            {
                text = "(error: macroname is null)";
            }
            else
            { 
                text = G.getMacroValueFunc(macroname);
                if (string.IsNullOrEmpty(text))
                {
                    text = string.Format("(error: no value for {0} )", macroname);
                }
                else
                {
                    text = psggConverterLib.MacroWork.Convert(text,0,m_mw.GetArgValueList());
                }
            }
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlist.AddRange(tmplines);           

            m_bContinue      = true;
        }
    }
    void add_line_lc()
    {
        m_resultlist.Add(m_line);
    }
    #endregion
    #region DONOTEDIT MARKの挿入
    void insert_donotedit()
    {
        m_src = G.insert_donotedit(m_src);
    }
    #endregion
}
