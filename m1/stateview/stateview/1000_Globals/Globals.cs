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
using System.Reflection;

namespace stateview
{
    public static class Globals
    {
        //アボート
        internal static bool ABORT = false;

        //Active
        internal static bool Active =  true;

        //自プロセスID
        internal static int     PROCESS_ID          { get { return System.Diagnostics.Process.GetCurrentProcess().Id; } }
        internal static IntPtr  VIEWFORM_HANDLE     { get { return view_form.Handle;                                  } }

        //予約ステート名
        internal static string STATENAME_state      { get { return StateWork.NM_STATE;      } }
        internal static string STATENAME_statecmt   { get { return StateWork.NM_STATECMT;   } }
        internal static string STATENAME_statetyp   { get { return StateWork.NM_STATETYP;   } }
        internal static string STATENAME_statedco   { get { return StateWork.NM_STATEDCO;   } }
        internal static string STATENAME_nextstate  { get { return StateWork.NM_NEXTSTATE;  } }
        internal static string STATENAME_branch     { get { return StateWork.NM_BRANCH;     } }
        internal static string STATENAME_branchcmt  { get { return StateWork.NM_BRANCHCMT;  } }
        internal static string STATENAME_brcond     { get { return StateWork.NM_BRCOND;     } }
        internal static string STATENAME_thumbnail  { get { return StateWork.NM_THUMBNAIL;  } }
        internal static string STATENAME_basestate  { get { return StateWork.NM_BASESTATE;  } }
        internal static string STATENAME_gosubstate { get { return StateWork.NM_GOSUBSTATE; } }
        internal static string STATENAME_return     { get { return StateWork.NM_RETURN;     } }

        //予約ステート名全部
        private  static string[] _STATENAME_ALLRESERVES;
        internal static string[] STATENAME_ALLRESERVES { get {
                if (_STATENAME_ALLRESERVES==null)
                {
                    _STATENAME_ALLRESERVES = new string[] {
                        STATENAME_state,
                        STATENAME_statecmt,
                        STATENAME_statetyp,
                        STATENAME_statedco,
                        STATENAME_nextstate,
                        STATENAME_branch,
                        STATENAME_branchcmt,
                        STATENAME_brcond,
                        STATENAME_thumbnail,
                        STATENAME_basestate,
                        STATENAME_gosubstate,
                        STATENAME_return
                    };
                }
                return _STATENAME_ALLRESERVES;
            } }

        //システム用ステート名 
        internal static string STATENAMESYS_dir  { get { return StateWork.NMS_DIR;  } } //!dir
        internal static string STATENAMESYS_uuid { get { return StateWork.NMS_UUID; } } //!uuid
        internal static string STATENAMESYS_pos  { get { return StateWork.NMS_POS;  } } //!pos


        //system言語
        internal static bool bJorE { get {  return system_lang == "jpn"; } }
        internal static string system_lang { get {
                if (_______lang==null) {
                    var regst_lang = RegistryWork.Get_lang();
                    if (regst_lang == "jp")
                    {
                        _______lang = "jpn";
                    }
                    else if (regst_lang == "en")
                    {
                        _______lang = "en";
                    }
                    else if (System.Globalization.CultureInfo.CurrentCulture.Name. StartsWith("ja-"))
                    {
                        _______lang = "jpn";
                    }
                    else
                    {
                        _______lang =  "en";
                    }
                }
                return _______lang;
            }
            set
            {
                _______lang = value;
                if (value == "jpn") RegistryWork.Set_lang("jp");
                else if (value == "en") RegistryWork.Set_lang("en");
            }
        }
        private  static string _______lang = null;

        //version
        internal static string version;
        internal static string githash;
        internal static string buildtime;
        internal static string milestone;
        internal static string milestonetxt;

        //tick counter
        internal static int tick_counter {
            get {
                if (view_form!=null)
                {
                    return view_form.m_timer_tick_counter;
                }
                return -1;
            }
        }

        internal static int thumbnail_size    = 100; //縦横のMAX

        internal static int bitmap_width      = 5000;
        internal static int bitmap_height     = 2000;

        internal static double scale_percent  = 100f;
        internal static void set_scalepercent_with_textbox(double value)
        {
            //G.scale_percent = value;
            G.view_form.zoomTextBox.Text = value.ToString();
        }

        internal static double scale  { get {return scale_percent * 0.01f; } }




        internal static bool   use_statecmt      = true;
        internal static bool   use_thumbnail     = true;
        internal static bool   use_contents      = true;

        //[Obsolete]
        //internal static bool   use_excel_color   = false;

        internal static string fillter_regextext = string.Empty;
        internal static int    max_num_of_states = int.MaxValue;

        internal static bool   force_display_outpin = false;

        //[Obsolete]
        //internal static string fillter_default_id   = string.Empty;
        //[Obsolete]
        //internal static string fillter_cur_id       = string.Empty;

        internal static Draw                      draw;

        internal static _5000_MainForm.ViewForm        view_form;
        internal static _7000_DebugForm.DebugForm      debug_form;
        internal static _5000_ViewForm.dialog.FindForm find_form;
        internal static _5000_ViewForm.dialog.OptionForm option_form;

        //internal static _1185_FocusTrack.FocusTrackForm focustrack_form;

        internal static MultiEditControl          multiedit_control;

        internal static _5300_EditForm.EditForm   edit_form             { get { return multiedit_control.m_editform; } }
        internal static PictureBox                main_picturebox       { get { return view_form.MainPictureBox; } }
        internal static PictureBox                overlay_picturebox    { get { return view_form.OverlayPictureBox; } }
        internal static PictureBox                freearrow_picturebox  { get { return view_form.FreeArrowPictureBox; } }
        internal static PictureBox                groupfocus_picturebox { get { return view_form.GroupFocusPictureBox; } }

        internal static ViewFormStateControl      vf_sc;
        internal static void                      req_redraw()          { if (vf_sc!=null) vf_sc.ReqRedraw(); }
        internal static void                      req_redraw_force()    { AppUpdate.mouse_update(MouseEventId.ABORT);  G.req_redraw(); }

