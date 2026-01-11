using lib.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace psgg2mermaid
{
    class Program
    {
        public static void Main(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();

            var bDidpContents = false; //  Array.Find(args, i=>i=="-c")!=null ;   Haxe not supported
            var bHtmlOutput = false;
            foreach (var a in args)
            {
                if (a == "-c")
                {
                    bDidpContents = true;
                }
                if (a == "-html")
                {
                    bHtmlOutput = true;
                }
            }
            var psgg_data = File.ReadAllText(args[0], Encoding.UTF8);

            var s = Convert(psgg_data, bDidpContents);

            if (bHtmlOutput)
            {
                File_WriteHtml(args[0], args[1], s);
            }
            else
            {
                File.WriteAllText(args[1], s, Encoding.UTF8);
            }
            Console.WriteLine("..done!");
        }

        private static void File_WriteHtml(string psgg, string file, string s)
        {
            var srcname = Path.GetFileNameWithoutExtension(psgg);
            var NL = Environment.NewLine;
            var txt =
                "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\">" + NL +
                "<html lang=\"en\">" + NL +
                "<head>" + NL +
                "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" + NL +
                "<title>" + srcname + " FLOW CHART" + "</title>" + NL +
                "</head>" + NL +
                "<body>" + NL +
                "<h1>" + srcname + "</h1>" + NL +
                "<p> source:" + psgg + "</p>" + NL + 
                "<p>﻿<div class=\"mermaid\">" + NL +
                s + NL +
                "</div><br> </p>" + NL +
                "<script>" + NL +
                "mermaid.initialize({startOnLoad: true, theme: 'neutral'});" + NL +
                "</script>" + NL +
                "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/mermaid/8.8.0/mermaid.min.js\" integrity=\"sha512-ja+hSBi4JDtjSqc4LTBsSwuBT3tdZ3oKYKd07lTVYmCnTCor56AnRql00ssqnTOR9Ss4gOP/ROGB3SfcJnZkeg==\" crossorigin=\"anonymous\"></script>" + NL +
                "</body>" + NL +
                "</html>" + NL
                ;
            File.WriteAllText(file, txt, Encoding.UTF8);
        }

        public static string Convert(string psggdata, bool bDidpContents)
        {

            Func<string, bool> valid = v => { return !string.IsNullOrEmpty(v); };

            
            var item = lib.util.PsggDataFileUtil.ReadPsggData(psggdata);
            var states = item.GetAllStates();

            //Console.WriteLine("States0 = " + states[0]);

            var NL = Environment.NewLine;
            var DQ = "\"";
            var BR = "<br/>";
            var s = "graph LR" + NL;
            var tab = "    ";
            var styledic = new Dictionary<string, List<string>>();
            Action<string, string> addstyle = (stylename, state) => {
                if (!styledic.ContainsKey(stylename))
                {
                    styledic.Add(stylename, new List<string>());
                }
                var list = styledic[stylename];
                if (!list.Contains(state))
                {
                    list.Add(state);
                }
                styledic[stylename] = list;
            };
            Func<string, string> MKBR = (ss) => {
                if (string.IsNullOrEmpty(ss)) return string.Empty;
                return ss.Replace(StringUtil._0d0a, BR).Replace(StringUtil._0a, BR);
            };
            Func<string, string> modEnd = (ss) => {
                if (valid(ss))
                {
                    return ss.Replace("end", "END");
                }
                return ss;
            };

            //Console.WriteLine("{5C4CB7A0-2816-4213-883D-77903F79C3A4}");

            foreach (var st in states)
            {
                if (!st.StartsWith("S_")) continue;

                var cmt = item.GetVal(st, "state-cmt");
                var nextstate = item.GetVal(st, "nextstate");
                var typ = item.GetVal(st, "state-typ");
                var gosub = item.GetVal(st, "gosubstate");
                var loopinit = item.GetVal(st, "loop_init");
                var loopcond = item.GetVal(st, "loop_cond");
                var loopnext = item.GetVal(st, "loop_next");
                var contents = string.Empty;
                if (bDidpContents)
                {
                    var names = item.GetAllNames();
                    foreach (var n in names)
                    {
                        if (n.StartsWith("!")) continue;
                        if (n.StartsWith("state")) continue;
                        if (n.StartsWith("branch")) continue;
                        if (n.StartsWith("brcond")) continue;
                        if (n == "basestate") continue;
                        if (n == "nextstate") continue;
                        if (n.IndexOf("-cmt") >= 0) continue;
                        if (n == "gosubstate")      continue;
                        if (n == "return")          continue;
                        if (n.StartsWith("loop_"))  continue;

                        var t = item.GetVal(st, n);
                        if (valid(t))
                        {
                            if (valid(contents)) contents += BR;
                            if (n == "nowait")
                            {
                                contents += "nowait;";
                                continue;
                            }
                            contents += MKBR(t);
                        }
                    }
                }
                //Console.WriteLine("{4E3F57A1-579E-452F-8218-916205141E0C}");

                var branch = item.GetVal(st, "branch");
                var brcond = item.GetVal(st, "brcond");
                var brcmt = item.GetVal(st, "branch-cmt");

                var britem = BranchUtil.Read(branch, brcond, brcmt);

                if (valid( cmt ))
                {
                    cmt = MKBR(cmt);
                }
                if (typ == "gosub" && valid(gosub))
                {
                    if (valid(cmt)) cmt += BR;
                    cmt += "CALL " + gosub.Substring(2);
                }
                if (typ == "loop")
                {
                    var t = "LOOP (" + BR + loopinit +  BR + loopcond + BR + loopnext + BR + ")" + BR + "CALL " + gosub;
                    cmt += t;
                }
                if (valid(contents))
                {
                    if (valid(cmt)) cmt += BR;
                    cmt += contents;
                }

                if (valid(cmt))
                {
                    cmt = cmt.Replace("\"", "`");
                }

                var v = DQ + (st.StartsWith("S_") ?  st.Substring(2) : st) + (valid(cmt) ? BR + cmt : "") + DQ;

                if (britem == null)
                {
                    if (typ == "gosub" || typ == "loop")
                    {
                        if (valid(nextstate))
                        {
                            s += tab + string_Format("{0}[[{1}]] --> {2}", st, v, nextstate) + NL;
                        }
                    }
                    else
                    {
                        var op_br = "[";
                        var cl_br = "]";
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
                            s += tab + string_Format("{0}" +op_br +  "{1}"+ cl_br +" --> {2}", st, v, nextstate) + NL;
                        }
                        else
                        {
                            s += tab + string_Format("{0}" + op_br +"{1}" + cl_br, st, v) + NL;
                        }
                    }
                }
                else
                {
                    var branch_node = st + "____br";
                    s += tab + string_Format("{0}[{1}] ==> {2}", st, v, branch_node ) + NL;

                    for (var n = 0; n < britem.count(); n++)
                    {
                        var api = britem.get_api(n);
                        var cod = britem.get_cond(n);
                        var nst = britem.get_state(n);
                        var c   = britem.get_cmt(n);

                        var bcmt = DQ;
                        Action<string> addif = vv => {
                            if (valid(vv))
                            {
                                if (bcmt != DQ) bcmt += " ";
                                bcmt +=  vv;
                            }
                        };

                        if (valid(c))
                        {
                            bcmt += c;
                        }
                        else
                        {
                            if (valid(cod)) { addif(cod); }
                            else
                            {
                                addif(api);
                            }
                        }
                        bcmt += DQ;

                        if (n == 0)
                        {
                            s += tab + string_Format("{0}{{{1}}} -->|{2}| {3}", branch_node, DQ + "?" + DQ , bcmt, nst) + NL;
                        }
                        else
                        {
                            s += tab + string_Format("{0} -->|{1}| {2}", branch_node, bcmt, nst) + NL;
                        }
                    }
                }
                //
                if (valid(typ))
                {
                    addstyle("typ_"+typ.ToUpper(), st);
                }
            }
            //Console.WriteLine("{1179EE1D-8B42-4FD1-85EC-A4D3BF5A24D1}");
            foreach (var st in states)
            {
                if (!st.StartsWith("C_")) continue;
                var cmt = item.GetVal(st, "state-cmt");
                if (cmt != null)
                {
                    cmt = MKBR(cmt);
                }
                var v = DQ + st + (valid(cmt) ? BR + cmt : "") + DQ;
                s += tab + string_Format("{0}[{1}]", st, v) + NL;

                addstyle("comment", st);
            }
            foreach (var st in states)
            {
                if (!st.StartsWith("E_")) continue;
                var cmt = item.GetVal(st, "state-cmt");
                if (cmt != null)
                {
                    cmt = MKBR(cmt);
                }
                var embed = item.GetVal(st, "embed");
                if (bDidpContents && valid(embed))
                {
                    embed = MKBR(embed);
                    cmt += BR + embed;
                }
                var v = DQ + st + (valid(cmt) ? BR + cmt : "") + DQ;
                s += tab + string_Format("{0}[{1}]", st, v) + NL;

                addstyle("embed", st);
            }

            //style
            s += tab + "classDef typ_START     fill:#9cf,stroke:#000,stroke-width:1px;" + NL;
            s += tab + "classDef typ_END       fill:#9cf,stroke:#000,stroke-width:1px;" + NL;
            s += tab + "classDef typ_SUBSTART  fill:#9cf,stroke:#000,stroke-width:1px;" + NL;
            s += tab + "classDef typ_SUBRETURN fill:#9cf,stroke:#000,stroke-width:1px;" + NL;
            s += tab + "classDef comment       fill:#ff9,stroke:#000,stroke-width:4px;" + NL;
            s += tab + "classDef embed         fill:#fff,stroke:#000,stroke-width:4px;" + NL;

            foreach (var p in styledic)
            {
                var idlist = string.Empty;
                foreach (var i in p.Value)
                {
                    if (valid(idlist)) idlist += ",";
                    idlist += i;
                }

                s += tab + "class " + idlist + " " + p.Key + NL;  
            }

            //Console.WriteLine("{D8119A68-D9D5-4F03-9AF2-98D683E492CC}");
            //Console.WriteLine(s);
            s = modEnd(s);

            //Console.WriteLine(s);
            //Console.WriteLine("{04ABD20A-AFC2-424B-AAA8-682B019BBECB}");

            return s;
        }
        static string string_Format(string fmt, string p0)
        {
            return fmt.Replace("{0}",p0);
        }
        static string string_Format(string fmt, string p0, string p1)
        {
            return fmt.Replace("{0}",p0).Replace("{1}",p1);
        }

        static string string_Format(string fmt, string p0, string p1, string p2)
        {
            return fmt.Replace("{0}",p0).Replace("{1}",p1).Replace("{2}",p2);
        }

        static string string_Format(string fmt, string p0, string p1, string p2, string p3)
        {
            return fmt.Replace("{0}",p0).Replace("{1}",p1).Replace("{2}",p2).Replace("{3}",p3);
        }
    }
}
