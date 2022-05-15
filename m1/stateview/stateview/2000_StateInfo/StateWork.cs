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

using System.Runtime.InteropServices;

namespace stateview
{
    internal class StateWork
    {
        internal static readonly string   NM_STATE     = "state";
        internal static readonly string   NM_STATECMT  = "state-cmt";
        internal static readonly string   NM_NEXTSTATE = "nextstate";
        internal static readonly string   NM_THUMBNAIL = "thumbnail";
        internal static readonly string   NM_BRANCH    = "branch";
        internal static readonly string   NM_BRANCHCMT = "branch-cmt";
        internal static readonly string   NM_BRCOND    = "brcond";
        internal static readonly string   NM_BASESTATE = "basestate";
        internal static readonly string   NM_GOSUBSTATE= "gosubstate";
        internal static readonly string   NM_RETURN    = "return";
        internal static readonly string[] NM_RESERVES  = new string[] {
            NM_STATE,
            NM_STATECMT,
            NM_STATETYP,
            NM_STATEDCO,
            NM_NEXTSTATE,
            NM_THUMBNAIL,
            NM_BRANCH,
            NM_BRANCHCMT,
            NM_BRCOND,
            NM_BASESTATE,
            NM_GOSUBSTATE,
            NM_RETURN
        };

        internal static readonly string   NMS_DIR      = "!dir";
        internal static readonly string   NMS_UUID     = "!uuid";
        internal static readonly string   NMS_POS      = "!pos";

        //2019.10追加 飾り
        internal static readonly string   NM_STATETYP  = "state-typ";
        internal static readonly string   NM_STATEDCO  = "state-dco";

