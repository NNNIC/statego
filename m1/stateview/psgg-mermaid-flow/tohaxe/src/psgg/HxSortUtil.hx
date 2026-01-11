package psgg;
using StringTools;
import system.*;
import anonymoustypes.*;

class HxSortUtil
{
    public static function Sort(l:Array<String>):Array<String> {
        var l2=l.copy();
        l2.sort( 
            function(a:String, b:String):Int {
                var a2 = a.toLowerCase();
                var b2 = b.toLowerCase();
                if (a2<b2) return - 1;
                if (a2>b2) return 1;
                return 0;
            }
        );
        return l2;
    }
    public function new()
    {
    }
}
