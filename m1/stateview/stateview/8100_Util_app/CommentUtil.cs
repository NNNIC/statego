using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G=stateview.Globals;

namespace stateview
{
    public static class CommentUtil
    {
        public static string make_commentline(string s)
        {
            if (G.macro_ini != null)
            {
                var commentline_macro = G.macro_ini.GetValue("commentline");
                if (string.IsNullOrEmpty(commentline_macro))
                {
                    G.NoticeToUser_warning("No make comment line.Use default // cmt");
                    commentline_macro = "// {%0}";
                }
                if (commentline_macro != null)
                {
                    var output = commentline_macro.Replace("{%0}", s);
                    return output;
                }
            }
            return s;
        }
    }
}
