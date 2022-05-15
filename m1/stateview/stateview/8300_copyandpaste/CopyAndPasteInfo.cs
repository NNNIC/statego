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


    問題

    ‐グループの情報がない
    解決策
    copyinfoの中に、全部のnodegroup情報を入れる。データはconfigと同じ
    そこから場所を引く

    [____copyinfo____] 

    nodegroup_comment_list=@@@
    [{"Key":"\/","Value":"This is the root"},{"Key":"\/BlankWork\/","Value":"ブランク空間作業"},{"Key":"\/SingleStateWork\/","Value":"ステート単体作業"},{"Key":"\/GroupFocus\/","Value":"グループフォーカス作業"},{"Key":"\/GroupNodeWork\/","Value":"グループノード作業"}]
    @@@
    nodegroup_pos_list=@@@
    [{"Key":"\/","Value":{"x":0,"y":0}},{"Key":"\/BlankWork\/","Value":{"x":1552,"y":514}},{"Key":"\/SingleStateWork\/","Value":{"x":1544,"y":164}},{"Key":"\/GroupFocus\/","Value":{"x":1561,"y":909}},{"Key":"\/GroupNodeWork\/","Value":{"x":1544,"y":316}}]
    @@@

    /hoge/hogeで、/からの直下時は　/hogeの情報を収集する。


   bitmap_num=2  .... ビットマップ数
   [bitmap0]
   hash=
   data=@@@
   @@@

   [bitmap1]
   hash=
   data=@@@
   @@@

*/


namespace stateview
{
    public class CopyAndPasteWork
    {
        public static string EXPORTMARKM    { get { return "{3745FCA0-D34D-4270-8C39-BB563F73BE4B}"; } }
        public static string COPYINFOKEYSTR { get { return "____copyinfo____"; } }
        public static string BITMAPINFOKEYSTR { get { return "____bitmapinfo____"; } }

        #region Export
        public static void export(List<string> statelist, List<string> displist=null)
        {
            var s = string.Empty;
            s  =  _make_copy_info() + Environment.NewLine + StateUtil.MakeExportListIni(statelist) + Environment.NewLine;
            s +=  _make_bitmap_data(statelist);
            Clipboard.SetText(s);

            var cclist = displist!=null ? displist : statelist;
            G.cc.copydata_path = G.cc.MakeCopyData(cclist,s,true);
            if (G.view_form.m_cc2!=null) {
                G.view_form.m_cc2.loaddata();
            }
            G.NoticeToUser("The serialized date was exported to clipboard.");
        }
        private static string _make_copy_info()
        {
            var s = string.Empty;                
            var NL = Environment.NewLine;

            Action<string,string> _addstr = (a,b)=> {
                s += a + "=@@@"+NL;
                s += b + NL;
                s += "@@@" + NL;
            };

            Action<string,object> _addobj = (a,b)=> {
                var json = string.Empty;
                try {
                    json = JsonUtil.Serialize(b);
                } catch {
                    G.NoticeToUser_warning("Faild to serialize " + a);
                }
                _addstr(a,json);
            };

            s = EXPORTMARKM + NL+NL;
            s+= "[" + COPYINFOKEYSTR + "]" + NL;
            s+= "pid=" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString() + NL;
            s+= "curdir=" + G.m_target_pathdir + NL;
            
            //_addobj("nodegroup_comment_list", G.nodegroup_comment_list_get());
            //_addobj("nodegroup_pos_list",     G.nodegroup_pos_list_get());
            _addstr("nodegroup_comment_list", get_nodegroup_comment_list_encode());
            _addstr("nodegroup_pos_list",     get_nodegroup_pos_list_encode());

            return s;
        }

        public static string get_nodegroup_comment_list_encode()
        {
            return JsonUtil.Serialize(G.nodegroup_comment_list_get());
        }
        public static string get_nodegroup_pos_list_encode()
        {
            return JsonUtil.Serialize(G.nodegroup_pos_list_get());
        }