        internal static void create_state_info()
        {
            Func<string,string> DSS = (s)=> { //Delete S_ in state
                if (G.option_delete_s_state_string)
                {
                    if ( !string.IsNullOrEmpty(s) && s.Length > 2 && s.StartsWith("S_")) return s.Substring(2);
                }
                return s;
            };

            Func<string, string> OMBS = (s)=> { //Omit basestate
                if (G.option_omit_basestate_string)
                {
                    return string.Empty;
                }
                return s;
            };

            Func<string,StateInfo.CellData, bool> is_shorttyp = (t,c) => {

                if (t==WordStorage.Store.state_typ_pass) return true; //pass は 無条件でtrue

                if (c!=null) return false; //コメントがあれば、短くしない。
                //var l = new string[] { "start","end","gosub","substart", "subreturn", "loop" };
                var l = new string[] {
                    WordStorage.Store.state_typ_start,
                    WordStorage.Store.state_typ_end,
                    WordStorage.Store.state_typ_gosub,
                    WordStorage.Store.state_typ_substart,
                    WordStorage.Store.state_typ_subreturn,
                    WordStorage.Store.state_typ_loop,
                    WordStorage.Store.state_typ_stop,
                    WordStorage.Store.state_typ_pass
                };
                var ret  = Array.Find(l,_=>_==t)!=null;
                return ret;
            };

            G.state_info_list = new List<StateInfo>();
            foreach(var state in G.state_list)
            {
                var cmtcell = GetCellData( G.excel_program.GetCellWithBasestate(state,NM_STATECMT) ); //コメントセルを最初に取得　※コメントがあると、short stateは使えない

                var si = new StateInfo();
                si.state          = state;
                si.state_typ      = G.excel_program.GetString(state,NM_STATETYP);
                si.basestate      = G.excel_program.GetString(state, NM_BASESTATE);
                si.state_cell     = GetCellData( G.excel_program.GetCell(state,NM_STATE)    );
                si.state_cell.text = (!string.IsNullOrEmpty(si.basestate )) ?  "<" + OMBS(  DSS( si.basestate )  ) + ">" + DSS ( si.state )    : DSS ( si.state );

                si.state_cell.height = DrawUtil.GetTextSize2(G.maingraphs,"XX",si.state_cell.fontname,si.state_cell.fontsize,1000).Height;
                si.state_cell.height = Math.Max(si.state_cell.height, (is_shorttyp(si.state_typ,cmtcell) ? G.state_short_height : G.state_height)   );

                si.state_cell.width =  is_shorttyp(si.state_typ, cmtcell) ? G.state_short_width : G.state_width;

                si.state_cmt_cell = is_shorttyp(si.state_typ, cmtcell) ? null : GetCellData( G.excel_program.GetCellWithBasestate(state,NM_STATECMT) );
                if (si.state_cmt_cell!=null)
                {
                    if (G.comment_block_fixed)
                    { 
                        si.state_cmt_cell.height = G.comment_block_height;
                    }
                    else
                    {
                        var fontname = si.state_cmt_cell.fontname;
                        if (string.IsNullOrEmpty(fontname)) fontname = G.DEFAULT_FONT;

                        var fontsize = si.state_cmt_cell.fontsize;
                        if (G.comment_font_size>0) fontsize = G.comment_font_size;

                        var width = si.state_cmt_cell.width;
                        var ht = DrawUtil.GetTextSize2(G.maingraphs,si.state_cmt_cell.text, fontname,fontsize,width).Height + G.TEXTMARGIN.Y * 2;

                        si.state_cmt_cell.height = Math.Max(G.comment_block_height, ht);
                    }
                }

                si.nextstate      = G.excel_program.GetStringWithBasestate(state,NM_NEXTSTATE);
                si.thumbnail_bmp  = G.excel_program.GetBmp(state,NM_THUMBNAIL);
                //if (G.thumbnailItems!=null)
                //{
                //    foreach(var i in G.thumbnailItems)
                //    {
                //        if (si.thumbnail_bmp!=null) break;
                //        si.thumbnail_bmp = G.excel_program.GetFileThumbnailBmp(state,i);
                //    }
                //}

                // コンテンツは合成して作成
                string content = null;
                StateInfo.CellData tmp = null;
                foreach(var name in G.name_list)
                {
                    if (Array.FindIndex(NM_RESERVES,i=>i==name) >= 0) continue;
                    if (name.EndsWith("-cmt")) continue;
                    if (name.EndsWith("-ref")) continue;
                    if (name.EndsWith("-typ")) continue;
                    if (name.EndsWith("-dco")) continue;
                    if (name.StartsWith("!")) continue; //システム関連除く

                    var s = G.option_hide_basestate_contents ? G.excel_program.GetString(state,name) : G.excel_program.GetStringWithBasestate(state,name);
                    if (string.IsNullOrEmpty(s)) continue;
                    s = s.Trim();
                    if (string.IsNullOrEmpty(s)) continue;

                    if (content!=null) content += "\n";

                    if (G.option_delete_bracket_string)
                    {
                        content += s;
                    }
                    else
                    {
                        content += "[" + name + "]" + s;
                    }

                    if (tmp==null)
                    {
                        tmp = GetCellData(G.excel_program.GetCellWithBasestate(state,name));//予約語以外で見つかった最初の項目
                    }
                }
                if (tmp!=null)
                {
                    if (string.IsNullOrEmpty(tmp.fontname)) tmp.fontname = G.DEFAULT_FONT;

                    tmp.fontsize = (G.contents_font_size > 0 ? G.contents_font_size : G.font_size);
                    if (tmp.fontsize <=0)                   tmp.fontsize = G.DEFAULT_FONTSIZE;

                    tmp.height = DrawUtil.GetTextSize2(G.maingraphs,content,tmp.fontname,tmp.fontsize,tmp.width).Height + G.TEXTMARGIN.Y * 2;
                    tmp.text = content;
                }
                si.content_cell = tmp; //結果

                si.gosub_cell = GetCellData( G.excel_program.GetCellWithBasestate(state,NM_GOSUBSTATE) );


                si.branch_cell = GetCellData( G.excel_program.GetCellWithBasestate(state,NM_BRANCH) );

                //branchの分解
                if (si.branch_cell!=null)
                {
                    si.branch_list =  new List< StateInfo.BranchItem >();

                    var val  = G.excel_program.GetStringWithBasestate(state,NM_BRANCH);
                    var cval = G.excel_program.GetStringWithBasestate(state,NM_BRCOND);
                    var cmt  = G.excel_program.GetStringWithBasestate(state,NM_BRANCHCMT);


                    var items = BranchParse.Parse(val);
                    if (items == null) return;
                    var condlist = StringUtil.SplitTrim(cval,StringUtil._0a[0]);
                    var cmtlist = StringUtil.SplitTrim(cmt,StringUtil._0a[0]);
                    for(var n = 0; n<items.Length; n++)        //foreach(var i in items)
                    {
                        var i = items[n];
                        var item = new StateInfo.BranchItem();
                        item.value = i.value;
                        item.api   = i.api;
                        item.nextstate = i.nextstate;

                        item.mode  = BranchUtil.GetMode(i.api);
                        item.cond  = ArrayUtil.IsValidIndex(condlist,n) ? condlist[n] : "?";
                        item.cmt   = ArrayUtil.IsValidIndex(cmtlist,n) ? cmtlist[n] : "?";

                        item.cond = item.cond.Replace(G.branch_special_newlinechar, "");

                        if (!G.option_hide_branchcmt_onbranchbox  &&  !string.IsNullOrEmpty(item.cmt) && item.cmt!="?")
                        {
                            item.disp = item.cmt;
                        }
                        else
                        { 
                            item.disp  = i.api;

                            switch (item.mode)
                            {
                                case BranchUtil.APIMODE.IF:     item.disp = "if (" + item.cond +")";       break;
                                case BranchUtil.APIMODE.ELSEIF: item.disp = "elif (" + item.cond + ")";    break;
                                case BranchUtil.APIMODE.ELSE:   item.disp = "else";                        break;
                            }
                        }

                        item.cell = si.branch_cell;
                        item.cell.height = DrawUtil.GetTextSize2(G.maingraphs,"XX",si.branch_cell.fontname,si.branch_cell.fontsize,si.branch_cell.width).Height;
                        item.cell.height = Math.Max(item.cell.height, G.state_height);

                        si.branch_list.Add(item);
                    }
                }
                // 飾り
                si.state_typ = G.excel_program.GetString(state,NM_STATETYP);
                si.state_dco = G.excel_program.GetString(state,NM_STATEDCO);

                G.state_info_list.Add(si);
            }
        }

