using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl
{
    Dictionary<int,string> Excel_GetRowNameDic()
    {
        var epd = new ExcelProgramDirect();
        epd.Open(G.load_file,G.sheetchart);
        var row_itemname_dic = epd.GetRowNameDic();
        epd.Close();
        return row_itemname_dic;
    }

    int Excel_MaxRow()
    {
        var epd = new ExcelProgramDirect();
        epd.Open(G.load_file,G.sheetchart);
        int maxrow = epd.MaxRow();
        epd.Close();
        return maxrow;
    }

    //Dictionary<int,int> Excel_GetRowBackColorDic()
    //{
    //    var epd = new ExcelProgramDirect();
    //    epd.Open(G.load_file,G.sheetchart);
    //    var row_backcolor_dic = epd.GetRowBackcolorDic();
    //    epd.Close();
    //    return row_backcolor_dic;
    //}

    #region FileDB 向け
    Dictionary<int,string> FileDb_GetRowNameDic()
    {
        var row_itemname_dic = new Dictionary<int,string>();
        for(var row = 1; row <= FileDbUtil.max_row(); row++)
        {
            var name = FileDbUtil.get_name_by_row(row);
            if (string.IsNullOrEmpty(name)) name = string.Empty;
            row_itemname_dic.Add(row,name);   
        }
        return row_itemname_dic;
    }

    int FileDb_MaxRow()
    {
        return FileDbUtil.max_row();
    }

    //Dictionary<int,int> FileDb_GetRowBackColorDic()
    //{
    //    var epd = new ExcelProgramDirect();
    //    epd.Open(G.load_file,G.sheetchart);
    //    var row_backcolor_dic = epd.GetRowBackcolorDic();
    //    epd.Close();
    //    return row_backcolor_dic;
    //}
    #endregion
}
