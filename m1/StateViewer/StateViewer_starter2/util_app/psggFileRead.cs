using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

public class psggFileRead
{
    public string version = null;
    public string xlsfile    = null;

    public bool Read(string file)
    {
        if (string.IsNullOrEmpty(file)) return false;
        if (Path.GetExtension(file).ToLower() != ".psgg") return false;
        if (!File.Exists(file)) return false;
        try {

            var lines = File.ReadAllLines(file, Encoding.UTF8);
            foreach(var l in lines)
            {
                if (version==null)
                {                 //  012345678
                    if (l.StartsWith("version="))
                    {
                        version = l.Trim().Substring(8);
                    }
                }
                if (xlsfile==null)
                {
                                  //  012345
                    if (l.StartsWith("file="))
                    {
                        xlsfile = l.Trim().Substring(5);
                        if (string.IsNullOrEmpty(Path.GetDirectoryName(xlsfile)))
                        {
                            xlsfile = Path.Combine( Path.GetDirectoryName(file), xlsfile);
                        }

                    }
                }
            }
            return (file!=null);
        } catch 
        {
            return false;
        }
    }
}