        // キーボードからの命令用
        internal static KEYEXEC                   keyexec { get {  if (vf_sc!=null) return vf_sc.m_keyexec;     return KEYEXEC.none; } set { if (vf_sc!=null) vf_sc.m_keyexec = value;     } }
        internal static bool                      m_keyopen_in_or_out { get {  if (vf_sc!=null) return vf_sc.m_keyopen_in_or_out;     return false; } set { if (vf_sc!=null) vf_sc.m_keyopen_in_or_out = value;     } }
        internal static InOutBaseData             m_keyopen_out_item  { get {  if (vf_sc!=null) return vf_sc.m_keyopen_out_item;      return null;  } set { if (vf_sc!=null) vf_sc.m_keyopen_out_item  = value;     } }

        // ドラッグ＆ドロップ  CopyCollectionよりドラッグ＆ドロップ
        public enum CCDD
        {
            none,
            dragenter,
            dragdrop
        }
        internal static CCDD                      m_cc_dragdrop;  //コピーコレクションのドラッグ中      
        internal static Bitmap                    m_cc_dropbmp;     //ドラッグ中のカーソルビットマップ
        internal static string                    m_cc_droppath;    //コピーコレクションのファイルパス　※bitmapへのパスなので、同フォルダ内にデータがある。

        #region statuslog
        private  static StringBuilder             _statuslog = new StringBuilder();
        private  static void                      _set_statuslog(string s)
        {
            lock(_statuslog)
            {
                _statuslog.Clear();
                _statuslog.Append(s);
            }
        }
        private static string                    _get_statuslog()
        {
            var s = string.Empty;
            lock(_statuslog)
            {
                if (_statuslog.Length!=0)
                {
                    s = _statuslog.ToString();
                }
            }
            return s;
        }

      #endregion

        #region statuslog_private
        private static StringBuilder           _log = new StringBuilder();
        private static void                    _set_log(string s)
        {
            lock(_log)
            {
                _log.Clear();
                _log.Append(s);
            }
        }
        private static string                  _get_log()
        {
            lock(_log)
            {
                return _log.ToString();
            }
        }
        internal static string log { get { return _get_log(); } set { _set_log(value); } }
        internal static void logAppend(string s)
        {
#if DEBUG
            debug_form.textBoxAppend.AppendText(s);
#endif
        }
        #endregion

        #region debug_pointer
        internal static Point point_on_viewform { get {
                try {
                    if (view_form!=null)
                    {
                        var pos = view_form.PointToClient(Cursor.Position);
                        if (PointUtil.Validate(pos,view_form.Size.Width,view_form.Size.Height))
                        {
                            return pos;
                        }
                    }
                } catch { }
                return new Point(-1,-1);
            } }
        internal static Point point_on_pixcturebox { get {
                try {
                    if (main_picturebox!=null)
                    {
                        var pos = (PointF)G.main_picturebox.PointToClient(Cursor.Position);
                        //pos = PointUtil.Multiply(pos, (float)(1.0f/G.scale));
                        return Point.Truncate(pos);
                    }
                }
                catch { }
                return new Point(-1,-1);
            } }
        internal static Point point_on_bmp { get {
                var pos = point_on_pixcturebox;
                if (PointUtil.IsEqual(pos,new Point(-1,-1),0))
                {
                    return pos;
                }
                pos = PointUtil.Multiply(pos, (float)(1.0f/G.scale));
                return pos;
            } }
        #endregion


        public static string                    psgg_file;
        public static bool                      psgg_file_w_data;//psgg fileにデータ付属 (ver1.1) ※以前のはver1.0

        private  static string                  _load_file;
        public   static string                  load_file  {
                                                                set {
                                                                        if (value != null) {
                                                                            // PSGGファイルを優先記録
                                                                            if (!string.IsNullOrEmpty(psgg_file)) {
                                                                                history.UpdateHistory(psgg_file);
                                                                            }
                                                                            else {
                                                                                history.UpdateHistory(value);
                                                                            }
                                                                        }
                                                                        _load_file = value;
                                                                    }
                                                                get { return _load_file; }
                                                           }
		public   static string                  load_file_name     { get { return Path.GetFileName(_load_file);      } }
        public   static string                  load_file_dir      { get { return Path.GetDirectoryName(_load_file); } }
        public   static string                  load_file_dir_last { get { return Path.GetFileName(load_file_dir);   } } // ファイル名が含まれるフォルダ名
        public   static string                  load_file_name_woext { get { return Path.GetFileNameWithoutExtension(_load_file); } } //拡張子を除いたファイル名

        public   static string                  gen_dir { get { return SettingIniUtil.GetGenDir(); } } //生成用フォルダ - フルパス
        public   static string                  gen_file { get { return SettingIniUtil.GetGeneratedSource(); } }  //生成用ファイル - フルパス
        public   static string                  imp_file { get { return SettingIniUtil.GetSourceForImplementing(); } } //実装用ファイル - フルパス
        public   static string                  macro_file { get { return SettingIniUtil.GetMacroIni(); } } //マクロファイル - フルパス
        //public   static string                  psgg_file { get { return Path.Combine(load_file_dir,load_file_name_woext) + ".psgg"; } }
        public   static string                  helpweb_file { get { return SettingIniUtil.GetHelpweb(); } }

        public   static Encoding                src_enc { get {
                try {
                    return Encoding.GetEncoding(SettingIniUtil.GetSrcEnc());
                } catch { return Encoding.UTF8; }
            } }

        public   static string                  guid {  get {  //一時フォルダ、履歴で対象認識に利用
                                                                if (string.IsNullOrEmpty(_guid)) _guid = Guid.NewGuid().ToString();
                                                                return _guid;
                                                            }
                                                        set { _guid = value; }
                                                }
        private  static string                  _guid;
        public   static string                  userenv_guid {// ユーザ環境用の本ファイルの個別guid ... コピーされて同じguidへの対策
                                                        get {
                                                            if (string.IsNullOrEmpty(_userenv_guid))
                                                            {
                                                                var file = psgg_file;
                                                                if (string.IsNullOrEmpty(psgg_file)) {  file = load_file; }
                                                                var id = PathUtil.PathToFolderNameHeaderString(file);
                                                                if (string.IsNullOrEmpty(id)) id = "null";
                                                                _userenv_guid = "ue" + id + file.GetHashCode().ToString();
                                                            }
                                                            return _userenv_guid; 
                                                        }
                                                }
        private static string                   _userenv_guid;
        public static System.Threading.Mutex    mutex;

