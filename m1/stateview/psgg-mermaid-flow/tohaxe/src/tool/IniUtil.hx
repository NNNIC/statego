package tool;

class IniUtil {
    public static function ReadIni(initxt : String) : Map<String, Dynamic> {
        var p = new IniUtilReadControl();
        p.m_buf = initxt;
        p.Run();
        return p.m_map;
    }
    public static function GetFromMap(map:Map<String,Dynamic>, key:String) : String {
        if (map!=null) {
            var v = map.get(key);
            return cast(v, String);
        }
        return null;
    }
    public static function GetFromGroupMap(map:Map<String,Dynamic>, group:String, key:String) : String {
        if (map!=null) {
            if (map.exists(group)) {
                var mapg : Map<String, Dynamic> = map.get(group);
                var v = mapg.get(key);
                return cast(v, String);
            }
        }
        return null;
    }
    public static function Get(ini:String, key:String) : String {
        var map = ReadIni(ini);
        return GetFromMap(map,key);
    }
    public static function GetGroup(ini:String, group:String, key:String) : String {
        var map = ReadIni(ini);
        return GetFromGroupMap(map,group,key);
    }
}