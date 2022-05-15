using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class CmdLineUtil
{
    public static string Get1stFilename(string cmdline)
    {
        if (string.IsNullOrEmpty(cmdline))
        {
            return "{812639E2-2A94-4A11-9844-43A3EC3A0A81}";
        }
        if (cmdline[0]=='\"')
        {
            var index = cmdline.IndexOf('\"',1);
            if (index < 0)
            {
                return "{086E8544-61B6-475F-B195-013F2DDA0743}";
            }
            var s = cmdline.Substring(1,index - 1);
            // test cmdline=""    s=null
            // test cmdline="1"   s=1
            return s;
        }
        var tokens = cmdline.Split(' ','\t');
        return tokens[0];
    }
    public static bool IsBatch(string cmdline)
    {   
        if (string.IsNullOrEmpty(cmdline))
        {
            return false;
        }

        var file = Get1stFilename(cmdline);
        if (string.IsNullOrEmpty(file) || file[0] =='{')
        {
            return false;
        }
        var ext = Path.GetExtension(file).ToLower();

        if (ext == ".bat") return true;
        if (ext == ".cmd") return true;
        return false;
    }
}

