//  psggConverterLib.dll converted from CreateControl.xlsx. 
public partial class CreateControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()
    {
        return CheckState(S_END);
    }



    /*
        S_CHECK_FOLDERS
        docとgen フォルダが設定済みであれば、g4nextをenableにする
    */
    void S_CHECK_FOLDERS(bool bFirst)
    {
        if (bFirst)
        {
            enable_g4next_if_has_folders();
        }
        if (!HasNextState())
        {
            SetNextState(S_WAIT_OBJECT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_CHECK_PREFIX
        Prefixがあれば、g2nextをenableへ
    */
    void S_CHECK_PREFIX(bool bFirst)
    {
        if (bFirst)
        {
            //enable_g2next_if_has_prefix();
            enable_g2next_if_has_statemachinename();
        }
        br_OK(S_CHECK_FOLDERS);
        br_NG(S_WAIT_OBJECT);
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
        S_INIT
        初期化
    */
    void S_INIT(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_WAIT_OBJECT);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SET_DISABLES
        対象ボタンの状態をDisable
    */
    void S_SET_DISABLES(bool bFirst)
    {
        if (bFirst)
        {
            set_disables();
        }
        if (!HasNextState())
        {
            SetNextState(S_CHECK_PREFIX);
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
            GoNextState();
        }
    }
    /*
        S_WAIT_OBJECT
        ボタン等の入力待ち
        対象は・・NEXTとBACK
    */
    void S_WAIT_OBJECT(bool bFirst)
    {
        if (bFirst)
        {
            clear_input();
        }
        if (!wait_object()) return;
        if (!HasNextState())
        {
            SetNextState(S_SET_DISABLES);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }

}

