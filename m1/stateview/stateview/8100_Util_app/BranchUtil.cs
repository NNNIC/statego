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

namespace stateview {
    //branchの文字列ユーティリティ ※非依存
    public class BranchUtil
    {
        public const string MATCH_BR = @"br_[a-zA-Z0-9_]+";

        public const string BRANCHAPI_IF    ="brif";        //IF を実現するための API
        public const string BRANCHAPI_ELSEIF="brelseif";    //ELSEIF
        public const string BRANCHAPI_ELSE  ="brelse";      //ELSE

        public static bool  EnabledIF { get {  return G.name_list.Contains(G.STATENAME_brcond); } }

        public enum  APIMODE
        {
            NONE,
            API,
            IF,
            ELSEIF,
            ELSE
        }
        public enum VALTYPE //データグリッドのValue部分の種類
        {
            NONE,
            API,
            CONDITION
        }
        public class APILABEL
        {
            public APIMODE mode;
            public string api;
            public string label;
            public string condition;
            public string comment;
        }
        public static List<APILABEL>  GetApiAndLabelListFromState(string state)
        {
            //var list = new List<APILABEL>();

            var sbranch = G.excel_program.GetStringWithBasestate(state, G.STATENAME_branch);
            var sbrcond = G.excel_program.GetStringWithBasestate(state, G.STATENAME_brcond);
            var sbcmt   = G.excel_program.GetStringWithBasestate(state, G.STATENAME_branchcmt);
            return GetApiAndLabelListFromString(sbranch,sbrcond,sbcmt);

        }
        public static List<APILABEL> GetApiAndLabelListFromString(string sb, string sc,string sbc)
        {
            var list = new List<APILABEL>();
            if (string.IsNullOrEmpty(sb)) return list;
            var lines = sb.Split('\x0a');
            var num = 0;
            foreach (var i in lines)
            {
                var l = i.Trim();
                var word = RegexUtil.Get1stMatch(@"^.+\(.+\)", l);
                if (string.IsNullOrEmpty(word)) continue;
                var api = RegexUtil.Get1stMatch(@"^.+\(", word).TrimEnd('(').Trim();
                var lbl = RegexUtil.Get1stMatch(@"\(.+\)", word).TrimStart('(').TrimEnd(')').Trim();
                var mode = GetMode(api);
                var cond = GetConditionFromString(sc, num) ;
                var cmt  = GetCommentFromString(sbc, num);
                list.Add(new APILABEL() { mode = mode, api = api, label = lbl, condition = cond, comment = cmt });
                num++;
            }
            return list;
        }
        public static List<APILABEL> NormalizeLabelListForBranch(List<APILABEL> list)
        {
            //brifc や brelseifc対応
            for (var n = 0; n < list.Count; n++)
            {
                APIMODE mode0 = list[n].mode;
                APIMODE mode1 = n + 1 < list.Count ? list[n + 1].mode : APIMODE.NONE;

                if (mode0 == APIMODE.IF)
                {
                    list[n].api = GetBranchAPI(APIMODE.IF);
                    if (mode1 == APIMODE.ELSEIF || mode1 == APIMODE.ELSE)
                    {
                        list[n].api += "c";
                    }
                }
                if (mode0 == APIMODE.ELSEIF)
                {
                    list[n].api = GetBranchAPI(APIMODE.ELSEIF);
                    if (mode1 == APIMODE.ELSEIF || mode1 == APIMODE.ELSE)
                    {
                        list[n].api += "c";
                    }
                }
            }
            return list;
        }

