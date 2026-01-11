package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class ListUtil
{
    public static function IsValidIndex<T>(list:Array<T>, index:Int):Bool
    {
        return list != null && index >= 0 && index < list.length;
    }
    public static function Get(list:Array<String>, i:Int):String
    {
        if (IsValidIndex(list, i))
        {
            return list[i];
        }
        return null;
    }
    public function new()
    {
    }
}
