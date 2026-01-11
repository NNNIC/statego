/*
This file serves two purposes:  
    1)  It imports every type that CS2HX generated.  haXe will ignore 
        any types that aren't used by haXe code, so this ensures haXe 
        compiles all of your code.

    2)  It lists all the static constructors.  haXe doesn't have the 
        concept of static constructors, so CS2HX generated cctor()
        methods.  You must call these manually.  If you call
        Constructors.init(), all static constructors will be called 
        at once.
*/
package ;
import lib.RefListString;
import lib.util.ArrayUtil;
import lib.util.BranchUtil;
import lib.util.BranchUtil_Item;
import lib.util.CsvUtil;
import lib.util.DictionaryUtil;
import lib.util.IniUtil;
import lib.util.ListUtil;
import lib.util.MacroUtil;
import lib.util.ParseUtil;
import lib.util.PathUtil;
import lib.util.PsggDataFileUtil;
import lib.util.PsggDataFileUtil_Item;
//import lib.util.RegexUtil;
//import lib.util.SortUtil;
import lib.util.StateUtil;
import lib.util.StringUtil;
import lib.wordstrage.Store;
import psgg2mermaid.Program;
import system.TimeSpan;
class VConstructors
{
    public static function init()
    {
        TimeSpan.cctor();
    }
}
