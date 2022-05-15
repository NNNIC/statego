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

/*
    Excel を キャッシュ無しに直接操作

    ExcelProgramは、 MS製Excel.dllへアクセスするために作られた。
    MS製のExcel.dllは動作が遅く、そのためのキャッシュだった。
    libXLに変え、キャッシュの必要がなくなった。

    ExcelProgramは、既存部分の多くが依存しているために、そのままになっている。(2018.12.31現在)

    本クラスは、キャッシュ無で操作するためのものです。

    ※ ExcelProgramとは別個に使うこと

    ※ InsertRow RemoveRowを考慮して、 内部キャッシュは使わずに直接読んで使うこと！
*/
namespace stateview
{
    public partial class ExcelProgramDirect
    {
        const int NAME_COL  = 2;
        const int START_COL = 3;
        const int STATE_ROW = 2;

        private ExcelDll m_ed;

        public void Open(string path, string sheetname)
        {
            m_ed =  new ExcelDll();
            m_ed.Load(path);
            m_ed.SetSheet(sheetname);
        }

        public void Close()
        {
            m_ed.Dispose();
            m_ed = null;
        }

        public int MaxRow()
        {
            return m_ed.MaxRow();
        }

        public int MaxCol()
        {
            return m_ed.MaxCol();
        }

        public void Save()
        {
            m_ed.Save();
        }

        public Dictionary<int,string> GetRowNameDic() // name colを縦に空白含めてすべて読込む  ※ bese 1 で  ※ Rowがkey
        {
            var list = new Dictionary<int,string>();
            for(var r = 1; r < m_ed.MaxRow() + 1; r++)
            {
                var s = m_ed.GetStr(r,NAME_COL);
                if (s!=null) s = s.Trim();               
                if (s==null) s = string.Empty;
                list.Add(r,s);
            }
            return list;
        }
        public Dictionary<int,int> GetRowBackcolorDic()// name colのbackcolor index を縦に空白含めてすべて読込む  ※ bese 1 で  ※ Rowがkey
        {
            var list = new Dictionary<int,int>();
            for(var r = 1; r < m_ed.MaxRow() + 1; r++)
            {
                var bc = m_ed.GetBackColor(r,NAME_COL);
                list.Add(r,bc);
            }
            return list;
        }
        public Dictionary<string,int> GetNameRowDic(bool detectDuplicateKeyError=true) // Nameをキーに。
        {
            var list = new Dictionary<string,int>();
            for(var r = 1; r < m_ed.MaxRow() + 1; r++)
            {
                var s = m_ed.GetStr(r,NAME_COL);
                if (s!=null) s = s.Trim();               
                if (!string.IsNullOrEmpty(s))
                {
                    if (!list.ContainsKey(s))
                    {
                        list.Add(s,r);
                    }
                    else
                    {
                        if (detectDuplicateKeyError)
                        {
                            G.NoticeToUser_warning( G.Localize("w_itemnamedup") /* "Item Name Duplicated :" */ + s);
                        }
                    }
                }
            }
            return list;
        }
        public List<string> GetNameList() //Nameをリスト可 base1互換のため、要素０は空白 
        {
            var list = new List<string>();
            list.Add(string.Empty); //空白
            for(var r = 1; r < m_ed.MaxRow() + 1; r++)
            {
                var s = m_ed.GetStr(r,NAME_COL);
                if (s!=null) s = s.Trim();               
                if (s==null) s = string.Empty;
                list.Add(s);
            }
            return list;
        }
        public Dictionary<int,string> GetColStateDic() //state colを start_col以降 Colをキーとしてリスト化
        {
            var list = new Dictionary<int,string>();
            for(var c = START_COL; c < m_ed.MaxCol() + 1; c++)
            {
                var s = m_ed.GetStr(STATE_ROW,c);
                if (s!=null) s = s.Trim();               
                if (s==null) s = string.Empty;
                list.Add(c,s);
            }
            return list;
        }

