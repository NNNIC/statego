//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

public class StringUtil
{
    public static readonly string _0d0a = "\x0d\x0a";
    public static readonly string _0a   = "\x0a";

    public static string[] SplitTrim(string s, char separator)
    {
        if (s==null) return null;
        var tokens = s.Split(separator);
        var list = new List<string>();
        foreach(var t in tokens)
        {
            var t2 = t.Trim();
            list.Add(t2);
        }

        return list.ToArray();
    }
    public static string[] SplitTrimSpaces(string s, char separator)
    {
        if (s==null) return null;
        var tokens = s.Split(separator);
        var list = new List<string>();
        foreach(var t in tokens)
        {
            var t2 = TrimSpaces(t);
            var len = t2.Length;
            list.Add(t2);
        }
        return list.ToArray();
    }
    public static string TrimSpaces(string s)
    {
        var s2 = s.Trim();
        var len = s2.Length;
        for (var i = 0; i < len; i++)
        {
            if (s2.Length > 0 && s2[i] <= 0x20)
            {
                s2 = s2.Substring(1);
            }
            else
            {
                break;
            }
        }
        len = s2.Length;
        for (var i = 0; i < len; i++)
        {
            if (s2.Length > 0 && s2[s2.Length - 1] <= 0x20)
            {
                s2 = s2.Substring(0, s2.Length - 1);
            }
            else
            {
                break;
            }
        }
        return s2;
    }

    // 文字列をできるママでライン化 
    public static string[] ToLines_noTrim(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        var s2 = ConvertNewLineForExcel(s); //全部　0x0aに
        return s2.Split('\x0a');
    }
    public static string MakeALine(string[] lines, string newchar)
    {
        string s = null;
        foreach (var l in lines)
        {
            if (s != null)
            {
                s += newchar;
            }
            s += l;
        }
        return s;
    }
    public static string MakeALine(List<string> lines, string newchar)
    {
        string s = null;
        foreach (var l in lines)
        {
            if (s != null)
            {
                s += newchar;
            }
            s += l;
        }
        return s;
    }