        //[Obsolete]
        //public static string MakeBranchStringFromApiAndLabelList_obs(List<APILABEL> list)
        //{
        //    var s = string.Empty;
        //    if (list == null) return s;
        //    foreach (var p in list)
        //    {
        //        if (!string.IsNullOrEmpty(s)) s += '\x0a';
        //        s += p.api + "(" + p.label + ");";
        //    }
        //    return s;
        //}
        public static void MakeBranchStringFromApiAndLabelList(List<APILABEL> list, out string branch, out string brcond, out string brcmt)
        {
            var sb = string.Empty;  // branch用
            var sc = string.Empty;  // brcond用
            var sbc = string.Empty; // branch-cmt用
            if (list == null || list.Count == 0)
            {
                branch  = sb;
                brcond  = sc;
                brcmt   = sbc;
                return;
            }
            for(var n=0; n <list.Count; n++)
            {
                var p = list[n];
                
                if (!string.IsNullOrEmpty(sb))  sb += StringUtil._0a;
                if (!string.IsNullOrEmpty(sc))  sc += StringUtil._0a;
                if (!string.IsNullOrEmpty(sbc)) sbc += StringUtil._0a;
                if (!IsValid(p))
                {
                    G.NoticeToUser_warning("Unexpected! {1FB47460-5A81-47C0-ADF7-3E973CF3EC68}\n" + msg_isvalid);
                    continue;
                }
                sb += p.api + "(" + p.label + ");";

                if (string.IsNullOrEmpty(p.condition))
                {
                    sc += "?";
                }
                else
                {
                    sc += p.condition;
                }
                if (string.IsNullOrEmpty(p.comment))
                {
                    sbc += "?";
                }
                else
                {
                    sbc += p.comment;
                }
            }
            branch = sb;
            brcond = sc;
            brcmt  = sbc;
        }
        public static bool IsEqual(List<APILABEL>  a, List<APILABEL> b)
        {
            if (a==null && b==null) return true;
            if (a==null && b.Count == 0) return true;
            if (a.Count == 0 && b==null) return true;

            if (a.Count != b.Count) return false;

            for(var i = 0 ; i<a.Count; i++)
            {
                var v1 = a[i];
                var v2 = b[i];
                if (
                    v1.mode != v2.mode 
                    ||
                    v1.api != v2.api 
                    || 
                    v1.label != v2.label
                    ||
                    v1.condition != v2.condition
                    ||
                    v1.comment != v2.comment
                    ) return false;
            }
            return true;
        }

        public static bool UpdateBranchByApiAndLabelList(string state, List<APILABEL> list)
        {
            var orglist = GetApiAndLabelListFromState(state);
            if (!IsEqual(orglist, list))
            {
                var buf_branch = string.Empty;
                var buf_brcond = string.Empty;
                var buf_brcmt  = string.Empty;
                MakeBranchStringFromApiAndLabelList(list, out buf_branch, out buf_brcond, out buf_brcmt);

                G.excel_program.SetString(state, G.STATENAME_branch, buf_branch);
                G.excel_program.SetString(state, G.STATENAME_brcond, buf_brcond);
                G.excel_program.SetString(state, G.STATENAME_branchcmt, buf_brcmt);
        
                return true;
            }
            return false;
        }

        public static bool SetBranchByApiAndLabelList(string state, List<APILABEL> list)
        {
            return UpdateBranchByApiAndLabelList(state,list);
        }



