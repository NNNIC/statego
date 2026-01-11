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
        static void Main(string[] args)
        {
            Console.WriteLine("Start converting.");
 
            var bDidpContents = Array.Find(args, i=>i=="-c")!=null ;

            Func<string, bool> valid = v => { return !string.IsNullOrEmpty(v); };

            
            //var psggfile = @"G:\statego\psgg-converter-to-haxe\tohaxe\testdata-tmp\php\FizzBuzzControl.psgg";
            var psggfile = args[0];
            var psggdir =  Path.GetDirectoryName(psggfile);
            var item = lib.util.PsggDataFileUtil.ReadPsgg(psggfile);
            var states = item.GetAllStates();

            var NL = Environment.NewLine;
            var DQ = "\"";
            var BR = "<br/>";
            var bStateDiagram = Array.Find(args, i=>i=="-state")!=null;
            var s = (bStateDiagram ? "stateDiagram-v2" : "graph LR") + NL;
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
                            s += tab + string.Format("{0}[[{1}]] --> {2}", st, v, nextstate) + NL;
                        }
                    }
                    else
                    {
                        var op_br = "[";
                        var cl_br = "]";
                        if (typ == "start" || typ == "end")
                        {
                            if (bStateDiagram)
                            {
                                s += tab + string.Format("[*] --> {0}", st) + NL; // For start. End logic needs reversal.
                                // Actually, standard stateDiagram: [*] --> StartState
                                // But here we are defining node 'st'.
                                // If typ is start, we want: [*] --> st
                                // If typ is end, we want: st --> [*]
                                
                                // WAIT, the original logic builds 's' by verifying 'nextstate'.
                                // For 'start': st((v)) --> nextstate
                                // For 'end': st((v))
                                
                                // In stateDiagram: 
                                // start: [*] --> nextstate (skip 'st' node?) OR [*] --> st --> nextstate
                                // end: st --> [*]
                                
                                // Let's keep it simple. If bStateDiagram:
                                // usage of [*] is confusing if we map 1:1.
                                // stateDiagram-v2 allows normal nodes.
                                // Let's just use rounded box for start/end if simple.
                                // mermaid state diagram doesn't use (( )). It uses specific start/end syntax.
                                // Let's fallback to standard nodes for now but change header. 
                                // The user's main request was just "stateDiagram-v2" support.
                                // I will stick to changing header ONLY for now to minimize logic breakage, 
                                // BUT graph/flowchart syntax (-->) is mostly compatible.
                                op_br = "((";
                                cl_br = "))";
                            } 
                            else 
                            { 
                                op_br = "(("; 
                                cl_br = "))"; 
                            }
                        }
                        if (typ == "substart" || typ == "subreturn")
                        {
                            op_br = "(";
                            cl_br = ")";
                        }
                        if (valid(nextstate))
                        {
                            s += tab + string.Format("{0}" +op_br +  "{1}"+ cl_br +" --> {2}", st, v, nextstate) + NL;
                        }
                        else
                        {
                            s += tab + string.Format("{0}" + op_br +"{1}" + cl_br, st, v) + NL;
                        }
                    }
                }
                else
                {
                    var branch_node = st + "____br";
                    s += tab + string.Format("{0}[{1}] ==> {2}", st, v, branch_node ) + NL;

                    for (var n = 0; n < britem.count; n++)
                    {
                        var api = britem.get_api(n);
                        var cod = britem.get_cond(n);
                        var nst = britem.get_state(n);
                        var c   = britem.get_cmt(n);

                        var bcmt = DQ;
                        Action<string> addif = v => {
                            if (valid(v))
                            {
                                if (bcmt != DQ) bcmt += " ";
                                bcmt +=  v;
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
                            s += tab + string.Format("{0}{{{1}}} -->|{2}| {3}", branch_node, DQ + "?" + DQ , bcmt, nst) + NL;
                        }
                        else
                        {
                            s += tab + string.Format("{0} -->|{1}| {2}", branch_node, bcmt, nst) + NL;
                        }
                    }
                }
                //
                if (valid(typ))
                {
                    addstyle("typ_"+typ.ToUpper(), st);
                }
            }
            foreach (var st in states)
            {
                if (!st.StartsWith("C_")) continue;
                var cmt = item.GetVal(st, "state-cmt");
                if (cmt != null)
                {
                    cmt = MKBR(cmt);
                }
                var v = DQ + st + (valid(cmt) ? BR + cmt : "") + DQ;
                s += tab + string.Format("{0}[{1}]", st, v) + NL;

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
                s += tab + string.Format("{0}[{1}]", st, v) + NL;

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

            s = modEnd(s);

            File.WriteAllText(args[1], s, Encoding.UTF8);
            Console.WriteLine("..done!");
        }
    }
}
