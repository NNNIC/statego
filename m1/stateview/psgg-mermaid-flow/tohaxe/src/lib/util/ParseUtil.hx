package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class ParseUtil
{
    public static function ParseInt(s:String, errorvalue:Int = -2147483648):Int
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return errorvalue;
        }
        var ret:CsRef<Int> = new CsRef<Int>(0);
        var retf:CsRef<Float> = new CsRef<Float>(0);
        if (Cs2Hx.TryParseInt(s, ret))
        {
            return ret.Value;
        }
        if (Cs2Hx.TryParseFloat(s, retf))
        {
            return Std.int(retf.Value);
        }
        return errorvalue;
    }
    public static function ParseLong(s:String, errorvalue:Float = -9223372036854775808):Float
    {
        var ret:CsRef<Float> = new CsRef<Float>(0);
        var retf:CsRef<Float> = new CsRef<Float>(0);
        if (Cs2Hx.TryParseFloat(s, ret))
        {
            return ret.Value;
        }
        if (Cs2Hx.TryParseFloat(s, retf))
        {
            return retf.Value;
        }
        return errorvalue;
    }
    public static function ParseFloat(s:String, errorvalue:Float = -3.402823E+38):Float
    {
        var ret:CsRef<Float> = new CsRef<Float>(0);
        if (Cs2Hx.TryParseFloat(s, ret))
        {
            return ret.Value;
        }
        return errorvalue;
    }
    public static function ParseFloatList(s:String, errorvalue:Float = -3.402823E+38):Array<Float>
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        var list:Array<Float> = new Array<Float>();
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ 44 ]);
        for (i in tokens)
        {
            var f:Float = ParseFloat(i, errorvalue);
            list.push(f);
        }
        return list;
    }
    public static function ParseIntList(s:String, errorvalue:Int = -2147483648):Array<Int>
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        var list:Array<Int> = new Array<Int>();
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ 44 ]);
        for (i in tokens)
        {
            var v:Int = ParseInt(i, errorvalue);
            list.push(v);
        }
        return list;
    }
    public static function ParseBool(s:String, error:Bool):Bool
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return error;
        }
        if (system.Cs2Hx.Trim(s).toLowerCase() == "true")
        {
            return true;
        }
        if (system.Cs2Hx.Trim(s).toLowerCase() == "false")
        {
            return false;
        }
        return error;
    }
    public function new()
    {
    }
}
