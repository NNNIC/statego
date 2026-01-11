package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class PsggDataFileUtil_Item
{
    public var m_header_buf:String;
    public var m_chart_buf:String;
    public var m_config_buf:String;
    public var m_tmpsrc_buf:String;
    public var m_tmpfnc_buf:String;
    public var m_setting_buf:String;
    public var m_help_buf:String;
    public var m_iteminf_buf:String;
    public var m_bitmap_buf:String;
    public var m_tmpsrc:String;
    public var m_tmpfnc:String;
    public function get_header(key:String):String
    {
        var v:Dynamic = lib.util.IniUtil.GetValue(key, m_header_buf);
        return (v != null ? v.toString() : null);
    }
    public function get_config(key:String):String
    {
        var v:Dynamic = lib.util.IniUtil.GetValue(key, m_config_buf);
        return (v != null ? v.toString() : null);
    }
    public function get_setting_String_String(group:String, key:String):String
    {
        return lib.util.IniUtil.GetValue_String_String_String(group, key, m_setting_buf);
    }
    public function get_setting(group:String):Dynamic
    {
        return lib.util.IniUtil.GetValue(group, m_setting_buf);
    }
    public function get_help(group:String, key:String):String
    {
        return lib.util.IniUtil.GetValue_String_String_String(group, key, m_help_buf);
    }
    public function get_iteminf(group:String, key:String):String
    {
        return lib.util.IniUtil.GetValue_String_String_String(group, key, m_iteminf_buf);
    }
    private var m_chart_ht:system.collections.generic.Dictionary<String, Dynamic>;
    private var m_chart_state_list:Array<String>;
    private var m_chart_name_list:Array<String>;
    private var m_state_dic:system.collections.generic.Dictionary<String, String>;
    private var m_name_dic:system.collections.generic.Dictionary<String, String>;
    private function chart_init():Void
    {
        if (m_chart_ht != null)
        {
            return;
        }
        m_chart_ht = lib.util.IniUtil.CreateHashtable(m_chart_buf);
        m_chart_state_list = get_staterow_list();
        m_chart_name_list = get_namecol_list();
        m_state_dic = new system.collections.generic.Dictionary<String, String>();
        var stateids:Array<String> = get_stateid_list();
        for (id in stateids)
        {
            var s:String = get_chart_String_String("id_state_dic", id);
            if (!system.Cs2Hx.IsNullOrEmpty(s))
            {
                m_state_dic.Add(s, id);
            }
        }
        m_name_dic = new system.collections.generic.Dictionary<String, String>();
        var nameids:Array<String> = get_nameid_list();
        for (id in nameids)
        {
            var n:String = get_chart_String_String("id_name_dic", id);
            if (!system.Cs2Hx.IsNullOrEmpty(n))
            {
                m_name_dic.Add(n, id);
            }
        }
    }
    private static inline var NAME_COL:Int = 2;
    private static inline var STATE_ROW:Int = 2;
    private static inline var START_COL:Int = 3;
    public function get_chart_val(row:Int, col:Int):String
    {
        chart_init();
        var state:String = lib.util.ListUtil.Get(m_chart_state_list, col - 1);
        var name:String = lib.util.ListUtil.Get(m_chart_name_list, row - 1);
        if (row == STATE_ROW)
        {
            return state;
        }
        if (col == NAME_COL)
        {
            return name;
        }
        var v:String = get_val(state, name);
        return v;
    }
    public function get_chart_String_String(group:String, key:String):String
    {
        chart_init();
        return lib.util.IniUtil.GetValueFromHashtable_String_String_DictionaryStringObject(group, key, m_chart_ht);
    }
    public function get_chart(key:String):String
    {
        chart_init();
        var v:Dynamic = lib.util.IniUtil.GetValueFromHashtable(key, m_chart_ht);
        return v != null ? v.toString() : null;
    }
    private function get_staterow_list():Array<String>
    {
        var val:String = get_chart("stateid_list");
        var id_list:Array<String> = lib.util.CsvUtil.GetALineString(val);
        var state_list:Array<String> = new Array<String>();
        for (i in id_list)
        {
            var v:String = "";
            if (!system.Cs2Hx.IsNullOrEmpty(i))
            {
                v = get_chart_String_String("id_state_dic", i);
            }
            state_list.push(v);
        }
        return state_list;
    }
    private function get_namecol_list():Array<String>
    {
        var val:String = get_chart("nameid_list");
        var id_list:Array<String> = lib.util.CsvUtil.GetALineString(val);
        var name_list:Array<String> = new Array<String>();
        for (i in id_list)
        {
            var v:String = "";
            if (!system.Cs2Hx.IsNullOrEmpty(i))
            {
                v = get_chart_String_String("id_name_dic", i);
            }
            name_list.push(v);
        }
        return name_list;
    }
    private function get_stateid_list():Array<String>
    {
        var val:String = get_chart("stateid_list");
        var list:Array<String> = lib.util.CsvUtil.GetALineString(val);
        return list;
    }
    private function get_nameid_list():Array<String>
    {
        var val:String = get_chart("nameid_list");
        var list:Array<String> = lib.util.CsvUtil.GetALineString(val);
        return list;
    }
    private function get_val(state:String, name:String):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(state) || system.Cs2Hx.IsNullOrEmpty(name))
        {
            return null;
        }
        var sid:String = lib.util.DictionaryUtil.Get(m_state_dic, state);
        var nid:String = lib.util.DictionaryUtil.Get(m_name_dic, name);
        if (nid == null || sid == null)
        {
            return null;
        }
        var v:String = get_chart_String_String(sid, nid);
        return v;
    }
    public function GetVal(state:String, name:String):String
    {
        chart_init();
        return get_val(state, name);
    }
    public function GetAllStates():Array<String>
    {
        chart_init();
        var list:Array<String> = new Array<String>();
        for (k in m_state_dic.Keys)
        {
            list.push(k);
        }
        return list;
    }
    public function GetAllNames():Array<String>
    {
        chart_init();
        var list:Array<String> = new Array<String>();
        for (k in m_name_dic.Keys)
        {
            list.push(k);
        }
        return list;
    }
    public function GetGeneratedSource(doc_path:String):String
    {
        var gensrc:String = get_setting_String_String(lib.wordstrage.Store.settingini_group_setting, lib.wordstrage.Store.settingini_setting_gensrc);
        var genrdir:String = get_setting_String_String(lib.wordstrage.Store.settingini_group_setupinfo, lib.wordstrage.Store.settingini_setupinfo_genrdir);
        var path2:String = psgg.HxFile.Combine_String_String_String(doc_path, genrdir, gensrc);
        var path3:String = psgg.HxFile.GetFullPath(path2);
        return path3;
    }
    public function GetGenDir(doc_path:String):String
    {
        return psgg.HxFile.GetDirectoryName(GetGeneratedSource(doc_path));
    }
    public function GetGeneratedSourceFileName():String
    {
        var gensrc:String = get_setting_String_String(lib.wordstrage.Store.settingini_group_setting, lib.wordstrage.Store.settingini_setting_gensrc);
        return gensrc;
    }
    public function GetIncDir(doc_path:String):String
    {
        var rdir:String = get_setting_String_String(lib.wordstrage.Store.settingini_group_setupinfo, lib.wordstrage.Store.settingini_setupinfo_incrdir);
        if (system.Cs2Hx.IsNullOrEmpty(rdir))
        {
            return null;
        }
        var path2:String = psgg.HxFile.Combine_String_String(doc_path, rdir);
        return psgg.HxFile.GetFullPath(path2);
    }
    public function GetCodeOutputStart():String
    {
        return get_setting_String_String(lib.wordstrage.Store.settingini_group_setupinfo, lib.wordstrage.Store.settingini_setupinfo_code_output_start);
    }
    public function GetCodeOutputEnd():String
    {
        return get_setting_String_String(lib.wordstrage.Store.settingini_group_setupinfo, lib.wordstrage.Store.settingini_setupinfo_code_output_end);
    }
    public function GetSrcEnc():String
    {
        return get_setting_String_String(lib.wordstrage.Store.settingini_group_setting, lib.wordstrage.Store.settingini_setting_src_enc);
    }
    public function GetStatemachine():String
    {
        return get_setting_String_String(lib.wordstrage.Store.settingini_group_setupinfo, lib.wordstrage.Store.settingini_setupinfo_statemachine);
    }
    var m_editor_name_list:Array<String>;
    var m_editor_row_list:Array<Int>;
    var m_editor_state_list:Array<String>;
    var m_editor_col_list:Array<Int>;
    function editor_list_init():Void
    {
        chart_init();
        if (m_editor_name_list != null)
        {
            return;
        }
        m_editor_name_list = new Array<String>();
        m_editor_row_list = new Array<Int>();
        var row:Int = 0;
        while (row <= 1000)
        {
            row++;
            var s:String = get_chart_val(row, NAME_COL);
            if (s == null)
            {
                continue;
            }
            s = system.Cs2Hx.Trim(s);
            if (system.Cs2Hx.IsNullOrEmpty(s))
            {
                continue;
            }
            var c:Int = (s.toLowerCase()).charCodeAt(0);
            if (c == 33 || c == 95 || (c >= 97 && c <= 122))
            {
                m_editor_name_list.push(s);
                m_editor_row_list.push(row);
            }
        }
        m_editor_state_list = new Array<String>();
        m_editor_col_list = new Array<Int>();
        var col:Int = START_COL - 1;
        while (col <= 10000)
        {
            col++;
            var s:String = get_chart_val(STATE_ROW, col);
            if (s == null)
            {
                continue;
            }
            s = system.Cs2Hx.Trim(s);
            if (system.Cs2Hx.IsNullOrEmpty(s))
            {
                continue;
            }
            if (lib.util.StateUtil.IsValidStateName(s))
            {
                m_editor_state_list.push(s);
                m_editor_col_list.push(col);
            }
        }
    }
    public function GetNameList():Array<String>
    {
        editor_list_init();
        return m_editor_name_list;
    }
    public function GetNameRowList():Array<Int>
    {
        editor_list_init();
        return m_editor_row_list;
    }
    public function GetStateList():Array<String>
    {
        editor_list_init();
        return m_editor_state_list;
    }
    public function GetStateColList():Array<Int>
    {
        editor_list_init();
        return m_editor_col_list;
    }
    public function new()
    {
    }
}
