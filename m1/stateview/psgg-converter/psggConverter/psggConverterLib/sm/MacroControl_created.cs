//             psggConverterLib.dll converted from psgg-file:MacroControl.psgg

public partial class MacroControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }


    /*
        S_ADDLINE
        結果に現ラインを追加
    */
    void S_ADDLINE(bool bFirst)
    {
        if (bFirst)
        {
            add_line();
        }
        if (!HasNextState())
        {
            SetNextState(S_LINELOOP_NEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_ADDRESTLINES
        残りのライン追加
    */
    void S_ADDRESTLINES(bool bFirst)
    {
        if (bFirst)
        {
            add_restlines();
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
        S_CHECKMACRO
        ライン内にマクロがあるか？
    */
    void S_CHECKMACRO(bool bFirst)
    {
        if (bFirst)
        {
            check_macro();
        }
        br_YES(S_SET_CHECKAGAIN);
        br_NO(S_ADDLINE);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DO_INCLUDE
        インクルードであれば、INCDIRよりファイル取得
    */
    void S_DO_INCLUDE(bool bFirst)
    {
        if (bFirst)
        {
            do_if_include();
        }
        br_Done(S_ADDRESTLINES);
        br_NotAbove(S_DO_LCUC);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DO_LCUC
        LoweCamel Upper
        $lc:,,,$
        $uc:..$
    */
    void S_DO_LCUC(bool bFirst)
    {
        if (bFirst)
        {
            do_if_lcuc();
        }
        br_Done(S_ADDRESTLINES);
        br_NotAbove(S_DO_MACRO);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DO_MACRO
        マクロ処理
    */
    void S_DO_MACRO(bool bFirst)
    {
        if (bFirst)
        {
            do_macro();
        }
        if (!HasNextState())
        {
            SetNextState(S_ADDRESTLINES);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DO_PREFIX
        プリフィックス変換
    */
    void S_DO_PREFIX(bool bFirst)
    {
        if (bFirst)
        {
            do_if_prefix();
        }
        br_Done(S_ADDRESTLINES);
        br_NotAbove(S_DO_STATEMACHINE);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DO_STATEMACHINE
        変換
    */
    void S_DO_STATEMACHINE(bool bFirst)
    {
        if (bFirst)
        {
            do_if_statemachine();
        }
        br_Done(S_ADDRESTLINES);
        br_NotAbove(S_DO_INCLUDE);
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
        S_LINELOOP_CHECK
        index < lines.count
    */
    void S_LINELOOP_CHECK(bool bFirst)
    {
        if (bFirst)
        {
            lineloop_check();
        }
        br_OK(S_SET_LINE);
        br_NG(S_END);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LINELOOP_INIT
        set index 0
    */
    void S_LINELOOP_INIT(bool bFirst)
    {
        if (bFirst)
        {
            lineloop_init();
        }
        if (!HasNextState())
        {
            SetNextState(S_LINELOOP_CHECK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LINELOOP_NEXT
        next処理
    */
    void S_LINELOOP_NEXT(bool bFirst)
    {
        if (bFirst)
        {
            lineloop_next();
        }
        if (!HasNextState())
        {
            SetNextState(S_LINELOOP_CHECK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SET_CHECKAGAIN
        再実行依頼
    */
    void S_SET_CHECKAGAIN(bool bFirst)
    {
        if (bFirst)
        {
            set_checkagain();
        }
        if (!HasNextState())
        {
            SetNextState(S_DO_PREFIX);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SET_LINE
        line = lines[index]
    */
    void S_SET_LINE(bool bFirst)
    {
        if (bFirst)
        {
            set_line();
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECKMACRO);
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
            SetNextState(S_LINELOOP_INIT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
