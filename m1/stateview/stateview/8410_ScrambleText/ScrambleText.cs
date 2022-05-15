using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// テキストをスクランブル化して、機密をもらさずに公開を可能にする。
/// </summary>
namespace stateview
{
    public class ScrambleText
    {
        /// <summary>
        /// 0 - なし
        /// 1 - 全部 x
        /// 2 - 記号を残す 大文字(X)小文字(x) 数字(3)
        /// 3 - 記号を残す ランダム　大文字小文字 数字
        /// </summary>
        private static int m_level = 0; 
        private static Dictionary<string,string> m_cachedic;
        public static void setlevel(int level)
        {
            m_level = level;
            m_cachedic = null;
        }
        private static string defined_str(string str)
        {
            if (m_cachedic == null)
            {
                m_cachedic = new Dictionary<string, string>();
                return null;
            }
            if (m_cachedic.ContainsKey(str))
            {
                return m_cachedic[str];
            }
            return null;
        }
        public class lexpart {
            public enum KIND {
                c_le20,
                c_sym,
                c_etc,
                w_ascii,
                w_utf16,
            }
            public KIND kind;
            public string value;
        }
        public static string get(string src)
        {
            if (m_level==0) return src;

            if (string.IsNullOrWhiteSpace(src)) return src;

            var s = defined_str(src);
            if (s!=null) return s;

            var os = "";
            if (m_level == 1) //全部ｘに
            {
                foreach(var c in src)
                {
                    if (c <= ' ') { os += c; continue; }
                    os += "x";
                }
                return os;
            }

            Func<int,char> getc = (i)=> {
                if (i < 0) return (char)0;
                if (i >= src.Length) return (char)0;
                return src[i];
            };

            //簡単なＬｅｘ
            var m_lexlist = new List<lexpart>();
            lexpart part = null;
            Action<lexpart.KIND,char> add = (k,c) => {
                if (k.ToString()[0]=='c')
                {
                    if (part!=null) m_lexlist.Add(part);
                    m_lexlist.Add(new lexpart() { kind = k, value = c.ToString() });
                    part = null;
                    return;
                }
                if (part != null)
                {
                    if (part.kind == k)
                    {
                        part.value += c.ToString();
                        return;
                    }
                    else
                    {
                        m_lexlist.Add(part);
                        part = null;
                    }
                }
                part = new lexpart() { kind = k, value = c.ToString() };
            };
            for(var n = 0;  n<src.Length; n++)
            {
                var v = getc(n);
                if (v <= 0x20) {
                    add(lexpart.KIND.c_le20,v);
                }
                else if ("~`!@#$%^&*()_+-={}[]|\\:;'\",.<>?/".Contains(v)) {
                    add(lexpart.KIND.c_sym,v);
                }
                else if (v < 0x100)
                {
                    add(lexpart.KIND.w_ascii,v);
                }
                else
                {
                    add(lexpart.KIND.w_utf16,v);
                }
            }
            if (part!=null) m_lexlist.Add(part);
            //Ｌｅｘここまで

            if (m_level == 2) //記号を残してあとは xかＵＴＦ１６の■
            {
                foreach(var p in m_lexlist)
                {
                    if (p.kind == lexpart.KIND.w_ascii)
                    {
                        var ow = "";
                        for(var n = 0; n < p.value.Length; n++) {
                            var c = p.value[n];
                            if (char.IsUpper(c)) ow +="X";
                            else if (char.IsDigit(c)) ow += "3";
                            else ow += "x";
                        }
                        p.value = ow;
                    }
                    else if (p.kind == lexpart.KIND.w_utf16)
                    {
                        var ow = "";
                        for(var n = 0; n < p.value.Length; n++) ow += "□";
                        p.value = ow;
                    }
                }
                var nsrc = "";
                foreach(var p in m_lexlist)
                {
                    nsrc += p.value;
                }
                m_cachedic.Add(src,nsrc);
                return nsrc;
            }
            if (m_level == 3) //ランダム文字
            {
                foreach(var p in m_lexlist)
                {
                    if (p.kind == lexpart.KIND.w_ascii)
                    {
                        var ow = "";
                        for(var n = 0; n < p.value.Length; n++) {
                            var c = p.value[n];
                            if (char.IsUpper(c))      ow += (char)('A' + RandomUtil.Select(0,23));
                            else if (char.IsDigit(c)) ow += (char)('0' + RandomUtil.Select(0,9));
                            else ow +=  (char)('a' + RandomUtil.Select(0,23));
                        }
                        p.value = ow;
                    }
                    else if (p.kind == lexpart.KIND.w_utf16)
                    {
                        var ow = "";
                        for(var n = 0; n < p.value.Length; n++) ow += "□";
                        p.value = ow;
                    }
                }
                var nsrc = "";
                foreach(var p in m_lexlist)
                {
                    nsrc += p.value;
                }
                m_cachedic.Add(src,nsrc);
                return nsrc;
            }

            return src;
        } 
    }
}
