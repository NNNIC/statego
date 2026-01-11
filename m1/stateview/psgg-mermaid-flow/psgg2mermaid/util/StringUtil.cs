using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;


namespace lib.util
{
    public class StringUtil
    {
        public static readonly string _0d0a = "\x0d\x0a";
        public static readonly string _0a = "\x0a";

        public static List<string> SplitTrim(string s, char separator)
        {
            if (s == null) return null;
            var tokens = s.Split(separator);
            var list = new List<string>();
            foreach (var t in tokens)
            {
                var t2 = t.Trim();
                list.Add(t2);
            }

            return list;
        }
        public static List<string> SplitTrimEnd(string s, char separator)
        {
            if (s == null) return null;
            var tokens = s.Split(separator);
            var list = new List<string>();
            foreach (var t in tokens)
            {
                var t2 = t.TrimEnd();
                list.Add(t2);
            }
            return list;
        }
        public static List<string> SplitTrimKeepSpace(string s, char separator)
        {
            if (string.IsNullOrEmpty(s))
            {
                //return new List<string>() { "" }; 
                var p = new List<string>();
                p.Add("");
                return p;
            }
            var lines = s.Split(separator);
            var outlines = new List<string>();
            foreach (var l in lines)   //元の状態を保つ！
            {
                var nl = l.Trim('\x0d', '\x0a');
                outlines.Add(nl);
            }
            return outlines;
        }
        public static string CutEmptyLines(string s)
        {
            if (s == null) return null;
            var newlinechar = FindNewLineChar(s);
            if (newlinechar == null) return s; //改行コードがない

            var tokens = SplitTrimEnd(s, '\x0a');

            var list = new List<string>();
            foreach (var t in tokens)
            {
                if (string.IsNullOrWhiteSpace(t)) continue;
                list.Add(t);
            }
            var o = string.Empty;
            list.ForEach(i =>
            {
                if (!string.IsNullOrEmpty(o)) o += newlinechar;
                o += i;
            });
            return o;
        }
        public static List<string> CutEmptyLines(List<string> src)
        {
            if (src == null) return null;
            var list = new List<string>();
            foreach (var l in src)
            {
                if (string.IsNullOrWhiteSpace(l))
                {
                    continue;
                }
                list.Add(l);
            }
            return list;
        }

        public static string ConvertNewLineForExcel(string s)
        {
            if (s != null)
            {
                if (FindNewLineChar(s) == _0d0a)
                {
                    return s.Replace(_0d0a, _0a);
                }
            }
            return s;
        }

        public static string FindNewLineChar(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            if (s.Contains(_0d0a)) return _0d0a;
            if (s.Contains(_0a)) return _0a;
            return null;
        }

        public static string ConverNewLineCharForDisplay(string s)
        {
            if (s != null)
            {
                var srcnl = FindNewLineChar(s);
                if (srcnl == _0a)
                {
                    return s.Replace(_0a, _0d0a);
                }
            }
            return s;
        }

        public static List<string> FindMatchedLines(List<string> lines, string firstmatch, string endmatch, out int firstline)
        {
            firstline = -1;
            if (lines == null) return null;

            var result = new List<string>();

            var bFirstMatchDone = false;
            //for(var index = 0; index < lines.Count; index++)
            var index = -1;
            while (true)
            {
                index++;
                if (index >= lines.Count)
                {
                    break;
                }

                var line = lines[index];

                if (!bFirstMatchDone)
                {
                    if (line.Contains(firstmatch))
                    {
                        bFirstMatchDone = true;
                        firstline = index;
                        result.Add(line);
                    }
                    continue;
                }
                else
                {
                    result.Add(line);
                    if (line.Contains(endmatch))
                    {
                        return result;
                    }
                }
            }

            if (bFirstMatchDone)
            {
                throw new SystemException("Can not find end-match");
            }
            return null;
        }
        public static List<string> FindMatchedLines2(List<string> lines, string firstmatch, string endmatch, out int firstline)
        {
            firstline = -1;
            if (lines == null) return null;

            var result = new List<string>();

            var bFirstMatchDone = false;
            var pushCounter = 0;
            //for(var index = 0; index < lines.Count; index++)
            var index = -1;
            while (true)
            {
                index++;
                if (index >= lines.Count)
                {
                    break;
                }

                var line = lines[index];

                if (!bFirstMatchDone)
                {
                    if (line.Contains(firstmatch))
                    {
                        bFirstMatchDone = true;
                        firstline = index;
                        result.Add(line);
                    }
                    continue;
                }
                else
                {
                    result.Add(line);

                    if (line.Contains(firstmatch))
                    {
                        pushCounter++;
                        continue;
                    }
                    else if (line.Contains(endmatch))
                    {
                        if (pushCounter > 0)
                        {
                            pushCounter--;
                            continue;
                        }
                        else if (pushCounter == 0)
                        {
                            return result;
                        }
                    }
                }
            }

            if (bFirstMatchDone)
            {
                throw new SystemException("Can not find end-match");
            }
            return null;
        }