        public static List<string> GetLabelListFromState(string state)
        {
            var list = new List<string>();
            var apilabellist = GetApiAndLabelListFromState(state);
            if (apilabellist==null) return null;
            foreach(var i in apilabellist)
            {
                list.Add(i.label);
            }
            return list;
        }
        public static bool IsValid_API_BR(string s)
        {
            return RegexUtil.IsMatch(MATCH_BR,s);
        }
        public static APIMODE GetMode(string api)
        {
#if old
            if (api == BRANCHAPI_IF)       return APIMODE.IF; 
            if (api == BRANCHAPI_ELSEIF)   return APIMODE.ELSEIF;
            if (api == BRANCHAPI_ELSE)     return APIMODE.ELSE;
            if (string.IsNullOrEmpty(api)) return APIMODE.NONE;
#else
            Func<string,bool> is_equal = (s)=> {
                if (api == s) return true;
                if (api == s + "c") return true; // "c"がある場合とない場合がある。  cは 後ろに else elif がある場合に表現が異なる言語があるため
                return false;
            };
            if (is_equal(BRANCHAPI_IF))     return APIMODE.IF;
            if (is_equal(BRANCHAPI_ELSEIF)) return APIMODE.ELSEIF;
            if (api == BRANCHAPI_ELSE)      return APIMODE.ELSE;
            if (string.IsNullOrEmpty(api))  return APIMODE.NONE;

#endif
            return APIMODE.API; //過去の資産もあるので、何かあれば apiとする。
        }
        public static string GetBranchAPI(APIMODE mode)
        {
            switch (mode)
            {
                case APIMODE.IF:     return BRANCHAPI_IF;
                case APIMODE.ELSEIF: return BRANCHAPI_ELSEIF;
                case APIMODE.ELSE:   return BRANCHAPI_ELSE;
            }
            return null;
        }
        public static string GetCondition(string state, int num)
        {
            var s = G.excel_program.GetStringWithBasestate(state,G.STATENAME_brcond);
            return GetConditionFromString(s,num);
            //if (string.IsNullOrEmpty(s)) return null;
            //var lines = StringUtil.SplitTrim(s,StringUtil._0a[0]);
            //if (num < lines.Length)
            //{
            //    return lines[num];
            //}
            //return null;
        }
        public static string GetConditionFromString(string s, int num)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var lines = StringUtil.SplitTrim(s, StringUtil._0a[0]);
            if (num < lines.Length)
            {
                return lines[num];
            }
            return null;
        }
        public static string GetCommentFromString(string s, int num)
        {
            if (string.IsNullOrEmpty(s)) return null;
            var lines = StringUtil.SplitTrim(s, StringUtil._0a[0]);
            if (num < lines.Length)
            {
                return lines[num];
            }
            return null;
        }
        public static VALTYPE GetValueType(APIMODE mode)
        {
            switch (mode)
            {
                case APIMODE.API:    return VALTYPE.API;
                case APIMODE.IF:     return VALTYPE.CONDITION;
                case APIMODE.ELSEIF: return VALTYPE.CONDITION;
                case APIMODE.ELSE:   return VALTYPE.NONE;
            }
            return VALTYPE.NONE;
        }
        public static APILABEL GetApiLabelFromDisplayValue(APIMODE mode, string dispval, APILABEL src=null)
        {
            APILABEL p = null;
            if (src != null) //元データは、編集中の手違いによるCondition紛失を防ぐため
            {
                p = src;
            }
            else
            {
                p = new APILABEL();
            }
            var valtype = GetValueType(mode);
            switch(valtype)
            {
                case VALTYPE.API:       p.api       = dispval;    break;
                case VALTYPE.CONDITION: p.condition = dispval;    break;
            }
            var branch_api = GetBranchAPI(mode);
            if (!string.IsNullOrEmpty(branch_api))
            {
                p.api = branch_api;
            }
            return p;
        }
        public static string GetDisplayValue(BranchUtil.APILABEL p)
        {
            var valtype = BranchUtil.GetValueType(p.mode);
            switch (valtype)
            {
                case BranchUtil.VALTYPE.API: return p.api;
                case BranchUtil.VALTYPE.CONDITION: return p.condition;
            }
            return string.Empty;
        }
        public static string msg_isvalid;
        public static bool IsValid(APILABEL p)
        {
            msg_isvalid = null;
            if (p==null)                     { msg_isvalid = "p==null";   return false; }
            if (p.mode == APIMODE.NONE)      { msg_isvalid = "p.mode == APIMODE.NONE"; return false; }
            if (string.IsNullOrEmpty(p.api)) { msg_isvalid = "string.IsNullOrEmpty(p.api)"; return false; }
            var modeapi = GetBranchAPI(p.mode);
            if (string.IsNullOrEmpty(modeapi))
            {
                if (RegexUtil.Get1stMatch(MATCH_BR,p.api) != p.api)
                {
                    msg_isvalid = "RegexUtil.Get1stMatch(MATCH_BR,p.api) != p.api";
                    return false;
                }
            }
            else
            {
                if ( GetMode(modeapi) != GetMode(p.api) ) { msg_isvalid = "modeapi != p.api"; return false; }
            }
            if (string.IsNullOrEmpty( p.label )) { msg_isvalid = "string.IsNullOrEmpty( p.label )"; return false; }
            /* 
            if (GetValueType(p.mode) == VALTYPE.CONDITION && string.IsNullOrEmpty(p.condition))
            {
                return false;
            }　　コンディションに値がなくてもとりあえずＯＫ
            */
            return true;
        }
    }
}
