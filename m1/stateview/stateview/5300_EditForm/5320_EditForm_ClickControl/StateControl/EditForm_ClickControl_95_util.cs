using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class EditForm_ClickControl  {

    string conver_text_from_excel(string s)
    {
        if (s!=null)
        {   
            return s.Replace("\x0a","\x0d\x0a");
        }
        return s;
    }

    string convert_text_to_excel(string s)
    {
        if (s!=null)
        {
            return s.Replace("\x0d\x0a","\x0a");
        }
        return s;
    }

    string get_1st_line(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        var l = s.Split('\x0a');
        if (l==null || l.Length== 0) return string.Empty;
        var n = l[0].Trim();
        return n;
    }
}
