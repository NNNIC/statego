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
            psggファイルを読んで、filedbを作成する。
        */
        public void ReadPsgg(string psggfile, string guid)
        {
            initdb_folders(guid);

            m_state_chart = new state_chart();

            var alltext = File.ReadAllText(psggfile,Encoding.UTF8);

            var index_statechart = alltext.IndexOf(MARK_STATECHART_SHEET);
            var index_first_vairous_sheet = alltext.IndexOf(MARK_VARIOUS_SHEET,index_statechart + MARK_VARIOUS_SHEET.Length);
            var index_bmp_data = alltext.IndexOf(MARK_BITMAP_DATA, index_first_vairous_sheet + MARK_VARIOUS_SHEET.Length);
                        
            psgg_crop_statechart(alltext, index_statechart, index_first_vairous_sheet);            

            var current_index_various_sheet = index_first_vairous_sheet;
            while(current_index_various_sheet > 0)
            {
                var next_index_various_sheet = alltext.IndexOf(MARK_VARIOUS_SHEET,current_index_various_sheet + MARK_VARIOUS_SHEET.Length);
                psgg_crop_various_sheet(alltext,current_index_various_sheet,next_index_various_sheet);

                current_index_various_sheet = next_index_various_sheet;
            }

            var hashbmpdic = psgg_crop_bitmapdata(alltext,index_bmp_data);

            m_state_chart.SetHashBmpDic(hashbmpdic);

            
        }
        void psgg_crop_statechart(string alltext, int start, int end)
        {
            var s= alltext.Substring(start,end-start);
            m_state_chart.InitPsggStateChart(s);
        }
        void psgg_crop_various_sheet(string alltext, int start, int end)
        {
            if (start > 0 && end > 1 && start >= end) throw new SystemException("{85D875F4-F641-4A34-B3B3-AAD2979369FD}");

            var s = string.Empty;
            if (end > 0) { 
                s= alltext.Substring(start,end-start);
            }
            else
            {
                s = alltext.Substring(start);
            }
            var sheetindex=s.IndexOf("sheet"); //念のため一部を取り出す
            if (sheetindex < 0) throw new SystemException("{A39F1783-8B58-4E54-9FAE-423074D8FD3E}");

            var tmp = string.Empty;
            if (s.Length < sheetindex + 80)
            { 
                tmp = s.Substring(sheetindex);
            }
            else
            {
                tmp = s.Substring(sheetindex,80);
            }
            var sheet = IniUtil.GetValue("sheet",tmp);
            if (string.IsNullOrEmpty(sheet)) throw new SystemException("{33F7EC39-C10F-4B02-8784-5D732CD75563}");

            //ファイル内容をとる
            var startindex = s.IndexOf(MARK_VARIOUS_BEGIN);
            if (startindex < 0) throw new SystemException("{7999E4E3-DEC8-4EF5-A8F6-F601121659DA}");
            var endindex = s.IndexOf(MARK_VARIOUS_END,startindex + MARK_VARIOUS_BEGIN.Length);
            if (endindex < 0) throw new SystemException("{EE9BE0D4-F8DD-4898-8341-FD0E35B8462B}");

            var contents = s.Substring(startindex, endindex - startindex );
            contents = contents.Substring(MARK_VARIOUS_BEGIN.Length);
            contents = contents.Trim(); //冒頭と後尾の改行取る

            if      (sheet == G.sheetconfig)   m_sheet_config_val             = contents;
            else if (sheet == G.sheettempsrc)  m_sheet_template_source_val    = contents;
            else if (sheet == G.sheettempfunc) m_sheet_template_func_val = contents;
            else if (sheet == G.sheetsetting)  m_sheet_setting_ini_val        = contents;
            else if (sheet == G.sheethelp)     m_sheet_help_val               = contents;
            else if (sheet == G.sheetitems)    m_sheet_items_val              = contents;
            else
            {
                throw new SystemException("{AE642329-40FA-4CB7-887C-3034CDAA4AF7}");
            }
        }

        Dictionary<string,Bitmap> psgg_crop_bitmapdata(string s, int index)
        {
            if (index < 0) return null;
            if (s.IndexOf(MARK_BITMAP_DATA)!=index) throw new SystemException("{B9D2D8DF-228B-4D8A-807C-291DF8D0FDF3}");

            var dic = new Dictionary<string,Bitmap>();

            var cur = index + MARK_BITMAP_DATA.Length;
            while(cur >= 0)
            {
                var begin = s.IndexOf(MARK_BITMAP_BEGIN,cur);
                if (begin<0) break;

                var end = s.IndexOf(MARK_BITMAP_END,begin);
                if (end < 0) break;

                var hash_ini = s.Substring(cur,begin-cur);
                var hash = IniUtil.GetValue("hash",hash_ini);

                var bmpstart  = begin+MARK_BITMAP_BEGIN.Length;
                var bmpsize   = end - bmpstart;
                var bmpbase64 = s.Substring(bmpstart,bmpsize).Trim();
                var bmp = BitmapUtil.FromBase64(bmpbase64);
                if (bmp!=null)
                {
                    dic.Add(hash,bmp);
                }
                cur = end + MARK_BITMAP_END.Length;
            }

            return dic;

        }

        public partial class state_chart
        {
            public void InitPsggStateChart(string  s)
            {
                var ht = IniUtil.CreateHashtable(s);
                var sheet = IniUtil.GetValueFromHashtable("sheet",ht);
                if (sheet != G.sheetchart) throw new SystemException("{903CB1C8-CF93-4A41-AE46-17453DEBA9F0}");

                var nameid_list_array = IniUtil.GetValueFromHashtable("nameid_list",ht).Split(',');
                nameid_list = new List<string>(nameid_list_array);

                var stateid_list_array = IniUtil.GetValueFromHashtable("stateid_list",ht).Split(',');
                stateid_list = new List<string>(stateid_list_array);

                max_name_id  = IniUtil.GetParsedValueFromHashtable<int>("max_name_id",ht);
                max_state_id = IniUtil.GetParsedValueFromHashtable<int>("max_state_id",ht);

                var id_name_dic_ht = (Hashtable)ht["id_name_dic"];
                id_name_dic = new Dictionary<string, string>();
                foreach(var k in id_name_dic_ht.Keys) id_name_dic.Add(k.ToString(), id_name_dic_ht[k].ToString());
                
                var id_state_dic_ht = (Hashtable)ht["id_state_dic"];
                id_state_dic = new Dictionary<string, string>();
                foreach(var k in id_state_dic_ht.Keys) id_state_dic.Add(k.ToString(), id_state_dic_ht[k].ToString());

                id_state_data_dic = new Dictionary<string, Dictionary<string, string>>();
                foreach(var stateid in stateid_list)
                {
                    var state_data_ht = (Hashtable)ht[stateid];
                    if (state_data_ht== null) continue;

                    var dic = new Dictionary<string,string>();
                    foreach(var nameid in nameid_list)
                    {
                        if (!state_data_ht.ContainsKey(nameid))  continue;
                        var val = state_data_ht[nameid];
                        if (val!=null)
                        { 
                            dic.Add(nameid,val.ToString());
                        }
                    }

                    id_state_data_dic.Add(stateid,dic);
                }
            }
            public void SetHashBmpDic(Dictionary<string,Bitmap> dic)
            {
                hash_bmp_dic = dic;
            }
        }
    }
}
