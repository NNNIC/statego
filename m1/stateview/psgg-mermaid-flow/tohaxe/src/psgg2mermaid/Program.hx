package psgg2mermaid;
using StringTools;
import system.*;
import anonymoustypes.*;

class Program
{
    public static function Main(args:Array<String>):Void
    {
        var bDidpContents:Bool = false;
        for (a in args)
        {
            if (a == "-c")
            {
                bDidpContents = true;
                break;
            }
        }
        var psgg_data:String = psgg.HxFile.ReadAllText_String_Encoding(args[0], system.text.Encoding.UTF8);
        var s:String = Convert(psgg_data, bDidpContents);
        psgg.HxFile.WriteAllText_String_String_Encoding(args[1], s, system.text.Encoding.UTF8);
        system.Console.WriteLine("..done!");
    }
    public static function Convert(psggdata:String, bDidpContents:Bool):String
    {
        var valid:(String -> Bool) = function (v:String):Bool
        {
            return !system.Cs2Hx.IsNullOrEmpty(v);
        }
        ;
        var item:lib.util.PsggDataFileUtil_Item = lib.util.PsggDataFileUtil.ReadPsggData(psggdata);
        var states:Array<String> = item.GetAllStates();
        var NL:String = system.Environment.NewLine;
        var DQ:String = "\"";
        var BR:String = "<br/>";
        var s:String = "graph LR" + system.Cs2Hx.NullCheck(NL);
        var tab:String = "    ";
        var styledic:system.collections.generic.Dictionary<String, Array<String>> = new system.collections.generic.Dictionary<String, Array<String>>();
        var addstyle:(String -> String -> Void) = function (stylename:String, state:String):Void
        {
            if (!styledic.ContainsKey(stylename))
            {
                styledic.Add(stylename, new Array<String>());
            }
            var list:Array<String> = styledic.GetValue_TKey(stylename);
            if (!system.Cs2Hx.Contains(list, state))
            {
                list.push(state);
            }
            styledic.SetValue_TKey(stylename, list);
        }
        ;
        var MKBR:(String -> String) = function (ss:String):String
        {
            if (system.Cs2Hx.IsNullOrEmpty(ss))
            {
                return "";
            }
            return ss.replace(lib.util.StringUtil._0d0a, BR).replace(lib.util.StringUtil._0a, BR);
        }
        ;
        var modEnd:(String -> String) = function (ss:String):String
        {
            if (valid(ss))
            {
                return ss.replace("end", "END");
            }
            return ss;
        }
        ;
        for (st in states)
        {
            if (!system.Cs2Hx.StartsWith(st, "S_"))
            {
                continue;
            }
            var cmt:String = item.GetVal(st, "state-cmt");
            var nextstate:String = item.GetVal(st, "nextstate");
            var typ:String = item.GetVal(st, "state-typ");
            var gosub:String = item.GetVal(st, "gosubstate");
            var loopinit:String = item.GetVal(st, "loop_init");
            var loopcond:String = item.GetVal(st, "loop_cond");
            var loopnext:String = item.GetVal(st, "loop_next");
            var contents:String = "";
            if (bDidpContents)
            {
                var names:Array<String> = item.GetAllNames();
                for (n in names)
                {
                    if (system.Cs2Hx.StartsWith(n, "!"))
                    {
                        continue;
                    }
                    if (system.Cs2Hx.StartsWith(n, "state"))
                    {
                        continue;
                    }
                    if (system.Cs2Hx.StartsWith(n, "branch"))
                    {
                        continue;
                    }
                    if (system.Cs2Hx.StartsWith(n, "brcond"))
                    {
                        continue;
                    }
                    if (n == "basestate")
                    {
                        continue;
                    }
                    if (n == "nextstate")
                    {
                        continue;
                    }
                    if (n.indexOf("-cmt") >= 0)
                    {
                        continue;
                    }
                    if (n == "gosubstate")
                    {
                        continue;
                    }
                    if (n == "return")
                    {
                        continue;
                    }
                    if (system.Cs2Hx.StartsWith(n, "loop_"))
                    {
                        continue;
                    }
                    var t:String = item.GetVal(st, n);
                    if (valid(t))
                    {
                        if (valid(contents))
                        {
                            contents += BR;
                        }
                        if (n == "nowait")
                        {
                            contents += "nowait;";
                            continue;
                        }
                        contents += MKBR(t);
                    }
                }
            }
            var branch:String = item.GetVal(st, "branch");
            var brcond:String = item.GetVal(st, "brcond");
            var brcmt:String = item.GetVal(st, "branch-cmt");
            var britem:lib.util.BranchUtil_Item = lib.util.BranchUtil.Read(branch, brcond, brcmt);
            if (valid(cmt))
            {
                cmt = MKBR(cmt);
            }
            if (typ == "gosub" && valid(gosub))
            {
                if (valid(cmt))
                {
                    cmt += BR;
                }
                cmt += "CALL " + system.Cs2Hx.NullCheck(gosub.substr(2));
            }
            if (typ == "loop")
            {
                var t:String = "LOOP (" + system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(loopinit) + system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(loopcond) + system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(loopnext) + system.Cs2Hx.NullCheck(BR) + ")" + system.Cs2Hx.NullCheck(BR) + "CALL " + system.Cs2Hx.NullCheck(gosub);
                cmt += t;
            }
            if (valid(contents))
            {
                if (valid(cmt))
                {
                    cmt += BR;
                }
                cmt += contents;
            }
            if (valid(cmt))
            {
                cmt = cmt.replace("\"", "`");
            }
            var v:String = system.Cs2Hx.NullCheck(DQ) + system.Cs2Hx.NullCheck((system.Cs2Hx.StartsWith(st, "S_") ? st.substr(2) : st)) + system.Cs2Hx.NullCheck((valid(cmt) ? system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(cmt) : "")) + system.Cs2Hx.NullCheck(DQ);
            if (britem == null)
            {
                if (typ == "gosub" || typ == "loop")
                {
                    if (valid(nextstate))
                    {
                        s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String_String("{0}[[{1}]] --> {2}", st, v, nextstate)) + system.Cs2Hx.NullCheck(NL);
                    }
                }
                else
                {
                    var op_br:String = "[";
                    var cl_br:String = "]";
                    if (typ == "start" || typ == "end")
                    {
                        op_br = "((";
                        cl_br = "))";
                    }
                    if (typ == "substart" || typ == "subreturn")
                    {
                        op_br = "(";
                        cl_br = ")";
                    }
                    if (valid(nextstate))
                    {
                        s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String_String("{0}" + system.Cs2Hx.NullCheck(op_br) + "{1}" + system.Cs2Hx.NullCheck(cl_br) + " --> {2}", st, v, nextstate)) + system.Cs2Hx.NullCheck(NL);
                    }
                    else
                    {
                        s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String("{0}" + system.Cs2Hx.NullCheck(op_br) + "{1}" + system.Cs2Hx.NullCheck(cl_br), st, v)) + system.Cs2Hx.NullCheck(NL);
                    }
                }
            }
            else
            {
                var branch_node:String = system.Cs2Hx.NullCheck(st) + "____br";
                s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String_String("{0}[{1}] ==> {2}", st, v, branch_node)) + system.Cs2Hx.NullCheck(NL);
                { //for
                    var n:Int = 0;
                    while (n < britem.count())
                    {
                        var api:String = britem.get_api(n);
                        var cod:String = britem.get_cond(n);
                        var nst:String = britem.get_state(n);
                        var c:String = britem.get_cmt(n);
                        var bcmt:String = DQ;
                        var addif:(String -> Void) = function (vv:String):Void
                        {
                            if (valid(vv))
                            {
                                if (bcmt != DQ)
                                {
                                    bcmt += " ";
                                }
                                bcmt += vv;
                            }
                        }
                        ;
                        if (valid(c))
                        {
                            bcmt += c;
                        }
                        else
                        {
                            if (valid(cod))
                            {
                                addif(cod);
                            }
                            else
                            {
                                addif(api);
                            }
                        }
                        bcmt += DQ;
                        if (n == 0)
                        {
                            s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String_String_String("{0}{{{1}}} -->|{2}| {3}", branch_node, system.Cs2Hx.NullCheck(DQ) + "?" + system.Cs2Hx.NullCheck(DQ), bcmt, nst)) + system.Cs2Hx.NullCheck(NL);
                        }
                        else
                        {
                            s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String_String("{0} -->|{1}| {2}", branch_node, bcmt, nst)) + system.Cs2Hx.NullCheck(NL);
                        }
                        n++;
                    }
                } //end for
            }
            if (valid(typ))
            {
                addstyle("typ_" + system.Cs2Hx.NullCheck(typ.toUpperCase()), st);
            }
        }
        for (st in states)
        {
            if (!system.Cs2Hx.StartsWith(st, "C_"))
            {
                continue;
            }
            var cmt:String = item.GetVal(st, "state-cmt");
            if (cmt != null)
            {
                cmt = MKBR(cmt);
            }
            var v:String = system.Cs2Hx.NullCheck(DQ) + system.Cs2Hx.NullCheck(st) + system.Cs2Hx.NullCheck((valid(cmt) ? system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(cmt) : "")) + system.Cs2Hx.NullCheck(DQ);
            s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String("{0}[{1}]", st, v)) + system.Cs2Hx.NullCheck(NL);
            addstyle("comment", st);
        }
        for (st in states)
        {
            if (!system.Cs2Hx.StartsWith(st, "E_"))
            {
                continue;
            }
            var cmt:String = item.GetVal(st, "state-cmt");
            if (cmt != null)
            {
                cmt = MKBR(cmt);
            }
            var embed:String = item.GetVal(st, "embed");
            if (bDidpContents && valid(embed))
            {
                embed = MKBR(embed);
                cmt += system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(embed);
            }
            var v:String = system.Cs2Hx.NullCheck(DQ) + system.Cs2Hx.NullCheck(st) + system.Cs2Hx.NullCheck((valid(cmt) ? system.Cs2Hx.NullCheck(BR) + system.Cs2Hx.NullCheck(cmt) : "")) + system.Cs2Hx.NullCheck(DQ);
            s += system.Cs2Hx.NullCheck(tab) + system.Cs2Hx.NullCheck(string_Format_String_String_String("{0}[{1}]", st, v)) + system.Cs2Hx.NullCheck(NL);
            addstyle("embed", st);
        }
        s += system.Cs2Hx.NullCheck(tab) + "classDef typ_START     fill:#9cf,stroke:#000,stroke-width:1px;" + system.Cs2Hx.NullCheck(NL);
        s += system.Cs2Hx.NullCheck(tab) + "classDef typ_END       fill:#9cf,stroke:#000,stroke-width:1px;" + system.Cs2Hx.NullCheck(NL);
        s += system.Cs2Hx.NullCheck(tab) + "classDef typ_SUBSTART  fill:#9cf,stroke:#000,stroke-width:1px;" + system.Cs2Hx.NullCheck(NL);
        s += system.Cs2Hx.NullCheck(tab) + "classDef typ_SUBRETURN fill:#9cf,stroke:#000,stroke-width:1px;" + system.Cs2Hx.NullCheck(NL);
        s += system.Cs2Hx.NullCheck(tab) + "classDef comment       fill:#ff9,stroke:#000,stroke-width:4px;" + system.Cs2Hx.NullCheck(NL);
        s += system.Cs2Hx.NullCheck(tab) + "classDef embed         fill:#fff,stroke:#000,stroke-width:4px;" + system.Cs2Hx.NullCheck(NL);
        for (p in styledic.GetEnumerator())
        {
            var idlist:String = "";
            for (i in p.Value)
            {
                if (valid(idlist))
                {
                    idlist += ",";
                }
                idlist += i;
            }
            s += system.Cs2Hx.NullCheck(tab) + "class " + system.Cs2Hx.NullCheck(idlist) + " " + system.Cs2Hx.NullCheck(p.Key) + system.Cs2Hx.NullCheck(NL);
        }
        s = modEnd(s);
        return s;
    }
    static function string_Format(fmt:String, p0:String):String
    {
        return fmt.replace("{0}", p0);
    }
    static function string_Format_String_String_String(fmt:String, p0:String, p1:String):String
    {
        return fmt.replace("{0}", p0).replace("{1}", p1);
    }
    static function string_Format_String_String_String_String(fmt:String, p0:String, p1:String, p2:String):String
    {
        return fmt.replace("{0}", p0).replace("{1}", p1).replace("{2}", p2);
    }
    static function string_Format_String_String_String_String_String(fmt:String, p0:String, p1:String, p2:String, p3:String):String
    {
        return fmt.replace("{0}", p0).replace("{1}", p1).replace("{2}", p2).replace("{3}", p3);
    }
    public function new()
    {
    }
}
