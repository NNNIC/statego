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

using stateview;

public partial class ViewFormStateControl {
    public string m_state_under_pointer;

    void pointer_check()
    {
        //m_branchInfo.m_branchpoint_state  = null;    //分岐上にポインタがあるか？
        m_state_under_pointer = null;   //ステート上にポインタがあるか？

        if (G.m_draw_data_list==null) return;

        var state = GetStateOnPointer();
        if (state!=null)
        {
            m_state_under_pointer = state;
        }
        else
        {
            G.log += "Null at GetStateOnPointer" + Environment.NewLine;
        }

    }

    public class BranchInfo
    {
        public PointF m_branchpoint_pos;                   //その時のポジション
        public string m_branchpoint_state;                 //分岐が属するステート
        public string m_branchpoint_label;                 //分岐のラベル
        public int?  m_branchpoint_isNextStateOrBranchOrGosub;   //NextStateかブランチか？
        public int?   m_branchpoint_branch_index;          //ブランチ時のインデックス
        public string m_branchpoint_branch_string;         //ブランチ時の文字列

        public bool   m_branchpoint_inputpoint;            // 入力ポイント (無理に追加) 2019.12.22

        public void Clear()
        {
            m_branchpoint_state = null;
            m_branchpoint_label = null;
            m_branchpoint_isNextStateOrBranchOrGosub = null;
            m_branchpoint_branch_index = null;
            m_branchpoint_branch_string = null;

            m_branchpoint_inputpoint = false;
        }

        public BranchInfo Clone()
        {
            var bi = new BranchInfo();
            bi.m_branchpoint_pos = m_branchpoint_pos;
            bi.m_branchpoint_state = m_branchpoint_state;
            bi.m_branchpoint_label = m_branchpoint_label;
            bi.m_branchpoint_isNextStateOrBranchOrGosub = m_branchpoint_isNextStateOrBranchOrGosub;
            bi.m_branchpoint_branch_index = m_branchpoint_branch_index;
            bi.m_branchpoint_branch_string = m_branchpoint_branch_string;

            bi.m_branchpoint_inputpoint = m_branchpoint_inputpoint;

            return bi;
        }
    }

    public BranchInfo m_branchInfo = new BranchInfo();
    public BranchInfo m_save_branchInfo;

}
