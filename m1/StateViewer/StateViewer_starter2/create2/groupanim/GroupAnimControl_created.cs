//             psggConverterLib.dll converted from psgg-file:GroupAnimControl.psgg

public partial class GroupAnimControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()
    {
        return CheckState(S_END);
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
        S_G1toG2
    */
    void S_G1toG2(bool bFirst)
    {
        if (bFirst)
        {
            setnext(1,2);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G2toG1
        G1へ
    */
    void S_G2toG1(bool bFirst)
    {
        if (bFirst)
        {
            setback(2,1);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G2toG3
    */
    void S_G2toG3(bool bFirst)
    {
        if (bFirst)
        {
            setnext(2,3);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G2toG6
    */
    void S_G2toG6(bool bFirst)
    {
        if (bFirst)
        {
            setnext(2,6);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G3toG2
    */
    void S_G3toG2(bool bFirst)
    {
        if (bFirst)
        {
            setback(3,2);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G3toG4
    */
    void S_G3toG4(bool bFirst)
    {
        if (bFirst)
        {
            setnext(3,4);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G4toG3
    */
    void S_G4toG3(bool bFirst)
    {
        if (bFirst)
        {
            setback(4,3);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G4toG5
    */
    void S_G4toG5(bool bFirst)
    {
        if (bFirst)
        {
            setnext(4,5);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G4toG6
    */
    void S_G4toG6(bool bFirst)
    {
        if (bFirst)
        {
            setnext(4,6);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G5toG4
    */
    void S_G5toG4(bool bFirst)
    {
        if (bFirst)
        {
            setback(5,4);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G5toG6
    */
    void S_G5toG6(bool bFirst)
    {
        if (bFirst)
        {
            setnext(5,6);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVENEXT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G6toG2
    */
    void S_G6toG2(bool bFirst)
    {
        if (bFirst)
        {
            setback(6,2);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G6toG4
    */
    void S_G6toG4(bool bFirst)
    {
        if (bFirst)
        {
            setback(6,4);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_G6toG5
    */
    void S_G6toG5(bool bFirst)
    {
        if (bFirst)
        {
            setback(6,5);
        }
        if (!HasNextState())
        {
            SetNextState(S_MOVEBACK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_INIT
        initialize
    */
    void S_INIT(bool bFirst)
    {
        if (bFirst)
        {
            init();
        }
        if (!HasNextState())
        {
            SetNextState(S_WAIT_REQUEST);
        }
        if (HasNextState())
        {
                NoWait();
            GoNextState();
        }
    }
    /*
        S_MOVEBACK
        移動
    */
    void S_MOVEBACK(bool bFirst)
    {
        if (bFirst)
        {
            movebk_start(0.1f);
        }
        movebk_update();
        if (!movebk_isdone()) return;
        if (!HasNextState())
        {
            SetNextState(S_WAIT_REQUEST);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_MOVENEXT
        移動
    */
    void S_MOVENEXT(bool bFirst)
    {
        if (bFirst)
        {
            movefwd_start(0.1f);
        }
        movefwd_update();
        if (!movefwd_isdone()) return;
        if (!HasNextState())
        {
            SetNextState(S_WAIT_REQUEST);
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
            SetNextState(S_INIT);
        }
        if (HasNextState())
        {
                NoWait();
            GoNextState();
        }
    }
    /*
        S_WAIT_REQUEST
        リクエスト待ち
    */
    void S_WAIT_REQUEST(bool bFirst)
    {
        if (bFirst)
        {
        }
        br_MOVE(S_G1toG2,"br_G1toG2");
        br_MOVE(S_G2toG3,"br_G2toG3");
        br_MOVE(S_G3toG4,"br_G3toG4");
        br_MOVE(S_G4toG5,"br_G4toG5");
        br_MOVE(S_G5toG6,"br_G5toG6");
        br_MOVE(S_G4toG6,"br_G4toG6");
        br_MOVE(S_G2toG6,"br_G2toG6");
        br_MOVE(S_G6toG5,"br_G6toG5");
        br_MOVE(S_G5toG4,"br_G5toG4");
        br_MOVE(S_G4toG3,"br_G4toG3");
        br_MOVE(S_G3toG2,"br_G3toG2");
        br_MOVE(S_G2toG1,"br_G2toG1");
        br_MOVE(S_G6toG4,"br_G6toG4");
        br_MOVE(S_G6toG2,"br_G6toG2");
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
