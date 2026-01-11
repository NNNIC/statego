package psgg;

class HxEncoding extends system.text.Encoding {

    public function new(){
        super();
    }

    public var enc : String;
    public var bom : Bool;

    public static function GetEncoding_String(enc:String) : HxEncoding  {
        var p = new HxEncoding();
        p.enc = enc;
        p.bom = false;
        return p;
    }

    public static function IsASCIIEncoding(enc:system.text.Encoding) : Bool {
        var error = "";
        if (Std.is(enc,system.text.ASCIIEncoding)) {
            var c1 = cast(enc, system.text.ASCIIEncoding);
            return c1 != null;
        } 
        if (Std.is(enc, psgg.HxEncoding)) {
            var c2 = cast(enc, psgg.HxEncoding);
            return (c2!=null && c2.enc.toLowerCase() == "ascii");
        } 
        return false;
    }
    public static function IsUTF8Encoding(enc:system.text.Encoding) : Bool {
        var error = "";
        if (Std.is(enc, system.text.UTF8Encoding)) {
            var c1 = cast(enc, system.text.UTF8Encoding);
            return c1 != null;
        }
        if (Std.is(enc, psgg.HxEncoding)) {
            var c2 = cast(enc, psgg.HxEncoding);
            return (c2!=null && c2.enc.toLowerCase() == "utf8");
        }
        return false;
    }
    public static function ISUTF8Encoding_with_bom(enc:system.text.Encoding) : Bool {
        if (IsUTF8Encoding(enc)) {
            if (Std.is(enc, psgg.HxEncoding)) {
                var c2 = cast(enc, psgg.HxEncoding);
                return (c2!=null && c2.bom );
            }
        }
        return false;
    }    


}