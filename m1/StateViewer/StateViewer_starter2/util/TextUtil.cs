using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateViewer_starter2
{
    class TextUtil
    {
        internal static string BreakUpText(string text, int num_in_aline)
        {
            if (string.IsNullOrEmpty(text) || text.Length < num_in_aline) return text;

            var list = new List<string>();
            var s = text;
            while(s.Length > num_in_aline)
            {
                var a = s.Substring(0,num_in_aline);
                s = s.Substring(num_in_aline);
                list.Add(a);
            }
            if (s.Length>0) list.Add(s);

            var output = string.Empty;
            foreach(var i in list)
            {
                if (output.Length>0) output += Environment.NewLine;
                output += i;
            }
            return output;
        }    
    }
}
