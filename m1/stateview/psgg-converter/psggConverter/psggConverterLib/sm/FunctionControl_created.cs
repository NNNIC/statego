//  psggConverterLib.dll converted from FunctionControl.xlsx. 
public partial class FunctionControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }


    /*
        S_CVTMACRO
        マクロ変換
    */
    void S_CVTMACRO(bool bFirst)
    {
        if (bFirst)
        {
            convert_macro();
        }
        br_NeedAgain(S_LOOPNEXT);
        br_OK(S_POSTPROC);
        br_NG(S_END);
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
        S_LOOP_INIT
        ループ初期化
    */
    void S_LOOP_INIT(bool bFirst)
    {
        if (bFirst)
        {
            loop_init();
        }
        if (!HasNextState())
        {
            SetNextState(S_LOOPCHECK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LOOPCHECK
        ループ確認
    */
    void S_LOOPCHECK(bool bFirst)
    {
        if (bFirst)
        {
            loop_check();
        }
        br_OK(S_PREPROC);
        br_NG(S_END);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_LOOPNEXT
        ループNEXT処理
    */
    void S_LOOPNEXT(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_LOOPCHECK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_POSTPROC
        後処理
        空白行削除
    */
    void S_POSTPROC(bool bFirst)
    {
        if (bFirst)
        {
            postprocess();
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
        S_PREPROC
        バッファの前処理
        <..?xxxx ...>　を処理
    */
    void S_PREPROC(bool bFirst)
    {
        if (bFirst)
        {
            preprocess();
        }
        br_NeedAgain(S_LOOPNEXT);
        br_OK(S_SETVALUE);
        br_NG(S_END);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETBUF
        対象バッファに関数テンプレートを設定する
    */
    void S_SETBUF(bool bFirst)
    {
        if (bFirst)
        {
            set_buf();
        }
        if (!HasNextState())
        {
            SetNextState(S_SPLITBUF);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETMACROBUF
        対象バッファにMACROバッファを設定する
    */
    void S_SETMACROBUF(bool bFirst)
    {
        if (bFirst)
        {
            set_macrobuf();
        }
        if (!HasNextState())
        {
            SetNextState(S_SPLITBUF);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SETVALUE
        値の挿入
        を値に変換する
    */
    void S_SETVALUE(bool bFirst)
    {
        if (bFirst)
        {
            set_value();
        }
        br_NeedAgain(S_LOOPNEXT);
        br_OK(S_CVTMACRO);
        br_NG(S_END);
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SPLITBUF
        バッファをラインに分割
    */
    void S_SPLITBUF(bool bFirst)
    {
        if (bFirst)
        {
            split_buf();
        }
        br_OK(S_LOOP_INIT);
        br_NG(S_END);
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
        br_USE_TEMPFUNC(S_SETBUF);
        br_USE_MACROBUF(S_SETMACROBUF);
        if (HasNextState())
        {
            GoNextState();
        }
    }

}