        public static string _make_bitmap_data(List<string> statelist)
        {
            if (!G.psgg_file_w_data) return null; //本機能はpsgg_w_data時のみ。

            var hashlist = new List<string>();
            foreach(var state in statelist)
            {
                if (!StateUtil.IsValidStateName(state)) continue;
                var val = G.excel_program.GetString(state,G.STATENAME_thumbnail);
                if (val!=null && val.Length > 1 && val[0] == '#' )
                {
                    if (!hashlist.Contains(val))
                    {
                        hashlist.Add(val);
                    }
                }
            }
            
            var index = 0;
            var nl = Environment.NewLine;
            var s = string.Empty;
            foreach(var hash in hashlist)
            {
                var data = FileDbUtil.GetBitmapString(hash);
                if (data==null) continue;
                
                s += string.Format("hash_{0}=", index) + hash + nl;
                s += string.Format("data_{0}=", index) + "@@@" + nl + data + nl + "@@@" + nl;

                index++;
            }

            s = "[" + BITMAPINFOKEYSTR + "]" + nl + "num_of_bitmap=" + index.ToString() + nl + s + nl;

            return s;
        }

        #endregion

        #region Import
        enum IMPORT_TYPE
        {
            NONE,
            POS_CHANGE,     //
            DIRPOS_CHANGE,  //
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
        public static bool import(Point ipos, bool wo_ouflow)
        {
            var sm = new ImportControl();
            sm.m_wo_outflow = wo_ouflow;
            sm.m_click_pos = ipos;
            sm.Run();
            if (sm.m_error!=null)
            {
                G.NoticeToUser_warning(sm.m_error);
                return false;
            }
            
            // record
            var states = sm.GetPasteStates();
            var cmt =string.Empty;
            for(var n = 0; n < 3; n++)
            {
                if (states!=null && states.Count > n)
                { 
                    var s = states[n];
                    if (AltState.IsAltState(s)) s = AltState.TrimAltStateName(s);
                    if (!string.IsNullOrEmpty(cmt))
                    {
                        cmt += ",";
                    }
                    cmt += s;
                }
            }
            History2.SaveForce_paste(cmt);

            return true;
        }

#if obs
        public static bool import2(Point ipos)
        {
            Point click_pos = ipos;

            int src_pid = 0;
            string src_dir=null;

            var s = Clipboard.GetText();
            if (string.IsNullOrEmpty(s))
            {
                G.NoticeToUser_warning(G.Localize("w_clipboardtextisnull")/*  "Clipboard text is null."*/);
                return false;
            }

            // UUID確認
            if (!s.StartsWith(EXPORTMARKM)) {
                G.NoticeToUser_warning(G.Localize("w_clipboardbuffernotvalid")/* "Clipboard buffer is not valid."*/);
                return false;
            }
            s = ";" + s;
    
            var dicdic = StateUtil.MakeImportIni(s);
            if (dicdic==null || dicdic.Count==0) {
                G.NoticeToUser_warning(G.Localize("w_faildtoimportclipboard")/* "Faild to import from clipboard." */);
                return false;
            }

            try { 
                var infodic = DictionaryUtil.Get( dicdic ,COPYINFOKEYSTR);
                dicdic.Remove(COPYINFOKEYSTR); //dicdicから削除
                src_pid = ParseUtil.ParseInt(infodic["pid"]);
                src_dir = infodic["curdir"].Trim();
            }
            catch (SystemException e) {
                G.NoticeToUser_warning("Faild to getInfo:" + e.Message);
                return false;
            }

            /*
                コピー元のdir とコピー先の dirを調整する
            */
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

            /*
                コピー位置を調整する。
                １. コピー元を走査して、位置の最小値と最大値を求め、中間点を得る
                    a)対象は直下のみ
                    b)直下のDirNodeの位置も
            */
            Point minmum = new Point(int.MaxValue,int.MaxValue);
            Point maxmum = new Point(int.MinValue,int.MinValue);
            foreach(var k in dicdic.Keys)
            {
                var pos = Point.Empty;

                var dic = dicdic[k];
                var imtp = check_import_type(dic);
                if (imtp == IMPORT_TYPE.DIRPOS_CHANGE)
                {
                    var dirdata = DictionaryUtil.Get(dic,G.STATENAMESYS_dir);
                    var posx = DirPathExcelUtil.get_dirpos_dirdata(dirdata);
                    if (posx!=null)
                    {
                        pos = (Point)posx;
                    }
                }
                else if (imtp == IMPORT_TYPE.POS_CHANGE)
                {
                    var posstr = DictionaryUtil.Get( dic , G.STATENAMESYS_pos );
                    var posx = PointUtil.Parse(posstr);
                    if (posx==null)
                    {
                        G.NoticeToUser_warning("{5966BF76-1E3F-46C1-8FDA-5CD78B3A125C}"); 
                        continue;
                    }
                    pos = (Point)posx;
                }
                else
                {
                    continue;
                }
                minmum.X = Math.Min(minmum.X, pos.X);
                minmum.Y = Math.Min(minmum.Y, pos.Y);

                maxmum.X = Math.Max(maxmum.X, pos.X);
                maxmum.Y = Math.Max(maxmum.Y, pos.Y);
            }
            //var b_equal_minmax = minmum.Equals(maxmum);
            //Point midpos = b_equal_minmax  ?  PointUtil.Add_XY(minmum, (int)(G.state_width * 0.5f), (int)(G.state_height*0.5f)) : PointUtil.Center(minmum,maxmum);
            Point size = PointUtil.Abs( PointUtil.Sub_Point(maxmum,minmum ));
            size = PointUtil.Add_XY(size, (int)G.state_width , (int)G.state_height);

            /*
                コピー位置を調整する。
                2. click_posを minmum位置として、更新する
            */
            Func<Point,Point> update_point = (p)=>
            {
                var diff = PointUtil.Sub_Point(p, minmum);
                var p2 = PointUtil.Add_Point(click_pos,diff);
                var p3 = PointUtil.Add_XY(p2, -0.5f * size.X, -0.5f * size.Y );
                return Point.Truncate( p3 );
            };
            foreach(var k in dicdic.Keys)
            {
                var pos = Point.Empty;
                var dic = dicdic[k];
                var imtp = check_import_type(dic);
                if (imtp == IMPORT_TYPE.DIRPOS_CHANGE)
                {
                    var dirdata = DictionaryUtil.Get(dic,G.STATENAMESYS_dir);
                    var posx = DirPathExcelUtil.get_dirpos_dirdata(dirdata);
                    if (posx!=null)
                    {
                        pos = (Point)posx;
                        var newpos = update_point(pos);
                        var newdirdata = DirPathExcelUtil.makedata_dirpos(dirdata,newpos);
                        DictionaryUtil.Set(dic,G.STATENAMESYS_dir,newdirdata);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (imtp == IMPORT_TYPE.POS_CHANGE)
                { 
                    var posstr = DictionaryUtil.Get( dic, G.STATENAMESYS_pos);
                    var posx = PointUtil.Parse(posstr);
                    if (posx==null)
                    {
                        G.NoticeToUser_warning("{657C853F-506A-4D97-9EA5-9D7EC466AE3B}"); 
                        continue;
                    }
                    pos = (Point)posx;
                    //var diff = PointUtil.Sub_Point(pos, minmum);
                    //var pos2 = PointUtil.Add_Point(click_pos,diff);
                    //var pos3 = PointUtil.Add_XY(pos2, -0.5f * size.X, -0.5f * size.Y );
                    var pos3 = update_point(pos);
                    var pos3str =  PointUtil.ToStringF0_CSV(pos3);
                    DictionaryUtil.Set(dic, G.STATENAMESYS_pos,pos3str);
                }
            }

            // 同名のステート時にリネーム
            var statelist = G.excel_program.GetStateList();
            for (var loop = 0; loop<=1000; loop++)
            {
                if (loop >=1000) {
                    G.NoticeToUser_warning("Unexpected! {1D2DBDBB-0F42-4AD6-AAAA-7E7B02508870}");
                    return false;
                }
                var needloop = false;

                var dicstatelist = new List<string>(dicdic.Keys);
                foreach (var state in dicdic.Keys)
                {
                    if (statelist.Contains(state)) //送り先に同名ファイル発見
                    {
                        var to_state = state;
                        for(var loop2=0;loop2<100;loop++)
                        {
                            if (loop2>=100) {
                                G.NoticeToUser_warning("Unexpected! {0D81325A-B597-4712-B0F0-42A0CE30EE2C}");
                                return false;
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

            //直下のグループノードに同名がある場合
            for(var loop = 0; loop <= 1000; loop++)
            {
                var bNeedLoop = false;
                foreach(var state in dicdic.Keys)
                {
                    var dic = dicdic[state];
                    var imtp = check_import_type(dic);
                    if (imtp == IMPORT_TYPE.DIRPOS_CHANGE) //直下グループ
                    {
                        var dirpath = get_dirpath_from_dic(dic);
                        if (statelist.Find(i=>DirPathExcelUtil.get_dirpath(i)==dirpath)!=null) //同名が存在している
                        {
                            var newdirpath = make_another_groupname(dirpath, dicdic, statelist); //新規グループ名
                            overwrite_dstDirpath_if_same_srcDirpath(dirpath,newdirpath, dicdic); //入れ替え
                            bNeedLoop = true;
                            break;
                        }
                    }
                }
                if (bNeedLoop) continue;
                break;
            }


            // ステート作成
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

            }

            //フォーカス作成
            var focus_list = new List<string>();
            foreach(var key in dicdic.Keys)
            {
                var index = key;
                var dirpath = DirPathExcelUtil.get_dirpath_dirdata((dicdic[key])[G.STATENAMESYS_dir]);
                if (!dirpath.Contains(G.m_target_pathdir))
                {
                    G.NoticeToUser_warning("{6673278E-02CF-46B0-9AF2-10B5E46239B2}");
                    continue;
                }
                if (dirpath.Length > G.m_target_pathdir.Length)
                {
                    var rest = dirpath.Substring(G.m_target_pathdir.Length);
                    var groupname = RegexUtil.Get1stMatch(@"^[^\/]+\/",rest).Trim('/');
                    var altstate = AltState.MakeAltStateName(groupname);
                    index = altstate;
                }
                if (!focus_list.Contains(index))
                { 
                    focus_list.Add(index);
                }
            }
            G.vf_sc.m_group_focus_list = focus_list;

            History.ReqToSave("Imported states");

            return true;
        }
        private static string _create_new_dir(string src_dir,  string src_state_dir, string target_dir)
        {
            var addsrc = src_state_dir.StartsWith(src_dir) ?  src_state_dir.Substring(src_dir.Length) : "";
            var newdir = GroupNodeUtil.path_normalize(target_dir + addsrc);
            return newdir;
        }

        /// <summary>
        /// 新規の別グループ名作成。
        /// 使われていない名前であること！
        /// </summary>
        private static string make_another_groupname(string dirpath, Dictionary<string,  Dictionary<string,string>> dicdic, List<string> statelist )
        {
            if (dirpath == "/") throw new SystemException("{773D29B7-1BB5-401F-9695-D87388FEABF2}");
            var parentpath = GroupNodeUtil.get_parent_path(dirpath);
            var lastpath = GroupNodeUtil.get_last_path(dirpath);
            var  newname = lastpath.Trim('/');

            Func<string,bool> is_in_dicdic = (s) => { 
                foreach(var ts in dicdic.Keys)
                {
                    var dic = dicdic[ts]; 
                    var tdirpath = get_dirpath_from_dic(dic);
                    if (tdirpath == s) return true;
                }
                return false;
            };
            
            Func<string,bool> is_in_statelist = (s) => {
                foreach(var ts in statelist)
                {
                    var tdirpath = DirPathExcelUtil.get_dirpath(ts);
                    if (tdirpath == s) return true;
                }
                return false;
            };

            for(var loop = 0; loop<=1000;loop++)
            { 
                if (loop == 1000) throw new SystemException("{C494E00A-1320-4A22-A403-FB5FED8EA949}");

                newname = StateUtil.MakeNewName( newname );
                var tdirpath = GroupNodeUtil.pathcombine(parentpath,newname);
                if (is_in_dicdic(tdirpath)) continue;
                if (is_in_statelist(tdirpath)) continue;

                return GroupNodeUtil.pathcombine(parentpath, tdirpath);
            }

            G.NoticeToUser_warning("{FDA27A3C-22E0-4876-A3CC-FF81AD11F09F}");

            return dirpath;
        }

        private static string get_dirpath_from_dic(Dictionary<string,string> dic)
        {
            var dirdata = DictionaryUtil.Get(dic, G.STATENAMESYS_dir);
            var dirpath = DirPathExcelUtil.get_dirpath_dirdata(dirdata);
            return dirpath;
        }


        /// <summary>
        /// dicdicないのdirpathが srcdirpathと前一致した時、dstdirpathに入れ替える
        /// </summary>
        private static void overwrite_dstDirpath_if_same_srcDirpath(string srcdirpath, string dstdirpath, Dictionary<string, Dictionary<string,string>> dicdic)
        {
            foreach(var s in dicdic.Keys)
            {
                var dic = dicdic[s];
                var dirdata = DictionaryUtil.Get(dic,G.STATENAMESYS_dir);
                var dirpath = DirPathExcelUtil.get_dirpath_dirdata(dirdata);
                if (dirpath.StartsWith(srcdirpath)) //前方一致？
                {
                    var newpathdir = GroupNodeUtil.pathcombine( dstdirpath,  dirpath.Substring(srcdirpath.Length) ); //前方入れ替え
                    var dirdata2 = DirPathExcelUtil.makedata_dirpath(dirdata, newpathdir);

                    DictionaryUtil.Set(dic,G.STATENAMESYS_dir,dirdata2);
                }
            }
        }
#endif

#endregion


    }
}
