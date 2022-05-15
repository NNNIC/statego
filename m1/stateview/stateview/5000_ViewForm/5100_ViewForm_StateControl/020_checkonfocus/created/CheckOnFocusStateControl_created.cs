//             psggConverterLib.dll converted from psgg-file:..\doc\CheckOnFocusStateControl.psgg

public partial class CheckOnFocusStateControl {

    /*
        S_CHECKEVENT
        イベント確認
    */
    void S_CHECKEVENT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                check_event();
                if (!wait_has_event()) return;
                br_Click(S_SETEVT_CLICK);
                br_DClick(S_SETEVT_DCLICK);
                br_Drag(S_SETEVT_DRAG);
                br_NotAbove(S_SETEVT_CANCEL);
                if (HasNextState())
                {
                    NoWait();
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
            //else
            {
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_INIT_VARS
        経過時間,現ポイント記録
    */
    void S_INIT_VARS(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                init_vars();
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_CHECKEVENT);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_SETEVT_CANCEL
        キャンセル
    */
    void S_SETEVT_CANCEL(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                setevt_cancel();
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_END);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_SETEVT_CLICK
        クリックイベント決定
    */
    void S_SETEVT_CLICK(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                setevt_click();
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_END);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_SETEVT_DCLICK
        クリックイベント決定
    */
    void S_SETEVT_DCLICK(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                setevt_dclick();
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_END);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_SETEVT_DRAG
        ドラッグイベント決定
    */
    void S_SETEVT_DRAG(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                setevt_drag();
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_END);
                GoNextState();
                return;
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
            }
            //else
            {
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_WAIT_MBDOWN);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_WAIT_MBDOWN
        マウスボタン押す待ち
    */
    void S_WAIT_MBDOWN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_isMbDown()) return;
                if (HasNextState())
                {
                    NoWait();
                    GoNextState();
                    return;
                }
                NoWait();
                SetNextState(S_INIT_VARS);
                GoNextState();
                return;
            }
        }
    }

}
