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

using stateview;


namespace StateViewer_filedb
{
    public partial class FileDb
    {
        public enum DIRTY
        {
            none,
            MODIFY,
            REMOVEDATA
        }

        public partial class state_chart
        {
            /*
                ※ 項目名、ステート名の変更を考慮して、それぞれをIDで管理する。

                name_id  : 'n'+ HHH   = 4096まで
                state_id : 's'+ HHHH  = 65536まで
            */

            public int max_name_id  = 0;  public const int MAXNAMEID =4095;
            public int max_state_id = 0;  public const int MAXSTATEID=65535;

            public string Generate_name_id()  { if (max_name_id>MAXNAMEID-1000)     return Reuse_nameid();     max_name_id++;  if (max_name_id  > MAXNAMEID)  throw new SystemException("{97940343-49CD-43E6-9908-DA71BFC308EF}"); return  make_nameid(max_name_id);            }
            public string Generate_state_id() { if (max_state_id>MAXSTATEID-10000)  return Reuse_stateid();    max_state_id++; if (max_state_id > MAXSTATEID) throw new SystemException("{8EC51613-C34B-4B0F-9DE4-B0C49E420C73}"); return  make_stateid(max_state_id);           }
            string make_nameid(int num)       { return "n" + num.ToString("x3"); }
            string make_stateid(int num)      { return "s" + num.ToString("x4");  }

            public Dictionary<string,string> id_name_dic;  //項目IDと項目名辞書
            public Dictionary<string,string> id_state_dic; //ステートIDとステート名辞書

            public List<string>              nameid_list;   // NAME  羅列リスト    ※ エクセルでは添え字は１から始まる。このリストは０は飽きとして１から利用する。
            public List<string>              stateid_list;  // STATE 羅列リスト    ※ 同上

            public Dictionary<string, Dictionary<string,string>> id_state_data_dic;  //ステートIDとその内容
            public Dictionary<string, DIRTY>                     id_state_dirty_dic = new Dictionary<string, DIRTY>(); //ステートIDとファイル dirty時は要ファイル更新　そして、falseへ ※ビットマップはハッシュ値の変更時にDirty

            public Dictionary<string, Bitmap> hash_bmp_dic; //ビットマップのハッシュ値とビットマップの辞書

            #region EXCEL用
            public void InitExcel(string excelfile)
            {
                id_name_dic  = new Dictionary<string, string>();
                id_state_dic = new Dictionary<string, string>();

                nameid_list  = new List<string>();
                stateid_list = new List<string>();

                id_state_dic = new Dictionary<string, string>();

                id_state_data_dic = new Dictionary<string, Dictionary<string, string>>();

                hash_bmp_dic = new Dictionary<string, Bitmap>();

                var ed = new ExcelDll();
                { 
                    ed.Load(excelfile);
                    ed.SetSheet(G.sheetchart);

                    var excel_namelist = GetValidRowAndRead_NAME(ed); 
                    Set_name_id_list(excel_namelist);
                
                    var excel_statelist = GetValidColAndRead_STATE(ed);
                    Set_state_id_list(excel_statelist);

                    Set_all_state_data(ed);

                    Set_all_bmp_data(ed);

                    ed.Dispose();
                }
                ed = null;
            }
            /// <summary>
            /// エクセルのname部分のlistから id_name_dicと nameid_listを作成する。
            /// listの0要素は Empty　※ Excelは１から
            /// </summary>
            private void Set_name_id_list(List<string> list)
            {
                for(var n = 0; n<list.Count; n++)
                {
                    var i = list[n];
                    if (!string.IsNullOrEmpty(i))
                    {
                        var id = Generate_name_id();
                        id_name_dic.Add(id,i);
                        nameid_list.Add(id);
                    }
                    else
                    { 
                        nameid_list.Add(string.Empty);
                    }
                }
            }

            /// <summary>
            /// エクセルのstate部分のlistから id_state_dicと stateid_listを作成する。
            /// listの0要素は Empty ※ Excelは１から
            /// </summary>
            private void Set_state_id_list(List<string> list)
            {
                for(var n = 0; n<list.Count; n++)
                {
                    var i = list[n];
                    if (!string.IsNullOrEmpty(i))
                    { 
                        var id = Generate_state_id();
                        id_state_dic.Add(id,i);
                        stateid_list.Add(id);
                    }
                    else
                    {
                        stateid_list.Add(string.Empty);
                    }
                }
            }

