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


public partial class ArrowFlowStateControl2 : StateControlBase
{
    static float DUNIT =20f;//{ get { return D.DUINT; } }

    #region アクセス
    //public List<StateData> m_stateData {get { return StateInfo.m_stateData;  } }
    internal Dictionary<string,DStateData> m_stateData { get { return G.m_draw_data_list; } }
    #endregion


    internal DStateData m_state_start;
    internal DStateData m_state_goal;

    internal PointF m_posS;
    internal PointF m_posG;

    internal int?  m_branch_index;
    internal bool  m_bNextOrBranch { get { return m_branch_index==null; } }

    internal List<PointF> m_result;

    /// <summary>
    ///
    /// </summary>
    /// <param name="start_st">始点所属ステートデータ</param>
    /// <param name="goal_st">終点所属ステートデータ</param>
    /// <param name="branch_index">null：next,数値はブランチのインデックス</param>
    /// <param name="start">開始位置</param>
    /// <param name="goal">終了位置</param>
    internal void Begin(DStateData start_st, DStateData goal_st, int? branch_index, PointF start, PointF goal)
    {
        m_state_start = start_st;
        m_state_goal  = goal_st;

        m_branch_index = branch_index;

        m_posS = start;
        m_posG  = goal;

        sc_start(S_NONE);

        SetNextState(S_START);
        GoNextState();
    }

    internal void Calc()
    {
        for(var loop = 0; loop<10000; loop++)
        {
            sc_update();
            if (m_sm.CheckState(S_END))
            {
                break;
            }
        }
    }

    internal List<PointF> GetResult()
    {
        return m_result;
    }
}
