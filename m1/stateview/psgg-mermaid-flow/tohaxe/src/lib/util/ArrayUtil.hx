package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class ArrayUtil
{
    public static function GetVal(array:Array<String>, index:Int, error:String = null):String
    {
        if (index >= 0 && index < array.length)
        {
            return array[index];
        }
        return error;
    }
    public function new()
    {
    }
}
