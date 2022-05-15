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
    [Serializable]
    public class ExcelCellCacheItem
    {
        public bool bAlt;  // Alt時は、Excel出力なし

        public int row;
        public int col;

        public string text;


        public bool     modified;
        public bool     newitem;  //新規

        public float   width_px()
        {
            return /*width*/ 100 * 96f / 72f;
        }
        public float   height_px()
        {
            return /*height*/ 16 * 96f / 72f;
        }

        public int    key { get { return make_key(row,col); } }

        public ExcelCellCacheItem Clone()
        {
            var ci = new ExcelCellCacheItem();

            ci.bAlt = bAlt;

            ci.row = row;
            ci.col = col;

            ci.text = text != null ? (string)text.Clone() : null;


            ci.modified = modified;
            ci.newitem  = newitem;

            return ci;
        }

        public bool isEqual(ExcelCellCacheItem i)
        {
            if (i.row != row) return false;
            if (i.col != col) return false;

            if (string.IsNullOrWhiteSpace(i.text) != string.IsNullOrWhiteSpace(text)) return false;
            if (i.text != text) return false;

            return true;
        }
        public bool isEqualText(ExcelCellCacheItem i)
        {
            if (i.row != row) return false;
            if (i.col != col) return false;

            if (string.IsNullOrWhiteSpace(i.text) != string.IsNullOrWhiteSpace(text)) return false;
            if (i.text != text) return false;

            return true;
        }

        public static int make_key(int row, int col)
        {
            return row * 100000 + col;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(text)) return text;
            return "-null-";
        }
    }
}
