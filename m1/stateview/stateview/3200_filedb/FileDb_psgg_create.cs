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

namespace StateViewer_filedb
{
    public partial class FileDb
    {
        /*
            psggを作成する。
            1. 高速で作成するため、ファイルDBをAppendするだけ。
        */
        string NL { get { return Environment.NewLine; } }

        public static string MARK_STATECHART_SHEET  { get { return WordStorage.Store.PSGG_MARK_STATECHART_SHEET; } }//    = "------#======*<Guid(D13821FE-FA27-4B04-834C-CEC1E5670F48)>*======#------";
        static string        MARK_VARIOUS_SHEET     { get { return WordStorage.Store.PSGG_MARK_VARIOUS_SHEET   ; } }// = "------#======*<Guid(70C5A739-223A-457D-8AEE-1A0E2050D5AE)>*======#------";
        static string        MARK_BITMAP_DATA       { get { return WordStorage.Store.PSGG_MARK_BITMAP_DATA     ; } }// = "------#======*<Guid(4DC98CBA-6257-4E26-A454-A53F85BC234C)>*======#------";
        static string        MARK_VARIOUS_BEGIN     { get { return WordStorage.Store.PSGG_MARK_VARIOUS_BEGIN   ; } }// = "###VARIOUS-CONTENTS-BEGIN###";
        static string        MARK_VARIOUS_END       { get { return WordStorage.Store.PSGG_MARK_VARIOUS_END     ; } }// = "###VARIOUS-CONTENTS-END###";
        static string        MARK_BITMAP_BEGIN      { get { return WordStorage.Store.PSGG_MARK_BITMAP_BEGIN    ; } }// = "###BITMAP-DATA-BEGIN###";
        static string        MARK_BITMAP_END        { get { return WordStorage.Store.PSGG_MARK_BITMAP_END      ; } }// = "###BITMAP-DATA-END###";

        public bool CreatePsgg(
                        string i_psggfile,
                        string guid,
                        string readfrom,
                        string savemode,
                        string checkwrite
            )
        {
            G.NoticeToUser("START TO CREATE PSGG");
            var starttime = DateTime.Now;

            // USE temp.psgg before update
            var testout = Path.Combine(m_temp_folder,"temp.psgg");
            File.Copy(i_psggfile,testout,true);
            var psggfile = testout;

            // 
            if (!File.Exists(psggfile))
            {
                G.NoticeToUser_warning("{187055ED-3DB3-4CAA-94D6-FC8F69E2E455}");
                return false;
            }


            //0.ヘッダのみに

            var header = make_psgg_headerinfo_text(
                         Path.GetFileNameWithoutExtension(i_psggfile) + ".xlsx",
                         guid,
                         readfrom,
                         savemode,
                         checkwrite
                ); // get_psgg_file_text_header();

            if (string.IsNullOrEmpty(header)) {
                G.NoticeToUser_warning("{B7085875-CCF7-4992-91DC-2956E73E9C42}");
                return false;
            }
            header = header.Trim() + NL;
            File.WriteAllText(psggfile,header,Encoding.UTF8);

            try { 
                //1 Open

                var fs = FileUtil.AppendOpen(psggfile);
                Action<string> appendtext = (s) => {
                    FileUtil.AppendUTF8Text(fs,s);
                };
                Action<string> appendfile = (s) => {
                    FileUtil.AppendUTF8TextFile(fs,s);
                };

         　     //2. STATECHART SHEET
                appendtext(NL + MARK_STATECHART_SHEET + NL + NL);
                appendtext("sheet=state-chart" + NL);
                appendfile(m_statechart_manager_file);
                foreach(var fi in (new DirectoryInfo(m_statechart_data_folder).GetFiles()))
                {
                    appendfile(fi.FullName);
                }
                appendtext(NL);

                //3. VARIOUS SHEET
                Action<string> append_various_file = (f) =>
                {
                    if (!File.Exists(f)) return;
                    appendtext(NL + MARK_VARIOUS_SHEET + NL + NL);
                    appendtext("sheet=" + Path.GetFileNameWithoutExtension( f ) + NL + NL);
                    appendtext(MARK_VARIOUS_BEGIN + NL);
                    appendfile(f);
                    appendtext(NL + MARK_VARIOUS_END + NL);
                    appendtext(NL);
                };

                var sheets = new string[] {
                    G.sheetconfig,
                    G.sheettempsrc,
                    G.sheettempfunc,
                    G.sheetsetting,
                    G.sheethelp,
                    G.sheetitems,
                };

                foreach(var s in sheets)
                {
                    var file = Path.Combine(m_various_folder,s + ".txt");
                    append_various_file(file);
                }
                appendtext(NL);

                //4. BITMAP DATA
                appendtext(NL + MARK_BITMAP_DATA + NL + NL);
                foreach(var fi in (new DirectoryInfo(m_statechart_bmp_folder)).GetFiles("*.txt"))
                {
                    appendtext("hash=" + Path.GetFileNameWithoutExtension( fi.Name) + NL + NL);
                    appendtext(MARK_BITMAP_BEGIN + NL);
                    appendfile(fi.FullName);
                    appendtext(NL);
                    appendtext(MARK_BITMAP_END + NL);
                    appendtext(NL);
                }
                appendtext(NL);

                //5. Close                    !! これにより１秒以内に収まる！
                FileUtil.AppendClose(fs);

                //更新
                File.Copy(testout, i_psggfile, true);

                var diff = (DateTime.Now - starttime).TotalSeconds;
                G.NoticeToUser("PSGG CREATING TIME : " + diff.ToString());

                return true;
            }
            catch (SystemException e)
            {
                G.NoticeToUser_warning("{96ADBDEC-3232-43C8-A3F4-4751DC425F2F}" + e.Message);
            }
            return false;

        }

