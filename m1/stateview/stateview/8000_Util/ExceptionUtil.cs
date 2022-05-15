using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ExceptionUtil
{
    public static string GetDetail(SystemException e)
    {
        if (e==null) return "ExceptionUtil:e = null";
        var s = e.Message;
        s += Environment.NewLine;
        s += e.StackTrace;

        return s;
    }
}
