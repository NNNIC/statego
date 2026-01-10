using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psggConverterLib
{
    public partial class Convert
    {
        public string insert_donotedit(string src)
        {
            if (!USE_DONOTEDIT_MARK) return src;
            if (DONOTEDIT_MARK_COLMNS == null || DONOTEDIT_MARK_COLMNS.Length == 0) return src;
            if (string.IsNullOrEmpty(DONOTEDIT_MARK)) return src;
            if (string.IsNullOrEmpty(COMMENTLINE_FORMAT)) return src;

            //m_src --  ソース
            if (string.IsNullOrEmpty(src)) return src;
            var newlinechar = StringUtil.FindNewLineChar(src);
            var lines = StringUtil.SplitTrimEnd(src, newlinechar[newlinechar.Length - 1]);

            var newlines = new List<string>();
            for (var n = 0; n < lines.Count; n++)
            {
                var line = lines[n];
                var line2 = insert_donotedit_line(line);
                newlines.Add(line2);
            }
            var output = StringUtil.LineToBuf(newlines, newlinechar);
            return output;
        }

        private string insert_donotedit_line(string s)
        {
            var G = this;
            //表示の長さを求める。２バイト文字はlength=2
            var displen = 0;
            foreach (var c in s)
            {
                if ((int)c > 255)
                {
                    displen += 2;
                }
                else
                {
                    displen += 1;
                }
            }

            //カラムの決定
            var column = -1;
            for (var n = 0; n < DONOTEDIT_MARK_COLMNS.Length; n++)
            {
                var sample = DONOTEDIT_MARK_COLMNS[n];
                if (sample > displen)
                {
                    column = sample; //決定
                    break;
                }
            }
            if (column < 0) //決定しなかった
            {
                column = ((displen + 3) / 4) * 4;
            }
            var rest = column - displen;
            var cmt = DONOTEDIT_MARK_CMT();
            var news = s + (new string(' ', rest)) + cmt;
            return news;
        }
        public string DONOTEDIT_MARK_CMT()
        {
            return GetComment(DONOTEDIT_MARK);
        }
    }
}

