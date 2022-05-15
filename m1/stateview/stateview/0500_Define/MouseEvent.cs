using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    internal enum MouseEventId
    {
        NONE,
        MOUSEDOWN,
        MOUSEUP,
        MOVE,
        CLICK,
        DOUBLECLICK,

        CANCEL,
        ABORT,   //強制　 セレクト状態をプログラムから解除するため。

        RCLICK, //右クリック(オプションで分別)
    }
}