        public static string                    last_action; //履歴で使用

        internal static bool                    request_startload;

        internal static Dictionary<int, ExcelCellCacheItem> m_excel_cell_cache_dic;
        internal static ExcelCellCacheItem                  get_excel_cell_cache(int row, int col)
                                                            {
                                                                var key = ExcelCellCacheItem.make_key(row,col);
                                                                if (m_excel_cell_cache_dic.ContainsKey(key)) return m_excel_cell_cache_dic[key];
                                                                return null;
                                                            }
        internal static string                              get_excel_str(string state, string name)
                                                            {
                                                                return excel_program.GetString(state,name);
                                                            }
        internal static Dictionary<string,string>           get_excel_allstr(string state)
                                                            {
                                                                return excel_program.GetAllString(state);
                                                            }
        internal static Point?                              get_excel_position(string state)
                                                            {
                                                                var v = excel_program.GetString(state, STATENAMESYS_pos);
                                                                return PointUtil.Parse(v);
                                                            }
        internal static Dictionary<int, ExcelCellCacheItem> clone_excel_cell_cache_dic()
                                                            {
                                                                var newdic = new Dictionary<int, ExcelCellCacheItem>();
                                                                foreach(var p in m_excel_cell_cache_dic )
                                                                {
                                                                    var cell = p.Value.Clone();
                                                                    newdic.Add(p.Key, cell);
                                                                }
                                                                return newdic;
                                                            }

        internal static int                                 m_excel_max_row;
        internal static int                                 m_excel_max_col;

        //[Obsolete]
        //internal static bool                    excel_read_optimized = true;

        internal static string                  sheetchart    { get { return WordStorage.Store.sheetchart; } }// "state-chart";
        internal const int                      NAME_COL  = 2;
        internal const int                      STATE_ROW = 2;

        [Obsolete]
        internal static readonly string         sheetlayout = "layout";

        internal static string                  sheetconfig   { get { return WordStorage.Store.sheetconfig;   } } //= "config";
        internal static string                  sheettempsrc  { get { return WordStorage.Store.sheettempsrc;  } } // = "template-source";
        internal static string                  sheettempfunc { get { return WordStorage.Store.sheettempfunc; } } // = "template-statefunc";
        internal static string                  sheetsetting  { get { return WordStorage.Store.sheetsetting;  } } // = "setting.ini";
        internal static string                  sheethelp     { get { return WordStorage.Store.sheethelp;     } } // = "help"
        internal static string                  sheetitems    { get { return WordStorage.Store.sheetitems;    } } // = "items"

        internal static ExcelProgram            excel_program;
        internal static ExcelPictures           excel_pictures;

        internal static ExcelConfig             excel_config;
        internal static ExcelConvertSettings    excel_convertsettings;

        internal static MacroIni                macro_ini;

        internal static bool                    save_by_uuid_order = true;
        internal static SaveData                loaded_data;
        internal static string                  fillter_text;

        internal static Dictionary<string, PointF>                      state_location_list;           //ポジション
        internal static Dictionary<string, Dictionary<string, PointF> > fillter_state_location_list;   //フィルター単位のポジション

        internal static Dictionary<string, List<InOutBaseData>>            state_input_src_list;
        internal static Dictionary<string, List<InOutBaseData>>            state_output_dst_list;
        
        /// <summary>
        /// state_input_src_list[state]内のステートを全部返す　（但し baseは除外）
        /// </summary>
        internal static List<string>                                       state_input_src_list_allstates_wo_base(string state)
        {
            var list = DictionaryUtil.Get(state_input_src_list,state);
            if (list == null) return null;
            var outlist = new List<string>();
            list.ForEach(i=> {
                if (i.attrib != InOutBaseData.ATTRIB._base)
                {
                    outlist.Add(i.state);
                }
            });
            return outlist;
        }

        /// <summary>
        /// state_output_src_list[state]内のステートを全部返す　（但し baseは除外）
        /// </summary>
        internal static List<string>                                       state_output_dst_list_allstates_wo_base(string state)
        {
            var list = DictionaryUtil.Get(state_output_dst_list,state);
            if (list == null) return null;
            var outlist = new List<string>();
            list.ForEach(i=> {
                if (i.attrib != InOutBaseData.ATTRIB._base)
                {
                    outlist.Add(i.state);
                }
            });
            return outlist;
        }

        /// <summary>
        /// 表示中のグループノードの流入先リスト
        /// </summary>
        internal static Dictionary<string, List<string>>                  group_input_src_list_wo_base_on_current;

        #region node group comment/position list
        private static Dictionary<string, string>                      nodegroup_comment_list;        //ノードグループのコメントリスト  ※配下のノードがグループだけの時にコメントが消えるバグ対応
        private static Dictionary<string, Point>                       nodegroup_pos_list;            //ノードグループのポイントリスト

        internal static Dictionary<string, string> nodegroup_comment_list_get() {  return nodegroup_comment_list;  }
        internal static void nodegroup_comment_list_set(Dictionary<string,string> list) { nodegroup_comment_list = list; }

        internal static Dictionary<string,Point> nodegroup_pos_list_get() { return nodegroup_pos_list; }
        internal static void nodegroup_pos_list_set(Dictionary<string,Point> list) { nodegroup_pos_list = list; }

