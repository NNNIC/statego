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
    public class SaveLoadIni
    {
        static string __temppath = null;
        static string m_temppath {
            get {
                if (__temppath==null)
                {
                    __temppath = Path.Combine( Path.GetTempPath(),string.Format("~psgged_{0}.ini",G.userenv_guid));
                }
                return __temppath;
            }
        }
        internal static DateTime m_tempsavetime = DateTime.MinValue;

        internal static void SaveTempIni()
        {
            if (G.m_draw_data_list==null) return;
            _save_to_excelsheet(m_temppath);
            m_tempsavetime = DateTime.Now;
        }
        internal static void SaveIni()
        {
            if (G.m_draw_data_list == null)
            {
                G.NoticeToUser_warning( G.Localize("w_nodatawasfoundsaveini") /*"No data was found at save ini."*/);
                return;
            }

            _save_to_excelsheet();

            G.NoticeToUser("Config Save Data was created.");

        }
        internal static bool LoadIni()
        {
            return _load_excel();
        }
        internal static bool LoadTempIni()
        {
            return _load_tmpfile();
        }
        internal static bool LoadIniText(string text)
        {
            try {
                var sd = read_ini(text);
                load_SaveData(sd);
                return true;
            }
            catch
            {
                G.NoticeToUser_warning(G.Localize("w_configsavedatacannotbreadfromtp")/*"Config Save Data cannot be read from tempfile."*/);
                return false;
            }
        }
        private static bool _load_excel()
        {
            try {
                var excel_ini = G.excel_config.m_configini;
                var sd = read_ini(excel_ini);
                if (sd!=null)
                {
                    load_SaveData(sd);
                    return true;
                }
                return false;
            }
            catch
            {
                G.NoticeToUser_warning(G.Localize("w_configsavefromexcel")/*"Config Save Data cannot be read from excel."*/);
                return false;
            }
        }
        private static bool _load_tmpfile()
        {
            try {
                var tmp_ini = File.ReadAllText(m_temppath,Encoding.UTF8);
                var sd = read_ini(tmp_ini);
                load_SaveData(sd);
                return true;
            }
            catch
            {
#if DEBUG
                G.NoticeToUser_warning("[DEBUG]Config Save Data cannot be read from tempfile.");
#endif
                return false;
            }
        }

        private static void _save_to_excelsheet(string filename = null)
        {
            try {
                updatePosition();
                var sd  = store_SaveData();
                var ini = create_ini(sd);
                G.excel_config.m_configini = ini;
                if (!string.IsNullOrEmpty(filename))
                {
                    File.WriteAllText(filename,ini,Encoding.UTF8);
                }
            } catch (SystemException e)
            {
                G.NoticeToUser_warning("Save Exception : " + e.Message);
            }
        }
        private static string create_ini(SaveData sd)
        {
            var nl = new string((char)0x0a,1);
            var s = "; The setting was created automatically. " + DateTime.Now.ToLocalTime() +nl;
            s    += "; * pssgEditor version : " + G.version + "." +G.githash + nl;
            Action<string,string> _add = (a,b) => {
                s += a + "=" + b + nl;
            };
            Action<string,int> _addint = (a,b) => {
                s += a + "=" + b.ToString() + nl;
            };
            Action<string,float> _addfloat = (a,b) => {
                s += a + "=" + b.ToString() + nl;
            };
            Action<string,bool> _addbool = (a,b) => {
                s += a + "=" + (b ? "1" : "0") + nl;
            };
            Action<string,string> _addstr = (a,b)=> {
                s += a + "=@@@"+nl;
                s += b + nl;
                s += "@@@" + nl;
            };
            Action<string,object> _addobj = (a,b)=> {
                var json = string.Empty;
                try {
                    json = JsonUtil.Serialize(b);
                } catch (SystemException e) {
                    G.NoticeToUser_warning("Faild to serialize " + a + " err=" + e.Message);
                }
                _addstr(a,json);
            };


            _addstr("psggfile",sd.psggfile);
            _addstr("xlsfile",sd.xmlfile);
            _addstr("guid", sd.guid);

            _addint("bitmap_width" ,sd.bitmap_width);
            _addint("bitmap_height",sd.bitmap_height);

            _addbool("c_statec_cmt",sd.c_statec_cmt);
            _addbool("c_thumbnail",sd.c_thumbnail);
            _addbool("c_contents",sd.c_contents);

            _addbool("force_display_outpin",sd.force_display_outpin);

            _addstr("last_action",sd.last_action);
            _addstr("target_pathdir",sd.target_pathdir);

            s = _addobj2(s, "state_location_list",sd.state_location_list);

            s = _addobj2(s, "nodegroup_comment_list",sd.nodegroup_comment_list);
            s = _addobj2(s, "nodegroup_pos_list",sd.nodegroup_pos_list);

            s = _addobj2(s, "fillter_state_location_list",sd.fillter_state_location_list);

            s = _addobj2(s, "linecolor_data",sd.linecolor_data);

            _addbool("use_external_command",sd.use_external_command);
            _addstr("external_command",sd.external_command);

            _addstr("source_editor_set",sd.source_editor_set);
            //_addstr("source_editor",sd.source_editor);
            //_addbool("source_editor_vs2015_support",sd.source_editor_vs2015_support);

            _addbool("label_show",sd.label_show);
            _addstr("label_text",sd.label_text);

            _addbool("option_delete_thisstring",sd.option_delete_thisstring);
            _addbool("option_delete_br_string" ,sd.option_delete_br_string);
            _addbool("option_delete_bracket_string"   ,sd.option_delete_bracket_string);
            _addbool("option_delete_s_state_string"   ,sd.option_delete_s_state_string);

            _addbool("option_copy_output_to_clipboard", sd.option_copy_output_to_clipboard);
            _addbool("option_convert_with_confirm", sd.option_convert_with_confirm);
            _addbool("option_ignore_case_of_state", sd.option_ignore_case_of_state);
            //_addbool("option_set_default_comment",  sd.option_set_default_comment);

            _addbool("option_editbranch_automode",sd.option_editbranch_automode);
            _addbool("option_use_custom_prefix",sd.option_use_custom_prefix);
            _addbool("option_omit_basestate_string", sd.option_omit_basestate_string);
            _addbool("option_hide_basestate_contents", sd.option_hide_basestate_contents);
            _addbool("option_hide_branchcmt_onbranchbox",sd.option_hide_branchcmt_onbranchbox);
            //_addbool("option_mrb_enable",sd.option_mrb_enable);

            _addstr("font_name", sd.font_name);
            _addfloat("font_size", sd.font_size);

            _addfloat("comment_font_size",sd.comment_font_size);
            _addfloat("contents_font_size",sd.contents_font_size);

            _addfloat("state_width",sd.state_width);
            _addfloat("state_height",sd.state_height);

            _addfloat("state_short_width",sd.state_short_width);
            _addfloat("state_short_height",sd.state_short_height);

            _addfloat("comment_block_height", sd.comment_block_height);
            _addfloat("content_max_height", sd.content_max_height);
            _addbool("comment_block_fixed",sd.comment_block_fixed);
            _addfloat("line_space", sd.line_space);

            _addstr("userbutton_title", sd.userbutton_title);
            _addstr("userbutton_command", sd.userbutton_command);
            _addbool("userbutton_callafterconvert", sd.userbutton_callafterconvert);

            s = _addobj2(s,"itemeditform_size_list", sd.itemeditform_size_list);

            _addstr("decoimage_typ_name",sd.decoimage_typ_name);

            //編集不可マーク
            _addbool("use_donotedit_mark", sd.use_donotedit_mark);
            _addstr( "donotedit_mark_columns", sd.donotedit_mark_columns);
            _addstr( "donotedit_mark", sd.donotedit_mark);

            return s;
        }

        private static string _addstr2(string s, string a, string b) {
            var nl = System.Environment.NewLine;
            s += a + "=@@@"+nl;
            s += b + nl;
            s += "@@@" + nl;
            return s;
        }
        private static string _addobj2<T>(string s,string a, T b) {
            var json = string.Empty;
            try {
                json = JsonUtil.Serialize(b);
            } catch (SystemException e) {
                G.NoticeToUser_warning("#Faild to serialize " + a + " err=" + e.Message + " if debuggin, ignore this.");
            }
            s = _addstr2(s, a,json);
            return s;
         }


        private static SaveData read_ini(string s)
        {
            //System.Diagnostics.Debugger.Launch();

            if (string.IsNullOrEmpty(s)) return null;

            var ht = IniUtil.CreateHashtable(s);

            if (ht==null) return null;
            if (ht.Keys.Count==0) return null;

            Func<string,string> _get = (a)=> {
                return IniUtil.GetValueFromHashtable(a,ht);
            };

             Func<string,string,string> _getwerr = (a,err)=> {
                    var v = IniUtil.GetValueFromHashtable(a,ht);
                    if (string.IsNullOrEmpty(v))
                    {
                        return err;
                    }
                    return v;
            };

            Func<string,int> _getint = (a)=> {
                var v = _get(a);
                return ParseUtil.ParseInt(v,int.MinValue);
            };
            Func<string,float> _getfloat = (a)=> {
                var v = _get(a);
                return ParseUtil.ParseFloat(v,int.MinValue);
            };
            Func<string,float,float> _getfloatwerr = (a,err)=> {
                var v = _get(a);
                return ParseUtil.ParseFloat(v,err);
            };
            Func<string, bool> _getbool = (a) => {
                var v = _get(a);
                return v == "1";
            };
            Func<string,bool,bool> _getboolwErr = (a,errorval) => {
                var v = _get(a);
                if (v==null) return errorval;
                return v == "1";
            };
            Func<string,Type,bool,object> _getobj = (a,type, bIgnoreError) => {
                try {
                    var v = _get(a);
                    return JsonUtil.Deserialize(v,type);
                } catch {
                    var msg = string.Format( "{0} " + G.Localize("w_inicouldnotread")  /*"{0} in ini could not be read."*/, a);
                    if (bIgnoreError)
                    {
                        G.NoticeToUser("Ignoring the error : " + msg);
                    }
                    else
                    {
                        G.NoticeToUser_warning(msg);
                    }
                    return null;
                }
            };

            var sd = new SaveData();

            sd.psggfile                    = _get("psggfile");
            sd.xmlfile                     = _get("xlsfile");
            sd.guid                        = _get("guid");
            sd.bitmap_width                = _getint("bitmap_width");
            sd.bitmap_height               = _getint("bitmap_height");

            sd.c_statec_cmt                = _getbool("c_statec_cmt");
            sd.c_thumbnail                 = _getbool("c_thumbnail");
            sd.c_contents                  = _getbool("c_contents");

            sd.force_display_outpin        = _getbool("force_display_outpin");

            sd.last_action                 = _get("last_action");
            sd.target_pathdir              = _get("target_pathdir");

            sd.state_location_list         = (Dictionary<string, PointF>)_getobj("state_location_list", typeof(Dictionary<string, PointF>),true);

            sd.nodegroup_comment_list      = (Dictionary<string,string>)_getobj("nodegroup_comment_list", typeof(Dictionary<string,string>),true);
            sd.nodegroup_pos_list = (Dictionary<string,Point>)_getobj("nodegroup_pos_list", typeof(Dictionary<string,Point>),true);

            sd.fillter_state_location_list = (Dictionary<string, Dictionary<string, PointF> >)_getobj("fillter_state_location_list",typeof(Dictionary<string, Dictionary<string, PointF> >),false);

            sd.linecolor_data              = (List<LineColor.Item>)_getobj("linecolor_data",typeof(List<LineColor.Item>),true);

            sd.use_external_command        = _getbool("use_external_command");
            sd.external_command            = _get("external_command");

            sd.source_editor_set           = _get("source_editor_set");

            //sd.source_editor               = _get("source_editor");
            //sd.source_editor_vs2015_support= _getbool("source_editor_vs2015_support");

            sd.label_show                  = _getbool("label_show");
            sd.label_text                  = _get("label_text");

            sd.option_delete_thisstring    = _getbool("option_delete_thisstring");
            sd.option_delete_br_string     = _getbool("option_delete_br_string");
            sd.option_delete_bracket_string       = _getbool("option_delete_bracket_string");
            sd.option_delete_s_state_string       = _getbool("option_delete_s_state_string");

            sd.option_copy_output_to_clipboard    = _getbool("option_copy_output_to_clipboard");
            sd.option_convert_with_confirm        = _getboolwErr("option_convert_with_confirm",G.option_convert_with_confirm);
            sd.option_ignore_case_of_state        = _getbool("option_ignore_case_of_state");
            //sd.option_set_default_comment         = _getboolwErr("option_set_default_comment",true);

            sd.option_editbranch_automode         = _getbool("option_editbranch_automode");
            sd.option_use_custom_prefix           = _getbool("option_use_custom_prefix");
            sd.option_omit_basestate_string       = _getbool("option_omit_basestate_string");
            sd.option_hide_basestate_contents     = _getbool("option_hide_basestate_contents");
            sd.option_hide_branchcmt_onbranchbox  = _getbool("option_hide_branchcmt_onbranchbox");
            //sd.option_mrb_enable                  = _getboolwErr("option_mrb_enable",G.option_mrb_enable);

            sd.font_name                   = _getwerr("font_name",G.font_name);
            sd.font_size                   = _getfloatwerr("font_size",G.font_size);

            sd.comment_font_size           = _getfloatwerr("comment_font_size",G.comment_font_size);
            sd.contents_font_size          = _getfloatwerr("contents_font_size",G.contents_font_size);

            sd.state_width                 = _getfloatwerr("state_width",G.state_width);
            sd.state_height                = _getfloatwerr("state_height",G.state_height);

            sd.state_short_width           = _getfloatwerr("state_short_width", G.state_short_width);
            sd.state_short_height          = _getfloatwerr("state_short_height", G.state_short_height);

            sd.comment_block_height        = _getfloatwerr("comment_block_height",G.comment_block_height);
            sd.comment_block_fixed         = _getboolwErr("comment_block_fixed",G.comment_block_fixed);
            sd.content_max_height          = _getfloatwerr("content_max_height",G.content_max_height);
            sd.line_space                  = _getfloatwerr("line_space",G.line_space);

            sd.userbutton_title            = _get("userbutton_title");
            sd.userbutton_command          = _get("userbutton_command");
            sd.userbutton_callafterconvert = _getbool("userbutton_callafterconvert");

            sd.decoimage_typ_name          = _getwerr("decoimage_typ_name","sym");

            sd.itemeditform_size_list      = (Dictionary<string,Size>)_getobj("itemeditform_size_list",typeof(Dictionary<string,Size>),true);

            //編集不可マーク
            sd.use_donotedit_mark          = _getboolwErr("use_donotedit_mark",false);
            sd.donotedit_mark_columns      = _getwerr("donotedit_mark_columns", "76,116,136");
            sd.donotedit_mark              = _getwerr("donotedit_mark", "*DoNotEdit*");

            return sd;
        }
        private static SaveData store_SaveData()
        {
            var sd = new SaveData();

            sd.psggfile      = Path.GetFileName(G.psgg_file);
            sd.xmlfile       = Path.GetFileName(G.load_file);
            sd.guid          = G.guid;
            sd.bitmap_width  = G.bitmap_width   ;
            sd.bitmap_height = G.bitmap_height  ;

            sd.c_statec_cmt  = G.use_statecmt   ;
            sd.c_thumbnail   = G.use_thumbnail  ;
            sd.c_contents    = G.use_contents   ;

            sd.force_display_outpin = G.force_display_outpin;

            //sd.max_num_of_states = G.max_num_of_states;
            //sd.fillter_text      = G.fillter_text;

            sd.last_action                 = G.last_action;
            sd.target_pathdir              = G.m_target_pathdir;

            sd.state_location_list         = G.state_location_list;
            sd.nodegroup_comment_list      = G.nodegroup_comment_list_get();
            sd.nodegroup_pos_list          = G.nodegroup_pos_list_get();
            sd.fillter_state_location_list = G.fillter_state_location_list;
            //sd.config_line_data            = G.config_line_data;
            sd.linecolor_data              = G.line_color.m_itemlist;

            sd.use_external_command        = G.use_external_command;
            sd.external_command            = G.external_command;
            //sd.source_editor               = G.external_source_editor;
            //sd.source_editor_vs2015_support= G.source_editor_vs2015_support;
            sd.source_editor_set            = G.source_editor_set;

            sd.label_show                  = G.label_show;
            sd.label_text                  = G.label_text;

            sd.option_delete_thisstring    = G.option_delete_thisstring;
            sd.option_delete_br_string     = G.option_delete_br_string;
            sd.option_delete_bracket_string       = G.option_delete_bracket_string;
            sd.option_delete_s_state_string       = G.option_delete_s_state_string;

            sd.option_copy_output_to_clipboard    = G.option_copy_output_to_clipboard;
            sd.option_convert_with_confirm        = G.option_convert_with_confirm;
            sd.option_ignore_case_of_state        = G.option_ignore_case_of_state; 
            //sd.option_set_default_comment         = G.option_set_default_comment;

            sd.option_editbranch_automode         = G.option_editbranch_automode;
            sd.option_use_custom_prefix           = G.option_use_custom_prefix;
            sd.option_omit_basestate_string       = G.option_omit_basestate_string;
            sd.option_hide_basestate_contents     = G.option_hide_basestate_contents;
            sd.option_hide_branchcmt_onbranchbox  = G.option_hide_branchcmt_onbranchbox;
            //sd.option_mrb_enable                  = G.option_mrb_enable;

            sd.font_name                   = G.font_name;
            sd.font_size                   = G.font_size;

            sd.comment_font_size           = G.comment_font_size;
            sd.contents_font_size          = G.contents_font_size;

            sd.state_width                 = G.state_width;
            sd.state_height                = G.state_height;

            sd.state_short_width           = G.state_short_width;
            sd.state_short_height          = G.state_short_height;

            sd.comment_block_height        = G.comment_block_height;
            sd.content_max_height          = G.content_max_height;
            sd.comment_block_fixed         = G.comment_block_fixed;
            sd.line_space                  = G.line_space;

            sd.userbutton_title            = G.userbutton_title;
            sd.userbutton_command          = G.userbutton_command;
            sd.userbutton_callafterconvert = G.userbutton_callafterconvert;

            sd.itemeditform_size_list      = G.itemeditform_size_list;

            sd.decoimage_typ_name          = G.decoimage_typ_name;

            //編集不可マーク
            sd.use_donotedit_mark          = G.option_use_donotedit_mark;
            sd.donotedit_mark_columns      = G.option_donotedit_mark_columns;
            sd.donotedit_mark              = G.option_donotedit_mark;

            return sd;
        }
        private static void load_SaveData(SaveData sd)
        {
            G.loaded_data                 = sd;

            //反映
            G.guid                        = sd.guid;

            G.bitmap_width                = sd.bitmap_width;
            G.bitmap_height               = sd.bitmap_height;

            G.use_statecmt                = sd.c_statec_cmt;
            G.use_thumbnail               = sd.c_thumbnail;
            G.use_contents                = sd.c_contents;

            //G.use_excel_color             = sd.useExcelColor;

            G.force_display_outpin        = sd.force_display_outpin;

            //G.max_num_of_states = sd.max_num_of_states;

            G.fillter_text                = sd.fillter_text;

            G.last_action                 = sd.last_action;
            G.m_target_pathdir            = sd.target_pathdir;

            G.state_location_list         = sd.state_location_list;

            G.nodegroup_comment_list_set(sd.nodegroup_comment_list);
            G.nodegroup_pos_list_set(sd.nodegroup_pos_list);

            G.fillter_state_location_list = sd.fillter_state_location_list;

            //G.dirform_layout_items        = new _5150_DirForm.DirFormLayout.Item[] { };

            //G.config_line_data            = sd.config_line_data;
            G.line_color.m_itemlist       = sd.linecolor_data;

            G.use_external_command        = sd.use_external_command;
            G.external_command            = sd.external_command;

            //G.external_source_editor      = sd.source_editor;
            //G.source_editor_vs2015_support= sd.source_editor_vs2015_support;

            G.source_editor_set           = sd.source_editor_set;

            G.label_show                  = sd.label_show;
            G.label_text                  = sd.label_text;

            G.option_delete_thisstring    = sd.option_delete_thisstring;
            G.option_delete_br_string     = sd.option_delete_br_string;
            G.option_delete_bracket_string       = sd.option_delete_bracket_string;
            G.option_delete_s_state_string       = sd.option_delete_s_state_string;

            G.option_copy_output_to_clipboard    = sd.option_copy_output_to_clipboard;
            G.option_convert_with_confirm        = sd.option_convert_with_confirm;
            G.option_ignore_case_of_state        = sd.option_ignore_case_of_state;

            //G.option_set_default_comment         = sd.option_set_default_comment;

            G.option_use_custom_prefix           = sd.option_use_custom_prefix;

            G.option_editbranch_automode         = sd.option_editbranch_automode;
            G.option_use_custom_prefix           = sd.option_use_custom_prefix;
            G.option_omit_basestate_string       = sd.option_omit_basestate_string;
            G.option_hide_basestate_contents     = sd.option_hide_basestate_contents;
            G.option_hide_branchcmt_onbranchbox  = sd.option_hide_branchcmt_onbranchbox;
            //G.option_mrb_enable                  = sd.option_mrb_enable;

            G.font_name                   = sd.font_name;
            G.font_size                   = sd.font_size;

            G.comment_font_size           = sd.comment_font_size;
            G.contents_font_size          = sd.contents_font_size;

            G.state_width                 = sd.state_width;
            G.state_height                = sd.state_height;

            G.state_short_width           = sd.state_short_width;
            G.state_short_height          = sd.state_short_height;

            G.comment_block_height        = sd.comment_block_height;
            G.comment_block_fixed         = sd.comment_block_fixed;
            G.content_max_height          = sd.content_max_height;
            G.line_space                  = sd.line_space;

            G.userbutton_title            = sd.userbutton_title;
            G.userbutton_command          = sd.userbutton_command;
            G.userbutton_callafterconvert = sd.userbutton_callafterconvert;

            G.itemeditform_size_list      = sd.itemeditform_size_list;

            G.decoimage_typ_name          = sd.decoimage_typ_name;

            //編集不可マーク
            G.option_use_donotedit_mark     = sd.use_donotedit_mark;
            G.option_donotedit_mark_columns = sd.donotedit_mark_columns;
            G.option_donotedit_mark         = sd.donotedit_mark;
        }
        private static void updatePosition()
        {
            Dictionary<string,PointF> dic = null;
            if (G.fillter_state_location_list!=null && G.fillter_state_location_list.ContainsKey(G.node_get_cur_dirpath()))
            {
                dic = G.fillter_state_location_list[G.node_get_cur_dirpath()];
            }
            else
            {
                dic = new Dictionary<string,PointF>();
            }

            foreach(var p in  G.m_draw_data_list)
            {
                var state = p.Key;
                var d = p.Value;
                if (d==null) continue;
                if (dic.ContainsKey(state))
                {
                    dic[state] = d.offset;
                }
                else
                {
                    dic.Add(state,d.offset);
                }
            }
            //ノード対応
            {
                var key = G.node_get_cur_dirpath();
                if (G.fillter_state_location_list==null) G.fillter_state_location_list = new Dictionary<string, Dictionary<string, PointF>>();
                if (G.fillter_state_location_list.ContainsKey(key))
                {
                    G.fillter_state_location_list[key] = dic;
                }
                else
                {
                    G.fillter_state_location_list.Add(key,dic);
                }
            }

            G.log+="Update Location"+ Environment.NewLine;
        }

    }
}