        public static List<string> ReplaceLines(List<string> src, int src_target_start, int src_target_size, List<string> rep)
        {
            if (src == null) return null;

            var result = new List<string>();
            for (var i = 0; i < src_target_start; i++)
            {
                result.Add(src[i]);
            }
            result.AddRange(rep);
            for (var i = src_target_start + src_target_size; i < src.Count; i++)
            {
                result.Add(src[i]);
            }
            return result;
        }
        /// <summary>
        /// 文字列中の対象文字を代替文字に入れ替える
        /// ※代替文字に改行が含まれていた場合、見栄えを調整する。この場合に複数行になる
        /// </summary>
        public static List<string> ReplaceWordsInLine(string line, string target, string replace, bool bTrimEnd = true)
        {
            if (string.IsNullOrEmpty(line)) throw new SystemException("Unexpected! {8F041B67-5F7C-4159-83BC-A0A20858C242}");
            if (string.IsNullOrEmpty(target)) throw new SystemException("Unexpected! {475F3A7E-03A0-4AE0-94AD-8668BDA5B217}");
            if (target.Trim() != target) throw new SystemException("Unexpected! {BC4E8F0B-5DAA-4ED5-9E75-98134929CF0B}");
            if (!line.Contains(target)) throw new SystemException("Unexpected! {D5C8183F-D166-4C6E-AB6B-2E7FD7155696}");

            var replace2 = string.Empty;
            if (!string.IsNullOrEmpty(replace))
            {
                replace2 = replace.Trim();
            }
            var newline = StringUtil.FindNewLineChar(replace2);
            if (newline == null) //1行
            {
                var tmp = line.Replace(target, replace2);
                // return new List<string>() { tmp };
                var p = new List<string>();
                p.Add(tmp);
                return p;
            }
            /*
                複数行
                if ([[hoge]]){ return; }
                  v
                  v
                if (
                    hoge1
                    hoge2
                     :
                            ){ return; }


            */
            List<string> replines = bTrimEnd ? StringUtil.SplitTrimEnd(replace2, '\x0a') : StringUtil.SplitTrim(replace2, '\x0a');
            var firstspace = RegexUtil.Get1stMatch(@"^\s", line);
            var targetindex = line.IndexOf(target);

            var result = new List<string>();

            //1. 第一行：ターゲット手前まで
            {
                var buf = line.Substring(0, targetindex);
                result.Add(buf);
            }
            //2. 代替文字列 先頭に "firstspace" と (targetindex - firstspace.length)分のスペース 
            foreach (var r in replines)
            {
                var buf = string.Empty;
                if (firstspace != null)
                {
                    buf += firstspace + new string(' ', targetindex - firstspace.Length);
                }
                else
                {
                    buf += new string(' ', targetindex);
                }
                buf += r;
                result.Add(buf);
            }
            //3. 最終行：代替文字列と同様の空白、その後にlineのターゲット文字以降を挿入
            {
                var buf = string.Empty;
                if (firstspace != null)
                {
                    buf += firstspace + new string(' ', targetindex + target.Length - firstspace.Length);
                }
                else
                {
                    buf += new string(' ', targetindex + target.Length);
                }
                buf += line.Substring(targetindex + target.Length);
                result.Add(buf);
            }
            return result;
        }
        public static string LineToBuf(List<string> lines, string newlinechar = null)
        {
            if (newlinechar == null) newlinechar = "\n";
            var s = string.Empty;
            foreach (var l in lines)
            {
                if (!string.IsNullOrEmpty(s)) s += newlinechar;
                s += l.TrimEnd();
            }
            return s;
        }
        public static List<string> SplitComma(string i) //カンマ区切りで分割 ダブルクォート対応 \"対応
        {
            if (string.IsNullOrEmpty(i)) return null;

            var s = i.Trim();
            if (string.IsNullOrEmpty(s)) return null;

            var dw = @"\x22((\x5c\x22)|([^\x22]))*?\x22"; // DQに囲まれた文字列　￥”対応

            var p1 = @"[^\x22]+?\x2c"; // "以外が続き 最後が , (カンマ)
            var p2 = @"[^\x22]+?$";    // "以外が続き 最後が 行末
            var p3 = dw + @"\s*\x2c";        //  文字列で最後が,
            var p4 = dw + @"\s*$";           //  文字列で最後が 行末
            var regex = string.Format("^(({0})|({1})|({2})|({3}))", p1, p2, p3, p4);

            var tb = s;
            var list = new List<string>();
            for (var loop = 0; loop <= 100; loop++)
            {
                if (loop == 100) throw new SystemException("Unexpected! {11529044-AA98-47BC-9B8B-A7D2B5322265}");
                var f = RegexUtil.Get1stMatch(regex, tb);
                if (!string.IsNullOrEmpty(f))
                {
                    var f2 = f.Trim(',').Trim();
                    list.Add(f2);
                    tb = tb.Substring(f.Length);
                }
                else
                {
                    break;
                }
            }

            if (list.Count > 0) return list;
            return null;
        }
        /*
            Split buffer to api(arg0,arg1..)
            note :   api() or api retur api wo args;

        */
        public static bool SplitApiArges(string buf, out string api, out List<string> args, out string error)
        {
            api = null;
            args = null;
            error = null;
            if (string.IsNullOrEmpty(buf))
            {
                error = "buf is null";
                return false;
            }
            var sp = buf.IndexOf('(');
            if (sp < 0)
            {
                api = buf;
                return true;
            }
            var ep = buf.IndexOf(')');
            if (ep < sp)
            {
                error = "arg string is invalid. #1";
                return false;
            }
            api = buf.Substring(0, sp);
            //var argstr = buf.Substring(sp, (ep - sp)).TrimStart('(').TrimEnd(')').Trim();
            var argstr = buf.Substring(sp, (ep - sp));
            if (!string.IsNullOrEmpty(argstr))
            {
                argstr = argstr.TrimStart('(');
                if (!string.IsNullOrEmpty(argstr))
                {
                    //argstr = TrimEnd(argstr, ')');
                    argstr = StringUtil.TrimEnd(argstr, ')');
                }
                if (!string.IsNullOrEmpty(argstr)) argstr = argstr.Trim();
            }
            if (string.IsNullOrEmpty(argstr))
            {
                return true;
            }
            var arglist = StringUtil.SplitComma(argstr);
            if (arglist == null)
            {
                error = "unexpected! {87753187-4E54-4E2D-A445-239002F2E59A}";
                return false;
            }
            //arglist.Insert(0,api);
            args = arglist;
            return true;
        }
        public static string TrimEnd(string s, char c) // bc HAXE not suported.
        {
            if (string.IsNullOrEmpty(s)) return s;
            var len = s.Length;
            for(var i = 0; i < len; i++)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    if (s[s.Length - 1] == c)
                    {
                        s = s.Substring(0,s.Length-1);
                    }
                }
            }
            return s;
        }
        public static List<string> SplittComma_And_ApiArges(string s) //API型もカンマ区切のデータも、まとめて引数化
        {
            string api;
            List<string> args;
            string error;

            StringUtil.SplitApiArges(s, out api, out args, out error);
            if (!string.IsNullOrEmpty(error) || api.Contains(","))
            {// カンマリストとみなす
                api = null;
                args = StringUtil.SplitComma(s);
            }
            else
            {
                if (args == null) args = new List<string>();
                args.Insert(0, api);
                api = null;
            }
            return args;
        }
        public static int CountChar(string s, char c)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            var count = 0;
            foreach (var i in s)
            {
                if (i == c) count++;
            }
            return count;
        }

        public static string convert_to_snake_word_and_lower(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            string o = string.Empty;

            int save = 0;
            Func<char, int> ckuplow = (_) =>
            {
                if (_ >= 'a' && _ <= 'z') return 1;
                if (_ >= 'A' && _ <= 'Z') return 2;
                if (_ == '_') return 3;
                return 0;
            };
            //for(var n = 0; n<s.Length; n++)
            var n = -1;
            while (true)
            {
                n++;
                if (n >= s.Length)
                {
                    break;
                }

                var c = s[n];
                var ul = ckuplow(c);
                if (n == 0)
                {
                    save = ul;
                    o += c;
                    continue;
                }
                if (ul == 3) //c=='_'
                {
                    save = 3;
                    o += c;
                    continue;
                }
                if (save == ul) //ulが同じ
                {
                    o += c;
                    continue;
                }
                if (save == 3) //ひとつ前は _ 
                {
                    save = ul;
                    o += c;
                    continue;
                }
                else if (save == 2 && ul == 1) //大文字から小文字はOK
                {
                    save = ul;
                    o += c;
                    continue;
                }
                else //if (ul == 1 || ul == 2 || ul == 0)
                {
                    save = ul;
                    o += '_';
                    o += c;
                    continue;
                }
            }
            var o2 = o.ToLower();
            return o2;
        }

        /// <summary>
        ///  
        ///  input to_hoge 
        ///  
        ///  to upper camel  ToHoge
        ///  to lower camel  toHoge
        /// </summary>
        public static string convert_to_camel_word(string s, bool upperOrLower)
        {
            if (string.IsNullOrEmpty(s)) return s;

            string o = string.Empty;
            int save = 0;
            Func<char, int> ckuplow = (_) =>
            {
                if (_ >= 'a' && _ <= 'z') return 1;
                if (_ >= 'A' && _ <= 'Z') return 2;
                if (_ == '_') return 3;
                return 0;
            };

            //for(var n = 0; n < s.Length; n++)
            var n = -1;
            while (true)
            {
                n++;
                if (n >= s.Length) { break; }

                var c = s[n];
                var cs = new string(c, 1);
                var ul = ckuplow(c);
                if (n == 0)
                {
                    save = ul;
                    o += upperOrLower ? cs.ToUpper() : cs.ToLower();
                    continue;
                }
                if (ul == 3) // 現在 _
                {
                    save = ul;
                    // o +=  更新なし
                    continue;
                }
                if (save == 3) // 先行は _
                {
                    save = ul;
                    o += cs.ToUpper();
                    continue;
                }
                save = ul;
                o += cs;
                continue;
            }
            return o;
        }


    }
}