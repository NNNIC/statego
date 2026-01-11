package lib.util;
using StringTools;
import system.*;
import anonymoustypes.*;

class StringUtil
{
    public static inline var _0d0a:String = "\x0d\x0a";
    public static inline var _0a:String = "\x0a";
    public static function SplitTrim(s:String, separator:Int):Array<String>
    {
        if (s == null)
        {
            return null;
        }
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ separator ]);
        var list:Array<String> = new Array<String>();
        for (t in tokens)
        {
            var t2:String = system.Cs2Hx.Trim(t);
            list.push(t2);
        }
        return list;
    }
    public static function SplitTrimEnd(s:String, separator:Int):Array<String>
    {
        if (s == null)
        {
            return null;
        }
        var tokens:Array<String> = system.Cs2Hx.Split(s, [ separator ]);
        var list:Array<String> = new Array<String>();
        for (t in tokens)
        {
            var t2:String = system.Cs2Hx.TrimEnd(t);
            list.push(t2);
        }
        return list;
    }
    public static function SplitTrimKeepSpace(s:String, separator:Int):Array<String>
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            var p:Array<String> = new Array<String>();
            p.push("");
            return p;
        }
        var lines:Array<String> = system.Cs2Hx.Split(s, [ separator ]);
        var outlines:Array<String> = new Array<String>();
        for (l in lines)
        {
            var nl:String = system.Cs2Hx.Trim_(l, [ 13, 10 ]);
            outlines.push(nl);
        }
        return outlines;
    }
    public static function CutEmptyLines(s:String):String
    {
        if (s == null)
        {
            return null;
        }
        var newlinechar:String = FindNewLineChar(s);
        if (newlinechar == null)
        {
            return s;
        }
        var tokens:Array<String> = SplitTrimEnd(s, 10);
        var list:Array<String> = new Array<String>();
        for (t in tokens)
        {
            if (Cs2Hx.IsNullOrWhiteSpace(t))
            {
                continue;
            }
            list.push(t);
        }
        var o:String = "";
        system.Cs2Hx.ForEach(list, function (i:String):Void
        {
            if (!system.Cs2Hx.IsNullOrEmpty(o))
            {
                o += newlinechar;
            }
            o += i;
        }
        );
        return o;
    }
    public static function CutEmptyLines_ListString(src:Array<String>):Array<String>
    {
        if (src == null)
        {
            return null;
        }
        var list:Array<String> = new Array<String>();
        for (l in src)
        {
            if (Cs2Hx.IsNullOrWhiteSpace(l))
            {
                continue;
            }
            list.push(l);
        }
        return list;
    }
    public static function ConvertNewLineForExcel(s:String):String
    {
        if (s != null)
        {
            if (FindNewLineChar(s) == _0d0a)
            {
                return s.replace(_0d0a, _0a);
            }
        }
        return s;
    }
    public static function FindNewLineChar(s:String):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        if (system.Cs2Hx.StringContains(s, _0d0a))
        {
            return _0d0a;
        }
        if (system.Cs2Hx.StringContains(s, _0a))
        {
            return _0a;
        }
        return null;
    }
    public static function ConverNewLineCharForDisplay(s:String):String
    {
        if (s != null)
        {
            var srcnl:String = FindNewLineChar(s);
            if (srcnl == _0a)
            {
                return s.replace(_0a, _0d0a);
            }
        }
        return s;
    }
    public static function FindMatchedLines(lines:Array<String>, firstmatch:String, endmatch:String, firstline:CsRef<Int>):Array<String>
    {
        firstline.Value = -1;
        if (lines == null)
        {
            return null;
        }
        var result:Array<String> = new Array<String>();
        var bFirstMatchDone:Bool = false;
        var index:Int = -1;
        while (true)
        {
            index++;
            if (index >= lines.length)
            {
                break;
            }
            var line:String = lines[index];
            if (!bFirstMatchDone)
            {
                if (system.Cs2Hx.StringContains(line, firstmatch))
                {
                    bFirstMatchDone = true;
                    firstline.Value = index;
                    result.push(line);
                }
                continue;
            }
            else
            {
                result.push(line);
                if (system.Cs2Hx.StringContains(line, endmatch))
                {
                    return result;
                }
            }
        }
        if (bFirstMatchDone)
        {
            return throw new system.SystemException("Can not find end-match");
        }
        return null;
    }
    public static function FindMatchedLines2(lines:Array<String>, firstmatch:String, endmatch:String, firstline:CsRef<Int>):Array<String>
    {
        firstline.Value = -1;
        if (lines == null)
        {
            return null;
        }
        var result:Array<String> = new Array<String>();
        var bFirstMatchDone:Bool = false;
        var pushCounter:Int = 0;
        var index:Int = -1;
        while (true)
        {
            index++;
            if (index >= lines.length)
            {
                break;
            }
            var line:String = lines[index];
            if (!bFirstMatchDone)
            {
                if (system.Cs2Hx.StringContains(line, firstmatch))
                {
                    bFirstMatchDone = true;
                    firstline.Value = index;
                    result.push(line);
                }
                continue;
            }
            else
            {
                result.push(line);
                if (system.Cs2Hx.StringContains(line, firstmatch))
                {
                    pushCounter++;
                    continue;
                }
                else if (system.Cs2Hx.StringContains(line, endmatch))
                {
                    if (pushCounter > 0)
                    {
                        pushCounter--;
                        continue;
                    }
                    else if (pushCounter == 0)
                    {
                        return result;
                    }
                }
            }
        }
        if (bFirstMatchDone)
        {
            return throw new system.SystemException("Can not find end-match");
        }
        return null;
    }
    public static function ReplaceLines(src:Array<String>, src_target_start:Int, src_target_size:Int, rep:Array<String>):Array<String>
    {
        if (src == null)
        {
            return null;
        }
        var result:Array<String> = new Array<String>();
        { //for
            var i:Int = 0;
            while (i < src_target_start)
            {
                result.push(src[i]);
                i++;
            }
        } //end for
        system.Cs2Hx.AddRange(result, rep);
        { //for
            var i:Int = src_target_start + src_target_size;
            while (i < src.length)
            {
                result.push(src[i]);
                i++;
            }
        } //end for
        return result;
    }
    public static function ReplaceWordsInLine(line:String, target:String, replace:String, bTrimEnd:Bool = true):Array<String>
    {
        if (system.Cs2Hx.IsNullOrEmpty(line))
        {
            return throw new system.SystemException("Unexpected! {8F041B67-5F7C-4159-83BC-A0A20858C242}");
        }
        if (system.Cs2Hx.IsNullOrEmpty(target))
        {
            return throw new system.SystemException("Unexpected! {475F3A7E-03A0-4AE0-94AD-8668BDA5B217}");
        }
        if (system.Cs2Hx.Trim(target) != target)
        {
            return throw new system.SystemException("Unexpected! {BC4E8F0B-5DAA-4ED5-9E75-98134929CF0B}");
        }
        if (!system.Cs2Hx.StringContains(line, target))
        {
            return throw new system.SystemException("Unexpected! {D5C8183F-D166-4C6E-AB6B-2E7FD7155696}");
        }
        var replace2:String = "";
        if (!system.Cs2Hx.IsNullOrEmpty(replace))
        {
            replace2 = system.Cs2Hx.Trim(replace);
        }
        var newline:String = lib.util.StringUtil.FindNewLineChar(replace2);
        if (newline == null)
        {
            var tmp:String = line.replace(target, replace2);
            var p:Array<String> = new Array<String>();
            p.push(tmp);
            return p;
        }
        var replines:Array<String> = bTrimEnd ? lib.util.StringUtil.SplitTrimEnd(replace2, 10) : lib.util.StringUtil.SplitTrim(replace2, 10);
        var firstspace:String = psgg.HxRegexUtil.Get1stMatch("^\\s", line);
        var targetindex:Int = line.indexOf(target);
        var result:Array<String> = new Array<String>();
        {
            var buf:String = line.substr(0, targetindex);
            result.push(buf);
        }
        for (r in replines)
        {
            var buf:String = "";
            if (firstspace != null)
            {
                buf += system.Cs2Hx.NullCheck(firstspace) + system.Cs2Hx.NullCheck(Cs2Hx.NewString(32, targetindex - firstspace.length));
            }
            else
            {
                buf += Cs2Hx.NewString(32, targetindex);
            }
            buf += r;
            result.push(buf);
        }
        {
            var buf:String = "";
            if (firstspace != null)
            {
                buf += system.Cs2Hx.NullCheck(firstspace) + system.Cs2Hx.NullCheck(Cs2Hx.NewString(32, targetindex + target.length - firstspace.length));
            }
            else
            {
                buf += Cs2Hx.NewString(32, targetindex + target.length);
            }
            buf += line.substr(targetindex + target.length);
            result.push(buf);
        }
        return result;
    }
    public static function LineToBuf(lines:Array<String>, newlinechar:String = null):String
    {
        if (newlinechar == null)
        {
            newlinechar = "\n";
        }
        var s:String = "";
        for (l in lines)
        {
            if (!system.Cs2Hx.IsNullOrEmpty(s))
            {
                s += newlinechar;
            }
            s += system.Cs2Hx.TrimEnd(l);
        }
        return s;
    }
    public static function SplitComma(i:String):Array<String>
    {
        if (system.Cs2Hx.IsNullOrEmpty(i))
        {
            return null;
        }
        var s:String = system.Cs2Hx.Trim(i);
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return null;
        }
        var dw:String = "\\x22((\\x5c\\x22)|([^\\x22]))*?\\x22";
        var p1:String = "[^\\x22]+?\\x2c";
        var p2:String = "[^\\x22]+?$";
        var p3:String = system.Cs2Hx.NullCheck(dw) + "\\s*\\x2c";
        var p4:String = system.Cs2Hx.NullCheck(dw) + "\\s*$";
        var regex:String = psgg.HxString.Format("^(({0})|({1})|({2})|({3}))", [ p1, p2, p3, p4 ]);
        var tb:String = s;
        var list:Array<String> = new Array<String>();
        { //for
            var loop:Int = 0;
            while (loop <= 100)
            {
                if (loop == 100)
                {
                    return throw new system.SystemException("Unexpected! {11529044-AA98-47BC-9B8B-A7D2B5322265}");
                }
                var f:String = psgg.HxRegexUtil.Get1stMatch(regex, tb);
                if (!system.Cs2Hx.IsNullOrEmpty(f))
                {
                    var f2:String = system.Cs2Hx.Trim(system.Cs2Hx.Trim_(f, [ 44 ]));
                    list.push(f2);
                    tb = tb.substr(f.length);
                }
                else
                {
                    break;
                }
                loop++;
            }
        } //end for
        if (list.length > 0)
        {
            return list;
        }
        return null;
    }
    public static function SplitApiArges(buf:String, api:CsRef<String>, args:CsRef<Array<String>>, error:CsRef<String>):Bool
    {
        api.Value = null;
        args.Value = null;
        error.Value = null;
        if (system.Cs2Hx.IsNullOrEmpty(buf))
        {
            error.Value = "buf is null";
            return false;
        }
        var sp:Int = system.Cs2Hx.IndexOfChar(buf, 40);
        if (sp < 0)
        {
            api.Value = buf;
            return true;
        }
        var ep:Int = system.Cs2Hx.IndexOfChar(buf, 41);
        if (ep < sp)
        {
            error.Value = "arg string is invalid. #1";
            return false;
        }
        api.Value = buf.substr(0, sp);
        var argstr:String = buf.substr(sp, (ep - sp));
        if (!system.Cs2Hx.IsNullOrEmpty(argstr))
        {
            argstr = system.Cs2Hx.TrimStart(argstr, [ 40 ]);
            if (!system.Cs2Hx.IsNullOrEmpty(argstr))
            {
                argstr = lib.util.StringUtil.TrimEnd(argstr, 41);
            }
            if (!system.Cs2Hx.IsNullOrEmpty(argstr))
            {
                argstr = system.Cs2Hx.Trim(argstr);
            }
        }
        if (system.Cs2Hx.IsNullOrEmpty(argstr))
        {
            return true;
        }
        var arglist:Array<String> = lib.util.StringUtil.SplitComma(argstr);
        if (arglist == null)
        {
            error.Value = "unexpected! {87753187-4E54-4E2D-A445-239002F2E59A}";
            return false;
        }
        args.Value = arglist;
        return true;
    }
    public static function TrimEnd(s:String, c:Int):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return s;
        }
        var len:Int = s.length;
        { //for
            var i:Int = 0;
            while (i < len)
            {
                if (!system.Cs2Hx.IsNullOrEmpty(s))
                {
                    if (s.charCodeAt(s.length - 1) == c)
                    {
                        s = s.substr(0, s.length - 1);
                    }
                }
                i++;
            }
        } //end for
        return s;
    }
    public static function SplittComma_And_ApiArges(s:String):Array<String>
    {
        var api:CsRef<String> = new CsRef<String>(null);
        var args:CsRef<Array<String>> = new CsRef<Array<String>>(null);
        var error:CsRef<String> = new CsRef<String>(null);
        lib.util.StringUtil.SplitApiArges(s, api, args, error);
        if (!system.Cs2Hx.IsNullOrEmpty(error.Value) || system.Cs2Hx.StringContains(api.Value, ","))
        {
            api.Value = null;
            args.Value = lib.util.StringUtil.SplitComma(s);
        }
        else
        {
            if (args.Value == null)
            {
                args.Value = new Array<String>();
            }
            args.Value.insert(0, api.Value);
            api.Value = null;
        }
        return args.Value;
    }
    public static function CountChar(s:String, c:Int):Int
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return 0;
        }
        var count:Int = 0;
        for (i in Cs2Hx.ToCharArray(s))
        {
            if (i == c)
            {
                count++;
            }
        }
        return count;
    }
    public static function convert_to_snake_word_and_lower(s:String):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return s;
        }
        var o:String = "";
        var save:Int = 0;
        var ckuplow:(Int -> Int) = function (_:Int):Int
        {
            if (_ >= 97 && _ <= 122)
            {
                return 1;
            }
            if (_ >= 65 && _ <= 90)
            {
                return 2;
            }
            if (_ == 95)
            {
                return 3;
            }
            return 0;
        }
        ;
        var n:Int = -1;
        while (true)
        {
            n++;
            if (n >= s.length)
            {
                break;
            }
            var c:Int = s.charCodeAt(n);
            var ul:Int = ckuplow(c);
            if (n == 0)
            {
                save = ul;
                o += c;
                continue;
            }
            if (ul == 3)
            {
                save = 3;
                o += c;
                continue;
            }
            if (save == ul)
            {
                o += c;
                continue;
            }
            if (save == 3)
            {
                save = ul;
                o += c;
                continue;
            }
            else if (save == 2 && ul == 1)
            {
                save = ul;
                o += c;
                continue;
            }
            else
            {
                save = ul;
                o += 95;
                o += c;
                continue;
            }
        }
        var o2:String = o.toLowerCase();
        return o2;
    }
    public static function convert_to_camel_word(s:String, upperOrLower:Bool):String
    {
        if (system.Cs2Hx.IsNullOrEmpty(s))
        {
            return s;
        }
        var o:String = "";
        var save:Int = 0;
        var ckuplow:(Int -> Int) = function (_:Int):Int
        {
            if (_ >= 97 && _ <= 122)
            {
                return 1;
            }
            if (_ >= 65 && _ <= 90)
            {
                return 2;
            }
            if (_ == 95)
            {
                return 3;
            }
            return 0;
        }
        ;
        var n:Int = -1;
        while (true)
        {
            n++;
            if (n >= s.length)
            {
                break;
            }
            var c:Int = s.charCodeAt(n);
            var cs:String = Cs2Hx.NewString(c, 1);
            var ul:Int = ckuplow(c);
            if (n == 0)
            {
                save = ul;
                o += upperOrLower ? cs.toUpperCase() : cs.toLowerCase();
                continue;
            }
            if (ul == 3)
            {
                save = ul;
                continue;
            }
            if (save == 3)
            {
                save = ul;
                o += cs.toUpperCase();
                continue;
            }
            save = ul;
            o += cs;
            continue;
        }
        return o;
    }
    public function new()
    {
    }
}
