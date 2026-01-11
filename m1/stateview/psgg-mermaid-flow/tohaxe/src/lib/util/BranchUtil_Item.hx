package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class BranchUtil_Item
{
    public var br_raw_list:Array<String>;
    public var br_api_list:Array<String>;
    public var br_state_list:Array<String>;
    public var br_cond_list:Array<String>;
    public var br_cmt_list:Array<String>;
    public function count():Int
    {
        return br_api_list != null ? br_api_list.length : 0;
    }
    public function get_api(n:Int):String
    {
        var s:String = lib.util.ListUtil.Get(br_api_list, n);
        if (s == null)
        {
            return "";
        }
        if (system.Cs2Hx.StartsWith(s, "brif"))
        {
            return "";
        }
        if (system.Cs2Hx.StartsWith(s, "brelseif"))
        {
            return "";
        }
        if (system.Cs2Hx.StartsWith(s, "brelse"))
        {
            return "else";
        }
        return s;
    }
    public function get_state(n:Int):String
    {
        var s:String = lib.util.ListUtil.Get(br_state_list, n);
        if (s == null)
        {
            return "";
        }
        return s;
    }
    public function get_cond(n:Int):String
    {
        var s:String = lib.util.ListUtil.Get(br_cond_list, n);
        if (s == null)
        {
            return "";
        }
        if (s == "?")
        {
            return "";
        }
        return s;
    }
    public function get_cmt(n:Int):String
    {
        var s:String = lib.util.ListUtil.Get(br_cmt_list, n);
        if (s == null)
        {
            return "";
        }
        return s;
    }
    public function new()
    {
    }
}
