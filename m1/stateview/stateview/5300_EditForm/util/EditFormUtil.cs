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

namespace stateview._5300_EditForm
{
    public class EditFormUtil
    {
        public static void validate_number(TextBox tb, string error=null)
        {
            if (error==null) error ="";

            var text = tb.Text;
            var o = 0;
            if (int.TryParse(text,out o))
            {
                tb.Text = o.ToString();
            }
            else
            {
                tb.Text = error;
            }
        }

        public static void validate_number(DataGridView dg, int row, int col, string error = null) // _num 検知と処理
        {
            if (error == null) error = "";
            if (        row < 0 || row >= dg.Rows.Count
                        ||
                        col < 0 && col >= dg.Columns.Count
            )
            {
                //unexpected;
                return;
            }

            var header = dg.Columns[col].Name;
            if (header.EndsWith("_num"))
            {
                Object cell = null;
                if (dg[col,row]!=null) {
                    cell = dg[col,row].Value;
                }
                var text = cell!=null ? cell.ToString() : null;
                var o = 0;
                if (int.TryParse(text,out o))
                {
                    text = o.ToString();
                }
                else
                {
                    text = error;
                }
                dg[col,row].Value = text;
            }
        }

        public static string get_str(DataGridView dg, int row, int col, string error = null)
        {
            if (dg==null) return error;
            if (row < 0 || row >= dg.Rows.Count || col < 0 || col >= dg.Columns.Count) return error;
            var text = string.Empty;
            if (dg[col,row].Value!=null) {
                text = dg[col,row].Value.ToString();
            }
            if (text==null) return error;
            return text;
        }
        public static int get_int(DataGridView dg, int row, int col, int error = -1)
        {
            var text = get_str(dg,row,col);
            if (text==null) return error;
            var o = 0;
            if (int.TryParse(text,out o))
            {
                return o;
            }
            return error;
        }
        public static bool get_lock(DataGridView dg, int row, int col)
        {
            if(dg == null)
                return true;
            if(row < 0 || row >= dg.Rows.Count || col < 0 || col >= dg.Columns.Count)
                return true;
            var checkbox = dg[col,row].Value;
            if(checkbox == null) return true;

            return (checkbox.ToString().ToLower() == "1");
        }
        public static void set_str(DataGridView dg, int row, int col, string val)
        {
            if (row < 0 || row >=dg.Rows.Count || col < 0 || col >= dg.Columns.Count) return;
            dg[col,row].Value = val;
        }
        public static void set_int(DataGridView dg, int row, int col, int val)
        {
            set_str(dg,row,col,val.ToString());
        }
    }
}
