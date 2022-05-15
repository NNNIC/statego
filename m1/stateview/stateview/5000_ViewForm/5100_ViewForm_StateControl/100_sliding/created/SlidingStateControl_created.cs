//             psggConverterLib.dll converted from psgg-file:..\doc\SlidingStateControl.psgg

public partial class SlidingStateControl {

    /*
        S_DESTROY
        開始
    */
    void S_DESTROY(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                hide_pb();
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
        S_INIT
        開始
    */
    void S_INIT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                show_pb();
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
                SetNextState(S_UPDATE);
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
        S_UPDATE
        更新
    */
    void S_UPDATE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                update_pb();
                if (!check_mouse()) return;
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

}
