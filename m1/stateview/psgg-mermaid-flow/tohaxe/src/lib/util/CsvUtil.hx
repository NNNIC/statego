package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class CsvUtil
{
    public static function MakeALine<T>(data:Array<T>):String
    {
        var s:String = "";
        if (data != null)
        {
            for (d in data)
            {
                var p:String = Std.string(d);
                if (system.Cs2Hx.StringContains(p, ","))
                {
                    p = "\"" + system.Cs2Hx.NullCheck(p) + "\"";
                }
                if (!system.Cs2Hx.IsNullOrEmpty(s))
                {
                    s += ",";
                }
                s += p;
            }
        }
        return s;
    }
    public static function GetALine(s:String, error:Dynamic = null):Array<Int>
    {
        if (error != null)
        {
            return lib.util.ParseUtil.ParseIntList(s, (function():Int return error)());
        }
        return lib.util.ParseUtil.ParseIntList(s);
    }
    public static function GetALineString(s:String):Array<String>
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        var list:Array<String> = new Array<String>();
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ 44 ]);
        for (t in tokens)
        {
            var t1:String = system.Cs2Hx.Trim(t);
            list.push(t1);
        }
        return list;
    }
    public static function Get(s:String, index:Int):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        if (index < 0)
        {
            return null;
        }
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ 44 ]);
        if (tokens != null && index < tokens.length)
        {
            return tokens[index];
        }
        return null;
    }
    public static function GetAllRest(s:String, index:Int):String
    {
        if (index < 0)
        {
            return null;
        }
        if (index == 0)
        {
            return s;
        }
        var cmidx:Int = 0;
        var count_index:Int = 0;
        while (count_index < index)
        {
            cmidx = system.Cs2Hx.IndexOfChar(s, 44, cmidx);
            if (cmidx < 0)
            {
                return null;
            }
            cmidx++;
            count_index++;
            if (cmidx >= s.length)
            {
                return null;
            }
        }
        return s.substr(cmidx);
    }
    public function new()
    {
    }
}
