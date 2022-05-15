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

using System.Runtime.InteropServices;

namespace stateview
{
    internal class ExcelPictures
    {
        internal class Item
        {
            internal int col;
            internal int row;

            internal Bitmap bmp;

            internal bool   modifed;

            internal string hash; //ハッシュ値 FileDBに伴い新設
        }

        internal List<Item> m_items = new List<Item>();

        internal void Init(ExcelDll ed) // Curent sheet
        {
            m_items.Clear();

            int row = -1;
            for(var r = 1;r<ed.MaxRow();r++)
            {
                var s = ed.GetStr(r,G.NAME_COL);
                if (s == G.STATENAME_thumbnail)
                {
                    row = r;
                    break;
                }
            }

            if (row<0)
            {
                return;
            }

            ed.ReadPics();

            for(var col = 1; col<=ed.MaxCol();col++)
            {
                var bmp = ed.GetBmp(row,col);
                if (bmp!=null)
                {
                    var item = new Item();
                    item.row = row;
                    item.col = col;
                    item.bmp = (Bitmap)bmp.Clone();
                    m_items.Add(item);
                }
            }
        }

        internal Item GetItem(int row,int col)
        {
            if (m_items!=null)
            {
                foreach(var i in m_items)
                {
                    if (i.row == row && i.col == col)
                    {
                        return i;
                    }
                }
            }
            return null;
        }

        internal string SetItem(int row, int col, Bitmap bmp, bool modifed=true)
        {
            var find = m_items.Find(i=>i.row == row && i.col == col);
            if (find!=null)
            {
                if (find.bmp!=null)
                {
                    try { find.bmp.Dispose(); } catch { }
                }
                find.bmp = null;
            }
            else
            {
                find = new Item();
                find.col = col;
                find.row = row;
                m_items.Add(find);
            }

            find.bmp = bmp;

            if (bmp!=null)
            { 
                find.hash = "#"+ BitmapUtil.GetHash_escape_for_filename(bmp);
            }
            else
            {
                find.hash = string.Empty;
            }

            find.modifed = true;

            G.bDirty_by_modified_value = true;//G.bDirty = true;
            G.update_viewform_title();

            return find.hash;
        }


        internal void Dispose()
        {
            if (m_items!=null)
            {
                foreach(var i in m_items)
                {
                    if (i.bmp!=null)
                    {
                        try {
                            i.bmp.Dispose();
                        }catch { }
                    }
                    i.bmp = null;
                }
            }
        }

        internal void WriteToExcel(ExcelDll ed)
        {
            if (m_use_filedb) throw new SystemException("{15209EC2-EBCB-445C-BDA5-698C335D15EA}"); 

            if (m_items==null || m_items.Count==0) return;

            ed.ReadPics();

            m_items.ForEach(i=> {
                if (i.modifed)
                {
                    ed.SetBmp(i.row,i.col,i.bmp);
                }
            });

            ed.UpdateBmps();
        }

        #region FILE DB W DATA   ※FLIE DBへ記録　エクセルへは別クラスよりエクスポート
        bool m_use_filedb = false;
        internal void Init_by_filedb()
        {
            m_use_filedb = true;

            m_items.Clear();
            var row = FileDbUtil.get_row_by_name(G.STATENAME_thumbnail);
            if (row < 0) return;
            for(var col = 1; col <= FileDbUtil.max_col(); col++)
            {
                var hash = FileDbUtil.get_val(row, col);
                if (string.IsNullOrEmpty(hash)) continue;
                var bmp = FileDbUtil.get_bmp_by_hash(hash);
                if (bmp!=null)
                {
                    var item = new Item();
                    item.row = row;
                    item.col = col;
                    item.bmp = (Bitmap)bmp.Clone();
                    item.hash = hash;
                    m_items.Add(item);
                }
            }
        }
        internal void WriteToFileDB()
        {
            if (m_items==null || m_items.Count==0) return;

            var dic = new Dictionary<string, Bitmap>();
            m_items.ForEach(i=> {
                FileDbUtil.set_val(i.row,i.col,i.hash); //ハッシュ値の更新
                DictionaryUtil.Set(dic,i.hash,i.bmp);
            });

            FileDbUtil.set_hash_bitmap_dic(dic); //ハッシュとbitmapの辞書セット

            FileDbUtil.write_filedb_manager_and_dirty_statedata_only(); // FileDbのstatechartのマネージャと変更ステートデータの更新

            FileDbUtil.write_filedb_bmp_files(); //ビットマップファイル部分の更新

            //FileDbUtil.create_psgg();// psgg ファイル作成 　　調べた結果、必要なし
        }
        #endregion
    }
}
