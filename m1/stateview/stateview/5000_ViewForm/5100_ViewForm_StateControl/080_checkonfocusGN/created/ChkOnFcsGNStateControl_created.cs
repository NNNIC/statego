//  psggConverterLib.dll converted from ChkOnFcsGNStateControl.xlsx.    psgg-file:..\doc\ChkOnFcsGNStateControl.psgg
public partial class ChkOnFcsGNStateControl {

    /*
        S_CheckMove
        マウス移動チェック
    */
    void S_CheckMove(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_move()) return;
                /*
                    ?
                    ?
                    ?
                    ?
                */
                br_IsDrag(S_Def_Drag);
                br_IsDClick(S_Def_DClick);
                br_IsClick(S_Def_Click);
                br_NotAbove(S_Def_Cancel);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Def_Cancel
        キャンセル決定
    */
    void S_Def_Cancel(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_Cancel();
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
        S_Def_Click
        クリック決定
    */
    void S_Def_Click(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_Click();
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
        S_Def_CLICK_ON_STATE
        ステート上でクリック
    */
    void S_Def_CLICK_ON_STATE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_Click_onState();
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
        S_Def_DClick
        ダブルクリック決定
    */
    void S_Def_DClick(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_DClick();
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
        S_Def_Drag
        ドラッグ決定
    */
    void S_Def_Drag(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_Drag();
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
        S_IsMBDown
        マウスボタンダウン待ち
    */
    void S_IsMBDown(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_mbdown()) return;
                /*
                    ?
                    ?
                    ?
                */
                br_IsMBD(S_SavePos);
                br_IsMBD_onState(S_Def_CLICK_ON_STATE);
                br_NotAbove(S_Def_Cancel);
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
        S_SavePos
        現ポジションセーブ
    */
    void S_SavePos(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                save_pos();
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
                SetNextState(S_CheckMove);
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
                SetNextState(S_IsMBDown);
                GoNextState();
                return;
            }
        }
    }

}
