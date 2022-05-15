using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    public static class BranchParse
    {
        public class Item
        {
            public string value;
            public string api;
            public string nextstate;
        }

        public static Item[] Parse(string val)
        {
            if (string.IsNullOrEmpty(val)) return null;
            val = val.Trim();
            if (string.IsNullOrEmpty(val)) return null;

            var list = new List<Item>(); 

            var lines = val.Split('\x0a');
            foreach(var i in lines)
            {
                var l = i.Trim();
                if (string.IsNullOrEmpty(l)) continue;
                var idx1 = l.IndexOf('(');
                if (idx1 < 0) continue;
                var idx2 = l.IndexOfAny(new char[2] { ',',')' },idx1+1);
                if (idx2 < 0) continue;

                var item = new Item();
                item.value     = l;
                item.api       = l.Substring(0,idx1);
                item.nextstate = l.Substring(idx1+1, idx2 -idx1 -1);

                list.Add(item);
            }

            return list.ToArray();
        }

        public static string Replace1stParameter(string l, string param)
        {
            var idx1 = l.IndexOf('(');
            var idx2 = l.IndexOfAny(new char[2] { ',',')' },idx1+1);

            var text = l.Substring(0,idx1);
            text += "(";
            text += param;
            text += l.Substring(idx2);

            return text;
        }
    }
}