        internal static void nodegroup_comment_pos_clear()
        {
            if (nodegroup_comment_list==null)
            {
                nodegroup_comment_list = new Dictionary<string, string>();
                nodegroup_pos_list = new Dictionary<string, Point>();
            }
            else
            {
                nodegroup_comment_list.Clear();
                nodegroup_pos_list.Clear();
            }
        }
        internal static string nodegroup_comment_get(string path)
        {
            return (nodegroup_comment_list!=null && nodegroup_comment_list.ContainsKey(path)) ? nodegroup_comment_list[path] : "";
        }
        internal static void nodegroup_comment_set(string path,string comment)
        { 
            if (nodegroup_comment_list==null) throw new SystemException("{4FC7B0A2-7CF0-4F8B-9004-3C9DC64AC712}");
            if (nodegroup_comment_list.ContainsKey(path)) {  nodegroup_comment_list[path] = comment; } else { nodegroup_comment_list.Add(path,comment); }
        }
        internal static Point? nodegroup_pos_get(string path)
        {
            var pos =  (nodegroup_pos_list!=null && nodegroup_pos_list.ContainsKey(path)) ? nodegroup_pos_list[path] : new Point(int.MinValue,int.MinValue);
            if (pos.X == int.MinValue) return null;
            return pos;
        }
        internal static void nodegroup_pos_set(string path, Point? pos)
        {
            if (nodegroup_pos_list==null) throw new SystemException("{44B932AB-2E97-43DA-BC45-CE570541C39B}");
            var posx = pos == null ? new Point(int.MinValue , int.MinValue) : (Point)pos;
            if (nodegroup_pos_list.ContainsKey(path)) { nodegroup_pos_list[path] = posx; } else { nodegroup_pos_list.Add(path,posx); }
        }
        #endregion

        internal static List<string>            state_working_list;      // AltStateが含んだ、作業用のステートリスト
        internal static List<int>               state_working_col_list;  //上記のカラムインデックスリスト

                                                //キャッシュ変化後に実行せよ。
        internal static void                    state_working_list_reflesh() {
                                                    state_working_list     = G.excel_program.GetStateList()   ;
                                                    state_working_col_list = G.excel_program.GetStateColList();
                                                }

        internal static int                     get_state_col(string state) {
                                                    var idx = state_working_list.FindIndex(i=>i==state);
                                                    if (idx>=0) return state_working_col_list[idx];
                                                    return -1;
                                                }
        internal static void                    set_state_group(string state,string group)
                                                {
                                                    excel_program.SetString(state,"!group",group);
                                                }
        internal static string                  get_state_group(string state)
                                                {
                                                    return excel_program.GetString(state,"!group");
                                                }
        internal static double                  get_state_uuid(string state)
                                                {
                                                    return excel_program.GetUUID(state);
                                                }
        internal static List<string>            state_list;
        internal static List<string>            name_list;
        internal static List<StateInfo>         state_info_list;
        internal static StateInfo               get_state_info(string state) { return G.state_info_list.Find(i=>i.state == state); }

        internal static Graphics                maingraphs;     //メイングラフ
        internal static Bitmap                  mainbitmap;     //メインビットマップ
        internal static RectangleF              mainbmprect { get {return new RectangleF(0,0,mainbitmap.Width,mainbitmap.Height); } }

        internal static Graphics                overlaygraphs;  //オーバレイグラフ
        internal static Bitmap                  overlaybitmap;  //オーバレイビットマップ

        internal static MouseEventId            mouse_event;
        internal static bool                    mouse_down_or_up;
        internal static MouseButtons            mouse_latest_button;
        internal static Double                  delta_time;     //アップデート差分秒
        internal static bool                    mouse_double_clicked; //ダブルクリックを優先反応させるため。

        internal static DrawList drawlistMain     = new DrawList(); //メイン用
        internal static DrawList drawlistFocus    = new DrawList(); //フォーカス用
        internal static DrawList drawlistOverlay  = new DrawList(); //オーバーレイ用

        internal static Dictionary<string,Draw.DrawStateData> m_draw_data_list;
        internal static Draw.DrawStateData                    get_draw_data(string state)
                                                              {
                                                                    if (state!=null && G.m_draw_data_list!=null && G.m_draw_data_list.ContainsKey(state)) return G.m_draw_data_list[state];
                                                                    return null;
                                                              }
        internal static RectangleF?                           get_draw_wp_frame(string state)
                                                              {
                                                                    var dd = get_draw_data(state);
                                                                    if (dd!=null)
                                                                    {
                                                                        return dd.wp_frame_drect;
                                                                    }
                                                                    return null;
                                                              }

        internal static RectangleF?                           get_draw_wp_outframe(string state)
                                                              {
                                                                    var dd = get_draw_data(state);
                                                                    if (dd!=null)
                                                                    {
                                                                        return dd.wp_outframe_drect;
                                                                    }
                                                                    return null;
                                                              }

        internal static Color bg_color            = Color.FromArgb(65,65,65);          //背景色
        internal static Color grid_color          = Color.FromArgb(112,112,112);       //グリッド色

        //[Obsolete]internal static Color outframe_color      = Color.FromArgb(237,125,49);        //外枠色
        //[Obsolete]internal static Color outframebg_color    = Color.FromArgb(175,171,171);       //外枠背景色


        internal static Color outputpo_color       = Color.Red;                       //出力ポイント色
        internal static Color outputpoframe_color  = Color.White;                     //出力ポイント枠色

        //[Obsolete]internal static Color boutpo_color         = Color.Red;                       //分岐出力ポイント色
        //[Obsolete]internal static Color boutpoframe_color    = Color.White;                     //分岐出力ポイント枠色


        internal static PointF STARTPOS        = new PointF(50,100);
        internal static float  FRAMELINESIZE   = 1.0f;
        internal static float  GAP             = 1.0f;
        internal static PointF PADDING         = new PointF(9,9);
        internal static PointF TEXTMARGIN      = new PointF(0,0);
        internal static float  POINTDIAMETER  = 8;

        internal static int DWPRI_STATE   = 2000;
        internal static int DWPRI_ARROW   = 1000;

        internal static readonly string DEFAULT_FONT = "MS UI Gothic";
        internal static readonly float  DEFAULT_FONTSIZE = 11;