        static StateInfo.CellData GetCellData(ExcelCellCacheItem cell)
        {
            if (cell==null) return null;

            var cd = _getcelldata(cell);
            return cd;
        }

        static StateInfo.CellData _getcelldata(ExcelCellCacheItem cell)
        {
            var s = cell.text;
            if (string.IsNullOrEmpty(s)) return null;
            s  = s.Trim();
            if (string.IsNullOrEmpty(s)) return null;

            var cd = new StateInfo.CellData();

            cd.fontname  = G.font_name;//cell.fontname;
            cd.fontsize  = G.font_size;//cell.fontsize;
            cd.fontcolor = Color.Black;// cell.fontcolor;

            cd.width     = G.state_width;//(float)cell.width;
            cd.height    = 100;//cell.height_px();

            cd.bgcolor   = Color.White;// cell.bgcolor;

            cd.text = s;

            return cd;
        }
        //static StateInfo.CellData _makecelldata(ExcelCellCacheItem cell)
        //{
        //    var s = cell.text;
        //    if (!string.IsNullOrEmpty(s)) {
        //        s  = s.Trim();
        //    }
        //    if (s==null)
        //    {
        //        s = "";
        //    }

        //    var cd = new StateInfo.CellData();

        //    cd.fontname  = G.font_name;//cell.fontname;
        //    cd.fontsize  = G.font_size;//cell.fontsize;
        //    cd.fontcolor = Color.Black;// cell.fontcolor;

        //    cd.width     = G.state_width;//(float)cell.width;
        //    cd.height    = 100;//cell.height_px();

        //    cd.bgcolor   = Color.White;// cell.bgcolor;

        //    cd.text = s;

        //    return cd;
        //}


    }

}
