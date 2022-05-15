//             psggConverterLib.dll converted from psgg-file:..\doc\GroupFocusStateControl.psgg

public partial class GroupFocusStateControl {

    /*
        S_CHECKMOUSE
        マウス状態確認
    */
    void S_CHECKMOUSE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                check_mouse();
            }
            //else
            {
                br_MBDown(S_CHECKPOS);
                br_MBUp(S_COLLECT);
                br_MBCancel(S_FOCUS_NONE);
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
        S_CHECKPOS
        PBを準備
    */
    void S_CHECKPOS(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                checkpos();
            }
            //else
            {
                br_diff_pos(S_UPDATE_PB);
                br_same_pos(S_ONE_TICK);
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
        S_COLLECT
        フォーカス対象を収集
    */
    void S_COLLECT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                collect_targets();
            }
            //else
            {
                br_Exist(S_DRAW_FOCUS);
                br_None(S_FOCUS_NONE);
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
        S_DESTROY
        PBを削除
    */
    void S_DESTROY(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                destroy();
            }
            //else
            {
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
                SetNextState(S_END);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_DRAW_FOCUS
        フォーカス描画
        ＰＢ内の全ステートをフォーカス描画
    */
    void S_DRAW_FOCUS(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                drow_focuses();
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
                SetNextState(S_DESTROY);
                GoNextState();
                return;
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
        S_FOCUS_NONE
        キャンセル決定
    */
    void S_FOCUS_NONE(int phase, bool bFirst)
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
                SetNextState(S_DESTROY);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_INIT
        PBを準備
    */
    void S_INIT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                init();
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
                SetNextState(S_CHECKPOS);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_ONE_TICK
        ＰＢ領域更新
    */
    void S_ONE_TICK(int phase, bool bFirst)
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
                SetNextState(S_CHECKMOUSE);
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
                SetNextState(S_INIT);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_UPDATE_PB
        ＰＢ領域更新
    */
    void S_UPDATE_PB(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                update_pb();
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
                SetNextState(S_ONE_TICK);
                GoNextState();
                return;
            }
        }
    }

}
