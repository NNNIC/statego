using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class WebBrowserFunc
    {
        public void open(string s)
        {
            ExecUtil.execute_start(s,"");
        }
    }
}