            /// <summary>
            /// 全てのデータを保持する。
            /// </summary>
            private void Set_all_state_data(ExcelDll ed)
            {
                foreach(var stateid in stateid_list)
                {
                    if (string.IsNullOrEmpty(stateid)) continue;
                    var dic = new Dictionary<string,string>();
                    var col = Get_col_by_state_id(stateid);
                    foreach(var nameid in nameid_list)
                    {
                        if (string.IsNullOrEmpty(nameid)) continue;
                        var row = Get_row_by_name_id(nameid);
                        var s = ed.GetStr(row,col);
                        if (!string.IsNullOrEmpty(s)) //最適化のため、無いときは登録しない。
                        { 
                            dic.Add(nameid,s);
                        }
                    }
                    id_state_data_dic.Add(stateid,dic);
                }
            }

            /// <summary>
            /// 全てのビットマップデータを保持する。
            /// </summary>
            private void Set_all_bmp_data(ExcelDll ed)
            {
                /*
                ハッシュ値とbmpの辞書を作成
                ステート側のbmp格納場所にハッシュ値を書く #xxxx
                */
                ed.ReadPics(); //全部読む。dllがそのような作りになっているため
                //現在は thumbnailだけが対象
                var row = Get_row_by_name(G.STATENAME_thumbnail);
                if (row > 0)
                { 
                    for(var col = 1; col <= ed.MaxCol(); col++)
                    {
                        var bmp =ed.GetBmp(row,col);
                        if (bmp!=null)
                        {
                            var stateid = Get_state_id_by_col(col);
                            if (stateid!=null)
                            {
                                var statedic = DictionaryUtil.Get(id_state_data_dic,stateid); 
                                if (statedic==null) continue;

                                var nameid = Get_name_id_by_row(row);
                                if (nameid==null) continue;

                                var hash = "#" + BitmapUtil.GetHash_escape_for_filename(bmp);
                                DictionaryUtil.Set(statedic,nameid,hash); //該当ステートのthumbnailにハッシュ値を登録
                                DictionaryUtil.Set(hash_bmp_dic,hash,bmp);
                            }
                        }
                    }
                }
            }

            /// <summary>
            /// マネージャファイルのエクスポート
            /// </summary>
            public void write_filedb_manager(string file)
            {
                var nl = Environment.NewLine;
                var s = "nameid_list="  + CsvUtil.ToCSV( nameid_list )  + nl; 
                s    += "stateid_list=" + CsvUtil.ToCSV( stateid_list ) + nl;   
                s    += "max_name_id=" + max_name_id + nl;
                s    += "max_state_id=" + max_state_id + nl;
                s    += "[id_name_dic]" + nl;
                s    += DictionaryUtil.DicToIni(id_name_dic) + nl;
                s    += "[id_state_dic]" + nl;
                s    += DictionaryUtil.DicToIni(id_state_dic) + nl;

                File.WriteAllText(file,s,Encoding.UTF8);
            }

