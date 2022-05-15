//             psggConverterLib.dll converted from psgg-file:..\doc\ViewFormStateControl.psgg

using G=stateview.Globals;
public partial class ViewFormStateControl {

    /*
        E_Idle3
    */
    bool m_needtrack=false;
    /*
        S_AddBranch
        一覧から追加
    */
    void S_AddBranch(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_add_branch();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_AddStateInGF
        グループフォーカスにステート追加
    */
    void S_AddStateInGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(G.mouse_down_or_up==false)) return;
                focus_add_stateGF();
                draw_focuses();//focus_draw();
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_AlignHorizontally
        水平にそろえる
    */
    void S_AlignHorizontally(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                align_horizontal();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_AlignVertically
        垂直にそろえる
    */
    void S_AlignVertically(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                align_vertical();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_BranchEditClose
        ブランチエディタダイアログ表示
    */
    void S_BranchEditClose(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_branchdlg_close()) return;
                G.frontend_enable(true);
                branchdlg_clear();
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_BranchEditOpen
        ブランチエディタダイアログ表示
    */
    void S_BranchEditOpen(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
            }
            //else
            {
                branchdlg_open();
                if (!HasNextState())
                {
                    SetNextState(S_BranchEditClose);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_BranchEditOpen1
        対象はステート？
    */
    void S_BranchEditOpen1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_checkStateForBranchEdit(S_BranchEditOpen);
                br_notAbove(S_BranchEditOpen2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_BranchEditOpen2
        WARNING
    */
    void S_BranchEditOpen2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.NoticeToUser_warning("Because a group, cannot open dialog.");
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CCDRAGENTER
        コピーコレクションドラッグ開始
    */
    void S_CCDRAGENTER(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    bmp作成
                    描画
                    表示
                */
                ovlbmp_create(G.m_cc_dropbmp);
                //ovldraw_do();
                ovlpb_setbmp();
                ovlpb_show_cc();
                if (!HasNextState())
                {
                    SetNextState(S_DragMove1);
                }
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
        S_CCRESET
    */
    void S_CCRESET(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                ovlpb_hide();
                G.m_cc_dragdrop=G.CCDD.none;
                if (G.m_cc_dropbmp!=null)
                {
                    G.m_cc_dropbmp.Dispose();
                    G.m_cc_dropbmp = null;
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_COPY_CLIPBOARD);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CCRESET1
    */
    void S_CCRESET1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                ovlpb_hide();
                G.m_cc_dragdrop=G.CCDD.none;
                if (G.m_cc_dropbmp!=null)
                {
                    G.m_cc_dropbmp.Dispose();
                    G.m_cc_dropbmp = null;
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CENTER_GROUP
        指定グループを中央に
    */
    void S_CENTER_GROUP(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                center_group(m_center_focus_group);
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_LOCALE_POINTER1);
                }
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
        S_CENTER_STATE
        指定ステートを中央に
    */
    void S_CENTER_STATE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                center_state(m_center_focus_state);
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_LOCALE_POINTER);
                }
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
        S_CenterFocusGroup
        特定グループをセンター表示かつフォーカス
        作業中
    */
    void S_CenterFocusGroup(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                if (!m_center_focus_same_dirpath)
                {
                    var dirpath = stateview.GroupNodeUtil.get_parent_path(m_center_focus_group);
                    G.node_set_curdir(dirpath);
                    DrawBenri.draw_opt();
                }
                else
                {
                    m_center_focus_same_dirpath=false;
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CENTER_GROUP);
                }
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
        S_CenterFocusState
        特定ステートをセンター表示かつフォーカス
        作業中
    */
    void S_CenterFocusState(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                if (!m_center_focus_same_dirpath)
                {
                    var dirpath = stateview.DirPathExcelUtil.get_dirpath(m_center_focus_state);
                    G.node_set_curdir(dirpath);
                    DrawBenri.draw_opt();
                }
                else
                {
                    m_center_focus_same_dirpath = false;
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CENTER_STATE);
                }
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
        S_Change
        種別変更
    */
    void S_Change(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_change();
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_IsMouseDown);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CHECK_CTRL
        コントロールキー押下時はステート名をコピー
    */
    void S_CHECK_CTRL(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                inout_copy_name();
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_goFocus(S_Focus1);
                br_notAbove(S_PASS1);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CHECK_DST
    */
    void S_CHECK_DST(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_is_dst_altstate(S_TRANS_BRANCINFO);
                br_notAbove(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CHECK_MRB_OPTION
        マウス右有効時、右ボタンだったら開く
    */
    void S_CHECK_MRB_OPTION(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    メニューへ
                    ?
                */
                br_open_if_mbr(S_ShowStateMenu);
                br_notAbove(S_IsMouseDown);
                if (!HasNextState())
                {
                    SetNextState(S_ShowStateMenu);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CheckCtrKey
        コントロールキーダウン中？
    */
    void S_CheckCtrKey(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_isCntrlKeyDown(S_GroupFocus_2States);
                br_notAbove(S_FocusCancel);
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
        S_CheckCtrKey1
        コントロールキー？
    */
    void S_CheckCtrKey1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_isCntrlKeyDown(S_FocusCancel1);
                br_notAbove(S_CHECK_MRB_OPTION);
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
        S_CheckCtrKey2
        コントロールキー？
    */
    void S_CheckCtrKey2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_isCntrlKeyDown(S_GroupFocus_2States1);
                br_notAbove(S_TICK);
                if (HasNextState())
                {
                    //NoWait();
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CheckCtrKey3
        コントロールキー？
    */
    void S_CheckCtrKey3(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_isCntrlKeyDown(S_GroupFocus_2States2);
                br_notAbove(S_FocusCancelWithTick);
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
        S_CheckKeyExec
    */
    void S_CheckKeyExec(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                    ?
                    ?
                */
                br_openContextMenu(S_ShowStateMenu);
                br_keyFocusAll(S_Draw_AllFoucs);
                br_keyDelete(S_Delete);
                br_notAbove(S_FocusCancel);
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
        S_CheckKeyExec1
    */
    void S_CheckKeyExec1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_openContextMenu(S_ShowGroupNodeMenu);
                br_keyEnterGroup(S_EnterGN);
                br_keyFocusAll(S_Draw_AllFoucs);
                br_keyDelete(S_DeleteGN);
                br_notAbove(S_FocusCancel);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CheckKeyExec2
    */
    void S_CheckKeyExec2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                    ?
                    ?
                */
                br_openContextMenu(S_ShowStateMenuGF);
                br_keyFocusAll(S_Draw_AllFoucs);
                br_keyDelete(S_DeleteGF);
                br_notAbove(S_FocusCancel);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CheckMbrOption
        Mbrオプション時は左ボタンのみスライド
    */
    void S_CheckMbrOption(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                    ?
                */
                br_mbroption_if_mbl(S_Sliding);
                br_mbroption_if_mbr(S_CheckMbrOption1);
                br_notAbove(S_Idle3);
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
        S_CheckMbrOption1
        Mbrオプション時は右ボタンのみ選択へ
    */
    void S_CheckMbrOption1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_mbroption_if_mbr(S_needtrack);
                br_notAbove(S_CheckMbrOption);
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
        S_CheckMouse
        マウスのアクション確認
    */
    void S_CheckMouse(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_chkonfcs_init();
            }
            //else
            {
                subsc_chkonfcs_update();
                if (!subsc_chkonfcs_done()) return;
                /*
                    ドラッグ時
                    ダブルクリック時
                    クリック時
                    フォーカス外時
                */
                br_isDrg(S_Drag);
                br_isDClck(S_Edit);
                br_isClick(S_CheckCtrKey1);
                br_notAbove(S_IsMouseDown);
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
        S_CheckMouseGF
        マウス状態確認
    */
    void S_CheckMouseGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_chkonfcsGF_init();
            }
            //else
            {
                subsc_chkonfcsGF_update();
                if (!(subsc_chkonfcsGF_done() || hasKeyexec() || G.m_cc_dragdrop != G.CCDD.none )) return;
                /*
                    ?
                    コンテキストメニュー表示
                    ドラッグ開始
                    グループをCtrクリック
                    グループをクリック
                    他ステートをCtrクリック
                    以外
                */
                br_hasKeyExec(S_CheckKeyExec2);
                br_openContextMenu(S_OpenMenu_w_mbr);
                br_isDrgDG(S_DragAndDropGF);
                br_isCtrlClickDG(S_RemoveStateInGF);
                br_isClickDG(S_OpenMenu_w_mbr);
                br_isCtrlClickOnStateDG(S_AddStateInGF);
                br_notAbove(S_FocusCancel);
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
        S_CheckMouseGN
        マウス状態確認
    */
    string m_save_groupnode_name;
    void S_CheckMouseGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_save_groupnode_name = m_groupnode_name;
                subsc_chkonfcsGN_init();
            }
            //else
            {
                subsc_chkonfcsGN_update();
                if (!(subsc_chkonfcsGN_done() || m_center_focus_state!=null || m_center_focus_group!=null || hasKeyexec() || G.m_cc_dragdrop != G.CCDD.none )) return;
                Check_state_under_pointer();
                /*
                    キーコマンドあり
                    現グループノードをドラッグ
                    異なるグループノードをドラッグ
                    現グループノードをクリック
                    現グループノードをダブルクリック
                    異なるグループノードをクリック
                    単なるステートをクリック
                    ?
                    以外
                */
                br_hasKeyExec(S_CheckKeyExec1);
                br_isDrgGN(S_DragAndDropGN);
                br_isDrgOtherGN(S_TICK);
                br_isClickStateGN(S_OpenMenuByMbrOnlyIfOption);
                br_isDClickStateGN(S_EnterGN);
                br_isClickOtherStateGN(S_CheckCtrKey2);
                br_isClickOnStateGN(S_CheckCtrKey3);
                br_isClickNotStateGN(S_FocusCancelWithTick);
                br_notAbove(S_FocusCancel);
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
        S_CheckOuter
        メインピクチャ内か？
    */
    void S_CheckOuter(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    外
                    中
                */
                br_isOuter(S_ShowOuterMenu);
                br_notAbove(S_ShowBlankMenu);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CLEAR_DST
        流出先クリア
    */
    void S_CLEAR_DST(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                inout_menu_clear_dst();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_PASS1);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ClearKeyExec
    */
    void S_ClearKeyExec(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_keyexec = stateview.KEYEXEC.none;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CommentOut
        コメントアウト
    */
    void S_CommentOut(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                comment_out_gf();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Copy
        コピー
    */
    void S_Copy(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_copy();
            }
            //else
            {
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_COPY_CLIPBOARD
    */
    void S_COPY_CLIPBOARD(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                cc_copy_clipboard();
            }
            //else
            {
                /*
                    OK
                    CANCEL
                */
                br_Ok(S_ImportClipboard2);
                br_notAbove(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CopyGN
        ＧＮを丸ごとコピーする
    */
    void S_CopyGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_copyGN();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_CopyStateName
        ステート名をクリップへコピー
    */
    void S_CopyStateName(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_copystatename();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Delete
        ベースステートがあるか？
    */
    void S_Delete(int phase, bool bFirst)
    {
        m_okCancel = check_hasbase();
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    OK
                    ?
                */
                br_Ok(S_Delete2);
                br_Cancel(S_Delete1);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Delete1
        削除
    */
    void S_Delete1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_delete_init();
            }
            //else
            {
                if (!wait_statemenu_delete_done()) return;
                G.frontend_enable(true);
                /*
                    OK
                    ?
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_IsMouseDown);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Delete2
        削除強硬するか？
    */
    void S_Delete2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_delete_base_init();
            }
            //else
            {
                if (!wait_statemenu_delete_base_done()) return;
                G.frontend_enable(true);
                /*
                    OK
                    ?
                */
                br_Ok(S_Delete1);
                br_Cancel(S_IsMouseDown);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DeleteGF
        グループフォーカスされたステートを削除
    */
    void S_DeleteGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                gfstatemenu_delete_init();
            }
            //else
            {
                if (!gfstatemenu_delete_wait()) return;
                G.frontend_enable(true);
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGF);
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DeleteGN
        グループノード削除
    */
    void S_DeleteGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_deleteGN_init();
            }
            //else
            {
                if (!wait_statemenu_deleteGN_done()) return;
                G.frontend_enable(true);
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGN);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DONE_CENTER_FOCUS_STATE
        センタリング＆フォーカス終了処理
    */
    void S_DONE_CENTER_FOCUS_STATE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_center_focus_state = null;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_FOCUSTRACK_IFNEED);
                }
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
        S_DONE_CENTER_FOCUS_STATE1
        センタリング＆フォーカス終了処理
    */
    void S_DONE_CENTER_FOCUS_STATE1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_center_focus_group = null;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_FOCUSTRACK_IFNEED1);
                }
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
        S_Drag
        ステートボックスのドラッグ開始
    */
    void S_Drag(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    bmp作成
                    描画
                    表示
                */
                ovlbmp_create();
                ovldraw_do();
                ovlpb_setbmp();
                ovlpb_show();
                if (!HasNextState())
                {
                    SetNextState(S_DragMove);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DragAndDropGF
        ドラッグ＆ドロップ
    */
    void S_DragAndDropGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_dandGF_init();
            }
            //else
            {
                subsc_dandGF_update();
                if (!subsc_dandGF_done()) return;
                /*
                    フォーカス上
                    以外
                */
                br_Ok(S_CheckMouseGF);
                br_Cancel(S_CheckMouseGF);
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
        S_DragAndDropGN
        グループノードボックスのドラッグ開始
    */
    void S_DragAndDropGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_dandGN_init();
            }
            //else
            {
                subsc_dandGN_update();
                if (!subsc_dandGN_wait()) return;
                br_Ok(S_CheckMouseGN);
                br_Cancel(S_CheckMouseGN);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DragCancel
        ドラッグ取消
    */
    void S_DragCancel(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    非表示
                */
                ovlpb_hide();
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
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
                /*
                    ドロップ時
                    キャンセル時
                */
                br_isDrp(S_Drop);
                br_isDrgCncl(S_DragCancel);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_DragMove1
        ドラッグ移動
    */
    void S_DragMove1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                move_update_cc();
                if (!wait_cc_dd()) return;
                /*
                    ドロップ時
                    キャンセル時
                */
                br_Ok(S_CCRESET);
                br_notAbove(S_CCRESET1);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Draw_AllFoucs
        全選択DRAW
    */
    void S_Draw_AllFoucs(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                draw_focuses();
                if (!HasNextState())
                {
                    SetNextState(S_FocusTrackRecord);
                }
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
        S_DrawFocuses
        フォーカス描画
    */
    void S_DrawFocuses(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                draw_focuses();
                if (!HasNextState())
                {
                    SetNextState(S_GroupFocus1);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Drop
        ステートボックスのドロップ
    */
    void S_Drop(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                ovlpb_hide();
                /*
                    全ステートボックス再描画
                */
                statebox_redraw();
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Edit
        編集
    */
    void S_Edit(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
            }
            //else
            {
                editbox_show();
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_EditWait);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EditBranch
        ブランチ編集
    */
    void S_EditBranch(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_editbranch_show();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_EditBranchWait);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EditBranchWait
        ブランチ編集待ち
    */
    void S_EditBranchWait(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_editbranch_done()) return;
                G.frontend_enable(true);
                br_isEdBrCncl(S_IsMouseDown);
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EditGN
        GroupNode
        編集
    */
    void S_EditGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_editGN();
            }
            //else
            {
                if (!statemenu_editGN_wait()) return;
                G.frontend_enable(true);
                /*
                    エディット
                    キャンセル
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGN);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EditWait
        編集待ち
    */
    void S_EditWait(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!wait_editdone()) return;
                G.frontend_enable(true);
                /*
                    エディットキャンセル
                */
                br_isEdCncl(S_IsMouseDown);
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_EnterGN
        GN中へ
    */
    void S_EnterGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_enterGN();
            }
            //else
            {
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ExportClipboard
        クリップボードへエキスポート
    */
    void S_ExportClipboard(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                gfstatemenu_export_clipboard();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ExportClipboardGN
        エキスポート
    */
    void S_ExportClipboardGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_exportGN();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ExportClipboardSM
        エキスポート
    */
    void S_ExportClipboardSM(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_exportsm();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Focus
        指定ステートをフォーカス
    */
    void S_Focus(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_focus_state();
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_Ok(S_Idle2);
                br_Cancel(S_IsMouseDown);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Focus1
    */
    void S_Focus1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                inoutmenu_focus_state();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_PASS1);
                }
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
        S_FocusCancel
        フォーカス取消
    */
    void S_FocusCancel(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                focus_reset();
                focus_erase();
                tab_contentclear();
                /*
                    0
                */
                /*
                    ?
                    ?
                    ?
                */
                br_isCCDrop(S_Idle2);
                br_hasKeyExec(S_Idle2);
                br_isOnSt(S_FocusState);
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
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
        S_FocusCancel1
        フォーカス取消
    */
    void S_FocusCancel1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!G.mouse_down_or_up==false) return;
                focus_reset();
                focus_erase();
                tab_contentclear();
                /*
                    0
                */
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_FocusCancelWithTick
        フォーカス取消
    */
    void S_FocusCancelWithTick(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                focus_reset();
                focus_erase();
                tab_contentclear();
                /*
                    0
                */
                /*
                    Keyコマンドあり
                    ?
                */
                br_hasKeyExec(S_Idle2);
                br_isOnSt(S_FocusState);
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_FocusGroupNode
        グループノードをフォーカス
    */
    void S_FocusGroupNode(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_erase();
                focus_set_groupnode();
                focus_draw_groupnode();
                if (m_needtrack){
                    m_needtrack = false;
                    track_focus_groupnode();
                }
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGN);
                }
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
        S_FocusHead
        先頭にフォーカス
    */
    void S_FocusHead(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                stateview.KeyProc.set_nearest_state_and_focus();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_FocusOneGroup
        フォーカス１つのグループ用に変数設定
    */
    void S_FocusOneGroup(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_setoneGF_group();
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGN);
                }
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
        S_FocusOneState
        フォーカス１つのステート用に変数設定
    */
    void S_FocusOneState(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_setoneGF_state();
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
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
        S_FocusState
        ステートをフォーカス
    */
    bool m_ctrl_down_at_focus;
    void S_FocusState(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_set();
                focus_draw();
                if (m_needtrack)
                {
                    m_needtrack = false;
                    track_focus();
                }
                m_ctrl_down_at_focus=isCtrlKey();
                tab_contentwrite();
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
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
        S_FocusState2
        ステートをフォーカス
    */
    void S_FocusState2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_set();
                focus_draw();
                tab_contentwrite();
                if (!HasNextState())
                {
                    SetNextState(S_Edit);
                }
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
        S_FocusTail
        末尾にフォーカス
    */
    void S_FocusTail(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                stateview.KeyProc.set_farest_state_and_focus();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_FOCUSTRACK_IFNEED
    */
    void S_FOCUSTRACK_IFNEED(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                if (m_needtrack)
                {
                    m_needtrack = false;
                    track_focus();
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
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
        S_FOCUSTRACK_IFNEED1
    */
    void S_FOCUSTRACK_IFNEED1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                if (m_needtrack)
                {
                    m_needtrack = false;
                    track_focus();
                }
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGN);
                }
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
        S_FocusTrackRecord
        フォーカストラックレコード
    */
    void S_FocusTrackRecord(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                track_focus_states();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_FreeArrow
        ドラッグ用矢印の作成
    */
    void S_FreeArrow(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_init_freearrow();
            }
            //else
            {
                subsc_update_freearrow();
                if (!wait_freearrow_done()) return;
                /*
                    ?
                    ?
                    ?
                */
                br_is_src_altstate(S_OPEN_ALT_SRC_SELECT);
                br_is_dst_altstate(S_OPEN_ALTDST_SELECT);
                br_notAbove(S_Idle2);
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
        S_GroupFocus
        グループへのフォーカス処理
    */
    void S_GroupFocus(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_groupfcs_init();
            }
            //else
            {
                subsc_groupfcs_update();
                if (!subsc_groupfcs_done()) return;
                if (!HasNextState())
                {
                    SetNextState(S_GroupFocus1);
                }
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
        S_GroupFocus_2States
        フォーカス対象を追加して、グループフォーカスへ
    */
    void S_GroupFocus_2States(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(G.mouse_down_or_up==false)) return;
                focus_set_2state();
                //focus_draw();
                draw_focuses();
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    //NoWait();
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_GroupFocus_2States1
    */
    void S_GroupFocus_2States1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(G.mouse_down_or_up==false)) return;
                focus_set_2group();
                //focus_erase();
                draw_focuses();
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_GroupFocus_2States2
    */
    void S_GroupFocus_2States2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(G.mouse_down_or_up==false)) return;
                focus_set_group_and_state();
                focus_erase();
                draw_focuses();
                if (!HasNextState())
                {
                    SetNextState(S_CheckMouseGF);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_GroupFocus1
        グループへのフォーカス処理
    */
    void S_GroupFocus1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    一つのステート時
                    一つのグループ時
                    複数ノード時
                    ?
                */
                br_ExistOneGF_state(S_FocusOneState);
                br_ExistOneGF_group(S_FocusOneGroup);
                br_ExistMultiGF(S_FocusTrackRecord);
                br_NotExistGF(S_Idle2);
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
        S_Grouping
        グループ化
    */
    void S_Grouping(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                gfstatemenu_grouping_init();
            }
            //else
            {
                if (!gfstatemenu_grouping_wait()) return;
                G.frontend_enable(true);
                /*
                    エディットキャンセル
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGF);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_HistoryBack
        履歴を一つ遡る
    */
    void S_HistoryBack(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_historyback();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_HistoryForward
        履歴を一つ先へ
    */
    void S_HistoryForward(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_historyforward();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_HistoryShow
        履歴パネル開く
    */
    void S_HistoryShow(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.m_history_record_panel.open_or_close();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Idle2
        アイドル
        マウスダウン待ち
    */
    void S_Idle2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_init_idle2();
            }
            //else
            {
                subsc_update_idle2();
                if (!(wait_subsc_idle2_done() || m_keyexec!=stateview.KEYEXEC.none )) return;
                m_needtrack = false;
                tab_contentclear();
                /*
                    ?
                    ?
                    ?
                    ステート上時
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_hasKeyExec(S_KeyExec);
                br_isAskDataUpgrade(S_ShowAskUpdate);
                br_isOpenDataUpgrade(S_OpenDataUpgrade_start);
                br_isOnState(S_SetNeedTrack);
                br_isDcOnState(S_FocusState2);
                br_isReqRedraw(S_RedrawOpt);
                br_isReqCenterFocusState(S_CenterFocusState);
                br_isReqCenterFocusGroup(S_CenterFocusGroup);
                br_isDcOnBranch(S_BranchEditOpen1);
                br_isDragBranch(S_FreeArrow);
                br_isDragInSpace(S_CheckMbrOption1);
                br_isOnGroupNode(S_NeedTrack);
                br_isClickOnBlank(S_OpenMenuByMbrOnlyIfOption1);
                br_isHoldMBD(S_CheckMbrOption);
                br_isClickBranch(S_SHOW_INOUTMENU);
                br_isCCDragEnter(S_CCDRAGENTER);
                br_notAbove(S_Idle3);
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
        S_Idle3
        new state
    */
    void S_Idle3(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ImportClipboard
        クリップボードよりインポート
    */
    void S_ImportClipboard(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_okCancel = statemenu_importclipboard(false);
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_Ok(S_SaveHistory_ommit);
                br_Cancel(S_RedrawOpt);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ImportClipboard1
        インポート
        流出先除外
    */
    void S_ImportClipboard1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_okCancel = statemenu_importclipboard(true);
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_Ok(S_SaveHistory_ommit);
                br_Cancel(S_RedrawOpt);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ImportClipboard2
        インポート
        キーボードから
        流出先フラグ使用
    */
    void S_ImportClipboard2(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                var wo_outflow = m_keyexec == stateview.KEYEXEC.paste_wo_outflow;
                m_okCancel = statemenu_importclipboard(wo_outflow);
                m_keyexec = stateview.KEYEXEC.none;
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_Ok(S_SaveHistory_ommit);
                br_Cancel(S_RedrawOpt);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ImportClipboardAsBase
        インポート
        ベースステートとして
    */
    void S_ImportClipboardAsBase(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                paste_as_base(); //実質新ステート
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
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
        初期化する
    */
    void S_Init(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                ovlpb_hide();
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_INSERT_NEW
        新規ステートの挿入
    */
    void S_INSERT_NEW(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                inout_menu_insert();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_IsMouseDown
        マウスダウンしたか
    */
    void S_IsMouseDown(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(wait_mousedown() || m_center_focus_state!=null || m_center_focus_group!=null || hasKeyexec() || G.m_cc_dragdrop != G.CCDD.none  )) return;
                pointer_check();
                /*
                    CCドロップ
                    キーボードコマンド
                    フォーカス上
                    外クリック、他ステート以外
                    外クリック、他ステート上
                    ?
                */
                br_isCCDrop(S_FocusCancel);
                br_hasKeyExec(S_CheckKeyExec);
                br_isOnFcs(S_CheckMouse);
                br_isFcsCnclNotOnSt(S_FocusCancelWithTick);
                br_isFcsCnclOnSt(S_CheckCtrKey);
                br_notAbove(S_FocusCancelWithTick);
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
        S_KeyExec
    */
    void S_KeyExec(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_keyPate(S_ImportClipboard2);
                br_keyHistoryBack(S_HistoryBack);
                br_keyHistoryForward(S_HistoryForward);
                br_keyFocusTrackBack(S_TrackBack);
                br_keyFocusTrackForward(S_TrackForward);
                br_openContextMenu(S_ShowBlankMenu);
                br_keyFocusState(S_NEED_TRACK);
                br_keyFocusGroupNode(S_NEED_TRACK1);
                br_keyFocusAll(S_Draw_AllFoucs);
                br_openInOutMenu(S_SHOW_INOUTMENU);
                br_keyFocusClear(S_FocusCancelWithTick);
                br_keyEnterGroup(S_EnterGN);
                br_keyLeaveGroup(S_Leave);
                br_notAbove(S_ClearKeyExec);
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
        S_Leave
        離れる
        上の階層へ
    */
    void S_Leave(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_leave();
            }
            //else
            {
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_LOCALE_POINTER
        ポインタをフォーカスの上に
    */
    void S_LOCALE_POINTER(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                if (m_center_focus_wo_cursor==false)
                {
                    stateview.ViewUtil.SetPointerOnState(m_center_focus_state );
                }
                m_center_focus_wo_cursor = false;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_DONE_CENTER_FOCUS_STATE);
                }
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
        S_LOCALE_POINTER1
        ポインタをフォーカスの上に
    */
    void S_LOCALE_POINTER1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                var group = stateview.GroupNodeUtil.get_last_path(m_center_focus_group);
                if (!string.IsNullOrEmpty(group)) group = group.Replace("/","");
                if (G.vf_sc.m_center_focus_wo_cursor == false)
                {
                    stateview.ViewUtil.SetPointerOnState(stateview.AltState.MakeAltStateName(group));
                }
                G.vf_sc.m_center_focus_wo_cursor = false;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_DONE_CENTER_FOCUS_STATE1);
                }
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
        S_Moveto
        グループ移動
    */
    void S_Moveto(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_moveto_init();
            }
            //else
            {
                if (!wait_statemenu_moveto_done()) return;
                /*
                    ?
                    ?
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_IsMouseDown);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_MoveTo
        グループへ移動
    */
    void S_MoveTo(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                gfstatemenu_moveto_init();
            }
            //else
            {
                if (!gfstatemenu_moveto_wait()) return;
                G.frontend_enable(true);
                /*
                    エディットキャンセル
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGF);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_MovetoGN
        グループへ移動
    */
    void S_MovetoGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                statemenu_movetoGN();
            }
            //else
            {
                if (!statemenu_movetoGN_done()) return;
                G.frontend_enable(true);
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_NEED_TRACK
    */
    void S_NEED_TRACK(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_needtrack = true;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CenterFocusState);
                }
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
        S_NEED_TRACK1
    */
    void S_NEED_TRACK1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_needtrack = true;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_CenterFocusGroup);
                }
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
        S_needtrack
    */
    void S_needtrack(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_needtrack=true;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_GroupFocus);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_NeedTrack
    */
    void S_NeedTrack(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_needtrack = true;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_FocusGroupNode);
                }
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
        S_NewState
        新規ステート作成
    */
    void S_NewState(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_newstate();
            }
            //else
            {
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OPEN_ALT_SRC_SELECT
        ALT時流入先入力
    */
    void S_OPEN_ALT_SRC_SELECT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                altstate_src_dialog_open();
            }
            //else
            {
                if (!altstate_src_dialog_done()) return;
                m_okCancel = altstate_src_dialog_ok();
                /*
                    ?
                    ?
                */
                br_Ok(S_CHECK_DST);
                br_Cancel(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OPEN_ALTDST_SELECT
        ALT時の行先決定ダイアログ
    */
    void S_OPEN_ALTDST_SELECT(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                altstate_dst_dialog_open();
            }
            //else
            {
                if (!altstate_dst_dialog_done()) return;
                m_okCancel = altstate_dst_dialog_ok();
                /*
                    ?
                    ?
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OPEN_SRC
        ソース開く
    */
    void S_OPEN_SRC(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_opensrc();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Open_upgrade_dlg
        アップグレードダイアログ表示
    */
    void S_Open_upgrade_dlg(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                upgrade_dlg_start();
            }
            //else
            {
                if (!upgrade_dlg_done()) return;
                /*
                    ?
                    ?
                */
                br_Ok(S_Restart_MSG);
                br_notAbove(S_OpenDataUpgrade_end);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenDataUpgrade_end
    */
    void S_OpenDataUpgrade_end(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.psgg_open_upgrade = false;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenDataUpgrade_start
    */
    void S_OpenDataUpgrade_start(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Open_upgrade_dlg);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenLink
        リンクを開く
    */
    void S_OpenLink(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_link();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_IsMouseDown);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenMenu_w_mbr
    */
    void S_OpenMenu_w_mbr(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_open_if_mbr(S_ShowStateMenuGF);
                br_notAbove(S_CheckMouseGF);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenMenuByMbrOnlyIfOption
        右ボタンオプション有効時右クリックのみオープンへ
    */
    void S_OpenMenuByMbrOnlyIfOption(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_open_if_mbr(S_ShowGroupNodeMenu);
                br_notAbove(S_CheckMouseGN);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_OpenMenuByMbrOnlyIfOption1
    */
    void S_OpenMenuByMbrOnlyIfOption1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ?
                    ?
                */
                br_open_if_mbr(S_CheckOuter);
                br_notAbove(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_PASS1
    */
    void S_PASS1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_RedrawOpt
        最適化再描画
    */
    void S_RedrawOpt(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    最適化全再描画
                */
                statebox_optdraw();
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_RedrawOpt1
        最適化再描画
    */
    void S_RedrawOpt1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    最適化全再描画
                */
                statebox_optdraw();
                if (!HasNextState())
                {
                    SetNextState(S_DrawFocuses);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Refactor
        リファクタリング
    */
    void S_Refactor(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_refactor();
            }
            //else
            {
                if (!wait_statemenu_refactor_done()) return;
                /*
                    ?
                    ?
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_IsMouseDown);
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ReFocus1
        異なるグループノードをフォーカス
    */
    void S_ReFocus1(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    描画
                    PB表示
                */
                focus_erase();
                //focus_set_groupnode();  使用しない！ 既にセット済み
                focus_draw_groupnode();
                if (!HasNextState())
                {
                    SetNextState(S_DragAndDropGN);
                }
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
        S_RemoveComment
        コメント削除
    */
    void S_RemoveComment(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_remove_comment();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_RemoveStateInGF
        グループフォーカスからステート削除
    */
    void S_RemoveStateInGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!(G.mouse_down_or_up==false)) return;
                focus_remove_stateGF();
                draw_focuses();
                /*
                    ?
                    ?
                    ?
                    ?
                */
                br_ExistOneGF_state(S_FocusOneState);
                br_ExistOneGF_group(S_FocusOneGroup);
                br_ExistMultiGF(S_CheckMouseGF);
                br_NotExistGF(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Restart
    */
    void S_Restart(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                restart();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_OpenDataUpgrade_end);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Restart_MSG
        リスタートメッセージ
    */
    void S_Restart_MSG(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                restart_msg_start();
            }
            //else
            {
                if (!restart_msg_done()) return;
                if (!HasNextState())
                {
                    SetNextState(S_Restart);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_SaveHistory_ommit
    */
    void S_SaveHistory_ommit(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                //stateview.History2.SaveForce_modify_value("Pasted.");
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt1);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_SetNeedTrack
    */
    void S_SetNeedTrack(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                m_needtrack = true;
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_FocusState);
                }
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
        S_SHOW_INOUTMENU
    */
    void S_SHOW_INOUTMENU(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                inoutmenu_init();
            }
            //else
            {
                if (!inoutmenu_done()) return;
                /*
                    ダイアログへ
                    流出先クリア
                    新規ステート挿入
                    ステート指定
                    指定せず
                */
                br_goInOutDialog(S_BranchEditOpen1);
                br_clearDst(S_CLEAR_DST);
                br_insertNew(S_INSERT_NEW);
                br_hasInOutState(S_CHECK_CTRL);
                br_notAbove(S_PASS1);
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
        S_ShowAskUpdate
        データアップデート確認表示
    */
    void S_ShowAskUpdate(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                ask_update_dlg_start();
            }
            //else
            {
                if (!ask_update_dlg_done()) return;
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ShowBlankMenu
        ブランクメニュー表示
    */
    void S_ShowBlankMenu(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenuBlank_init();
            }
            //else
            {
                if (!statemenuBlank_wait()) return;
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_Leave(S_Leave);
                br_NewState(S_NewState);
                br_HistoryShow(S_HistoryShow);
                br_HistoryBack(S_HistoryBack);
                br_HistoryForward(S_HistoryForward);
                br_ImportClipboard(S_ImportClipboard);
                br_ImportClipboard_wo_outflow(S_ImportClipboard1);
                br_ImportClipboard_as_base(S_ImportClipboardAsBase);
                br_TrackShow(S_TrackShow);
                br_TrackBack(S_TrackBack);
                br_TrackForward(S_TrackForward);
                br_FocusHead(S_FocusHead);
                br_FocusTail(S_FocusTail);
                br_notAbove(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ShowGroupNodeMenu
        グループノードメニュー表示
    */
    void S_ShowGroupNodeMenu(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenuGN_init();
            }
            //else
            {
                if (!statemenuGN_wait()) return;
                br_EnterGN(S_EnterGN);
                br_UngroupGN(S_UngroupingGN);
                br_EditGN(S_EditGN);
                br_MovetoGN(S_MovetoGN);
                br_CopyGN(S_CopyGN);
                br_DeleteGN(S_DeleteGN);
                br_ExportClipboardGN(S_ExportClipboardGN);
                br_notAbove(S_CheckMouseGN);
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ShowOuterMenu
        外メニュー表示
    */
    void S_ShowOuterMenu(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenuOuter_init();
            }
            //else
            {
                if (!statemenuOuter_wait()) return;
                /*
                    ?
                    ?
                */
                br_FocusHead(S_FocusHead);
                br_notAbove(S_Idle2);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_ShowStateMenu
        編集
    */
    void S_ShowStateMenu(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_init();
            }
            //else
            {
                if (!wait_statemenu_done()) return;
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_OpenEditFull(S_Edit);
                br_EditBranch(S_EditBranch);
                br_AddBranch(S_AddBranch);
                br_RemoveComment(S_RemoveComment);
                br_Copy(S_Copy);
                br_Delete(S_Delete);
                br_Refactor(S_Refactor);
                br_Moveto(S_Moveto);
                br_Change(S_Change);
                br_Link(S_OpenLink);
                br_ExportClipboard_SM(S_ExportClipboardSM);
                br_CopyStateName(S_CopyStateName);
                br_OpenSrc(S_OPEN_SRC);
                br_ViewExState(S_VIEW_EXSTATE);
                br_Focus(S_Focus);
                br_notAbove(S_IsMouseDown);
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
        S_ShowStateMenuGF
        ＧＦ編集
    */
    void S_ShowStateMenuGF(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenuGF_init();
            }
            //else
            {
                if (!statemenuGF_wait()) return;
                /*
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                    ?
                */
                br_Grouping(S_Grouping);
                br_DeleteGF(S_DeleteGF);
                br_MovetoGF(S_MoveTo);
                br_Ungrouping(S_Ungrouping);
                br_ExportClipboard(S_ExportClipboard);
                br_AlignHorizontal(S_AlignHorizontally);
                br_AlignVertical(S_AlignVertically);
                br_CommentOut(S_CommentOut);
                br_notAbove(S_CheckMouseGF);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Sliding
        スライディング
    */
    void S_Sliding(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                subsc_sliding_init();
            }
            //else
            {
                subsc_sliding_update();
                if (!subsc_sliding_wait()) return;
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
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
        S_TICK
    */
    void S_TICK(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_NeedTrack);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_TrackBack
        前の場所へ
    */
    void S_TrackBack(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_trackback();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_TrackResultBranch);
                }
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
        S_TrackForward
        次の場所へ
    */
    void S_TrackForward(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_trackforward();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_TrackResultBranch);
                }
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
        S_TrackResultBranch
        結果で分岐
    */
    void S_TrackResultBranch(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
            }
            //else
            {
                /*
                    ステートフォーカスへ
                    グループノードフォーカスへ
                    ステート集合フォーカスへ
                    ?
                */
                br_trackresult_go_focusstate(S_FocusState);
                br_trackresult_go_focusgroupnode(S_FocusGroupNode);
                br_trackresult_go_focusstates(S_DrawFocuses);
                br_notAbove(S_RedrawOpt);
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
        S_TrackShow
        フォーカストラック
        パネル表示
    */
    void S_TrackShow(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.m_focus_track_panel.open_or_close();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_TRANS_BRANCINFO
        分岐情報の移行
    */
    void S_TRANS_BRANCINFO(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                altsrcbi_to_srcbi();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_OPEN_ALTDST_SELECT);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_Ungrouping
        グループ解除
    */
    void S_Ungrouping(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                G.frontend_enable(false);
                gfstatemenu_ungrouping_init();
            }
            //else
            {
                if (!gfstatemenu_ungrouping_wait()) return;
                G.frontend_enable(true);
                /*
                    エディットキャンセル
                */
                br_Ok(S_RedrawOpt);
                br_Cancel(S_CheckMouseGF);
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_UngroupingGN
        GN中へ
    */
    void S_UngroupingGN(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_ungroupingGN();
            }
            //else
            {
                /*
                    エディットキャンセル
                */
                if (!HasNextState())
                {
                    SetNextState(S_RedrawOpt);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }
    /*
        S_VIEW_EXSTATE
        外部ステートの対象グループへ移動
    */
    void S_VIEW_EXSTATE(int phase, bool bFirst)
    {
        if (phase==0)
        {
            if (bFirst)
            {
                statemenu_view_exstate();
            }
            //else
            {
                if (!HasNextState())
                {
                    SetNextState(S_Idle2);
                }
                if (HasNextState())
                {
                    GoNextState();
                    return;
                }
            }
        }
    }

}
