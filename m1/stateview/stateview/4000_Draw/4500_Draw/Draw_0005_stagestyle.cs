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
    internal class StateStyle {
        internal enum STYLE
        {
            NORMAL,     //通常
            FORGROUP,   //グループとして
            FORSRC,     //流入元として　　　※これより前の流入先はない
            FORDST,     //流出先として      ※これより後の流出先はない
            EXCLUDE     //除外              ※表示対象外
        }

        internal enum PARTS
        {
            STATE,      //ステート名
            STATE_CMT,  //ステートコメント
            THUMBNAIL,  //サムネル
            CONTENT,    //コンテンツ
            BRANCH,     //ブランチ      ※ false時は ブランチ表示なしとなり、下の MULTIBRANCHは無意味となる
            MULTIBRANCH //複数ブランチ  ※ false時は single branchとなる。ForGroup用

        }

        private static bool[][] StateStyle_Mask  = new bool[][] {
                            //               STATE   STATE_CMT   THUMBNAIL   CONTENT     BRANCH      MULTIBRANCH
           new bool[] {　   /*NORMAL*/       true,   true,       true,       true,       true,       true        },
           new bool[] {　   /*FORGROUP*/     true,   true,       false,      false,      true,       false       },
           new bool[] {　   /*FORSRC*/       true,   true,       false,      false,      true,       true        },
           new bool[] {　   /*FORDST*/       true,   true,       true,       false,      false,      false       },
        };

        internal static bool Mask(STYLE style, PARTS parts)
        {
            return StateStyle_Mask[ (int)style][(int)parts];
        }
    }
}