            /// <summary>
            /// ステートデータを指定パス以下に出力
            /// </summary>
            public void write_filedb_statedata_files(string path)
            {
                foreach(var stateid in id_state_data_dic.Keys)
                {
                    _write_statedata(path, stateid);
                }
                id_state_dirty_dic.Clear();
            }
            /// <summary>
            /// 内容に更新があったステートデータを指定パスに出力
            /// </summary>
            public void write_filedb_statedata_dirty_files(string path)
            {
                foreach(var stateid in id_state_dirty_dic.Keys)
                {
                    if (id_state_dirty_dic[stateid] == DIRTY.MODIFY)
                    { 
                        _write_statedata(path, stateid);
                    }
                    else
                    {
                        _delete_statedata(path,stateid);
                    }
                }
                id_state_dirty_dic.Clear();
            }
            private void _write_statedata(string path, string stateid)
            {
                var nl = Environment.NewLine;
                var s = string.Format("[{0}]", stateid) + nl;
                var orderlist = Get_nameid_list_by_excelorder_for_makeoutput();
                var ht = DictionaryUtil.DicToHashtable(id_state_data_dic[stateid]);
                s += IniUtil.MakeOutput_by_orderkey(ht, orderlist, nl);
                File.WriteAllText(Path.Combine(path, stateid) + ".ini", s, Encoding.UTF8);
            }
            private void _delete_statedata(string path, string stateid)
            {
                var file = Path.Combine(path, stateid) + ".ini";
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            /// <summary>
            /// 全ビットマップファイルを指定パス以下に出力
            /// </summary>
            public void write_filedb_bmp_files(string path)
            {
                FileUtil.DeleteAllFiles(path); //全更新のため

                foreach(var p in hash_bmp_dic)
                {
                    var bmp = p.Value;
                    if (bmp==null) continue;
                    _write_bmp_files(bmp,p.Key,path);
                    //var file = p.Key  + ".png";
                    //if (string.IsNullOrEmpty(file)) continue;
                    //bmp.Save( Path.Combine(path, file),System.Drawing.Imaging.ImageFormat.Png);

                    //var base64str  = BitmapUtil.ToBase64(bmp);
                    //var base64file = p.Key + ".txt";
                    //File.WriteAllText(Path.Combine(path, base64file), base64str, Encoding.UTF8);
                }
            }
            /// <summary>
            /// 指定ビットマップファイルを指定パス以下に出力
            /// </summary>
            public void write_filedb_spefified_bmp_files(string path, List<string> hashlist)
            {
                if (hashlist == null) return;
                
                foreach(var hash in hashlist)
                {
                    if (!hash_bmp_dic.ContainsKey(hash)) throw new SystemException("{B9BC3A17-45F0-4DE9-B26A-F129C0F00CDA}");
                    var bmp = hash_bmp_dic[hash];
                    if (bmp == null) continue;
                    _write_bmp_files(bmp,hash,path);                  
                }
            }
            private bool _write_bmp_files(Bitmap bmp, string fileWoExt, string path)
            {
                var file = fileWoExt  + ".png";
                if (string.IsNullOrEmpty(file)) return false;
                bmp.Save( Path.Combine(path, file),System.Drawing.Imaging.ImageFormat.Png);

                var base64str  = BitmapUtil.ToBase64(bmp);
                var base64file = fileWoExt + ".txt";
                try {
                    File.WriteAllText(Path.Combine(path, base64file), base64str, Encoding.UTF8);
                    return true;
                } catch(SystemException e)
                {
                    throw new SystemException("{82A2B1E6-8851-4FF5-9F65-32C65A5C99D8}" + e.Message);
                }
                
            }


            /// <summary>
            /// データのみよりエクセルファイルを構築する。
            /// ※新しい試み　2019.8.17
            /// </summary>
            public void create_excelfile(string path)
            {
                var exceldata = stateview.Properties.Resources.filedb_new;
                File.WriteAllBytes(path,exceldata);

                var ed = new ExcelDll();

                ed.Load(path);
                ed.SetSheet(G.sheetchart);

                ed.ReadPics();
                
                //name list
                foreach(var nameid in nameid_list)
                {
                    if (string.IsNullOrEmpty(nameid)) continue;
                    if (!id_name_dic.ContainsKey(nameid)) throw new SystemException("{A15603A7-876E-45E0-8468-1FF4CA054EAA}");
                    var row = Get_row_by_name_id(nameid);
                    var name = id_name_dic[nameid];
                    if (row < 0) throw new SystemException("{0FFB649D-78DF-432C-A21B-F211A91AB2A4}");
                    ed.SetStr(row,G.NAME_COL,name);
                    ed.CopyFormat(row,1,row,G.NAME_COL);
                }
                //state
                foreach(var stateid in stateid_list)
                {
                    if (string.IsNullOrEmpty(stateid)) continue;
                    foreach(var nameid in nameid_list)
                    {
                        if (string.IsNullOrEmpty(nameid)) continue;
                        var row = Get_row_by_name_id(nameid);
                        var col = Get_col_by_state_id(stateid);
                        if (row < 0 || col < 0) throw new SystemException("{D82E4583-5E7B-41F6-BF4F-64F4CEE8E3A0}");
                        var val = Get_str_by_stateid_nameid(stateid,nameid);
                        if (!string.IsNullOrEmpty(val))
                        {
                            ed.SetStr(row,col,val);
                            ed.CopyFormat(row,1,row,col);
                        }
                    }
                }
                //bmp
                foreach(var stateid in stateid_list)
                {
                    if (string.IsNullOrEmpty(stateid)) continue;

                    var nameid = Get_name_id_by_name(G.STATENAME_thumbnail);
                    if (string.IsNullOrEmpty(nameid)) continue;

                    var hash = Get_str_by_stateid_nameid(stateid,nameid);
                    if (string.IsNullOrEmpty(hash)) continue;

                    var bmp = DictionaryUtil.Get(hash_bmp_dic,hash);
                    if (bmp==null) continue;

                    var row = Get_row_by_name_id(nameid);
                    var col = Get_col_by_state_id(stateid);
                    if (row < 1 || col < 1) continue;

                    ed.SetBmp( row, col,bmp );
                }
                ed.Save();
            }
            #endregion

            #region ステート追加・削除
            private void Add_new_state(int col, string new_state_name)
            {
                if (!string.IsNullOrEmpty( Get_state_id_by_col(col)) )
                {
                    throw new SystemException("{4DA89D3D-0CE3-4894-9FF7-CC2F08CE8639}"); //新規ステートを作成する場所は、Emptyでなくてはならない！！
                }
                var new_state_id = Generate_state_id(); //新規のステートIDを取得
                ListUtil.SetValForce(ref stateid_list, col, new_state_id);  // stateid_listの該当場所に 新ステートIDをセット。

                id_state_dic.Add(new_state_id, new_state_name); // ステートIDとステート名の関係をセット

                var new_state_data_dic = new Dictionary<string,string>();
                new_state_data_dic.Add( Get_name_id_by_name(G.STATENAME_state), new_state_name  ); //新ステート用の辞書を用意して、ステート名をセット
                id_state_data_dic.Add( new_state_id, new_state_data_dic ); 

                id_state_dirty_dic.Add( new_state_id, DIRTY.MODIFY); //ダーティをセット。つまり、次の更新の反映対象に設定。
            }
            private void Remove_state(int col)
            {
                var stateid = Get_state_id_by_col(col);
                if (string.IsNullOrEmpty( stateid ))
                {
                    throw new SystemException("{13478755-CDAF-4490-B385-F8DAA38E41FC}"); //削除されるのだから、当然値はあるはず
                }
                
                //ListUtil.Remove(ref stateid_list,stateid);
                var index = stateid_list.IndexOf(stateid);
                if (index >= 0)
                {
                    stateid_list[index] = string.Empty;
                }
                DictionaryUtil.Remove(id_state_dic,stateid);
                DictionaryUtil.Remove(id_state_data_dic,stateid);

                var past = DictionaryUtil.Get(id_state_dirty_dic,stateid);
                if (past != DIRTY.REMOVEDATA)
                { 
                    DictionaryUtil.Set(id_state_dirty_dic,stateid,DIRTY.REMOVEDATA);
                }
            }
            #endregion

            #region 便利
            int Get_row_by_name_id(string nameid)   {return nameid_list.IndexOf(nameid);   }
            int Get_col_by_state_id(string stateid) {return stateid_list.IndexOf(stateid); }
            int Get_row_by_name(string name)
            {
                foreach(var p in id_name_dic)
                {
                    if (p.Value == name)
                    {
                        return Get_row_by_name_id(p.Key);
                    }
                }
                return int.MinValue;
            }
            int Get_col_by_state(string state)
            {
                foreach(var p in id_state_dic)
                {
                    if (p.Value == state)
                    {
                        return Get_col_by_state_id(p.Key);
                    }
                }
                return int.MinValue;
            }
            string Get_state_id_by_state(string state)
            {
                foreach(var p in id_state_dic)
                {
                    if (p.Value == state) return p.Key;
                }
                return null;
            }
            string Get_state_id_by_col(int col)
            {
                if (ListUtil.IsValidIndex(stateid_list,col))
                {
                    return stateid_list[col];
                }
                return null;
            }
            string Get_state_by_col(int col)
            {
                var stateid = Get_state_id_by_col(col);
                if (string.IsNullOrEmpty(stateid)) return null;
                return DictionaryUtil.Get(id_state_dic,stateid);
            }
            string Get_name_id_by_row(int row)
            {
                if (ListUtil.IsValidIndex(nameid_list,row))
                {
                    return nameid_list[row];
                }
                return null;
            }
            string Get_name_id_by_name(string name)
            {
                foreach(var p in id_name_dic)
                {
                    if (p.Value == name) return p.Key;
                }
                return null;
            }
            string Get_name_by_row(int row)
            {
                var nameid = Get_name_id_by_row(row);
                if (string.IsNullOrEmpty(nameid)) return null;
                return DictionaryUtil.Get(id_name_dic,nameid);
            }
            List<string> Get_name_list_by_excelorder()
            {
                var list = new List<string>();
                foreach(var n in nameid_list)
                {
                    var name = id_name_dic[n];
                    if (!string.IsNullOrEmpty(name))
                    {
                        list.Add(name);
                    }
                }
                return list;
            }
            List<string> Get_nameid_list_by_excelorder_for_makeoutput()
            {
                var list = new List<string>();
                nameid_list.ForEach(i=> {
                    if (!string.IsNullOrEmpty(i))
                    {
                        list.Add(i);
                    }
                });
                return list;
            }
            string Get_str_by_stateid_nameid(string stateid, string nameid)
            {
                if (string.IsNullOrEmpty(stateid)) return null;
                if (string.IsNullOrEmpty(nameid)) return null;
                if (!id_state_data_dic.ContainsKey(stateid)) throw new SystemException("{6B2614A0-32BC-4A42-A363-C0A86711C694}");
                var dic = id_state_data_dic[stateid];
                if (dic==null) throw new SystemException("{A7782F97-9144-46D7-B880-0AC1F7E2F471}");
                if (!dic.ContainsKey(nameid)) return null;  //最適化のため、カラの値は登録されていない。
                return dic[nameid];
            }
            #endregion

            #region NAME LIST / STATE LIST用
            // 同名のAPIより
            private List<string> GetValidRowAndRead_NAME(ExcelDll ed) //項目欄col=2を走査して、有効なRowのみをハッシュテーブルに格納する 且つ キャッシュに格納
            {
                var list = new List<string>();
                list.Add(string.Empty); //添え字０はEmptyを設定
                for(var row = 1; row <= ed.MaxRow(); row++)
                {
                    var s = ed.GetStr(row,G.NAME_COL);
                    if (ItemNameUtil.IsValid(s))
                    {
                        list.Add(s);
                    }
                    else
                    {
                        list.Add(string.Empty);
                    }
                }
                return list;
            }
            private List<string> GetValidColAndRead_STATE(ExcelDll ed) //state行row=2を走査して、有効なColのみをハッシュテーブルに格納する
            {
                var list = new List<string>();
                list.Add(string.Empty); //添え字０はEmptyを設定
                list.Add(string.Empty); //添え字１はEmptyを設定
                list.Add(string.Empty); //添え字２はEmptyを設定
                for(var col = 3; col <= ed.MaxCol(); col++)
                {
                    var s = ed.GetStr(G.STATE_ROW,col);
                    if (StateUtil.IsValidStateName(s))
                    {
                        list.Add(s);
                    }
                    else
                    {
                        list.Add(string.Empty);
                    }
                }
                return list;
            }

            #endregion
            #region REUSE NAMEID AND STATEID
            private string Reuse_nameid()
            {
                for(var loop = 0; loop<=100; loop++)
                {
                    var sel = RandomUtil.Select(1,MAXNAMEID-1);
                    var cd = make_nameid(sel);
                    if (!nameid_list.Contains(cd)) return cd;
                }
                for(var i = 1; i < MAXNAMEID; i++)
                {
                    var cd = make_nameid(i);
                    if (!nameid_list.Contains(cd)) return cd;
                }
                throw new SystemException("{0371853E-299B-41F2-AC06-8B8AE8101F6D}");
            }
            private string Reuse_stateid()
            {
                for(var loop = 0; loop<=100; loop++)
                {
                    var sel = RandomUtil.Select(1,MAXSTATEID-1);
                    var cd = make_stateid(sel);
                    if (!stateid_list.Contains(cd)) return cd;
                }
                for(var i = 1; i < MAXSTATEID; i++)
                {
                    var cd = make_stateid(i);
                    if (!stateid_list.Contains(cd)) return cd;
                }
                throw new SystemException("{4CC14611-1C74-4872-9775-4700BB08A27D}");
            }
            #endregion
            #region interface
            public int getMaxRow() { return nameid_list.Count;  }
            public int getMaxCol() { return stateid_list.Count; }
            public string getVal(int row, int col)
            {
                if (col == G.NAME_COL)
                {
                    return Get_name_by_row(row);
                }
                if (row == G.STATE_ROW)
                {
                    return Get_state_by_col(col);
                }

                var nameid  = Get_name_id_by_row(row);
                var stateid = Get_state_id_by_col(col);
                if (string.IsNullOrEmpty(nameid))  return null;
                if (string.IsNullOrEmpty(stateid)) return null;
                var v = Get_str_by_stateid_nameid(stateid,nameid);
                return v;
            }
            public int getRowByName(string name)
            {
                return Get_row_by_name(name);
            }
            public bool setVal(int row, int col, string val)
            {
                if (col == G.NAME_COL)
                {
                    var nameid_t = Get_name_id_by_row(row);
                    if (string.IsNullOrEmpty(nameid_t)) throw new SystemException("{AFABBA1C-A372-4E67-A233-2C11388FA1AA}"); //未サポート。おそらく、edit items時に実装予定
                    DictionaryUtil.Set(id_name_dic,nameid_t,val);
                    return true;
                }
                if (row == G.STATE_ROW) //ステートを変更するケース
                {
                    val = val?.Trim();
                    var stateid_t = Get_state_id_by_col(col);

                    if (string.IsNullOrEmpty(stateid_t)) { //新規に対して
                        Add_new_state(col,val);
                        return true;
                    }

                    if (string.IsNullOrEmpty(val)) //ステートの削除
                    {
                        Remove_state(col);
                        return true;
                    }

                    DictionaryUtil.Set(id_state_dic,stateid_t,val);
                    var dic_t = id_state_data_dic[stateid_t];
                    dic_t[ Get_name_id_by_name(G.STATENAME_state) ] = val;

                    DictionaryUtil.Set(id_state_dirty_dic,stateid_t,DIRTY.MODIFY);

                    return true;
                }

                var stateid = Get_state_id_by_col(col);
                if (string.IsNullOrEmpty(stateid)) return false;

                var nameid = Get_name_id_by_row(row);
                if (string.IsNullOrEmpty(nameid)) return false;

                var dic = DictionaryUtil.Get(id_state_data_dic,stateid);
                if (dic==null) { 
                    if ( DictionaryUtil.Get(id_state_dirty_dic,stateid) != DIRTY.REMOVEDATA ) //削除対象
                    { 
                        throw new SystemException("{24AA5F7B-7BC1-4477-99E2-9D1F4C347BA1}");
                    }
                    return true;
                }
                
                dic[nameid] = val;

                DictionaryUtil.Set(id_state_dirty_dic,stateid, DIRTY.MODIFY);

                return true;
            }
            public string getNameByRow(int row)
            {
                var name = Get_name_by_row(row);
                return name;
            }
            public List<string> getNameList() //　0を空きとして、１から
            {
                var list =new List<string>();
                list.Add(""); //空き
                for(var row = 1; row <= getMaxRow(); row++)
                {
                    var name = getNameByRow(row);
                    if (name==null) name= string.Empty;
                    list.Add(name);
                }
                return list;
            }
            public void RenameNAME(int row, string name) //項目名の修正
            {
                if (row < 1) throw new SystemException("{7883F88F-0731-48FA-85C8-28205B1FEC7C}");
                var nameid = Get_name_id_by_row(row);
                if (string.IsNullOrEmpty(nameid))
                { 
                    if (!string.IsNullOrEmpty(name))
                    {
                        var newnameid = Generate_name_id();
                        ListUtil.SetValForce(ref nameid_list,row,newnameid);
                        id_name_dic.Add(newnameid,name);
                    }
                    return;
                }

                if (string.IsNullOrEmpty(name))
                {//設定する名前がないので項目削除
                    _removeNameId(nameid);
                }
                else //新ネームに変更
                { 
                    if (!id_name_dic.ContainsKey(nameid)) throw new SystemException("{F09DF0CF-900D-4978-95C5-F7D714FDF8BE}");
                    id_name_dic[nameid]= name;
                }
            }
            public void RemoveRow(int row) //行の削除
            {
                if (row < 1) throw new SystemException("{378C7FA5-C782-4BA1-8480-39E2A78B962E}");
                var nameid = Get_name_id_by_row(row);
                if (!string.IsNullOrEmpty(nameid)) //行には項目があった
                {
                    _removeNameId(nameid);
                }

                if (row < nameid_list.Count)
                {
                    nameid_list.RemoveAt(row);
                }
            }
            void _removeNameId(string nameid)
            {
                if (string.IsNullOrEmpty(nameid)) throw new SystemException("{F04D2EB8-E030-4477-9315-AA2166CDCC65}"); //対象がない
                var index = nameid_list.IndexOf(nameid);
                if (index < 0) throw new SystemException("{DE52ADF2-DA91-4C44-9B7C-6F54D086730A}");
                nameid_list[index] = string.Empty;

                if (!id_name_dic.ContainsKey(nameid)) throw new SystemException("{14E266D3-E8E4-4DB8-A0F1-3670A29BABBB}");
                id_name_dic.Remove(nameid);

                foreach(var p in id_state_data_dic) //ステートデータ内の該当項目削除
                {
                    var dic = p.Value;
                    DictionaryUtil.Remove(dic,nameid);
                }
            }
            public void InsertRow(int row) //行の挿入
            {
                if (row < 1) throw new SystemException("{B0A38339-550E-448E-ABEF-C7CEF9C296F2}");
                if (row >= nameid_list.Count)
                {
                    ListUtil.SetValForce(ref nameid_list,row,string.Empty);
                }
                else
                {
                    nameid_list.Insert(row,string.Empty);
                }
            }
            public void CopyRowAndEmptySrc(int srcrow, int dstrow)//指定行にコピーして、元はカラに。
            {
                //コピー先の確認
                if (dstrow < 1 || dstrow >= nameid_list.Count) new SystemException("{6D1B79C9-1DB5-4E74-8133-068E45C92F6A}");
                //コピー元の確認
                if (srcrow < 1 || srcrow >= nameid_list.Count) new SystemException("{A858C258-DD48-4905-A353-B097AE63D09A}");
                //コピー元にはnameidあり、コピー先にはないと仮定
                if (srcrow < nameid_list.Count && string.IsNullOrEmpty( nameid_list[srcrow] )) new SystemException("{AD713434-8ECC-48CA-86D3-923180F82DDF}");
                if (!string.IsNullOrEmpty( nameid_list[dstrow])) new SystemException("{6509104D-635D-4F6A-B3D0-04B872E120F6}");

                ListUtil.SetValForce(ref nameid_list,dstrow, ListUtil.GetVal(nameid_list,srcrow) ); // nameid_list[dstrow] = nameid_list[srcrow];
                ListUtil.SetValForce(ref nameid_list,srcrow, string.Empty);         // nameid_list[srcrow] = string.Empty;

            }
            public string GetBitmapString(string hash)
            {
                var bmp = DictionaryUtil.Get( hash_bmp_dic, hash);
                if (bmp==null) return null;
                return BitmapUtil.ToBase64(bmp);
            }
            public void RegisterBitmap(string hash, string data)
            {
                if (hash_bmp_dic.ContainsKey(hash)) return;
                var bmp = BitmapUtil.FromBase64(data);
                if (bmp==null) return;
                hash_bmp_dic.Add(hash,bmp);
            }
            public void RemoveUnusedBitmap()
            {
                if (id_state_dic == null) throw new SystemException("{D2A56F7F-3236-4259-B180-FD13695E2CB0}");
                if (hash_bmp_dic == null) throw new SystemException("{28DD45F4-E1FB-45E9-BC1B-57D4B7928E33}");

                var using_hash_list= new List<string>();
                var nameid = Get_name_id_by_name(G.STATENAME_thumbnail);
                foreach(var state_id in id_state_dic.Keys)
                {
                    var val = Get_str_by_stateid_nameid(state_id,nameid);
                    if (!string.IsNullOrEmpty(val) && val[0]=='#')
                    {
                        using_hash_list.Add(val);
                    }
                }
                var remove_hash_list = new List<string>();
                foreach(var hash in hash_bmp_dic.Keys)
                {
                    if (!using_hash_list.Contains(hash))
                    {
                        if (!remove_hash_list.Contains(hash)) { 
                            remove_hash_list.Add(hash);
                        }
                    }
                }
                remove_hash_list.ForEach(i=>hash_bmp_dic.Remove(i));
                //foreach(var hash in remove_hash_list)
                //{
                //    _delete_bitmap_files(path,hash);
                //}
            }
            //private void _delete_bitmap_files(string path, string hash)
            //{
            //    try { 
            //        var txtfile = Path.Combine(path,hash + ".txt");
            //        if (File.Exists(txtfile))
            //        {
            //            File.Delete(txtfile);
            //        }
            //        var pngfile = Path.Combine(path,hash + ".png");
            //        if (File.Exists(pngfile))
            //        {
            //            File.Delete(pngfile);
            //        }
            //    } catch (SystemException e)
            //    {
            //        G.NoticeToUser_warning( "{8CD1EEE7-8C73-466C-8B7C-BA3CD555DA22} " + e.Message);
            //    }
            //}

            #endregion
        }
    }
}
