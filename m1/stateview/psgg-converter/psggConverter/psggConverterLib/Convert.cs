using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace psggConverterLib
{
    public partial class Convert
    {
        public bool   BRKGS     = false;  //  Breakpoint At Generate Source   
        public bool   BRKGF     = false;  //  Breakpoint At Generate Function
        public bool   BRKP      = false;  //  Breakpoint At Prepare

        public void   TEST()      { Console.WriteLine("psggConvertLib TEST");}

        public string ERRMSG;

        public string VERSION()   { return ver.version;    }
        public string GITHASH()   { return githash.hash;   }
        public string BUILDTIME() { return ver.datetime;   }
        public string COPYRIGHT() { return "2018 NNNIC / MIT Licence"; }
        public string DEPOT()     { return ver.depot;      }

        public int    NAME_COL     =2;
        public int    STATE_ROW    =2;
        public string NEWLINECHAR  = "\x0d\x0a";
        public string BASESTATE    = "basestate";

        //[Obsolete]
        //public string COMMMENTLINE_OBS = "//";

        public string COMMENTLINE_FORMAT  = "// {%0}";

        public string LANG         = "";
        public string OUTPUT       = "";
        public string ENC          = "utf-8";
        public string GENDIR       = "";
        public string XLSDIR       = ""; //エクセルファイル、マクロ等
        public string INCDIR       = ""; //インクルードマクロ用
        public string TEMSRC       = ""; //specify another template source.
        public string TEMFUNC      = ""; //specify another template function.
        public string PREFIX       = ""; //for another template souce and function.
        public string STATEMACHINE = "STATEMACHINENAME"; //ステートマシン名

        public string TEMSRC_save  = "";  //save TEMSRC for clear
        public string TEMFUNC_save = "" ; //save TEMFUNC for clear

        public string MARK_START  = "";  //インサート用
        public string MARK_END    = "";
        public string TGTFILE     = "";  //インサートターゲットファイル
        public bool   CVTHEXCHAR  = false; // \xXXの変換

        public string PSGGFILE    = "";  //出力ファイルに記録するpsggファイルへの相対パス -- ソースコードからpsggファイルを開く機能に利用するため

        public readonly string CONTENTS1     =  "$contents1$";
        public readonly string CONTENTS1PTN  = @"\$contents1.*?\$";

        public readonly string CONTENTS2  ="$contents2$";
        public readonly string CONTENTS3  ="$contents3$";
        public readonly string PREFIXMACRO ="$prefix$";
        public readonly string STATEMACHINEMACRO = "$statemachine$";

        public readonly string REGEXCONT  = @"\$\/.+\/\$\s*$";      // $/正規表現/$ 
        public readonly string REGEXCONT2 = @"\$\/.+\/->#.+\$\s*$"; // $/正規表現/->#xxx$

        //public readonly string INCLUDEFILE= @"\$include:.+?\$"; //Regexp
        //public readonly string MACRO      = @"$MACRO:.+?\$";    //Regexp

        public string template_src; // buffer
        public string template_func;// buffer
        public Func<int,int,string> getChartFunc; // string = (row,col) Base 1,  as Excel Access
        public Func<string,string>  getMacroValueFunc; // get macro value

        public string setting_ini; // setting_ini text 使用保留

        public List<string> state_list;
        public List<int>    state_col_list;

        public List<string> name_list;
        public List<int>    name_row_list;

        //-- インサートコンバート時に利用する編集禁止マーク
        public bool   USE_DONOTEDIT_MARK;
        public int[]  DONOTEDIT_MARK_COLMNS = new int[] { 36,74,116 };    //指定カラムに表示。これ以上の場合は、４タブで後表示
        //                              1234567890123
        public string DONOTEDIT_MARK = "*DO*NOT*EDIT*";
        //-- 

        public string BRANCHEDIT_NEWLINECHAR = "￢"; //分岐コンディション内の改行コード


        #region init
        public void Init(
            string i_template_src, 
            string i_template_func,
            Func<int,int,string> i_getChartFunc,
            Func<string,string>  i_getMacroValueFunc = null
            )
        {
            template_src      = i_template_src;
            template_func     = i_template_func;
            getChartFunc      = i_getChartFunc;
            getMacroValueFunc = i_getMacroValueFunc;

            _init();
        }

        private void _init()
        {
            state_list     = new List<string>();
            state_col_list = new List<int>();

            name_list     = new List<string>();
            name_row_list = new List<int>();

            for(var c = 1; c <10000; c++)
            {
                var state = getChartFunc(STATE_ROW, c);
                if (!string.IsNullOrEmpty(state))
                {
                    if (RegexUtil.Get1stMatch(@"^[a-zA-Z_][a-zA-Z_0-9]*$",state)==state)
                    {
                        state_list.Add(state);
                        state_col_list.Add(c);
                    }
                }
            }

            for(var r = 1; r < 10000; r++)
            {
                var name = getChartFunc(r, NAME_COL);
                if (!string.IsNullOrEmpty(name))
                {
                    name_list.Add(name);
                    name_row_list.Add(r);
                }
            }
        }
        #endregion

        #region generate
        public void   GenerateSource(string excel, string gendir, string incdir)
        {
            INCDIR = incdir;
            GenerateSource(excel,gendir);
        }
        public void   GenerateSource(string excel, string gendir)
        {
            if(BRKGS)
            {
                System.Diagnostics.Debugger.Break();
            }
            //if(string.IsNullOrEmpty(INCDIR))
            //    INCDIR = gendir;

            //var sm = new SourceControl();
            //sm.G = this;
            //sm.m_excel = excel;
            //sm.m_gendir = gendir;

            //_runSourceControl(sm,SourceControl.MODE.INIT);
            //_runSourceControl(sm,SourceControl.MODE.CVT);

            var sm = new SourceControl();
            sm.G = this;
            sm.m_excel = excel;
            sm.m_gendir = gendir;
            sm.m_cvthexchar = CVTHEXCHAR;

            _runSourceControl(sm, SourceControl.MODE.INIT);
            _runSourceControl(sm, SourceControl.MODE.CVT);

            return;
        }
        public string generate_for_inserting_src(string excel, string template_src_for_inserting, int indent) //throw ! 
        {
            var sm = new SourceControl();
            sm.G = this;
            sm.m_excel = excel;
            sm.m_insert_template_src = template_src_for_inserting;
            sm.m_cvthexchar = CVTHEXCHAR;

            _runSourceControl(sm, SourceControl.MODE.INSERT, indent);

            return sm.m_insert_output;
        }
        public void Prepare() // Prepare for converting
        {
            if(BRKP)
            {
                System.Diagnostics.Debugger.Break();
            }

            var sm = new SourceControl();
            sm.G = this;
            sm.m_excel = null;
            sm.m_gendir = null;
            sm.m_cvthexchar = CVTHEXCHAR;

            _runSourceControl(sm,SourceControl.MODE.INIT);
        }

        private static void _runSourceControl(SourceControl sm, SourceControl.MODE mode, int indent=0)
        {
            sm.mode = mode;
            sm.m_indent = indent;
            
            sm.Start();
            for(var loop = 0;loop <= 10000;loop++)
            {
                if(loop == 10000)
                    throw new SystemException("Unexpected! {96B6D10A-FFF4-4BD4-B9E0-C155CF2C16EB}");

                sm.update();

                if(sm.IsEnd())
                    break;
            }
        }
        public string CreateFunc(string state, string macrobuf = null)
        {
            if (BRKGF)
            { 
                System.Diagnostics.Debugger.Break();
            }
            return CreateFuncWork(state, macrobuf);
        }

        internal string CreateFuncInternal(string state,string macrobuf = null)
        {
            return CreateFuncWork(state, macrobuf);
        }
        internal string CreateFuncWork(string state,string macrobuf)
        {
            var sm = new FunctionControl();
            sm.G = this;
            sm.m_state = state;
            sm.m_macro_buf = macrobuf;
            sm.m_useMacroOrTemplate = !string.IsNullOrEmpty(macrobuf);

            sm.Start();
            for(var loop=0;loop<=10000;loop++)
            {
                if (loop == 10000) throw new SystemException("Unexpected! {D5DF7922-8A36-4458-A4F4-7B80A240EB08}");
                sm.update();
                if (sm.IsEnd()) break;
            }
            return sm.m_result_src;
        }

        [Obsolete]
        public bool createFunc_prepare_obs(string state, ref List<string> lines)
        {
            if (lines == null) return false;              

            var findindex = -1;
            var targetlines = StringUtil.FindMatchedLines(lines,"<<<?",">>>",out findindex);
            if (targetlines == null) return false; 
            if (targetlines.Count < 2) throw new SystemException("Unexpected! {A6446D1F-DFD0-4A63-93C7-299265119AC7}");

            //存在を確認して、残すか消す

            var line0 = targetlines[0];
            var targetname = RegexUtil.Get1stMatch(@"(?!\<\<\<\?)(\w+)",line0);
            if (isExist(state, targetname))
            {
                var size = targetlines.Count;

                //最終行が EOF>>>か？
                bool bEOF = (targetlines[targetlines.Count - 1].ToLower().Contains("eof>>>"));

                //先頭行と最終行の削除
                targetlines.RemoveAt(0);
                targetlines.RemoveAt(targetlines.Count-1);

                if (bEOF) //以降を削除
                {
                    while(lines.Count > findindex + 1)
                    {
                        lines.RemoveAt(lines.Count-1);
                    }
                    size = 1;
                }
                //変換したものに入れ替え
                lines = StringUtil.ReplaceLines(lines,findindex,size,targetlines);
                return true;
            }
            else
            {            
                lines.RemoveRange(findindex,targetlines.Count);
            }            
            return true;
        }
        [Obsolete]
        public bool createFunc_prepare_obs2(string state, ref List<string> lines)
        {
            if (lines == null) return false;

            var findindex = -1;
            var targetlines = StringUtil.FindMatchedLines2(lines, "<<<?", ">>>", out findindex);
            if (targetlines == null) return false;
            if (targetlines.Count < 2) throw new SystemException("Unexpected! {A6446D1F-DFD0-4A63-93C7-299265119AC7}");

            //存在を確認して、残すか消す

            var line0 = targetlines[0];
            var bValid = false;
            var itemname = string.Empty;
            var val = string.Empty;
            var regex = string.Empty;
            var target = RegexUtil.Get1stMatch(@"\<\<\<\?.+\s*$",line0);
            target = target.Substring(4).Trim(); // <<<?を削除
            if (target[0] == '\"') //　　<<<?"文字列"/正規表現/
            {
                var dqw= RegexUtil.Get1stMatch(@"\x22.*\x22",target);
                val = dqw.Trim('\x22');
                regex = target.Substring(dqw.Length);
            }
            else 
            { //  <<<?itemname    または  <<<?itemname/正規表現/
                itemname = RegexUtil.Get1stMatch(@"[0-9a-zA-Z_\-]+", target);
                regex = target.Substring(itemname.Length);
                val = getString2(state, itemname);
            }

            bValid = !string.IsNullOrEmpty(val);

            if (!string.IsNullOrEmpty(regex) && regex.Length > 2)
            {
                if (regex[0]=='/' && regex[regex.Length-1]=='/')
                {
                    regex = regex.Substring(1);
                    regex = regex.Substring(0,regex.Length - 1);

                    var match = RegexUtil.Get1stMatch(regex,val);
                    bValid = !string.IsNullOrEmpty(match);
                }
                else
                {
                    bValid  = false;
                    throw new SystemException("Unexpected! {9280C652-054F-46D2-9340-BC281A2299A7} \n" + line0);
                }
            }

            if (bValid)
            {
                var size = targetlines.Count;

                //最終行が EOF>>>か？
                bool bEOF = (targetlines[targetlines.Count - 1].ToLower().Contains("eof>>>"));

                //先頭行と最終行の削除
                targetlines.RemoveAt(0);
                targetlines.RemoveAt(targetlines.Count - 1);

                if (bEOF) //以降を削除
                {
                    while (lines.Count > findindex + 1)
                    {
                        lines.RemoveAt(lines.Count - 1);
                    }
                    size = 1;
                }
                //変換したものに入れ替え
                lines = StringUtil.ReplaceLines(lines, findindex, size, targetlines);
                return true;
            }
            else
            {
                lines.RemoveRange(findindex, targetlines.Count);
            }
            return true;
        }
        public bool createFunc_prepare(string state, ref List<string> lines)
        {
            var sm = new CfPrepareControl();
            sm.m_state = state;
            sm.m_lines = lines;
            sm.m_parent = this;
            sm.Run();

            lines = sm.m_lines;

            return sm.m_bResult;
        } 


        public bool createFunc_work(string state, ref List<string> lines)
        {
            if (lines == null) return false;

            for(var i = 0; i<lines.Count; i++)
            {
                var tstate = state;
                var line = lines[i];
                var targetvalue = RegexUtil.Get1stMatch(@"\[\[.*?\]\]",line);
                if (string.IsNullOrEmpty(targetvalue))
                {
                    continue;
                }
                var tmp_targetvalue = targetvalue;
                // ::STATE_NAMEの取得
                if (tmp_targetvalue.StartsWith("[[::"))
                {
                    tstate = RegexUtil.Get1stMatch(@"^" + RegexUtil.VARNAME_PATTERN , tmp_targetvalue.Substring(4));
                    if (string.IsNullOrEmpty(tstate)) continue;

                    tmp_targetvalue = tmp_targetvalue.Substring(4);
                    if (string.IsNullOrEmpty(tmp_targetvalue))
                    {
                        continue;
                    }
                    tmp_targetvalue = tmp_targetvalue.Substring(tstate.Length);
                    if (string.IsNullOrEmpty(tmp_targetvalue))
                    {
                        continue;
                    }
                    if (tmp_targetvalue[0]!=':') // [[::STATE:ITEM]]となるのが正しい
                    {
                        continue;
                    }
                    tmp_targetvalue = tmp_targetvalue.Substring(1);
                    tmp_targetvalue = "[[" + tmp_targetvalue; //以降が期待する [[item]]の形
                }

                var name = RegexUtil.Get1stMatch(@"[\!0-9a-zA-Z_\-]+",tmp_targetvalue);
                var macroname = string.Empty;
                var linenum = -1;
                var argnum  = -1;
                var num_colon = StringUtil.CountChar(tmp_targetvalue,':');
                if (num_colon>=1)
                {
                    try {
                        var linenumstr = RegexUtil.GetNthMatch(@":\d+",tmp_targetvalue,1);
                        linenumstr = linenumstr.Substring(1);
                        linenum = int.Parse(linenumstr);
                    } catch (SystemException e)
                    {
                        throw new SystemException("Unpexected! {09F04A64-E5DE-4692-8784-1D0A493715D7} " + e.Message +"\n" + line);
                    }
                }
                if (num_colon>=2)
                {
                    try {
                        var argnumstr = RegexUtil.GetNthMatch(@":\d+", tmp_targetvalue, 2);
                        argnumstr = argnumstr.Substring(1);
                        argnum = int.Parse(argnumstr);
                    } catch (SystemException e)
                    {
                        throw new SystemException("Unpexected! {68DE5327-ECE6-4241-A2E3-CF9F87C9F5F1} " + e.Message + "\n" + line);
                    }
                }
                bool? b_loweCamel_or_upper = null;
                if (tmp_targetvalue.Contains(">>lc")) b_loweCamel_or_upper = true;
                if (tmp_targetvalue.Contains(">>uc")) b_loweCamel_or_upper = false;
                macroname = name;
                if (tmp_targetvalue.Contains("->@"))
                {
                    macroname = RegexUtil.Get1stMatch(@"->@.+?]",tmp_targetvalue);
                    macroname = macroname.Substring(3);
                    macroname = macroname.Substring(0,macroname.Length - 1);
                    if (argnum != -1)
                    {
                        throw new SystemException("Macro cannot use with argnument number. { 68DE5327 - ECE6 - 4241 - A2E3 - CF9F87C9F5F1 } \n" + line);
                    }

                    {//nameの語尾に - があるケースがあった。targetvalueの ->のインデックスまではnameとする。
                        var s = (string)tmp_targetvalue;
                        var idx = s.IndexOf("->@");
                        var newname = s.Substring(0,idx);
                        name = newname.TrimStart('['); 
                    }
                }
                if (string.IsNullOrEmpty(name))
                {
                    continue;
                }
                var val = getString2(tstate, name);
                if (!string.IsNullOrEmpty(val) && linenum>=0)
                {
                    var tmplines = StringUtil.SplitTrimEnd(val,StringUtil._0a[0]);
                    val = linenum < tmplines.Count ? tmplines[linenum] : string.Empty;
                }
                if (!string.IsNullOrEmpty(val) && argnum>=0)
                {
                    var args = StringUtil.SplittComma_And_ApiArges(val);
                    val = argnum < args.Count ? args[argnum] : string.Empty;
                }
                if (b_loweCamel_or_upper!=null)
                {
                    var upper_or_lower = !((bool)b_loweCamel_or_upper);
                    val = StringUtil.convert_to_camel_word(val, upper_or_lower);
                }

                if (name == "brcond") //コンディションの改行対応
                {
                    if (!string.IsNullOrEmpty(val))
                    {
                        if (!string.IsNullOrEmpty(BRANCHEDIT_NEWLINECHAR))
                        {
                            val = val.Replace(BRANCHEDIT_NEWLINECHAR, Environment.NewLine);
                        }
                    }
                }

                var replacevalue = val;
                var replacevalue3 = get_line_macro_value(macroname, replacevalue); // @stateマクロがあれば、各行に適用する

                var tmplines2 = StringUtil.ReplaceWordsInLine(line, targetvalue, replacevalue3);

                lines.RemoveAt(i);
                lines.InsertRange(i, tmplines2);
                return true;

            }
            return false;
        }
        #endregion

        #region insert the output to the target file
        public void InsertOutputToFile(string excel, string targetfile, string incdir)
        {
            if (BRKGS)
            {
                System.Diagnostics.Debugger.Break();
            }

            INCDIR = incdir;
            TGTFILE = targetfile;
            
            var sm = new InsertCodeControl();
            sm.G = this;
            sm.m_excel = excel;
            sm.m_filepath = targetfile;

            sm.MARK_START = MARK_START;
            sm.MARK_END = MARK_END;

            sm.Start();
            for (var loop = 0; loop < 10000; loop++)
            {
                if (sm.IsEnd())
                {
                    break;
                }
                sm.update();
            }
            return;
        }

        #endregion
        // --- tools
        public bool isExist(string state, string name)
        {
            var v = getString2(state, name);
            return !string.IsNullOrWhiteSpace(v);     
        }
        public int getCol(string state)
        {
            var index = state_list.IndexOf(state);
            if (index >=0)
            {
                return state_col_list[index];
            }
            return -1;
        }
        public int getRow(string name)
        {
            var index = name_list.IndexOf(name);
            if (index >= 0)
            {
                return name_row_list[index];
            }
            return -1;
        }
        public string _getString(string state, string name)
        {
            var col = getCol(state);
            var row = getRow(name);

            return getChartFunc(row,col);
        }
        public string getString2(string state, string name)
        {
            var new_state = state;
            for(var loop = 0; loop<10; loop++)
            {
                var val = _getString(new_state,name);
                if (string.IsNullOrEmpty(val))
                {
                    var next_state = _getString(new_state,BASESTATE);
                    if (string.IsNullOrEmpty(next_state))
                    {
                        return val;
                    }
                    else
                    {
                        new_state = next_state;
                        continue;
                    }
                }
                return val;
            }
            throw new SystemException("Unexpected! {A91F45A0-6C2D-42B9-A23D-B8A71F56F1A2}");
        }
    }
}
