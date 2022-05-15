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

namespace stateview._5000_MainForm
{
    /// <summary>
    /// 引数解釈
    ///
    /// arg1 : エクセルファイル
    /// arg2 : 設定ファイル(ini形式)
    ///
    /// ini format
    /// thumbnail=           ; サムネイルに項目のファイルキャッシュを使用する 複数可（項目に値がない場合に次へ）
    /// thumbnailDir=        ; サムネイルイメージが格納されているフォルダ
    /// stateType=           ; ステートのタイプ(about,room,btn,item)を指定
    /// help=                ; ヘルプファイル指定
    /// commonini=           ; 共通iniファイル指定
    ///
    /// </summary>
    public partial class ViewForm
    {
        private void InterpretIniFile(string inifile)
        {
            if (!File.Exists(inifile)) return;
            var txt = File.ReadAllText(inifile,Encoding.GetEncoding("sjis"));
            var ht  = IniUtil.CreateHashtable(txt);

            //{//search=(項目|パス),...   G.searchFilePathList
            //    var s = IniUtil.GetValueFromHashtable("search",ht);
            //    if (!string.IsNullOrWhiteSpace(s))
            //    {
            //        var items = StringUtil.SplitTrim(s,',');
            //        if (items!=null && items.Length>0)
            //        {
            //            G.searchFilePathList = new Dictionary<string, string>();
            //            foreach(var item in items)
            //            {
            //                var i2 = item.Trim('(',')').Trim();
            //                var l = i2.Split('|');
            //                if (l!=null && l.Length==2)
            //                {
            //                    G.searchFilePathList.Add(l[0],l[1]);
            //                }
            //            }
            //        }
            //    }
            //}

            var helpfile     = IniUtil.GetValueFromHashtable("help",ht);
            //G.help_program   = new helpProgram();
            //G.help_program.Load(helpfile);
            //G.uuidstart     = IniUtil.GetDoubleFromHashtable("uuidStart",ht,0);
            //G.ignore_items  = StringUtil.SplitTrim(IniUtil.GetValueFromHashtable("ignore",ht),',');

            //共通
            var commonini    = IniUtil.GetValueFromHashtable("commonini",ht);
            if (File.Exists(commonini))
            {
                var txt2 = File.ReadAllText(commonini,Encoding.GetEncoding("sjis"));
                var ht2  = IniUtil.CreateHashtable(txt2);

                //G.thumbnailDir   =  IniUtil.GetValueFromHashtable("thumbnailDir",ht2);
                //G.varDir = IniUtil.GetValueFromHashtable("varDir",ht2);
                //G.appDir = IniUtil.GetValueFromHashtable("appDir",ht2);
                //G.itemdefFile = IniUtil.GetValueFromHashtable("itemdefFile",ht2);
                //G.gfdefFile   = IniUtil.GetValueFromHashtable("gfdefFile",ht2);

                var helpfile2 = IniUtil.GetValueFromHashtable("commonhelp",ht2);
                //G.help_program.LoadCommonFile(helpfile2);

                var webhelp = IniUtil.GetValueFromHashtable("webhelp",ht2);
                //if (!string.IsNullOrEmpty(webhelp)) G.web_help_js = webhelp;

                //var webinfo = IniUtil.GetValueFromHashtable("webinfo",ht2);
                //if (!string.IsNullOrEmpty(webinfo)) G.web_info_js = webinfo;

                var webinfo_text = IniUtil.GetValueFromHashtable("webinfo_text",ht2);
                //if (!string.IsNullOrEmpty(webinfo_text)) G.web_info_txt = webinfo_text;


                G.but_extra_text = IniUtil.GetValueFromHashtable("butextra_text",ht2);
                G.but_extra_cmd  = IniUtil.GetValueFromHashtable("butextra_cmd",ht2);

                G.external_command = IniUtil.GetValueFromHashtable("build_cmd",ht2);

                G.about_addon_text = IniUtil.GetValueFromHashtable("about_addon",ht2);
            }

            //G.help_program.Interpret();
        }

    }
}
