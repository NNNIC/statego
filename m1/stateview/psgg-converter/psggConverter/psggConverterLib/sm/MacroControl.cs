using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class MacroControl  {
		
    public bool                     IsEnd()     { return CheckState(S_END); }
    public psggConverterLib.Convert G;	
    public bool                     m_bOkNg;
    public bool                     m_bYesNo;
    public bool                     m_bDone;
    public string                   m_state;
    public List<string>             m_lines;
    public List<string>             m_resultlines;
    int                             m_lineIndex;
    string                          m_line;
    psggConverterLib.MacroWork      m_mw;

    public bool                     m_bNeedCheckAgain;

    void lineloop_init()
    {
        m_resultlines = new List<string>();
        m_lineIndex = 0;
    }
    void lineloop_check()
    {
        m_bOkNg = m_lineIndex < m_lines.Count;    
    }
    void lineloop_next()
    {
        m_lineIndex++;
    }

    void set_line()
    {
        m_line       = m_lines[m_lineIndex];
        m_bNeedCheckAgain = false;
    }

    void check_macro()
    {
        if (m_mw == null) { 
            m_mw = new psggConverterLib.MacroWork();
        }
        m_mw.Init();
        m_bYesNo = m_mw.CheckMacro(m_line);
    }

    void set_checkagain()
    {
        m_bNeedCheckAgain = true;
    }

    void do_if_include()
    {
        m_bDone = false;
        if (m_mw.IsInclude())
        {
            var matchstr = m_mw.GetMatchStr();
            var file     = m_mw.GetIncludFilename();
            var enc      = m_mw.GetIncludeFileEnc();
            var text     = IncludeFile.readfile(G, matchstr, file, enc);

            m_resultlines.Add(G.GetComment(" #start include -" + file));

            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);

            m_resultlines.AddRange(tmplines);

            m_resultlines.Add(G.GetComment(" #end include -" + file));

            m_bDone = true;
        }
    }
    void do_if_lcuc()
    {
        m_bDone = false;
        if (m_mw.IsLcUc())
        {
            var matchstr = m_mw.GetMatchStr();
            var text     = m_mw.GetLcUcText();
            var ucOrlc = matchstr.StartsWith("$uc");

            var text2 = StringUtil.convert_to_camel_word(text,ucOrlc);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text2);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
    }

    void do_if_prefix()
    {
        m_bDone = false;
        if (m_mw.IsPrefix())
        {
            var matchstr= m_mw.GetMatchStr();
            var text = G.PREFIX;
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
    }

    void do_if_statemachine()
    {
        m_bDone = false;
        if (m_mw.IsStatemachine()) // $statemachine$
        {
            var matchstr= m_mw.GetMatchStr();
            var text = G.STATEMACHINE;
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
        else if (m_mw.Is_state_machine()) // $state_machine$ スネーク型へ
        {
            var matchstr= m_mw.GetMatchStr();
            var text = StringUtil.convert_to_snake_word_and_lower(G.STATEMACHINE);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
        else if (m_mw.Is_stateMachine()) // $stateMachine$ Lower Camel
        {
            var matchstr= m_mw.GetMatchStr();
            var text = StringUtil.convert_to_camel_word(G.STATEMACHINE, false);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
        else if (m_mw.Is_StateMachine()) // $StateMachine$ Upper Camel
        {
            var matchstr= m_mw.GetMatchStr();
            var text = StringUtil.convert_to_camel_word(G.STATEMACHINE, true);
            var tmplines = StringUtil.ReplaceWordsInLine(m_line,matchstr,text);
            m_resultlines.AddRange(tmplines);
            m_bDone = true;
        }
    }
    void do_macro()
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
        m_resultlines.AddRange(tmplines);
    }

    void add_restlines()
    {
        for(var index = m_lineIndex +1; index < m_lines.Count; index++)
        {
            var line = m_lines[index];
            m_resultlines.Add(line);
        }
    }

    void add_line()
    {
        m_resultlines.Add(m_line);
    }
    #region branch
    void br_OK(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_bOkNg)
            {
                SetNextState(st);
            }
        }
    }
    void br_NG(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (!m_bOkNg)
            {
                SetNextState(st);
            }
        }
    }
    void br_YES(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_bYesNo)
            {
                SetNextState(st);
            }
        }
    }
    void br_NO(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (!m_bYesNo)
            {
                SetNextState(st);
            }
        }
    }
    void br_Done(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_bDone)
            {
                SetNextState(st);
            }
        }
    }
    void br_NotAbove(Action<bool> st)
    {
        if (!HasNextState())
        {
            SetNextState(st);
        }
    }
    #endregion
}
