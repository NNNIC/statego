using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


public class IncludeFile
{
    public static string readfile(psggConverterLib.Convert g, string matchstr,string file, string enc)
    {
        if(string.IsNullOrEmpty(enc)) {
            enc = "utf-8";
        }
        var filepath = Path.Combine(g.INCDIR,file);
        if(!File.Exists(filepath))
        {
            filepath = Path.Combine(g.XLSDIR,file);
            if(!File.Exists(filepath))
            {
                filepath = Path.Combine(g.GENDIR,file);
            }
        }
        var text = string.Empty;
        if(File.Exists(filepath))
        {
            try
            {
                text = File.ReadAllText(filepath,Encoding.GetEncoding(enc));
            }
            catch(SystemException e)
            {
                text = string.Format("(error: can not read : {0})",e.Message);
            }
        }
        else
        {
            text = "(error: file not found : " + filepath + ")";
        }
        return text;
    }

}