        internal static float  LINE_WIDTH       = 2;
        internal static float  ARROWHEAD_WIDTH  = 10;
        internal static float  ARROWHEAD_HEIGHT = 10;

        internal static bool option_delete_thisstring = true; //表示時にthis.を削除する
        internal static bool option_delete_br_string = true;  //表示時にbr_を削除する
        internal static bool option_delete_bracket_string = true;    //表示時に[]を削除する
        internal static bool option_delete_s_state_string = true;    //表示時に'S_'を削除する
        internal static bool option_omit_basestate_string = false;   //ステートに表示するベースステート名を省略する
        internal static bool option_hide_basestate_contents = true;  //ベースステートのコンテンツを非表示にする
        internal static bool option_hide_branchcmt_onbranchbox = false;  //ステートボックスのブランチ部分にコメントを表示しない           

        internal static bool option_copy_output_to_clipboard = false; //変換結果をクリップボードへコピー
        internal static bool option_convert_with_confirm = false;     //変換時に確認の表示
        internal static bool option_ignore_case_of_state = false;     //ステート名の大文字小文字を区別しない 

        internal static bool option_set_default_comment {       // 初期値としてコメントを入力
            get { return RegistryWork.Get_item_option_set_default_comment();  }  //レジストリに変更
            set { RegistryWork.Set_item_option_set_default_comment(value);}
        }

        internal static bool option_editbranch_automode = true;        //分岐編集時に自動でIF/ELSE IF/ELSEを選択肢に出す。および移動する。

        internal static bool option_use_custom_prefix = false;        //ステート名の先頭文字を自由にするか？

        internal static bool option_mrb_enable = false;  　　　　　　  //マウス右ボタンでの特殊操作有効
        internal static bool option_historypanel_onstart = true;        //起動時のヒストリパネルONOFF状況 
        internal static bool option_forceclose_ifviewchangeonly = false; // 閲覧のみの変更時、クローズさせてよい。

        internal static bool option_jump_if_statego_exist = false;     //既にStateGoで開いている場合に自動遷移

        internal static bool   option_use_donotedit_mark = false;            //各行の最後に編集不可のマークを挿入する  ※ G.donotsave_push(false) G.donotsave_pop() ・・一時的値変更用
        internal static string option_donotedit_mark_columns = "76,116,136"; //編集不可マークの追加カラム数。直近のカラム数が使われ、それ以上は後ろに追加される。
        internal static string option_donotedit_mark = "*DoNotEdit*";        //編集不可マーク

        internal static bool   option_lexical_color_onoff = true;            //字句解析により色設定ON/OFF

        internal static string font_name = DEFAULT_FONT;
        internal static float  font_size = DEFAULT_FONTSIZE;

        internal static float  comment_font_size = 0;    // >0 の時有効
        internal static float  contents_font_size = 0;   // >0 の時有効

        internal static float  state_width  = 140;
        internal static float  state_height = 20;
        internal static float  state_short_width = 50;    //start/end等
        internal static float  state_short_height = 20;   //　　同
        internal static float  comment_block_height = 20;
        internal static bool   comment_block_fixed = false;
        internal static float  content_max_height = 200;
        internal static float  line_space   = -1;


        internal static helpProgram2              help_program2;// = new helpProgram2();
        internal static itemsInfoProgram          itemsInfo_program;

        #region ブランチ内の改行用
        internal static string branch_special_newlinechar = "￢";
        internal static string encode_branch_special_newlinechar(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var val = s.Replace(Environment.NewLine, branch_special_newlinechar);
            return val;
        }
        internal static string decode_branch_special_newlinechar(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var val = s.Replace(branch_special_newlinechar,Environment.NewLine);
            return val;
        }
        #endregion

        #region TabTreeNode タブ型ツリーノード
        internal static TabNodeTree tabNodeTree;


        #endregion

        internal static LineColor      line_color;

        #region NodeDir

        internal static string m_target_pathdir = "/";

        private static GroupNodeDir m_nodedir = new GroupNodeDir();
        /// <summary>
        /// カレントノードディレクトリセット
        /// </summary>
        internal static void node_set_curdir(string path)
        {
            m_nodedir.Analize(path);
        }
        /// <summary>
        /// グループノード内へ
        /// </summary>
        internal static void node_enter_group(string groupname)
        {
            m_nodedir.Analize_enter(groupname);
        }
        /// <summary>
        /// 一階層上へ
        /// </summary>
        internal static bool node_leave_group()
        {
            return m_nodedir.Analize_leave();
        }
        /// <summary>
        /// カレント Dir Path 取得
        /// </summary>
        internal static string node_get_cur_dirpath()
        {
            var path = GroupNodeUtil.path_normalize(m_nodedir.m_target_path);
            return path;
        }
        /// <summary>
        /// カレントノードディレクトリ直下のフォルダ取得
        /// </summary>
        internal static List<string> node_get_groupsOnTargetDir()
        {
            return m_nodedir.GetGroupsOnTargetDir();
        }
        /// <summary>
        /// ステートのスタイル取得
        /// </summary>
        internal static SS.STYLE node_get_style(string state)
        {
            var slist = m_nodedir.m_style_list;
            if (slist!=null)
            {
                if (slist.ContainsKey(state))
                {
                    return slist[state];
                }
            }
            return SS.STYLE.EXCLUDE;
        }
        /// <summary>
        /// 指定グループに属するステート一覧
        /// </summary>
        internal static List<string> node_get_statelist_in_group(string group)
        {
            var glist = m_nodedir.m_subdir_list;
            if (glist == null) return null;
            if (glist.ContainsKey(group))
            {
                return glist[group];
            }
            return null;
        }
        /// <summary>
        /// 指定ステートのグループノード名
        /// カレント直下のsubDir名を返す
        /// </summary>
        internal static string node_get_groupname(string state)
        {
            if (string.IsNullOrEmpty(state)) return null;
            return m_nodedir.GetDirNameUnderCurrent(state);
        }

        /// <summary>
        ///  指定ステートのグループのコメント取得  (!dir値`のコメント部)
        /// </summary>
        internal static string node_get_comment_by_state(string state)
        {
            return m_nodedir.GetDirCommentByState(state);
        }

