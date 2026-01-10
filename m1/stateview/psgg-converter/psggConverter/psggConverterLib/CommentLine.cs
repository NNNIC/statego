using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace psggConverterLib
{
    public partial class Convert
    {
        public string GetComment(string s)
        {
            var commentline_format = getMacroValueFunc("commentline");
            if (string.IsNullOrEmpty(commentline_format))
            {
                commentline_format = COMMENTLINE_FORMAT;
            }

            return MacroWork.Convert(commentline_format,s);
        }

    }
}