        public bool CreatePsgg(string psggfile)
        {
            return CreatePsgg(
                psggfile ,
                G.psgg_header_info_guid ,
                G.psgg_header_info_read_from,
                G.psgg_header_info_save_mode_withexcel ? "with_excel" : "psgg_only"  ,
                G.psgg_header_info_check_excel_writable                   
            );

            //G.NoticeToUser_warning("START TO CREATE PSGG");
            //var starttime = DateTime.Now;

            //// USE temp.psgg before update
            //var testout = Path.Combine(m_temp_folder,"temp.psgg");
            //File.Copy(psggfile,testout,true);
            //psggfile = testout;

            //// 
            //if (!File.Exists(psggfile))
            //{
            //    G.NoticeToUser_warning("{187055ED-3DB3-4CAA-94D6-FC8F69E2E455}");
            //    return false;
            //}


            ////0.ヘッダのみに

            //var header = make_psgg_headerinfo_text(); // get_psgg_file_text_header();
            //if (string.IsNullOrEmpty(header)) {
            //    G.NoticeToUser_warning("{B7085875-CCF7-4992-91DC-2956E73E9C42}");
            //    return false;
            //}
            //header = header.Trim() + NL;
            //File.WriteAllText(psggfile,header,Encoding.UTF8);

            //try { 
            //    //1 Open

            //    var fs = FileUtil.AppendOpen(psggfile);
            //    Action<string> appendtext = (s) => {
            //        FileUtil.AppendUTF8Text(fs,s);
            //    };
            //    Action<string> appendfile = (s) => {
            //        FileUtil.AppendUTF8TextFile(fs,s);
            //    };

         　  //   //2. STATECHART SHEET
            //    appendtext(NL + MARK_STATECHART_SHEET + NL + NL);
            //    appendtext("sheet=state-chart" + NL);
            //    appendfile(m_statechart_manager_file);
            //    foreach(var fi in (new DirectoryInfo(m_statechart_data_folder).GetFiles()))
            //    {
            //        appendfile(fi.FullName);
            //    }
            //    appendtext(NL);

            //    //3. VARIOUS SHEET
            //    Action<string> append_various_file = (f) =>
            //    {
            //        if (!File.Exists(f)) return;
            //        appendtext(NL + MARK_VARIOUS_SHEET + NL + NL);
            //        appendtext("sheet=" + Path.GetFileNameWithoutExtension( f ) + NL + NL);
            //        appendtext(MARK_VARIOUS_BEGIN + NL);
            //        appendfile(f);
            //        appendtext(NL + MARK_VARIOUS_END + NL);
            //        appendtext(NL);
            //    };

            //    var sheets = new string[] {
            //        G.sheetconfig,
            //        G.sheettempsrc,
            //        G.sheettempfunc,
            //        G.sheetsetting,
            //        G.sheethelp,
            //        G.sheetitems,
            //    };

            //    foreach(var s in sheets)
            //    {
            //        var file = Path.Combine(m_various_folder,s + ".txt");
            //        append_various_file(file);
            //    }
            //    appendtext(NL);

            //    //4. BITMAP DATA
            //    appendtext(NL + MARK_BITMAP_DATA + NL + NL);
            //    foreach(var fi in (new DirectoryInfo(m_statechart_bmp_folder)).GetFiles("*.txt"))
            //    {
            //        appendtext("hash=" + Path.GetFileNameWithoutExtension( fi.Name) + NL + NL);
            //        appendtext(MARK_BITMAP_BEGIN + NL);
            //        appendfile(fi.FullName);
            //        appendtext(NL);
            //        appendtext(MARK_BITMAP_END + NL);
            //        appendtext(NL);
            //    }
            //    appendtext(NL);

            //    //5. Close                    !! これにより１秒以内に収まる！
            //    FileUtil.AppendClose(fs);

            //    //if (!bTest)
            //    //{ 
            //    //    //更新
            //    //    File.Copy(testout,G.psgg_file,true);
            //    //}
            //    //
            //    var diff = (DateTime.Now - starttime).TotalSeconds;
            //    G.NoticeToUser_warning("PSGG CREATING TIME : " + diff.ToString());

            //    return true;
            //}
            //catch (SystemException e)
            //{
            //    G.NoticeToUser_warning("{96ADBDEC-3232-43C8-A3F4-4751DC425F2F}" + e.Message);
            //}
            //return false;
        }

