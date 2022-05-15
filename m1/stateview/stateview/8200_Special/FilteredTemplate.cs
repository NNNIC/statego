//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

using stateview;
public class FilteredTemplate
{
    const string MARK_STATE     = "<<<?state/";
    const string MARK_STATE_RGX = @"\<\<\<\?state[=\/]{0,1}";

    const string MARK_STATE_TYP = "<<<?state-typ";
    const string MARK_STATE_TYP_RGX = @"\<\<\<\?state-typ[=\/]{0,1}";

    const string MARK_CMNSTART  = "<<<?";
    const string MARK_END       = ">>>";
    const string ESC_GT         = "`____&GT*____`"; // <
    const string ESC_LT         = "`____&LT*____`"; // >
    const string ESC_DOLLER     = "`____&DL*____`"; // $
    const string ESC_BROPEN     = "`____&BO*____`"; // [
    const string ESC_BRCLOSE    = "`____&BC*____`"; // ]



    public static string Make(string state)
    {

        var temp_template = to_special_escape(G.excel_convertsettings.m_template_func);
        if (string.IsNullOrEmpty(temp_template)) return string.Empty;

        // コンバートする
        Converter.Prepare();
        Converter.change_temp_func(temp_template);

        var cvrt_temp_template = Converter.GetFuncSrc(state);

        if (string.IsNullOrEmpty(cvrt_temp_template)) return string.Empty;

        // templateを逆変換
        var fileterd_template = from_special_escape(cvrt_temp_template);

        return fileterd_template;
    }

    private static string to_special_escape(string template)
    {
        // templateの <<<?state/ と <<<?state-typ/を残して
        // 残りの <<<?xxx  >>>を　__#TGT__ xxxx __#TLT__ に変更
        // [[ ]] を __#WBRO__ __#WBRC__に変更
        // $を __#DOL__ に変更

        if (string.IsNullOrEmpty(template)) return template;

        var newlinechars = StringUtil.FindNewLineChar(template);

        var lines = StringUtil.ToLines_noTrim(template);
        if (lines == null) return null;

        var ignore_list = new List<int>();

        for(var index = 0; index < lines.Length; index++)
        {
            var l = lines[index];
            if (string.IsNullOrEmpty(l)) continue;
            var start_mark = string.Empty;
            if (RegexUtil.IsMatch(MARK_STATE_RGX,l))  //if (l.Contains(MARK_STATE))
            {
                start_mark = MARK_STATE;
            }
            if (RegexUtil.IsMatch(MARK_STATE_TYP_RGX,l)) //if (string.IsNullOrEmpty(start_mark))
            {
                if (l.Contains(MARK_STATE_TYP))
                {
                    start_mark = MARK_STATE_TYP;
                }
            }
            if (string.IsNullOrEmpty(start_mark)) continue;
            
            try {             
                var first_index = -1;
                var last_index = -1;
                StringUtil.FindMatchedLines(lines,MARK_CMNSTART,MARK_END,out first_index,out last_index, index);
                ignore_list.Add(first_index);
                ignore_list.Add(last_index);
            } catch (SystemException e)
            {
                return "{9255B1B2-5705-41C6-AF93-4FE84B39EC04} " + e.Message;
            }
        }

        //Escape
        for(var index = 0; index<lines.Length; index++)
        {
            if (ignore_list.IndexOf(index) >= 0) continue;
            var l = lines[index];
            lines[index]  = l.Replace("<",ESC_GT)
                             .Replace(">",ESC_LT)
                             .Replace("$",ESC_DOLLER)
                             .Replace("[",ESC_BROPEN)
                             .Replace("]",ESC_BRCLOSE);
        }

        var os = "-------------------" + newlinechars;
        os    += " FILTERED TEMPLATE"  + newlinechars;
        os    += "-------------------" + newlinechars;
        Array.ForEach(lines,l=> {
            if (!string.IsNullOrEmpty(os)) os += newlinechars;
            os += l;
        });
        


        return os;
    }

    static private string from_special_escape(string s)
    {
        if (string.IsNullOrEmpty(s)) return null;

        return s.Replace(ESC_GT,"<")
                .Replace(ESC_LT,">")
                .Replace(ESC_DOLLER,"$")
                .Replace(ESC_BROPEN,"[")
                .Replace(ESC_BRCLOSE,"]");
    }

    //---
    /// <summary>
    /// テンプレートからアイテム名を取得する
    /// </summary>
    public static List<string> get_names_from_current_filterdtemplate()
    {
        var list = new List<string>();

        var s = G.current_func_template;
        if (string.IsNullOrEmpty(s)) return list;

        var ptns = new string [2] {
            @"\[\[[a-zA-Z_0-9]+[^\s\]]*\]\]", //[[adas]] [[ada-cmt]] [[asa->@sdsa]]
            @"\<\<\<\?[a-zA-Z0-9_]+[=\/]{0,1}" //<<<?cxczxczx
        };
        foreach(var ptn in ptns)
        {
            var matches = RegexUtil.GetAllMatches(ptn,s);
            foreach(var m in matches)
            {
                var name = RegexUtil.Get1stMatch(@"[a-zA-Z0-9_]+",m);
                if (string.IsNullOrEmpty(name)) continue;
                if (!list.Contains(name))
                {
                    list.Add(name);
                }
            }
        }

        //必須追加
        var must_items = new string[] { G.STATENAME_state, G.STATENAME_statecmt, G.STATENAME_basestate, G.STATENAME_thumbnail};
        
        foreach(var item in must_items)
        {
            if (!G.name_list.Contains(item)) continue;
            if (list.Contains(item)) continue;
            list.Add(item);
        }

        //不要項目
        var needless_items = new string[] { G.STATENAME_branch, G.STATENAME_brcond };

        foreach(var item in needless_items)
        {
            if (list.Contains(item))
            {
                list.Remove(item);
            }
        }

        return list;
    }

}

