package psgg;

class HxString {
    public function new ()
    {

    }
    public static function Format(fmt:String,  plist:Array<String> ) : String { 
        var s = fmt;
        for(i in 0...plist.length) {
            var sub = "{" + i + "}";
            s = StringTools.replace(s,sub,plist[i]);
        }
        return s;
    }
}