        /// <summary>
        /// 指定グループのコメント取得
        /// </summary>
        internal static string node_get_comment_by_groupname(string groupname)
        {
            return m_nodedir.GetDirCommentByGroupname(groupname);
        }

        /// <summary>
        /// 指定パスDIRのコメント取得 ※  /hoge/hege/ 形式
        /// </summary>
        internal static string node_get_comment_by_pathdir(string pathdir)
        {
            return m_nodedir.GetDirCommentByPath(pathdir);
        }

        /// <summary>
        /// 指定グループにコメント設定
        /// </summary>
        internal static void node_set_comment_by_groupname(string groupname, string comment)
        {
            m_nodedir.SetDirCommentByGroupname(groupname,comment);

        }
        /// <summary>
        /// エクセルキャッシュに書き込み
        /// </summary>
        internal static void node_write_to_excel_cache()
        {
            m_nodedir.WriteToExcelCache();
        }

        /// <summary>
        /// 表示用ステート全取得　※フィルター済み
        /// </summary>
        internal static List<string> node_get_display_all()
        {
            return m_nodedir.GetAllDisplayingStates();
        }

        /// <summary>
        /// ステートの位置取得
        /// </summary>
        internal static Point? node_get_pos(string state)
        {
            return m_nodedir.GetPosition(state);
        }

        /// <summary>
        /// グループ作成
        /// </summary>
        internal static void node_grouping(string groupname, List<string> targets, string click_state, string comment = "")
        {
            m_nodedir.MakeGroup(groupname,  targets,click_state, comment);
        }

        /// <summary>
        /// グループ解除
        /// </summary>
        internal static void node_ungrouping(string groupname)
        {
            m_nodedir.MakeUngroup(groupname);
        }

        /// <summary>
        /// グループ名変更
        /// </summary>
        internal static void node_rename_groupname(string from, string to)
        {
            m_nodedir.Rename_groupname(from,to);
        }

        /// <summary>
        /// グループコピー
        /// </summary>
        internal static void node_copy_group(string src)
        {
            var dst = m_nodedir.Make_groupname_for_copy(src);
            m_nodedir.Copy_group(src,dst);
        }

        internal static void node_delete_group(string src)
        {
            m_nodedir.Delete_group(src);
        }

        /// <summary>
        /// ステートを指定パスへ移動
        /// </summary>
        internal static void node_moveto_group(string state, string pathdir)
        {
            node_moveto_group(new List<string> {state }, pathdir);
        }
        internal static void node_moveto_group(List<string> state_list, string pathdir)
        {
            m_nodedir.Moveto_group(state_list,pathdir);
        }
        internal static void node_moveto_group_group(string src_pathdir, string dst_pathdir)
        {
            m_nodedir.Moveto_group_group(src_pathdir,dst_pathdir);
        }
        /// <summary>
        /// 指定パスの存在確認
        /// </summary>
        internal static bool node_exist_pathdir(string pathdir)
        {
            return m_nodedir.CheckExist_pathdir(pathdir);
        }
        /// <summary>
        /// 全パスの取得
        /// </summary>
        internal static List<string> node_get_all_pathdir()
        {
            return m_nodedir.GetAll_pathdir();
        }
        /// <summary>
        /// 指定の表示中のグループノード配下のステート取得
        /// </summary>
        internal static List<string> node_get_allstates_on_groupnode(string group)
        {
            if (group == null) return null;
            return m_nodedir.get_allstates_on_group(group);
        }

        /// <summary>
        /// 指定の表示中のグループノード配下のステート取得
        /// </summary>
        internal static List<string> node_get_allstates_by_dirpath(string dirpath)
        {
            if (string.IsNullOrEmpty(dirpath)) throw new SystemException("Unexpected! {70D54E30-5D3F-4209-9C4B-6DD5896FE3C1}");
            var outlist = new List<string>();
            foreach(var i in G.state_working_list)
            {
                if (AltState.IsAltState(i)) continue; //代替削除

                var ipath = node_get_dirpath(i);
                if (ipath.StartsWith(dirpath))
                {
                    outlist.Add(i);
                }
            }
            return outlist;
        }

        /// <summary>
        /// ノードルートデータ取得
        /// </summary>
        internal static GroupNodeManager.NodeData node_get_rootdata()
        {
            return m_nodedir.GetNodeRoot();
        }

        /// <summary>
        /// ツリー描画用のステートデータ取得
        /// </summary>
        public static void node_get_current_statedrawtreedata(out string highlight_node,out List<string> highlight_states, out List<string> highlight_groups, out List<string> srcdst_states)
        {
            m_nodedir.GetCurrentStateDrawTreeData(out highlight_node,out highlight_states, out highlight_groups, out srcdst_states);
        }

        /// <summary>
        /// ステートの属するパスを取得する
        /// </summary>
        public static string node_get_dirpath(string state)
        {
            return m_nodedir.GetDirPath(state);
        }

        public static string node_get_dirpath_by_groupname(string groupname)
        {
            return m_nodedir.GetDirPath_byGroupname(groupname);
        }

        /// <summary>
        /// ステートがグループに属する場合、そのグループの代替ステートを返す。
        /// </summary>
        public static string node_normalize_state_for_alt(string state)
        {
            return m_nodedir.NormalizeStateForAlt(state);
        }

        /// <summary>
        /// nodedirのポジションを更新
        /// </summary>
        public static void node_save_position()
        {
            m_nodedir.SaveNodePosition();
        }

        #endregion

