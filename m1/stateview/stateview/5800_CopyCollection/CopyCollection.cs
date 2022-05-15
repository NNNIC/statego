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
using System.Drawing.Imaging;

/*
    コピーコレクション　ツール
    G.ccにてアクセス
*/

namespace stateview
{
    public class CopyCollection
    {
        #region 定義
        public const string STATEGOWORK_DIR    = "StateGoWork";
        public const string COPYCOLLECTION_DIR = "CopyCollection";
        public const string COLLECTION_DIR     = "_Collection";

        /*Obsolete*/
        public const string TEMP_DIR           = "_Temp";   // 廃止へ
        /*Obsolete*/
        public const string TRASH_DIR          = "_Trash";  // 廃止へ   //削除を保存
        public const string PRESET_DIR         = "_Preset";

        public const string TEMP2_DIR          = "_Temp2";   //20201129 一時フォルダ内へ
        public const string TRASH2_DIR         = "_Trash2";  //20201129 一時フォルダ内へ
        public const string SYSTEMP_DIR        = "systemp";  //20201129 一時フォルダ内へ
        public const string SYSTEMP_DIR_WB     = "systemp/"; //20201129 一時フォルダ内へ

        public const string CAP_FILE           = "cap.png";
        public const string ICON_FILE          = "icon.png";
        public const string INFOINI_FILE       = "info.ini";
        public const string DATA_FILE          = "data.txt";

        public const string SUBINI_FILE        = "sub.ini";

        public const string INFO_CREATETIME      = "createtime";
        public const string INFO_CREATELOCALTIME = "createlocaltime";
        public const string INFO_STATEGOFILE     = "stategofile";
        public const string INFO_NAME            = "name";
        public const string INFO_KEEP            = "keep";
        public const string INFO_STATELIST       = "statelist";
        public const string INFO_COMMENT_JP      = "comment_jp";
        public const string INFO_COMMENT_EN      = "comment_en";
        public const string INFO_COPYCOUNT       = "copycount";     //廃止してファイル化

        public const string COPYCOUNT_FILE       = "copycount.bin";  //コピーカウンタ
        public const string DISPORDER_FILE       = "disporder.bin"; //表示順番

        #endregion

        #region プリセット登場による、readonlyモード
        public bool m_read_only =false;
        #endregion

        //#region form
        //public stateview._5800_CopyCollection.CCForm m_ccform;
        //#endregion

        #region 作業フォルダ関連

        public string STATEGOWRIK_COPYCOLLECTION_DIR {
            get { return Path.Combine( STATEGOWORK_DIR , COPYCOLLECTION_DIR); }
        }

        /// <summary>
        ///  CopyCollectionの下のCollectionフォルダへフルパス
        ///  注意：CopyCollection下！！！
        /// </summary>
        public string m_collection_fullpath {  
            get {
                if (string.IsNullOrEmpty(m_copycollectionFolder_fullpath)) return null;
                return  Path.Combine(m_copycollectionFolder_fullpath, COLLECTION_DIR);
            }
        }

        /// <summary>
        ///  CopyCollectionの下のTempフォルダへフルパス
        /// </summary>
        /*Obsolete*/
        public string m_temp_fullpath {
            get { return Path.Combine(m_copycollectionFolder_fullpath, TEMP_DIR); }
        }

        /// <summary>
        ///  CopyCollectionの下のTrashフォルダへフルパス
        /// </summary>
        /*Obsolete*/
        public string m_trash_fullpath {
            get { return Path.Combine(m_copycollectionFolder_fullpath, TRASH_DIR); }
        }

        /// <summary>
        /// システム一時フォルダ内のCopyCollectionの下のTemp2フォルダへフルパス
        /// </summary>
        public string m_temp2_fullpath {
            get { return Path.Combine(m_copycollection_systempFolder_fullpath, TEMP2_DIR); }
        }
        /// <summary>
        /// システム一時フォルダ内のCopyCollectionの下のTrash2フォルダへフルパス
        /// </summary>
        public string m_trash2_fullpath {
            get { return Path.Combine(m_copycollection_systempFolder_fullpath, TRASH2_DIR); }
        }
        

        /// <summary>
        /// CoopyCollectionの下のPresetフォルダへのフルパス
        /// </summary>
        public string m_preset_fullpath {
            get { return Path.Combine(m_copycollectionFolder_fullpath, PRESET_DIR); }
        }


        public string m_stategoWorkFolder_fullpath    { get; private set; }  // ユーザ
        public string m_copycollectionFolder_fullpath { get; private set; }  // ユーザ
        public string m_copycollection_systempFolder_fullpath { get; private set; }  //Temp2,Trash2用

