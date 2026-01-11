package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class BranchUtil
{
    public static function Read(branch:String, brcond:String, branch_cmt:String):lib.util.BranchUtil_Item
    {
        var item:lib.util.BranchUtil_Item = new lib.util.BranchUtil_Item();
        if (system.Cs2Hx.IsNullOrEmpty(branch))
        {
            return null;
        }
        item.br_raw_list = lib.util.StringUtil.SplitTrim(branch, lib.util.StringUtil._0a.charCodeAt(0));
        item.br_api_list = new Array<String>();
        item.br_state_list = new Array<String>();
        { //for
            var n:Int = 0;
            while (n < item.br_raw_list.length)
            {
                var s:String = item.br_raw_list[n];
                var api:String = psgg.HxRegexUtil.Get1stMatch("^.+\\(", s);
                api = lib.util.StringUtil.TrimEnd(api, 40);
                item.br_api_list.push(api);
                var bst:String = system.Cs2Hx.TrimStart(psgg.HxRegexUtil.Get1stMatch("\\(.+\\)", s), [ 40 ]);
                bst = lib.util.StringUtil.TrimEnd(bst, 41);
                item.br_state_list.push(bst);
                n++;
            }
        } //end for
        if (!system.Cs2Hx.IsNullOrEmpty(brcond))
        {
            item.br_cond_list = lib.util.StringUtil.SplitTrim(brcond, lib.util.StringUtil._0a.charCodeAt(0));
        }
        if (!system.Cs2Hx.IsNullOrEmpty(branch_cmt))
        {
            item.br_cmt_list = lib.util.StringUtil.SplitTrim(branch_cmt, lib.util.StringUtil._0a.charCodeAt(0));
            { //for
                var n:Int = 0;
                while (n < item.br_cmt_list.length)
                {
                    var a:String = system.Cs2Hx.Trim(item.br_cmt_list[n]);
                    item.br_cmt_list[n] = a == "?" ? "" : a;
                    n++;
                }
            } //end for
        }
        return item;
    }
    public function new()
    {
    }
}