        public Dictionary<string, int> GetStateColDic(bool detectDuplicateKeyError=true) //colをキーとして stateをリスト化
        {
            var list = new Dictionary<string,int>();
            for(var c = START_COL; c < m_ed.MaxCol() + 1; c++)
            {
                var s = m_ed.GetStr(STATE_ROW,c);
                if (s!=null) s = s.Trim();               
                if (!string.IsNullOrEmpty(s))
                {
                    if (!list.ContainsKey(s))
                    {
                        list.Add(s,c);
                    }
                    else
                    {
                        if (detectDuplicateKeyError)
                        {
                            G.NoticeToUser_warning( G.Localize("w_statenamedup")/* "State Name Duplicated :" */+ s);
                        }
                    }
                }
            }
            return list;
        }

        public int GetRow(string name)
        {
            var list = GetNameRowDic();
            if (list.ContainsKey(name))
            {
                return list[name];
            }
            return -1;
        }

        public int GetBackColor(int row)
        {
            var cnum = m_ed.GetBackColor(row,NAME_COL);
            return cnum;
        }

        public bool RenameName(string src, string dst,bool bTry = false)
        {
            if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(dst))
            {
                G.NoticeToUser_warning("{0FE64BBF-5342-47C3-80BC-3C1B6D758EEF}");
                return false;
            }
            var s = src.Trim();
            var d = dst.Trim();
            
            var name_row_dic = GetNameRowDic();
            if (name_row_dic.ContainsKey(s))
            {
                var row = name_row_dic[s];
                if (!name_row_dic.ContainsKey(d))
                {
                    if (!bTry)
                    {
                        m_ed.SetStr(row,NAME_COL,d);
                    }
                    return true;
                }
            }
            return false;
        }

        /*
            挿入・削除・リネームを総合的に実行
        */
        public class CopyCompleItem
        {
            public int src_row;      //元のrow番号     新規は　負のユニークなid
            public int dst_row;      //先のrow番号     削除は -1

            public string new_name;  //新規の名前　変更ない場合は元の名前
        }


        public List<string> CopyComplex(List<CopyCompleItem> items, bool bTry = false) //return は 実行後の name list で Try時の参考とする
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
                    m_ed.SetStr(r,NAME_COL,newname);
                }
                srcnamemap[r] =  newname;
            };

            Action<CopyCompleItem> delrow = (i)=> { //引数のアイテムから探して削除
                if (i==null || i.dst_row != -1) throw new SystemException("{23A24DCB-B1E0-42C0-B7FD-130158C2DBA2}");//条件

                var cursrc_row = srcmap.IndexOf(i.src_row);

                if (!bTry)
                {
                    m_ed.RemoveRow(cursrc_row);                
                }

                srcmap.RemoveAt(cursrc_row);
                srcnamemap.RemoveAt(cursrc_row);
            };
          
            Action<CopyCompleItem> insertrow = (i)=> { //指定行にinsert
                if (i==null || i.src_row >=0 || i.dst_row < 0) throw new SystemException("{B7AFE95A-9742-4077-9992-5612F52C9F11}"); //条件
                var r = i.dst_row;
                if (!bTry)
                {
                    m_ed.InsertRow(r);
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
                    m_ed.InsertRow(r);
                }
                srcmap.Insert(r,-999999);
                srcnamemap.Insert(r,"(insert temp)");

                // コピー
                var cursrc_row = srcmap.IndexOf(i.src_row);
                if (!bTry)
                {
                    m_ed.CopyRow(cursrc_row,r);
                }
                srcmap[r] += srcmap[cursrc_row] + NEWPADID;
                srcnamemap[r] = srcnamemap[cursrc_row];

                // リネーム
                rename(r,i.new_name);

                // 削除
                if (!bTry)
                {
                    m_ed.RemoveRow(cursrc_row);
                }
                srcmap.RemoveAt(cursrc_row);
                srcnamemap.RemoveAt(cursrc_row);
            };

            //ソースマップ
            for (var n = 0; n<m_ed.MaxRow()+100; n++) srcmap.Add(n);
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

            return srcnamemap;
        }



    }
}
