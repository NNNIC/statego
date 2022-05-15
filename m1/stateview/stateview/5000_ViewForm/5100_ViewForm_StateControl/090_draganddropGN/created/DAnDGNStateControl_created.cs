//             psggConverterLib.dll converted from psgg-file:..\doc\DAnDGNStateControl.psgg

public partial class DAnDGNStateControl {

    /*
        S_DefCancel
        キャンセル決定
    */
    void S_DefCancel(int phase, bool bFirst)
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
                SetNextState(S_Destroy);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_DefSuccess
        正常
    */
    void S_DefSuccess(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                def_Success();
            }
            //else
            {
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
                SetNextState(S_Destroy);
                GoNextState();
                return;
            }
        }
    }
    /*
        S_Destroy
        終了処理
    */
    void S_Destroy(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                destroy_all();
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
        S_DragMove
        ドラッグ移動
    */
    void S_DragMove(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                move_update();
                if (!wait_mouseany()) return;
                br_IsDrop(S_Drop);
                br_NotAbove(S_DefCancel);
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
        S_Drop
        ドロップ
    */
    void S_Drop(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                ovlpb_hide();
                statebox_redraw();
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
                SetNextState(S_DefSuccess);
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
        S_Init
        グループフォーカスのステート移動開始
    */
    void S_Init(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                ovlbmp_create();
                ovldraw_do();
                ovlpb_setbmp();
                ovlpb_show();
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
                SetNextState(S_DragMove);
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
                SetNextState(S_Init);
                GoNextState();
                return;
            }
        }
    }

}
