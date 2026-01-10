using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace psggConverterLib
{
    public partial class Convert
    {
        //var replacevalue3  = get_line_macro_value(namereplacevalue2);
        public string get_line_macro_value(string macroname, string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var macrovalue = getMacroValueFunc("@" + macroname);
            if (string.IsNullOrEmpty(macrovalue)) return s; //null時は、変更なし

            var lines = StringUtil.SplitTrim(s,'\x0a');
            var result = new List<string>();


            // 各ラインを args に変換
            // カンマ区切りはその通りに args
            // api(a,b..)は api を arg0 として
            int linenum = 0;
            foreach(var l in lines)
            { 
                if (string.IsNullOrEmpty(l)) continue;

                //string api;
                //List<string > args;
                //string error;

                //StringUtil.SplitApiArges(l,out api, out args, out error);
                //if (!string.IsNullOrEmpty(error) || api.Contains(","))
                //{// カンマリストとみなす
                //    api = null;
                //    args = StringUtil.SplitComma(l);
                //}
                //else
                //{
                //    if (args == null) args = new List<string>();
                //    args.Insert(0,api);
                //    api = null;
                //}

                var args = StringUtil.SplittComma_And_ApiArges(l);

                // この時点で argsリスト完成
                var text = MacroWork.Convert(macrovalue, linenum,args,true);
                result.Add(text);
                linenum++;
            }

            return StringUtil.LineToBuf(result,NEWLINECHAR);
        }
    }
}
