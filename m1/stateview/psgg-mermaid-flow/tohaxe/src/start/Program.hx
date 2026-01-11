package start;
import tool.IniUtil;

class Program {
    public static function main() {
        VConstructors.init();

        trace("#Convert from " + Sys.args()[0]);
        trace("#Convert to " + Sys.args()[1]);
        // var p = new Convert();
        // p.TEST();

        psgg2mermaid.Program.Main(Sys.args());

    }

    public static function conv(s : String, bCode : Bool) : String {
        VConstructors.init();
        return psgg2mermaid.Program.Convert(s, bCode);
    }


}