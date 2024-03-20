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
    [Serializable]
    public class SaveData
    {
        public string psggfile; // PSGG ファイル名
        public string xmlfile;  //エクセルファイル名

        public string  guid;   //一時フォルダ用　※履歴で使用

        public int    bitmap_width;
        public int    bitmap_height;

        public bool   c_statec_cmt;
        public bool   c_thumbnail;
        public bool   c_contents;

        [Obsolete]
        public bool   useExcelColor;

        public bool   force_display_outpin;


        public int    max_num_of_states;

        public string last_action;    //最新の行動 ※ヒストリに利用
        public string target_pathdir; //現在注目中の場所

        public Dictionary<string, PointF> state_location_list;

        public Dictionary<string,string>  nodegroup_comment_list;
        public Dictionary<string, Point>  nodegroup_pos_list;

        #region Fillter
        /*Obsolete*/
        public string fillter_text;

        public Dictionary<string, Dictionary<string, PointF> > fillter_state_location_list;
        #endregion

        //public ConfigLineData config_line_data;
        public List<LineColor.Item> linecolor_data;

        //public _5150_DirForm.DirFormLayout.Item[] dirform_layout_items;

        public bool   use_external_command;
        public string external_command;
        //public string source_editor;
        //public bool   source_editor_vs2015_support;
        public string source_editor_set; //ソースエディタ設定名　defaultはnullまたはEmpty


        public bool   label_show;
        public string label_text;

        public bool option_delete_thisstring; //表示時にthis.を削除する
        public bool option_delete_br_string;  //表示時にbr_を削除する
        public bool option_delete_bracket_string;    //表示時に[]を削除する
        public bool option_delete_s_state_string;    //表示時にS_を削除する
        public bool option_omit_basestate_string;    //ステートに表示するベースステート名を省略する
        public bool option_hide_basestate_contents;  //ベースステートのコンテンツを非表示にする
        public bool option_hide_branchcmt_onbranchbox; //ステートボックスのブランチ部分にコメントを表示しない 

        public bool option_copy_output_to_clipboard; //変換結果をクリップボードへコピー
        public bool option_convert_with_confirm;     //変換時に確認
        public bool option_ignore_case_of_state;     //ステート名の大文字小文字を区別しない
        //public bool option_set_default_comment;    //ステート生成時にコメントを生成する。 →レジストリへ

        public bool option_editbranch_automode; //分岐編集にて、IF/ELSE IF/ELSEの表示を自動化

        public bool option_use_custom_prefix;   //ステート名の先頭文字をカスタム化

        //public bool option_mrb_enable;          //マウス右ボタンでメニュー開く   セーブ対象から外す。 レジストリへ

        public string font_name;
        public float  font_size;

        public float  comment_font_size;
        public float  contents_font_size;

        public float  state_width;
        public float  state_height;

        public float  state_short_width;
        public float  state_short_height;

        public float  comment_block_height;
        public bool   comment_block_fixed;
        public float  content_max_height;
        public float  line_space;

        #region user button
        public string userbutton_title;
        public string userbutton_command;
        public bool userbutton_callafterconvert;
        #endregion

        public Dictionary<string,Size> itemeditform_size_list;

        public string decoimage_typ_name;

        #region 編集不可マーク
        public bool   use_donotedit_mark;
        public string donotedit_mark_columns;
        public string donotedit_mark;
        #endregion

        #region 特別な条件
        public string special_condition;
        #endregion
    }
}
