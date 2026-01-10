//             psggConverterLib.dll converted from psgg-file:InsertCodeControl.psgg

using System;
using System.Collections.Generic;
using System.IO;
public partial class InsertCodeControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()
    {
        return CheckState(S_END);
    }



    /*
        I_BOM
        BOMの有無
    */
    System.Text.Encoding m_enc = System.Text.Encoding.UTF8;
    /*
        I_BREAKLINE
        改行コード
    */
    string m_bl;
    /*
        I_CURLINENUM
        現在の読み込み行
    */
    int m_cur;
    /*
        I_ERROR
        エラー用ストリング
    */
    string m_error;
    /*
        I_FILEPATH
        ファイルパス
    */
    public string m_filepath;
    /*
        I_LINES
        読み込みソース
    */
    List<string> m_lines;
    /*
        I_MARKS
        マーク文字列
    */
    public string MARK_START;
    public string MARK_END;
    /*
        I_NULLCHECK
        文字列NULLチェック
    */
    bool is_null(string s)
    {
        return string.IsNullOrEmpty(s);
    }
    /*
        S_CHECK_EXIST_NEXTLINE
        次の行があるか？
    */
    void S_CHECK_EXIST_NEXTLINE(bool bFirst)
    {
        // branch
        if (m_cur<m_lines.Count) { SetNextState( S_FIND_STARTMARK ); }
        else { SetNextState( S_SAVE ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CONVERT
        コンバートする
    */
    string m_output;
    void S_CONVERT(bool bFirst)
    {
        if (bFirst)
        {
            m_output = convert(m_indent, m_command);
        }
        // branch
        if (is_null(m_error)) { SetNextState( S_INSERT ); }
        else { SetNextState( S_ERORR ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_ERORR
        エラー
    */
    void S_ERORR(bool bFirst)
    {
        if (bFirst)
        {
            throw new SystemException(m_error);
        }
        //
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_FIND_ENDMARK
        終了マークを探す
    */
    void S_FIND_ENDMARK(bool bFirst)
    {
        if (bFirst)
        {
            m_cur = m_mark_start + 1;
            m_mark_end = find_end_mark();
            if (m_mark_end < 0) {
                m_error = "No end mark";
            }
        }
        // branch
        if (is_null(m_error)) { SetNextState( S_GET_PARAM ); }
        else { SetNextState( S_ERORR ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_FIND_STARTMARK
        開始マークを探す
    */
    int m_mark_start;
    int m_mark_end;
    void S_FIND_STARTMARK(bool bFirst)
    {
        if (bFirst)
        {
            m_mark_start = find_start_mark();
        }
        // branch
        if (m_mark_start>=0) { SetNextState( S_FIND_ENDMARK ); }
        else { SetNextState( S_SAVE ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_GET_PARAM
        開始マークの行からパラメータを収集する
    */
    int m_indent=4;
    string m_command;
    void S_GET_PARAM(bool bFirst)
    {
        if (bFirst)
        {
            var l = m_lines[m_mark_start];
            get_param(l);
        }
        // branch
        if (is_null(m_error)) { SetNextState( S_CONVERT ); }
        else { SetNextState( S_ERORR ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_INSERT
        開始マークと終了マークの間に結果を挿入する。
    */
    void S_INSERT(bool bFirst)
    {
        if (bFirst)
        {
            insert_output();
        }
        //
        if (!HasNextState())
        {
            SetNextState(S_NEXT);
        }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEXT
        次へ
    */
    void S_NEXT(bool bFirst)
    {
        if (bFirst)
        {
            m_cur = m_mark_end + 1;
        }
        //
        if (!HasNextState())
        {
            SetNextState(S_CHECK_EXIST_NEXTLINE);
        }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_READSRC
        対象ソース読み込み
        改行コード保持
    */
    string m_src;
    void S_READSRC(bool bFirst)
    {
        if (bFirst)
        {
            m_error = null;
            m_cur = 0;
            read_file();
        }
        // branch
        if (is_null(m_error)) { SetNextState( S_CHECK_EXIST_NEXTLINE ); }
        else { SetNextState( S_ERORR ); }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SAVE
        ファイルセーブ
    */
    void S_SAVE(bool bFirst)
    {
        if (bFirst)
        {
            save();
        }
        //
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        //
        if (HasNextState())
        {
            GoNextState();
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
            SetNextState(S_READSRC);
        }
        //
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
