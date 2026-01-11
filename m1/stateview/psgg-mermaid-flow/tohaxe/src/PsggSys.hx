import haxe.io.BytesBuffer;
import haxe.io.Encoding;
import haxe.io.Bytes;
class PsggSys {
    public function new(){}

    public static function print(v:String) {
        var so = Sys.stdout();
        so.writeString(v,Encoding.UTF8);
        // var data = Bytes.ofString(v);
        // var so = Sys.stdout();
        // var data = Bytes.ofString(v);
        // //so.writeString(v,Encoding.UTF8);
        // //so.write(data);
        // var g = new BytesBuffer();
        // g.addByte(0xE6);
        // g.addByte(0x9C);
        // g.addByte(0x89);

        // so.write( g.getBytes() );
    }
}