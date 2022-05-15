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
    public partial class ImportControl  {

        private string EXPORTMARKM      { get { return CopyAndPasteWork.EXPORTMARKM;     } }
        public  string COPYINFOKEYSTR   { get { return CopyAndPasteWork.COPYINFOKEYSTR;  } }
        public  string BITMAPINFOKEYSTR { get { return CopyAndPasteWork.BITMAPINFOKEYSTR; } }

        private Dictionary<string, Dictionary<string,string>> get_dicdic(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                m_error = G.Localize("w_clipboardtextisnull");/*  "Clipboard text is null."*/
                return null;
            }

            // UUID確認
            if (!s.StartsWith(EXPORTMARKM)) {
                m_error = G.Localize("w_clipboardbuffernotvalid");/* "Clipboard buffer is not valid."*/
                return null;
            }
            s = ";" + s;
    
            var dicdic = StateUtil.MakeImportIni(s);
            if (dicdic==null || dicdic.Count==0) {
                m_error = G.Localize("w_faildtoimportclipboard");/* "Faild to import from clipboard." */
                return null;
            }
            return dicdic;
        }


        private static IMPORT_TYPE check_import_type(Dictionary<string,string> dic)
        {
            if (dic==null) throw new SystemException("{6E959971-9408-494F-B4D6-C92896687663}");

            var dirdata = DictionaryUtil.Get(dic,G.STATENAMESYS_dir);
            var dirpath = DirPathExcelUtil.get_dirpath_dirdata(dirdata);
            if (dirpath != G.m_target_pathdir) //直下ステート以外
            {
                return IMPORT_TYPE.DIRPOS_CHANGE;
            }
            else //直下ステート
            {
                return IMPORT_TYPE.POS_CHANGE;
            }
        }

        Dictionary<string,string> get_infodic(ref Dictionary<string,Dictionary<string,string>> dicdic)
        {
            var infodic = DictionaryUtil.Get( dicdic ,COPYINFOKEYSTR);
            dicdic.Remove(COPYINFOKEYSTR); //dicdicから削除
            return infodic;
        }
        #region bitmap
        Dictionary<string,string> get_bmpdic(ref Dictionary<string,Dictionary<string,string>> dicdic)
        {
            var bmpdic = DictionaryUtil.Get( dicdic, BITMAPINFOKEYSTR);
            dicdic.Remove(BITMAPINFOKEYSTR);
            return bmpdic;
        }
        void write_bimtap_filedb(Dictionary<string, string> bmpdic)
        {
            if (!G.psgg_file_w_data)
            {
                G.NoticeToUser_warning("Not suported to copy bitmap");
                return;
            }
            if (bmpdic == null)
            {
                G.NoticeToUser_warning("{D64AC479-03AA-4006-A3F0-CCBDE7722E27}");
                return;
            }
            var numstr = DictionaryUtil.Get(bmpdic,"num_of_bitmap");
            if (numstr == null) return;
            var num = ParseUtil.ParseInt(numstr);
            if (num<=0) return;
            var hashlist = new List<string>();
            for(var n = 0; n<num; n++)
            {
                var hash = DictionaryUtil.Get(bmpdic, "hash_" + n.ToString());
                if (string.IsNullOrEmpty(hash)) continue;
                var data = DictionaryUtil.Get(bmpdic, "data_" + n.ToString());
                if (string.IsNullOrEmpty(data)) continue;
                FileDbUtil.RegisterFieDbBitmap(hash,data);
                hashlist.Add(hash);
            }

            FileDbUtil.write_filedb_specified_bmp_files(hashlist);
        }
        #endregion
        void update_info_if_same_procid(ref Dictionary<string,string> dic)
        {
            var dicpid = DictionaryUtil.Get(dic,"pid");
            var apppid = System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            if (dicpid!=apppid)
            {
                //更新の必要なし
                return;
            }
            var new_pos_list_encode = CopyAndPasteWork.get_nodegroup_pos_list_encode();
            var new_cmt_list_encode = CopyAndPasteWork.get_nodegroup_comment_list_encode();
            DictionaryUtil.Set(dic, "nodegroup_pos_list",new_pos_list_encode);
            DictionaryUtil.Set(dic, "nodegroup_comment_list",new_cmt_list_encode);
        }
        object _getobj(Dictionary<string,string> dic, string  a, Type type) {
            try {
                var v = DictionaryUtil.Get(dic,a);
                return JsonUtil.Deserialize(v,type);
            } catch {
                var msg = string.Format( "{0} at clipboard cannot be read.", a);  
                G.NoticeToUser_warning(msg);
                return null;
            }
        }
        Dictionary<string,string> get_nodegroup_comment_list(Dictionary<string,string> dic)
        {
            return (Dictionary<string,string>)_getobj(dic,"nodegroup_comment_list", typeof(Dictionary<string,string>));
        }
        Dictionary<string, Point> get_nodegroup_pos_list(Dictionary<string,string> dic)
        {
            return (Dictionary<string,Point>)_getobj(dic, "nodegroup_pos_list", typeof(Dictionary<string,Point>));
        }
        void adjust_dirpath(ref Dictionary<string, Dictionary<string, string>> dicdic, string src_dir)
        {
            foreach(var k in dicdic.Keys)
            {
                var dic = dicdic[k];
                var src_state_dirdata     = DictionaryUtil.Get(dic,G.STATENAMESYS_dir);
                var src_state_dir         = DirPathExcelUtil.get_dirpath_dirdata(src_state_dirdata); 
                var target_dir            = G.m_target_pathdir;
                var src_state_newdir      = _create_new_dir(src_dir, src_state_dir, target_dir);
                var new_src_state_dirdata = DirPathExcelUtil.makedata_dirpath(src_state_dirdata,src_state_newdir);
                DictionaryUtil.Set(dic,G.STATENAMESYS_dir,new_src_state_dirdata);
            }
        }
        private string _create_new_dir(string src_dir,  string src_state_dir, string target_dir)
        {
            var addsrc = src_state_dir.StartsWith(src_dir) ?  src_state_dir.Substring(src_dir.Length) : "";
            var newdir = GroupNodeUtil.path_normalize(target_dir + addsrc);
            return newdir;
        }

        Point? get_groupnode_pos(string dir)
        {
            if (m_src_nodegroup_pos_list.ContainsKey(dir))
            {
                return m_src_nodegroup_pos_list[dir];
            }
            return null;
        }
        string get_groupnode_comment(string dir)
        {
            if (m_src_nodegroup_comment_list.ContainsKey(dir))
            {
                return m_src_nodegroup_comment_list[dir];
            }
            return null;
        }
        #region get and set ...from_dic
        private static string get_dirpath_from_dic(Dictionary<string,string> dic)
        {
            var dirdata = DictionaryUtil.Get(dic, G.STATENAMESYS_dir);
            var dirpath = DirPathExcelUtil.get_dirpath_dirdata(dirdata);
            return dirpath;
        }
        private static Point? get_pos_from_dic(Dictionary<string,string> dic)
        {
            var posstr = DictionaryUtil.Get( dic, G.STATENAMESYS_pos);
            var posx = PointUtil.Parse(posstr);
            return posx;
        }
        private static void set_dirpath_to_dic(Dictionary<string,string> dic, string dirpath)
        {
            var dirdata = DictionaryUtil.Get(dic, G.STATENAMESYS_dir);
            var dirdata2 = DirPathExcelUtil.makedata_dirpath(dirdata,dirpath);
            DictionaryUtil.Set(dic, G.STATENAMESYS_dir, dirdata2);          
        }
        private static void set_pos_to_dic(Dictionary<string,string> dic, Point pos)
        {
            var dirdata = DictionaryUtil.Get(dic, G.STATENAMESYS_dir);
            var dirdata2 = DirPathExcelUtil.makedata_dirpos(dirdata,pos);
            DictionaryUtil.Set(dic, G.STATENAMESYS_dir, dirdata2);
        }
        #endregion
        /*
            get_pos_related_state_just_under_path関数は、
                ステートがカレントパスの直下であれば、そのポジションを返す
                そうでなければ、カレントバス直下の（ステートの親（先代））グループのポジションを返す。
                
                本関数は特殊な使い方をする。
                ポジション算出の根拠となったのが、ステート自身の位置か、それともグループノードであるかを
                実行後に判断できるようにする。
                出力はそのための変数
                bStateOrGroup　   --- 算出根拠が ステート時は true グループ時は false  nullはエラー
                groupnode_dirpath --- 算出根拠が グループ時の時のパス
        */
        Point? get_pos_related_state_just_under_path(string cur_pathdir,  Dictionary<string, string> dic, Dictionary<string, Point> groupnode_pos_list)
        {
            bool?   bStateOrGroup;
            string  groupnode_dirpath;
            return get_pos_related_state_just_under_path(cur_pathdir, dic, groupnode_pos_list, out bStateOrGroup, out groupnode_dirpath);
        }
        Point? get_pos_related_state_just_under_path(string cur_pathdir,  Dictionary<string, string> dic, Dictionary<string, Point> groupnode_pos_list, out bool? bStateOrGroup, out string groupnode_dirpath  )
        {
            bStateOrGroup = null;
            groupnode_dirpath = null;

            var dirpath  = get_dirpath_from_dic(dic);
            var dirpos   = DictionaryUtil.Get(groupnode_pos_list,dirpath);
            var statepos = get_pos_from_dic(dic);
                
            if (!dirpath.Contains(cur_pathdir))
            {
                m_error = "{F2FE64D8-B353-406C-802D-8DB5AB8A17C1}";
                return null;                
            } 
            if (dirpath == cur_pathdir)
            {
                bStateOrGroup = true;
                return statepos;
            }
            else
            {
                var groupunderpath = GroupNodeUtil.get_groupnode_just_under_path(dirpath, G.m_target_pathdir);
                if (string.IsNullOrEmpty(groupunderpath))
                {
                    m_error = "{DCFAF00F-8B35-406C-82CC-A8AC27A5B723}";
                    return null;
                }
                var targetpath = GroupNodeUtil.pathcombine(G.m_target_pathdir,groupunderpath);

                bStateOrGroup = false;
                groupnode_dirpath = targetpath;

                return DictionaryUtil.Get(groupnode_pos_list,targetpath);
            }
        }
        private void get_maxmin_p(string cur_pathdir, Dictionary<string, Point> groupnode_pos_list, Dictionary<string, Dictionary<string,string>> dicdic,out Point min, out Point max, out Point size)
        {
            min = new Point(int.MaxValue,int.MaxValue);
            max = new Point(int.MinValue,int.MinValue);
            foreach(var k in dicdic.Keys)
            {
                var dic      = dicdic[k];
                Point? _pos = get_pos_related_state_just_under_path(cur_pathdir, dic, groupnode_pos_list);
                if (_pos == null) continue;

                var pos = (Point)_pos;

                min.X = Math.Min(min.X, pos.X);
                min.Y = Math.Min(min.Y, pos.Y);

                max.X = Math.Max(max.X, pos.X);
                max.Y = Math.Max(max.Y, pos.Y);
            }
            size = PointUtil.Abs( PointUtil.Sub_Point(max,min ));
            size = PointUtil.Add_XY(size, (int)G.state_width , 200/*(int)G.state_height*/);
        }

        private void change_all_pos(ref Dictionary< string, Dictionary<string,string> > dicdic, ref Dictionary< string, Point > groupnode_pos_list, string cur_pathdir, Point min, Point max, Point size, Point click_pos)
        {
            Func<Point,Point> update_point = (p)=>
            {
                var dst_center = click_pos;
                var dst_min    = PointUtil.Add_XY(dst_center, -size.X * 0.5f, -size.Y * 0.5f);
                var diff = PointUtil.Sub_Point(dst_min, min);
                var newpoint = PointUtil.Add_Point(p,diff);
                return Point.Truncate(newpoint);

                //var diff = PointUtil.Sub_Point(p, min);
                //var p2 = PointUtil.Add_Point(click_pos,diff);
                //var p3 = PointUtil.Add_XY(p2, -0.5f * size.X, -0.5f * size.Y );
                //return Point.Truncate( p3 );
            };

            var new_groupnode_list = new Dictionary<string,Point>(); //多重変更防止上、新規辞書に格納。（最後に反映）
            foreach(var k in dicdic.Keys)
            {
                var dic = dicdic[k];
                bool?  bStateOrGroup;
                string groupnode_dirpath;

                var posx = get_pos_related_state_just_under_path(cur_pathdir,dic,groupnode_pos_list,out bStateOrGroup, out groupnode_dirpath);
                if (posx==null) continue;
                if (bStateOrGroup==null) continue;

                var pos = (Point)posx;
                var newpos = update_point(pos);

                if (bStateOrGroup == true)
                {
                    var newposstr = PointUtil.ToStringF0_CSV(newpos);
                    DictionaryUtil.Set(dic,G.STATENAMESYS_pos, newposstr);
                    continue;
                }
                if (bStateOrGroup == false)
                {
                    DictionaryUtil.Set(new_groupnode_list,groupnode_dirpath,newpos);
                    continue;
                }
            }
            foreach(var k in new_groupnode_list.Keys)
            {
                var v = new_groupnode_list[k];
                DictionaryUtil.Set(groupnode_pos_list,k,v);
            }
        }

        void rename_if_same_state_exists(ref Dictionary<string, Dictionary<string,string> > dicdic)
        {
            var statelist= G.excel_program.GetStateList();

            for (var loop = 0; loop<=2000; loop++)
            {
                if (loop >=2000) {
                    m_error = "Unexpected! {1D2DBDBB-0F42-4AD6-AAAA-7E7B02508870}";
                    return;
                }
                var needloop = false;

                var dicstatelist = new List<string>(dicdic.Keys);
                foreach (var state in dicdic.Keys)
                {
                    if (statelist.Contains(state)) //送り先に同名ファイル発見
                    {
                        var to_state = state;
                        for(var loop2=0;loop2<=1000;loop2++)
                        {
                            if (loop2>=1000) {
                                m_error = "Unexpected! {0D81325A-B597-4712-B0F0-42A0CE30EE2C}";
                                return;
                            }
                            to_state = StateUtil.MakeIncName(to_state);
                            if (statelist.Contains(to_state) || dicstatelist.Contains(to_state)) //中にある。
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        dicdic = StateUtil.Rename(dicdic, state, to_state);
                        needloop = true;
                        break;
                    }
                }
                if (!needloop) break;
            }
        }
        #region rename関連
        List<string> collect_just_under_dirpath_from_dic(string cur_dirpath, Dictionary<string, Dictionary<string,string>> dicdic, Dictionary<string, Point> nodegroup_pos_list)
        {
            var src_group_dirpath_list = new List<string>();
            foreach(var state in dicdic.Keys)
            {
                var dic = dicdic[state];
                bool? bStateOrGroup;
                string groupnode_dirpath;

                var posx = get_pos_related_state_just_under_path(cur_dirpath,dic,nodegroup_pos_list,out bStateOrGroup, out groupnode_dirpath);
                if (posx==null) continue;
                if (bStateOrGroup==null) continue;
                if (bStateOrGroup == true)
                {
                    continue; //無視
                }
                var dirpath = groupnode_dirpath;
                if (!src_group_dirpath_list.Contains(dirpath))
                { 
                    src_group_dirpath_list.Add(dirpath);
                }
            }
            return src_group_dirpath_list;
        }
        void rename_if_same_group_exists(string cur_dirpath,  ref Dictionary< string, Dictionary<string,string>> dicdic, ref Dictionary<string, Point> nodegroup_pos_list, ref Dictionary<string, string> nodegroup_comment_list )
        {
            //1. 対象パス収集
            var src_group_dirpath_list = collect_just_under_dirpath_from_dic(cur_dirpath, dicdic,m_src_nodegroup_pos_list);

            //同名修正
            var cur_nodegroup_pos_list = G.nodegroup_pos_list_get();
            foreach(var try_dirpath in src_group_dirpath_list)
            {
                if (cur_nodegroup_pos_list.ContainsKey(try_dirpath) ) //同名あり
                {
                    var new_dirpath = make_anothergroupname(try_dirpath, cur_nodegroup_pos_list,  nodegroup_pos_list);
                    update_dirpath(try_dirpath, new_dirpath, ref dicdic, ref nodegroup_pos_list, ref nodegroup_comment_list);
                }
            }
        }
        string make_anothergroupname(string try_dirpath, Dictionary<string, Point> cur_nodegroup_pos_list, Dictionary<string, Point> nodegroup_pos_list)
        {
            var parentname = GroupNodeUtil.get_parent_path(try_dirpath);
            var lastname   = GroupNodeUtil.get_last_path(try_dirpath);
            var newname    = lastname.Trim('/');
            for(var loop = 0; loop <= 1000; loop++ )
            {
                newname = StateUtil.MakeIncName( newname );
                var tdirpath = GroupNodeUtil.pathcombine(parentname,newname);
                if (cur_nodegroup_pos_list.ContainsKey(tdirpath)) continue;
                if (nodegroup_pos_list.ContainsKey(tdirpath)) continue;
                
                return tdirpath;
            }

            m_error = "{9B94B143-72E9-4957-8ADD-E0D30A0662AD}";

            return null;
        }
        void update_dirpath(string org_dirpath, string new_dirpath, ref Dictionary<string, Dictionary<string, string>> dicdic, ref Dictionary<string, Point> nodegroup_pos_list, ref Dictionary<string, string> nodegroup_comment_list)
        {
            foreach(var k in dicdic.Keys)
            {
                var dic = dicdic[k];
                var dirpath = get_dirpath_from_dic(dic);
                if (dirpath.StartsWith(org_dirpath))
                {
                    var dirpath2 = new_dirpath  +  dirpath.Substring(org_dirpath.Length);
                    set_dirpath_to_dic(dic,dirpath2);
                }
            }
            for(var loop =0; loop<=1000; loop++)
            { 
                if (loop == 1000) m_error = "{267392EB-D1BB-4491-9267-4F49498DA942}";
                var bNeedLoop = false;
                foreach(var dirpath in nodegroup_pos_list.Keys)
                {
                    if (dirpath.StartsWith(org_dirpath))
                    {
                        var dirpath2 = new_dirpath + dirpath.Substring(org_dirpath.Length);
                        var val = nodegroup_pos_list[dirpath];
                        nodegroup_pos_list.Remove(dirpath);
                        nodegroup_pos_list.Add(dirpath2, val);
                        bNeedLoop = true;
                        break;              
                    }
                }
                if (bNeedLoop) continue;
                break;
            }
            for(var loop =0; loop<=1000; loop++)
            { 
                if (loop == 1000) m_error = "{CA1A0D71-A454-40D0-8931-9DC0EDE1848D}";
                var bNeedLoop = false;
                foreach(var dirpath in nodegroup_comment_list.Keys)
                {
                    if (dirpath.StartsWith(org_dirpath))
                    {
                        var dirpath2 = new_dirpath + dirpath.Substring(org_dirpath.Length);
                        var val = nodegroup_comment_list[dirpath];
                        nodegroup_comment_list.Remove(dirpath);
                        nodegroup_comment_list.Add(dirpath2, val);
                        bNeedLoop = true;
                        break;              
                    }
                }
                if (bNeedLoop) continue;
                break;
            }

            //if (nodegroup_pos_list.ContainsKey(org_dirpath))
            //{
            //    var val= nodegroup_pos_list[org_dirpath];
            //    nodegroup_pos_list.Remove(org_dirpath);
            //    nodegroup_pos_list.Add(new_dirpath,val);
            //}
            //if (nodegroup_comment_list.ContainsKey(org_dirpath))
            //{
            //    var val = nodegroup_comment_list[org_dirpath];
            //    nodegroup_comment_list.Remove(org_dirpath);
            //    nodegroup_comment_list.Add(new_dirpath,val);
            //}
        }
        #endregion

        #region copy states
        /// <summary>
        /// 入力dicdic(ステートと値のセット)をG.exece_programのエクセルキャッシュにインポートする。
        /// </summary>
        void copy_from_states_in_dicdic(Dictionary<string, Dictionary<string,string>> dicdic, Dictionary<string, Point> nodegroup_pos_list, Dictionary<string,string> nodegroup_comment_list)
        {
            var cur_nodegroup_pos_list = G.nodegroup_pos_list_get();
            var cur_nodegroup_comment_lsit = G.nodegroup_comment_list_get();

           // ステート作成

            //dicdic内の全てのdirpath用
            var dicdic_dirpath_list = new List<string>();
            Action<string> collectpath = (p) => {
                var p2 = p;
                while(!string.IsNullOrEmpty(p2) && p2!="/")
                {
                    if (!dicdic_dirpath_list.Contains(p2))
                    { 
                        dicdic_dirpath_list.Add(p2);
                    }
                    p2 = GroupNodeUtil.get_parent_path(p2);
                }
            };

            foreach(var key in dicdic.Keys)
            {
                var state = key;
                var dic = dicdic[key];
                Point pos = Point.Empty;
            
                G.excel_program.NewState_import(dic);

                var posstr = DictionaryUtil.Get(dic, G.STATENAMESYS_pos);
                var posx = PointUtil.Parse(posstr);
                if (posx != null)
                {
                    pos = (Point)posx;
                }
                else
                {
                    pos = Point.Truncate(　G.vf_sc.GetPointerOnMainBmp());
                }

                G.UpdateExcelPos(state, (Point)pos, true);

                var dirpath    = DirPathExcelUtil.get_dirpath(state);
                var dircomment =  DirPathExcelUtil.get_dircomment(state); 
                var dirpos     = DictionaryUtil.Get(nodegroup_pos_list,dirpath);
                DirPathExcelUtil.set_diritems(state,dirpath,dirpos,dircomment);

                G.nodegroup_comment_set(dirpath,dircomment);
                G.nodegroup_pos_set(dirpath,dirpos);

                collectpath(dirpath); //収集
            }

            //漏れがないように再度 ※ノードを持たないグループ情報が抜ける場合があるため
            foreach(var p in dicdic_dirpath_list)
            {
                var pos = DictionaryUtil.Get(nodegroup_pos_list,p);
                DictionaryUtil.Set(cur_nodegroup_pos_list,p,pos);
                var cmt = DictionaryUtil.Get(nodegroup_comment_list,p);
                DictionaryUtil.Set(cur_nodegroup_comment_lsit,p,cmt);
            }

            if (G.psgg_file_w_data) //新PSGG時は、thumbnailも更新
            {
                foreach(var key in dicdic.Keys)
                {
                    var dic = dicdic[key];
                    if (dic==null) continue;
                    var hash = DictionaryUtil.Get(dic,G.STATENAME_thumbnail);
                    if (string.IsNullOrEmpty(hash)) continue;
                    var bmp = FileDbUtil.get_bmp_by_hash(hash);
                    if (bmp==null) continue;

                    var col = G.excel_program.GetCol(key);
                    var row = G.excel_program.GetRow(G.STATENAME_thumbnail);

                    G.excel_pictures.SetItem(row,col,bmp);
                }
            }
        }
        /// <summary>
        /// 　コピーの際に、元となるCopyCollection WorkItemのカウンタをアップする。
        /// </summary>
        void increment_cc_counter()
        {
            if (G.cc.copydata_path!=null && Directory.Exists(G.cc.copydata_path))
            {
                G.cc.increment_copycount(G.cc.copydata_path);
            }
        }
        #endregion

        #region setup focus
        void setup_focus_list(string cur_dirpath, Dictionary<string, Dictionary<string,string>> dicdic, Dictionary<string, Point> nodegroup_pos_list)
        {
            var focus_list = new List<string>();
            foreach(var state in dicdic.Keys)
            {
                var dic = dicdic[state];
                bool? bStateOrGroup;
                string groupnode_dirpath;

                var posx = get_pos_related_state_just_under_path(cur_dirpath,dic,nodegroup_pos_list,out bStateOrGroup, out groupnode_dirpath);
                if (posx==null) continue;
                if (bStateOrGroup==null) continue;
                
                if (bStateOrGroup==true)
                {
                    focus_list.Add(state);
                }
                else
                {
                    var dirpath = DirPathExcelUtil.get_dirpath(state);
#if x //複数のグループノード時にハングした
                    var last = GroupNodeUtil.get_last_path(dirpath);

                    if (last.Length > 1 && last[0] == '/'  ) last = last.Substring(1);
                    if (last.Length > 1 && last[last.Length-1] == '/') last = last.Substring(0,last.Length-1);

                    var altname = AltState.MakeAltStateName(last);
                    if (!focus_list.Contains(altname))
                    {
                        focus_list.Add(altname);
                    }
#endif
                    if (dirpath.Contains(G.m_target_pathdir)) //直下確認
                    {
                        var diff = dirpath.Substring(G.m_target_pathdir.Length); // group1/group2/.../...
                        if (!string.IsNullOrEmpty(diff))
                        {
                            var group = RegexUtil.Get1stMatch(@"^[^\/]+?\/",diff);
                            if (!string.IsNullOrEmpty(group))
                            {
                                group = group.Trim('/');    //直下のグループのみを対象とする。
                                var altname = AltState.MakeAltStateName(group);
                                if (!focus_list.Contains(altname))
                                {
                                    focus_list.Add(altname);
                                }
                            }
                        }
                    }
                }
            }
            G.vf_sc.m_group_focus_list = focus_list;
            //if (G.vf_sc.m_group_focus_list.Count == 1)
            //{
            //    G.vf_sc.m_groupnode_name = AltState.TrimAltStateName(G.vf_sc.m_group_focus_list[0]);
            //}
        }
        #endregion

        #region 流出先除外
        void exclude_outflow(ref Dictionary< string, Dictionary<string,string>> dicdic)
        {
            var allstates = new List<string>(dicdic.Keys);
            
            Func<string,bool> check_state = (s)=> { return allstates.Contains(s);  };

            foreach(var s in allstates)
            {
                var dic = DictionaryUtil.Get( dicdic, s);
                if (dic == null) continue;
                //nextstate
                var nextstate = DictionaryUtil.Get(dic,G.STATENAME_nextstate);
                if (!string.IsNullOrEmpty(nextstate))
                {
                    if (StateUtil.IsValidStateName(nextstate) && !AltState.IsAltState(nextstate))
                    {
                        if (!check_state(nextstate))
                        {
                            dic[G.STATENAME_nextstate] = "";
                            dicdic[s] = dic;
                        }
                    }
                }
                //gosub
                var gosub = DictionaryUtil.Get(dic,G.STATENAME_gosubstate);
                if (StateUtil.IsValidStateName(gosub))
                {
                    if (!check_state(gosub))
                    {
                        dic[G.STATENAME_gosubstate] = "?";
                        dicdic[s] = dic;
                    }
                }
                //branch
                var branchvalue = DictionaryUtil.Get(dic,G.STATENAME_branch);
                var brcondvalue = DictionaryUtil.Get(dic,G.STATENAME_brcond);
                var brcmtvalue  = DictionaryUtil.Get(dic,G.STATENAME_branchcmt);
                var apilist = BranchUtil.GetApiAndLabelListFromString(branchvalue,brcondvalue,brcmtvalue);
                if (apilist!=null)
                {
                    for(var i = 0; i < apilist.Count; i++)
                    {
                        var p = apilist[i];
                        if (StateUtil.IsValidStateName(p.label))
                        {
                            if (!check_state(p.label))
                            {
                                p.label = "?";
                            }
                        }
                    }
                    BranchUtil.MakeBranchStringFromApiAndLabelList(apilist, out branchvalue, out brcondvalue, out brcmtvalue);
                    dic[G.STATENAME_branch] = branchvalue;
                    dicdic[s] = dic;
                }

            }

        }
        #endregion

        #region はみだし調整
        void adjust_location(Point p, Point size, ref Point clickpos)
        {
            /*
                 click posが矩形の中心
            */
            var p_rect   = new Rectangle(p.X,p.Y,size.X,size.Y);
            var t_rect   = RectangleUtil.MoveCenter(p_rect,clickpos);

            //左
            if (t_rect.Left < 0)
            {
                clickpos.X += (int)(-t_rect.Left);
            }
            //左
            if (t_rect.Right >= G.bitmap_width)
            {
                var diff = t_rect.Right - G.bitmap_width;
                clickpos.X -= (int)diff;
            }
            //上
            if (t_rect.Top < 0)
            {
                clickpos.Y += (int)(-t_rect.Top);
            }
            //下
            if (t_rect.Bottom >= G.bitmap_height)
            {
                var diff = t_rect.Bottom - G.bitmap_height;
                clickpos.Y -= (int)diff;
            }
        }

        #endregion

        #region ステート一覧 外部参照用
        public List<string> GetPasteStates()
        {
            return G.vf_sc.m_group_focus_list;  //ここに格納済み            
        }
        #endregion

        #region manager
        Action<bool> m_curfunc;
        Action<bool> m_nextfunc;

        bool         m_noWait;
    
        public void Update()
        {
            while(true)
            {
                var bFirst = false;
                if (m_nextfunc!=null)
                {
                    m_curfunc = m_nextfunc;
                    m_nextfunc = null;
                    bFirst = true;
                }
                m_noWait = false;
                if (m_curfunc!=null)
                {   
                    m_curfunc(bFirst);
                }
                if (!m_noWait) break;
            }
        }
        void Goto(Action<bool> func)
        {
            m_nextfunc = func;
        }
        bool CheckState(Action<bool> func)
        {
            return m_curfunc == func;
        }
        bool HasNextState()
        {
            return m_nextfunc != null;
        }
        void NoWait()
        {
            m_noWait = true;
        }
#endregion
#region gosub
        List<Action<bool>> m_callstack = new List<Action<bool>>();
        void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
        {
            m_callstack.Insert(0,returnstate);
            Goto(nextstate);
        }
        void ReturnState()
        {
            var nextstate = m_callstack[0];
            m_callstack.RemoveAt(0);
            Goto(nextstate);
        }
#endregion

        public void Start()
        {
            Goto(S_START);
        }
        public bool IsEnd()     
        { 
            return CheckState(S_END); 
        }
    
        public void Run()
        {
		    int LOOPMAX = (int)(1E+5);
		    Start();
		    for(var loop = 0; loop <= LOOPMAX; loop++)
		    {
			    if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			    Update();
			    if (IsEnd()) break;
		    }
	    }

#region    // [PSGG OUTPUT START] indent(8) $/./$
//  psggConverterLib.dll converted from ImportControl.xlsx.    psgg-file:doc\ImportControl.psgg
        /*
            E_DICDIC
            dicdic,infodic,bmpdocの変数
        */
        Dictionary<string, Dictionary<string,string>> m_dicdic;
        Dictionary<string, string> m_infodic;
        Dictionary<string, string> m_bmpdic;
        /*
            E_ENUM
        */
        enum IMPORT_TYPE
        {
            NONE,
            POS_CHANGE,
            DIRPOS_CHANGE,
        }
        /*
            E_ERROR
        */
        public string m_error;
        /*
            E_INFO
        */
        string m_src_dir;
        Dictionary<string,Point> m_src_nodegroup_pos_list;
        Dictionary<string,string> m_src_nodegroup_comment_list;
        /*
            E_INPUT
        */
        public Point m_click_pos;
        public bool  m_wo_outflow;//出力先除外
        /*
            S_0001
            histroy save
        */
        void S_0001(bool bFirst)
        {
            //
            if (bFirst)
            {
                History.SaveForce("Pasting was done.");
            }
            //
            if (!HasNextState())
            {
                Goto(S_0017);
            }
        }
        /*
            S_0002
            BitmapをfileDBキャッシュへ
            最終コピー前に
        */
        void S_0002(bool bFirst)
        {
            //
            if (bFirst)
            {
                write_bimtap_filedb(m_bmpdic);
            }
            //
            if (!HasNextState())
            {
                Goto(S_0012);
            }
        }
        /*
            S_0003
            クリップボードテキストから infodicとdicdirを取得する。
        */
        string m_cs; //クリップボードストリング
        void S_0003(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_cs = Clipboard.GetText();
                m_dicdic = get_dicdic(m_cs);
            }
            // branch
            if (m_error==null) { Goto( S_0005 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0004
            初期化
        */
        void S_0004(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_error = null;
            }
            //
            if (!HasNextState())
            {
                Goto(S_0003);
            }
        }
        /*
            S_0005
            infodic取得
            bmpdic取得
        */
        void S_0005(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_infodic = get_infodic(ref m_dicdic);
                m_bmpdic = get_bmpdic(ref m_dicdic);
                m_src_dir = DictionaryUtil.Get(m_infodic,"curdir").Trim();
            }
            // branch
            if (m_error==null) { Goto( S_0006 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0006
            nodegroup情報取得
        */
        void S_0006(bool bFirst)
        {
            //
            if (bFirst)
            {
                m_src_nodegroup_comment_list=get_nodegroup_comment_list(m_infodic);
                m_src_nodegroup_pos_list=get_nodegroup_pos_list(m_infodic);
            }
            // branch
            if (m_error==null) { Goto( S_0007 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0007
            コピー元のdir とコピー先の dirを調整する
        */
        void S_0007(bool bFirst)
        {
            //
            if (bFirst)
            {
                adjust_dirpath(ref m_dicdic, m_src_dir);
            }
            // branch
            if (m_error == null) { Goto( S_0008 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0008
            コピー位置算出のため、直下のステートとグループの位置を囲む最少矩形を求める。
        */
        Point m_pmin;
        Point m_pmax;
        Point m_psize;
        void S_0008(bool bFirst)
        {
            //
            if (bFirst)
            {
                get_maxmin_p(G.m_target_pathdir, m_src_nodegroup_pos_list, m_dicdic,out m_pmin,out m_pmax, out m_psize);
            }
            // branch
            if (m_error == null) { Goto( S_0015 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0009
            直下のステートとグループのそれぞれの位置をクリックポジションから算出して移動
        */
        void S_0009(bool bFirst)
        {
            //
            if (bFirst)
            {
                change_all_pos(ref m_dicdic, ref m_src_nodegroup_pos_list, G.m_target_pathdir, m_pmin, m_pmax, m_psize, m_click_pos);
            }
            // branch
            if (m_error==null) { Goto( S_0010 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0010
            dicdic内の同名のステート時にリネーム
        */
        void S_0010(bool bFirst)
        {
            //
            if (bFirst)
            {
                rename_if_same_state_exists(ref m_dicdic);
            }
            // branch
            if (m_error==null) { Goto( S_0011 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0011
            dicdic内の直下の同名グループのリネーム
        */
        void S_0011(bool bFirst)
        {
            //
            if (bFirst)
            {
                rename_if_same_group_exists(G.m_target_pathdir, ref m_dicdic, ref m_src_nodegroup_pos_list, ref m_src_nodegroup_comment_list);
            }
            // branch
            if (m_error==null) { Goto( S_0014 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0012
            dicdicのステートをコピー
            groupnode_..._listの関連を反映
        */
        void S_0012(bool bFirst)
        {
            //
            if (bFirst)
            {
                copy_from_states_in_dicdic(m_dicdic, m_src_nodegroup_pos_list, m_src_nodegroup_comment_list);
                increment_cc_counter();
            }
            // branch
            if (m_error==null) { Goto( S_0013 ); }
            else { Goto( S_END ); }
        }
        /*
            S_0013
            フォーカスリストの作成
        */
        void S_0013(bool bFirst)
        {
            //
            if (bFirst)
            {
                setup_focus_list(G.m_target_pathdir, m_dicdic,m_src_nodegroup_pos_list);
            }
            //
            if (!HasNextState())
            {
                Goto(S_0016);
            }
        }
        /*
            S_0014
            流出先除外指定時に、流出先を除外させる。
        */
        void S_0014(bool bFirst)
        {
            //
            if (bFirst)
            {
                if (m_wo_outflow)
                {
                    exclude_outflow(ref m_dicdic);
                }
            }
            //
            if (!HasNextState())
            {
                Goto(S_0002);
            }
        }
        /*
            S_0015
            はみ出す場合のコピー位置調整。
        */
        void S_0015(bool bFirst)
        {
            //
            if (bFirst)
            {
                adjust_location(m_pmin,m_psize,ref m_click_pos);
            }
            //
            if (!HasNextState())
            {
                Goto(S_0009);
            }
        }
        /*
            S_0016
            カーソルポインタを移動
        */
        void S_0016(bool bFirst)
        {
            //
            if (bFirst)
            {
                Cursor.Position = Point.Round(G.vf_sc.GetScreenPosFormPointOnImage(m_click_pos));
            }
            //
            if (!HasNextState())
            {
                Goto(S_0001);
            }
        }
        /*
            S_0017
            focus track
        */
        void S_0017(bool bFirst)
        {
            //
            if (bFirst)
            {
                FocusTrack.Record(G.vf_sc.m_group_focus_list);
            }
            //
            if (!HasNextState())
            {
                Goto(S_END);
            }
        }
        /*
            S_END
        */
        void S_END(bool bFirst)
        {
        }
        /*
            S_START
        */
        void S_START(bool bFirst)
        {
            //
            if (!HasNextState())
            {
                Goto(S_0004);
            }
        }


#endregion // [PSGG OUTPUT END]

	    // write your code below

	   // bool m_bYesNo;
	
	   // void br_YES(Action<bool> st)
	   // {
		  //  if (!HasNextState())
		  //  {
			 //   if (m_bYesNo)
			 //   {
				//    Goto(st);
			 //   }
		  //  }
	   // }

	   // void br_NO(Action<bool> st)
	   // {
		  //  if (!HasNextState())
		  //  {
			 //   if (!m_bYesNo)
			 //   {
				//    Goto(st);
			 //   }
		  //  }
	   // }
    }
}
/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/
