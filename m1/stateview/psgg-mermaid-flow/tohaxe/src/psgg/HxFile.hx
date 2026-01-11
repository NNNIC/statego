package psgg;

import haxe.io.Path;
import sys.FileSystem;
import sys.io.File;

class HxFile {
    public function new () { }


    public static function ReadAllText_String_Encoding(path:String, enc:system.text.Encoding ) : String { 
        if (HxEncoding.IsASCIIEncoding(enc)){
            return PsggFile.ReadASCII(path);
        }
        return PsggFile.ReadUTF8(path);
    }
    public static function WriteAllText_String_String_Encoding(path:String, buf:String, enc:system.text.Encoding) {
        if (HxEncoding.IsASCIIEncoding(enc)){
            PsggFile.WriteASCII(path,buf);
            return;
        }
        var bom = HxEncoding.ISUTF8Encoding_with_bom(enc);
        PsggFile.WriteUTF8(path,buf,bom);
    }
    public static  function ReadAllBytes(path:String) :haxe.io.Bytes {
        return PsggFile.ReadAllBytes(path);
    }
    public static function GetFullPath(path: String) : String {
        var abspath = FileSystem.absolutePath(path);
        var normalpath = Path.normalize(abspath);
        return normalpath;
    }
    public static function Combine_String_String(path1:String, path2:String) : String {
        var p2h = path2.charAt(0);
        if (p2h == "\\" || p2h == "/" ) {
            path2 = path2.substr(1);
        }
        return path1 + "\\" + path2;
    }
    public static function Combine_String_String_String(path1:String, path2:String, path3:String) : String {
        var p2h = path2.charAt(0);
        if (p2h == "\\" || p2h == "/" ) {
            path2 = path2.substr(1);
        }
        var p3h = path3.charAt(0);
        if (p3h == "\\" || p3h == "/") {
            path3 = path3.substr(1);
        }
        return path1 + "\\" + path2 + "\\" + path3 ;
    }
    public static function GetDirectoryName(path:String) : String {
        var index = path.lastIndexOf("\\");
        if (index < 0) {
            index = path.lastIndexOf("/");
        }
        if (index < 0) 
        {
            return "-no dir ";
        }

        var v = path.substr(0,index);
        return v;
    }
}