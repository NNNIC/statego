//             psggConverterLib.dll converted from psgg-file:..\doc\ArrowFlow.psgg

public enum ArrowFlowState2
{

    S_END,
    S_NONE,
    S_OUTLINE,
    S_RS_BEGIN,
    S_RS_CALC,
    S_RS_CHK_QR,
    S_RS_CHK_SQ,
    S_RS_CK_RG,
    S_RS_END,
    S_START,
    S_STRAIGHT,

}

public partial class ArrowFlowStateControl2 {

    /*
        S_END
        終了
    */
    void S_END(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_NONE
    */
    void S_NONE(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_OUTLINE
        アウトライン作成
    */
    void S_OUTLINE(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_BEGIN);
                /*
                    アウトライン作成作成
                */
                outline_create();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_BEGIN
        ルート検索開始
    */
    void S_RS_BEGIN(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_CALC);
                /*
                    各調整値にdを設定
                */
                setdiff_SP();
                setdiff_clear_PQ();
                setdiff_TG();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_CALC
        ポイント計算
    */
    void S_RS_CALC(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_CHK_QR);
                /*
                    PQRTの作成
                */
                point_PQRT();
                /*
                    全クリア
                */
                setdiff_allclear();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_CHK_QR
        QRの確認
    */
    void S_RS_CHK_QR(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_CHK_SQ);
                /*
                */
                check_QR();
                /*
                    check_QRの結果でPQのdiff値をセット
                */
                setdiff_PQ_chkQR();
            }
            /*
                check_QRの結果で遷移
            */
            br_checkQR(S_RS_CALC);
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_CHK_SQ
        SQの確認
    */
    void S_RS_CHK_SQ(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_CK_RG);
                /*
                */
                check_SQ();
                /*
                    check_SQの結果でSPのdiff値をセット
                */
                setdiff_SP_chkSQ();
            }
            /*
                check_SQの結果で遷移
            */
            br_checkSQ(S_RS_CALC);
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_CK_RG
        RGの確認
    */
    void S_RS_CK_RG(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_RS_END);
                /*
                */
                check_RG();
                /*
                    check_RGの結果でTGのdiff値をセット
                */
                setdiff_TG_chkRG();
            }
            /*
                check_RGの結果で遷移
            */
            br_checkRG(S_RS_CALC);
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_RS_END
        ルート検索終了
    */
    void S_RS_END(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                /*
                    ルートを作成
                */
                point_createroute();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_START
        開始
    */
    void S_START(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_OUTLINE);
                /*
                    直線確認
                */
                check_straight();
            }
            /*
                直線で引けるのであれば、S_STRAIGHTへ
            */
            br_straight(S_STRAIGHT);
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }
    /*
        S_STRAIGHT
        直線
    */
    void S_STRAIGHT(int phase, bool bFirst)
    {
        if (phase == 0)
        {
            if (bFirst)
            {
                SetNextState(S_END);
                /*
                    直線のポイント作成
                */
                point_straight();
            }
            if (HasNextState())
            {
                GoNextState();
            }
        }
    }

}
