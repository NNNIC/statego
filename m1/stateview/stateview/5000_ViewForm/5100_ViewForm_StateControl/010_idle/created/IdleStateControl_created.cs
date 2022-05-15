//             psggConverterLib.dll converted from psgg-file:..\doc\idle.psgg

public partial class IdleStateControl {

    /*
        E_CLR_BRANCH3
        m_brclick_count
        この時間までにDoubleClickがなければbrabch clickが確定する。
        カウンタで、１回につき100msec(Timer)
    */
    int m_brclick_count = 0;
    /*
        S_BR_CLICK_IF
        ブランチクリック決定？
    */
    void S_BR_CLICK_IF(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            m_yesno = false;
            if (m_brclick_count > 0)
            {
                m_brclick_count--;
                if (m_brclick_count == 0) m_yesno = true;
            }
        }
        br_yes(S_SET_ClickOnBranch);
        br_no(S_IsReqReDraw);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CheckClickOnBranch
        クリックした箇所がブランチか？
    */
    void S_CheckClickOnBranch(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckClickOnBranch();
        }
        br_yes(S_SET_LIMIT);
        br_no(S_IsOnState);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CheckNotBranch
        ブランチ上に除外
    */
    void S_CheckNotBranch(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsOnBranch_CheckIsNotInput();
        }
        br_yes(S_CLR_BRANCH2);
        br_no(S_SET_ClickOnBlank);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_ClickOnBlank
        クリックされたか？
    */
    void S_ClickOnBlank(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckClickOnBlank();
        }
        br_yes(S_CheckNotBranch);
        br_no(S_MouseDown);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CLR_BRANCH
        ブランチ情報クリア
    */
    void S_CLR_BRANCH(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_IsReqReDraw);
            ClrBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CLR_BRANCH2
        ブランチ情報クリア
    */
    void S_CLR_BRANCH2(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_ONE_TCIK);
            ClrBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CLR_LIMIT
    */
    void S_CLR_LIMIT(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_END);
            m_brclick_count=0;
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_DoubleClick
        ダブルクリックされたか
    */
    void S_DoubleClick(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckDoubleClick();
        }
        br_yes(S_IsOnBranchDC);
        br_no(S_ClickOnBlank);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_END
    */
    void S_END(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
        }
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_IsCCDragDrop
        コピーコレクションからのドラッグが入ってきた！
    */
    void S_IsCCDragDrop(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckCCDragEnter();
        }
        br_yes(S_SET_REQCCDRAGENTER);
        br_no(S_DoubleClick);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsDragBranch
        ブランチからのドラッグか？
    */
    void S_IsDragBranch(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsDragBranch();
        }
        br_yes(S_SET_DRAGBRANCH);
        br_no(S_ONE_TCIK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsDragInSpace
        空き空間でドラッグ
    */
    void S_IsDragInSpace(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            DragInSpace_init();
        }
        if (!(DragInSpace_wait())) return;
        br_yes(S_SET_DRAGINSPACE);
        br_clickOnBlank(S_SET_ClickOnBlank);
        br_holdMBD(S_SET_HOLDMBD);
        br_gotick(S_ONE_TCIK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsGroupNode
        グループノード？
    */
    void S_IsGroupNode(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsGroupNode();
        }
        br_yes(S_SET_ONGROUPNODE);
        br_no(S_SET_ONSTATE);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsKeyExec
        キー命令か？
    */
    void S_IsKeyExec(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckKeyExec();
        }
        br_yes(S_CLR_LIMIT);
        br_no(S_IsCCDragDrop);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsOnBranch
        ブランチ上にポインタ
    */
    void S_IsOnBranch(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsOnBranch();
        }
        br_yes(S_REC_BRANCH);
        br_no(S_IsRECBRANCH);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsOnBranchDC
        ブランチ上にポインタ
    */
    void S_IsOnBranchDC(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsOnBranch_CheckIsNotInput();
        }
        br_yes(S_SET_DCONBRANCH);
        br_no(S_IsOnStateDC);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsOnState
        ステート上にポインタあり
    */
    void S_IsOnState(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsOnState();
        }
        br_yes(S_IsGroupNode);
        br_no(S_IsOnBranch);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsOnStateDC
        ブランチ上にポインタ
    */
    void S_IsOnStateDC(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsOnState();
        }
        br_yes(S_SET_DCONSTATE);
        br_gotick(S_ONE_TCIK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsRECBRANCH
        ブランチ情報レコード
    */
    void S_IsRECBRANCH(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            Check_RecBranch();
        }
        br_yes(S_IsDragBranch);
        br_no(S_IsDragInSpace);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsReqCenterGroup
        指定グループを中央表示＆フォーカスリクエスト
    */
    void S_IsReqCenterGroup(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckReqCenterFocusGroup();
        }
        br_yes(S_SET_REQREDRAW2);
        br_no(S_IsKeyExec);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsReqCenterState
        特定ステートを中央表示&フォーカスリクエスト
    */
    void S_IsReqCenterState(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckReqCenterFocusState();
        }
        br_yes(S_SET_REQREDRAW1);
        br_no(S_IsReqCenterGroup);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_IsReqReDraw
        再描画依頼か？
    */
    void S_IsReqReDraw(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckIsReqRedraw();
        }
        br_yes(S_SET_REQREDRAW);
        br_no(S_IsReqCenterState);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_MouseDown
        マウスボタン押されているか
    */
    void S_MouseDown(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState();
            CheckMouseDown();
        }
        br_yes(S_CheckClickOnBranch);
        br_no(S_CLR_BRANCH2);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_ONE_TCIK
        １ティック待つ
    */
    void S_ONE_TCIK(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_BR_CLICK_IF);
        }
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_REC_BRANCH
        ブランチ情報レコード
    */
    void S_REC_BRANCH(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_ONE_TCIK);
            RecBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_ClickOnBlank
        決定
        Click On Blank
    */
    void S_SET_ClickOnBlank(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ClickOnBlank();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_ClickOnBranch
    */
    void S_SET_ClickOnBranch(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ClickOnBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_DCONBRANCH
        決定 ONBRANCH
    */
    void S_SET_DCONBRANCH(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_DcOnBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_DCONSTATE
        決定　ONSTATE
    */
    void S_SET_DCONSTATE(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_DcOnState();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_DRAGBRANCH
        決定　DRAGBRANCH
    */
    void S_SET_DRAGBRANCH(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_DragBranch();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_DRAGINSPACE
        空間内決定　DRAGINSPACE
    */
    void S_SET_DRAGINSPACE(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_DragInSpace();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_HOLDMBD
        MB下保持決定HOLDMBD
    */
    void S_SET_HOLDMBD(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_HoldMBD();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_LIMIT
        0.5秒
    */
    void S_SET_LIMIT(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_ONE_TCIK);
            m_brclick_count=7;
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_ONGROUPNODE
        決定　ONGROUPNODE
    */
    void S_SET_ONGROUPNODE(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_OnGroupNode();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_ONSTATE
        決定　ONSTATE
    */
    void S_SET_ONSTATE(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_OnState();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_REQCCDRAGENTER
    */
    void S_SET_REQCCDRAGENTER(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ReqCCDragEnter();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_REQREDRAW
        決定 REQUEST REDRAW
    */
    void S_SET_REQREDRAW(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ReqRedraw();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_REQREDRAW1
        決定 REQUEST REDRAW
    */
    void S_SET_REQREDRAW1(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ReqCenterFocusState();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_SET_REQREDRAW2
        決定 REQUEST REDRAW
    */
    void S_SET_REQREDRAW2(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_LIMIT);
            Set_ReqCenterFocusGroup();
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_START
    */
    void S_START(int phase, bool bFirst)
    {
        if (bFirst)
        {
            SetNextState(S_CLR_BRANCH);
        }
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }

}
