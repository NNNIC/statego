package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class PathUtil
{
    public static function GetRelativePath(baseUri:String, targetUri:String):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(baseUri) || system.Cs2Hx.IsNullOrEmpty(targetUri))
        {
            return null;
        }
        var _abs:(String -> String) = function (s:String):String
        {
            var s2:String = psgg.HxFile.GetFullPath(s);
            var s3:String = s2;
            if (s3.length > 3 && s3.charCodeAt(s3.length - 1) == 92)
            {
                s3 = s3.substr(0, s3.length - 1);
            }
            return s3;
        }
        ;
        var abs_base_uri:String = _abs(baseUri);
        var abs_target_uri:String = _abs(targetUri);
        if (abs_base_uri.toLowerCase().charCodeAt(0) != abs_target_uri.toLowerCase().charCodeAt(0))
        {
            return targetUri;
        }
        if (system.Cs2Hx.StartsWith(abs_target_uri, abs_base_uri))
        {
            var v:String = abs_target_uri.substr(abs_base_uri.length);
            if (!system.Cs2Hx.IsNullOrEmpty(v))
            {
                if (v.charCodeAt(0) == 92 || v.charCodeAt(0) == 47)
                {
                    v = v.substr(1);
                }
            }
            return v;
        }
        var list_base:Array<String> = PathToList(abs_base_uri);
        var list_target:Array<String> = PathToList(abs_target_uri);
        var latestIndex:Int = 0;
        { //for
            var i:Int = 0;
            while (i < list_target.length)
            {
                var base_v:String = lib.util.ArrayUtil.GetVal(list_base, i);
                var tgt_v:String = lib.util.ArrayUtil.GetVal(list_target, i);
                if (base_v != null && tgt_v != null && base_v == tgt_v)
                {
                    latestIndex = i;
                }
                else
                {
                    break;
                }
                i++;
            }
        } //end for
        var diff:Int = list_target.length - latestIndex;
        var newpath:String = "";
        { //for
            var i:Int = latestIndex + 1;
            while (i < list_base.length)
            {
                if (!system.Cs2Hx.IsNullOrEmpty(newpath))
                {
                    newpath += "\\";
                }
                newpath += "..";
                i++;
            }
        } //end for
        { //for
            var i:Int = latestIndex + 1;
            while (i < list_target.length)
            {
                if (!system.Cs2Hx.IsNullOrEmpty(newpath))
                {
                    newpath += "\\";
                }
                newpath += list_target[i];
                i++;
            }
        } //end for
        return newpath;
    }
    public static inline var TRV_TIMEOUTERROR:String = "::timeout!!";
    public static inline var TRV_EXCEPTION:String = "::exception!!";
    public static inline var TRV_UNKNOWN:String = "::unknown!!";
    public static function PathToList(path:String):Array<String>
    {
        if (system.Cs2Hx.IsNullOrEmpty(path))
        {
            return null;
        }
        return system.Cs2Hx.Split(path, [ 92 ]);
    }
    public static function ListToPath(list:Array<String>):String
    {
        if (list == null)
        {
            return null;
        }
        var s:String = "";
        for (i in list)
        {
            if (!system.Cs2Hx.IsNullOrEmpty(s))
            {
                s += "\\";
            }
            s += i;
        }
        return s;
    }
    public function new()
    {
    }
}
