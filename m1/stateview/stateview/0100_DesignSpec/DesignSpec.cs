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

namespace stateview {

    public class DesignSpec
    {
        public enum PARTS
        {
            BOUNDFRAME,   // 外枠  ※注意　この枠の外側に見えない　OUTFRAMEがある。
            BOUNDBG,

            STATEFRAME,
            STATEBG,
            STATETEXT,

            CONTENTFRAME,
            CONTENTBG,
            CONTENTTEXT,

            BRANCHFRAME,
            BRANCHBG,
            BRANCHTEXT,

            SRCPOFRAME,
            SRCPOBG,

            DSTPOFRAME,
            DSTPOBG,

            //2019.10追加
            GOSUBFRAME,
            GOSUBBG,
            GOSUBTEXT
        }


        //色定義  Style は stateview.StateStyleを参照せよ
        public static Color[][]  StyleColor = new Color[][] {
                            //Style      BOUNDFRAME                   BOUNDBG                      STATEFRAME                STATEBG                      STATETEXT                   CONTENTFRAME                CONTENTBG                   CONTENTTEXT   BRANCHFRAME                 BRANCHBG                    BRANCHTEXT   SRCPOFRAME        SRCPOBG                  DSTPOFRAME        DSTPOBG                  GOSUBFRAME                  GOSUBBG                      GOSUBTEXT  
            new Color[] {   /*NORMAL*/   Color.FromArgb(0,120,215),   Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(0,120,215),   Color.White                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(65,113,156), Color.FromArgb(255,255,255), Color.FromArgb(255,0,0), },
            new Color[] {   /*FORGROUP*/ Color.FromArgb(140,56,54),   Color.FromArgb(255,170,0),   Color.FromArgb(255,170,0),Color.FromArgb(255,170,0),   Color.FromArgb(32,21,238)  ,Color.FromArgb(192,80,77),  Color.FromArgb(192,80,77),  Color.White  ,Color.FromArgb(255,170,0),  Color.FromArgb(255,170,0),  Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(65,113,156), Color.FromArgb(255,255,255), Color.FromArgb(255,0,0), },
            new Color[] {   /*FORSRC*/   Color.FromArgb(21,188,21),   Color.FromArgb(21,100,21),   Color.FromArgb(21,21,21), Color.FromArgb(113,137,63),  Color.Black,                Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(113,137,63), Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(65,113,156), Color.FromArgb(255,255,255), Color.FromArgb(255,0,0), },
            new Color[] {   /*FORDST*/   Color.FromArgb(21,188,21),   Color.FromArgb(21,100,21),   Color.FromArgb(21,21,21), Color.FromArgb(113,137,63),  Color.Black,                Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(113,137,63), Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(65,113,156), Color.FromArgb(255,255,255), Color.FromArgb(255,0,0), },
            new Color[] {   /*EXCLUDE*/  Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(170,170,170), Color.White                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(146,208,80), Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(65,113,156), Color.FromArgb(255,255,255), Color.FromArgb(255,0,0), },
        };
        //new Color[] {   /*NORMAL*/   Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(0,120,215),   Color.White                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0), Color.FromArgb(255,255,255), Color.FromArgb(54,143,63), Color.FromArgb(0,0,0), },
        //new Color[] {   /*NORMAL*/   Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(170,170,170), Color.White                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0)   },
        //new Color[] {   /*FORSRC*/   Color.FromArgb(21,188,21),   Color.FromArgb(21,100,21),   Color.FromArgb(21,21,21), Color.FromArgb(1,100,1),     Color.FromArgb(200,230,200),Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(113,137,63), Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0)   },
        //new Color[] {   /*FORDST*/   Color.FromArgb(185,63,151),  Color.FromArgb(125,21,24),   Color.FromArgb(21,21,21), Color.FromArgb(125,21,24),   Color.FromArgb(215,166,166),Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(226,253,188),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0)   },
        //new Color[] {   /*FORDST*/   Color.FromArgb(185,63,151),  Color.FromArgb(200,85,85),   Color.FromArgb(21,21,21), Color.FromArgb(200,85,85),   Color.Black,                Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(226,253,188),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0)   },

