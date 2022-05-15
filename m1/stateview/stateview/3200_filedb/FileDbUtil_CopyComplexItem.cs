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

using CopyCompleItem = stateview.ExcelProgramDirect.CopyCompleItem;
using stateview;

public partial class FileDbUtil
{ 
    /// <summary>
    /// ExcelPrigramDirectの CopyComplex を FileDb用に修正
    /// 
    ///  自分でもどうやったか覚えていない。改めて解析する。
    ///  srcmapという、元の行番号マップを持つ。コピー処理されると、1000+元の行の値が入る。　挿入・削除で移動する。
    ///  srcnamemapも同時に操作される。
    /// 
    /// </summary>
    public static List<string> CopyComplex(List<CopyCompleItem> items, bool bTry = false) //return は 実行後の name list で Try時の参考とする
    {
        var NEWPADID=1000;
        var srcmap = new List<int>();   // 元ソースのマップ   ※ List[0]は保留扱い ※base 1 との互換のため
                                        // v < NEWPADID 元
                                        // v > NEWPADID コピー後の元 
                                        // 初期状態は Index = value
        var srcnamemap = GetNameList() ; // srcmapと連動

        Action<int,string> rename = (r,newname)=> {
            if (!bTry)
            {
                rename_name_by_row(r,newname); //   m_ed.SetStr(r,NAME_COL,newname);
            }
            srcnamemap[r] =  newname;
        };

        Action<CopyCompleItem> delrow = (i)=> { //引数のアイテムから探して削除
            if (i==null || i.dst_row != -1) throw new SystemException("{23A24DCB-B1E0-42C0-B7FD-130158C2DBA2}");//条件

            var cursrc_row = srcmap.IndexOf(i.src_row);

            if (!bTry)
            {
                FileDbUtil.remove_row(cursrc_row);//m_ed.RemoveRow(cursrc_row);                
            }

            srcmap.RemoveAt(cursrc_row);
            srcnamemap.RemoveAt(cursrc_row);
        };
          
        Action<CopyCompleItem> insertrow = (i)=> { //指定行にinsert
            if (i==null || i.src_row >=0 || i.dst_row < 0) throw new SystemException("{B7AFE95A-9742-4077-9992-5612F52C9F11}"); //条件
            var r = i.dst_row;
            if (!bTry)
            {
                FileDbUtil.insert_row(r);//m_ed.InsertRow(r);
            }
            srcmap.Insert(r,i.src_row);
            srcnamemap.Insert(r,"(insert temp)");

            rename(r,i.new_name);
        };

        Action<CopyCompleItem> moverow = (i)=> { //指定行に移動 
            /*
                インサートして、コピーして、元を消す
            */
            if (i==null || i.src_row < 0 || i.dst_row < 0 ) throw new SystemException("{B7AFE95A-9742-4077-9992-5612F52C9F11}"); //条件
                
            var r = i.dst_row;

            // インサート                
            if (!bTry)
            {
                FileDbUtil.insert_row(r);//m_ed.InsertRow(r);
            }
            srcmap.Insert(r,-999999);
            srcnamemap.Insert(r,"(insert temp)");

            // コピー
            var cursrc_row = srcmap.IndexOf(i.src_row);
            if (!bTry)
            {
                FileDbUtil.copy_row_empty_src(cursrc_row,r);//m_ed.CopyRow(cursrc_row, r);
            }
            srcmap[r] += srcmap[cursrc_row] + NEWPADID;
            srcnamemap[r] = srcnamemap[cursrc_row];

            // リネーム
            rename(r,i.new_name);

            // 削除
            if (!bTry)
            {
                FileDbUtil.remove_row(cursrc_row);// m_ed.RemoveRow(cursrc_row);
            }
            srcmap.RemoveAt(cursrc_row);
            srcnamemap.RemoveAt(cursrc_row);
        };

        //ソースマップ
        for (var n = 0; n</*m_ed.MaxRow()*/FileDbUtil.max_row()  +100; n++) srcmap.Add(n);
        //ソースネームマップ ソースマップと連動
        srcnamemap.Add(string.Empty);

        // dst_rowをキーにして、小さい順にソート
        var dlist = new List<CopyCompleItem>(items);
        dlist.Sort((a,b)=>a.dst_row.CompareTo(b.dst_row));
            
        // dlistの順に処理　同時に srcmapを処理させて、srcの位置を保持する
        for(var n = 0; n < dlist.Count; n++)
        {
            var item = dlist[n];
            if (item.dst_row < 0) //削除された
            {
                delrow(item);
                continue;
            }
            if (item.src_row < 0) //新規
            {
                insertrow(item);   
                continue;
            }
            //以外   ・・・移動、リネーム
            moverow(item);
        }

        FileDbUtil.write_filedb_manager_and_dirty_statedata_only(); //fileDB内部ファイル更新
        FileDbUtil.create_psgg();
        if (G.psgg_header_info_save_mode_withexcel)
        {
            FileDbUtil.create_excel();
        }

        return srcnamemap;
    }
    // 上記で利用するツール
    private static List<string> GetNameList()
    {
        return G.file_db.m_state_chart.getNameList();
    }

}