        #region Notice ユーザに状況を伝える
        public static void NoticeToUser(string text)
        {
            Logging.WriteLine(text);

            if (G.view_form==null || G.view_form.NoticeTextBox==null) return;
            var tb = G.view_form.NoticeTextBox;
            tb.AppendText(text + Environment.NewLine);
            tb.SelectionStart = tb.Text.Length;
            //tb.Focus();
            tb.ScrollToCaret();
        }
        public static void NoticeToUser_woNewLine(string text)
        {
            Logging.Write(text);

            if (G.view_form==null || G.view_form.NoticeTextBox==null) return;
            var tb = G.view_form.NoticeTextBox;
            tb.AppendText(text);
            tb.SelectionStart = tb.Text.Length;
            //tb.Focus();
            tb.ScrollToCaret();
        }
        public static void NoticeToUser(string text, Color col)
        {
            Logging.WriteLine("<color=" + col.ToString() + ">" + text + "</color>");

            if (G.view_form==null || G.view_form.NoticeTextBox==null) return;
            var tb = G.view_form.NoticeTextBox;
            var savecol = tb.SelectionColor;
            {
                tb.SelectionColor = col;
                tb.AppendText(text + Environment.NewLine);
            }
            tb.SelectionColor = savecol;

            tb.SelectionStart = tb.Text.Length;
            //tb.Focus();
            tb.ScrollToCaret();
        }
        public static void NoticeToUser_warning(string text)
        {
            NoticeToUser("Warning:" + text, Color.Red);
        }
        public static void NoticeToUser_msgbox(string text)
        {
            var form = new _6200_msgboxForm.MsgBoxForm();
            form.Text = "Notice";
            form.textBox1.Text = text;
            form.Show(view_form);
        }
        #endregion

        #region 外部コマンド実行

        /*Obsolete*/         internal static bool   use_external_command; 
        /*Obsolete*/         internal static string external_command;

        /// <summary>
        /// ソースエディタ設定
        /// </summary>
        internal static string source_editor_set; 

        internal static string external_source_editor;
        internal static bool source_editor_vs2015_support;
        internal static bool use_batch_for_source_editor_open;
        internal static bool use_cmn_editor;

        /// <summary>
        ///  ソースエディタパスは引数込みなので、パス部分のみを抽出する。
        /// </summary>
        internal static string external_source_editor_path {
            get {
                var s = external_source_editor;
                if (string.IsNullOrEmpty(s)) return string.Empty;

                var indexexe = s.ToLower().IndexOf(".exe");
                if(indexexe < 0) return s;
                var path = s.Substring(0,indexexe + 4);
                path = path.Replace("\"","");
                return path;
            }
        }
        //ユーザボタン
        internal static string userbutton_title;
        internal static string userbutton_command;
        internal static bool   userbutton_callafterconvert;

        #endregion

        #region Label
        public static bool label_show {
            get { return view_form.textBoxLabel.Visible;  }
            set { view_form.textBoxLabel.Visible = value; }
        }
        public static string label_text {
            get { return view_form.textBoxLabel.Text;  }
            set {
                view_form.textBoxLabel.Text = value;
                update_viewform_title();
            }
        }
        #endregion

        #region History

        internal static HistoryUtil history = new HistoryUtil(); 

        #endregion


        #region Embeded Browser Setting
        internal static bool   web_js_disable = false;

        internal static string web_base      = "https://statego.programanic.com/";

        internal static string web_info_js {
            get
            {
                var url = string.Format("https://statego.programanic.com/add/{0}/", milestone);
                return (system_lang == "jpn") ? url + "index-off-j.html" : url + "index-off-e.html";
            }
        }
        

        internal static string web_info { get { return web_js_disable ? "" : web_info_js; } }


        #endregion

        #region Extra button setting
        internal static string but_extra_text;
        internal static string but_extra_cmd;
        #endregion

        #region About Adon message
        internal static string about_addon_text;
        #endregion

        #region FrontEndUI ON/OFF
        public static void frontend_enable(bool b)
        {
            view_form.menuStrip1.Enabled = b;
            view_form.groupBoxUtility.Enabled = b;
            view_form.tabControl.Enabled = b;
        }
        public static bool frontend_enabled()
        {
            return view_form.menuStrip1.Enabled;
        }
        #endregion

        #region SteteMenu追加要素
        public static BranchApiCollector   brancApiCollector;    //Branch API Collector
        public static LinkItemsOnStateMenu linkItemsOnStateMenu; //Link Items on state menu
        #endregion

        public static string current_func_src;
        public static string current_func_template;

        public static void update_viewform_title()
        {
            if (G.view_form!=null)
            {
                var cmt = G.nodegroup_comment_get(G.m_target_pathdir);
                if (!string.IsNullOrEmpty(cmt))
                {
                    cmt = cmt.Replace("\xa","").Replace("\xd","");
                }

                G.view_form.Text = string.Format("StateGo {5} {3} {0} [{1}] - current = {2} ({4})", 
                    (string.IsNullOrEmpty(G.label_text) || G.label_text.ToLower()=="test")  ? "" : "\"" + G.label_text +"\"",
                    //G.load_file_dir_last, 
                    Path.GetFileName( G.psgg_file_w_data ? G.psgg_file :  G.load_file ), 
                    G.m_target_pathdir,    
                    (G.bDirtySmart ? "*" : ""),
                    cmt,
                    G.milestonetxt
                );
            }
        }

        public static void UpdateExcelPos(string state, PointF pos, bool bForce = false)
        {
            if (node_get_style(state) == SS.STYLE.NORMAL || bForce)
            {
                //G.excel_program.SetString(state, STATENAMESYS_pos, pos.X.ToString("F0") + "," + pos.Y.ToString("F0"));
                G.excel_program.SetString(state, STATENAMESYS_pos, PointUtil.ToStringF0_CSV(pos) );
            }
        }

        public static void UpdateExcelPos_w_clamp(string state, PointF pos, bool bForce = false)
        {
            var x = MathX.Clamp(pos.X,0,G.bitmap_width  - G.state_width);
            var y = MathX.Clamp(pos.Y,0,G.bitmap_height - G.state_height);
            var newpos = new PointF(x,y);
            UpdateExcelPos(state,newpos,bForce);
        }

        #region lates focus state
        public static string    latest_focuse_state;
        #endregion

        #region export import
        public static void ExportToClipboard(List<string> statelist, List<string> displist = null)
        {
            CopyAndPasteWork.export(statelist,displist);
        }
        #endregion