        #region ワークフォルダー作成
        public void create_workfolder(string targetdir)
        {
            // target/StateGoWork/CopyCollection
            if (!Directory.Exists(targetdir))
            {
                throw new Exception("{A638C2A9-A7D8-4F09-91C8-B5AB3335B9D4}");
            }

            var createpath = Path.Combine(targetdir, STATEGOWRIK_COPYCOLLECTION_DIR);

            Directory.CreateDirectory(createpath);

            SetWorkFolderIfExists();

            create_sub_workfolder(m_collection_fullpath, "Collection folder","Collection folder","コレクションフォルダ",true);

            SetAndCreateSysTempFolferIfNotExists();

            create_sub_workfolder(m_temp_fullpath,"Temp","Temporary folder for copy data","コピーデータの一時的保管用フォルダ",true);
            create_sub_workfolder(m_trash_fullpath,"Trash","Trash folder","ゴミ箱",true);

            create_sub_workfolder(m_preset_fullpath,"Preset","Preset folder","プリセット フォルダ",true);

        }
        /// <summary>
        ///  サブフォルダを作成します。
        ///  keepをすると消すユーザ削除対象から外れます
        ///  recreate_iniを指定すると、iniがあっても上書きされます。
        /// </summary>
        public void create_sub_workfolder(string path, string name, string comment_en, string comment_jp, bool keep = false, bool recreate_ini=false)
        {
            if (m_read_only) return;

            if (Directory.Exists(path))
            {
                G.NoticeToUser("exists " + path +". But try to renew");
            }
            else
            { 
                Directory.CreateDirectory(path);
            }

            var ht = new Hashtable();
            ht[INFO_NAME] = name;
            ht[INFO_COMMENT_EN] = comment_en;
            ht[INFO_COMMENT_JP] = comment_jp;
            var now = DateTime.UtcNow;
            ht[INFO_CREATETIME] = now.ToBinary();
            ht[INFO_CREATELOCALTIME] = now.ToLocalTime().ToString();
            ht[INFO_KEEP] = keep.ToString();
            
            var ini = IniUtil.MakeOutput(ht);
            var inipath  = Path.Combine(path,SUBINI_FILE);

            if (recreate_ini || !File.Exists(inipath))
            {
                File.WriteAllText(inipath,ini,Encoding.UTF8);
            }
        }
        #endregion

        #region ワークフォルダをスタートキットからコピーする
        public void copy_workfolder_from_kit( string referencedir, string targetdir)
        {
            FileUtil.CopyDir(referencedir,Path.Combine(targetdir, STATEGOWRIK_COPYCOLLECTION_DIR));
            SetWorkFolderIfExists();
            SetAndCreateSysTempFolferIfNotExists();
        }
        #endregion

        public string FindCopyCollectionFolder()
        {
            var psggfile = Path.GetFullPath(G.psgg_file);
            var path = Path.GetDirectoryName(psggfile);

            while(!string.IsNullOrEmpty(path))
            {
                var checkpath = Path.Combine(path, STATEGOWRIK_COPYCOLLECTION_DIR);
                if (Directory.Exists(checkpath))
                {
                    return checkpath;
                }

                var newpath = Path.GetFullPath(path + @"\..");
                if (newpath == path)
                {
                    break;
                }
                path = newpath;
            }
            return null;
        }
        /// <summary>
        /// Collection直下にサンプルフォルダを作成する。
        /// </summary>
        public void create_collection_sample_folder(string name, double disporder = double.MinValue)
        {
            if (m_read_only) return;

            var folder = "_" + Guid.NewGuid().ToString();
            var fullpath = Path.Combine( m_collection_fullpath, folder);
            Directory.CreateDirectory(fullpath);

            var ht = new Hashtable();
            var now = DateTime.UtcNow;
            ht[INFO_CREATETIME] = now.ToBinary();
            ht[INFO_CREATELOCALTIME] = now.ToLocalTime().ToString();
            ht[INFO_NAME] = name;
            ht[INFO_COMMENT_JP] = "サンプルです。";
            ht[INFO_COMMENT_EN] = "This is a sample.";
            var s = IniUtil.MakeOutput(ht);

            var subinipath = Path.Combine(fullpath, SUBINI_FILE);
            File.WriteAllText(subinipath, s, Encoding.UTF8);
            
            save_disporder(fullpath,disporder);
        }

        /// <summary>
        /// システム一時フォルダ内のTemp2,Trash2のセットアップ
        /// </summary>
        public void SetAndCreateSysTempFolferIfNotExists()
        {
            if (m_read_only) return;
            if (string.IsNullOrEmpty( m_copycollectionFolder_fullpath)) return;
            if (!Directory.Exists(m_copycollectionFolder_fullpath)) return;
            try {
                var hash = G.userenv_guid;//m_copycollectionFolder_fullpath.GetHashCode();
                m_copycollection_systempFolder_fullpath = Path.Combine( Path.GetTempPath(),"psgg_cc" + hash, SYSTEMP_DIR);
                if (!Directory.Exists(m_copycollection_systempFolder_fullpath))
                {
                    Directory.CreateDirectory(m_copycollection_systempFolder_fullpath);
                }
                create_sub_workfolder(m_temp2_fullpath,"Temp2","Temporary folder for StateGo copy data","StateGoコピーデータの一時的保管用フォルダ",true);
                create_sub_workfolder(m_trash2_fullpath,"Trash2","Trash folder for StateGo cc data","StateGo CC用ゴミ箱",true);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{7B3A6B28-883D-4B2F-839F-9A6953F97E57}" + e.Message);
            }
        }

