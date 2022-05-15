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
//>>>
namespace StateViewer_starter2 { 
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

    // 文字列をできるママでライン化 
    public static string[] ToLines_noTrim(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;
        var s2 = ConvertNewLineForExcel(s); //全部　0x0aに
        return s2.Split('\x0a');
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
    public static List<string> TrimLines(List<string> l)
    {
        if (l == null) return null;
        var nl = new List<string>();
        foreach(var a in l)
        {
            if (string.IsNullOrEmpty(a)) continue;
            nl.Add(a);
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
            nl.Add(a);
        }
        return nl.ToArray();
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
}
}