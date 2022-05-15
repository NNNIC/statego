using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using stateview;
using G = stateview.Globals;

public partial class ItemEditControl {
    void click_ok()
    {
        /*
        1.  items.iniの更新
        2.  help.iniの更新
        3.  エクセルの更新
        　　上から順に見ていく
        */

        if (m_info.UpdateIni(this)) m_form.m_ask_reload = true;
        if (m_help.UpdateIni(this)) m_form.m_ask_reload = true;

        /*
            データの作成
        */

        var row_name_dic =  G.psgg_file_w_data ? /*新PSGG*/FileDb_GetRowNameDic() :  /*従来式*/Excel_GetRowNameDic();

        var items = new List<ExcelProgramDirect.CopyCompleItem>();
        var vrow = -1; //セパレータまで　rowカウントしない
        for (var r = 0; r < m_dg.Rows.Count; r++)
        {
            var indexstr = m_dg[CC_INDEX, r].Value?.ToString();
            var itemname = m_dg[CC_NAME, r].Value?.ToString();

            if (vrow == -1)
            {
                if (indexstr == SEPARATORMARK) vrow = 0;　// vrow=0として、次回からカウント
                continue;
            }

            vrow++; //base 1 のため、先にカウントアップ

            if (indexstr == NEWMARK)
            {
                var i = new ExcelProgramDirect.CopyCompleItem();
                i.src_row = -r;
                i.dst_row = vrow;
                i.new_name = itemname;
                items.Add(i);
                continue;
            }
            var index = ParseUtil.ParseInt(indexstr);
            if (index < 0)
            {
                G.NoticeToUser_warning("{2FDAD90B-0B2D-45CC-A78B-9AEFDC5F24CF}");
                return;
            }
            else {
                var i = new ExcelProgramDirect.CopyCompleItem();
                i.src_row = index;
                i.dst_row = vrow;
                i.new_name = itemname;

                //if ( index == vrow &&  row_name_dic.ContainsKey(index) && row_name_dic[index] == itemname)
                if ( index == vrow &&  row_name_dic.ContainsKey(index))
                {
                    if ( string.IsNullOrEmpty(row_name_dic[index]) && string.IsNullOrEmpty(itemname))
                    {
                        //null
                        continue;
                    }
                    if ( row_name_dic[index].Trim() == itemname.Trim())
                    {
                        //変化なし
                        continue;
                    }
                }
                items.Add(i);
                continue;
            }
        }
        //deleteの確認
        //dgのindexのリスト
        var indexlist = new List<string>();
        for(var r = 0; r<m_dg.Rows.Count; r++)
        {
            var indexstr = m_dg[CC_INDEX, r].Value?.ToString();
            if (indexstr==null) indexstr = string.Empty;
            indexlist.Add(indexstr);
        }
        //row-name-dicを順に走査して、対象rowがindexlistに存在しなければ削除対象
        foreach(var p in row_name_dic)
        {
            var srow = p.Key.ToString();
            if (!indexlist.Contains(srow)) //削除されている
            {
                var i = new ExcelProgramDirect.CopyCompleItem();
                i.src_row = p.Key;
                i.dst_row = -1;
                i.new_name = p.Value?.ToString();
                items.Add(i);
            }
        }

        if (items.Count > 0)
        {
            if (G.psgg_file_w_data)
            {
                FileDbUtil.CopyComplex(items);
            }
            else
            { 
                //エクセル編集
                var epd = new ExcelProgramDirect();
                epd.Open(G.load_file,G.sheetchart);
                var result = epd.CopyComplex(items);
                epd.Save();
                epd.Close();
            }

            G.NoticeToUser("item names has been updated.");

            m_form.m_ask_reload = true;
        }
        else
        {
            G.NoticeToUser("item names have not been changed.");
            //m_form.m_ask_reload = false;
        }
        m_form.DialogResult = DialogResult.OK;
        m_form.Close();
    }
}
