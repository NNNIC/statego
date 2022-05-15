//             psggConverterLib.dll converted from psgg-file:..\doc\WebAdCheckControl.psgg

using System.Collections;
using System.Collections.Generic;

public partial class WebAdCheckControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }


    /*
        S_END
        終了
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
        S_START
        開始
    */
    void S_START(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_WAIT_REQ);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_WAIT_REQ
        リクエスト待ち
    */
    void S_WAIT_REQ(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!wait_req()) return;
        if (!HasNextState())
        {
            SetNextState(S_WATCH_URL);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_WATCH_URL
        URL監視
    */
    void S_WATCH_URL(bool bFirst)
    {
        if (bFirst)
        {
        }
        watch_url();
        if (!wait_done()) return;
        if (!HasNextState())
        {
            SetNextState(S_END);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
