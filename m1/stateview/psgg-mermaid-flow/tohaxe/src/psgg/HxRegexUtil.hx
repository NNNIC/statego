package psgg;
using StringTools;
import system.*;
import anonymoustypes.*;

class HxRegexUtil
{
    public static inline var VARNAME_PATTERN:String = "[_a-zA-Z][_a-zA-Z0-9]+";
    public static function GetVerNamePattern() : String {
        return VARNAME_PATTERN;
    }
    public static function IsMatch(regexstr:String, s:String):Bool {
        var r = new EReg(regexstr,"m");
        return r.match(s);
    }
    public static function Get1stMatch(regexstr:String, s:String):String {
        var r = new EReg(regexstr,"m");
        if (r.match(s)) {
            return r.matched(0);
        }
        return null;
    }
    public static function GetNthMatch(regexstr:String, s:String, n:Int):String {
        var input = s;
        var cnt = 0;
        var r = new EReg(regexstr,"m");
        while(r.match(input)) {
            cnt++;
            if (n == cnt) {
                return r.matched(0);
            }
            input = r.matchedRight();
        }
        return null;
    }

    public function new(){}
}