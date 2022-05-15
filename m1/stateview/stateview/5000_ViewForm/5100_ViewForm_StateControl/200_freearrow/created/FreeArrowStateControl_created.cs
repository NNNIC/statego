//             psggConverterLib.dll converted from psgg-file:..\doc\FreeArrow.psgg

//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>
public partial class FreeArrowStateControl {

    /*
        E_INIT1
    */
    public enum RESULT
    {
         none,
         cancel,
         dst_is_state,
         dst_is_altstate
    }
    public RESULT m_result;
    /*
        S_CHECK_DST
        行先確認
    */
    void S_CHECK_DST(int phase, bool bFirst)
    {
        SetNextState();
        if (bFirst)
        {
            G.frontend_enable(false);
        }
        check_dst();
        if (!wait_checkdg_done()) return;
        G.frontend_enable(true);
        br_setdst(S_SET_DST);
        br_cancel(S_SET_RESULT_CANCEL);
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_CHECK_MOUSE
        マウスボタン状態確認
    */
    void S_CHECK_MOUSE(int phase, bool bFirst)
    {
        SetNextState();
        check_mouse();
        br_stay(S_CHECKPOS);
        br_leave(S_CHECK_DST);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_CHECKPOS
        マウスポジション確認
    */
    void S_CHECKPOS(int phase, bool bFirst)
    {
        SetNextState();
        checkpos();
        br_diff_pos(S_UPDATE_PICBOX);
        br_same_pos(S_ONE_TICK);
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_DESTROY
        終了処理
    */
    void S_DESTROY(int phase, bool bFirst)
    {
        SetNextState(S_END);
        destroy();
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_DRAW_ARROW
        矢印描画
    */
    void S_DRAW_ARROW(int phase, bool bFirst)
    {
        SetNextState(S_ONE_TICK);
        draw_arrow();
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
        SetNextState();
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_INIT
        初期化
        Picturebox準備
    */
    void S_INIT(int phase, bool bFirst)
    {
        SetNextState(S_CHECKPOS);
        if (bFirst)
        {
            m_result = RESULT.none;
        }
        init();
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }
    /*
        S_ONE_TICK
        1 TICK 待つ
    */
    void S_ONE_TICK(int phase, bool bFirst)
    {
        SetNextState(S_CHECK_MOUSE);
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_SET_DST
        行先を更新
        再描画依頼
    */
    void S_SET_DST(int phase, bool bFirst)
    {
        SetNextState();
        set_dst();
        br_dst_is_state(S_SET_RESULT_STATE);
        br_dst_is_altstate(S_SET_RESULT_STATE1);
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_SET_RESULT_CANCEL
    */
    void S_SET_RESULT_CANCEL(int phase, bool bFirst)
    {
        SetNextState(S_DESTROY);
        if (bFirst)
        {
            m_result = RESULT.cancel;
        }
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_SET_RESULT_STATE
    */
    void S_SET_RESULT_STATE(int phase, bool bFirst)
    {
        SetNextState(S_DESTROY);
        if (bFirst)
        {
            m_result = RESULT.dst_is_state;
        }
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_SET_RESULT_STATE1
    */
    void S_SET_RESULT_STATE1(int phase, bool bFirst)
    {
        SetNextState(S_DESTROY);
        if (bFirst)
        {
            m_result = RESULT.dst_is_altstate;
        }
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_START
    */
    void S_START(int phase, bool bFirst)
    {
        SetNextState(S_INIT);
        if (HasNextState())
        {
            GoNextState();
            return;
        }
    }
    /*
        S_UPDATE_PICBOX
        ピクチャボックス更新
    */
    void S_UPDATE_PICBOX(int phase, bool bFirst)
    {
        SetNextState(S_DRAW_ARROW);
        update_pb();
        if (HasNextState())
        {
            NoWait();
            GoNextState();
            return;
        }
    }

}