        #region ダーティ変数　セーブされていないデータがあるという意味　クローズ時の確認　と　タイトルバーに知らせ
        public static bool bDirty
        {
            get {  return bDirty_by_modified_value || bDirty_by_not_same_inital_group || bDirty_by_edited_pos_only;  }
        }
        public static bool bDirtySmart
        {
            get
            {
                if (G.option_forceclose_ifviewchangeonly)
                {
                    return bDirty_by_modified_value ||bDirty_by_edited_pos_only;  
                }
                else
                {
                    return bDirty;
                }
            }
        }
        public static void Dirty_clear_all()
        {
            bDirty_by_modified_value = bDirty_by_moved_group_only = bDirty_by_edited_pos_only = false;
        }
        public static int Dirty_save()
        {
            var a = bDirty_by_modified_value   ?   1: 0;
            var b = bDirty_by_moved_group_only ?  10: 0;
            var c = bDirty_by_edited_pos_only  ? 100: 0;
            return a + b +c;
        }
        public static void Dirty_restore(int n)
        {
            var a = n % 10;   bDirty_by_modified_value   = a==1;
            n = n - a;  
            var b = n % 100;  bDirty_by_moved_group_only = b == 10;
            var c = n - b;    bDirty_by_edited_pos_only  = c == 100;
        }
        public static bool bDirty_by_modified_value;     //値が変更された

        /*Obsolete*/
        public static bool bDirty_by_moved_group_only;   //見てる画面が移動した  ※廃止

        public static bool bDirty_by_edited_pos_only;    //位置のみ変更された

        public static bool bDirty_by_not_same_inital_group {
            get {
                var initialpath = History2.Get_initial_dirpath();
                var curpath = G.node_get_cur_dirpath();
                return initialpath != curpath;
            }
        }

        #endregion

        #region ローカライズ便利
        public static string Localize(string id)
        {
            return WordStorage.Res.Get(id,G.system_lang);
        }
        #endregion

        #region PSGG FILE W DATA対応
        public static StateViewer_filedb.FileDb file_db;

        // psggのヘッダ情報
        public static string psgg_header_info_version;
        public static string psgg_header_info_file;
        public static string psgg_header_info_guid;
        public static string psgg_header_info_read_from { get {  return psgg_header_info_read_from_excel ? WordStorage.Store.excel : WordStorage.Store.psgg ;  }  }
        public static string psgg_header_info_save_mode;
        public static string psgg_header_info_check_excel_writable;

        public static bool   psgg_header_info_read_from_excel;
        public static bool   psgg_header_info_check_excel_writable_yes;
        public static bool   psgg_header_info_save_mode_withexcel;

        public static bool?  psgg_ask_upgrade = null; //ファイルロード直後のデータバージョンが1.0確認に使用 null時は未チェック
        public static bool   psgg_open_upgrade = false; //アップグレードダイアログを開く

        #endregion

        #region 項目編集用フォームのサイズ管理
        internal static Dictionary<string, Size> itemeditform_size_list;// = new Dictionary<string, Size>();
        #endregion

        #region typ イメージ用

        internal static string decoimage_typ_name = "eScript";     //ステートタイプマークのイメージパス　state-images\typ\[[ここ]]　  ※セーブ対象

        internal static string decoimage_typ_folder
        {
            get { 
                var path = Path.Combine( Path.GetDirectoryName( Application.ExecutablePath), @"state-images\typ");
                System.Diagnostics.Debug.WriteLine(path);
                if (Directory.Exists(path))
                {
                    return path;
                }
#if DEBUG
                path = Path.GetDirectoryName( Application.ExecutablePath );
                var estr = @"\psgg-editor\";
                var eindex = path.IndexOf(estr);
                if (eindex>=0)
                {
                    var path2 = Path.Combine( path.Substring(0,eindex + estr.Length), @"state-images\typ");
                    return path2;                        
                }
#endif
                return null;
            }
        }
        #endregion

        #region 関数テンプレートにstate-typが使用されているか？
        internal static bool template_has_statetyp
        {
            get
            {
                var template = excel_convertsettings?.m_template_func;
                if (!string.IsNullOrEmpty(template))
                {
                    return template.Contains("<<<?"+STATENAME_statetyp);
                }
                return false;
            }
        }
        #endregion

        #region キーボードイベント  ※参照 キーボード操作.pptx
        internal static Keys Key=Keys.None;
        #endregion

        #region フォーカストラックとヒストリレコードパネル
        internal static HistoryRecordPanel m_history_record_panel;
        internal static FocusTrackPanel    m_focus_track_panel;
        #endregion

        #region 手動スクロール
        internal static ScrollControl scroll;
        internal static void Scroll_init() {
            if (scroll==null) scroll = new ScrollControl();
            scroll.m_mode = ScrollControl.Mode.init;
            scroll.Run();
        }
        internal static void Scroll_mpb_changed()
        {
            if (scroll == null) return;
            if (scroll.m_mode != ScrollControl.Mode.none)
            {
                //System.Diagnostics.Debugger.Break();
                return;
            }
            scroll.m_mode = ScrollControl.Mode.mpb_changed;
            scroll.Run();
        }
        internal static void Scroll_moved()
        {
            if (scroll == null) return;
            if (scroll.m_mode != ScrollControl.Mode.none)
            {
                //System.Diagnostics.Debugger.Break();
                return;
            }
            scroll.m_mode = ScrollControl.Mode.scrolled;
            scroll.Run();
        }
        #endregion

        #region コピーコレクション
        internal static CopyCollection cc;

        #endregion

        #region 編集禁止マークの一時的変更
        private static bool m_donotedit_save;
        internal static void donotedit_push(bool b)
        {
            m_donotedit_save = option_use_donotedit_mark;
            option_use_donotedit_mark = b;
        }
        internal static void donotedit_pop()
        {
            option_use_donotedit_mark = m_donotedit_save;
        }
        internal static string CMTLINE_DONOTENDMARK()
        {
            return CommentUtil.make_commentline(option_donotedit_mark);
        }
        #endregion
    }
}