        //ステートが別種別時に色を変更 stle=NORMALのみ S以外
        public static Color[] StyleNormalNonstateColor = new Color[] {
          //BOUNDFRAME                   BOUNDBG                      STATEFRAME                STATEBG                        STATETEXT                   CONTENTFRAME                CONTENTBG                   CONTENTTEXT   BRANCHFRAME                 BRANCHBG                    BRANCHTEXT   SRCPOFRAME        SRCPOBG                  DSTPOFRAME        DSTPOBG
          //Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(255,255,255),   Color.Black                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
            Color.Blue,                  Color.FromArgb(170,170,170), Color.Blue,               Color.FromArgb(255,255,255),   Color.Black                ,Color.FromArgb(65,113,156), Color.White,                Color.Black  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
        };
        public static Color[] StyleNormalEmbedColor = new Color[] {
          //BOUNDFRAME                   BOUNDBG                      STATEFRAME                STATEBG                        STATETEXT                   CONTENTFRAME                CONTENTBG                   CONTENTTEXT   BRANCHFRAME                 BRANCHBG                    BRANCHTEXT   SRCPOFRAME        SRCPOBG                  DSTPOFRAME        DSTPOBG
          //Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21), Color.FromArgb(255,255,255),   Color.Black                ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
          //Color.Blue,                  Color.FromArgb(170,170,170), Color.Blue,               Color.FromArgb(255,255,255),   Color.Black                ,Color.FromArgb(65,113,156), Color.White,                Color.Black  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
            Color.Black,                 Color.FromArgb(170,170,170), Color.White,              Color.FromArgb(207,214,229),   Color.Black                ,Color.White,                Color.White,                Color.Black  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
        };
        public static Color[] StyleNormalCommentColor = new Color[] {
          //BOUNDFRAME                   BOUNDBG                      STATEFRAME                  STATEBG                        STATETEXT                 CONTENTFRAME                CONTENTBG                   CONTENTTEXT   BRANCHFRAME                 BRANCHBG                    BRANCHTEXT   SRCPOFRAME        SRCPOBG                  DSTPOFRAME        DSTPOBG
          //Color.FromArgb(185,63,151),  Color.FromArgb(170,170,170), Color.FromArgb(21,21,21),   Color.FromArgb(255,255,255),   Color.Black              ,Color.FromArgb(65,113,156), Color.FromArgb(91,155,213), Color.White  ,Color.FromArgb(65,113,156), Color.FromArgb(255,255,255),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White,  
          //Color.Black,                 Color.FromArgb(246,246,246), Color.FromArgb(246,246,246),Color.FromArgb(246,246,246),   Color.Black              ,Color.FromArgb(246,246,246), Color.White,                Color.Black ,Color.FromArgb(246,246,246),Color.FromArgb(246,246,246),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
            Color.Black,                 Color.FromArgb(246,246,246), Color.FromArgb(246,246,246),Color.FromArgb(255,242,157),   Color.Black              ,Color.FromArgb(255,242,157), Color.White,                Color.Black ,Color.FromArgb(255,242,157),Color.FromArgb(255,242,157),Color.Black, Color.White,      Color.FromArgb(0,128,0), Color.White,      Color.FromArgb(255,0,0) , Color.White, Color.White, Color.White, 
        };

        //スタイルとパーツから色を選択
        internal static Color GetColor(SS.STYLE style, PARTS parts, string state=null)
        {
            var col = StyleColor[(int)style][(int)parts];
            if (style == StateStyle.STYLE.NORMAL && !string.IsNullOrEmpty(state))
            {
                var c = state[0];
                if (c != 'S')
                {
                    if (c == 'E')
                    {
                        col = StyleNormalEmbedColor[(int)parts];
                    }
                    else if (c == 'C')
                    {
                        col = StyleNormalCommentColor[(int)parts];
                    }
                    else
                    {
                        col = StyleNormalNonstateColor[(int)parts];
                    }
                }
            }
            return col;
        }

        public static Color ArrowColor_normal    = Color.White;
        public static Color ArrowColor_highlite  = Color.Red;

    }
}
