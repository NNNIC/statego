//             psggConverterLib.dll converted from psgg-file:..\doc\EditForm_ClickControl.psgg

public partial class EditForm_ClickControl  : StateControlBase {

    /*
        S_EDT_BMP
        ビットマップ編集
        Ｄ＆Ｄを想定
    */
    void S_EDT_BMP(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                bmpdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EDT_BST
        basestate変更
        ステートを選択
        または無し
    */
    void S_EDT_BST(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                bstdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EDT_GSS
        gosubstate変更
        ステートを選択
        または無し
    */
    void S_EDT_GSS(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                gssdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EDT_NST
        nextstate変更
        ステートを選択
        または無し
    */
    void S_EDT_NST(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                nstdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EDT_ST
        STATE編集
        重複確認
        データ内全走査変更
    */
    void S_EDT_ST(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                stdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EDT_TEXT
        テキスト編集
    */
    void S_EDT_TEXT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                txtdlg_open();
            }
            else {
                if (!wait_close()) return;
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_END
        終了
    */
    void S_END(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            else {
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_START
        開始
    */
    void S_START(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                input_check();
            }
            else {
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_tumb(S_EDT_BMP);
                br_state(S_EDT_ST);
                br_nextstate(S_EDT_NST);
                br_basestate(S_EDT_BST);
                br_gosubstate(S_EDT_GSS);
                br_branch(S_EDT_TEXT);
                br_other(S_EDT_TEXT);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }

}
