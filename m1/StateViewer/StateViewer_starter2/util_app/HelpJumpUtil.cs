using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HelpJumpUtil
{
    public static void Jump(string filename_wo_langchar, string id_wo_sharp, bool bJorE)
    {
        var url = "https://statego.programanic.com/tec3/" +
                  filename_wo_langchar +  ( (bJorE ? "_j.html" :"_e.html" ) ) +
                  "#" + id_wo_sharp;
        ExecUtil.execute_start(url,"");
    }
}