        public bool SetWorkFolderIfExists()
        {
            try {
                if (SetWorkFolderIfExists_in_user_space())
                {
                    return true;
                }
                return SetWorkFolferIfExists_in_system_space();
            } catch (System.Exception e)
            {
                return false;
            }
        }
        private bool SetWorkFolderIfExists_in_user_space()
        {
            var path = FindCopyCollectionFolder();
            if (path!=null)
            {
                m_copycollectionFolder_fullpath = Path.GetFullPath( path );
                m_stategoWorkFolder_fullpath = Path.GetFullPath(Path.Combine(path, @"..\")).TrimEnd('\\');

                G.view_form.label_cc.Visible = true;
                m_read_only = false;

                return true;
            }

            G.view_form.label_cc.Visible = false;

            return false;
        }
        private bool SetWorkFolferIfExists_in_system_space()
        {
            var kitpath = SettingIniUtil.GetKitPath();
            if (string.IsNullOrEmpty(kitpath)) return false;
            var path = Path.Combine( kitpath, STATEGOWRIK_COPYCOLLECTION_DIR);
            if (Directory.Exists(path))
            {
                m_copycollectionFolder_fullpath = Path.GetFullPath( path );
                m_stategoWorkFolder_fullpath = Path.GetFullPath(Path.Combine(path, @"..\")).TrimEnd('\\');

                G.view_form.label_cc.Visible = true;
                m_read_only = true;
                
                return true;
            }

            return false;
        }
        #endregion

        #region エキスポート関連
        public string copydata_path; //コピー＆ペースト時の格納変数
        public string MakeCopyData(List<string> statelist, string s, bool updateIfExists)
        {
            if (updateIfExists==false)
            {
                return MakeCopyData(statelist,s);
                
            }

            if (
                string.IsNullOrEmpty( m_copycollectionFolder_fullpath)
                ||
                !Directory.Exists( m_copycollectionFolder_fullpath)
                )
            {
                G.NoticeToUser(G.Localize("cc_noworkdir"));
                return null;
            }
            /*
              ※_Temp配下のみ対象とする。
                同じのはないかを検索する。
                １．stategofileが同じ
                ２．statelistが同じ
                ※ sは比較しない。
                　 sは更新される。
            */
            var templist = GetTempItems();
            if (templist!=null && templist.Count > 0)
            {
                var statego_filepath = get_statego_filepath();
                var find = default(WorkItem);
                for(var n = 0; n<templist.Count; n++)
                {
                    var target = templist[n];
                    if (target.stategofile == statego_filepath && ListUtil.IsEqual_wo_Order(target.list_statelist(), statelist))
                    {
                        find = target;
                        break;
                    }
                }
                if (find != null)
                {
                    //find.copycount++;
                    //save_copycount(find.folder,find.copycount);
                    save_data(find.folder,s);
                    G.NoticeToUser(G.Localize("cc_alreadyhas"));
                    return find.folder;
                }
            }

            return MakeCopyData(statelist,s);
        }

        string MakeCopyData(List<string> statelist, string s)
        {
            if (
                string.IsNullOrEmpty( m_copycollectionFolder_fullpath)
                ||
                !Directory.Exists( m_copycollectionFolder_fullpath)
                )
            {
                G.NoticeToUser(G.Localize("cc_noworkdir"));
                return null;
            }

            if (m_read_only) return null;

            var id = Guid.NewGuid().ToString();
            var datadir = Path.Combine(m_temp2_fullpath, id);
            Directory.CreateDirectory(datadir);

            save_image(statelist, Path.Combine(datadir, CAP_FILE));
            save_ini(statelist,datadir);
            save_disporder(datadir);
            save_copycount(datadir,0);
            save_data(datadir, s); //File.WriteAllText(Path.Combine(datadir, DATA_FILE),s,Encoding.UTF8);
            G.NoticeToUser(G.Localize("cc_madefiles"));

            return datadir;
        }
        string make_name_from_statelist(List<string> statelist,int max)
        {
            var name = string.Empty;
            foreach(var s in statelist)
            {
                var s2 = string.Empty;
                if (AltState.IsAltState(s))
                {
                    s2 = "{" + AltState.TrimAltStateName(s) + "}";
                }
                else
                {
                    s2 = s.Length>2 ? s.Substring(2) : s;
                }
                if (!string.IsNullOrEmpty(name)) name +=",";
                name += s2;
            }
            if (name.Length > max) name = name.Substring(0,max) + "...";

            return name;
        }
        string make_comment_from_statelist(List<string> statelist, int max)
        {
            var comment = string.Empty;
            foreach(var s in statelist)
            {
                var tmpcmt = G.excel_program.GetString(s,G.STATENAME_statecmt);
                if (string.IsNullOrEmpty(tmpcmt)) continue;
                if (!string.IsNullOrEmpty(comment)) comment += System.Environment.NewLine;
                comment += tmpcmt;
            }
            if (string.IsNullOrEmpty(comment))
            {
                comment = "?";
            }

            if (comment.Length > max)
            {
                comment = comment.Substring(0,max) + "...";
            }
            return comment;
        }
        void save_image(List<string> statelist, string savepath)
        {
            //
            RectangleF rect = new RectangleF(-1,-1,1,1);
            foreach(var s in statelist)
            {
                var dd = DictionaryUtil.Get(G.m_draw_data_list,s);
                if (dd==null)
                {
                    G.NoticeToUser_warning("{525846B5-AFB7-48F2-A194-3BAA13159B84}");
                    continue;
                }
                if (rect.X < 0)
                {   
                    rect = dd.wp_outframe_drect;
                    if (!string.IsNullOrEmpty( G.excel_program.GetString(s,G.STATENAME_statetyp)))
                    {
                        rect = RectangleUtil.AddMargin(rect,25);
                    }
                }
                else
                {
                    rect = RectangleUtil.CombineF(rect, dd.wp_outframe_drect);
                }
            }

            var newrect = RectangleUtil.ClampMax(rect,G.mainbitmap.Width,G.mainbitmap.Height);

            var bmp = G.mainbitmap.Clone(newrect,G.mainbitmap.PixelFormat);
            bmp.Save(savepath,ImageFormat.Png);
            bmp.Dispose();
        }
        #endregion

        #region 作業領域の変化を確認するためのハッシュ作成
        public int MakeChangeHash()
        {
            if (!Directory.Exists(m_copycollectionFolder_fullpath))
            {
                return 0;
            }
            return DirUtil.MakeChangeHash(m_copycollectionFolder_fullpath);
        }
        #endregion

        #region  作業アイテム・ 作業フォルダからのデータ収集

        /// <summary>
        /// 作業アイデムの格納クラス
        /// ※作業フォルダからデータを収集して、本クラスへ収集する。
        /// 階層を構造をもつため、ディレクトリの場合もある。
        /// </summary>
        public class WorkItem {
            public string   uuid;

            public bool     bSysTemp;    //システム一時フォルダ内

            public string   folder;      //本アイテムデータへのパス
            public bool     bDir; 　　　 //階層ディレクトリ？
            public bool     bKeep;       //キープ。削除禁止

            public string   name;        //名前
            public string   comment_en; 
            public string   comment_jp;

            public DateTime createtime;  //作成日時 (utc)
            public string   stategofile; 
            public string   statelist;

            public int      copycount; //コピーされた数
            public double   disporder; //表示用オーダー


            //格納物へのパス。 存在しないときはNULLを格納
            public string   inipath;
            public string   cappng_path;
            public string   iconpng_path;
            public string   datatxt_path;
            public string   copycount_path;
            public string   disporder_path;

            // ---- work --- 便宜上の処置
            public Bitmap   bitmap;
            public string   comment {
                get { return G.bJorE ? comment_jp : comment_en; }
                set { if (G.bJorE) { 
                          comment_jp = value;
                      }
                      else {
                          comment_en = value;
                      }
                }  
            }
            //public string dictionary_key {
            //    get {
            //        var parentpath = Path.GetFullPath(folder + @"\..");
            //        var relpath = parentpath.Substring(G.cc.m_copycollectionFolder_fullpath.Length);
            //        var key = relpath.Replace(@"\","/");
            //        if (string.IsNullOrEmpty(key)) key = "/";
            //        return key;
            //    }
            //}
            public string create_dickey()
            {
                var parentpath = Path.GetFullPath(folder + @"\..");
                if (bSysTemp)
                {
                    var relpath = parentpath.Substring(G.cc.m_copycollection_systempFolder_fullpath.Length);
                    var key = relpath.Replace(@"\","/");
                    if (string.IsNullOrEmpty(key)) key = "/";
                    key = SYSTEMP_DIR + key;
                    return key;
                }
                else
                {
                    var relpath = parentpath.Substring(G.cc.m_copycollectionFolder_fullpath.Length);
                    var key = relpath.Replace(@"\","/");
                    if (string.IsNullOrEmpty(key)) key = "/";
                    return key;
                }
            }
            public List<string> list_statelist()//statelistを List<>化
            {
                if (statelist==null) return null;
                var tokens = statelist.Split(',');
                return new List<string>(tokens);
            }
            /// <summary>
            ///  アイテムのクローン作製
            ///  ※ uuidは新規に
            ///  ※ 同一フォルダの場合は、nameにcopy ofが追加
            ///  ※フォルダ、ファイル作成はしない。
            /// </summary>
            public WorkItem Clone(string newparentfolder)
            {
                var item2    = new WorkItem();
                item2.uuid   = (bDir ? "_" : "" ) + Guid.NewGuid().ToString();
                item2.folder = Path.Combine(newparentfolder, item2.uuid) ;
                item2.name   = (item2.create_dickey()  ==  create_dickey() ? "copy of " : "" ) + name;
                item2.comment_en = comment_en;
                item2.comment_jp = comment_jp;

                item2.createtime  = createtime;
                item2.stategofile = stategofile;
                item2.statelist   = statelist;
                item2.copycount   = copycount;

                item2.inipath      = Path.Combine(item2.folder, (bDir ? SUBINI_FILE : INFOINI_FILE));
                item2.cappng_path  = cappng_path !=null ? Path.Combine(item2.folder, CAP_FILE) : null;
                item2.iconpng_path = iconpng_path!=null ? Path.Combine(item2.folder, INFOINI_FILE) : null;
                item2.datatxt_path = datatxt_path!=null ? Path.Combine(item2.folder, DATA_FILE) : null;

                item2.copycount_path = Path.Combine(item2.folder, COPYCOUNT_FILE);
                item2.disporder_path = Path.Combine(item2.folder, DISPORDER_FILE);

                item2.bitmap       = bitmap!=null ? new Bitmap( bitmap) : null;
                
                return item2;
            }
        }

        Dictionary<string, List<WorkItem> > m_work_dic = null;

        void  work_dic_add(WorkItem i){
            var key = i.create_dickey();
            var list = DictionaryUtil.Get(m_work_dic,key);
            if (list == null) list = new List<WorkItem>();
            ListUtil.AddValIfNotExist(ref list, i);
            DictionaryUtil.Set(m_work_dic,key,list);
        }
        void work_dic_del(WorkItem i) {

            if (i.bitmap != null)
            {
                i.bitmap.Dispose();
                i.bitmap = null;
            }

            var key = i.create_dickey();
            var list = DictionaryUtil.Get(m_work_dic,key);
            if (list == null) {
                G.NoticeToUser_warning("{BB3DBE23-D835-4E3A-B91C-EA5CFEF928C2}");
                return;
            }
            var index = list.FindIndex(_=>_.uuid == i.uuid);
            if (index < 0)
            {
                G.NoticeToUser_warning("{FF24745A-040C-46E6-B839-F4045BEF0E7A}");
                return;
            }
            list.RemoveAt(index);
            DictionaryUtil.Set(m_work_dic,i.create_dickey(),list);
        }
        /// <summary>
        /// 作業フォルダから 作業辞書作成
        /// </summary>
        public void read_work_date()
        {
            // m_work_dicを初期化
            if (m_work_dic!=null) 
            {
                foreach(var key in m_work_dic.Keys)
                {
                    var list= m_work_dic[key];
                    if (list!=null)
                    {
                        list.ForEach(i=>{
                            if (i.bitmap!=null) i.bitmap.Dispose();
                            i.bitmap = null;
                        });
                    }
                }
            }
            m_work_dic = new Dictionary<string, List<WorkItem>>();

            //Action<string, WorkItem> _add = (p,i) => {
            //    var list = DictionaryUtil.Get(m_work_dic,p);
            //    if (list == null) list = new List<WorkItem>();
            //    ListUtil.AddValIfNotExist(ref list, i);
            //    DictionaryUtil.Set(m_work_dic,p,list);
            //};

            double dammy_count = 0; // もしdisporderがないときに、一気によむと差分がなくなるから

            Action<bool,string> _make = null;
            _make = (bSysTemp, path)=> {
                if (!Directory.Exists(path)) return;
                var di = new DirectoryInfo(path);
                foreach(var di2 in di.GetDirectories())
                {
                    var item = new WorkItem();
                    item.bDir = di2.Name.StartsWith("_");
                    item.bSysTemp = bSysTemp;
                    if (item.bDir)
                    {
                        item.uuid = di2.Name;
                        var subpath = Path.Combine(path,di2.Name);
                        item.folder = subpath;
                        var subinipath = Path.Combine(subpath, SUBINI_FILE);
                        item.inipath = subinipath;
                        if (File.Exists(subinipath))
                        {
                            var ini = File.ReadAllText(subinipath, Encoding.UTF8);
                            var ht = IniUtil.CreateHashtable(ini);
                            item.name       = IniUtil.GetValueFromHashtable(INFO_NAME,ht);
                            item.comment_en = IniUtil.GetValueFromHashtable(INFO_COMMENT_EN,ht);
                            item.comment_jp = IniUtil.GetValueFromHashtable(INFO_COMMENT_JP,ht);
                            item.bKeep      = ParseUtil.ParseBool( IniUtil.GetValueFromHashtable(INFO_KEEP,ht));
                            var datebin = ParseUtil.ParseLongInt(IniUtil.GetValueFromHashtable(INFO_CREATETIME,ht));
                            item.createtime = DateTime.FromBinary(datebin); 
                        }

                        item.disporder = read_disporder_ifFailCreate(item.folder, create_dispordernum() + dammy_count);
                        dammy_count += 0.1f;

                        work_dic_add(item);
                        _make(bSysTemp, subpath);
                        continue;                                        
                    }                
                    else
                    {
                        item.uuid = di2.Name;

                        var datapath = Path.Combine(path, di2.Name);
                        item.folder = datapath;                             
                        var datainipath = Path.Combine(datapath, INFOINI_FILE);

                        var cappngpath = Path.Combine(datapath,CAP_FILE);
                        if (File.Exists(cappngpath)) {
                            item.cappng_path = cappngpath;
                        }

                        var iconpngath = Path.Combine(datapath,ICON_FILE);
                        if (File.Exists(iconpngath))
                        {
                            item.iconpng_path = iconpngath;
                        }

                        var datatxtpath = Path.Combine(datapath,DATA_FILE);
                        if (File.Exists(datatxtpath))
                        {
                            item.datatxt_path = datatxtpath;
                        }

                        if (File.Exists(datainipath))
                        {
                            try {
                                item.inipath = datainipath;
                                var ini = File.ReadAllText(datainipath, Encoding.UTF8);
                                var ht = IniUtil.CreateHashtable(ini);
                                item.name        = IniUtil.GetValueFromHashtable(INFO_NAME,ht);
                                item.comment_en  = IniUtil.GetValueFromHashtable(INFO_COMMENT_EN,ht);
                                item.comment_jp  = IniUtil.GetValueFromHashtable(INFO_COMMENT_JP,ht);

                                var datebin = ParseUtil.ParseLongInt(IniUtil.GetValueFromHashtable(INFO_CREATETIME,ht));

                                item.createtime  = DateTime.FromBinary(datebin);
                                item.stategofile = IniUtil.GetValueFromHashtable(INFO_STATEGOFILE,ht);
                                item.statelist   = IniUtil.GetValueFromHashtable(INFO_STATELIST,ht);
                                //item.copycount   = IniUtil.GetParsedValueFromHashtable<int>(INFO_COPYCOUNT,ht);

                                var bmpfile = !string.IsNullOrEmpty( item.iconpng_path ) ? item.iconpng_path : item.cappng_path;
                                try {
                                    if (File.Exists(bmpfile))
                                    {
                                        var tmp = new Bitmap(bmpfile);           
                                        item.bitmap = new Bitmap(tmp);
                                        tmp.Dispose();
                                        tmp = null;           
                                    }
                                } catch (SystemException e)
                                {
                                    G.NoticeToUser_warning("{A285D2C4-060A-40C3-8021-CC83062B7D5E} " + e.Message);
                                }
                            } catch (SystemException e)
                            {
                                G.NoticeToUser_warning("{85F2C879-F154-4C18-95C4-0860E21D0188}" + e.Message);
                            }
                        }
                        item.disporder = read_disporder_ifFailCreate(item.folder,create_dispordernum() + dammy_count);
                        dammy_count += 0.1f;
                        item.disporder_path = Path.Combine(item.folder, DISPORDER_FILE);

                        item.copycount = read_copycount(item.folder);
                        item.copycount_path = Path.Combine(item.folder, COPYCOUNT_FILE);

                        work_dic_add(item);
                    }
                }
            };

            try {
                //作業データ内登録は CollectionとTempフォルダに分かれる
                _make(false,m_copycollectionFolder_fullpath); // ユーザフォルダ内
                _make(true,m_copycollection_systempFolder_fullpath);  // システム一時フォルダ内
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{563BC59A-83EF-4427-85D4-5C0DD441441A}" + e.Message);
            }
        }
        public List<WorkItem> GetTempItems()
        {
            var key = SYSTEMP_DIR_WB + TEMP2_DIR;
            var items = DictionaryUtil.Get(m_work_dic,key);
            if (items!=null) {
                items.Sort((a,b)=> {
                    if (a!=null && a.name != null && b!=null && b.name !=null) {
                        return a.name.CompareTo(b.name);
                    }
                    return 0;
                });
            }
            return items;
        }

        public List<WorkItem> GetTrashItems()
        {
            var key = SYSTEMP_DIR_WB + TRASH2_DIR;
            var items = DictionaryUtil.Get(m_work_dic,key);
            if (items!=null) {
                items.Sort((a,b)=> {
                    if (a!=null && a.name != null && b!=null && b.name !=null) {
                        return a.name.CompareTo(b.name);
                    }
                    return 0;
                });
            }
            return items;
        }
        public WorkItem GetTempItem()
        {
            var find = FindItem(TEMP2_DIR,SYSTEMP_DIR_WB);
            return find;
        }
        public WorkItem GetTrashItem()
        {
            var find = FindItem(TRASH2_DIR,SYSTEMP_DIR_WB);
            return find;
        }
        public WorkItem FindItem(string uuid)
        {
            foreach(var k in m_work_dic.Keys)
            {
                var list = m_work_dic[k];
                foreach(var i in list)
                {
                    if (i.uuid == uuid)
                    {
                        return i;
                    }
                }
            }
            return null;
        }
        public WorkItem FindItem(string uuid, WorkItem pageitem)
        {
            var items = GetItemsIfFolder(pageitem);

            foreach(var i in items)
            {
                if (i.uuid == uuid)
                {
                    return i;
                }
            }
            return null;
        }
        public WorkItem FindItem(string uuid, string key)
        {
            if (m_work_dic.ContainsKey(key))
            {
                var list = m_work_dic[key];
                foreach(var i in list)
                {
                    if (i.uuid == uuid)
                    {
                        return i;
                    }
                }
            }
            return null;
        }
        public WorkItem GetCollectionItem()
        {
            return FindItem(COLLECTION_DIR, "/");
        }
        public List<WorkItem> GetCollectionRootItems()
        {
            var key = "/" + COLLECTION_DIR;
            var list = DictionaryUtil.Get(m_work_dic,key);
            if (list != null)
            {
                sort_workitem_list(ref list);
            }
            return list;
        }
        private void sort_workitem_list(ref List<WorkItem> list)
        {
            list.Sort((a,b)=> {
                if (a!=null && b!=null)
                {
                    return a.disporder.CompareTo(b.disporder);
                }
                return 0;
            });
        }
        public List<WorkItem> GetCollectionItems(string key)
        {
            var list = DictionaryUtil.Get(m_work_dic,key);
            if (list != null)
            {
                sort_workitem_list(ref list);
            }
            return list;
        }
        public List<WorkItem> GetItemsIfFolder(WorkItem folderitem)
        {
            var folderkey= folderitem.create_dickey();
            var key = "";
            if (folderitem.bSysTemp)
            {
                key = folderkey +  folderitem.uuid;
            }
            else
            {
                key =  folderkey +  (folderkey == "/" ? "": "/") + folderitem.uuid;
            }
            var list = DictionaryUtil.Get(m_work_dic,key);
            if (list != null) {
                sort_workitem_list(ref list);
            }
            return list;
        }
        #endregion

        #region データの更新  
        #endregion

        #region クローン・削除
        public WorkItem CloneItem(WorkItem item, string newparentfolder)
        {
            if (m_read_only) return null;

            var item2 = item.Clone(newparentfolder);
            Directory.CreateDirectory(item2.folder);

            if (item.inipath!=null && File.Exists(item.inipath))
            {
                var srcini = File.ReadAllText(item.inipath,Encoding.UTF8);
                var ht = IniUtil.CreateHashtable(srcini);
                ht[INFO_NAME] = item2.name; //copy of が追加される場合対応
                var ini = IniUtil.MakeOutput(ht);
                File.WriteAllText(item2.inipath,ini, Encoding.UTF8);
            }
            if (item.cappng_path!=null  && File.Exists(item.cappng_path))  File.Copy(item.cappng_path,  item2.cappng_path,true);
            if (item.iconpng_path!=null && File.Exists(item.iconpng_path)) File.Copy(item.iconpng_path, item2.iconpng_path,true);
            if (item.datatxt_path!=null && File.Exists(item.datatxt_path)) File.Copy(item.datatxt_path, item2.datatxt_path,true);

            if (File.Exists(item.disporder_path)) File.Copy(item.disporder_path, item2.disporder_path, true);
            if (File.Exists(item.copycount_path)) File.Copy(item.copycount_path, item2.copycount_path, true);

            work_dic_add(item2);

            return item2;
        }
        public void DeleteItem(WorkItem item)
        {
            if (m_read_only) return;

            work_dic_del(item);

            try {
                Directory.Delete(item.folder,true);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{2A58CCCE-056C-43DF-9DCC-E5ACB697629B}" +e.Message);
            }
        }
        #endregion

        #region ページ（フォルダ）追加・削除
        public string page_add(double disporder=double.MinValue)
        {
            var pagename= "Sample" + Guid.NewGuid().ToString().Substring(0,4);
            
            create_collection_sample_folder(pagename,disporder);
            return pagename;
        }
        public bool page_del(WorkItem pageitem)
        {
            if (pageitem==null || !pageitem.bDir) {
                G.NoticeToUser_warning("{A564EF2D-3F66-4EA9-AAFE-F349FB3F8730}" + pageitem.uuid);
                return false;
            }
            if (pageitem.bKeep)
            {
                G.NoticeToUser_warning(G.Localize("cc_notdelspec"));
                return false;
            }
            var di = new DirectoryInfo(pageitem.folder);
            if (di.GetDirectories().Length > 0)
            {
                G.NoticeToUser_warning(G.Localize("cc_notdeldata"));
                return false;
            }
            try {
                Directory.Delete(pageitem.folder,true);
                return true;
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{E4B9FB42-C84F-4363-B2EF-614FFBE30E11}" + e.Message);
            }
            return false;
        }
        #endregion
        #region 簡易表示用
        public List<WorkItem> listup_pages()
        {
            var list = new List<WorkItem>();
            var rootitems = DictionaryUtil.Get(m_work_dic,"/");
            var temppage = rootitems.Find(i=>i.folder == m_temp2_fullpath);

            list.Add(temppage);
            var collection_pages = GetCollectionRootItems();
            list.AddRange(collection_pages);

            return list;
        }
        #endregion

        #region セーブ・ロード
        // ini セーブ
        void save_ini(List<string> statelist,string savedir)
        {
            if (m_read_only) return;

            var name = make_name_from_statelist(statelist,128);
            if (name ==null) name = string.Empty;

            var comment = (name.Length>48 ?  name.Substring(0,48) : name);// + "\n" +  make_comment_from_statelist(statelist,512);
            var comment_from_statelist = make_comment_from_statelist(statelist,512);
            if (!string.IsNullOrEmpty(comment_from_statelist) && comment_from_statelist.Trim()!="?")
            {
                comment += "\n" + comment_from_statelist;
            }

            var stategofilepath = get_statego_filepath();//PathUtil.GetRelativePath(m_stategoWorkFolder_fullpath,G.psgg_file);

            var utcnow = DateTime.UtcNow;
            var ht = new Hashtable();

            ht[INFO_CREATETIME]      = utcnow.ToBinary().ToString();
            ht[INFO_CREATELOCALTIME] = utcnow.ToLocalTime().ToString();
            ht[INFO_STATEGOFILE]     = stategofilepath;
            ht[INFO_STATELIST]       = CsvUtil.ToCSV(statelist);
            ht[INFO_NAME]            = name;
            ht[INFO_COMMENT_EN]      = comment;
            ht[INFO_COMMENT_JP]      = comment;
            //ht[INFO_COPYCOUNT]       = "0";
            
            var s = IniUtil.MakeOutput(ht);

            File.WriteAllText(Path.Combine(savedir, INFOINI_FILE),s, Encoding.UTF8);
        }
        string get_statego_filepath() { return PathUtil.GetRelativePath(m_stategoWorkFolder_fullpath,G.psgg_file); }
        // DISPORDERセーブ・リード
        void save_disporder(string savedir, double neworder = double.MinValue)
        {
            if (m_read_only) return;

            var path = Path.Combine(savedir,DISPORDER_FILE);
            double ordernum = 0;
            if (neworder == double.MinValue)
            {
                ordernum = create_dispordernum();
            }
            else
            {
                ordernum = neworder;
            }
            var binarray = BitConverter.GetBytes(ordernum);
            try {
                File.WriteAllBytes(path,binarray);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{CEB5542B-339C-4510-A9BC-0C9C45B53EE4}" + e.Message + " " + path);
            }
        }
        double create_dispordernum()
        {
            return (DateTime.Now - DateTime.ParseExact("20200101", "yyyyMMdd", null)).TotalSeconds;　// トリック：初期値は時間バイナリ。
        }
        double read_disporder_ifFailCreate(string savedir, double error_ordernum = double.MinValue)
        {
            bool readfile = false;
            double ordernum = error_ordernum;
            var path = Path.Combine(savedir,DISPORDER_FILE);
            if (File.Exists(path))
            {
                try {
                    var binarray = File.ReadAllBytes(Path.Combine(savedir,DISPORDER_FILE));
                    ordernum = BitConverter.ToDouble(binarray,0);
                    readfile = true;
                }catch(SystemException e)
                {
                    G.NoticeToUser("{308660FC-9222-41D5-B607-D738B5B8C167}" + e.Message + " " + path);
                    ordernum = error_ordernum;
                }
            }
            if (!readfile)
            {            
                save_disporder(savedir, ordernum);
            }
            return ordernum;
        }
        double read_disporder(string savedir,double error = double.MinValue)
        {
            double ordernum = error;
            var path = Path.Combine(savedir,DISPORDER_FILE);
            if (File.Exists(path))
            {
                try {
                    var binarray = File.ReadAllBytes(Path.Combine(savedir,DISPORDER_FILE));
                    ordernum = BitConverter.ToDouble(binarray,0);
                }catch(SystemException e)
                {
                    G.NoticeToUser_warning("{155F9E62-2CCE-48CC-9B1D-BEC3CADB5037}" + e.Message + " " + path);
                    ordernum = error;
                }
            }
            return ordernum;
        }
        // COPYCOUNTセーブ・リード
        void save_copycount(string savedir, Int32 count)
        {
            if (m_read_only) return;
            var path = Path.Combine(savedir, COPYCOUNT_FILE);
            var binarray = BitConverter.GetBytes(count);
            try {
                File.WriteAllBytes(path,binarray);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{69E81C77-DF7C-4A9A-ADA1-AF87A8DDE49A}" + e.Message + " " + path);
            }
        }
        int read_copycount(string savedir)
        {
            int copycount = 0;
            var path = Path.Combine(savedir, COPYCOUNT_FILE);
            if (File.Exists(path))
            {
                try {
                    var binarray = File.ReadAllBytes(Path.Combine(savedir,COPYCOUNT_FILE));
                    copycount = BitConverter.ToInt32(binarray,0);
                }catch(SystemException e)
                {
                    G.NoticeToUser_warning("{155F9E62-2CCE-48CC-9B1D-BEC3CADB5037}" + e.Message + " " + path);
                    copycount = 0;
                }
            }
            return copycount;
        }
        public void increment_copycount(string savedir) //インクリメント
        {
            var cnt = read_copycount(savedir);
            cnt++;
            save_copycount(savedir,cnt);
            if (G.view_form.m_cc2!=null) G.view_form.m_cc2.force_update();
        }
        void save_data(string datadir, string data)
        {
            try {
                File.WriteAllText(Path.Combine(datadir, DATA_FILE),data,Encoding.UTF8);
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("{2CF3B6A1-F674-4E26-A80C-B524CC3EA5D4}" + e.Message);
            }
        }
        /// <summary>
        /// 更新データでファイルを変更　※ファイル用
        /// 対象：name,comment_en,comment_jp,copycount,（icon.png）
        /// 
        /// ビットマップ指定時は、icon.pngを更新(または新規書込み)する。
        /// </summary>
        public void UpdateItem(WorkItem item, bool bWithBitmap=false)
        {
            if (m_read_only) return;

            if (item==null || item.bDir) throw new SystemException("{541C8963-5973-43F1-864B-9A3405EB4530}");

            var inipath = Path.Combine(item.folder, INFOINI_FILE);
            if (!File.Exists(inipath))
            {
                G.NoticeToUser_warning("{747877A2-80A4-43C4-BB81-33877081EE71}");
                return;
            }
            var ini = File.ReadAllText(inipath, Encoding.UTF8);
            var ht = IniUtil.CreateHashtable(ini);
            if (ht==null || ht.Count == 0) {
                G.NoticeToUser_warning("{6CC9C593-990C-4D4A-8AA8-9E20F77804F1}");
                return;
            }
            bool bNeedUpdateIni = false;
            Action<string,string> _update_iniitem = (key,value)=> {
                if (ht[key] == null || ht[key].ToString() != value)
                {
                    ht[key] = value;
                    bNeedUpdateIni = true;
                }

            };
            _update_iniitem(INFO_NAME, item.name);
            _update_iniitem(INFO_COMMENT_EN, item.comment_en);
            _update_iniitem(INFO_COMMENT_JP, item.comment_jp);
            if (bNeedUpdateIni)
            {
                var ini2 = IniUtil.MakeOutput(ht);
                try {
                    File.WriteAllText(inipath,ini2,Encoding.UTF8);
                } catch (SystemException e)
                {
                    G.NoticeToUser_warning("{41A6334C-65AF-425D-B98A-469EB39BAB80}" + e.Message + " " + inipath);
                }
            }
            //DISPORDER
            var saved_disporder = read_disporder_ifFailCreate(item.folder);
            if (saved_disporder != item.disporder)
            {
                save_disporder(item.folder,item.disporder);
            }
            //COPYCOUNT
            var saved_copycount = read_copycount(item.folder);
            if (saved_copycount != item.copycount)
            {
                save_copycount(item.folder,item.copycount);
            }

            if (bWithBitmap)
            {
                if (item.bitmap==null) throw new SystemException("{8CBCAAEA-446D-470A-8BB3-8682EF3375AF}");
                    var iconpath = Path.Combine(item.folder,ICON_FILE);
                try {
                    item.bitmap.Save(iconpath, ImageFormat.Png);
                    item.iconpng_path = iconpath;
                } catch (SystemException e)
                {
                    G.NoticeToUser_warning("{85A4B394-71FA-45FA-B88E-DC4A8CBECCBB}" + e.Message + " " +iconpath);
                }
            }            
        }

        /// <summary>
        ///  フォルダ用: 更新データでファイルを更新 ※フォルダ用
        /// </summary>
        public void UpdateFolderItem(WorkItem item)
        {
            if (m_read_only) return;

            if (item==null || !item.bDir) throw new SystemException("{C8275848-9E76-4DAF-9586-5762D604B026}");

            var inipath = Path.Combine(item.folder, SUBINI_FILE);
            if (!File.Exists(inipath))
            {
                G.NoticeToUser_warning("{B0313099-3E5E-4E40-8027-121D526A028A}");
                return;
            }
            var ini = File.ReadAllText(inipath, Encoding.UTF8);
            var ht = IniUtil.CreateHashtable(ini);
            if (ht==null || ht.Count == 0) {
                G.NoticeToUser_warning("{67AD752F-A8DF-4127-96EF-83AECAA298C0}");
                return;
            }
            ht[INFO_NAME]       = item.name;
            ht[INFO_COMMENT_EN] = item.comment_en;
            ht[INFO_COMMENT_JP] = item.comment_jp;

            var ini2 = IniUtil.MakeOutput(ht);
            
            File.WriteAllText(inipath,ini2,Encoding.UTF8);
        }
        #endregion

    }
}
