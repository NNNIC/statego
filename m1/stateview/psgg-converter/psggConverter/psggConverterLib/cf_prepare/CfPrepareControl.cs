using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace psggConverterLib
{
public partial class CfPrepareControl  {
   
    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			if (IsEnd()) break;
			
			Update();
		}
	}

	#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from CfPrepareControl.xlsx. 
        /*
            E_0003
        */
        int m_findindex;
        List<string> m_targetlines = null;
        /*
            E_0005
        */
        string m_line0;
        bool   m_bValid;
        string m_itemname;
        string m_regex;
        string m_val;
        string m_target;
        /*
            E_INOUT
        */
        public string m_state;
        public List<string> m_lines;
        public bool m_bResult;
        public Convert m_parent;
        /*
            E_USE_NEWWAY
            新手法切替
        */
        bool m_newway = true;
        /*
            S_CHECK_BOT
            ターゲットの先頭文字
        */
        void S_CHECK_BOT(bool bFirst)
        {
            var c = m_target[0];
            // branch
            if (c=='\"') { Goto( S_STR_W_REGEX ); }
            else { Goto( S_ITEM_W_REGEX ); }
        }
        /*
            S_CHECK_BOT1
            ターゲットの先頭文字
             base state : S_CHECK_BOT
        */
        void S_CHECK_BOT1(bool bFirst)
        {
            var c = m_target[0];
            // branch
            if (c=='\"') { Goto( S_STR_W_REGEX1 ); }
            else { Goto( S_ITEM_W_REGEX1 ); }
        }
        /*
            S_CHECK_EOF
            最終行が eof＞＞＞か？
        */
        bool m_bEOF;
        void S_CHECK_EOF(bool bFirst)
        {
            m_bEOF = (m_targetlines[m_targetlines.Count - 1].ToLower().Contains("eof>>>"));
            //
            if (!HasNextState())
            {
                Goto(S_REMOVE_TOPBOT);
            }
        }
        /*
            S_CHECK_EQSTR
            =="文字列"の確認が必要か？
        */
        void S_CHECK_EQSTR(bool bFirst)
        {
            // branch
            if (m_eqstr!=null) { Goto( S_COMPARE_EQSTR ); }
            else { Goto( S_CHECK_ESTR ); }
        }
        /*
            S_CHECK_EQUALS
            開始行の最後が　=="文字列"となってる場合
        */
        string m_eqstr;//==の比較値     ※nullと""は意味が違う。 null時は==""の文字列がなかった意味
        string m_exstr; //抽出結果用
        void S_CHECK_EQUALS(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_exstr = null; //結果クリア
                // 抽出　=="hoge"
                var s=RegexUtil.Get1stMatch(@"==\x22[^\x22]*?\x22$",m_target);
                if (!string.IsNullOrEmpty(s))
                {
                    m_eqstr = s.Substring(2).Trim('\x22');
                    m_target = m_target.Substring(0,m_target.Length - s.Length);
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_BOT1);
            }
        }
        /*
            S_CHECK_ESTR
            結果の存在確認
        */
        void S_CHECK_ESTR(bool bFirst)
        {
            var bOk=!string.IsNullOrEmpty(m_exstr);
            // branch
            if (bOk) { Goto( S_GET_SIZE ); }
            else { Goto( S_REMOVE ); }
        }
        /*
            S_CHECK_EXCEPTION
        */
        void S_CHECK_EXCEPTION(bool bFirst)
        {
            //
            if (bFirst)
            {
                if (m_targetlines.Count < 2) throw new SystemException("Unexpected! {A6446D1F-DFD0-4A63-93C7-299265119AC7}");
            }
            //
            if (!HasNextState())
            {
                Goto(S_INIT2);
            }
        }
        /*
            S_CHECK_LINES
        */
        void S_CHECK_LINES(bool bFirst)
        {
            // branch
            if (m_lines==null) { Goto( S_RETURN_FALSE ); }
            else { Goto( S_FIND_MATCHLINES ); }
        }
        /*
            S_CHECK_VALID_VAL
            対象値が有効か？
        */
        void S_CHECK_VALID_VAL(bool bFirst)
        {
            m_bValid = !string.IsNullOrEmpty(m_val);
            // branch
            if (m_bValid) { Goto( S_IS_VALID_REGEX ); }
            else { Goto( S_REMOVE ); }
        }
        /*
            S_COMPARE_EQSTR
            結果の比較
        */
        void S_COMPARE_EQSTR(bool bFirst)
        {
            var bEq=false;
            //
            if (bFirst)
            {
                if (string.IsNullOrEmpty(m_exstr) && string.IsNullOrEmpty(m_eqstr)) {
                    bEq=true;
                }
                else if (m_exstr == m_eqstr)
                {
                    bEq=true;
                }
            }
            // branch
            if (bEq) { Goto( S_GET_SIZE ); }
            else { Goto( S_REMOVE ); }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_EXEC_REGEX
            正規表現の実行
        */
        void S_EXEC_REGEX(bool bFirst)
        {
            //
            if (bFirst)
            {
                if (m_regex[0]=='/' && m_regex[m_regex.Length-1]=='/')
                {
                    m_regex = m_regex.Substring(1);
                    m_regex = m_regex.Substring(0,m_regex.Length - 1);
                    var match = RegexUtil.Get1stMatch(m_regex,m_val);
                    m_bValid = !string.IsNullOrEmpty(match);
                }
                else
                {
                    m_bValid  = false;
                    throw new SystemException("{59858294-6BCF-45B6-B441-076A5A6041D8}\n" + m_line0);
                }
            }
            // branch
            if (m_bValid) { Goto( S_GET_SIZE ); }
            else { Goto( S_REMOVE ); }
        }
        /*
            S_EXTRUCT
            抽出
        */
        void S_EXTRUCT(bool bFirst)
        {
            //
            if (bFirst)
            {
                if (string.IsNullOrEmpty(m_regex) || string.IsNullOrEmpty(m_val))
                {
                    m_exstr = m_val;
                }
                else
                {
                    m_exstr = RegexUtil.Get1stMatch(m_regex,m_val);
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_EQSTR);
            }
        }
        /*
            S_FIND_MATCHLINES
            ＜＜＜？～＞＞＞に囲まれたバッファ抽出
        */
        void S_FIND_MATCHLINES(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_findindex = -1;
                m_targetlines = StringUtil.FindMatchedLines2(m_lines, "\x3c\x3c\x3c\x3f", "\x3e\x3e\x3e", out m_findindex);
            }
            // branch
            if (m_targetlines==null) { Goto( S_RETURN_FALSE ); }
            else { Goto( S_CHECK_EXCEPTION ); }
        }
        /*
            S_GET_SIZE
            EOFのため先にターゲットラインの行数を求める
        */
        int m_size = 0;
        void S_GET_SIZE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_size = m_targetlines.Count;
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_EOF);
            }
        }
        /*
            S_GOTO_FALSE
        */
        void S_GOTO_FALSE(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_RETURN_FALSE);
            }
        }
        /*
            S_IF_EOF
        */
        void S_IF_EOF(bool bFirst)
        {
            // branch
            if (m_bEOF) { Goto( S_REMOVE_REST ); }
            else { Goto( S_REPLACE ); }
        }
        /*
            S_INIT2
        */
        void S_INIT2(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_line0=m_targetlines[0];
                m_bValid = false;
                m_itemname = string.Empty;
                m_val = string.Empty;
                m_regex = string.Empty;
            }
            //
            if (!HasNextState())
            {
                Goto(S_TARGET);
            }
        }
        /*
            S_IS_VALID_REGEX
        */
        void S_IS_VALID_REGEX(bool bFirst)
        {
            var b = !string.IsNullOrEmpty(m_regex) && m_regex.Length > 2;
            // branch
            if (b) { Goto( S_EXEC_REGEX ); }
            else { Goto( S_REMOVE ); }
        }
        /*
            S_ITEM_W_REGEX
            ＜＜＜？itemname/正規表現/
        */
        void S_ITEM_W_REGEX(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_itemname = RegexUtil.Get1stMatch(@"[0-9a-zA-Z_\-]+", m_target);
                m_regex = m_target.Substring(m_itemname.Length);
                m_val = m_parent.getString2(m_state, m_itemname);
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_VALID_VAL);
            }
        }
        /*
            S_ITEM_W_REGEX1
            ＜＜＜？itemname/正規表現/
             base state : S_ITEM_W_REGEX
        */
        void S_ITEM_W_REGEX1(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_itemname = RegexUtil.Get1stMatch(@"[0-9a-zA-Z_\-]+", m_target);
                m_regex = m_target.Substring(m_itemname.Length);
                m_val = m_parent.getString2(m_state, m_itemname);
            }
            //
            if (!HasNextState())
            {
                Goto(S_NORMALIZE_REGEX);
            }
        }
        /*
            S_NORMALIZE_REGEX
            正規表現があれば、前後の'/'を削除
        */
        void S_NORMALIZE_REGEX(bool bFirst)
        {
            var bOk=false;
            //
            if (bFirst)
            {
                if (string.IsNullOrEmpty(m_regex)) {
                    bOk=true;
                }
                else if (m_regex.Length>2) {
                    if (m_regex[0]=='/' && m_regex[m_regex.Length-1]=='/') {
                        m_regex = m_regex.Substring(1);
                        m_regex = m_regex.Substring(0,m_regex.Length - 1);
                        bOk = true;
                    }
                }
            }
            // branch
            if (bOk) { Goto( S_EXTRUCT ); }
            else { Goto( S_GOTO_FALSE ); }
        }
        /*
            S_REMOVE
        */
        void S_REMOVE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_lines.RemoveRange(m_findindex, m_targetlines.Count);
            }
            //
            if (!HasNextState())
            {
                Goto(S_RETURN_TRUE);
            }
        }
        /*
            S_REMOVE_REST
            以降を削除し、行数を１に
        */
        void S_REMOVE_REST(bool bFirst)
        {
            //
            if (bFirst)
            {
                while (m_lines.Count > m_findindex + 1)
                {
                    m_lines.RemoveAt(m_lines.Count - 1);
                }
                m_size = 1;
            }
            //
            if (!HasNextState())
            {
                Goto(S_REPLACE);
            }
        }
        /*
            S_REMOVE_TOPBOT
            //先頭行と最終行の削除
        */
        void S_REMOVE_TOPBOT(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_targetlines.RemoveAt(0);
                m_targetlines.RemoveAt(m_targetlines.Count - 1);
            }
            //
            if (!HasNextState())
            {
                Goto(S_IF_EOF);
            }
        }
        /*
            S_REPLACE
            変換したものに入れ替え
        */
        void S_REPLACE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_lines = StringUtil.ReplaceLines(m_lines, m_findindex, m_size, m_targetlines);
            }
            //
            if (!HasNextState())
            {
                Goto(S_RETURN_TRUE);
            }
        }
        /*
            S_RETURN_FALSE
        */
        void S_RETURN_FALSE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_bResult = false;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_RETURN_TRUE
        */
        void S_RETURN_TRUE(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_bResult = true;
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_LINES);
            }
        }
        /*
            S_STR_W_REGEX
            ＜＜＜？"文字列"/正規表現/
        */
        void S_STR_W_REGEX(bool bFirst)
        {
            //
            if (bFirst)
            {
                var dqw= RegexUtil.Get1stMatch(@"\x22.*\x22",m_target);
                m_val = dqw.Trim('\x22');
                m_regex = m_target.Substring(dqw.Length);
            }
            //
            if (!HasNextState())
            {
                Goto(S_CHECK_VALID_VAL);
            }
        }
        /*
            S_STR_W_REGEX1
            ＜＜＜？"文字列"/正規表現/
             base state : S_STR_W_REGEX
        */
        void S_STR_W_REGEX1(bool bFirst)
        {
            //
            if (bFirst)
            {
                var dqw= RegexUtil.Get1stMatch(@"\x22.*\x22",m_target);
                m_val = dqw.Trim('\x22');
                m_regex = m_target.Substring(dqw.Length);
            }
            //
            if (!HasNextState())
            {
                Goto(S_NORMALIZE_REGEX);
            }
        }
        /*
            S_TARGET
            ＜＜＜？のターゲットを取得
        */
        void S_TARGET(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_target = RegexUtil.Get1stMatch(@"\<\<\<\?.+\s*$",m_line0);
                m_target = m_target.Substring(4).Trim(); // ＜＜＜？を削除
            }
            //
            if (!HasNextState())
            {
                Goto(S_USE_NEWWAY);
            }
        }
        /*
            S_USE_NEWWAY
        */
        void S_USE_NEWWAY(bool bFirst)
        {
            // branch
            if (m_newway) { Goto( S_CHECK_EQUALS ); }
            else { Goto( S_CHECK_BOT ); }
        }


	#endregion // [PSGG OUTPUT END]

	// write your code below

	//bool m_bYesNo;
	
	//void br_YES(Action<bool> st)
	//{
	//	if (!HasNextState())
	//	{
	//		if (m_bYesNo)
	//		{
	//			Goto(st);
	//		}
	//	}
	//}

	//void br_NO(Action<bool> st)
	//{
	//	if (!HasNextState())
	//	{
	//		if (!m_bYesNo)
	//		{
	//			Goto(st);
	//		}
	//	}
	//}
}
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

