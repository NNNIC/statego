using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib.util
{
    //Haxeへの変換時、Sortができなかったための対処
    public class SortUtil
    {
        public static List<string> Sort(List<string> l)
        {
            var l2 = new List<string>();
            l2.AddRange(l);
            l2.Sort();
            return l2;
        }
    }

}