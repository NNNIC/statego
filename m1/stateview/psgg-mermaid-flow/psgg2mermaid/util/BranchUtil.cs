using System;
using System.Collections.Generic;
using System.Text;

namespace lib.util
{
    public class BranchUtil
    {
        public class Item
        {
            public List<string> br_raw_list;
            public List<string> br_api_list;
            public List<string> br_state_list;
            public List<string> br_cond_list;
            public List<string> br_cmt_list;

            public int count() { return br_api_list != null ? br_api_list.Count : 0;   }
            public string get_api(int n)
            {
                var s = ListUtil.Get(br_api_list, n);
                if (s == null) return string.Empty;

                if (s.StartsWith("brif")) return string.Empty;
                if (s.StartsWith("brelseif")) return string.Empty;
                if (s.StartsWith("brelse")) return "else";

                return s;
            }
            public string get_state(int n)
            {
                var s = ListUtil.Get(br_state_list, n);
                if (s == null) return string.Empty;
                return s;
            }
            public string get_cond(int n)
            {
                var s = ListUtil.Get(br_cond_list, n);
                if (s == null) return string.Empty;
                if (s == "?") return string.Empty;
                return s;
            }
            public string get_cmt(int n)
            {
                var s = ListUtil.Get(br_cmt_list, n);
                if (s == null) return string.Empty;
                return s;
            }
        }

        public static Item Read(string branch, string brcond, string branch_cmt)
        {
            var item = new Item();
            if (string.IsNullOrEmpty(branch)) return null;
            item.br_raw_list = StringUtil.SplitTrim(branch, StringUtil._0a[0]);
            item.br_api_list = new List<string>();
            item.br_state_list = new List<string>();
            for (var n = 0; n < item.br_raw_list.Count; n++)
            {
                var s = item.br_raw_list[n];
                //var api = RegexUtil.Get1stMatch(@"^.+\(", s).TrimEnd('(');
                var api = RegexUtil.Get1stMatch(@"^.+\(", s);//.TrimEnd('(');
                api = StringUtil.TrimEnd(api, '(');
                item.br_api_list.Add(api);
                //var bst = RegexUtil.Get1stMatch(@"\(.+\)", s).TrimStart('(').TrimEnd(')');
                var bst = RegexUtil.Get1stMatch(@"\(.+\)", s).TrimStart('(');
                bst = StringUtil.TrimEnd(bst, ')');
                item.br_state_list.Add(bst);
            }
            if (!string.IsNullOrEmpty(brcond))
            {
                item.br_cond_list = StringUtil.SplitTrim(brcond, StringUtil._0a[0]);
            }
            if (!string.IsNullOrEmpty(branch_cmt))
            {
                item.br_cmt_list =  StringUtil.SplitTrim(branch_cmt, StringUtil._0a[0]);
                for (var n = 0; n < item.br_cmt_list.Count; n++)
                {
                    var a = item.br_cmt_list[n].Trim();
                    item.br_cmt_list[n] = a == "?" ? "" : a;
                }
            }
            return item;
        }

    }
}
