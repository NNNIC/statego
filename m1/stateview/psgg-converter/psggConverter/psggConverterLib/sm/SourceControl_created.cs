//             psggConverterLib.dll converted from psgg-file:SourceControl.psgg

public partial class SourceControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }


    /*
        E_OPTIONS
    */
    public bool m_cvthexchar = false;
    /*
        E_OPTIONS1
        インデント数
    */
    public int m_indent;
    /*
        S_ADDLINE_LC
        どれにも当てはまらない場合、ママ追加
    */
    void S_ADDLINE_LC(bool bFirst)
    {
        if (bFirst)
        {
            add_line_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_NEXT_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_BIND_SRC
        SRCにバインド
    */
    void S_BIND_SRC(bool bFirst)
    {
        if (bFirst)
        {
            bind_src_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_ESCAPE_TO_CHAR);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CHECK_AGAIN
        再確認必要か？
    */
    void S_CHECK_AGAIN(bool bFirst)
    {
        if (bFirst)
        {
            check_again_lc();
        }
        br_YES(S_SETUP2_LC);
        br_NO(S_BIND_SRC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CHECKCOUNT_LC
        行変換のカウンタ確認
    */
    void S_CHECKCOUNT_LC(bool bFirst)
    {
        if (bFirst)
        {
            checkcount_lc();
        }
        br_OK(S_GETLINE_LC);
        br_NG(S_LINESTOBUF_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CHECKMODE
        選択
        >初期化モード
        >変換モード
    */
    void S_CHECKMODE(bool bFirst)
    {
        if (bFirst)
        {
        }
        br_INIT(S_LOADSETTING);
        br_CVT(S_WRITEHEDDER);
        br_INSERT(S_WRITEHEDDER);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CONTENTS_1
        コンテンツ①の作成
        コンテンツ①は、カンマ区切りのステートリスト
    */
    void S_CONTENTS_1(bool bFirst)
    {
        if (bFirst)
        {
            create_contents1();
        }
        if (!HasNextState())
        {
            SetNextState(S_CONTENTS_2);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CONTENTS_2
        コンテンツ②の作成
        コンテンツ②は全ステートを関数テンプレートにて展開
    */
    void S_CONTENTS_2(bool bFirst)
    {
        if (bFirst)
        {
            create_contents2();
        }
        if (!HasNextState())
        {
            SetNextState(S_SETUP_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CREATESOURCE
        ソース生成スタート
    */
    void S_CREATESOURCE(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_CONTENTS_1);
        }
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
        if (bFirst)
        {
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_ESCAPE_TO_CHAR
        エスケープ文字を変換
    */
    void S_ESCAPE_TO_CHAR(bool bFirst)
    {
        if (bFirst)
        {
            if (m_cvthexchar)
            {
                escape_to_char();
            }
        }
        if (!HasNextState())
        {
            SetNextState(S_OUTPUTCHECK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_GETLINE_LC
        一行取得
    */
    void S_GETLINE_LC(bool bFirst)
    {
        if (bFirst)
        {
            getline_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_IS_END_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_INS_DONOTEDIT
    */
    void S_INS_DONOTEDIT(bool bFirst)
    {
        if (bFirst)
        {
            insert_donotedit();
        }
        if (!HasNextState())
        {
            SetNextState(S_OUTPUT_INSERTBUF);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_COMMENT
        コメントの確認
        但し、:end以後の場合はコードとして扱う
    */
    void S_IS_COMMENT(bool bFirst)
    {
        if (bFirst)
        {
            is_comment();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_CONTENTS_1_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_CONTENTS_1_LC
        コンテンツ①確認
    */
    void S_IS_CONTENTS_1_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_contents_1_lc();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_CONTENTS_2_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_CONTENTS_2_LC
        コンテンツ②確認
    */
    void S_IS_CONTENTS_2_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_contents_2_lc();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_REGEX_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_END_LC
        :endを確認
        以後、":"を行要素とみなす。
    */
    void S_IS_END_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_end_lc();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_COMMENT);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_INCLUDE_LC
        インクルード確認
    */
    void S_IS_INCLUDE_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_include_lc();
        }
        br_CONTINUE(S_SET_CHECKAGAIN);
        br_NOTABOVE(S_IS_MACRO_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_MACRO_LC
        マクロ確認
    */
    void S_IS_MACRO_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_macro_lc();
        }
        br_CONTINUE(S_SET_CHECKAGAIN);
        br_NOTABOVE(S_ADDLINE_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_PREFIX
        確認
    */
    void S_IS_PREFIX(bool bFirst)
    {
        if (bFirst)
        {
            is_prefix_lc();
        }
        br_CONTINUE(S_SET_CHECKAGAIN);
        br_NOTABOVE(S_IS_INCLUDE_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_REGEX_LC
        正規表現コンテンツ確認
    */
    void S_IS_REGEX_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_regex_contents_lc();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_REGEX2_LC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_IS_REGEX2_LC
        正規表現コンテンツ確認②
    */
    void S_IS_REGEX2_LC(bool bFirst)
    {
        if (bFirst)
        {
            is_regex_contents2_lc();
        }
        br_CONTINUE(S_NEXT_LC);
        br_NOTABOVE(S_IS_PREFIX);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LINESTOBUF_LC
        ラインリストをバッファに
    */
    void S_LINESTOBUF_LC(bool bFirst)
    {
        if (bFirst)
        {
            lines_to_buf();
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECK_AGAIN);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LOADSETTING
        設定読込み
    */
    void S_LOADSETTING(bool bFirst)
    {
        if (bFirst)
        {
            load_setting();
        }
        need_check_again();
        br_YES(S_LOADSETTING);
        br_NO(S_SETLANG);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEXT_LC
        ループＮＥＸＴ処理
    */
    void S_NEXT_LC(bool bFirst)
    {
        if (bFirst)
        {
            next_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECKCOUNT_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_OUTPUT_INSERTBUF
        インサートバッファへ
    */
    void S_OUTPUT_INSERTBUF(bool bFirst)
    {
        if (bFirst)
        {
            write_insertbuf();
        }
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_OUTPUTCHECK
        モード別に出力を変える
    */
    void S_OUTPUTCHECK(bool bFirst)
    {
        if (bFirst)
        {
        }
        br_CVT(S_WRITEFILE);
        br_INSERT(S_INS_DONOTEDIT);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SET_CHECKAGAIN
        再変換要素がある可能性があるので、再チェック依頼
    */
    void S_SET_CHECKAGAIN(bool bFirst)
    {
        if (bFirst)
        {
            set_check_again();
        }
        if (!HasNextState())
        {
            SetNextState(S_NEXT_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETLANG
        言語設定
    */
    void S_SETLANG(bool bFirst)
    {
        if (bFirst)
        {
            set_lang();
        }
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETUP_LC
        チェックモード
    */
    void S_SETUP_LC(bool bFirst)
    {
        if (bFirst)
        {
        }
        br_CVT(S_USE_G_TEMPLSRC);
        br_INSERT(S_USE_INSERT_TEMP);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETUP2_LC
        バッファを行に分割
    */
    void S_SETUP2_LC(bool bFirst)
    {
        if (bFirst)
        {
            setup_split_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECKCOUNT_LC);
        }
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
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECKMODE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_USE_G_TEMPLSRC
        行変換準備
        lc = LineConvert
    */
    void S_USE_G_TEMPLSRC(bool bFirst)
    {
        if (bFirst)
        {
            setup_buffer_lc();
        }
        if (!HasNextState())
        {
            SetNextState(S_SETUP2_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_USE_INSERT_TEMP
        行変換準備
        lc = LineConvert
    */
    void S_USE_INSERT_TEMP(bool bFirst)
    {
        if (bFirst)
        {
            setup_buffer_lc_insert();
        }
        if (!HasNextState())
        {
            SetNextState(S_SETUP2_LC);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_WRITEFILE
        ファイル書込み
    */
    void S_WRITEFILE(bool bFirst)
    {
        if (bFirst)
        {
            write_file();
        }
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_WRITEHEDDER
        ヘッダ―記入
    */
    void S_WRITEHEDDER(bool bFirst)
    {
        if (bFirst)
        {
            write_header();
        }
        if (!HasNextState())
        {
            SetNextState(S_CREATESOURCE);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
