//             psggConverterLib.dll converted from psgg-file:..\doc\EditBranchControl.psgg

public partial class EditBranchControl : StateManager {

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()
    {
        return CheckState(S_END);
    }



    /*
        C_Edit2
        編集
    */
    void C_Edit2(bool bFirst)
    {
        if (bFirst)
        {
            edit_start();
        }
        if (!edit_done()) return;
        edit_post();
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_BLANK_MENU
        ブランク用メニュー表示
    */
    void S_BLANK_MENU(bool bFirst)
    {
        if (bFirst)
        {
            show_blank_menu();
        }
        check_blank_menu();
        br_NewAPI(S_NEW_API);
        br_NewIF(S_NEW_IF);
        br_NewELSEIF(S_NEW_ELSEIF);
        br_NewELSE(S_NEW_ELSE);
        br_Select(S_SELECT_NEW);
        br_Cancel(S_WAIT_CLICK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
        }
    }
    /*
        S_CMT_MENU
        コメント欄用メニュー表示
    */
    void S_CMT_MENU(bool bFirst)
    {
        if (bFirst)
        {
            show_cmt_menu();
        }
        check_cmt_menu();
        br_Edit(S_Edit_CMT);
        br_Up(S_UP);
        br_Down(S_DOWN);
        br_Delete(S_DELETE);
        br_Cancel(S_WAIT_CLICK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
        }
    }
    /*
        S_DELETE
        削除
    */
    void S_DELETE(bool bFirst)
    {
        if (bFirst)
        {
            delete_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_DOWN
        下へ移動
    */
    void S_DOWN(bool bFirst)
    {
        if (bFirst)
        {
            down_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_Edit
        編集
    */
    void S_Edit(bool bFirst)
    {
        if (bFirst)
        {
            editbox_start();
        }
        if (!editbox_done()) return;
        editbox_post();
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_Edit_CMT
        コメント編集
    */
    void S_Edit_CMT(bool bFirst)
    {
        if (bFirst)
        {
            editcmt_start();
        }
        if (!editcmt_done()) return;
        editcmt_post();
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_EDIT_NEW
        編集へ
    */
    void S_EDIT_NEW(bool bFirst)
    {
        if (bFirst)
        {
            edit_new();
        }
        if (!HasNextState())
        {
            SetNextState(S_Edit1);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_Edit1
    */
    void S_Edit1(bool bFirst)
    {
        if (bFirst)
        {
        }
        if (!HasNextState())
        {
            SetNextState(S_Edit);
        }
        if (HasNextState())
        {
            NoWait();
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
        S_ITEM_MENU
        アイテムメニュー表示
    */
    void S_ITEM_MENU(bool bFirst)
    {
        if (bFirst)
        {
            show_item_menu();
        }
        check_item_menu();
        br_Edit(S_Edit1);
        br_Select(S_SELECT_OW);
        br_Up(S_UP);
        br_Down(S_DOWN);
        br_Delete(S_DELETE);
        br_Cancel(S_WAIT_CLICK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
        }
    }
    /*
        S_MODE_ITEM_MENU
        モードアイテムメニュー表示
    */
    void S_MODE_ITEM_MENU(bool bFirst)
    {
        if (bFirst)
        {
            show_mode_menu();
        }
        if (!show_mode_menu_done()) return;
        show_mode_menu_post();
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEW_API
        新規追加
    */
    void S_NEW_API(bool bFirst)
    {
        if (bFirst)
        {
            new_api_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW_NEW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEW_ELSE
        新規追加
    */
    void S_NEW_ELSE(bool bFirst)
    {
        if (bFirst)
        {
            new_else_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEW_ELSEIF
        新規追加
    */
    void S_NEW_ELSEIF(bool bFirst)
    {
        if (bFirst)
        {
            new_elseif_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW_NEW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_NEW_IF
        新規追加
    */
    void S_NEW_IF(bool bFirst)
    {
        if (bFirst)
        {
            new_if_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW_NEW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_REDRAW
        再描画
    */
    void S_REDRAW(bool bFirst)
    {
        if (bFirst)
        {
            redraw();
            reselect();
        }
        if (!HasNextState())
        {
            SetNextState(S_WAIT_CLICK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_REDRAW_NEW
        ＮＥＷ用再描画
    */
    void S_REDRAW_NEW(bool bFirst)
    {
        if (bFirst)
        {
            redraw();
        }
        if (!HasNextState())
        {
            SetNextState(S_EDIT_NEW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SELECT_NEW
        新規追加
    */
    void S_SELECT_NEW(bool bFirst)
    {
        if (bFirst)
        {
            select_new_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_SELECT_OW
        ヒストリから選択
        上書き
    */
    void S_SELECT_OW(bool bFirst)
    {
        if (bFirst)
        {
            select_ow_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
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
            SetNextState(S_WAIT_CLICK);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_UP
        上に移動
    */
    void S_UP(bool bFirst)
    {
        if (bFirst)
        {
            up_start();
        }
        if (!HasNextState())
        {
            SetNextState(S_REDRAW);
        }
        if (HasNextState())
        {
            GoNextState();
        }
    }
    /*
        S_WAIT_CLICK
        クリック待ち
    */
    void S_WAIT_CLICK(bool bFirst)
    {
        if (bFirst)
        {
        }
        check_point();
        br_OnItem(S_ITEM_MENU);
        br_OnCmt(S_CMT_MENU);
        br_OnModeItem(S_MODE_ITEM_MENU);
        br_Blank(S_BLANK_MENU);
        br_NotAbove(S_WAIT_CLICK);
        if (HasNextState())
        {
            GoNextState();
        }
    }

}
