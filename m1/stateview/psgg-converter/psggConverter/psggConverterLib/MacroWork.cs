using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psggConverterLib
{
    // call from macrocontrol and sourcecontrol
    public class MacroWork
    {
        public const string m_includepattern = @"\$include:.+?\$";
        public const string m_macropattern   = @"\$(macro|m):(.+?)\$";
        public const string m_argpattern     = @"\{%(~{0,1})\d+\}";   //埋め込み側の引数パターン  {%0} または {%~0}  チルダ(~)があると文字列両端のダブルクォートを削除する
        public const string m_numpattern     = @"\{%[Nn]\}";          //埋め込み側の引数パターン  {%N} または {%n} 

        public string           m_prefixpattern  = @"\$prefix\$";
        public readonly string  m_statemachinepattern  = @"\$statemachine\$";
        public readonly string  m_state_machinepattern = @"\$state_machine\$";  //スネーク型に変換
        public readonly string  m_stateMachinePattern  = @"\$stateMachine\$";   // lower camelに変換
        public readonly string  m_StateMachinePattern  = @"\$StateMachine\$";   // Upper camelに変換
        public readonly string  m_lcOrUcPattern        = @"\$(lc|uc):.+?\$";    // 引数をlower camel または upper camelに変換

        public string       m_error;
                            
        bool         m_bValid;
        bool         m_bInclude;
        bool         m_blcOrUc;
        bool         m_bPrefix;
        bool         m_bStatemachine;
        bool         m_b_state_machine; //スネーク型
        bool         m_b_stateMachine;  // lower Camel 型 
        bool         m_b_StateMachine;  // Upper Camel 型

        string       m_matchstr;   //
        string       m_filename;   // for include
        string       m_lcuctext;   // for lc uc
        string       m_fileenc;    // file encoding
        string       m_macrovalue; // ie hoge(a,b);
        string       m_api;        // ie hoge
        List<string> m_args; // {a,b}

        public void Init()
        {
            m_error      = null;

            m_bValid     = false;
            m_bInclude   = false;
            m_blcOrUc    = false;
            m_bPrefix    = false;
            m_matchstr   = null;
            m_filename   = null;
            m_fileenc    = null;
            m_macrovalue = null;
            m_api        = null;
            m_args       = null;

        }

        public bool CheckMacro(string buf)
        {
            string match = RegexUtil.Get1stMatch(m_includepattern,buf); // $include 
            if (!string.IsNullOrEmpty(match))
            {
                m_bValid = true;
                m_bInclude = true;
                m_matchstr = match;

                analyze_include();
            }
            if (!m_bValid)
            {
                match = RegexUtil.Get1stMatch(m_lcOrUcPattern,buf); // $lc:xxx$ or $uc:xxxx$
                if (!string.IsNullOrEmpty(match))
                {
                    m_bValid  = true;
                    m_blcOrUc = true;
                    m_matchstr = match;

                    analyze_lcOrUc();
                }
            }
            if (!m_bValid)
            {
                match = RegexUtil.Get1stMatch(m_prefixpattern,buf);
                if (!string.IsNullOrEmpty(match))
                {
                    m_bValid  = true;
                    m_bPrefix = true;
                    m_matchstr = match;
                }
            }
            if (!m_bValid)
            {
                match = RegexUtil.Get1stMatch(m_statemachinepattern,buf); // ママ
                if (!string.IsNullOrEmpty(match))
                {
                    m_bValid = true;
                    m_bStatemachine = true;
                    m_matchstr = match;
                }
            }
            if (!m_bValid)
            {
                match = RegexUtil.Get1stMatch(m_state_machinepattern,buf); //スネーク型
                if (!string.IsNullOrEmpty(match)) {
                    m_bValid = true;
                    m_b_state_machine = true;
                    m_matchstr = match;
                }
            }
            if (!m_bValid)
            {
               match = RegexUtil.Get1stMatch(m_stateMachinePattern,buf); // lower Camel型
                if (!string.IsNullOrEmpty(match)) {
                    m_bValid = true;
                    m_b_stateMachine = true;
                    m_matchstr = match;
                }
            }
            if (!m_bValid)
            {
               match = RegexUtil.Get1stMatch(m_StateMachinePattern,buf); // Upper Camel型
                if (!string.IsNullOrEmpty(match)) {
                    m_bValid = true;
                    m_b_StateMachine = true;
                    m_matchstr = match;
                }
            }
            if (!m_bValid)
            {
                match = RegexUtil.Get1stMatch(m_macropattern,buf);
                if (!string.IsNullOrEmpty(match))
                {
                    m_bValid   = true;
                    m_bInclude = false;
                    m_matchstr = match;

                    analyze_macro();
                }
            }
            return m_bValid;
        }
        void analyze_include()
        {
                                             // 0123456789
            //m_filename = m_matchstr.Substring(/*$include:*/9).TrimEnd('$');
            var str = m_matchstr.Substring(/*$include:*/9).TrimEnd('$');
            if (str.Contains(','))
            {
                var tokens = str.Split(',');
                if (tokens!=null && tokens.Length >=2)
                {
                    m_filename = tokens[0];
                    m_fileenc  = tokens[1];
                }
                else
                {
                    throw new SystemException("Unexpected! {A496CE7C-9F74-4D7A-A105-B9B469A349D0}");
                }
            }
            else
            {
                m_filename = str;
            }
        }
        void analyze_lcOrUc()
        {
                                               // 01234
            //m_filename = m_matchstr.Substring(/*$lc:*/4).TrimEnd('$');
            var str = m_matchstr.Substring(/*$lc:*/4).TrimEnd('$');
            m_lcuctext = str;
        }
        void analyze_macro()
        {
            if (m_matchstr.StartsWith("$macro:"))
            {
                //01234567
                m_macrovalue = m_matchstr.Substring(/*$macro:*/7).TrimEnd('$');
            }
            else if (m_matchstr.StartsWith("$m:"))
            {
                //0123
                m_macrovalue = m_matchstr.Substring(/*$m:*/3).TrimEnd('$');
            }
            else
            {
                throw new SystemException("Unexpected! {E29A31CE-06CB-4B0B-A403-858269C1E38E}");
            }
            string api;
            List<string> args;
            string error;
            StringUtil.SplitApiArges(m_macrovalue,out api, out args, out error);
            m_api = api;

            if (args==null)
            {
                args = new List<string>();
            }
            if (args!=null)  args.Insert(0,api); //$0-apiとする
            m_args = args;

            m_error = error;
        }
        public bool IsValid()
        {
            return  m_bValid;
        }
        public bool IsPrefix()
        {
            return m_bPrefix;
        }
        public bool IsStatemachine() //ママ
        {
            return m_bStatemachine;
        }
        public bool Is_state_machine() //スネーク型へ
        {
            return m_b_state_machine;
        }
        public bool Is_stateMachine() //lower Camel
        {
            return m_b_stateMachine;
        }
        public bool Is_StateMachine() //Upper camel
        {
            return m_b_StateMachine;
        }
        public bool IsInclude()
        {
            return m_bInclude;
        }
        public bool IsLcUc()
        {
            return m_blcOrUc;
        }
        public string GetLcUcText()
        {
            return m_lcuctext;
        }
        public string GetIncludFilename()
        {
            return m_filename;
        }
        public string GetIncludeFileEnc()
        {
            return m_fileenc;
        }
        public string GetMatchStr()
        {
            return m_matchstr;
        }
        public string GetMacroname()
        {
            return m_api;
        }
        public List<string> GetArgValueList()
        {
            return m_args;
        }

        //埋込用文字列    引数はargpatternで取得した文字列
        //public string GetArgValue(string argstr, bool bAcceptNullArg = false)
        //{
        //    return GetArgValue(argstr, m_args, bAcceptNullArg);
        //}

        // 別からも利用できるように static化
        public static string GetArgValue(string argstr, List<string> args, bool bAcceptNullArg = false)
        {
            if (args==null) return "<!!" + argstr.Replace("{%","//").Trim('<','>') +  "(error:no args in macro)!!>"; //変換できず。
            if (!RegexUtil.IsMatch(m_argpattern,argstr))
            {
                throw new SystemException("Unexpected! {0A4A6F44-838E-44D4-8CCA-873C26573E6B}");
            }
            var numstr = RegexUtil.Get1stMatch(@"\d+",argstr);
            var num = int.Parse(numstr);
            var bDqOff = argstr.Contains("~");

            var v = string.Empty;
            if (bAcceptNullArg)
            {
                if (num  < args.Count)
                {
                    v = args[num];
                }
            }
            else
            {
                if (num>=args.Count) return "<!!" + argstr.Replace("{%", "//").Trim('<','>') + "(error: arg num is grater than args count)!!>"; //変換できず
                v = args[num];
            }

            if (bDqOff)
            {
                v = v.Trim('\"');
            }
            if (!bAcceptNullArg && string.IsNullOrEmpty(v))
            {
                v = "<!!" + argstr.Replace("{%", "//").Trim('<','>') + "(error: arg is null)!!>"; 
            }
            return v;
        }

        // 汎用コンバータ
        public static string Convert(string text, int num, List<string> args,bool bAcceptNullArg=false)
        {
            var src = text;
            for(var loop = 0; loop<=100; loop++)
            {
                if (loop==100) throw new SystemException("Unexpected! {710FA2E8-7740-43F9-8A26-703AF71085C6}\n" + src + " #" + num.ToString());
                
                var match = RegexUtil.Get1stMatch(m_argpattern,src);
                if (!string.IsNullOrEmpty(match))
                {
                    var val = GetArgValue(match,args,bAcceptNullArg);
                    src = src.Replace(match,val);
                    continue;
                }
                match = RegexUtil.Get1stMatch(m_numpattern, src);
                if (!string.IsNullOrEmpty(match))
                {
                    var val = num.ToString();
                    src = src.Replace(match,val);
                    continue;
                }

                break;
            }
            return src;
        }
        public static string Convert(string text, string arg0, string arg1=null, string arg2=null)
        {
            if (string.IsNullOrEmpty(arg0))
            {
                return "(error: arg0 is null {2145EA6E-3B45-47FC-B9FD-B82F56E47D89})";
            }
            var args = new List<string>();
            args.Add(arg0);
            if (arg1!=null) args.Add(arg1);
            if (arg2!=null) args.Add(arg2);

            return Convert(text,0,args);
        }
    }
}
