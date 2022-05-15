using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

public  class DataGridViewUtil
{
    public static int GetColumnIndex(DataGridView dgv, string name)
    {
        for(var c = 0; c<dgv.Columns.Count; c++)
        {
            if (dgv.Columns[c].Name == name)
            {
                return c;
            }
        }
        return -1;
    }

    public static int FindRowIndexAtColumnForText(DataGridView dgv, int col, string text)
    {
        for(var r = 0; r<dgv.Rows.Count; r++)
        {
            var cell = dgv[col,r];
            if (cell!=null && cell.Value!=null)
            {
                if (cell.Value.ToString() == text)
                {
                    return r;
                }
            }
        }
        return -1;
    }

    public static bool IsContainCol(DataGridView dgv,  int c, string s)
    {
        for(var r = 0; r<dgv.Rows.Count; r++)
        {
            var v = dgv[c,r].Value.ToString();
            if (v!=null)
            {
                v = v.Trim();
            }
            if (v==s) return true;
        }
        return false;
    }

    public static string GetString(DataGridView dgv, int col, int row)
    {
        if (dgv != null 
            && 
            col >=0 && col < dgv.Columns.Count 
            && 
            row >=0 && row < dgv.Rows.Count
            )
        {
            var cell = dgv[col,row];
            if (cell!=null && cell.Value!=null)
            {
                return cell.Value.ToString();
            }
        }
        return string.Empty;
    }

}
