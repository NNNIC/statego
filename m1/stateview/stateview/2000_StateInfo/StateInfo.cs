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

namespace stateview
{

    internal class StateInfo
    {
        internal class CellData
        {
            internal float width;
            internal float height;

            internal string fontname;
            internal float fontsize;
            internal Color fontcolor;

            internal Color bgcolor;
            internal string  text;
        }

        internal string      state;
        internal string      basestate;

        internal CellData    state_cell;
        internal CellData    state_cmt_cell;

        internal string      nextstate;

        internal Bitmap      thumbnail_bmp;

        internal CellData    content_cell; //合成したもの

        internal CellData    gosub_cell;

        internal CellData    branch_cell;

        internal class BranchItem
        {
            internal BranchUtil.APIMODE mode;       //api | if | elseif | else
            internal string             value;      //オリジナル
            internal string             api;        //api部分
            internal string             cond;       //条件 
            internal string             nextstate;  //第一引数

            internal string             disp;       //表示用

            internal string             cmt;        //コメント用

            internal CellData           cell;     //個別化したもの
        }

        internal int num_of_branches { get { return branch_list!=null ? branch_list.Count: 0;} }
        internal List<BranchItem> branch_list;

        #region 飾り 2019.10
        internal string state_typ;
        internal string state_dco; //未定
        #endregion
    }


}
