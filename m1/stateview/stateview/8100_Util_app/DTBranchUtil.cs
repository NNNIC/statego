using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    //branchの文字列ユーティリティ
    internal class DTBranchUtil
    {
        //ボタンジャンプ命令からラベルを取得する
        public static string GetLabelInBTN(string btn,string s)
        {
            //RBTN_XX(XX)を検索   ex  \b[AR]BTN_BYD\(.+\)
            var rgstr = @"\b[AR]BTN_" + btn + @"\(.+\)";
            var word = RegexUtil.Get1stMatch(rgstr,s);
            if (string.IsNullOrEmpty(word)) return string.Empty;
            
            var word2 = RegexUtil.Get1stMatch(@"\(.+\)",word);
            if (string.IsNullOrEmpty(word2)) return string.Empty;
            
            var label = word2.Trim('(',')').Trim();

            return label;
        }
        //フォーカスジャンプ命令からラベルを習得する
        public static string GetLabelInFC(string s,out int n, out string item)
        {
            n    = -1;
            item = string.Empty;

            //RFCn_XXXX(Label)
            var rgstr = @"[RA]FC[01]_[A-Za-z][_A-Za-z0-9]+\(.+\)";
            var word  = RegexUtil.Get1stMatch(rgstr,s);
            if (string.IsNullOrEmpty(word)) return string.Empty;

            n = int.Parse(word[3].ToString());

            var word2 = RegexUtil.Get1stMatch(@"_[A-Za-z][_A-Za-z0-9]+\(",word);
            if (string.IsNullOrEmpty(word2)) return string.Empty;
            item = word2.Trim('_','(').Trim();

            var word3 = RegexUtil.Get1stMatch(@"\(.+\)",word);
            if (string.IsNullOrEmpty(word3)) return string.Empty;

            var label = word3.Trim('(',')').Trim();
            
            return label;
        }
        //ブランチ文字列からレベルジャンプ命令を行先とペアで取り出す
        public static Dictionary<int, string> GetLevelLabelList(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var lines = s.Split('\x0a');
            var list = new Dictionary<int, string>();
            foreach(var i in lines)
            {
                var l = i.Trim();
                var word = RegexUtil.Get1stMatch(@"^[AR]LV_[0-9]+\(.+\)",l);
                if (string.IsNullOrEmpty(word)) continue;
                var numstr = RegexUtil.Get1stMatch(@"^[0-9]+",word.Substring(4));
                var num    = int.Parse(numstr);
                var pword  = RegexUtil.Get1stMatch(@"\(.+\)",l).Trim('(',')').Trim();
                list.Add(num,pword);
            }

            return list;
        }
        //ブランチ文字列をインデックスで取得する
        public static string GetOneLineByIndex(string s, int index)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var lines = s.Split('\x0a');
            var cur = 0;
            foreach(var i in lines)
            {
                var l = i.Trim();
                if (RegexUtil.IsMatch(@".+\(.+\)",l))
                {
                    if (cur == index)
                    {
                        return l;
                    }
                    else
                    {
                        cur++;
                    }
                }
            }
            return null;
        }
        //ブランチ文字列の先頭から、APIとラベルを取得する
        public static bool GetApiAndLabel(string s, out string api, out string label)
        {
            api = string.Empty;
            label = string.Empty;
            if (string.IsNullOrEmpty(s)) return false;
            s = s.Trim();
            if (!RegexUtil.IsMatch(@".+\(.+\)",s))
            {
                return false;
            }

            api   = RegexUtil.Get1stMatch(@".+\(",s).TrimEnd('(');
            label = RegexUtil.Get1stMatch(@"\(.+\)",s).TrimStart('(').TrimEnd(')');

            return true;
        }
        //ブランチ文字列の先頭から行き先ラベルのみを取得する
        public static string GetLabel(string s)
        {
            var api   = string.Empty;
            var label = string.Empty;

            GetApiAndLabel(s,out api, out label);
            return label;
        }
        //ブランチ文字列の指定番目のラベルを変更する
        public static string SetLebel(string s, int index, string label)
        {
            if (string.IsNullOrEmpty(s)) return s;
            var lines = s.Split('\x0a');
            var cur = 0;
            var outs = string.Empty;
            foreach(var i in lines)
            {
                var l = i.Trim();
                if (RegexUtil.IsMatch(@".+\(.*\)",l))
                {
                    if (cur == index)
                    {
                        var src = RegexUtil.Get1stMatch(@"\(.*\)",l);
                        l = l.Replace(src,"(" + label + ")");
                    }

                    if (outs!=string.Empty)
                    {
                        outs += '\x0a';
                    }
                    outs += l;

                    cur++;
                }
            }
            return outs;


        }

        //ジャンプフラグアイテム
        public class JFItem {
            public string gfname;
            public int    num;
            public string lgoto; 
        };
        public static JFItem[] GetJumpFlagList(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var lines = s.Split('\x0a');
            var list = new List<JFItem>();
            foreach(var i in lines)
            {
                var l = i.Trim();
                var word = RegexUtil.Get1stMatch(@"^[AR]FG[0-9]_[a-z][a-zA-Z0-9]+\(.+\)",l);
                if (string.IsNullOrEmpty(word)) continue;
                var numstr = word.Substring(3,1);
                var num = int.Parse(numstr);
                var name   = RegexUtil.Get1stMatch(@"^[a-z][a-zA-Z0-9]+\(",l.Substring(5)).Trim('('); 
                var pword  = RegexUtil.Get1stMatch(@"\(.+\)",l).Trim('(',')').Trim();
                var item = new JFItem();
                item.gfname = name;
                item.num = num;
                item.lgoto = pword;

                list.Add(item);
            }
            return list.ToArray();
        }
    }
}
