package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class StateUtil
{
    public static function MakeExtraName(name:String):String
    {
        return system.Cs2Hx.NullCheck(name) + "_ex";
    }
    public static function IsValidStateName(name:String):Bool
    {
        if (system.Cs2Hx.IsNullOrEmpty(name))
        {
            return false;
        }
        return psgg.HxRegexUtil.Get1stMatch("[_a-zA-Z]+_[_a-zA-Z0-9]*", name) == name;
    }
    public function new()
    {
    }
}