    public static string ConvertNewLineForExcel(string s)
    {
        if (s!=null)
        {
            if (FindNewLineChar(s) == _0d0a)
            {
                return s.Replace(_0d0a,_0a);
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
        if (s!=null)
        {
            var srcnl = FindNewLineChar(s);
            if (srcnl == _0a)
            {
                return s.Replace(_0a,_0d0a);
            }
        }
        return s;
    }
    public static string Get1stLineTrim(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        var lines = SplitTrim(s,_0a[0]);
        if (lines!=null && lines.Length>0)
        {
            return lines[0];
        }
        return string.Empty;
    }
    public static string ConvertNewLineChar(string target, string newlinechar)
    {
        if (target==null) return target;
        var lines = target.Split('\x0a');
        string result = null;
        foreach(var l in lines)
        {
            if (result!=null) result += newlinechar;
            var l2 = l.TrimEnd();
            result += l2;
        }
        return result; 
    }
    public static bool IsEqual(string a, string b)
    {
        if (string.IsNullOrEmpty(a) && string.IsNullOrEmpty(b)) return true;
        var a1 = MakeCompareLineString(a);
        var b1 = MakeCompareLineString(b);

        return a1==b1;
    }
    public static string MakeCompareLineString(string a)
    {
        if (string.IsNullOrEmpty(a)) return string.Empty;

        var s = string.Empty;
        foreach(var c in a)
        {
            if ((int)c < 0x20) continue;
            s += c.ToString();
        }
        return s;
    }

    /// <summary>
    /// null、空白行削除
    /// </summary>
    public static List<string> TrimLines(List<string> l)
    {
        if (l == null) return null;
        var nl = new List<string>();
        foreach(var a in l)
        {
            if (string.IsNullOrEmpty(a)) continue;
            if (string.IsNullOrEmpty(a.Trim())) continue;
            var a2 = a.TrimEnd(); 

            nl.Add(a2);
        }
        return nl;
    }
    public static string[] TrimLines(string[] l)
    {
        if (l == null) return null;
        var nl = new List<string>();
        foreach(var a in l)
        {
            if (string.IsNullOrEmpty(a)) continue;
            if (string.IsNullOrEmpty(a.Trim())) continue;
            var a2 = a.TrimEnd();           

            nl.Add(a2);
        }
        return nl.ToArray();
    }
    public static string TrimLines(string multilinestring, string newchar=null)
    {
        if (string.IsNullOrEmpty(multilinestring)) return multilinestring;
        if (newchar == null)
        {
            newchar = FindNewLineChar(multilinestring);
        }
        if (newchar == null)
        {
            return multilinestring;
        }

        var lines = ToLines_noTrim(multilinestring);
        var lines2 = TrimLines(lines);
        var s = MakeALine(lines2, newchar);
        return s;
    }
    
    /// <summary>
    /// バッファよりワードを検索し、その行数（０基数）を返す。不明時は -1
    /// </summary>
    public static int GetLineNumOfSerchingWord(string s, string w) //バッファよりワードを検索し、その行数（０基数）を返す
    {
        var newchar = FindNewLineChar(s);
        var lines = SplitTrim(s,newchar[0]);
        for(var line = 0; line < lines.Length; line++)
        {
            var lb = lines[line];
            if (lb.Contains(w))
            {
                return line;
            }
        }
        return -1;
    }

    /// <summary>
    /// 改行コード入りの文字列より指定ラインを取得。ラインの基数は０
    /// </summary>
    public static string GetLine(string s, int line)
    {
        var lines = ToLines_noTrim(s);
        if (lines == null || lines.Length == 0)
        {
            return s;
        }
        var val = ArrayUtil.GetVal(lines, line);
        return val;
    }
    /// <summary>
    /// バッファの指定行（基数０）を指定文字列と入れ替え
    /// </summary>
    public static string ReplaceLine(string s, int line, string replace)
    {
        var lines = ToLines_noTrim(s);
        if (lines == null || lines.Length == 0)
        {
            return s;
        }
        if (line < lines.Length)
        {
            lines[line] = replace;
        }
        var newchar = FindNewLineChar(s);
        var val = MakeALine(lines, newchar);
        return val;
    }

    /// <summary>
    /// バッファを行リストにして、開始行と終了行の間を取り出す。行は基数０。　開始行と終了行は含まれる
    /// </summary>
    public static List<string> CropByLineNum(string s, int startNum, int endNum) 
    {

        var list = new List<string>();

        // 改行文字を１文字にして、Splitする
        var newchar = FindNewLineChar(s);
        if (newchar.Length>1)
        {
            s = s.Replace(newchar, newchar[0].ToString());  
        }
        var lines =  s.Split(newchar[0]);

        if (startNum >= 0 && endNum >= 0 && startNum < endNum && endNum < lines.Length)
        {
            bool bIn = false;
            for(var l = 0; l <lines.Length ; l++)
            {
                if (!bIn)
                {
                    if (l == startNum)
                    {
                        list.Add( lines[l] );
                        bIn = true;
                        continue;
                    }
                }
                else
                {
                    list.Add( lines[l] );
                    if (l == endNum)
                    {
                        break;
                    }
                }
            }
            return list;
        }
        return null;
    }

    /// <summary>
    /// 各行の相対位置を保ちつつ冒頭スペースを削除する。
    /// </summary>
    public static List<string> NomalizeWithTrimHeaderSpace(List<string> lines)
    {
#if xx
        var min_space = int.MaxValue; //ラインの最少スペースを求める
        lines.ForEach( i=>
        {
            var spaces = RegexUtil.Get1stMatch(@"^\s*",i);
            if (spaces==null)
            {
                min_space = 0;
            }
            else
            { 
                min_space = Math.Min(spaces.Length, min_space);
            }
        }
        );
        var lines2 = new List<string>();
        for(var l = 0; l < lines.Count; l++)
        {
            var newbuf = lines[l].Substring(min_space);
            lines2.Add(newbuf);
        }
        return lines2;
#else
        if (lines == null || lines.Count==0) return lines;
        var linesTmp = lines.ToArray();

        var indentsize = CountIndent_lines(linesTmp);
        DeleteIndent(ref linesTmp,indentsize);

        var linesTmp2 = DeleteSpaceLinesAtBottom(linesTmp);

        var newlines = new List<string>(linesTmp2);
        return newlines;
#endif

    }

    /// <summary>
    /// 識別子ペア検索
    /// 入れ子対応
    /// </summary>
    public static List<string> FindMatchedLines(string[] lines, string firstmatch, string endmatch, out int firstline, out int lastline, int start_index = 0)
    {
        firstline = -1;
        lastline  = -1;
        if (lines==null) return null;

        var result = new List<string>();

        var bFirstMatchDone = false;
        var pushCounter = 0;
        for(var index = start_index; index < lines.Length; index++)
        {
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
                    if (pushCounter>0)
                    {
                        pushCounter--;
                        continue;
                    }
                    else if (pushCounter == 0)
                    {
                        lastline = index;
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
    public static string convert_to_snake_word_and_lower(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;

        string o = string.Empty;
        
        int  save = 0;
        Func<char,int> ckuplow = (_)=> {
            if ( _ >='a' && _ <='z') return 1;
            if ( _ >='A' && _ <='Z') return 2;
            if ( _ == '_') return 3;
            return 0;
        }; 
        for(var n = 0; n<s.Length; n++)
        {
            var c = s[n];
            var ul = ckuplow(c);
            if (n==0)
            {
                save = ul;
                o += c;
                continue;
            }
            if ( ul == 3) //c=='_'
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
        Func<char,int> ckuplow = (_)=> {
            if ( _ >='a' && _ <='z') return 1;
            if ( _ >='A' && _ <='Z') return 2;
            if ( _ == '_') return 3;
            return 0;
        }; 

        for(var n = 0; n < s.Length; n++)
        {
            var c  = s[n];
            var cs = new string(c,1);
            var ul = ckuplow(c);
            if (n==0)
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
                o  += cs.ToUpper();
                continue;
            }
            save = ul;
            o += cs;
            continue;
        }
        return o;
    }

    public static void DeleteIndent(ref string[] lines, int n)
    {
        if (lines == null || lines.Length == 0)
        {
            return;
        }
        
        for(var c = 0; c < n ; c++)
        {
            for(var l = 0; l < lines.Length; l++)
            {
                var s = lines[l];
                if (string.IsNullOrEmpty(s)) continue;
                var val = (int)s[0];
                if (val <= 0x20)
                {
                    lines[l] = s.Substring(1);
                }
            }
        }
        return;
    }

    public static int CountIndent_lines(string[] lines)
    {
        if (lines == null || lines.Length == 0) return 0;

        var count = int.MaxValue;
        for(var n = 0; n < lines.Length; n++)
        {
            var s = lines[n];
            if (string.IsNullOrEmpty(s)) continue; 
            var lineindent = CountIndent_a_line(s);
            count = Math.Min(count,lineindent);
        }
        return count;
    }
    public static int CountIndent_a_line(string s)
    {
        if (string.IsNullOrEmpty(s)) return int.MaxValue; //対象外
        int c = 0;
        for(var n = 0; n < s.Length; n++)
        {
            var v = (int)s[n];
            if (v <= 0x20)
            {
                c++;
                continue;
            }
            else
            {
                break;
            }
        }
        return c;
    }
    public static string[] DeleteSpaceLinesAtBottom(string[] lines)
    {
        if (lines == null || lines.Length==0) return lines;

        var newlines = new List<string>(lines);
        while(newlines.Count>0)
        {
            var s = newlines[newlines.Count - 1];
            if (string.IsNullOrWhiteSpace(s))
            {
                newlines.RemoveAt(newlines.Count-1);
                continue;
            }
            else
            {
                break;
            }
        }

        return newlines.ToArray();
    }

    public static string[] ToLower(string[] list)
    {
        if (list == null) return null;
        var list2 = new List<string>();
        foreach (var l in list)
        {
            if (l != null)
            {
                list2.Add(l.ToLower());
            }
            else
            {
                list2.Add(l);
            }
        }
        return list2.ToArray();
    }
    public static string[] ToTrimSpaces(string[] list)
    {
        if (list == null) return null;
        var list2 = new List<string>();
        foreach (var l in list)
        {
            if (l != null)
            {
                list2.Add(TrimSpaces(l));
            }
            else
            {
                list2.Add(l);
            }
        }
        return list2.ToArray();
    }

    /// <summary>
    /// DQ(ダブルクォート)に囲まれた最初の文字列を取り出す
    /// 例)  xxx  "vsdfs" xxx  結果 vsdfs
    /// </summary>
    /// <returns></returns>
    public static string FindFirstDQWord(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        var dq1st = s.IndexOf('"');
        if (dq1st < 0) return null;

        if (s.Length < dq1st + 2) return null;   
        // 検証
        // index 0 ->   len:1  < 2(0 + 2)
        // index 1 ->   len:2  < 3(1 + 2) 

        var dq2nd = s.IndexOf('"',dq1st+1);
        var cropstart = dq1st + 1;
        var cropsize = dq2nd - cropstart;

        var a = s.Substring(cropstart, cropsize);
        // 検証
        //  "1" -> dq1=0,dq2=2   cstart=1  csize=1 
        //  ""  -> dq1=0,dq2=1   cstart=1  csize=0

        return a;
    }
}