        public string get_psgg_file_text_header()
        {
            var text = File.ReadAllText(G.psgg_file,Encoding.UTF8);
            var index = text.IndexOf(StateViewer_filedb.FileDb.MARK_STATECHART_SHEET);
            if (index > 0)
            {
                text = text.Substring(0,index);
            }
            return text;
        }

        public string make_psgg_headerinfo_text()
        {
            //var nl = System.Environment.NewLine;
            //var s = ";PSGG Editor Backup File"                  + nl;
            //s    += "version=" + WordStorage.Store.psgg_ver     + nl;
            //s    += "file=" + G.psgg_header_info_file           + nl;
            //s    += "guid=" + G.psgg_header_info_guid           + nl;
            //s    += ";set read_from  excel or psgg"             + nl;
            //s    += "read_from=" + G.psgg_header_info_read_from + nl;
            //s    += ";set savemode with_excel or psgg_only"     + nl;
            //s    += "save_mode=" + (G.psgg_header_info_save_mode_withexcel ? "with_excel" : "psgg_only" ) + nl;
            //s    += ";set check_excel_writable yes or no"       + nl;
            //s    += "check_excel_writable=" + G.psgg_header_info_check_excel_writable + nl;
            //s    +=  nl;

            //return s;

            return make_psgg_headerinfo_text(
                file      : G.psgg_header_info_file ,
                guid      : G.psgg_header_info_guid ,
                readfrom  : G.psgg_header_info_read_from,
                savemode  : G.psgg_header_info_save_mode_withexcel ? "with_excel" : "psgg_only"  ,
                checkwrite: G.psgg_header_info_check_excel_writable
                );
        }

        public string make_psgg_headerinfo_text(
            string file,
            string guid,
            string readfrom,
            string savemode,
            string checkwrite
            )
        {
            var nl = System.Environment.NewLine;
            //var s = ";PSGG Editor Backup File"                  + nl;
            //s    += "version=" + WordStorage.Store.psgg_ver     + nl;
            //s    += "file=" + G.psgg_header_info_file           + nl;
            //s    += "guid=" + G.psgg_header_info_guid           + nl;
            //s    += ";set read_from  excel or psgg"             + nl;
            //s    += "read_from=" + G.psgg_header_info_read_from + nl;
            //s    += ";set savemode with_excel or psgg_only"     + nl;
            //s    += "save_mode=" + (G.psgg_header_info_save_mode_withexcel ? "with_excel" : "psgg_only" ) + nl;
            //s    += ";set check_excel_writable yes or no"       + nl;
            //s    += "check_excel_writable=" + G.psgg_header_info_check_excel_writable + nl;
            //s    +=  nl;

            var s = ";PSGG Editor Backup File"                  + nl;
            s    += "version=" + WordStorage.Store.psgg_ver     + nl;
            s    += "file=" + file                              + nl;
            s    += "guid=" + guid                              + nl;
            s    += ";set read_from  excel or psgg"             + nl;
            s    += "read_from=" + readfrom                     + nl;
            s    += ";set savemode with_excel or psgg_only"     + nl;
            s    += "save_mode=" + savemode                     + nl;
            s    += ";set check_excel_writable yes or no"       + nl;
            s    += "check_excel_writable=" + checkwrite        + nl;
            s    +=  nl;

            return s;
        }


    }
}
